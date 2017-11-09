using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Aspose.Cells;
using Aspose.Cells.Rendering;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.OBK;
using PW.Prism.Controllers.OBK;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers.OBKExpDocument
{
    public class OBKExpDocumentController : Controller
    {
        OBKExpDocumentRepository expRepo = new OBKExpDocumentRepository();


        public ActionResult ExpDocumentView(Guid id)
        {
            var stage = expRepo.GetAssessmentStage(id);
            var model = stage.OBK_AssessmentDeclaration;

            var expDocResult = expRepo.GetStageExpDocResult(model.Id);
            if (expDocResult != null) {
                var booleans = new ReadOnlyDictionaryRepository().GetUOBKCheck();
                ViewData["UObkExpertiseResult"] = new SelectList(booleans, "ExpertiseResult", "Name", expDocResult.ExpResult);
            }
            else {
                var booleans = new ReadOnlyDictionaryRepository().GetUOBKCheck();
                ViewData["UObkExpertiseResult"] = new SelectList(booleans, "ExpertiseResult", "Name");
            }
            
            //// номерклатура
            //var nomeclature = new AssessmentStageRepository().GetRefNomenclature();
            //ViewData["UObkNomenclature"] = new SelectList(nomeclature, "Id", "Name");
            ////основание
            //var reasons = new SafetyAssessmentRepository().GetRefReasons();
            //ViewData["UObkReasons"] = new SelectList(reasons, "ExpertiseResult", "Name");

            return PartialView(model);
        }

        public ActionResult ExpDocumentDeclarationResponse(Guid id)
        {
            var stage = expRepo.GetAssessmentStage(id);
            var model = stage.OBK_AssessmentDeclaration;
            var expDocResult = expRepo.GetStageExpDocResult(model.Id);
            model.ExpDocumentResult = expDocResult.ExpResult;
            //основание
            var reasons = new SafetyAssessmentRepository().GetRefReasons();
            ViewData["UObkReasons"] = new SelectList(reasons, "Id", "Name");
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SaveExpDocument(bool expResult, Guid modelId)
        {
            expRepo.SaveExpDocumentResult(expResult, modelId);
            return Json(new { isSuccess = true });
        }

        public ActionResult GetSaveExpDoc(OBK_StageExpDocument expData)
        {
            var series = new SafetyAssessmentRepository().GetStageExpDocument(expData.ProductSeriesId);
            if (series != null)
            {
                series.AssessmentDeclarationId = expData.AssessmentDeclarationId;
                series.ProductSeriesId = expData.ProductSeriesId;
                series.ExpResult = expData.ExpResult;
                series.ExpStartDate = expData.ExpStartDate;
                series.ExpEndDate = expData.ExpEndDate;
                series.ExpReasonNameRu = expData.ExpReasonNameRu;
                series.ExpReasonNameKz = expData.ExpReasonNameKz;
                series.ExpProductNameRu = expData.ExpProductNameRu;
                series.ExpProductNameKz = expData.ExpProductNameKz;
                series.ExpNomenclatureRu = expData.ExpNomenclatureRu;
                series.ExpNomenclatureKz = expData.ExpNomenclatureKz;
                series.ExpAddInfoRu = expData.ExpAddInfoRu;
                series.ExpAddInfoKz = expData.ExpAddInfoKz;
                series.ExpConclusionNumber = expData.ExpConclusionNumber;
                series.ExpBlankNumber = expData.ExpBlankNumber;
                series.ExpApplicationNumber = expData.ExpApplicationNumber;
                series.ExecutorId = UserHelper.GetCurrentEmployee().Id;
                series.ExpApplication = true;
                new SafetyAssessmentRepository().SaveExpDocument(series);
            }
            else
            {
                var expDoc = new OBK_StageExpDocument()
                {
                    Id = Guid.NewGuid(),
                    AssessmentDeclarationId = expData.AssessmentDeclarationId,
                    ProductSeriesId = expData.ProductSeriesId,
                    ExpResult = expData.ExpResult,
                    ExpStartDate = expData.ExpStartDate,
                    ExpEndDate = expData.ExpEndDate,
                    ExpReasonNameRu = expData.ExpReasonNameRu,
                    ExpReasonNameKz = expData.ExpReasonNameKz,
                    ExpProductNameRu = expData.ExpProductNameRu,
                    ExpProductNameKz = expData.ExpProductNameKz,
                    ExpNomenclatureRu = expData.ExpNomenclatureRu,
                    ExpNomenclatureKz = expData.ExpNomenclatureKz,
                    ExpAddInfoRu = expData.ExpAddInfoRu,
                    ExpAddInfoKz = expData.ExpAddInfoKz,
                    ExpConclusionNumber = expData.ExpConclusionNumber,
                    ExpBlankNumber = expData.ExpBlankNumber,
                    ExpApplicationNumber = expData.ExpApplicationNumber,
                    ExecutorId = UserHelper.GetCurrentEmployee().Id,
                    ExpApplication = true
                };
                new SafetyAssessmentRepository().SaveExpDocument(expDoc);
            }
            return Json(new { isSuccess = true });
        }

        public ActionResult ExpDocumentExportFilePdf(string productSeriesId, Guid id)
        {
            string name = "Заключение о безопасности и качества.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/OBKExpDocument.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["StageExpDocumentId"].ValueObject = Convert.ToInt32(productSeriesId);
                report.Dictionary.Variables["AssessmentDeclarationId"].ValueObject = id;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }

        public ActionResult ExpDocumentMotivRefusExportFilePdf(Guid id)
        {
            string name = "Уведомление о мотивированном отказе.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/OBK/ObkExpDocumentMotivRefus.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }
                report.Dictionary.Variables["AssessmentDeclarationId"].ValueObject = id;
                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }

        [HttpGet]
        public ActionResult GetSignExpDocument(Guid id)
        {
            var signData = expRepo.GetSignData(id);
            return Json(new { data = signData }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSignedExpDocument(Guid id, string signedData)
        {
            var message = expRepo.SaveSignExpDoc(id, signedData);
            return Json(new { message });
        }

        public ActionResult ReturnToExecutor(Guid id)
        {
            var result = expRepo.GetReturnToExecutor(id, CodeConstManager.STAGE_OBK_EXPERTISE_DOC);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActExportFilePdf(Guid id)
        {
            string name = "Акт выполненных работ.pdf";

            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/OBK/1c/ObkCertificateOfCompletion.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["AssessmentDeclarationId"].ValueObject = id;
                report.Dictionary.Variables["ContractId"].ValueObject = expRepo.GetAssessmentDeclaration(id).ContractId;
                report.Dictionary.Variables["ValueAddedTax"].ValueObject = expRepo.GetValueAddedTax();
                var totalCount = expRepo.GetContractPrice(expRepo.GetAssessmentDeclaration(id).ContractId);
                report.Dictionary.Variables["TotalCount"].ValueObject = totalCount;
                var priceText = RuDateAndMoneyConverter.CurrencyToTxtTenge(Convert.ToDouble(totalCount), false);
                report.Dictionary.Variables["TotalCountText"].ValueObject = priceText;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }
    }
}