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
using Newtonsoft.Json;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.OBK;

namespace PW.Prism.Controllers.OBKExpDocument
{
    public class OBKExpDocumentController : Controller
    {
        OBKExpDocumentRepository expRepo = new OBKExpDocumentRepository();
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult ExpDocumentView(Guid id)
        {
            var stage = expRepo.GetAssessmentStage(id);
            var model = stage.OBK_AssessmentDeclaration;


            var expDocResult = expRepo.GetStageExpDocResult(model.Id);
            if (expDocResult != null)
            {
                ViewBag.HasExpDocumentResult = true;
                var booleans = new ReadOnlyDictionaryRepository().GetUOBKCheck();
                ViewData["UObkExpertiseResult"] = new SelectList(booleans, "ExpertiseResult", "Name", expDocResult.ExpResult);
            }
            else
            {
                ViewBag.HasExpDocumentResult = false;
                var booleans = new ReadOnlyDictionaryRepository().GetUOBKCheck();
                ViewData["UObkExpertiseResult"] = new SelectList(booleans, "ExpertiseResult", "Name");
            }

            ViewData["OBKRefReasonList"] = new SelectList(expRepo.OBKRefReasonList(), "Id", "NameRu");

            ViewData["ExecutorType"] = expRepo.ExecutorType(model.Id);
            ViewData["SignExpDocument"] = expRepo.checkSignData(stage.Id);


            return PartialView(model);
        }

        public ActionResult GetMotivationRefuse(Guid declarationId)
        {
            var refuse = expRepo.GetMotivationRefuse(declarationId);
            if (refuse != null)
            {
                return Json(new
                {
                    success = true,
                    MotivationRefuse = new
                    {
                        motivationRefuseRu = refuse.ExpReasonNameRu,
                        motivationRefuseKz = refuse.ExpReasonNameKz,
                        reasonId = refuse.RefReasonId
                    }
                });
            }
            else
            {
                return Json(new { success = false });
            }

        }

        public ActionResult SaveMotivationRefuse(int? OBKRefReason, string motivationRefuseRu, string motivationRefuseKz,
            Guid declarationId, Guid? OBK_StageExpDocumentId)
        {
            var stageExpDocumentId = expRepo.SaveMotivationRefuse(OBKRefReason, motivationRefuseRu, motivationRefuseKz,
                declarationId, OBK_StageExpDocumentId);
            return Json(new { success = true, OBK_StageExpDocumentId = stageExpDocumentId });
        }

        public ActionResult ViewMotivationRefuse(Guid declarationId)
        {
            return PartialView("MotivationRefuseTemplate", declarationId);
        }

        public ActionResult PrintMotivationRefuse(Guid declarationId, bool view)
        {
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/OBK/OBKMotivationRefuse.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }
                report.Dictionary.Variables["assessmentDeclarationId"].ValueObject = declarationId;
                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }

            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;

            string name = "Мотивационный отказ" + DateTime.Now.ToString() + ".pdf";

