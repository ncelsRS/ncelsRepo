using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;
using Ncels.Core.ActivityManager;
using Ncels.Helpers;
using Newtonsoft.Json;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.Contract;
using PW.Ncels.Helpers;
using PW.Ncels.Models;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Ncels.Controllers
{
    [Authorize()]
    public class ContractController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();
        private readonly ContractRepository _contractRepository = new ContractRepository(false);
        public ActionResult Index()
        {
            return View("ContractList", new ContractListViewModel()
            {
                ListUrl = Url.Action("GetAllList", "Contract"),
                BreadcumbPath = "Все",
                GridCaption = "Список всех договоров",
                GridUrl = Url.Action("GetAllList", "Contract"),
                NgController = "contractGrid",
                ListAction = "Index"
            });
        }

        public ActionResult GetAllList(ModelRequest request)
        {
            var result = _contractRepository.EmployeeContracts(UserHelper.GetCurrentEmployee().Id).ToDataSourceResult(request, e => new
            {
                e.Id,
                e.Type,
                e.Number,
                e.CreatedDate,
                ManufactureOrgName = e.ContractId != null ? e.ParentContract.ManufacturerOrganization.NameRu : e.ManufacturerOrganization.NameRu,
                StatusName = e.ContractStatus.Name
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Draft()
        {
            return View("ContractList", new ContractListViewModel()
            {
                ListUrl = Url.Action("Draft", "Contract"),
                BreadcumbPath = "Проекты",
                GridCaption = "Список проектов договоров",
                GridUrl = Url.Action("GetInWorkList", "Contract"),
                NgController = "contractGrid",
                ListAction = "Draft"
            });
        }

        public ActionResult GetInWorkList(ModelRequest request)
        {
            var result = _contractRepository.GetContractsByStatuses(UserHelper.GetCurrentEmployee().Id, Database.DataModel.Contract.StatusesInWork).ToDataSourceResult(request, e => new
            {
                e.Id,
                e.Type,
                e.Number,
                e.CreatedDate,
                ManufactureOrgName = e.ContractId != null ? e.ParentContract.ManufacturerOrganization.NameRu : e.ManufacturerOrganization.NameRu,
                StatusName = e.ContractStatus.Name
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Active()
        {
            return View("ContractList", new ContractListViewModel()
            {
                ListUrl = Url.Action("Active", "Contract"),
                BreadcumbPath = "Активные",
                GridCaption = "Список активных договоров",
                GridUrl = Url.Action("GetListByStatus", "Contract", new { statusCode = Database.DataModel.Contract.StatusActive }),
                NgController = "contractCompletedGrid",
                ListAction = "Active"
            });
        }

        public ActionResult Expired()
        {
            return View("ContractList", new ContractListViewModel()
            {
                ListUrl = Url.Action("Expired", "Contract"),
                BreadcumbPath = "Просроченные",
                GridCaption = "Список просроченных договоров",
                GridUrl = Url.Action("GetListByStatus", "Contract", new { statusCode = Database.DataModel.Contract.StatusExpired }),
                NgController = "contractCompletedGrid",
                ListAction = "Expired"
            });
        }

        public ActionResult GetListByStatus(ModelRequest request, string statusCode)
        {
            var result = _contractRepository.GetContractsByStatuses(UserHelper.GetCurrentEmployee().Id, statusCode).ToDataSourceResult(request, e => new
            {
                e.Id,
                e.Type,
                e.Number,
                e.CreatedDate,
                ManufactureOrgName = e.ContractId != null ? e.ParentContract.ManufacturerOrganization.NameRu : e.ManufacturerOrganization.NameRu,
                StatusName = e.ContractStatus.Name
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contract(Guid? id, string listAction)
        {
            ViewBag.ListAction = listAction;
            return View(id ?? Guid.NewGuid());
        }

        public ActionResult ContractAddition(Guid? id, string listAction)
        {
            ViewBag.ListAction = listAction;
            return View(id ?? Guid.NewGuid());
        }

        public ActionResult SendProjectContract(RequestModel request)
        {
            Document baseDocument = db.Documents.FirstOrDefault(m => m.Id == request.Contract.Id);

            if (baseDocument == null)
            {

                var user = UserHelper.GetCurrentEmployee();
                Document document = new Document()
                {
                    Id = request.Contract.Id,
                    DocumentType = 4,
                    ExecutionDate = DateTime.Now.AddDays(20),
                    DocumentDate = DateTime.Now,
                    AttachPath = FileHelper.GetObjectPathRoot(),
                    CorrespondentsInfo = UserHelper.GetCurrentEmployee().DisplayName,
                    CorrespondentsId = user.Id.ToString(),
                    CorrespondentsValue = user.DisplayName,
                    ExecutorsId = user.Id.ToString(),
                    ExecutorsValue = user.DisplayName,
                    TemplateId = new Guid("C3292589-A25B-4CEF-8CB5-C7E64946C1D3"),
                    IsTradeSecret = false
                };
                var project = db.Contracts.First(o => o.Id == request.Contract.Id);

                project.Status = 1;
                document.DocumentDate = document.DocumentDate ?? DateTime.Now;
                document.StateType = 1;
                document.ProjectType = project.Type.Value;
                //baseDocument.ExecutionDate = baseDocument.ExecutionDate ?? baseDocument.DocumentDate.Value.AddDays(15);
                document.RegistratorId = UserHelper.GetCurrentEmployee().Id.ToString();
                document.RegistratorValue = UserHelper.GetCurrentEmployee().DisplayName;
                document.FirstExecutionDate = document.ExecutionDate;
                document.ModifiedDate = DateTime.Now;
                Registrator.SetNumber(document);
                db.Documents.Add(document);
                db.SaveChanges();
            }
            else
            {
                baseDocument.IsTradeSecret = false;
                Document baseDocument2 = db.Documents.Where(m => m.DocumentType == 1).OrderByDescending(m => m.CreatedDate).FirstOrDefault(m => m.AnswersId == request.Contract.Id.ToString());
                if (baseDocument2 != null && baseDocument2.AnswersId != null)
                {


                    Activity activity = new Activity
                    {
                        Id = Guid.NewGuid(),
                        //ParentTask = task.Id,
                        DocumentId = baseDocument.Id,
                        AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                        AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                        ExecutorsId = baseDocument2.CreatedUserId,
                        ExecutorsValue = baseDocument2.CreatedUserValue,
                        //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                        //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                        Type = 2,
                        IsParrent = false,
                        CreatedDate = DateTime.Now,
                        //ParentId = task.ActivityId,
                        ExecutionDate = DateTime.Now.AddDays(20),
                        Text = "Замечания исправлены",
                        IsNotActive = false,
                        //TypeEx = task.Stage,
                        //IsMainLine = task.IsMainLine
                    };
                    db.Activities.Add(activity);
                    db.SaveChanges();
                }
            }
            return Json("Ок");

        }

        public ActionResult SigningConfirm(Guid taskId)
        {
            Database.DataModel.Task task = db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);

            //var dictionaries = _db.Dictionaries.Where(o => o.Type == "Nomenclature" && o.IsGuide).ToList().Select(o => o.Id.ToString());

            Activity parentActivity = db.Activities.FirstOrDefault(x => x.DocumentId == task.DocumentId && x.Branch == 0 && !db.Activities.Where(o => o.DocumentId == task.DocumentId && o.Branch == 0).Select(c => c.ParentId).Contains(x.Id));

            Activity activity = new Activity
            {
                Id = Guid.NewGuid(),
                //	ParentTask = task.Id,
                DocumentId = task.DocumentId,
                AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                AuthorValue = UserHelper.GetCurrentEmployee().DisplayName.ToString(),
                ExecutorsId = task.AuthorId,
                ExecutorsValue = task.AuthorValue,
                //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                Type = 4,
                IsParrent = parentActivity == null,
                CreatedDate = DateTime.Now,
                ParentId = parentActivity == null ? (Guid?)null : parentActivity.Id,
                ExecutionDate = task.Document.ExecutionDate,
                Text = "На регистрацию",
                IsNotActive = true,
                //IsMainLine = task.IsMainLine
            };
            db.Activities.Add(activity);
            db.SaveChanges();



            Report report = new Report()
            {
                Id = Guid.NewGuid(),
                TaskId = task.Id,
                ExecutionDate = DateTime.Now,
                Type = task.TypeEx == 1 ? 1 : 0,
                DocumentId = task.DocumentId
            };
            task.State = 2;
            task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;
            task.IsNotification = false;
            task.NotificationCount = 0;
            db.Reports.Add(report);

            db.SaveChanges();

            return
                Json(
                    new
                    {
                        State = true,
                        Task =
                            new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                    });
        }

        [HttpPost]
        public ActionResult SendContractInProcessing(Guid id)
        {
            return Json(_contractRepository.SendContractToProcessing(id));
        }

        public ActionResult ContractSave(RequestModel model)
        {
            _contractRepository.SaveContract(model);
            return Json(model.Contract.ContractStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContractAdditionSave(Contract contract)
        {
            _contractRepository.SaveContractAddition(contract);
            return Json(contract.ContractStatus, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult LoadContract(Guid id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            var project = db.Contracts.Include(e => e.ContractStatus).Include(e=>e.HolderType).FirstOrDefault(o => o.Id == id);
            Organization manufacture;
            Organization applicant;
            Organization holder;
            Organization payer;
            Organization payerTranslation;
            Contract holderContract = new Contract();

            if (project == null)
            {
                var holderType = DictionaryHelper.GetDicItemByCode(Dictionary.ContractHolderType.DicCode,
                    Dictionary.ContractHolderType.Producer);
                project = new Contract()
                {
                    Id = id,
                    ManufacturerOrganizationId = Guid.NewGuid(),
                    ApplicantOrganizationId = Guid.NewGuid(),
                    HolderOrganizationId = Guid.NewGuid(),
                    PayerOrganizationId = Guid.NewGuid(),
                    PayerTranslationOrganizationId = Guid.NewGuid(),
                    Type = 1,
                    CreatedDate = DateTime.Now,
                    DoverennostCreatedDate = DateTime.Now,
                    DoverennostExpiryDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    HolderTypeId = holderType.Id,
                    HolderTypeCode = holderType.Code,
                    OwnerId = UserHelper.GetCurrentEmployee().Id,
                    StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode, Database.DataModel.Contract.StatusNew)
                };
                manufacture = new Organization() { Id = project.ManufacturerOrganizationId.Value, Type = 21, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
                applicant = new Organization() { Id = project.ApplicantOrganizationId.Value, Type = 22, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
                holder = new Organization() { Id = project.HolderOrganizationId.Value, Type = 23, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
                payer = new Organization() { Id = project.PayerOrganizationId.Value, Type = 24, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
                payerTranslation = new Organization() { Id = project.PayerTranslationOrganizationId.Value, Type = 24, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
            }
            else
            {
                manufacture = db.Organizations.FirstOrDefault(o => o.Id == project.ManufacturerOrganizationId);
                applicant = db.Organizations.FirstOrDefault(o => o.Id == project.ApplicantOrganizationId);
                holder = db.Organizations.FirstOrDefault(o => o.Id == project.HolderOrganizationId);
                payer = db.Organizations.FirstOrDefault(o => o.Id == project.PayerOrganizationId);
                payerTranslation = db.Organizations.FirstOrDefault(o => o.Id == project.PayerTranslationOrganizationId);
                if (project.ContractId != null)
                {
                    holderContract = db.Contracts.FirstOrDefault(m => m.Id == project.ContractId);
                }
                project.HolderTypeCode = project.HolderType.Code;
            }
            return Content(JsonConvert.SerializeObject(new RequestModel
            {
                Contract = project,
                Manufacture = manufacture,
                Applicant = applicant,
                Holder = holder,
                Payer = payer,
                PayerTranslation = payerTranslation,
                HolderContract = holderContract,
            }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" }));
        }

        [HttpGet]
        public JsonResult LoadContractAddition(Guid id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;
            var project = db.Contracts.AsNoTracking().Include(e => e.ContractAdditionType)
                .Include(e => e.ContractStatus)
                .FirstOrDefault(o => o.Id == id);
            bool saved = project != null;
            if (project == null)
            {
                project = new Contract()
                {
                    Id = id,
                    Type = 2,
                    CreatedDate = DateTime.Now,
                    OwnerId = UserHelper.GetCurrentEmployee().Id,
                    StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode, Database.DataModel.Contract.StatusNew)
                };
            }
            List<string> attachCodes = new List<string>() { Dictionary.DicContractAdditionAttachType.ContractAdditionCode };
            if (project.ContractAdditionType != null)
            {
                if (project.ContractAdditionType.Code == Dictionary.DicContractAddition.BankChangedCode)
                    attachCodes.Add(Dictionary.DicContractAdditionAttachType.BankDocCode);
                if (project.ContractAdditionType.Code == Dictionary.DicContractAddition.BossChangedCode)
                    attachCodes.Add(Dictionary.DicContractAdditionAttachType.NewBossCode);
                if (project.ContractAdditionType.Code == Dictionary.DicContractAddition.LegalAddresChangeCode)
                    attachCodes.Add(Dictionary.DicContractAdditionAttachType.EgovDocCode);
                if (project.ContractAdditionType.Code == Dictionary.DicContractAddition.ContractPeriodChangedCode)
                {
                    attachCodes.Add(Dictionary.DicContractAdditionAttachType.ProducerProxyCode);
                    attachCodes.Add(Dictionary.DicContractAdditionAttachType.HolderProxyCode);
                }
                project.ContractAdditionType = null;
            }
            return Json(new
            {
                ContractAddition = project,
                AttachCodes = attachCodes,
                ContractSaved = saved
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetContract()
        {
            var userId = UserHelper.GetCurrentEmployee().Id;

            var data = db.Contracts.Where(e=>e.ContractStatus.Code==Database.DataModel.Contract.StatusActive && e.OwnerId==userId).ToList();

            return Json(data.Select(c => new { c.Id, Name = c.Number, c.EndDate }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ContractDetails(Guid id, string listAction)
        {
            ViewBag.ListAction = listAction;
            var contract = new ContractModel();
            var contractStatus = _contractRepository.GetContractStatus(id);
            contract.IsEdit = contractStatus.Code == Database.DataModel.Contract.StatusNew ||
                              contractStatus.Code == Database.DataModel.Contract.StatusOnCorrection;
            ViewBag.CanSign = contractStatus.Code == Database.DataModel.Contract.StatusOnApplicantSigningt;
            contract.ApplicantContract = db.ContractsViews.FirstOrDefault(m => m.Id == id);
            if (contract.ApplicantContract != null)
            {
                contract.Manufaturer = db.OrganizationsViews.First(m => m.Id == contract.ApplicantContract.ManufacturerOrganizationId);
                contract.Holder = db.OrganizationsViews.First(m => m.Id == contract.ApplicantContract.HolderOrganizationId);
                contract.Payer = db.OrganizationsViews.First(m => m.Id == contract.ApplicantContract.PayerOrganizationId);
                contract.Applicant = db.OrganizationsViews.First(m => m.Id == contract.ApplicantContract.ApplicantOrganizationId);
                contract.PayerTranslation = db.OrganizationsViews.First(m => m.Id == contract.ApplicantContract.PayerTranslationOrganizationId);
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
                return View(contract);
            }
            return null;
        }
        public ActionResult ContractAdditionDetails(Guid id, string listAction)
        {
            ViewBag.ListAction = listAction;
            var contract = new ContractModel();
            var contractStatus = _contractRepository.GetContractStatus(id);
            contract.IsEdit = contractStatus.Code == Database.DataModel.Contract.StatusNew ||
                              contractStatus.Code == Database.DataModel.Contract.StatusOnCorrection;
            ViewBag.CanSign = contractStatus.Code == Database.DataModel.Contract.StatusOnApplicantSigningt;
            contract.ApplicantContract = db.ContractsViews.FirstOrDefault(m => m.Id == id);
            if (contract.ApplicantContract != null)
            {
                if (contract.ApplicantContract.ContractId != null && contract.ApplicantContract.ContractId.Value != Guid.Empty)
                    contract.HolderContract = db.ContractsViews.FirstOrDefault(m => m.Id == contract.ApplicantContract.ContractId);
                return View(contract);
            }
            return null;
        }

        public ActionResult GetSigners()
        {
            string[] signerCodes = { "ncels_deputyceo", "ncels_ceo" };
            var items = db.Employees.Where(e => signerCodes.Contains(e.Position.Code)).Select(e => new
            {
                e.Id,
                Name = e.Position.ShortName + " " + e.ShortName
            });
            return Json(items.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ContractTemplate(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult GetContractTemplatePdf(Guid id)
        {
            string templatePath;
            Dictionary<string, string> templateData;
            var actualContractFile = _contractRepository.GetActualContractFile(id);
            var isContract = db.Contracts.Any(e => e.Id == id && e.ContractId == null);
            if (actualContractFile != null && actualContractFile.Extension == ".pdf")
            {
                var fileName =
                    FileHelper.GetFilePath(actualContractFile.DocumentId.Value.ToString(),
                        actualContractFile.CategoryId.Value.ToString(), null,
                        string.Format("{0}{1}", actualContractFile.Id, actualContractFile.Extension));
                return new FileStreamResult(System.IO.File.OpenRead(fileName), "application/pdf");
            }
            if (actualContractFile != null)
                templatePath = FileHelper.GetFilePath(actualContractFile.DocumentId.Value.ToString(),
                    actualContractFile.CategoryId.Value.ToString(), null,
                    string.Format("{0}{1}", actualContractFile.Id, actualContractFile.Extension));
            else
            {
                templatePath = isContract ? _contractRepository.GetContractTemplatePath(id) : _contractRepository.GetContractAdditionTemplatePath(id);
            }
            templateData = isContract ? _contractRepository.GetContractTemplateData(id) : _contractRepository.GetContractAdditionTemplateData(id);
            Aspose.Words.Document doc = new Aspose.Words.Document(templatePath);
            doc.ReplaceText(templateData);
            var file = new MemoryStream();
            doc.Save(file, SaveFormat.Pdf);
            file.Position = 0;
            return new FileStreamResult(file, "application/pdf");
        }
        public ActionResult GetContractTemplate(Guid id)
        {
            var actualContractFile = _contractRepository.GetActualContractFile(id);
            var templatePath = actualContractFile == null
                ? _contractRepository.GetContractAdditionTemplatePath(id)
                : FileHelper.GetFilePath(actualContractFile.DocumentId.Value.ToString(),
                    actualContractFile.CategoryId.Value.ToString(), null,
                    string.Format("{0}{1}", actualContractFile.Id, actualContractFile.Extension));
            Aspose.Words.Document doc = new Aspose.Words.Document(templatePath);
            doc.ReplaceText(_contractRepository.GetContractAdditionTemplateData(id));
            var file = new MemoryStream();
            doc.Save(file, SaveFormat.Docx);
            file.Position = 0;
            return new FileStreamResult(file, "application/msword");
        }

        public ActionResult ListComments(ModelRequest request, Guid contractId)
        {
            var result = db.ContractComments.Include(e => e.Author).AsNoTracking().Where(e => e.ContractId == contractId && e.Sended).ToDataSourceResult(request,
                c => new
                {
                    c.Id,
                    c.CreateDate,
                    Author = c.Author.DisplayName,
                    c.Comment
                });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SignContract(Guid contractId, string signedData)
        {
            _contractRepository.SaveSign(contractId, signedData, false);
            var activityManager = new ActivityManager();
            activityManager.ExecuteTask(activityManager.GetCurrentTaskId(contractId, Dictionary.ExpActivityType.ContractSigningByApplicantAndCeo).Value, signedData);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SignData(Guid id)
        {
            return Json(new
            {
                IsSuccess = true,
                preambleXml = _contractRepository.GetDataForSign(id)
            }, JsonRequestBehavior.AllowGet);

        }
    }
}