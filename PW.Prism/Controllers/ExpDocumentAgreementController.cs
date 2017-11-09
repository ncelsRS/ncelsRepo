using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers
{
    public class ExpDocumentAgreementController : Controller
    {
        private ncelsEntities _db = new ncelsEntities();
        public ActionResult Index(string filterId = "newTasks")
        {
            ViewBag.FilterId = filterId;
            return PartialView(Guid.NewGuid());
        }

        public ActionResult ListContract([DataSourceRequest] DataSourceRequest request)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var excludeDocs = new[] { Dictionary.ExpAgreedDocType.Contract, Dictionary.ExpAgreedDocType.DirectionToPay };
            var query = _db.EXP_TaskRegistryView.Where(e => e.ExecutorId == userId && !excludeDocs.Contains(e.DocumentTypeCode));
            return Json(query.ToDataSourceResult(request));
        }

        public ActionResult GetDeclarationInfoByFinalDoc(Guid finalDocId)
        {
            string expertiseController = "";
            var finalDocInfo = _db.EXP_ExpertiseStageDosage.Where(e => e.Id == finalDocId)
                .Select(e => new
                {
                    e.StageId,
                    DicStageId = e.EXP_ExpertiseStage.StageId,
                    e.EXP_DrugDosage.EXP_DrugDeclaration.Number
                }).FirstOrDefault();
            switch (finalDocInfo.DicStageId)
            {
                case CodeConstManager.STAGE_PRIMARY:
                    expertiseController = "DrugPrimary";
                    break;
                case CodeConstManager.STAGE_PHARMACEUTICAL:
                    expertiseController = "Pharmaceutical";
                    break;
                case CodeConstManager.STAGE_PHARMACOLOGICAL:
                    expertiseController = "Pharmacological";
                    break;
                case CodeConstManager.STAGE_ANALITIC:
                    expertiseController = "DrugAnalitic";
                    break;
            }
            return Json(new
            {
                Id= finalDocInfo.StageId,
                finalDocInfo.Number,
                Controller= expertiseController
            }, JsonRequestBehavior.AllowGet);
        }
    }
}