            if (view)
            {
                return new FileStreamResult(stream, "application/pdf");

            }
            else
            {
                return File(stream, "application/pdf", name);
            }

        }

        [HttpPost]
        public ActionResult SaveExpDocument(bool expResult, Guid modelId)
        {
            expRepo.SaveExpDocumentResult(expResult, modelId);
            return Json(new { isSuccess = true });
        }

        public ActionResult GetSaveExpDoc(OBK_StageExpDocument expData)
        {
            if (expData.ExpResult)
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
                    series.RefReasonId = expData.RefReasonId;
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
                        RefReasonId = expData.RefReasonId,
                        ExpApplication = true
                    };
                    new SafetyAssessmentRepository().SaveExpDocument(expDoc);
                }
            }
            else
            {
                var series = new SafetyAssessmentRepository().GetStageExpDocument((Guid)expData.AssessmentDeclarationId);
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
                    series.RefReasonId = expData.RefReasonId;
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
                        RefReasonId = expData.RefReasonId,
                        ExpApplication = true
                    };
                    new SafetyAssessmentRepository().SaveExpDocument(expDoc);
                }
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

        public ActionResult ExpDocumentExportFileStream(string productSeriesId, Guid id)
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

            return new FileStreamResult(stream, "application/pdf");
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

        public ActionResult SaveSignedExpDocumentParty(Guid id, string signedData)
        {
            var message = expRepo.SaveSignExpDocParty(id, signedData);
            return Json(new { message });
        }

        public ActionResult ReturnToExecutor(Guid id)
        {
            var result = expRepo.GetReturnToExecutor(id, CodeConstManager.STAGE_OBK_EXPERTISE_DOC);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSelectionPlace(DateTime selectionDate, DateTime selectionTime, string selectionAddress, Guid? assessmentId)
        {
            expRepo.SavePlace(selectionDate, selectionTime, selectionAddress, assessmentId);
            return Json(true);
        }

        public ActionResult DocumentRead(Guid id)
        {
            OBK_ActReception reception = db.OBK_ActReception.Find(id);
            OBKCertificateFileModel fileModel = new OBKCertificateFileModel();

            if (reception.AttachPath != null)
            {
                fileModel.AttachPath = reception.AttachPath;
                fileModel.AttachFiles = UploadHelper.GetFilesInfo(fileModel.AttachPath.ToString(), false);
            }
            else
            {
                fileModel.AttachPath = FileHelper.GetObjectPathRoot();
                fileModel.AttachFiles = UploadHelper.GetFilesInfo(fileModel.AttachPath.ToString(), false);
            }

            return Content(JsonConvert.SerializeObject(fileModel, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

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

        public ActionResult DeclarationResponse(Guid id)
        {
            var stage = expRepo.GetAssessmentStage(id);
            var model = stage.OBK_AssessmentDeclaration;
            var expDocResult = expRepo.GetStageExpDocResult(model.Id);
            ViewBag.ExpDocResult = expDocResult.ExpResult;
            //основание
            var reasons = new SafetyAssessmentRepository().GetRefReasons();
            ViewData["UObkReasons"] = new SelectList(reasons, "Id", "Name");

            if (stage.OBK_Ref_StageStatus.Code != "inWork")
            {
                ViewBag.ExecutorIsSign = true;
            }


            if (!expDocResult.ExpResult)
            {
                var result = model.OBK_StageExpDocument.FirstOrDefault(e => e.AssessmentDeclarationId == model.Id);
                if (result != null)
                {
                    ViewData["UObkReasons"] = new SelectList(reasons, "Id", "Name", result.RefReasonId);
                    ViewBag.ExpRejectReasonNameRu = result.ExpReasonNameRu;
                    ViewBag.ExpRejectReasonNameKz = result.ExpReasonNameKz;
                }
            }

            return PartialView(model);
        }

        public virtual ActionResult GetProducts(Guid id)
        {
            var declarant = expRepo.GetAssessmentDeclaration(id);
            if (declarant == null)
                return Json(new { isSuccess = false });

            //есть ли результат
            var obkStageExpDocumentResult = new SafetyAssessmentRepository().GetStageExpDocumentResult(declarant.Id);
            if (obkStageExpDocumentResult != null)
            {
                // если результат положительный
                if (obkStageExpDocumentResult.ExpResult)
                {
                    var results = new List<OBK_Procunts_Series>();
                    foreach (var product in declarant.OBK_Contract.OBK_RS_Products)
                    {
                        foreach (var productSeries in product.OBK_Procunts_Series)
                        {
                            var series = new OBK_Procunts_Series();
                            series.Id = productSeries.Id;
                            series.Series = productSeries.Series;
                            series.SeriesStartdate = productSeries.SeriesStartdate;
                            series.SeriesEndDate = productSeries.SeriesEndDate;
                            series.SeriesParty = productSeries.SeriesParty;
                            series.SeriesShortNameRu = productSeries.sr_measures.short_name;
                            series.SeriesNameRu = productSeries.Series + ", годен до " + productSeries.SeriesEndDate +
                                                  ", партия " + productSeries.SeriesParty + " " +
                                                  productSeries.SeriesShortNameRu;

                            series.NameRu = product.NameRu;
                            series.NameKz = product.NameKz;
                            series.ProducerNameRu = product.ProducerNameRu;
                            series.ProducerNameKz = product.ProducerNameKz;
                            series.CountryNameRu = product.CountryNameRu;
                            series.CountryNameKz = product.CountryNameKZ;

                            var obkStageExpDocumentSeries =
                                new SafetyAssessmentRepository().GetStageExpDocument(productSeries.Id);
                            if (obkStageExpDocumentSeries != null)
                            {
                                series.ExpId = obkStageExpDocumentSeries.Id;
                                series.ProductSeriesId = obkStageExpDocumentSeries.ProductSeriesId;
                                series.ExpResult = obkStageExpDocumentSeries.ExpResult ? "True" : "False";
                                series.ExpResultTitle = obkStageExpDocumentSeries.ExpResult
                                    ? "Соответствует требованиям"
                                    : "Не соответствует требованиям";
                                series.ExpStartDate =
                                    string.Format("{0:dd.MM.yyyy}", obkStageExpDocumentSeries.ExpStartDate);
                                series.ExpEndDate =
                                    string.Format("{0:dd.MM.yyyy}", obkStageExpDocumentSeries.ExpEndDate);
                                series.ExpReasonNameRu = obkStageExpDocumentSeries.ExpReasonNameRu;
                                series.ExpReasonNameKz = obkStageExpDocumentSeries.ExpReasonNameKz;
                                series.ExpProductNameRu = obkStageExpDocumentSeries.ExpProductNameRu;
                                series.ExpProductNameKz = obkStageExpDocumentSeries.ExpProductNameKz;
                                series.ExpNomenclatureRu = obkStageExpDocumentSeries.ExpNomenclatureRu;
                                series.ExpNomenclatureKz = obkStageExpDocumentSeries.ExpNomenclatureKz;
                                series.ExpAddInfoRu = obkStageExpDocumentSeries.ExpAddInfoRu;
                                series.ExpAddInfoKz = obkStageExpDocumentSeries.ExpAddInfoKz;
                                series.ExpConclusionNumber = obkStageExpDocumentSeries.ExpConclusionNumber;
                                series.ExpBlankNumber = obkStageExpDocumentSeries.ExpBlankNumber;
                                series.ExpApplication = obkStageExpDocumentSeries.ExpApplication;
                                series.ExpApplicationNumber = obkStageExpDocumentSeries.ExpApplicationNumber;
                            }
                            else
                            {
                                series.ExpConclusionNumber = expRepo.GenerateNumber(id, productSeries.Id);
                            }
                            results.Add(series);
                        }
                    }
                    return Json(new { isSuccess = true, results });
                }
                else
                {
                    var obkStageExpDocumentSeries =
                        new SafetyAssessmentRepository().GetStageExpDocument(declarant.Id);
                    if (obkStageExpDocumentSeries != null)
                    {
                        var series = new OBK_Procunts_Series();
                        series.ExpId = obkStageExpDocumentSeries.Id;
                        series.ProductSeriesId = obkStageExpDocumentSeries.ProductSeriesId;
                        series.ExpResult = obkStageExpDocumentSeries.ExpResult ? "True" : "False";
                        series.ExpResultTitle = obkStageExpDocumentSeries.ExpResult
                            ? "Соответствует требованиям"
                            : "Не соответствует требованиям";
                        series.ExpStartDate =
                            string.Format("{0:dd.MM.yyyy}", obkStageExpDocumentSeries.ExpStartDate);
                        series.ExpEndDate =
                            string.Format("{0:dd.MM.yyyy}", obkStageExpDocumentSeries.ExpEndDate);
                        series.ExpReasonNameRu = obkStageExpDocumentSeries.ExpReasonNameRu;
                        series.ExpReasonNameKz = obkStageExpDocumentSeries.ExpReasonNameKz;
                        series.ExpProductNameRu = obkStageExpDocumentSeries.ExpProductNameRu;
                        series.ExpProductNameKz = obkStageExpDocumentSeries.ExpProductNameKz;
                        series.ExpNomenclatureRu = obkStageExpDocumentSeries.ExpNomenclatureRu;
                        series.ExpNomenclatureKz = obkStageExpDocumentSeries.ExpNomenclatureKz;
                        series.ExpAddInfoRu = obkStageExpDocumentSeries.ExpAddInfoRu;
                        series.ExpAddInfoKz = obkStageExpDocumentSeries.ExpAddInfoKz;
                        series.ExpConclusionNumber = obkStageExpDocumentSeries.ExpConclusionNumber;
                        series.ExpBlankNumber = obkStageExpDocumentSeries.ExpBlankNumber;
                        series.ExpApplication = obkStageExpDocumentSeries.ExpApplication;
                        series.ExpApplicationNumber = obkStageExpDocumentSeries.ExpApplicationNumber;

                        return Json(new { isSuccess = true, series });
                    }
                    else
                    {
                        return Json(new { isSuccess = true });
                    }
                }

            }
            return Json(new { isSuccess = true });

        }








    }
}