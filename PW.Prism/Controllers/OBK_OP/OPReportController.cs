using Kendo.Mvc.Extensions;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK_OP
{
    public class OPReportController : Controller
    {
        private readonly ncelsEntities repo;

        public OPReportController()
        {
            repo = new ncelsEntities();
        }

        // GET: OPReport
        public ActionResult Index(Guid declarationId)
        {
            return PartialView(declarationId);
        }

        public ActionResult LoadReportOP(Guid declarationId)
        {
            try
            {
                var currentUserId = UserHelper.GetCurrentEmployee().Id;
                var statusCode = repo.OBK_AssessmentReportOP
                    .Where(x => x.DeclarationId == declarationId)
                    .Select(x => x.OBK_Ref_StageStatus.Code)
                    .FirstOrDefault();
                var statusCodesForExecutorType1 = new[] { "OPReportNew", "OPReportConfirmed", "OPReportOnReWork" };
                var statusCodesForExecutorType2 = new[] { "OPReportInConfirm" };

                var res = repo.OBK_AssessmentReportOP
                    .Where(x => x.DeclarationId == declarationId)
                    .ToList()
                    .Select(x => new
                    {
                        x.Id,
                        x.DeclarationId,
                        StatusCode = x.OBK_Ref_StageStatus.Code,
                        StatusName = x.OBK_Ref_StageStatus.NameRu,
                        Date = x.Date?.ToString("dd.MM.yyyy"),
                        x.Result,
                        ExecuteResultCode = x.ExecuteResult,
                        ExecuteResultName = x.ExecuteResult == null ? "" :
                            x.ExecuteResult == 1 ? "Соответствует требованиям"
                            : "Не соответствует требованиям",
                        IsExecutor = repo.OBK_AssessmentReportOPExecutors
                            .Any(y => y.ReportId == x.Id && y.EmployeeId == currentUserId && y.ExecuteResult == null
                                && ((statusCodesForExecutorType1.Contains(statusCode) && y.Type == 1)
                                    || (statusCodesForExecutorType2.Contains(statusCode) && y.Type == 2))),
                        Attach = FileHelper
                            .GetAttachListEdit(repo, declarationId.ToString(), "assessmentDeclarationOPReport", true)
                            .ToDataSourceResult(new Kendo.Mvc.UI.DataSourceRequest())
                    })
                    .FirstOrDefault();
                if (res == null) throw new ArgumentException("Report not found");

                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SendToWork(Guid declarationId, int executeResult, string result)
        {
            try
            {
                var report = repo.OBK_AssessmentReportOP
                    .FirstOrDefault(x => x.DeclarationId == declarationId);
                if (report == null) throw new ArgumentException("ReportOP not found in DB");
                report.StageStatusId = repo.OBK_Ref_StageStatus
                    .Where(x => x.Code == "OPReportInConfirm")
                    .Select(x => x.Id)
                    .FirstOrDefault();

                report.ExecuteResult = executeResult;
                report.Result = result;

                repo.OBK_AssessmentReportOPExecutors
                    .Where(x => x.ReportId == report.Id)
                    .ToList()
                    .ForEach(executor =>
                    {
                        executor.ExecuteResult = null;
                        executor.Comment = null;
                        executor.Date = null;
                    });
                repo.SaveChanges();
                return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult MeetRequirements(Guid declarationId, string comment)
        {
            try
            {
                var report = repo.OBK_AssessmentReportOP.FirstOrDefault(x => x.DeclarationId == declarationId);
                if (report == null) throw new ArgumentException("Report not found");
                var executors = repo.OBK_AssessmentReportOPExecutors.Where(x => x.ReportId == report.Id && x.Type == 2).ToList();
                var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
                var executor = executors.FirstOrDefault(x => x.EmployeeId == currentEmployeeId);
                if (executor == null) throw new ArgumentException("Current employee is not executor");
                executor.Comment = comment;
                executor.ExecuteResult = 1;
                executor.Date = DateTime.Now;

                if (executors.All(x => x.ExecuteResult == 1))
                {
                    report.StageStatusId = repo.OBK_Ref_StageStatus.Where(x => x.Code == "OPReportConfirmed").Select(x => x.Id).Single();
                }

                repo.SaveChanges();
                return Json(new { isSuccess = true, data = new { StatusCode = "OPReportConfirmed" } }, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult NotMeetRequirements(Guid declarationId, string comment)
        {
            try
            {
                var report = repo.OBK_AssessmentReportOP.FirstOrDefault(x => x.DeclarationId == declarationId);
                if (report == null) throw new ArgumentException("Report not found");
                var executors = repo.OBK_AssessmentReportOPExecutors.Where(x => x.ReportId == report.Id && x.Type == 2).ToList();
                var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
                var executor = executors.FirstOrDefault(x => x.EmployeeId == currentEmployeeId);
                if (executor == null) throw new ArgumentException("Current employee is not executor");
                executor.Comment = comment;
                executor.ExecuteResult = 0;
                executor.Date = DateTime.Now;

                report.StageStatusId = repo.OBK_Ref_StageStatus.Where(x => x.Code == "OPReportOnReWork").Select(x => x.Id).Single();

                repo.SaveChanges();
                return Json(new { isSuccess = true, data = new { StatusCode = "OPReportOnReWork" } }, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #region SelectExecutors
        public ActionResult ListExecutors(Guid declarationId)
        {
            try
            {
                var res = repo.OBK_AssessmentReportOPExecutors
                    .Where(x => x.OBK_AssessmentReportOP.DeclarationId == declarationId && x.Type == 2)
                    .ToList()
                    .Select(x => new
                    {
                        x.Id,
                        x.ReportId,
                        x.EmployeeId,
                        x.Employee.FullName,
                        x.Comment,
                        x.ExecuteResult,
                        Date = x.Date?.ToString("dd.MM.yyyy"),
                        ExecuteResultName = x.ExecuteResult == null ? "" :
                            x.ExecuteResult == 1 ? "Соответствует требованиям"
                            : "Не соответствует требованиям"
                    });
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListOrganization()
        {
            var orgs = repo.Units
                .Where(x => x.Code == OrganizationConsts.NCELS || x.Code == OrganizationConsts.FilialsParent) // Только НЦЭЛС
                .Select(x => new { x.Id, x.Name }).ToList();
            return Json(orgs, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListUnits(Guid organizationId)
        {
            var units = repo.Units
                .Where(x => x.ParentId == organizationId)
                .Select(x => new { x.Id, x.Name }).ToList();
            return Json(units, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListEmployees(Guid declarationId, Guid unitId)
        {
            var currentUserId = UserHelper.GetCurrentEmployee().Id;
            var employeeIds = repo.OBK_AssessmentReportOPExecutors
                .Where(c => c.OBK_AssessmentReportOP.DeclarationId == declarationId)
                .Select(e => e.EmployeeId);
            var employees = repo.Employees
                .Where(x => (
                    x.Position.ParentId == unitId
                    || x.Position.Parent.ParentId == unitId
                    || x.Position.Parent.Parent.ParentId == unitId)
                    && !employeeIds.Contains(x.Id) && x.Id != currentUserId)
                .Select(x => new { x.Id, x.FullName, PositionName = x.Position.Name }).ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpsertStageExecutor(Guid employeeId, Guid declarationId)
        {
            var reportId = repo.OBK_AssessmentReportOP.Where(x => x.DeclarationId == declarationId).Select(x => x.Id).FirstOrDefault();

            var old = repo.OBK_AssessmentReportOPExecutors
                .FirstOrDefault(x => x.ReportId == reportId && x.EmployeeId == employeeId);

            if (old == null)
            {
                var newExecutor = new OBK_AssessmentReportOPExecutors
                {
                    ReportId = reportId,
                    EmployeeId = employeeId,
                    Type = 2
                };

                repo.OBK_AssessmentReportOPExecutors.Add(newExecutor);

                repo.SaveChanges();
            }

            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveStageExecutor(Guid executorId, Guid declarationId)
        {
            var old = repo.OBK_AssessmentReportOPExecutors
                .FirstOrDefault(x => x.OBK_AssessmentReportOP.DeclarationId == declarationId && x.EmployeeId == executorId);
            repo.OBK_AssessmentReportOPExecutors.Remove(old);
            repo.SaveChanges();
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Dictionaries
        [HttpGet]
        public ActionResult ResultTypes()
        {
            var res = new[] {
                new { Value = 0, Text = "Не соответствует" },
                new { Value = 1, Text = "Соответствует" }
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}