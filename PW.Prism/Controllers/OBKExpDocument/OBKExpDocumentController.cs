﻿using System;
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
using System.Text;

namespace PW.Prism.Controllers.OBKExpDocument
{
    public class OBKExpDocumentController : Controller
    {
        OBKExpDocumentRepository expRepo = new OBKExpDocumentRepository();
        private readonly SafetyAssessmentRepository assessmentRepo = new SafetyAssessmentRepository();
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

            var stageObj = db.OBK_Ref_StageStatus.FirstOrDefault(o => o.Id == stage.StageStatusId);
            ViewData["StageStatus"] = stageObj.Code;

            return PartialView(model);
        }

        public ActionResult PartyExpDocumentView(Guid id)
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

        public ActionResult GetMotivationRefuseFields(Guid? declarationId)
        {
            return Json(new { isPreviousSaved = expRepo.GetMotivationRefuseFields(declarationId) });
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
                    if (expData.ExpApplication != true)
                    {
                        series.ExpApplicationNumber = expData.ExpApplicationNumber;
                    }
                    else
                    {
                        series.ExpApplicationNumber = null;
                    }
                    series.ExecutorId = UserHelper.GetCurrentEmployee().Id;
                    series.RefReasonId = expData.RefReasonId;
                    series.ExpApplication = expData.ExpApplication;
                    series.ExpProductShortNameRu = expData.ExpProductShortNameRu;
                    series.ExpProductShortNameKz = expData.ExpProductShortNameKz;
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
                        ExpApplicationNumber = expData.ExpApplication == false ? expData.ExpApplicationNumber : null,
                        ExecutorId = UserHelper.GetCurrentEmployee().Id,
                        RefReasonId = expData.RefReasonId,
                        ExpApplication = expData.ExpApplication,
                        ExpProductShortNameRu = expData.ExpProductShortNameRu,
                        ExpProductShortNameKz = expData.ExpProductShortNameKz
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
                        ExpApplication = true,
                    };
                    new SafetyAssessmentRepository().SaveExpDocument(expDoc);
                }
            }

            return Json(new { isSuccess = true });
        }

        public ActionResult ExpDocumentExportFilePdf(string productSeriesId, Guid id)
        {
            string name = $"Заключение о безопасности и качества.pdf";
            var serieId = Convert.ToInt32(productSeriesId);
            var expDocument = db.OBK_StageExpDocument.FirstOrDefault(o => o.ProductSeriesId == serieId);
            StiReport report = new StiReport();
            try
            {
                var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == id);

                var applicationBlankNumber = expDocument.ExpApplicationNumber != null ? expDocument.ExpApplicationNumber : "";

                if (applicationBlankNumber.Length < 6)
                {
                    for (int c = applicationBlankNumber.Length; c < 6; c++)
                    {
                        applicationBlankNumber = "0" + applicationBlankNumber;
                    }
                }

                if (CodeConstManager.OBK_SA_SERIAL.Equals(declaration.TypeId.ToString()))
                {
                    if (expDocument.ExpApplication == false)
                    {
                        report.Load(Server.MapPath("~/Reports/Mrts/OBKExpDocumentApplicationSerialPdf.mrt"));
                    }
                    else
                    {
                        report.Load(Server.MapPath("~/Reports/Mrts/OBKExpDocumentSerialPdf.mrt"));
                    }

                    var tail = " согласно приложения КЗП № {" + applicationBlankNumber + "}, серийная";
                    var dictionary = ZBKSpaceHelper.BuildName(expDocument.ExpProductNameRu + " серийный выпуск", 90, 120, 150, expDocument.ExpProductShortNameRu, tail);
                    report.Dictionary.Variables["ProductNameRu1"].ValueObject = dictionary.ContainsKey(1) ? dictionary[1] : "";
                    report.Dictionary.Variables["ProductNameRu2"].ValueObject = dictionary.ContainsKey(2) ? dictionary[2] : "";
                    report.Dictionary.Variables["ProductNameRu3"].ValueObject = dictionary.ContainsKey(3) ? dictionary[3] : "";

                    var tailKaz = " ҚҚҚ № {" + applicationBlankNumber + "} қосымшаға сәйкес, сериялы шыгарылым";
                    var dictionaryKaz = ZBKSpaceHelper.BuildName(expDocument.ExpProductNameKz + " сериялық", 60, 120, 150, expDocument.ExpProductShortNameKz, tailKaz);
                    report.Dictionary.Variables["ProductNameKz1"].ValueObject = dictionaryKaz.ContainsKey(1) ? dictionaryKaz[1] : "";
                    report.Dictionary.Variables["ProductNameKz2"].ValueObject = dictionaryKaz.ContainsKey(2) ? dictionaryKaz[2] : "";
                    report.Dictionary.Variables["ProductNameKz3"].ValueObject = dictionaryKaz.ContainsKey(3) ? dictionaryKaz[3] : "";
                    report.Dictionary.Variables["productSeriesId"].ValueObject = serieId;

                    var reportOp = db.OBK_AssessmentReportOP.FirstOrDefault(o => o.DeclarationId == id);
                    if (reportOp != null)
                    {
                        var opExecutor = db.OBK_AssessmentReportOPExecutors.OrderByDescending(o => o.Date).FirstOrDefault(o => o.ReportId == reportOp.Id);
                        if (opExecutor != null && opExecutor.Date != null)
                        {
                            report.Dictionary.Variables["OpExecutorDate"].ValueObject = ((DateTime)opExecutor.Date).ToString("dd.MM.yyyy");
                        }
                    }
                }
                else if (CodeConstManager.OBK_SA_PARTY.Equals(declaration.TypeId.ToString()))
                {
                    if (expDocument.ExpApplication == false)
                    {
                        report.Load(Server.MapPath("~/Reports/Mrts/OBKExpDocumentApplicationPartyPdf.mrt"));
                    }
                    else
                    {
                        report.Load(Server.MapPath("~/Reports/Mrts/OBKExpDocumentPartyPdf.mrt"));
                    }

                    var tail = " согласно приложения КЗП № {" + applicationBlankNumber + "}";
                    var dictionary = ZBKSpaceHelper.BuildName(expDocument.ExpProductNameRu, 90, 120, 170, expDocument.ExpProductShortNameRu, tail);
                    report.Dictionary.Variables["ProductNameRu1"].ValueObject = dictionary.ContainsKey(1) ? dictionary[1] : "";
                    report.Dictionary.Variables["ProductNameRu2"].ValueObject = dictionary.ContainsKey(2) ? dictionary[2] : "";
                    report.Dictionary.Variables["ProductNameRu3"].ValueObject = dictionary.ContainsKey(3) ? dictionary[3] : "";

                    var tailKaz = " ҚҚҚ № {" + applicationBlankNumber + "} қосымшаға сәйкес";
                    var dictionaryKaz = ZBKSpaceHelper.BuildName(expDocument.ExpProductNameKz, 60, 120, 160, expDocument.ExpProductShortNameKz, tailKaz);
                    report.Dictionary.Variables["ProductNameKz1"].ValueObject = dictionaryKaz.ContainsKey(1) ? dictionaryKaz[1] : "";
                    report.Dictionary.Variables["ProductNameKz2"].ValueObject = dictionaryKaz.ContainsKey(2) ? dictionaryKaz[2] : "";
                    report.Dictionary.Variables["ProductNameKz3"].ValueObject = dictionaryKaz.ContainsKey(3) ? dictionaryKaz[3] : "";
                    report.Dictionary.Variables["productSeriesId"].ValueObject = serieId;
                }
                else
                {
                    if (expDocument.ExpApplication == false)
                    {
                        report.Load(Server.MapPath("~/Reports/Mrts/OBKExpDocumentApplicationPdf.mrt"));
                    }
                    else
                    {
                        report.Load(Server.MapPath("~/Reports/Mrts/OBKExpDocumentPdf.mrt"));
                    }

                    var tail = " согласно приложения КЗП № {" + applicationBlankNumber + "}";
                    var dictionary = ZBKSpaceHelper.BuildName(expDocument.ExpProductNameRu, 90, 120, 170, expDocument.ExpProductShortNameRu, tail);
                    report.Dictionary.Variables["ProductNameRu1"].ValueObject = dictionary.ContainsKey(1) ? dictionary[1] : "";
                    report.Dictionary.Variables["ProductNameRu2"].ValueObject = dictionary.ContainsKey(2) ? dictionary[2] : "";
                    report.Dictionary.Variables["ProductNameRu3"].ValueObject = dictionary.ContainsKey(3) ? dictionary[3] : "";

                    var tailKaz = " ҚҚҚ № {" + applicationBlankNumber + "} қосымшаға сәйкес";
                    var dictionaryKaz = ZBKSpaceHelper.BuildName(expDocument.ExpProductNameKz, 60, 120, 160, expDocument.ExpProductShortNameKz, tailKaz);
                    report.Dictionary.Variables["ProductNameKz1"].ValueObject = dictionaryKaz.ContainsKey(1) ? dictionaryKaz[1] : "";
                    report.Dictionary.Variables["ProductNameKz2"].ValueObject = dictionaryKaz.ContainsKey(2) ? dictionaryKaz[2] : "";
                    report.Dictionary.Variables["ProductNameKz3"].ValueObject = dictionaryKaz.ContainsKey(3) ? dictionaryKaz[3] : "";
                }

                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["StageExpDocumentId"].ValueObject = Convert.ToInt32(productSeriesId);
                report.Dictionary.Variables["AssessmentDeclarationId"].ValueObject = id;
                report.Dictionary.Variables["StartMonthRu"].ValueObject = MonthHelper.getMonthRu(expDocument.ExpStartDate);
                report.Dictionary.Variables["StartMonthKz"].ValueObject = MonthHelper.getMonthKz(expDocument.ExpStartDate);

                if (expDocument.ExpEndDate != null)
                {
                    expDocument.ExpEndDate = ((DateTime)expDocument.ExpEndDate).AddMonths(1);
                }

                report.Dictionary.Variables["EndMonthRu"].ValueObject = MonthHelper.getMonthRu(expDocument.ExpEndDate);
                report.Dictionary.Variables["EndMonthKz"].ValueObject = MonthHelper.getMonthKz(expDocument.ExpEndDate);

                report.Dictionary.Variables["addInfo"].ValueObject = assessmentRepo.FormExpAdditionalInfo(declaration);
                report.Dictionary.Variables["addInfoKz"].ValueObject = assessmentRepo.FormExpAdditionalInfo(declaration, true);

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

        /// <summary>
        /// Печатная форма отказа
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ExpDocumentRejectFormPdf(int productSeriesId, Guid id, int pid)
        {
            var report = new StiReport();
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Mrts/OBK/OBKExpRejectDocument.mrt");
            report.Load(path);
            report.Compile();
            report.RegBusinessObject("rm", expRepo.GetExpRejectFormData(id, productSeriesId, pid));
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Word2007, stream);
            stream.Position = 0;
            return File(stream, "application/msword", $"Уведомление об отказе.docx");
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
            var reasons = new SafetyAssessmentRepository().GetRefReasons("Declaration", false);
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

            var stageObj = db.OBK_Ref_StageStatus.FirstOrDefault(o => o.Id == stage.StageStatusId);
            ViewData["StageStatus"] = stageObj.Code;

            return PartialView(model);
        }

        public ActionResult SelectCommissionOP(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult GetOBK_OP_Commission(Guid id)
        {
            var model = expRepo.GetOBK_OP_Commission(id);
            var result = model.Select(x =>
            {
                var employee = x.Employee;
                return new
                {
                    Organization = employee.Organization.Name,
                    Unit = employee.Units.FirstOrDefault(),
                    Position = employee.Position.Name,
                    FIO = employee.FullName
                };
            });
            return Json(new { isSuccess = true, result });
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
                            DateTime endDate = new DateTime();
                            var validDate = DateTime.TryParse(productSeries.SeriesEndDate, out endDate);
                            var endDateString = productSeries.SeriesEndDate;
                            if (validDate == true)
                            {
                                endDate = endDate.AddMonths(1);
                                endDateString = endDate.ToString("dd.MM.yyyy");
                            }
                            series.SeriesEndDate = endDateString;
                            series.SeriesParty = productSeries.SeriesParty;
                            series.SeriesShortNameRu = productSeries.sr_measures.short_name;
                            series.SeriesNameRu = productSeries.Series + ", годен до " + endDateString +
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
                                series.ExpEndDate = endDateString;
                                series.ExpReasonNameRu = obkStageExpDocumentSeries.ExpReasonNameRu;
                                series.ExpReasonNameKz = obkStageExpDocumentSeries.ExpReasonNameKz;
                                series.ExpProductNameRu = obkStageExpDocumentSeries.ExpProductNameRu;
                                series.ExpProductNameKz = obkStageExpDocumentSeries.ExpProductNameKz;
                                series.ExpProductShortNameRu = obkStageExpDocumentSeries.ExpProductShortNameRu;
                                series.ExpProductShortNameKz = obkStageExpDocumentSeries.ExpProductShortNameKz;
                                series.ExpNomenclatureRu = obkStageExpDocumentSeries.ExpNomenclatureRu;
                                series.ExpNomenclatureKz = obkStageExpDocumentSeries.ExpNomenclatureKz;
                                series.ExpAddInfoRu = assessmentRepo.FormExpAdditionalInfo(declarant);
                                series.ExpAddInfoKz = assessmentRepo.FormExpAdditionalInfo(declarant, true);
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
                    return Json(new { isSuccess = true });
                }

            }
            return Json(new { isSuccess = true });

        }



        #region Заклчюение для серии и партии

        public ActionResult ExpertiseConclusion(Guid declarationId)
        {
            try
            {
                var model = expRepo.ExpertiseConclusion(declarationId);
                return PartialView(model);
            }
            catch (Exception e)
            {
                LogHelper.Log.Error("Заклчюение для серии и партии: ExpertiseConclusion " + e.Message);
                throw;
            }
        }

        public ActionResult ShowModalTaskDetails(Guid assessmentDeclarationId, int productSeriesId)
        {
            var model = expRepo.GetTaskDetails(assessmentDeclarationId, productSeriesId);
            return PartialView(model);
        }

        public ActionResult ExpertiseConclusionPositive(int productSeriesId, Guid adId)
        {
            var model = expRepo.ExpertiseConclusionPositive(productSeriesId, adId);
            return PartialView(model);
        }

        public ActionResult SaveExpertiseConclusionPositive(OBKExpertiseConclusionPositive ecp)
        {
            var result = expRepo.SaveExpertiseConclusionPositive(ecp);
            return Json(new { isSuccess = result });
        }

        public ActionResult ExpertiseConclusionNegative(int productSeriesId, Guid adId)
        {
            var model = expRepo.ExpertiseConclusionNegative(productSeriesId, adId);
            return PartialView(model);
        }

        public ActionResult SaveExpertiseConclusionNegative(OBKExpertiseConclusionNegative ecn)
        {
            var result = expRepo.SaveExpertiseConclusionNegative(ecn);
            return Json(new { isSuccess = result });
        }

        public ActionResult ShowExpertiseConclusionPositive(int productSeriesId, Guid adId)
        {
            var model = expRepo.ShowExpertiseConclusionPositive(productSeriesId, adId);
            return PartialView("ExpertiseConclusionPositive", model);
        }

        public ActionResult ShowExpertiseConclusionNegative(int productSeriesId, Guid adId)
        {
            var model = expRepo.ShowExpertiseConclusionNegative(productSeriesId, adId);
            return PartialView("ExpertiseConclusionNegative", model);
        }

        public ActionResult TaskComment(Guid rcId, Guid teId)
        {
            var result = expRepo.GetTaskComments(rcId, teId);
            return PartialView(result);
        }

        public ActionResult SaveTaskComment(Guid rcId, string note)
        {
            expRepo.SaveTaskComment(rcId, note);
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReturnToResearchCenter(Guid adId, int psId)
        {
            expRepo.ReturnToResearchCenter(adId, psId);
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReturnToResearchCenters(List<OBKReturnToResearchCenter> rtrc)
        {
            expRepo.ReturnToResearchCenters(rtrc);
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowSignBtn(Guid adId)
        {
            var result = expRepo.GetValidShowSignBtn(adId);
            return Json(new { isSuccess = result }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }

}