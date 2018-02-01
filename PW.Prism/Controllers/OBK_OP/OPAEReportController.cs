using Kendo.Mvc.Extensions;
using PW.Ncels.Database.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK_OP
{
    public class OPAEReportController : Controller
    {
        private readonly ncelsEntities repo;

        public OPAEReportController()
        {
            repo = new ncelsEntities();
        }

        // GET: OPAEReport
        [HttpGet]
        public ActionResult Index(Guid declarationId)
        {
            return PartialView(declarationId);
        }

        #region Protocols
        [HttpGet]
        public ActionResult ListProtocols(Guid declarationId)
        {
            try
            {
                var res = repo.OBK_AssessmentProtocolOP
                    .Where(x => x.DeclarationId == declarationId)
                    .ToList()
                    .Select(x => new
                    {
                        x.Id,
                        x.NameRu,
                        x.NameKz,
                        x.Number,
                        x.Executor,
                        Date = x.Date?.ToString("yyyy-MM-dd"),
                        x.ExecuteResult,
                        ExecuteResultText = x.ExecuteResult == null ? ""
                                                                : (x.ExecuteResult == 0 ? "Не соответствует"
                                                                                        : "Соответствует")
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
        public ActionResult SaveProtocol(OBK_AssessmentProtocolOP dto)
        {
            var old = repo.OBK_AssessmentProtocolOP.FirstOrDefault(x => x.Id == dto.Id);
            old.Number = dto.Number;
            old.Executor = dto.Executor;
            old.ExecuteResult = dto.ExecuteResult;
            old.Date = DateTime.Now;
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