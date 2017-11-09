using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Core.ActivityManager;
using Ncels.Helpers;
using Newtonsoft.Json;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.Contract;
using PW.Prism.ViewModels;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        private ContractRepository _contractRepository = new ContractRepository(false);
        private ActivityManager _activityManager = new ActivityManager();

        // GET: Contract
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult FormMyTask(Guid id)
        {

            ViewBag.StageList = new List<Item>()
            {
                new Item { Id = "1", Name = "Первичная экспертиза"},
                new Item { Id = "2", Name = "Фармакологическая экспертиза"},
                new Item { Id = "3", Name = "Фармацевтическая экспертиза"},
                new Item { Id = "4", Name = "Аналитическая экспертиза"},
                new Item { Id = "5", Name = "Переводы"},
                new Item { Id = "6", Name = "Заключение оценки безопасности"},
            };

            ViewBag.StateList = new List<Item>()
       {        new Item { Id = "0", Name = "Новый"},
                new Item { Id = "1", Name = "В работе"},
                new Item { Id = "2", Name = "Исполнен"},
                new Item { Id = "3", Name = "Исполненный отрицательно"},
                new Item { Id = "4", Name = "Принят на исполнение"}
            };
            return PartialView(id);
        }

        private void SetContractEmployeeFilter()
        {
            var contractSettings = _contractRepository.GetContractProcSetting();
            var currentEmployee = UserHelper.GetCurrentEmployee();
            ViewBag.IsProcHeadContract = contractSettings != null &&
                                         contractSettings.ProcCenterHeadId == currentEmployee.Id;
        }
        public ActionResult Contract(string filterId = "inWorkContracts")
        {
            SetContractEmployeeFilter();
            ViewBag.FilterId = filterId;
            return PartialView(Guid.NewGuid());
        }
        public ActionResult ContractAddition(string filterId = "inWorkContracts")
        {
            SetContractEmployeeFilter();
            ViewBag.FilterId = filterId;
            return PartialView(Guid.NewGuid());
        }

        public ActionResult CardOut(Guid? id, Guid? guid)
        {
            ViewBag.TaskId = null;
            return PartialView(new[] { id.Value, guid.Value });
        }

        public ActionResult FormOut(Guid? id, Guid? guid)
        {
            return PartialView(new[] { id.Value, guid.Value });
        }
        public ActionResult Card1(Guid? id)
        {
            Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
            ViewBag.TaskId = null;
            return PartialView(guid);
        }

        public ActionResult FormBp(Guid? id)
        {
            string[] res = new string[2];
            Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
            res[0] = guid.ToString();
            res[1] = "";
            if (id.HasValue)
            {
                Document document = db.Documents.Find(id.Value);
                if (document != null)
                {
                    res[1] = document.StateType.ToString();
                }
            }
            return PartialView(res);
        }

        [HttpPost]
        public ActionResult DocumentBpReject(Guid id)
        {
            Document baseDocument = db.Documents.Find(id);
            DocumentHelper.DeleteSignTask(db, baseDocument);

            baseDocument.StateType = 6;

            db.SaveChanges();
            int branch = db.Activities.Where(d => d.DocumentId == baseDocument.Id).Max(o => o.Branch);

            var list = db.Activities.Where(d => d.DocumentId == baseDocument.Id && d.Branch == branch).Where(o => o.Type != 4);

            var listItem = list.FirstOrDefault(o => o.ParentId == null);
            if (listItem != null)
            {

                Guid? parentId = null;
                for (int i = 0; i < list.Count(); i++)
                {
                    Activity activity = new Activity
                    {
                        Id = Guid.NewGuid(),
                        //	ParentTask = task.Id,
                        DocumentId = id,
                        AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                        AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                        ExecutorsId = listItem.ExecutorsId,
                        ExecutorsValue = listItem.ExecutorsValue,
                        //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                        //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                        Type = 3,
                        IsParrent = !parentId.HasValue,
                        CreatedDate = DateTime.Now,
                        ParentId = !parentId.HasValue ? (Guid?)null : parentId.Value,
                        ExecutionDate = baseDocument.ExecutionDate,
                        Text = listItem.Text,
                        IsNotActive = true,
                        //IsMainLine = task.IsMainLine
                    };
                    parentId = activity.Id;
                    listItem = list.FirstOrDefault(o => o.ParentId == listItem.Id);
                    db.Activities.Add(activity);
                }
                db.SaveChanges();
            }
            DocumentModel model = new DocumentModel(baseDocument);
            return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

            return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);

            return Content(bool.FalseString);
        }

        public ActionResult DocumentBpReview(Guid id)
        {

            Document baseDocument = db.Documents.Find(id);


            baseDocument.StateType = 4;

            Activity activity = db.Activities.FirstOrDefault(x => x.DocumentId == id && x.IsParrent && x.Branch == 0);

            if (activity != null)
                activity.IsCurrent = true;

            db.SaveChanges();
            DocumentModel model = new DocumentModel(baseDocument);

            Activity parentActivity = db.Activities.Where(x => x.DocumentId == id && x.Branch == 0).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            if (parentActivity != null)
            {
                DocumentHelper.SetSignTask(db, baseDocument, parentActivity.Id);
            }
            else
            {
                DocumentHelper.SetSignTaskAsParent(db, baseDocument);
            }

            return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));


            return Content(bool.FalseString);
        }


        [HttpGet]
        public ActionResult Card2(Guid? id)
        {
            Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

            return PartialView(new[] { guid });
        }

        public ActionResult ListContract([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<ContractJournal> query = db.ContractJournals.AsNoTracking().Where(e => e.ContractId == null);
            var contractSettings = _contractRepository.GetContractProcSetting();
            var currentEmployee = UserHelper.GetCurrentEmployee();
            if (contractSettings == null || contractSettings.ProcCenterHeadId != currentEmployee.Id)
                query = query.Where(e => e.ExecutorId == currentEmployee.Id.ToString()
                || e.SignerId == currentEmployee.Id);
            return Json(query.ToDataSourceResult(request));
        }
        public ActionResult ListContractAddition([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<ContractAdditionJournal> query = db.ContractAdditionJournals.AsNoTracking().Where(e => e.ContractId != null);
            var contractSettings = _contractRepository.GetContractProcSetting();
            var currentEmployee = UserHelper.GetCurrentEmployee();
            if (contractSettings == null || contractSettings.ProcCenterHeadId != currentEmployee.Id)
                query = query.Where(e => e.ExecutorId == currentEmployee.Id.ToString()
                || e.SignerId == currentEmployee.Id);
            return Json(query.ToDataSourceResult(request));
        }

        public ActionResult FormCnt(Guid[] id)
        {
            var contract = new ContractModel();
            contract.Id = id[0];
            contract.ApplicantContract = db.ContractsViews.FirstOrDefault(m => m.Id == contract.Id);

            if (contract.ApplicantContract != null && contract.ApplicantContract.Type == 0)
            {
                contract.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ManufacturerOrganizationId);
                contract.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.HolderOrganizationId);
                contract.Payer = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.PayerOrganizationId);
                contract.Applicant = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ApplicantOrganizationId);
                return PartialView("ContractDetails", contract);
            }

            if (contract.ApplicantContract != null && contract.ApplicantContract.Type == 1)
            {
                contract.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ManufacturerOrganizationId);
                contract.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.HolderOrganizationId);
                contract.Payer = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.PayerOrganizationId);
                contract.Applicant = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ApplicantOrganizationId);
                contract.HolderContract = db.ContractsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ContractId) ?? new ContractsView();
                return PartialView("ContractExDetails", contract);
            }

            return null;
        }
        public ActionResult FormOutgoingList(Guid id)
        {
            return PartialView(id);
        }
        public ActionResult DocumentRead(Guid id, Guid? documentId, string type)
        {

            Document document = db.Documents.Find(id);
            if (document == null)
            {

                document = new Document()
                {
                    Id = id,
                    DocumentType = 4,
                    DocumentDate = DateTime.Now,
                    CreatedUserId = UserHelper.GetCurrentEmployee().Id.ToString(),
                    CreatedUserValue = UserHelper.GetCurrentEmployee().DisplayName,
                    TemplateId = new Guid("C3292589-A25B-4CEF-8CB5-C7E64946C1D3"),
                    MonitoringType = 1,
                    AttachPath = FileHelper.GetObjectPathRoot(),
                    ParleyStartDate = DateTime.Now,
                    ParleyEndDate = DateTime.Now
                };

                document.ProjectType = 1;

                Document documentmain = db.Documents.First(o => o.Id == documentId);

                document.CorrespondentsValue = documentmain.CorrespondentsValue;
                document.ApplicantEmail = documentmain.ApplicantEmail;
                document.AnswersId = documentmain.Id.ToString();
                document.AnswersValue = documentmain.DisplayName;
                document.LanguageDictionaryId = documentmain.LanguageDictionaryId;
                document.LanguageDictionaryValue = documentmain.LanguageDictionaryValue;
                document.DocumentKindDictionaryId = documentmain.DocumentKindDictionaryId;
                document.DocumentKindDictionaryValue = documentmain.DocumentKindDictionaryValue;
                document.QuestionDesignDictionaryId = documentmain.QuestionDesignDictionaryId;
                document.QuestionDesignDictionaryValue = documentmain.QuestionDesignDictionaryValue;
                document.ExecutionDate = documentmain.ExecutionDate;
                document.CorrespondentsId = documentmain.CorrespondentsId;
                document.CorrespondentsValue = documentmain.CorrespondentsValue;
                document.CorrespondentsInfo = documentmain.RegistratorValue;
                document.ExecutorsValue = documentmain.RegistratorValue;
                document.ExecutorsId = documentmain.RegistratorId;
                document.RemarkId = documentmain.RemarkId ?? 0;
                document.RemarkText1 = documentmain.RemarkText1;
                document.RemarkText2 = documentmain.RemarkText2;
                document.RemarkText3 = documentmain.RemarkText3;

                document.OutgoingType = 1;




            }
            DocumentModel model = new DocumentModel(document);
            return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DocumentAttachFiles(string attachPath, bool isArchive)
        {
            return Json(UploadHelper.GetFilesInfo(attachPath, isArchive), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ContractProcSettings()
        {
            return PartialView(Guid.NewGuid());
        }
        [HttpGet]
        public ActionResult LoadContractProcSettings()
        {
            var settings = _contractRepository.GetContractProcSetting() ?? new ContractProcSetting();

            return Json(settings,
                JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveContractProcSettings(ContractProcSetting settings)
        {
            return Json(_contractRepository.SaveContractProcSettings(settings), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult TakeToWork(Guid id)
        {
            ViewBag.ContractId = id;
            return PartialView(Guid.NewGuid());
        }
        [HttpPost]
        public ActionResult TakeToWork(Guid contractId, Guid employeeId)
        {
            _contractRepository.TakeToWork(contractId, employeeId);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContractCard(Guid contractId)
        {
            ViewBag.UiId = Guid.NewGuid().ToString();
            var contractSettings = _contractRepository.GetContractProcSetting();
            var currentEmployee = UserHelper.GetCurrentEmployee();
            var agreementTask = _activityManager.GetCurrentTask(contractId,
                Dictionary.ExpActivityType.ContractWithProcCenterHead);
            var signTask = _activityManager.GetCurrentTask(contractId,
                Dictionary.ExpActivityType.ContractSigningByApplicantAndCeo);
            var vm = new ContractViewModel()
            {
                Contract = db.Contracts.AsNoTracking().FirstOrDefault(e => e.Id == contractId),
                IsProcHeadContract = contractSettings != null && contractSettings.ProcCenterHeadId == currentEmployee.Id,
                HasAgreementTask = agreementTask != null && agreementTask.ExecutorId == currentEmployee.Id,
                IsExecutor = currentEmployee.Id == _contractRepository.GetExecutorId(contractId),
                IsSigner = currentEmployee.Id == _contractRepository.GetSignerId(contractId),
                IsContractWithoutDc = _contractRepository.IsContractWithoutDc(contractId),
                HasSigningTask = signTask != null && signTask.ExecutorId == currentEmployee.Id
            };
            return PartialView(vm);
        }
        public PartialViewResult ContractDetails(Guid contractId, string parentUiId)
        {
            ViewBag.UiId = parentUiId;
            var contract = new ContractModel();
            contract.Id = contractId;
            contract.ApplicantContract = db.ContractsViews.FirstOrDefault(m => m.Id == contract.Id);
            contract.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ManufacturerOrganizationId);
            contract.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.HolderOrganizationId);
            contract.Payer = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.PayerOrganizationId);
            contract.Applicant = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ApplicantOrganizationId);
            contract.PayerTranslation = db.OrganizationsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.PayerTranslationOrganizationId) ?? new OrganizationsView();
            if ((contract.Payer.Id == contract.Manufaturer.Id)
                || (contract.Payer.Id == contract.Applicant.Id)
                || (contract.Payer.Id == contract.Holder.Id))
                ViewBag.PayerTypeOther = false;
            else
                ViewBag.PayerTypeOther = true;
            if ((contract.PayerTranslation.Id == contract.Manufaturer.Id)
                || (contract.PayerTranslation.Id == contract.Applicant.Id)
                || (contract.PayerTranslation.Id == contract.Holder.Id))
                ViewBag.PayerTranslationTypeOther = false;
            else
                ViewBag.PayerTranslationTypeOther = true;
            return PartialView(contract);
        }
        public PartialViewResult ContractAdditionDetails(Guid contractId, string parentUiId)
        {
            ViewBag.UiId = parentUiId;
            var contract = new ContractModel();
            contract.Id = contractId;
            contract.ApplicantContract = db.ContractsViews.FirstOrDefault(m => m.Id == contract.Id);
            contract.HolderContract = db.ContractsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ContractId);
            return PartialView(contract);
        }
        [HttpGet]
        public ActionResult AddComment(Guid contractId, Guid commentId)
        {
            var comment = commentId == Guid.Empty
                ? new ContractComment()
                {
                    Id = commentId,
                    AuthorId = UserHelper.GetCurrentEmployee().Id,
                    ContractId = contractId,
                    CreateDate = DateTime.Now
                }
                : db.ContractComments.FirstOrDefault(e => e.Id == commentId);
            return PartialView(comment);
        }
        [HttpPost]
        public ActionResult AddComment(ContractComment contractComment)
        {
            var result = _contractRepository.AddComment(contractComment);
            return Json(!result ? (object)"unavaliableAction" : result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListComments([DataSourceRequest] DataSourceRequest request, Guid contractId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.ContractComments.Include("Author").AsNoTracking().Where(e => e.ContractId == contractId);
            return Json(query.ToDataSourceResult(request, c => new
            {
                c.Id,
                c.CreateDate,
                c.Author,
                c.Comment,
                c.ContractId,
                c.Sended
            }));
        }
        public ActionResult ListAttaches([DataSourceRequest] DataSourceRequest request, Guid contractId)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var query = db.FileLinkViews.Include("FileCategory").AsNoTracking().Where(e => e.DocumentId == contractId);
            return Json(query.ToDataSourceResult(request, c => new
            {
                c.Id,
                c.Version,
                c.CreateDate,
                c.Category,
                c.FileName,
                c.FilePath,
                c.CategoryId,
                c.DocumentId,
                c.ParentFileName
            }));
        }

        [HttpGet]
        public ActionResult AddNewContractVersion(Guid contractId)
        {
            ViewBag.UiId = Guid.NewGuid().ToString();
            var fileLink = _contractRepository.GetActualContractFile(contractId);
            var contractStatus = _contractRepository.GetContractStatus(contractId);
            var avaliableStatuses = new[]
            {
                Ncels.Database.DataModel.Contract.StatusInWork,
                Ncels.Database.DataModel.Contract.StatusOnCorrection,
                Ncels.Database.DataModel.Contract.StatusCorrected
            };
            if (avaliableStatuses.Contains(contractStatus.Code))
                fileLink.AcceptFormats = "application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            else
                fileLink.AcceptFormats = "application/pdf,image/jpeg";
            ViewBag.CanRegister = contractStatus.Code == Ncels.Database.DataModel.Contract.StatusOnRegistration;
            return PartialView(fileLink);
        }
        [HttpGet]
        public ActionResult GetFilledTemplate(Guid contractId)
        {
            var actualFileLink = _contractRepository.GetActualContractFile(contractId);
            string templatePath = FileHelper.GetFilePath(actualFileLink.DocumentId.Value.ToString(), actualFileLink.CategoryId.Value.ToString(),
                null, string.Format("{0}{1}", actualFileLink.Id, Path.GetExtension(actualFileLink.FileName))); ;
            Dictionary<string, string> templateData;
            if (db.Contracts.Any(e => e.Id == contractId && e.ContractId != null))
            {
                templateData = _contractRepository.GetContractAdditionTemplateData(contractId);
            }
            else
            {
                templateData = _contractRepository.GetContractTemplateData(contractId);
            }
            Aspose.Words.Document doc = new Aspose.Words.Document(templatePath);
            doc.ReplaceText(templateData);
            var file = new MemoryStream();
            doc.Save(file, SaveFormat.Docx);
            file.Position = 0;
            return File(file, "application/msword", actualFileLink.FileName);
        }
        [HttpGet]
        public ActionResult Approve(Guid contractId)
        {
            ViewBag.UiId = Guid.NewGuid().ToString();
            return PartialView(contractId);
        }
        public ActionResult SendToSign(Guid contractId, bool withDc)
        {
            var contract=_contractRepository.SetNumberAndDate(contractId);
            if (withDc)
                _activityManager.CreateActivity(contractId, Dictionary.ExpAgreedDocType.Contract,
                    Dictionary.ExpActivityType.ContractSigningByApplicantAndCeo, contract.Number, contract.ContractDate);
            else
                _contractRepository.SendToCopySign(contractId);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegisterContract(Guid contractId)
        {
            _contractRepository.RegisterContract(contractId);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendComments(Guid[] commentIds)
        {
            _contractRepository.SendComments(commentIds);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SignData(Guid contractId)
        {
            return Json(new { data = _contractRepository.GetDataForSign(contractId) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSignedContract(Guid contractId, string signedData)
        {
            _contractRepository.SaveSign(contractId, signedData, true);
            var activityManager = new ActivityManager();
            activityManager.ExecuteTask(activityManager.GetCurrentTaskId(contractId, Dictionary.ExpActivityType.ContractSigningByApplicantAndCeo).Value, signedData);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}