using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                            .Any(y => y.ReportId == x.Id && y.EmployeeId == UserHelper.GetCurrentEmployee().Id && y.ExecuteResult == null)
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

        public ActionResult LoadExecutors(Guid declarationId)
        {
            try
            {
                var res = repo.OBK_AssessmentReportOPExecutors
                    .Where(x => x.OBK_AssessmentReportOP.DeclarationId == declarationId)
                    .Select(x => new
                    {
                        x.Id,
                        x.ReportId,
                        Name = x.Employee.FullName,
                        x.Comment,
                        x.ExecuteResult,
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