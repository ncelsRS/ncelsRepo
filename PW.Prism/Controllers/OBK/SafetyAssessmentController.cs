using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.Database.Repository.Security;
using PW.Prism.ViewModels.Expertise;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Newtonsoft.Json;
using Stimulsoft.Report.Mvc;

namespace PW.Prism.Controllers.OBK
{
    [Authorize]
    public class SafetyAssessmentController : PrimsSafetyAssessmentController
    {
        private ncelsEntities db = UserHelper.GetCn();

        public ActionResult ListRegister([DataSourceRequest] DataSourceRequest request, string type, int stage, DeclarationRegistryFilter customFilter = null)
        {
            var stageName = GetName(stage);
            ActionLogger.WriteInt(stageName + ": Получение списка заявлений");
            var list =
                new SafetyAssessmentRepository().SafetyAssessmentRegisterList(type, stage,
                    UserHelper.GetCurrentEmployee().Id, customFilter);
            var result = list.ToDataSourceResult(request);
            return Json(result);
        }

        public string GetName(int stage)
        {
            switch (stage)
            {
                case CodeConstManager.STAGE_OBK_COZ:
                    return "ЦОЗ";
                case CodeConstManager.STAGE_OBK_EXPERTISE_DOC:
                    return "Экспертиза документов";
                default:
                    return stage.ToString();
            }
        }

        public ActionResult Design(Guid[] id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = GetAssessmentStage(id[0]);
            model.OBK_AssessmentDeclaration.Applicant = new EmployeesRepository().GetById(model.OBK_AssessmentDeclaration.EmployeeId);

            //проверка для кнопки выдать результат
            var certificateOfComplection = model.OBK_AssessmentDeclaration.OBK_CertificateOfCompletion.FirstOrDefault(
                    e => e.AssessmentDeclarationId == model.DeclarationId);
            if (certificateOfComplection == null)
                ViewBag.outputResultAct = false;
            else
                ViewBag.outputResultAct = certificateOfComplection.ActReturnedBack;

            FillDeclarationControl(model.OBK_AssessmentDeclaration);
            var stageName = GetName(model.StageId);
            ActionLogger.WriteInt(stageName + ": Получение заявления №" + model.OBK_AssessmentDeclaration.Number);
            return PartialView(model);
        }

