using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.Database.Repository.Security;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PW.Prism.Controllers.OBK
{
    [Authorize]
    public class SafetyAssessmentController : PrimsSafetyAssessmentController
    {
        private ncelsEntities db = UserHelper.GetCn();
        SafetyAssessmentRepository repository;
        AssessmentStageRepository stageRepository;

        public SafetyAssessmentController()
        {
            repository = new SafetyAssessmentRepository();
            stageRepository = new AssessmentStageRepository();
        }

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

        public ActionResult SaveTakenZBK(Guid declarationId)
        {
            var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == declarationId);
            if (declaration == null)
            {
                return Json(new { success = false, message = "Заключение не существует!" });
            }
            declaration.ZBKTaken = true;
            declaration.ExtraditeDate = DateTime.Now;
            db.SaveChanges();
            return Json(new { success = true, message = "Успешно сохранено!" });
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
            var expDocument = db.OBK_StageExpDocument.FirstOrDefault(o => o.AssessmentDeclarationId == model.OBK_AssessmentDeclaration.Id);

            if (certificateOfComplection == null)
            {
                ViewBag.outputResultAct = false;
                ViewBag.ZBKTaken = false;
            }
            else
            {
                ViewBag.outputResultAct = (certificateOfComplection.ActReturnedBack == true && model.OBK_AssessmentDeclaration.ZBKTaken == true);
                ViewBag.ZBKTaken = (model.OBK_AssessmentDeclaration.ZBKTaken) == true;
                ViewBag.ActReturnedBack = certificateOfComplection.ActReturnedBack;
            }
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
                    new SelectList(safetyRepository.GetPackageConditions(), "Id", "Name");

                ViewData["StorageConditionsList"] =
                    new SelectList(safetyRepository.GetStorageConditions(), "Id", "Name");

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
                             comment = series.Comment,
                             producerName = product.ProducerNameRu
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
                prod.Dimension = product.Dimension;
                prod.ExpertisePlace = product.ExpertisePlace;
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

        [HttpPost]
        public ActionResult GetContractFactory(Guid contractId)
        {
            var repo = new OBKContractRepository();
            var result = repo.GetContractFactories(contractId);
            return Json(result);
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
                stageRepository.ToNextStage(model, null, new int[] { CodeConstManager.STAGE_OBK_EXPERTISE_DOC }, out resultDescription);

            return Json("Ok!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult OutputResultView(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult OutputResult(Guid id, string receiverFio, DateTime receivedDate)
        {
            var okbRepo = new SafetyAssessmentRepository();
            okbRepo.SendOutputResult(id, receiverFio, receivedDate);
            return Json("Ok!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RequestSamples(Guid? declarationId)
        {
            var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == declarationId);

            NotificationManager notification = new NotificationManager();

            var text = "Уведомляем Вас о том, что необходимо предоставить образцы в ЦОЗ.";
            notification.SendNotificationFromCompany(text, ObjectType.ObkDeclaration, declaration.Id.ToString(), declaration.EmployeeId);

            return Json(new { Success = true });
        }

        #region Акт отбора
        public ActionResult SaveProductSeries(string productSeries, Guid actReceptionId)
        {
            var format = "dd.MM.yyyy"; // your datetime format
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
            var series = JsonConvert.DeserializeObject<List<SerieProduct>>(productSeries, dateTimeConverter).ToList();
            repository.SaveProductSeries(series, actReceptionId);
            return Json("");
        }

        public ActionResult GetProduct(int productId)
        {
            var data = db.OBK_RS_Products.FirstOrDefault(o => o.Id == productId);
            if (data == null)
            {
                return Json(new { success = false });
            }
            return Json(new { success = true, name = data.DrugFormFullName, producer = data.ProducerNameRu });
        }

        public ActionResult GetSerialActs([DataSourceRequest] DataSourceRequest request, Guid assessmentId)
        {
            var data = db.OBK_ActReception.Where(o => o.OBK_AssessmentDeclarationId == assessmentId).Select(o => new
            AddedAct
            {
                Id = o.Id,
                ActDate = o.ActDate,
                Number = o.Number,
                Worker = o.Worker
            });

            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult ExpertActData(Guid assessmentId)
        {
            var model = db.OBK_ActReception.FirstOrDefault(o => o.OBK_AssessmentDeclarationId == assessmentId);
            var assessment = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == assessmentId);

            ViewData["ContractId"] = assessment.ContractId;

            if (model == null)
            {
                model = new OBK_ActReception();
                model.Id = Guid.NewGuid();
                var exp = db.OBK_StageExpDocumentResult.FirstOrDefault(o => o.AssessmetDeclarationId == assessmentId);

                model.Number = assessment.Number;
                model.ActDate = exp.SelectionDate;
                model.Address = exp.SelectionPlace;
                model.OBK_AssessmentDeclarationId = assessmentId;
                var employee = db.Employees.FirstOrDefault(o => o.Id == assessment.EmployeeId);
                model.Declarer = employee.DisplayName;

                var product = db.OBK_RS_Products.FirstOrDefault(o => o.ContractId == assessment.ContractId);
                model.Producer = product.ProducerNameRu;

                db.OBK_ActReception.Add(model);
                db.SaveChanges();
            }

            var safetyRepository = new SafetyAssessmentRepository();

            ViewData["ProductSampleList"] =
                new SelectList(safetyRepository.GetProductSamples(), "Id", "Name");

            ViewData["InspectionInstalledList"] =
                new SelectList(safetyRepository.GetInspectionInstalls(), "Id", "Name");

            ViewData["PackageConditionList"] =
                new SelectList(safetyRepository.GetPackageConditions(), "Id", "Name");

            ViewData["StorageConditionsList"] =
                new SelectList(safetyRepository.GetStorageConditions(), "Id", "Name");

            ViewData["MarkingList"] =
                new SelectList(safetyRepository.GetMarkings(), "Id", "Name");

            ViewData["OBKApplicants"] =
                new SelectList(safetyRepository.OBKApplicants(), "Id", "NameRU");

            return PartialView(model);
        }

        public ActionResult GetActReception(Guid id)
        {
            var stage = db.OBK_AssessmentStage.FirstOrDefault(o => o.Id == id);
            var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == stage.DeclarationId);
            var model = db.OBK_ActReception.FirstOrDefault(o => o.OBK_AssessmentDeclarationId == stage.OBK_AssessmentDeclaration.Id);
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
                ViewData["ProductSampleList"] =
                    new SelectList(db.Dictionaries.Where(o => o.Type == "ProductSample"), "Id", "Name");
            }

            var stageObj = db.OBK_Ref_StageStatus.FirstOrDefault(o => o.Id == stage.StageStatusId);
            ViewData["StageStatus"] = stageObj.Code;

            return PartialView("ActReception", model);
        }

        public ActionResult DocumentReadSeries(string filePath)
        {
            OBKCertificateFileModel fileModel = new OBKCertificateFileModel()
            {
                AttachPath = filePath,
                AttachFiles = UploadHelper.GetFilesInfo(filePath, false)
            };

            return Content(JsonConvert.SerializeObject(fileModel, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

        }

        public ActionResult SerialActData(Guid? assessmentId)
        {
            var assessment = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == assessmentId);
            var numberCount = db.OBK_ActReception.Where(o => o.OBK_AssessmentDeclarationId == assessmentId).Count();

            ViewData["ContractId"] = assessment.ContractId;
            ViewData["AttachPath"] = FileHelper.GetObjectPathRoot();

            var model = new OBK_ActReception();
            model.Id = Guid.NewGuid();
            var exp = db.OBK_StageExpDocumentResult.FirstOrDefault(o => o.AssessmetDeclarationId == assessmentId);

            model.Number = assessment.Number + "-" + (numberCount + 1);
            model.OBK_AssessmentDeclarationId = assessmentId;
            var employee = db.Employees.FirstOrDefault(o => o.Id == assessment.EmployeeId);
            model.Declarer = employee.DisplayName;

            var product = db.OBK_RS_Products.FirstOrDefault(o => o.ContractId == assessment.ContractId);
            model.Producer = product.ProducerNameRu;

            ViewData["ProductSampleList"] =
                new SelectList(repository.GetProductSamples(), "Id", "Name");

            ViewData["InspectionInstalledList"] =
                new SelectList(repository.GetInspectionInstalls(), "Id", "Name");

            ViewData["PackageConditionList"] =
                new SelectList(repository.GetPackageConditions(), "Id", "Name");

            ViewData["StorageConditionsList"] =
                new SelectList(repository.GetStorageConditions(), "Id", "Name");

            ViewData["MarkingList"] =
                new SelectList(repository.GetMarkings(), "Id", "Name");

            ViewData["OBKApplicants"] =
                new SelectList(repository.OBKApplicants(), "Id", "NameRU");

            ViewData["ProductList"] =
               new SelectList(repository.OBKContractProducts(assessment.ContractId, model.Id), "Id", "DrugFormFullName");

            return PartialView(model);
        }

        public ActionResult GetSerialActReception(Guid id)
        {
            var stage = db.OBK_AssessmentStage.FirstOrDefault(o => o.Id == id);
            var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == stage.DeclarationId);

            ViewData["AssessmentDeclarationId"] = declaration.Id;
            ViewData["ContractId"] = declaration.ContractId;

            var stageStatus = db.OBK_Ref_StageStatus.FirstOrDefault(o => o.Id == stage.StageStatusId);
            if (OBK_Ref_StageStatus.InWork.Equals(stageStatus.Code))
            {
                ViewData["ShowAddEdit"] = true;
            }

            var stageObj = db.OBK_Ref_StageStatus.FirstOrDefault(o => o.Id == stage.StageStatusId);
            ViewData["StageStatus"] = stageObj.Code;

            return PartialView("SerialActReception", Guid.NewGuid());
        }

        public ActionResult EditSerialActData(Guid actReceptionId, Guid contractid)
        {
            var model = db.OBK_ActReception.FirstOrDefault(o => o.Id == actReceptionId);

            ViewData["ProductSampleList"] =
           new SelectList(repository.GetProductSamples(), "Id", "Name");

            ViewData["InspectionInstalledList"] =
                new SelectList(repository.GetInspectionInstalls(), "Id", "Name");

            ViewData["PackageConditionList"] =
                new SelectList(repository.GetPackageConditions(), "Id", "Name");

            ViewData["StorageConditionsList"] =
                new SelectList(repository.GetStorageConditions(), "Id", "Name");

            ViewData["MarkingList"] =
                new SelectList(repository.GetMarkings(), "Id", "Name");

            ViewData["OBKApplicants"] =
                new SelectList(repository.OBKApplicants(), "Id", "NameRU");

            ViewData["ProductList"] =
               new SelectList(repository.OBKContractProducts(contractid, model.Id), "Id", "DrugFormFullName");

            return PartialView("SerialActData", model);

        }

        public ActionResult SaveSerialExpertActReception(OBK_ActReception reception, string actDate)
        {
            repository.SaveSerialExpertActReception(reception, actDate);
            return Json(new { success = true, worker = reception.Worker });
        }

        public ActionResult SaveExpertActReception(OBK_ActReception reception, string actDate)
        {
            DateTime? actD = null;
            if (actDate != null || !actDate.Equals(""))
            {
                actD = DateTime.Parse(actDate);
            }
            var model = db.OBK_ActReception.FirstOrDefault(o => o.Id == reception.Id);
            model.InspectionInstalledId = reception.InspectionInstalledId;
            model.MarkingId = reception.MarkingId;
            model.Provider = reception.Provider;
            model.PackageConditionId = reception.PackageConditionId;
            model.ProductSamplesId = reception.ProductSamplesId;
            model.StorageConditionsId = reception.StorageConditionsId;
            model.Declarer = reception.Declarer;
            model.AttachPath = reception.AttachPath;
            model.ApplicantId = reception.ApplicantId;
            model.ActDate = actD;
            model.Address = model.Address;
            var employee = UserHelper.GetCurrentEmployee();
            model.Worker = employee.FullName;
            model.WorkerId = employee.Id;
            db.SaveChanges();

            return Json(new { success = true, worker = model.Worker });
        }

        public ActionResult DeleteExpertActReception(Guid? actReceptionId)
        {
            var model = db.OBK_ActReception.FirstOrDefault(o => o.Id == actReceptionId);

            if (model != null)
            {
                model.InspectionInstalledId = null;
                model.MarkingId = null;
                model.Producer = null;
                model.Provider = null;
                model.PackageConditionId = null;
                model.ProductSamplesId = null;
                model.StorageConditionsId = null;
                model.AttachPath = null;
                model.ApplicantId = null;
                model.Address = null;

                db.SaveChanges();
            }
            else
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        public ActionResult DeleteSerialActReception(Guid? actReceptionId)
        {
            repository.DeleteSerialActReception(actReceptionId);
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

        public ActionResult GetSerialSamples(Guid? actReceptionId)
        {
            if (actReceptionId != null)
            {
                var result = repository.GetSeriaProductSeries(actReceptionId).AsEnumerable().ToArray();
                return Json(new { isSuccess = true, list = result });

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

        public ActionResult MeasureSelectList([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.GetMeasures().Select(o => new { Id = o.id, Name = o.name }).OrderBy(o => o.Name);
            return Json(new { success = true, list = data.ToList() });
        }

        public ActionResult ContractAvailableProducts([DataSourceRequest] DataSourceRequest request, Guid contractId)
        {
            var data = repository.OBKContractAvailableProducts(contractId).Select(o => new ContractAvailableProducts
            {
                Name = o.DrugFormFullName,
                Producer = o.ProducerNameRu
            });

            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult MeasureSelect2(int measureId, Guid assessmentId, int? serieId)
        {
            var safetyRepository = new SafetyAssessmentRepository();

            ViewData["MeasureList"] =
                new SelectList(safetyRepository.GetMeasures(), "id", "name", measureId);
            ViewData["assessmentId"] = assessmentId;

            return PartialView("MeasureSelectView", serieId);
        }

        public ActionResult PrintActReception(Guid contractId, Guid actReceptionId, bool view, bool serial = false)
        {
            var db = new ncelsEntities();
            StiReport report = new StiReport();
            try
            {
                if (serial == true)
                {
                    report.Load(Server.MapPath("~/Reports/Mrts/OBK/OBKSerialActReception.mrt"));
                }
                else
                {
                    report.Load(Server.MapPath("~/Reports/Mrts/OBK/ObkActReception.mrt"));
                }

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

        public ActionResult ActTemplate(Guid actReceptionId, bool serial = false)
        {
            var act = db.OBK_ActReception.FirstOrDefault(o => o.Id == actReceptionId);

            var declaration = db.OBK_AssessmentDeclaration.FirstOrDefault(o => o.Id == act.OBK_AssessmentDeclarationId);

            ViewData["ContractId"] = declaration.ContractId;
            ViewData["ActReceptionId"] = actReceptionId;
            if (serial)
            {
                ViewData["Serial"] = "true";
            }
            else
            {
                ViewData["Serial"] = "false";
            }

            return PartialView("ActTemplate");
        }
        #endregion

        #region Архив
        public ActionResult Archive()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult ArchiveList([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.ArchiveList();
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult RequestTypeList()
        {
            var data = repository.RequestTypeList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CountryList()
        {
            var data = repository.CountryList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ArchiveDetails(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = stageRepository.GetStageByDeclaration(id);
            FillDeclarationControl(model.OBK_AssessmentDeclaration);
            var expDocumentResult = new OBKExpDocumentRepository().GetStageExpDocResult(model.DeclarationId);
            ViewBag.HasExpDocumentResult = expDocumentResult != null;
            return PartialView("~/Views/SafetyAssessment/ArchiveDetails.cshtml", model);
        }

        public ActionResult ZBKCopies(Guid declarationId)
        {
            return PartialView("ZBKCopies", declarationId);
        }

        public ActionResult ZBKCopyList([DataSourceRequest] DataSourceRequest request, Guid? declarationId)
        {
            var data = repository.ZBKCopyList(declarationId);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult AdditionalContract(Guid? contractId)
        {
            return PartialView(contractId);
        }

        public ActionResult AdditionalContractList([DataSourceRequest] DataSourceRequest request, Guid? contractId)
        {
            var data = repository.AdditionalContractList(contractId);
            return Json(data.ToDataSourceResult(request));
        }
        #endregion
    }
}