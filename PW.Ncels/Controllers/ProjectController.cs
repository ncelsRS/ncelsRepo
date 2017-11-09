using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Aspose.Words;
using Microsoft.Ajax.Utilities;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;
using PW.Ncels.Database.Enums.PriceProject;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using PW.Ncels.Database.Repository.Price;
using PW.Ncels.Models;
using Document = PW.Ncels.Database.DataModel.Document;
using PriceDetailsModel = PW.Ncels.Database.Models.PriceDetailsModel;

namespace PW.Ncels.Controllers {

	[Authorize()]
	public class ProjectController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();
        // GET: Project

#region VIEWS
        public ActionResult Index() {
			return View();
		}

        public ActionResult Agreement() {
			return PartialView();
		}

		public ActionResult Active() {
			return View();
		}

		public ActionResult Review() {
			return View();
		}

		public ActionResult Executed() {
			return View();
		}

		public ActionResult Draft() {
			return View();
		}

        public ActionResult Prices() {
			return View();
		}
        public ActionResult Registers() {
			return View();
		}

		public ActionResult Expertise() {
			return View();
		}

		public ActionResult Pricing() {
			return View();
		}

		public ActionResult PriceLists() {
			return View();
		}

        public ActionResult PriceLsList()
        {
            return View();
        }
        public ActionResult PriceLsModifyList()
        {
            return View();
        }
        public ActionResult PriceImnModifyList()
        {
            return View();
        }
        public ActionResult PriceImnList()
        {
            return View();
        }
        public ActionResult RePriceLsList()
        {
            return View();
        }
        public ActionResult RePriceImnList()
        {
            return View();
        }
  
        public ActionResult RegisterLsList()
        {
            return View();
        }
        public ActionResult ReRegisterLsList()
        {
            return View();
        }
        public ActionResult ChRegisterLsList()
        {
            return View();
        }

#endregion

        #region Списки

