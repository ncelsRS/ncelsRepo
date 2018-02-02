using Kendo.Mvc.Extensions;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Prism.ViewModels.OBK.OP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK_OP
{
    public class OPProgramController : Controller
    {
        private readonly ncelsEntities repo;

        const int programStageId = 16; // in dbo.OBK_Ref_Stages

        public OPProgramController()
        {
            repo = new ncelsEntities();
        }

        [HttpGet]
        public ActionResult Program(Guid declarationId)
        {
            var stage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == declarationId && x.StageId == programStageId);
            if (stage == null)
            {
                stage = new OBK_AssessmentStage
                {
                    Id = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    DeclarationId = declarationId,
                    StageId = programStageId,
                    StageStatusId = repo.OBK_Ref_StageStatus.Where(x => x.Code == "OPProgramNew").Select(x => x.Id).FirstOrDefault()
                };
                repo.OBK_AssessmentStage.Add(stage);
                repo.SaveChanges();
                var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
                var members = repo.OBK_OP_Commission
                    .Where(x => x.DeclarationId == declarationId)
                    .Select(x => new { x.RoleId, x.EmployeeId })
                    .ToList();
                members.ForEach(member =>
                {
                    var executor = new OBK_AssessmentStageExecutors
                    {
                        AssessmentStageId = stage.Id,
                        ExecutorId = member.EmployeeId,
                        ExecutorType = member.RoleId == new Guid("3935ad57-dea8-4d41-bb94-c99bc56973df")
                            ? CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                            : CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                    };
                    repo.OBK_AssessmentStageExecutors.Add(executor);
                });
                repo.SaveChanges();
            }
            return PartialView(declarationId);
        }

        [HttpGet]
        public ActionResult LoadProgram(Guid declarationId)
        {
            var res = repo.OBK_AssessmentDeclarationProgram
                .FirstOrDefault(x => x.DeclarationId == declarationId)
                .ToVM();

            var stage = repo.OBK_AssessmentStage
                .Where(x => x.DeclarationId == declarationId && x.StageId == 16)
                .Select(x => new { x.Id, StatusCode = x.OBK_Ref_StageStatus.Code })
                .FirstOrDefault();
            res.StatusCode = stage.StatusCode;
            var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
            var statusesForExecutorType1 = new[] { "OPProgramNew", "OPProgramSigned", "OPProgramConfirmed", "OPProgramInReWork" };
            var statusesForExecutorType2 = new[] { "OPProgramInConfirm" };
            res.IsExecutor = repo.OBK_AssessmentStageExecutors
                .Any(x => x.AssessmentStageId == stage.Id
                    && x.ExecutorId == currentEmployeeId
                    && x.ExecuteResult == null
                    && ((statusesForExecutorType1.Contains(res.StatusCode) && x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING)
                        || (statusesForExecutorType2.Contains(res.StatusCode) && (x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR))));
            res.Attach = FileHelper.GetAttachListEdit(repo, declarationId.ToString(), "assessmentDeclarationOPProgram")
                .ToDataSourceResult(new Kendo.Mvc.UI.DataSourceRequest());
            repo.Dispose();
            return Json(new { isSuccess = true, data = res }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProgram(AssessmentProgramVM program)
        {
            var oldProgram = repo.OBK_AssessmentDeclarationProgram.FirstOrDefault(x => x.DeclarationId == program.DeclarationId);
            if (oldProgram != null)
            {
                program.Id = oldProgram.Id;
                repo.OBK_AssessmentDeclarationProgram.Remove(oldProgram);
            }
            program.Id = Guid.NewGuid();
            repo.OBK_AssessmentDeclarationProgram.Add(program.ToDB());
            var stage = repo.OBK_AssessmentStage.FirstOrDefault(x => x.DeclarationId == program.DeclarationId && x.StageId == programStageId);
            stage.StageStatusId = repo.OBK_Ref_StageStatus.Where(x => x.Code == "OPProgramSigned").Select(x => x.Id).FirstOrDefault();
            repo.SaveChanges();
            return Json(new { isSuccess = true, data = program }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendToWork(Guid declarationId)
        {
            var stage = repo.OBK_AssessmentStage
                .FirstOrDefault(x => x.StageId == programStageId && x.DeclarationId == declarationId);
            if (stage == null)
                return Json(new { isError = true, data = "Документ прошел не все стадии, выберите согласующих" });
            stage.StageStatusId = repo.OBK_Ref_StageStatus
                .Where(x => x.Code == "OPProgramInConfirm")
                .Select(x => x.Id)
                .FirstOrDefault();
            repo.OBK_AssessmentStageExecutors
                .Where(x => x.AssessmentStageId == stage.Id)
                .ToList()
                .ForEach(x =>
                {
                    x.ExecuteResult = null;
                    x.ExecuteComment = null;
                    x.Date = null;
                });
            repo.SaveChanges();
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NotMeetRequirements(Guid declarationId, string comment)
        {
            try
            {
                var stage = repo.OBK_AssessmentStage
                    .FirstOrDefault(x => x.DeclarationId == declarationId && x.StageId == programStageId);
                if (stage == null) throw new ArgumentException("Stage not found");
                var executors = stage.OBK_AssessmentStageExecutors
                    .Where(x => x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                    .ToList();
                var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
                var executor = executors.FirstOrDefault(x => x.ExecutorId == currentEmployeeId);
                if (executor == null) throw new ArgumentException("Current employee is not executor");
                executor.ExecuteComment = comment;
                executor.ExecuteResult = 0;
                stage.StageStatusId = repo.OBK_Ref_StageStatus
                    .Where(x => x.Code == "OPProgramInReWork")
                    .Select(x => x.Id)
                    .FirstOrDefault();
                repo.SaveChanges();
                return Json(new { isSuccess = true, data = new { StatusCode = "OPProgramInReWork" } }, JsonRequestBehavior.AllowGet);
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
                var stage = repo.OBK_AssessmentStage
                    .FirstOrDefault(x => x.DeclarationId == declarationId && x.StageId == programStageId);
                if (stage == null) throw new ArgumentException("Stage not found");

                var executors = stage.OBK_AssessmentStageExecutors
                    .Where(x => x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                    .ToList();
                var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
                var executor = executors.FirstOrDefault(x => x.ExecutorId == currentEmployeeId);
                if (executor == null) throw new ArgumentException("Current employee is not executor");
                executor.ExecuteComment = comment;
                executor.ExecuteResult = 1;
                executor.Date = DateTime.Now;
                repo.SaveChanges();

                // Если не все еще подтвердили, просто возвращаем результат
                bool isAllConfirmed = executors.All(x => x.ExecuteResult == 1);
                if (!isAllConfirmed)
                    return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);

                // Если подтвердили все - двигаем заявку дальше на заполнение протоколов и заполнение отчета ОП

                stage.StageStatusId = repo.OBK_Ref_StageStatus
                    .Where(x => x.Code == "OPProgramConfirmed")
                    .Select(x => x.Id)
                    .FirstOrDefault();

                var protocolList = new List<OBK_AssessmentProtocolOP>();
                var i = 0;
                stage.OBK_AssessmentDeclaration
                    .OBK_Contract
                    .OBK_RS_Products
                    .Where(x => x.ExpertisePlace == 1)
                    .ToList().ForEach(product =>
                    {
                        var protocol = new OBK_AssessmentProtocolOP
                        {
                            DeclarationId = stage.DeclarationId,
                            ExecuteResult = null,
                            Executor = null,
                            Number = $"{stage.OBK_AssessmentDeclaration.Number}-{(++i).ToString()}",
                            NameRu = product.NameRu,
                            NameKz = product.NameKz
                        };
                        protocolList.Add(protocol);
                    });
                repo.OBK_AssessmentProtocolOP.AddRange(protocolList);

                var reportOP = new OBK_AssessmentReportOP
                {
                    Date = null,
                    ExecuteResult = null,
                    Result = null,
                    DeclarationId = declarationId,
                    StageStatusId = repo.OBK_Ref_StageStatus.Where(x => x.Code == "OPReportNew").Select(x => x.Id).Single()
                };
                repo.OBK_AssessmentReportOP.Add(reportOP);
                repo.SaveChanges();

                var reportExecutors = new List<OBK_AssessmentReportOPExecutors>();
                repo.OBK_AssessmentStageExecutors
                    .Where(x => x.AssessmentStageId == stage.Id)
                    .ToList()
                    .ForEach(e =>
                    {
                        var newExecutor = new OBK_AssessmentReportOPExecutors
                        {
                            ReportId = reportOP.Id,
                            EmployeeId = e.ExecutorId,
                            ExecuteResult = null,
                            Type = e.ExecutorType
                        };
                        repo.OBK_AssessmentReportOPExecutors.Add(newExecutor);
                    });

                repo.SaveChanges();
                return Json(new { isSuccess = true, data = new { StatusCode = "OPProgramConfirmed" } }, JsonRequestBehavior.AllowGet);
            }
            catch (ArgumentException ex)
            {
                Response.StatusCode = 400;
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

        #region SelectExecutors
        [HttpGet]
        public ActionResult ListExecutors(Guid declarationId)
        {
            var stageId = repo.OBK_AssessmentStage
                .Where(x => x.DeclarationId == declarationId && x.StageId == programStageId)
                .Select(x => x.Id)
                .FirstOrDefault();
            var executors = repo.OBK_AssessmentStageExecutors
                .Where(x => x.AssessmentStageId == stageId && x.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR)
                .AsEnumerable()
                .Select(x => new
                {
                    x.ExecutorId,
                    x.Employee.FullName,
                    ExecuteResult = x.ExecuteResult == null ? "" :
                        x.ExecuteResult == 1 ? "Соответствует требованиям"
                        : "Не соответствует требованиям",
                    x.ExecuteComment,
                    Date = x.Date?.ToString("yyyy-MM-dd") ?? ""
                });

            return Json(new { isSuccess = true, data = executors }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListOrganization()
        {
            var orgs = repo.Units
                .Where(x => x.Id == new Guid("8f0b91f3-af29-4d3c-96d6-019cbbdfc8be"))
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
            var employeeIds = repo.OBK_AssessmentStageExecutors
                .Where(c => c.OBK_AssessmentStage.DeclarationId == declarationId
                    && c.OBK_AssessmentStage.StageId == 16)
                .Select(e => e.ExecutorId);
            var employees = repo.Employees
                .Where(x => (
                    x.Position.ParentId == unitId
                    || x.Position.Parent.ParentId == unitId
                    || x.Position.Parent.Parent.ParentId == unitId)
                    && !employeeIds.Contains(x.Id))
                .Select(x => new { x.Id, x.FullName, PositionName = x.Position.Name }).ToList();
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpsertStageExecutor(Guid employeeId, Guid declarationId)
        {
            var stageIds = repo.OBK_AssessmentStage
                .Where(x => x.StageId == programStageId && x.DeclarationId == declarationId)
                .Select(x => x.Id);

            var old = repo.OBK_AssessmentStageExecutors
                .FirstOrDefault(x =>
                    stageIds.Contains(x.AssessmentStageId)
                    && x.ExecutorId == employeeId);

            if (old == null)
            {
                var newExecutor = new OBK_AssessmentStageExecutors
                {
                    ExecutorId = employeeId,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR,
                    AssessmentStageId = stageIds.FirstOrDefault()
                };

                repo.OBK_AssessmentStageExecutors.Add(newExecutor);

                repo.SaveChanges();
            }

            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveStageExecutor(Guid executorId, Guid declarationId)
        {
            var old = repo.OBK_AssessmentStageExecutors.FirstOrDefault(x => x.ExecutorId == executorId && x.OBK_AssessmentStage.DeclarationId == declarationId && x.OBK_AssessmentStage.StageId == programStageId);
            repo.OBK_AssessmentStageExecutors.Remove(old);
            repo.SaveChanges();
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}