        public ActionResult ExportFilePdf(Guid id)
        {
            var db = new ncelsEntities();
            string name = "Заявление на проведение оценки безопасности и качества лс.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/SafetyAssessmentDeclaration.mrt"));
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
            //var assessmentDeclaration = db.OBK_AssessmentDeclaration.FirstOrDefault(dd => dd.Id == id);
            //var assessmentDeclarationHistory = assessmentDeclaration.OBK_AssessmentDeclarationHistory.Where(dh => dh.XmlSign != null)
            //    .OrderByDescending(dh => dh.DateCreate).FirstOrDefault();
            //if (assessmentDeclarationHistory != null)
            //{
            //    report.ExportDocument(StiExportFormat.Word2007, stream);
            //    Aspose.Words.Document doc = new Aspose.Words.Document(stream);
            //    doc.InserQrCodesToEnd("ExecutorSign", assessmentDeclarationHistory.XmlSign);
            //    var pdfFile = new MemoryStream();
            //    pdfFile.Position = 0;
            //    stream.Close();
            //    return File(pdfFile, "application/pdf");
            //}
            return File(stream, "application/pdf", name);
        }

        /// <summary>
        /// прикрепленные файлы
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AttachFileView(Guid? id)
        {
            var model = GetAssessmentDeclaration(id.ToString());

            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_SERIAL))
            {
                var repository = new UploadRepository();
                var list = repository.GetAttachListEdit(id, CodeConstManager.ATTACH_SERIAL_SA_FILE_CODE);
                return PartialView(list);
            }
            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_PARTY))
            {
                var repository = new UploadRepository();
                var list = repository.GetAttachListEdit(id, CodeConstManager.ATTACH_PARTY_SA_FILE_CODE);
                return PartialView(list);
            }
            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_DECLARATION))
            {
                var repository = new UploadRepository();
                var list = repository.GetAttachListEdit(id, CodeConstManager.ATTACH_DECLARATION_SA_FILE_CODE);
                return PartialView(list);
            }
            return PartialView(null);
        }

        public ActionResult GetActReception(Guid id)
        {
            var stage = db.OBK_AssessmentStage.FirstOrDefault(o => o.Id == id);
            var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == stage.DeclarationId);
            var model = db.OBK_ActReception.FirstOrDefault(o => o.Id == stage.OBK_AssessmentDeclaration.Id);
            if (model == null)
            {
                model = new OBK_ActReception();
            }

            ViewData["AssessmentDeclarationId"] = declaration.Id;
            ViewData["ContractId"] = declaration.ContractId;

            if (declaration.ApplicantAgreement == true)
            {
                var expDocResult = db.OBK_StageExpDocumentResult.FirstOrDefault(o => o.AssessmetDeclarationId == declaration.Id);
                ViewData["expDocResult"] = expDocResult;
                return PartialView("ExpertActReception", model);
            }

            if (stage != null)
            {
                ViewData["AssessmentDeclarationId"] = stage.OBK_AssessmentDeclaration.Id;
                ViewData["ProductSampleList"] =
                    new SelectList(db.Dictionaries.Where(o => o.Type == "ProductSample"), "Id", "Name");
            }

            return PartialView("ActReception", model);
        }

        public ActionResult LoadActData(Guid id, Guid AssessmentDeclarationId)
        {
            var model = db.OBK_ActReception.First(o => o.Id == id);
            ViewData["ProductSampleList"] =
                new SelectList(db.Dictionaries.Where(o => o.Type == "ProductSample"), "Id", "Name");

            var AssessmentDeclaration = db.OBK_AssessmentDeclaration.First(o => o.Id == AssessmentDeclarationId);

            ViewData["KfSelection"] = AssessmentDeclaration.KfSelection;

            if (AssessmentDeclaration.KfSelection == true)
            {
                var safetyRepository = new SafetyAssessmentRepository();

                ViewData["InspectionInstalledList"] =
                    new SelectList(safetyRepository.GetInspectionInstalls(), "Id", "Name");

                ViewData["PackageConditionList"] =
                    new SelectList(safetyRepository.GetStorageConditions(), "Id", "Name");

                ViewData["StorageConditionsList"] =
                    new SelectList(safetyRepository.GetPackageConditions(), "Id", "Name");

                ViewData["MarkingList"] =
                    new SelectList(safetyRepository.GetMarkings(), "Id", "Name");
            }

            return PartialView("ActData", model);
        }


        public ActionResult GetSamples(Guid Id)
        {
            var result = from series in db.OBK_Procunts_Series
                         join product in db.OBK_RS_Products on series.OBK_RS_ProductsId equals product.Id
                         join contract in db.OBK_Contract on product.ContractId equals contract.Id
                         join measure in db.sr_measures on series.SeriesMeasureId equals measure.id
                         where contract.Id == Id
                         select new
                         {
                             serieId = series.Id,
                             name = product.DrugFormFullName,
                             measure = measure.name,
                             measureId = series.SeriesMeasureId,
                             serie = series.Series,
                             serieParty = series.SeriesParty,
                             seriesStartDate = series.SeriesStartdate,
                             seriesEndDate = series.SeriesEndDate,
                             quantity = series.Quantity,
                             available = series.Available == true ? true : false,
                             comment = series.Comment
                         };

            return Json(new { isSuccess = true, data = result });
        }

        [HttpPost]
        public virtual ActionResult UpdateProductSeries(string productSeries, Guid actReceptionId)
        {
            var series = JsonConvert.DeserializeObject<List<ProductSeriesModel>>(productSeries).OrderBy(o => o.SerieId).ToList();
            var serIds = series.Select(x => x.SerieId);
            var dbSeries = db.OBK_Procunts_Series.Where(o => serIds.Contains(o.Id)).OrderBy(o => o.Id).ToList();

            for (int i = 0; i < dbSeries.Count; i++)
            {
                var ob1 = series.ElementAt(i);
                var ob2 = dbSeries.ElementAt(i);
                if (!ob1.Comment.Equals(ob2.Comment) || !ob1.Available == ob2.Available)
                {
                    ob2.Available = ob1.Available;
                    ob2.Comment = ob1.Comment;
                }
            }
            var act = db.OBK_ActReception.FirstOrDefault(o => o.Id == actReceptionId);
            act.Accept = true;

            db.SaveChanges();

            return Json(new { Success = true });
        }

        [HttpPost]
        public virtual ActionResult UpdateProductSeries2(string productSeries, Guid actReceptionId)
        {
            var series = JsonConvert.DeserializeObject<List<ProductSeriesModel>>(productSeries).OrderBy(o => o.SerieId).ToList();
            var serIds = series.Select(x => x.SerieId);
            var dbSeries = db.OBK_Procunts_Series.Where(o => serIds.Contains(o.Id)).OrderBy(o => o.Id).ToList();

            for (int i = 0; i < dbSeries.Count; i++)
            {
                var ob1 = series.ElementAt(i);
                var ob2 = dbSeries.ElementAt(i);

                if (ob1.Quantity != ob2.Quantity)
                {
                    ob2.Quantity = ob1.Quantity;
                }

                if (ob1.SeriesMeasureId != ob2.SeriesMeasureId)
                {
                    ob2.SeriesMeasureId = ob1.SeriesMeasureId;
                }

            }
            var act = db.OBK_ActReception.FirstOrDefault(o => o.Id == actReceptionId);
            act.Accept = true;

            db.SaveChanges();

            return Json(new { Success = true });
        }

        public virtual ActionResult GetContract(Guid id)
        {
            var contract = new SafetyAssessmentRepository().GetContractById(id);

            if (contract == null)
            {
                return Json(new { isSuccess = false });
            }
            var products = new SafetyAssessmentRepository().GetRsProductsAndSeries(contract.Id);

            var result = new OBK_AssessmentDeclaration();

            var resultProducts = new List<OBK_RS_Products>();
            foreach (var product in products)
            {
                var prod = new OBK_RS_Products();
                prod.Id = product.Id;
                prod.NameRu = product.NameRu;
                prod.NameKz = product.NameKz;
                prod.ProducerNameRu = product.ProducerNameRu;
                prod.ProducerNameKz = product.ProducerNameKz;
                prod.CountryNameRu = product.CountryNameRu;
                prod.CountryNameKZ = product.CountryNameKZ;
                prod.DrugFormFullName = product.DrugFormFullName;
                prod.DrugFormFullNameKz = product.DrugFormFullNameKz;
                prod.DrugFormBoxCount = product.DrugFormBoxCount;
                //prod.TnvedCode = product.TnvedCode;
                //prod.KpvedCode = product.KpvedCode;
                prod.CurrencyId = product.CurrencyId;
                prod.Price = product.Price;
                prod.RegTypeId = product.RegTypeId;
                prod.RegNumber = product.RegNumber;
                prod.RegisterId = product.RegisterId;
                prod.RegNumberKz = product.RegNumberKz;
                prod.RegDate = product.RegDate;
                prod.ExpirationDate = product.ExpirationDate;
                prod.NdName = product.NdName;
                prod.NdNumber = product.NdNumber;
                foreach (var productSeries in product.OBK_Procunts_Series)
                {
                    var prodSeries = new OBK_Procunts_Series();
                    prodSeries.Id = productSeries.Id;
                    prodSeries.Series = productSeries.Series;
                    prodSeries.SeriesStartdate = productSeries.SeriesStartdate;
                    prodSeries.SeriesEndDate = productSeries.SeriesEndDate;
                    prodSeries.SeriesParty = productSeries.SeriesParty;
                    prodSeries.SeriesShortNameRu = productSeries.sr_measures.short_name;
                    var obkStageExpDocumentSeries = new SafetyAssessmentRepository().GetStageExpDocument(prodSeries.Id);
                    if (obkStageExpDocumentSeries != null)
                    {
                        prodSeries.ExpId = obkStageExpDocumentSeries.Id;
                        prodSeries.ProductSeriesId = obkStageExpDocumentSeries.ProductSeriesId;
                        prodSeries.ExpResult = obkStageExpDocumentSeries.ExpResult ? "True" : "False";
                        prodSeries.ExpResultTitle = obkStageExpDocumentSeries.ExpResult
                            ? "Соответствует требованиям"
                            : "Не соответствует требованиям";
                        prodSeries.ExpStartDate = string.Format("{0:dd.MM.yyyy}", obkStageExpDocumentSeries.ExpStartDate);
                        prodSeries.ExpEndDate = string.Format("{0:dd.MM.yyyy}", obkStageExpDocumentSeries.ExpEndDate);
                        prodSeries.ExpReasonNameRu = obkStageExpDocumentSeries.ExpReasonNameRu;
                        prodSeries.ExpReasonNameKz = obkStageExpDocumentSeries.ExpReasonNameKz;
                        prodSeries.ExpProductNameRu = obkStageExpDocumentSeries.ExpProductNameRu;
                        prodSeries.ExpProductNameKz = obkStageExpDocumentSeries.ExpProductNameKz;
                        prodSeries.ExpNomenclatureRu = obkStageExpDocumentSeries.ExpNomenclatureRu;
                        prodSeries.ExpNomenclatureKz = obkStageExpDocumentSeries.ExpNomenclatureKz;
                        prodSeries.ExpAddInfoRu = obkStageExpDocumentSeries.ExpAddInfoRu;
                        prodSeries.ExpAddInfoKz = obkStageExpDocumentSeries.ExpAddInfoKz;
                        prodSeries.ExpConclusionNumber = obkStageExpDocumentSeries.ExpConclusionNumber;
                        prodSeries.ExpBlankNumber = obkStageExpDocumentSeries.ExpBlankNumber;
                        prodSeries.ExpApplication = obkStageExpDocumentSeries.ExpApplication;
                        prodSeries.ExpApplicationNumber = obkStageExpDocumentSeries.ExpApplicationNumber;
                    }
                    prod.OBK_Procunts_Series.Add(prodSeries);
                }
                foreach (var mtPart in product.OBK_MtPart)
                {
                    var mtParts = new OBK_MtPart();
                    mtParts.PartNumber = mtPart.PartNumber;
                    mtParts.Model = mtPart.Model;
                    mtParts.Specification = mtPart.Specification;
                    mtParts.ProducerName = mtPart.ProducerName;
                    mtParts.CountryName = mtPart.CountryName;
                    mtParts.Name = mtPart.Name;
                    prod.OBK_MtPart.Add(mtParts);
                }
                resultProducts.Add(prod);
            }
            result.ObkRsProductses = resultProducts;
            return Json(new { isSuccess = true, result });
        }

        /// <summary>
        /// вренуть в работу заявителю
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reject(Guid id, string note)
        {
            var model = GetAssessmentStage(id);
            model.OBK_AssessmentDeclaration.DesignNote = note;
            return PartialView(model);
        }
        /// <summary>
        /// вренуть в работу заявителю
        /// </summary>
        /// <param name="id"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RejectConfirm(Guid? id, string note)
        {
            if (id != null)
            {
                var assessmentStage = GetAssessmentStage(id);
                assessmentStage.StageStatusId = new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.InReWork).Id;
                new SafetyAssessmentRepository().SaveStage(assessmentStage);
                var model = assessmentStage.OBK_AssessmentDeclaration;
                if (model != null)
                {
                    model.DesignNote = note;
                    model.DesignDate = DateTime.Now;
                    model.StatusId = CodeConstManager.STATUS_OBK_REJECT_ID;
                    new SafetyAssessmentRepository().Update(model);
                    var history = new OBK_AssessmentDeclarationHistory()
                    {
                        DateCreate = DateTime.Now,
                        AssessmentDeclarationId = model.Id,
                        StatusId = model.StatusId,
                        UserId = UserHelper.GetCurrentEmployee().Id,
                        Note = model.DesignNote
                    };
                    new SafetyAssessmentRepository().SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);

                    new NotificationManager().SendNotificationFromCompany(
                        string.Format("По Вашей заявке №{0} поступили замечания", model.Number),
                        ObjectType.ObkDeclaration, model.Id.ToString(), model.EmployeeId);
                }
            }
            return Json("Ok!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmeRefuseSafety(Guid stageId)
        {
            return PartialView(stageId);
        }
        public ActionResult RefuseInSafety(Guid stageId)
        {
            var repo = new AssessmentStageRepository();
            repo.StartRefuseInSafety(stageId, true);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// назначить исполнителя
        /// </summary>
        /// <param name="id">список заявлении</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetExecuter()
        {
            return PartialView(Guid.NewGuid());
        }
        [HttpPost]
        public ActionResult SetExecuter(Guid[] stages, Guid[] executors)
        {
            var okbExecutor = new SafetyAssessmentRepository();
            okbExecutor.SendToWork(stages, executors);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// отправка на экспертизу документов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DocumentReview(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult DocumentReviewConfirm(Guid? id)
        {
            if (id == null) return Json("Ok!", JsonRequestBehavior.AllowGet);
            var expertise = GetAssessmentStage(id);
            expertise.StageStatusId = new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.OnExpDocument).Id;
            expertise.FactEndDate = DateTime.Now;
            new SafetyAssessmentRepository().SaveStage(expertise);
            var model = expertise.OBK_AssessmentDeclaration;
            if (model == null) return Json("Ok!", JsonRequestBehavior.AllowGet);
            model.StatusId = CodeConstManager.STATUS_OBK_EXP_SEND_ID;
            new SafetyAssessmentRepository().Update(model);
            var history = new OBK_AssessmentDeclarationHistory()
            {
                DateCreate = DateTime.Now,
                AssessmentDeclarationId = model.Id,
                StatusId = model.StatusId,
                UserId = UserHelper.GetCurrentEmployee().Id,
            };
            new SafetyAssessmentRepository().SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            var stageRepository = new AssessmentStageRepository();
            string resultDescription;
            if (!stageRepository.HasStage(model.Id, CodeConstManager.STAGE_OBK_EXPERTISE_DOC))
                stageRepository.ToNextStage(model.Id, null, new[] { CodeConstManager.STAGE_OBK_EXPERTISE_DOC }, out resultDescription);

            return Json("Ok!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutputResultView(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult OutputResult(Guid id)
        {
            var okbRepo = new SafetyAssessmentRepository();
            okbRepo.SendOutputResult(id);
            return Json("Ok!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExpertActData(Guid assessmentId)
        {
            var model = db.OBK_ActReception.FirstOrDefault(o => o.Id == assessmentId);
            var assessment = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == assessmentId);

            ViewData["ContractId"] = assessment.ContractId;

            if (model == null)
            {
                model = new OBK_ActReception();
                model.Id = assessmentId;
                var exp = db.OBK_StageExpDocumentResult.FirstOrDefault(o => o.AssessmetDeclarationId == assessmentId);

                model.Number = assessment.Number;
                model.ActDate = exp.SelectionDate;
                model.Address = exp.SelectionPlace;

                var employee = db.Employees.FirstOrDefault(o => o.Id == assessment.EmployeeId);
                model.Declarer = employee.DisplayName;

                var product = db.OBK_RS_Products.FirstOrDefault(o => o.ContractId == assessment.ContractId);
                model.Producer = product.ProducerNameRu;

                db.OBK_ActReception.Add(model);
                db.SaveChanges();
            }

            if (model.AttachPath == null)
            {
                //model.AttachPath = 
            }

            var safetyRepository = new SafetyAssessmentRepository();

            ViewData["ProductSampleList"] =
                new SelectList(safetyRepository.GetProductSamples(), "Id", "Name");

            ViewData["InspectionInstalledList"] =
                new SelectList(safetyRepository.GetInspectionInstalls(), "Id", "Name");

            ViewData["PackageConditionList"] =
                new SelectList(safetyRepository.GetStorageConditions(), "Id", "Name");

            ViewData["StorageConditionsList"] =
                new SelectList(safetyRepository.GetPackageConditions(), "Id", "Name");

            ViewData["MarkingList"] =
                new SelectList(safetyRepository.GetMarkings(), "Id", "Name");

            ViewData["OBKApplicants"] =
                new SelectList(safetyRepository.OBKApplicants(), "Id", "NameRU");

            return PartialView(model);
        }

        public ActionResult SaveExpertActReception(OBK_ActReception reception)
        {
            var model = db.OBK_ActReception.FirstOrDefault(o => o.Id == reception.Id);
            model.InspectionInstalledId = reception.InspectionInstalledId;
            model.MarkingId = reception.MarkingId;
            model.Producer = reception.Producer;
            model.Provider = reception.Provider;
            model.PackageConditionId = reception.PackageConditionId;
            model.ProductSamplesId = reception.ProductSamplesId;
            model.StorageConditionsId = reception.StorageConditionsId;
            model.Declarer = reception.Declarer;
            model.AttachPath = reception.AttachPath;
            model.ApplicantId = reception.ApplicantId;
            model.ActDate = model.ActDate;
            model.Address = model.Address;

            db.SaveChanges();

            return Json(new { success = true });
        }

        public ActionResult DeleteExpertActReception(Guid? actReceptionId)
        {
            var model = db.OBK_ActReception.FirstOrDefault(o => o.Id == actReceptionId);

            if (model != null)
            {
                db.OBK_ActReception.Remove(model);
                db.SaveChanges();
            }
            else
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        public ActionResult GetSamples2(Guid? Id)
        {
            SafetyAssessmentRepository repository = new SafetyAssessmentRepository();

            if (Id != null)
            {
                var result = repository.GetProductSeries(Id).AsEnumerable().ToArray();
                return Json(new { isSuccess = true, data = result });

            }

            return Json(new { isSuccess = false });

        }

        public ActionResult MeasureSelect(int measureId)
        {
            var safetyRepository = new SafetyAssessmentRepository();

            ViewData["MeasureList"] =
                new SelectList(safetyRepository.GetMeasures(), "id", "name", measureId);

            return PartialView("MeasureSelectView", measureId);
        }

        public ActionResult MeasureSelect2(int measureId, Guid assessmentId, int? serieId)
        {
            var safetyRepository = new SafetyAssessmentRepository();

            ViewData["MeasureList"] =
                new SelectList(safetyRepository.GetMeasures(), "id", "name", measureId);
            ViewData["assessmentId"] = assessmentId;

            return PartialView("MeasureSelectView", serieId);
        }

        public ActionResult RequestSamples(Guid? declarationId)
        {
            var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == declarationId);

            NotificationManager notification = new NotificationManager();

            var text = "Уведомляем Вас о том, что необходимо предоставить образцы в ЦОЗ в течении 48 часов. В случае не предоставления образцов в течении указанного времени Вам будет выдано решение об отказе в выдаче заключения о безопасности и качества по текущей заявке.";
            notification.SendNotificationFromCompany(text, ObjectType.ObkDeclaration, declaration.Id.ToString(), declaration.EmployeeId);

            return Json(new { Success = true });
        }

        public ActionResult PrintActReception(Guid contractId, Guid actReceptionId, bool view)
        {
            var db = new ncelsEntities();
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/OBK/ObkActReception.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }
                report.Dictionary.Variables["ContractId"].ValueObject = contractId;
                report.Dictionary.Variables["ActReceptionId"].ValueObject = actReceptionId;
                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }

            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;

            string name = "Акт отбора" + DateTime.Now.ToString() + ".pdf";

            if (view)
            {
                return new FileStreamResult(stream, "application/pdf");

            }
            else
            {
                return File(stream, "application/pdf", name);
            }

        }

        public ActionResult ActTemplete(Guid actReceptionId)
        {
            var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == actReceptionId);

            ViewData["ContractId"] = declaration.ContractId;
            ViewData["ActReceptionId"] = actReceptionId;

            return PartialView("ActTemplete");
        }
    }
}