        [HttpPost]
        public async Task<JsonResult> ReadSuccess(ModelRequest request) {
            return Json(await ProjectServices.Instance.GetProject(db, request,true,0), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ReadRegisters(ModelRequest request, int? type)
        {
            return Json(await ProjectServices.Instance.GetProject(db, request, true, type), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ReadPrices(ModelRequest request,int? type)
        {
            return Json(await ProjectServices.Instance.GetProject(db, request, false,type), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ReadPriceRework(ModelRequest request,int type)
        {
            return Json(await ProjectServices.Instance.GetPriceRework(db, request,type), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> ReadSrReestrView(ModelRequest request, int type) {
            return Json(await ProjectServices.Instance.GetSrReestrView(db, request,type), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult> ReadOrphanDrugsView(ModelRequest request)
        {
            return Json(await ProjectServices.Instance.GetOrphanDrugsViewView(db, request), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Ценообразование

        #region Create

        public ActionResult PriceLs(Guid? id) {
            var user = UserHelper.GetCurrentEmployee();
            ViewBag.EditorId = user.Id;
            ViewBag.PriceProjectType = PriceProjectType.PriceLs;
            ViewBag.AllowReason = false;
            return View(id ?? Guid.NewGuid());
        }

        public ActionResult RePriceLsNew(Guid? id) {
            var user = UserHelper.GetCurrentEmployee();
            ViewBag.EditorId = user.Id;
            ViewBag.PriceProjectType = PriceProjectType.RePriceLs;
            ViewBag.AllowReason = false;
            return View(id ?? Guid.NewGuid());
        }

        public ActionResult RePriceImnNew(Guid? id){
            var user = UserHelper.GetCurrentEmployee();
            ViewBag.EditorId = user.Id;
            ViewBag.PriceProjectType = PriceProjectType.RePriceImn;
            ViewBag.AllowReason = false;
            return View(id ?? Guid.NewGuid());
        }

        public ActionResult PriceReject(Guid? id){
            return View(id ?? Guid.NewGuid());
        }

        public ActionResult CreateRejectDocument(RequestModel request) {
	        var reason = db.Dictionaries.FirstOrDefault(x => x.Id == request.Project.ReasonDicId.Value);
            var templatePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/DocxTemplates/PriceReject.docx");
            var templateData = new Dictionary<string, string>();

	        var registerData = request.Project.NameRu;
	        if (!string.IsNullOrEmpty(request.Project.MnnRu))
	            registerData += ", " + request.Project.MnnRu;
            if (!string.IsNullOrEmpty(request.Project.FormNameRu))
                registerData += ", " + request.Project.FormNameRu;
            if (!string.IsNullOrEmpty(request.Project.Dosage))
                registerData += ", " + request.Project.Dosage;
            if (!string.IsNullOrEmpty(request.Project.Concentration))
                registerData += ", " + request.Project.Concentration;
            if (!string.IsNullOrEmpty(request.Project.Volume) && !request.Project.Volume.Equals("0"))
                registerData += ", " + request.Project.Volume;
            if (!string.IsNullOrEmpty(request.Project.CountPackage) && !request.Project.CountPackage.Equals("0"))
                registerData += ", " + request.Project.CountPackage;

            templateData.Add("RejectReason", reason.DisplayName);
            templateData.Add("RegisterData", registerData);
            Aspose.Words.Document doc = new Aspose.Words.Document(templatePath);
            doc.ReplaceText(templateData);
            var file = new MemoryStream();
            doc.Save(file, SaveFormat.Pdf);
            file.Position = 0;

	        var docId = Guid.NewGuid();
	        var attachPath = FileHelper.GetObjectPathRoot();
	        var user = UserHelper.GetCurrentEmployee();

            Document document = new Document{
                Id = docId,
                DocumentType = (int)DocumentType.Project,
                ProjectType = (int)ProjectType.Administrative,
                StateType = 0,
                AttachPath = attachPath,
                CorrespondentsInfo = user.DisplayName,
                CorrespondentsId = user.Id.ToString(),
                CorrespondentsValue = user.DisplayName,
                CreatedUserId = user.Id.ToString(),
                CreatedUserValue = user.DisplayName,
                ApplicantType = (int)ApplicantType.Natural,
                DocumentDate = DateTime.Now
            };
            db.Documents.Add(document);
            db.SaveChanges();

            //FileHelper.SaveFile(null, docId.ToString(), "Письмо о неучастии.pdf", file, db);
	        FileHelper.UploadFile(attachPath, "Письмо о неучастии.pdf", file);
            //return new FileStreamResult(file, "application/pdf");

            //todo create Document
            //todo set FilePath

            //todo PriceRejectProject with DocumentId

            var rejectProject = new PriceRejectProject {
                Id = Guid.NewGuid(),
                RejectReasonDicId = reason.Id,
                DocumentId = docId,
                RegNumber = request.Project.RegNumber,
                RegisterId = request.Project.RegisterId,
                RegisterDfId = request.Project.RegisterDfId

            };
	        db.PriceRejectProjects.Add(rejectProject);
	        db.SaveChanges();

            return Json(docId);
        }

	    public ActionResult SendProjectPrice(RequestModel request) {
            Document baseDocument = db.Documents.FirstOrDefault(m => m.Id == request.Project.Id);
            if (baseDocument == null)
            {
                var user = UserHelper.GetCurrentEmployee();
                
                Document document = new Document()
                {
                    Id = request.Project.Id,
                    DocumentType = 0,
                    ProjectType = 0,
                    ExecutionDate = DateTime.Now.AddDays(30),
                    DocumentDate = DateTime.Now,
                    AttachPath = FileHelper.GetObjectPathRoot(),
                    CorrespondentsInfo = UserHelper.GetCurrentEmployee().DisplayName,
                    CorrespondentsId = user.Id.ToString(),
                    CorrespondentsValue = user.DisplayName,
                    IsTradeSecret = false
                };
                var project = db.PriceProjects.First(o => o.Id == request.Project.Id);

                project.Status = (int)PriceProjectStatus.OnRegistration;
                db.Entry(project).State = EntityState.Modified;
                db.Documents.Add(document);
                db.SaveChanges();
                SendNewAppNotification(request.Project.Id);
            }
            else
            {
                baseDocument.IsTradeSecret = false;
                Document baseDocument2 = db.Documents.Where(m => m.DocumentType == 1).OrderByDescending(m => m.CreatedDate).FirstOrDefault(m => m.AnswersId == request.Project.Id.ToString());

                var project = db.PriceProjects.FirstOrDefault(o => o.Id == baseDocument.Id);
                if (project != null) {
                    project.Status = (int)PriceProjectStatus.Registered;
                    db.Entry(project).State = EntityState.Modified;
                    db.SaveChanges();
                }

                if (baseDocument2 != null && !string.IsNullOrEmpty(baseDocument2.RegistratorId)) {
                    var registartor = db.Employees.FirstOrDefault(x => x.Id == new Guid(baseDocument2.RegistratorId));
                    if (registartor != null) {
                        new NotificationManager().SendNotification(
                            string.Format("Заявление №{0} исправлено заявителем", string.IsNullOrEmpty(baseDocument.OutgoingNumber) ? baseDocument.Number : baseDocument.OutgoingNumber),
                            ObjectType.Letter, baseDocument2.Id, registartor.Id);
                    }
                }

                if (baseDocument2 != null && baseDocument2.MainTaskId != null)
                {

                    Database.DataModel.Task task = db.Tasks.FirstOrDefault(m => m.Id == baseDocument2.MainTaskId);
                    if (task != null)
                    {
                        Activity activity = new Activity
                        {
                            Id = Guid.NewGuid(),
                            ParentTask = task.Id,
                            DocumentId = task.DocumentId,
                            AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                            AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                            ExecutorsId = task.ExecutorId,
                            ExecutorsValue = task.ExecutorValue,
                            //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                            //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                            Type = 2,
                            IsParrent = false,
                            CreatedDate = DateTime.Now,
                            ParentId = task.ActivityId,
                            ExecutionDate = task.Document.ExecutionDate,
                            Text = "Замечания исправлены",
                            IsNotActive = false,
                            TypeEx = task.Stage,
                            //IsMainLine = task.IsMainLine
                        };
                        db.Activities.Add(activity);
                        db.SaveChanges();
                    }
                }

            }

            return Json("Ок");
        }

        public ActionResult SendProjectPriceParent(RequestModel request)
        {
            Document baseDocument = db.Documents.FirstOrDefault(m => m.Id == request.Project.Id);
            if (baseDocument == null){
                //Случай когда на самом деле это не изменение
                if (request.Project.PriceProjectId == null) {
                    return SendProjectPrice(request);
                }

                var user = UserHelper.GetCurrentEmployee();
                var parentDocNumber = db.Documents.First(m => m.Id == request.Project.PriceProjectId).Number;
                var priceCount = db.PriceProjects.Count(m => m.PriceProjectId == request.Project.PriceProjectId && m.Status > 0);
                string number = string.Format("{0}.{1}", parentDocNumber, priceCount);
                Document document = new Document()
                {
                    Id = request.Project.Id,
                    DocumentType = 0,
                    ProjectType = 0,
                    ExecutionDate = DateTime.Now.AddDays(30),
                    DocumentDate = DateTime.Now,
                    AttachPath = FileHelper.GetObjectPathRoot(),
                    CorrespondentsInfo = UserHelper.GetCurrentEmployee().DisplayName,
                    CorrespondentsId = user.Id.ToString(),
                    CorrespondentsValue = user.DisplayName,
                    IsTradeSecret = false,
                    Number = number
                };
                var project = db.PriceProjects.First(o => o.Id == request.Project.Id);

                project.Status = (int)PriceProjectStatus.OnRegistration;
                db.Entry(project).State = EntityState.Modified;
                db.Documents.Add(document);

                var prices = db.Prices.Where(e => e.PriceProjectId == project.PriceProjectId);
                foreach (var price in prices)
                {
                    var newPrice = new Price()
                    {
                        Type = price.Type,
                        AvgObkCost = price.AvgObkCost,
                        AvgOptCost = price.AvgOptCost,
                        AvgOptPrice = price.AvgOptPrice,
                        AvgOptPriceCurrencyDicId = price.AvgOptPriceCurrencyDicId,
                        AvgOptPriceNote = price.AvgOptPriceNote,
                        AvgRozPrice = price.AvgRozPrice,
                        AvgRozPriceCurrencyDicId = price.AvgRozPriceCurrencyDicId,
                        AvgRozPriceNote = price.AvgRozPriceNote,
                        AvgRznCost = price.AvgRznCost,
                        BritishCost = price.BritishCost,
                        BritishPrice = price.BritishPrice,
                        CalcDateEnd = price.CalcDateEnd,
                        CalcDateStart = price.CalcDateStart,
                        CipPrice = price.CipPrice,
                        CipPriceCurrencyDicId = price.CipPriceCurrencyDicId,
                        CountryId = price.CountryId,
                        CreatedDate = price.CreatedDate,
                        Description = price.Description,
                        LimitCost = price.LimitCost,
                        LimitPrice = price.LimitPrice,
                        LimitPriceCurrencyDicId = price.LimitPriceCurrencyDicId,
                        LimitPriceNote = price.LimitPriceNote,
                        ManufacturerPrice = price.ManufacturerPrice,
                        ManufacturerPriceCurrencyDicId = price.ManufacturerPriceCurrencyDicId,
                        ManufacturerPriceNote = price.ManufacturerPriceNote,
                        MarkupCost = price.MarkupCost,
                        MarkupCostOpt = price.MarkupCostOpt,
                        MinimalCost = price.MinimalCost,
                        MtPartsId = price.MtPartsId,
                        Name = price.Name,
                        OriginalCost = price.OriginalCost,
                        OwnerPrice = price.OwnerPrice,
                        OwnerPriceCurrencyDicId = price.OwnerPriceCurrencyDicId,
                        PriceProjectId = project.Id,
                        RefPrice = price.RefPrice,
                        RefPriceCurrencyDicId = price.RefPriceCurrencyDicId,
                        RefPriceTypeDicId = price.RefPriceTypeDicId,
                        RequestDate = price.RequestDate,
                        UnitPrice = price.UnitPrice,
                        UnitPriceCurrencyDicId = price.UnitPriceCurrencyDicId,
                        ZakupCost = price.ZakupCost,
                        Id = Guid.NewGuid()
                    };
                    db.Prices.Add(newPrice);
                }

                db.SaveChanges();
                SendNewAppNotification(request.Project.Id);
            }
            else
            {
                baseDocument.IsTradeSecret = false;
                Document baseDocument2 = db.Documents.Where(m => m.DocumentType == 1).OrderByDescending(m => m.CreatedDate).FirstOrDefault(m => m.AnswersId == request.Project.Id.ToString());

                var project = db.PriceProjects.FirstOrDefault(o => o.Id == baseDocument.Id);
                if (project != null){
                    project.Status = (int)PriceProjectStatus.OnAnalize;
                    db.Entry(project).State = EntityState.Modified;
                    var prices = db.Prices.Where(e => e.PriceProjectId == project.PriceProjectId);
                    foreach (var price in prices)
                    {
                        var newPrice = new Price()
                        {
                            Type = price.Type,
                            AvgObkCost = price.AvgObkCost,
                            AvgOptCost = price.AvgOptCost,
                            AvgOptPrice = price.AvgOptPrice,
                            AvgOptPriceCurrencyDicId = price.AvgOptPriceCurrencyDicId,
                            AvgOptPriceNote = price.AvgOptPriceNote,
                            AvgRozPrice = price.AvgRozPrice,
                            AvgRozPriceCurrencyDicId = price.AvgRozPriceCurrencyDicId,
                            AvgRozPriceNote = price.AvgRozPriceNote,
                            AvgRznCost = price.AvgRznCost,
                            BritishCost = price.BritishCost,
                            BritishPrice = price.BritishPrice,
                            CalcDateEnd = price.CalcDateEnd,
                            CalcDateStart = price.CalcDateStart,
                            CipPrice = price.CipPrice,
                            CipPriceCurrencyDicId = price.CipPriceCurrencyDicId,
                            CountryId = price.CountryId,
                            CreatedDate = price.CreatedDate,
                            Description = price.Description,
                            LimitCost = price.LimitCost,
                            LimitPrice = price.LimitPrice,
                            LimitPriceCurrencyDicId = price.LimitPriceCurrencyDicId,
                            LimitPriceNote = price.LimitPriceNote,
                            ManufacturerPrice = price.ManufacturerPrice,
                            ManufacturerPriceCurrencyDicId = price.ManufacturerPriceCurrencyDicId,
                            ManufacturerPriceNote = price.ManufacturerPriceNote,
                            MarkupCost = price.MarkupCost,
                            MarkupCostOpt = price.MarkupCostOpt,
                            MinimalCost = price.MinimalCost,
                            MtPartsId = price.MtPartsId,
                            Name = price.Name,
                            OriginalCost = price.OriginalCost,
                            OwnerPrice = price.OwnerPrice,
                            OwnerPriceCurrencyDicId = price.OwnerPriceCurrencyDicId,
                            PriceProjectId = project.Id,
                            RefPrice = price.RefPrice,
                            RefPriceCurrencyDicId = price.RefPriceCurrencyDicId,
                            RefPriceTypeDicId = price.RefPriceTypeDicId,
                            RequestDate = price.RequestDate,
                            UnitPrice = price.UnitPrice,
                            UnitPriceCurrencyDicId = price.UnitPriceCurrencyDicId,
                            ZakupCost = price.ZakupCost,
                            Id = Guid.NewGuid()
                        };
                        db.Prices.Add(newPrice);
                    }
                    db.SaveChanges();
                }

                if (baseDocument2 != null && !string.IsNullOrEmpty(baseDocument2.RegistratorId)) {
                    var registartor = db.Employees.FirstOrDefault(x => x.Id == new Guid(baseDocument2.RegistratorId));
                    if (registartor != null) {
                        new NotificationManager().SendNotification(
                            string.Format("Заявление №{0} исправлено заявителем", string.IsNullOrEmpty(baseDocument.OutgoingNumber) ? baseDocument.Number : baseDocument.OutgoingNumber),
                            ObjectType.Letter, baseDocument2.Id, registartor.Id);
                    }
                }

                if (baseDocument2 != null && baseDocument2.MainTaskId != null)
                {

                    Database.DataModel.Task task = db.Tasks.FirstOrDefault(m => m.Id == baseDocument2.MainTaskId);
                    if (task != null)
                    {
                        Activity activity = new Activity
                        {
                            Id = Guid.NewGuid(),
                            ParentTask = task.Id,
                            DocumentId = task.DocumentId,
                            AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                            AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                            ExecutorsId = task.ExecutorId,
                            ExecutorsValue = task.ExecutorValue,
                            //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                            //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                            Type = 2,
                            IsParrent = false,
                            CreatedDate = DateTime.Now,
                            ParentId = task.ActivityId,
                            ExecutionDate = task.Document.ExecutionDate,
                            Text = "Замечания исправлены",
                            IsNotActive = false,
                            TypeEx = task.Stage,
                            //IsMainLine = task.IsMainLine
                        };
                        db.Activities.Add(activity);
                        db.SaveChanges();
                    }
                }

            }
            return Json("Ок");
        }

	    private void SendNewAppNotification(Guid appId)
	    {
	        var executors = db.EmployeePermissionRoles.Join(db.PermissionRoleKeys, x => x.PermissionRoleId,
	                x => x.PermissionRoleId,
	                (epr, prk) => new
	                {
	                    epr.EmployeeId,
	                    prk.PermissionKey,
	                    prk.PermissionValue
	                }).Where(o => o.PermissionKey == "IsPriceProjectProcCenterVisibility" && o.PermissionValue.ToLower() == "true").Select(e => e.EmployeeId)
	            .ToList();
            var nm=new NotificationManager();
	        foreach (var executor in executors)
	        {
	            nm.SendNotification("Поступила новая заявка на регистрацию", ObjectType.PriceProject, appId, executor);
	        }
        }

        public ActionResult PriceLsSave(RequestModel model) {
            var project = db.PriceProjects.Any(o => o.Id == model.Project.Id);
            if (project)
            {
                db.Entry(model.Project).State = EntityState.Modified;
                db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
                db.Entry(model.HolderOrganization).State = EntityState.Modified;
                db.Entry(model.ProxyOrganization).State = EntityState.Modified;
                db.Entry(model.CurentPrice).State = EntityState.Modified;

            }
            else
            {
                db.PriceProjects.Add(model.Project);
                db.Organizations.Add(model.ManufacturerOrganization);
                db.Organizations.Add(model.HolderOrganization);
                db.Organizations.Add(model.ProxyOrganization);
                db.Prices.Add(model.CurentPrice);
         

            }
            db.SaveChanges();
            return Json("Ок",JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadPriceLs(Guid id)
        {
            var project = db.PriceProjects.FirstOrDefault(o => o.Id == id);
            Organization holderOrganization;
            Organization proxyOrganization;
            Organization manufacturerOrganization;
            Price curentPrice;
            if (project == null)
            {
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                Guid tengeDicId = DictionaryHelper.GetDicIdByCode("Currency", "398");

                project = new PriceProject()
                {
                    Id = id,
                    HolderOrganizationId = Guid.NewGuid(),
                    ProxyOrganizationId = Guid.NewGuid(),
                    ManufacturerOrganizationId = Guid.NewGuid(),
                    Type = (int)PriceProjectType.PriceLs,
                    PayDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    ConclusionDate = DateTime.Now,
                    ContrDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    RegDate = DateTime.Now,
                    DoverennostCreatedDate = DateTime.Now,
                    DoverennostExpiryDate = DateTime.Now,
                    OwnerId = employeeId
                };
                holderOrganization = ProjectServices.Instance.GetOrganizationPrice(db, project.HolderOrganizationId, 0);
                proxyOrganization = ProjectServices.Instance.GetOrganizationPrice(db, project.ProxyOrganizationId, 1);
                manufacturerOrganization =new Organization() { Id = project.ManufacturerOrganizationId, Type = 2, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };

                 curentPrice = new Price() {Type = (int)PriceType.LsCurrentPrice, PriceProjectId = id, Id = Guid.NewGuid(), OwnerPriceCurrencyDicId = tengeDicId, UnitPriceCurrencyDicId = tengeDicId };
            }
            else
            {
                holderOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.HolderOrganizationId);
                proxyOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.ProxyOrganizationId);
                manufacturerOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.ManufacturerOrganizationId);
                curentPrice = db.Prices.FirstOrDefault(o => project.Type == (int)PriceProjectType.PriceLs && o.PriceProjectId == id);
            }
            return Json(new RequestModel {
                Project = project,
                HolderOrganization = holderOrganization,
                ProxyOrganization = proxyOrganization,
                ManufacturerOrganization = manufacturerOrganization,
                CurentPrice = curentPrice,
                Price = new Price() {Type = (int)PriceType.LsPrice, PriceProjectId =  id },
                StartDateDay = project.DoverennostCreatedDate?.Day ?? DateTime.Now.Day,
                StartDateMonth = project.DoverennostCreatedDate?.Month ?? DateTime.Now.Month,
                StartDateYear = project.DoverennostCreatedDate?.Year ?? DateTime.Now.Year,
                EndDateDay = project.DoverennostExpiryDate?.Day ?? DateTime.Now.Day,
                EndDateMonth = project.DoverennostExpiryDate?.Month ?? DateTime.Now.Month,
                EndDateYear = project.DoverennostExpiryDate?.Year ?? DateTime.Now.Year,
            }, JsonRequestBehavior.AllowGet);
		}

        public ActionResult PriceImn(Guid? id) {
            var user = UserHelper.GetCurrentEmployee();
            ViewBag.EditorId = user.Id;
            ViewBag.PriceProjectType = PriceProjectType.PriceImn;
            ViewBag.AllowReason = false;
            return View(id ?? Guid.NewGuid());
        }

        [HttpGet]
        public JsonResult LoadPriceImn(Guid id) {
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var project = db.PriceProjects.FirstOrDefault(o => o.Id == id);
            Organization holderOrganization;
            Organization proxyOrganization;
            Organization manufacturerOrganization;
            Guid tengeDicId = DictionaryHelper.GetDicIdByCode("Currency", "398");

            if (project == null) {

                project = new PriceProject() {
                    Id = id,
                    HolderOrganizationId = Guid.NewGuid(),
                    ProxyOrganizationId = Guid.NewGuid(),
                    ManufacturerOrganizationId = Guid.NewGuid(),
                    Type = (int)PriceProjectType.PriceImn,
                    PayDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    ConclusionDate = DateTime.Now,
                    ContrDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    RegDate = DateTime.Now,
                    DoverennostCreatedDate = DateTime.Now,
                    DoverennostExpiryDate = DateTime.Now,
                    OwnerId = employeeId
                };
                holderOrganization = ProjectServices.Instance.GetOrganizationPrice(db, project.HolderOrganizationId,3);
                proxyOrganization = ProjectServices.Instance.GetOrganizationPrice(db, project.ProxyOrganizationId, 4);
                manufacturerOrganization = new Organization() { Id = project.ManufacturerOrganizationId, Type = 5, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
            }
            else {
                holderOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.HolderOrganizationId);
                proxyOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.ProxyOrganizationId);
                manufacturerOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.ManufacturerOrganizationId);

            }
            return Json(new RequestModel {
                Project = project,
                HolderOrganization = holderOrganization,
                ProxyOrganization = proxyOrganization,
                ManufacturerOrganization = manufacturerOrganization,
                CurentPrice = new Price() { Type = (int)PriceType.ImnCurrentPrice, PriceProjectId = id , UnitPriceCurrencyDicId = tengeDicId, Id = Guid.NewGuid() },
                Price = new Price() { Type = (int)PriceType.ImnPrice, PriceProjectId = id, Id = Guid.NewGuid() },
                StartDateDay = project.DoverennostCreatedDate?.Day ?? DateTime.Now.Day,
                StartDateMonth = project.DoverennostCreatedDate?.Month ?? DateTime.Now.Month,
                StartDateYear = project.DoverennostCreatedDate?.Year ?? DateTime.Now.Year,
                EndDateDay = project.DoverennostExpiryDate?.Day ?? DateTime.Now.Day,
                EndDateMonth = project.DoverennostExpiryDate?.Month ?? DateTime.Now.Month,
                EndDateYear = project.DoverennostExpiryDate?.Year ?? DateTime.Now.Year,
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PriceImnSave(RequestModel model) {
            var project = db.PriceProjects.Any(o => o.Id == model.Project.Id);
            if (project) {
                db.Entry(model.Project).State = EntityState.Modified;
                db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
                db.Entry(model.HolderOrganization).State = EntityState.Modified;
                db.Entry(model.ProxyOrganization).State = EntityState.Modified;
            }
            else {
                db.PriceProjects.Add(model.Project);
                db.Organizations.Add(model.ManufacturerOrganization);
                db.Organizations.Add(model.HolderOrganization);
                db.Organizations.Add(model.ProxyOrganization);
            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PriceImnSaveNotCompleteness(RequestModel model) {
            var project = db.PriceProjects.Any(o => o.Id == model.Project.Id);
            if (project) {

                //Удаление старых которые в компелктности
                var prices = db.Prices.Where(o => o.PriceProjectId == model.Project.Id && o.MtPartsId != null);
                db.Prices.RemoveRange(prices);
                db.SaveChanges();

                db.Entry(model.Project).State = EntityState.Modified;
                db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
                db.Entry(model.HolderOrganization).State = EntityState.Modified;
                db.Entry(model.ProxyOrganization).State = EntityState.Modified;

                var isModified = db.Prices.Any(o => o.Id == model.CurentPrice.Id);
                if (isModified) {
                    db.Entry(model.CurentPrice).State = EntityState.Modified;
                }
                else {
                    db.Prices.Add(model.CurentPrice);
                }
            }
            else {
                db.PriceProjects.Add(model.Project);
                db.Organizations.Add(model.ManufacturerOrganization);
                db.Organizations.Add(model.HolderOrganization);
                db.Organizations.Add(model.ProxyOrganization);
                db.Prices.Add(model.CurentPrice);
            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult RePriceImn(Guid? id, Guid? parentId)
        {
            ViewBag.ParentId = parentId;
            var user = UserHelper.GetCurrentEmployee();
            ViewBag.EditorId = user.Id;
            ViewBag.PriceProjectType = PriceProjectType.RePriceImn;
            ViewBag.AllowReason = true;
            return View(id ?? Guid.NewGuid());
        }

        [HttpGet]
        public JsonResult LoadRePriceImnParent(Guid id, Guid parentId, int? priceProjectType)
        {
            var project = db.PriceProjects.Any(m => m.PriceProjectId == parentId)
                       ? db.PriceProjects.Where(m => m.PriceProjectId == parentId).OrderByDescending(m => m.CreatedDate).First()
                       : db.PriceProjects.First(o => o.Id == parentId);

            Organization holderOrganization = db.Organizations.FirstOrDefault(m => m.Id == project.HolderOrganizationId);
            Organization proxyOrganization = db.Organizations.FirstOrDefault(m => m.Id == project.ProxyOrganizationId);
            Organization manufacturerOrganization = db.Organizations.FirstOrDefault(m => m.Id == project.ManufacturerOrganizationId);

            //Price price = db.Prices.Any(m => m.PriceProjectId == project.Id && m.Type == (int)PriceType.ImnPrice) ? db.Prices.First(m => m.PriceProjectId == project.Id && m.Type == (int)PriceType.ImnPrice) : new Price { Id = Guid.NewGuid() };
            Price curentPrice = null;

            if (priceProjectType.HasValue && priceProjectType == (int) PriceProjectType.RePriceImn){
                //curentPrice = db.Prices.Any(m => m.PriceProjectId == parentId && m.Type == 2 && m.MtPartsId == null) ? db.Prices.First(m => m.PriceProjectId == parentId && m.Type == 2 && m.MtPartsId == null) : new Price { Id = Guid.NewGuid() };
                
                //price = db.Prices.Any(m => m.PriceProjectId == parentId && m.Type == 3) ? db.Prices.First(m => m.PriceProjectId == parentId && m.Type == 3) : new Price { Id = Guid.NewGuid() };
                
                var currentPrices = db.Prices.Where(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.ImnCurrentPrice && m.MtPartsId != null);
                if (currentPrices.Any()) {
                    foreach (var priceItem in currentPrices) {
                        var p = new Price {
                            PriceProjectId = id,
                            Type = (int) PriceType.ReImnCurrentPrice,
                            ManufacturerPrice = priceItem.ManufacturerPrice,
                            ManufacturerPriceCurrencyDicId = priceItem.ManufacturerPriceCurrencyDicId,
                            CipPrice = priceItem.CipPrice,
                            CipPriceCurrencyDicId = priceItem.CipPriceCurrencyDicId,
                            RefPriceCurrencyDicId = priceItem.RefPriceCurrencyDicId,
                            RefPrice = priceItem.RefPrice,
                            RefPriceTypeDicId = priceItem.RefPriceTypeDicId,
                            OwnerPrice = priceItem.OwnerPrice,
                            OwnerPriceCurrencyDicId = priceItem.OwnerPriceCurrencyDicId,
                            UnitPrice = priceItem.UnitPrice,
                            UnitPriceCurrencyDicId = priceItem.UnitPriceCurrencyDicId,
                            CreatedDate = priceItem.CreatedDate,
                            MtPartsId = priceItem.MtPartsId,
                            Id = Guid.NewGuid(),
                        };
                        db.Prices.Add(p);
                    }
                    db.SaveChanges();
                    curentPrice = new Price { Id = Guid.NewGuid() };
                }
                else {
                    curentPrice = db.Prices.Any(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.ImnCurrentPrice && m.MtPartsId == null) 
                        ? db.Prices.First(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.ImnCurrentPrice && m.MtPartsId == null) 
                        : new Price { Id = Guid.NewGuid()};
                }

                var prices = db.Prices.Where(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.ImnPrice);
                if (prices.Any()){
                    foreach (var priceItem in prices){
                        var p = new Price{
                            PriceProjectId = id,
                            Type = (int)PriceType.ReImnPrice,
                            CountryId = priceItem.CountryId,
                            ManufacturerPrice = priceItem.ManufacturerPrice,
                            ManufacturerPriceCurrencyDicId = priceItem.ManufacturerPriceCurrencyDicId,
                            ManufacturerPriceNote = priceItem.ManufacturerPriceNote,
                            CreatedDate = priceItem.CreatedDate,
                            Id = Guid.NewGuid(),
                        };
                        db.Prices.Add(p);
                    }
                    db.SaveChanges();
                }
            }

            if (holderOrganization == null)
                holderOrganization = new Organization();
            holderOrganization.Id = Guid.NewGuid();

            if (proxyOrganization == null)
                proxyOrganization = new Organization();
            proxyOrganization.Id = Guid.NewGuid();

            if (manufacturerOrganization == null)
                manufacturerOrganization = new Organization();
            manufacturerOrganization.Id = Guid.NewGuid();

            
            Price price = new Price { Id = Guid.NewGuid() };

            project.Id = id;
            project.PriceProjectId = parentId;
            curentPrice.PriceProjectId = id;
            curentPrice.Id = Guid.NewGuid();

            curentPrice.Type = (int)PriceType.ReImnCurrentPrice;
            price.PriceProjectId = id;
            price.Type = (int)PriceType.ReImnPrice;
            project.Type = (int)PriceProjectType.RePriceImn;

            project.ManufacturerOrganizationId = manufacturerOrganization.Id;
            project.HolderOrganizationId = holderOrganization.Id;
            project.ProxyOrganizationId = proxyOrganization.Id;

            return Json(new RequestModel
            {
                Project = project,
                HolderOrganization =  holderOrganization,
                ProxyOrganization = proxyOrganization,
                ManufacturerOrganization = manufacturerOrganization,
                CurentPrice =  curentPrice,
                Price = price,
                StartDateDay = project.DoverennostCreatedDate?.Day ?? DateTime.Now.Day,
                StartDateMonth = project.DoverennostCreatedDate?.Month ?? DateTime.Now.Month,
                StartDateYear = project.DoverennostCreatedDate?.Year ?? DateTime.Now.Year,
                EndDateDay = project.DoverennostExpiryDate?.Day ?? DateTime.Now.Day,
                EndDateMonth = project.DoverennostExpiryDate?.Month ?? DateTime.Now.Month,
                EndDateYear = project.DoverennostExpiryDate?.Year ?? DateTime.Now.Year
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadRePriceImn(Guid id) {
            Guid tengeDicId = DictionaryHelper.GetDicIdByCode("Currency", "398");
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var project = db.PriceProjects.FirstOrDefault(o => o.Id == id);
            Organization holderOrganization;
            Organization proxyOrganization;
            Organization manufacturerOrganization;
            if (project == null) {
                project = new PriceProject() {
                    Id = id,
                    HolderOrganizationId = Guid.NewGuid(),
                    ProxyOrganizationId = Guid.NewGuid(),
                    ManufacturerOrganizationId = Guid.NewGuid(),
                    Type = (int)PriceProjectType.RePriceImn,
                    PayDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    ConclusionDate = DateTime.Now,
                    ContrDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    RegDate = DateTime.Now,
                    DoverennostCreatedDate = DateTime.Now,
                    DoverennostExpiryDate = DateTime.Now,
                    OwnerId = employeeId
                };
                holderOrganization = ProjectServices.Instance.GetOrganizationPrice(db, project.HolderOrganizationId, 6);
                proxyOrganization = ProjectServices.Instance.GetOrganizationPrice(db, project.ProxyOrganizationId, 7);
                manufacturerOrganization = new Organization() { Id = project.ManufacturerOrganizationId, Type = 8, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };
            }
            else {
                holderOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.HolderOrganizationId);
                proxyOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.ProxyOrganizationId);
                manufacturerOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.ManufacturerOrganizationId);

            }
            return Json(new RequestModel {
                Project = project,
                HolderOrganization = holderOrganization,
                ProxyOrganization = proxyOrganization,
                ManufacturerOrganization = manufacturerOrganization,
                CurentPrice = new Price() { Type = (int)PriceType.ReImnCurrentPrice, PriceProjectId = id , UnitPriceCurrencyDicId = tengeDicId, Id = Guid.NewGuid() },
                Price = new Price() { Type = (int)PriceType.ReImnPrice, PriceProjectId = id, Id = Guid.NewGuid() },
                StartDateDay = project.DoverennostCreatedDate?.Day ?? DateTime.Now.Day,
                StartDateMonth = project.DoverennostCreatedDate?.Month ?? DateTime.Now.Month,
                StartDateYear = project.DoverennostCreatedDate?.Year ?? DateTime.Now.Year,
                EndDateDay = project.DoverennostExpiryDate?.Day ?? DateTime.Now.Day,
                EndDateMonth = project.DoverennostExpiryDate?.Month ?? DateTime.Now.Month,
                EndDateYear = project.DoverennostExpiryDate?.Year ?? DateTime.Now.Year,
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RePriceImnSave(RequestModel model) {
            var project = db.PriceProjects.Any(o => o.Id == model.Project.Id);
            if (project) {
                db.Entry(model.Project).State = EntityState.Modified;
                db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
                db.Entry(model.HolderOrganization).State = EntityState.Modified;
                db.Entry(model.ProxyOrganization).State = EntityState.Modified;
            }
            else {
                db.PriceProjects.Add(model.Project);
                db.Organizations.Add(model.ManufacturerOrganization);
                db.Organizations.Add(model.HolderOrganization);
                db.Organizations.Add(model.ProxyOrganization);
                //db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
                //db.Entry(model.HolderOrganization).State = EntityState.Modified;
                //db.Entry(model.ProxyOrganization).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

	    public ActionResult RePriceImnSaveNotCompleteness(RequestModel model)
	    {
	        var project = db.PriceProjects.Any(o => o.Id == model.Project.Id);
	        if (project)
	        {
                db.Entry(model.Project).State = EntityState.Modified;
                db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
	            db.Entry(model.HolderOrganization).State = EntityState.Modified;
                db.Entry(model.ProxyOrganization).State = EntityState.Modified;

	            var isModified = db.Prices.Any(o => o.Id == model.CurentPrice.Id);
	            if (isModified)
	            {
                    db.Entry(model.CurentPrice).State = EntityState.Modified;
	            }
	            else
	            {
	                db.Prices.Add(model.CurentPrice);
	            }
	        }
	        else
	        {
	            db.PriceProjects.Add(model.Project);
                db.Organizations.Add(model.ManufacturerOrganization);
                db.Organizations.Add(model.HolderOrganization);
                db.Organizations.Add(model.ProxyOrganization);
                //db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
                //db.Entry(model.HolderOrganization).State = EntityState.Modified;
                //db.Entry(model.ProxyOrganization).State = EntityState.Modified;
                db.Prices.Add(model.CurentPrice);
	        }
	        db.SaveChanges();
	        return Json("Ок", JsonRequestBehavior.AllowGet);
	    }

        public ActionResult RePriceLs(Guid? id,Guid? parentId) {
            var user = UserHelper.GetCurrentEmployee();
            ViewBag.EditorId = user.Id;
            ViewBag.ParentId = parentId;
            ViewBag.PriceProjectType = PriceProjectType.RePriceLs;
            ViewBag.AllowReason = true;
            return View(id ?? Guid.NewGuid());
        }
      
        public ActionResult RePriceLsSave(RequestModel model)
        {
            var project = db.PriceProjects.Any(o => o.Id == model.Project.Id);
            if (project) {
                db.Entry(model.Project).State = EntityState.Modified;
                db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
                db.Entry(model.HolderOrganization).State = EntityState.Modified;
                db.Entry(model.ProxyOrganization).State = EntityState.Modified;

                if (model.CurentPrice != null) {
                    db.Entry(model.CurentPrice).State = EntityState.Modified;
                }
            }
            else {
                db.PriceProjects.Add(model.Project);
                db.Organizations.Add(model.ManufacturerOrganization);
                db.Organizations.Add(model.HolderOrganization);
                db.Organizations.Add(model.ProxyOrganization);
                if (model.CurentPrice != null) {
                    db.Prices.Add(model.CurentPrice);
                }
                //db.Entry(model.ManufacturerOrganization).State = EntityState.Modified;
                //db.Entry(model.HolderOrganization).State = EntityState.Modified;
                //db.Entry(model.ProxyOrganization).State = EntityState.Modified;
                //db.Entry(model.CurentPrice).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadRePriceLsParent(Guid id,Guid parentId)
        {
            var project = db.PriceProjects.Any(m=>m.PriceProjectId==parentId)
                ? db.PriceProjects.Where(m => m.PriceProjectId == parentId).OrderByDescending(m=>m.CreatedDate).First()
                : db.PriceProjects.First(o => o.Id == parentId);
          
            Organization holderOrganization = db.Organizations.FirstOrDefault(m => m.Id == project.HolderOrganizationId); 
            Organization proxyOrganization = db.Organizations.FirstOrDefault(m => m.Id == project.ProxyOrganizationId);
            Organization manufacturerOrganization = db.Organizations.FirstOrDefault(m => m.Id == project.ManufacturerOrganizationId);

            var prices = db.Prices.Where(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.LsPrice);
            if (prices.Any()){
                foreach (var priceItem in prices){
                    var p = new Price{
                        PriceProjectId = id,
                        Type = (int)PriceType.ReLsPrice,
                        CountryId = priceItem.CountryId,
                        ManufacturerPrice = priceItem.ManufacturerPrice,
                        ManufacturerPriceCurrencyDicId = priceItem.ManufacturerPriceCurrencyDicId,
                        ManufacturerPriceNote = priceItem.ManufacturerPriceNote,
                        LimitPrice = priceItem.LimitPrice,
                        LimitPriceCurrencyDicId = priceItem.LimitPriceCurrencyDicId,
                        LimitPriceNote = priceItem.LimitPriceNote,
                        AvgOptPrice = priceItem.AvgOptPrice,
                        AvgOptPriceCurrencyDicId = priceItem.AvgOptPriceCurrencyDicId,
                        AvgOptPriceNote = priceItem.AvgOptPriceNote,
                        AvgRozPrice = priceItem.AvgRozPrice,
                        AvgRozPriceCurrencyDicId = priceItem.AvgRozPriceCurrencyDicId,
                        AvgRozPriceNote = priceItem.AvgRozPriceNote,
                        CreatedDate = priceItem.CreatedDate,
                        Id = Guid.NewGuid(),
                    };
                    db.Prices.Add(p);
                }
                db.SaveChanges();
            }

            Price curentPrice = db.Prices.Any(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.LsCurrentPrice) 
                ? db.Prices.First(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.LsCurrentPrice) 
                : new Price() {Id=Guid.NewGuid()};
            Price price = db.Prices.Any(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.LsPrice) 
                ? db.Prices.First(m => m.PriceProjectId == parentId && m.Type == (int)PriceType.LsPrice) 
                : new Price() { Id = Guid.NewGuid() };

            if(holderOrganization == null)
                holderOrganization = new Organization ();
            holderOrganization.Id = Guid.NewGuid();

            if (proxyOrganization == null)
                proxyOrganization = new Organization();
            proxyOrganization.Id = Guid.NewGuid();

            if (manufacturerOrganization == null)
                manufacturerOrganization = new Organization();
            manufacturerOrganization.Id = Guid.NewGuid();

            curentPrice.Id = Guid.NewGuid();

            project.Id = id;
            project.PriceProjectId = parentId;
            curentPrice.PriceProjectId = id;
            curentPrice.Type = (int)PriceType.ReLsCurrentPrice;
            price.PriceProjectId = id;
            price.Id = Guid.NewGuid();
            price.Type = (int)PriceType.ReLsPrice;

            project.Type = (int)PriceProjectType.RePriceLs;
            project.ManufacturerOrganizationId = manufacturerOrganization.Id;
            project.HolderOrganizationId = holderOrganization.Id;
            project.ProxyOrganizationId = proxyOrganization.Id;

            return Json(new RequestModel
            {
                Project = project,
                HolderOrganization = holderOrganization,
                ProxyOrganization = proxyOrganization,
                ManufacturerOrganization = manufacturerOrganization,
                CurentPrice = curentPrice,
                Price = price,
                StartDateDay = project.DoverennostCreatedDate?.Day ?? DateTime.Now.Day,
                StartDateMonth = project.DoverennostCreatedDate?.Month ?? DateTime.Now.Month,
                StartDateYear = project.DoverennostCreatedDate?.Year ?? DateTime.Now.Year,
                EndDateDay = project.DoverennostExpiryDate?.Day ?? DateTime.Now.Day,
                EndDateMonth = project.DoverennostExpiryDate?.Month ?? DateTime.Now.Month,
                EndDateYear = project.DoverennostExpiryDate?.Year ?? DateTime.Now.Year
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoadRePriceLs(Guid id) {
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var project = db.PriceProjects.FirstOrDefault(o => o.Id == id);
            Organization holderOrganization;
            Organization proxyOrganization;
            Organization manufacturerOrganization;
            Price curentPrice;
            if (project == null) {
                Guid tengeDicId = DictionaryHelper.GetDicIdByCode("Currency", "398");

                project = new PriceProject() {
                    Id = id,
                    HolderOrganizationId = Guid.NewGuid(),
                    ProxyOrganizationId = Guid.NewGuid(),
                    ManufacturerOrganizationId = Guid.NewGuid(),
                    Type = (int)PriceProjectType.RePriceLs,
                    PayDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    ConclusionDate = DateTime.Now,
                    ContrDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    RegDate = DateTime.Now,
                    DoverennostCreatedDate = DateTime.Now,
                    DoverennostExpiryDate = DateTime.Now,
                    OwnerId = employeeId
                };
                holderOrganization = ProjectServices.Instance.GetOrganizationPrice(db, project.HolderOrganizationId, 9);
                proxyOrganization = ProjectServices.Instance.GetOrganizationPrice(db, project.ProxyOrganizationId, 10);
                manufacturerOrganization = new Organization() { Id = project.ManufacturerOrganizationId, Type = 11, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now };

                curentPrice = new Price() { Type = (int)PriceType.ReLsCurrentPrice, PriceProjectId = id, Id = Guid.NewGuid(),OwnerPriceCurrencyDicId = tengeDicId ,UnitPriceCurrencyDicId = tengeDicId };
            }
            else {
                holderOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.HolderOrganizationId);
                proxyOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.ProxyOrganizationId);
                manufacturerOrganization = db.Organizations.FirstOrDefault(o => o.Id == project.ManufacturerOrganizationId);
                curentPrice = db.Prices.FirstOrDefault(o => o.Type == 6 && o.PriceProjectId == id);
            }
            return Json(new RequestModel {
                Project = project,
                HolderOrganization = holderOrganization,
                ProxyOrganization = proxyOrganization,
                ManufacturerOrganization = manufacturerOrganization,
                CurentPrice = curentPrice,
                Price = new Price() { Type = (int)PriceType.ReLsPrice, PriceProjectId = id },
                StartDateDay = project.DoverennostCreatedDate?.Day ?? DateTime.Now.Day,
                StartDateMonth = project.DoverennostCreatedDate?.Month ?? DateTime.Now.Month,
                StartDateYear = project.DoverennostCreatedDate?.Year ?? DateTime.Now.Year,
                EndDateDay = project.DoverennostExpiryDate?.Day ?? DateTime.Now.Day,
                EndDateMonth = project.DoverennostExpiryDate?.Month ?? DateTime.Now.Month,
                EndDateYear = project.DoverennostExpiryDate?.Year ?? DateTime.Now.Year
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCountryRePriceLs(object data) {
            return Json("", JsonRequestBehavior.AllowGet);
        }
       
      
        #endregion Create

        #region Details

        public ActionResult PriceLsDetails(Guid id) {
            var price = new PriceDetailsModel();
            ViewBag.PriceProjectType = PriceProjectType.PriceLs;
            price.IsEdit = DocumentHelper.IsEditRegisterProjects(db, id);
            price.PriceProject = db.PriceProjectsViews.FirstOrDefault(m => m.Id == id);
            if (price.PriceProject != null) {
                price.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.HolderOrganizationId);
                price.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ManufacturerOrganizationId);
                price.Proxy = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ProxyOrganizationId);
                price.CurrentPrice = db.PricesViews.FirstOrDefault(m => m.PriceProjectId == price.PriceProject.Id && m.Type== (int)PriceType.LsCurrentPrice);
                price.IsEdit = price.IsEdit || (price.PriceProject.Status == (int) PriceProjectStatus.OnRevision);
                return View(price);
            }
            return null;
        }

		public ActionResult PriceImnDetails(Guid id)
        {
            var price = new PriceDetailsModel();
            price.IsEdit = DocumentHelper.IsEditRegisterProjects(db, id);
            price.PriceProject = db.PriceProjectsViews.FirstOrDefault(m => m.Id == id);
            if (price.PriceProject != null)
            {
                price.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.HolderOrganizationId);
                price.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ManufacturerOrganizationId);
                price.Proxy = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ProxyOrganizationId);
                price.CurrentPrice = db.PricesViews.FirstOrDefault(m => m.PriceProjectId == id && m.MtPartsId == null && m.Type == (int)PriceType.ImnCurrentPrice);
                return View(price);
            }
            return null;
        }

        public ActionResult RePriceLsDetails(Guid id)
        {
            var price = new PriceDetailsModel();
            ViewBag.PriceProjectType = PriceProjectType.RePriceLs;
            price.IsEdit = DocumentHelper.IsEditRegisterProjects(db, id);
            price.PriceProject = db.PriceProjectsViews.FirstOrDefault(m => m.Id == id);
            if (price.PriceProject != null)
            {
                price.Holder = db.OrganizationsViews.First(m => m.Id == price.PriceProject.HolderOrganizationId);
                price.Manufaturer = db.OrganizationsViews.First(m => m.Id == price.PriceProject.ManufacturerOrganizationId);
                price.Proxy = db.OrganizationsViews.First(m => m.Id == price.PriceProject.ProxyOrganizationId);
                price.CurrentPrice = db.PricesViews.FirstOrDefault(m => m.PriceProjectId == price.PriceProject.Id && m.Type == (int)PriceType.ReLsCurrentPrice) ??
                                     new PricesView();
                return View(price);
            }
            return null;
        }

        public ActionResult RePriceImnDetails(Guid id)
        {
            var price = new PriceDetailsModel();
            price.IsEdit = DocumentHelper.IsEditRegisterProjects(db, id);
            price.PriceProject = db.PriceProjectsViews.FirstOrDefault(m => m.Id == id);
            if (price.PriceProject != null)
            {
                price.Holder = db.OrganizationsViews.First(m => m.Id == price.PriceProject.HolderOrganizationId);
                price.Manufaturer = db.OrganizationsViews.First(m => m.Id == price.PriceProject.ManufacturerOrganizationId);
                price.Proxy = db.OrganizationsViews.First(m => m.Id == price.PriceProject.ProxyOrganizationId);
                price.CurrentPrice = db.PricesViews.FirstOrDefault(m => m.PriceProjectId == id && m.MtPartsId == null && m.Type == (int)PriceType.ReImnCurrentPrice);
                return View(price);
            }
            return null;
        }
        #endregion Details

        #endregion Ценообразование

        #region Экспертиза

        #region Create

        public ActionResult RegisterLs(Guid? id) {
			return View(id ?? Guid.NewGuid());
		}

		public ActionResult ReRegisterLs(Guid? id) {
            return View(id ?? Guid.NewGuid());
        }

		public ActionResult ChRegisterLs(Guid? id) {
            return View(id ?? Guid.NewGuid());
        }

		public ActionResult SendProjectRegister(RequestModel request) {
		    Document baseDocument = db.Documents.FirstOrDefault(m => m.Id == request.Drug.Id);
		    if (baseDocument == null) {
		        var user = UserHelper.GetCurrentEmployee();
		        Document document = new Document() {
		            Id = request.Drug.Id,
		            DocumentType = 0,
		            ProjectType = 1,
		            ExecutionDate = DateTime.Now.AddDays(20),
		            DocumentDate = DateTime.Now,
		            AttachPath = FileHelper.GetObjectPathRoot(),
		            CorrespondentsInfo = UserHelper.GetCurrentEmployee().DisplayName,
		            CorrespondentsId = user.Id.ToString(),
		            CorrespondentsValue = user.DisplayName,
                    IsTradeSecret = false
		        };
		        var project = db.RegisterProjects.First(o => o.Id == request.Drug.Id);

		        project.Status = 1;
		        db.Documents.Add(document);
		        db.SaveChanges();
		    }
		    else {
                baseDocument.IsTradeSecret = false;
                Document baseDocument2 = db.Documents.Where(m=>m.DocumentType==1).OrderByDescending(m=>m.CreatedDate).FirstOrDefault(m => m.AnswersId == request.Drug.Id.ToString());
		   

                if (baseDocument2 != null && baseDocument2.MainTaskId!=null) {
                  
                    Database.DataModel.Task task = db.Tasks.FirstOrDefault(m => m.Id == baseDocument2.MainTaskId);
		            if (task != null) {
                        Activity activity = new Activity
                        {
                            Id = Guid.NewGuid(),
                            ParentTask = task.Id,
                            DocumentId = task.DocumentId,
                            AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                            AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                            ExecutorsId = task.ExecutorId,
                            ExecutorsValue = task.ExecutorValue,
                            //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                            //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                            Type = 2,
                            IsParrent =false,
                            CreatedDate = DateTime.Now,
                            ParentId = task.ActivityId,
                            ExecutionDate = task.Document.ExecutionDate,
                            Text = "Замечания исправлены",
                            IsNotActive = false,
                            TypeEx = task.Stage,
                            //IsMainLine = task.IsMainLine
                        };
                        db.Activities.Add(activity);
                        db.SaveChanges();
                    }
		        }

            }
		 
            return Json("Ок");
        }
        public ActionResult RegisterLsSave(RequestModel model) {
            var project = db.RegisterProjects.Any(o => o.Id == model.Drug.Id);
            if (project) {
                db.Entry(model.Drug).State = EntityState.Modified;

            }
            else {
                db.RegisterProjects.Add(model.Drug);

            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReRegisterLsSave(RequestModel model) {
            var project = db.RegisterProjects.Any(o => o.Id == model.Drug.Id);
            if (project) {
                db.Entry(model.Drug).State = EntityState.Modified;

            }
            else {
                db.RegisterProjects.Add(model.Drug);

            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChRegisterLsSave(RequestModel model) {
            var project = db.RegisterProjects.Any(o => o.Id == model.Drug.Id);
            if (project) {
                db.Entry(model.Drug).State = EntityState.Modified;

            }
            else {
                db.RegisterProjects.Add(model.Drug);

            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }
        public ActionResult LoadRegisterLs(Guid id) {
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var project = db.RegisterProjects.FirstOrDefault(o => o.Id == id);
            if (project == null) {
                project = new RegisterProject() {
                    Id = id,
                    Type = 0,
                    AccelerationDate = DateTime.Now,
                    PayDate = DateTime.Now,
                    AppPeriod1BeginDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    AppPeriod1FinishDate = DateTime.Now,
                    ConclusionDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AppPeriod2BeginDate = DateTime.Now,
                    AppPeriod2FinishDate = DateTime.Now,
                    ContrDate = DateTime.Now,
                    GmpExpiryDate = DateTime.Now,
                    PatentDate = DateTime.Now,
                    PatentExpiryDate = DateTime.Now,
                    RegDocDate = DateTime.Now,
                    RegDocExpiryDate = DateTime.Now,
                    SecureDocumentDate = DateTime.Now,
                    SecureDocumentExpiryDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    OwnerId = employeeId
                };
            }
            return Json(new RequestModel {
                Drug = project,
                Export = new Organization() { Type = 12, ObjectId = id, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now },
                Organization = new Organization() { Type = 13, ObjectId = id, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now },
                Manufacture = new Organization() { Type = 14, ObjectId = id, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now },
                Composition = new Composition() { ObjectId = id },
                Changes = new Change() { RegisterProjectId = id },
                Package = new Package() { ObjectId = id},
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadReRegisterLs(Guid id) {
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var project = db.RegisterProjects.FirstOrDefault(o => o.Id == id);
            if (project == null) {
                project = new RegisterProject() {
                    Id = id,
                    Type = 1,
                    AccelerationDate = DateTime.Now,
                    PayDate = DateTime.Now,
                    AppPeriod1BeginDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    AppPeriod1FinishDate = DateTime.Now,
                    ConclusionDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AppPeriod2BeginDate = DateTime.Now,
                    AppPeriod2FinishDate = DateTime.Now,
                    ContrDate = DateTime.Now,
                    GmpExpiryDate = DateTime.Now,
                    PatentDate = DateTime.Now,
                    PatentExpiryDate = DateTime.Now,
                    RegDocDate = DateTime.Now,
                    RegDocExpiryDate = DateTime.Now,
                    SecureDocumentDate = DateTime.Now,
                    SecureDocumentExpiryDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    OwnerId = employeeId
                };
            }
            return Json(new RequestModel {
                Drug = project,
                Export = new Organization() { Type = 15, ObjectId = id, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now },
                Organization = new Organization() { Type = 16, ObjectId = id, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now },
                Manufacture = new Organization() { Type = 17, ObjectId = id, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now },
                Composition = new Composition() { ObjectId = id},
                Changes = new Change() { RegisterProjectId = id },
                Package = new Package() { ObjectId  = id},
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadChRegisterLs(Guid id) {
            var employeeId = UserHelper.GetCurrentEmployee().Id;
            var project = db.RegisterProjects.FirstOrDefault(o => o.Id == id);
            if (project == null) {
                project = new RegisterProject() {
                    Id = id,
                    Type = 2,
                    AccelerationDate = DateTime.Now,
                    PayDate = DateTime.Now,
                    AppPeriod1BeginDate = DateTime.Now,
                    StartDate = DateTime.Now,
                    AppPeriod1FinishDate = DateTime.Now,
                    ConclusionDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    AppPeriod2BeginDate = DateTime.Now,
                    AppPeriod2FinishDate = DateTime.Now,
                    ContrDate = DateTime.Now,
                    GmpExpiryDate = DateTime.Now,
                    PatentDate = DateTime.Now,
                    PatentExpiryDate = DateTime.Now,
                    RegDocDate = DateTime.Now,
                    RegDocExpiryDate = DateTime.Now,
                    SecureDocumentDate = DateTime.Now,
                    SecureDocumentExpiryDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    OwnerId = employeeId
                };
            }
            return Json(new RequestModel {
                Drug = project,
                Export = new Organization() { Type = 18, ObjectId = id ,DocDate = DateTime.Now,DocExpiryDate = DateTime.Now },
                Organization = new Organization() { Type = 19, ObjectId = id, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now },
                Manufacture = new Organization() { Type = 20, ObjectId = id, DocDate = DateTime.Now, DocExpiryDate = DateTime.Now },
                Composition = new Composition() { ObjectId = id },
                Changes = new Change() { RegisterProjectId = id },
                Package = new Package() { ObjectId = id },
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion Create

        #region View

        public ActionResult RegisterLsDetails(Guid id) {
            var price = new PriceDetailsModel();
            price.Drug = db.RegisterProjectsViews.FirstOrDefault(m => m.Id == id);
            price.IsEdit = DocumentHelper.IsEditRegisterProjects(db,id);
            if (price.Drug != null)
            {
                price.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.ObjectId == price.Drug.Id);
                if (price.Drug.ContractId != null) {
                    price.Contract = db.ContractsViews.FirstOrDefault(m => m.Id == price.Drug.ContractId);
                }

                    return View(price);
            }
            return null;
		}

		public ActionResult ReRegisterLsDetails(Guid id) {
            var price = new PriceDetailsModel();
            price.Drug = db.RegisterProjectsViews.FirstOrDefault(m => m.Id == id);
            price.IsEdit = DocumentHelper.IsEditRegisterProjects(db, id);


            if (price.Drug != null)
            {
                if (price.Drug.ContractId != null)
                {
                    price.Contract = db.ContractsViews.FirstOrDefault(m => m.Id == price.Drug.ContractId);
                }
                price.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.ObjectId == price.Drug.Id);
                return View(price);
            }
            return null;
		}

		public ActionResult ChRegisterLsDetails(Guid id) {
            var price = new PriceDetailsModel();
            price.Drug = db.RegisterProjectsViews.FirstOrDefault(m => m.Id == id);
            price.IsEdit = DocumentHelper.IsEditRegisterProjects(db,id);

            if (price.Drug != null)
            {
                if (price.Drug.ContractId != null)
                {
                    price.Contract = db.ContractsViews.FirstOrDefault(m => m.Id == price.Drug.ContractId);
                }
                price.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.ObjectId == price.Drug.Id);
                return View(price);
            }
            return null;
		}

        #endregion View

        #endregion Экспертиза


        public ActionResult SignOperation(string id)
        {

            var repository = new RegisterProjectRepository();
            var model = repository.GetPreamble(new Guid(id));

            bool IsSuccess = true;
            string preambleXml = string.Empty;
            try
            {
                preambleXml = SerializeHelper.SerializeDataContract(model);
                preambleXml = preambleXml.Replace("utf-16", "utf-8");
            }
            catch (Exception e)
            {
                IsSuccess = false;
            }

            return Json(new
            {
                IsSuccess,
                preambleXml
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SignForm(string preambleId, string xmlAuditForm)
        {
            var success = true;
            var repository = new RegisterProjectRepository();
            var model = repository.GetById(new Guid(preambleId));
            new SignDocumentRepository().SaveSignDocument(UserHelper.GetCurrentEmployee().Id, xmlAuditForm, model);
            // Set Status Sended
            return Json(new
            {
                success
            }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ShowComment(string modelId, string idControl){
            var repository = new PriceProjectRepository();
            var model = repository.GetComments(modelId, idControl);
            if (model == null){
                model = new PriceProjectCom();
            }
            //if(model.PriceProject == null)
            //    model.PriceProject = new PriceProject();

            model.PriceProjectFieldHistories = repository.GetFieldHistories(modelId, idControl);
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult SaveComment(string modelId, string idControl, bool isError, string comment, string fieldValue, string userId, string fieldDisplay){
            new PriceProjectRepository().SaveComment(modelId, idControl, isError, comment, fieldValue, userId, fieldDisplay);
            return Json(new { Success = true });
        }

        [HttpPost]
        public virtual ActionResult UpdateModel(string code, string modelId, string userId, long? recordId, string fieldName, string fieldValue, string fieldDisplay){
            new PriceProjectRepository().UpdateModel(code, modelId, userId, recordId, fieldName, fieldValue, fieldDisplay);
            return Json(new { Success = true, modelId = modelId});
        }

    }
}