using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aspose.Pdf;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net.Repository.Hierarchy;
using Ncels.Helpers;
using Newtonsoft.Json;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;
using PW.Ncels.Database.Enums.PriceProject;
using PW.Ncels.Database.Notifications;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Document = PW.Ncels.Database.DataModel.Document;
using ReturnAction = Aspose.Slides.Warnings.ReturnAction;

namespace PW.Prism.Controllers
{
	[Authorize]
	public class IncomingDocController : Controller
	{
		private ncelsEntities db = UserHelper.GetCn();
        private NotificationManager _notificationManager=new NotificationManager();

		// GET: /IncomingDoc/
		public ActionResult Index() {
			Guid guid = Guid.NewGuid();
			//ViewBag.ResponsibleValue =  db.Documents.
			//	Where(o => o.IsDeleted == false && o.DocumentType == 0).
			//	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
			//	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
			//var documents = db.Documents.Include(d => d.Template);
			return PartialView(guid);
		}
		public ActionResult IndexRegion() {
			Guid guid = Guid.NewGuid();
			Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
			ViewBag.Regions =
				db.Units.Where(o => o.Type == 0 && o.Id != organizationId)
					.ToList().OrderBy(o=>o.Rank)
					.Select(o => new Item() {Id = o.Id.ToString(), Name = o.Name}).ToList();
			//ViewBag.ResponsibleValue =  db.Documents.
			//	Where(o => o.IsDeleted == false && o.DocumentType == 0).
			//	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
			//	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
			//var documents = db.Documents.Include(d => d.Template);
			return PartialView(guid);
		}

		// GET: /IncomingDoc/Create
		[HttpGet]
		public ActionResult Card(Guid? id)
		{	
			Guid guid = id.HasValue ? id.Value: Guid.NewGuid();

			return PartialView(new[] { guid });
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

        [HttpGet]
		public ActionResult Card2(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(new[] { guid });
		}

		[HttpGet]
		public ActionResult RepeatCard(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.Empty;
			if (guid == Guid.Empty)
				return PartialView("Card", new[] {guid});
			return PartialView("Card", new[] { Guid.NewGuid(), guid });
		}

		public ActionResult Form(Guid[] id) {
			return PartialView(id);
		}

		public ActionResult FormRef(Guid[] id) {
			var price = new PriceDetailsModel();
			price.Id = id[0];
			var obj = db.ProjectsViews.FirstOrDefault(m => m.Id == price.Id);
			if (obj != null && obj.IsRegisterProject.HasValue) {
				if (obj.IsRegisterProject.Value) {
					switch (obj.Type) {
						// RegisterLsDetails
						case 0:
							price.Drug = db.RegisterProjectsViews.FirstOrDefault(m => m.Id == price.Id);
							if (price.Drug != null) {
								price.Manufaturer = db.OrganizationsViews.First(m => m.ObjectId == price.Drug.Id);
								return PartialView("FormRegisterLsDetails", price);
							}
							break;
						// ReRegisterLsDetails
						case 1:
							price.Drug = db.RegisterProjectsViews.FirstOrDefault(m => m.Id == price.Id);
							if (price.Drug != null) {
								price.Manufaturer = db.OrganizationsViews.First(m => m.ObjectId == price.Drug.Id);
								return PartialView("FormReRegisterLsDetails", price);
							}
							break;
						// ChRegisterLsDetails
						case 2:
							price.Drug = db.RegisterProjectsViews.FirstOrDefault(m => m.Id == price.Id);
							if (price.Drug != null) {
								price.Manufaturer = db.OrganizationsViews.First(m => m.ObjectId == price.Drug.Id);
								return PartialView("FormChRegisterLsDetails", price);
							}
							break;
					}
				}
				else {
					switch (obj.Type) {
						//PriceLsDetails
						case 0:
							price.PriceProject = db.PriceProjectsViews.FirstOrDefault(m => m.Id == price.Id);
							if (price.PriceProject != null) {
								price.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.HolderOrganizationId);
								price.Manufaturer = db.OrganizationsViews.First(m => m.Id == price.PriceProject.ManufacturerOrganizationId);
								price.Proxy = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ProxyOrganizationId);
								price.CurrentPrice = db.PricesViews.FirstOrDefault(m => m.PriceProjectId == price.PriceProject.Id && m.Type == (int)PriceType.LsCurrentPrice) ?? new PricesView();
								return PartialView("FormPriceLsDetails", price);
							}
							break;
						// PriceImnDetails
						case 1:
							price.PriceProject = db.PriceProjectsViews.FirstOrDefault(m => m.Id == price.Id);
							if (price.PriceProject != null) {
								price.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.HolderOrganizationId);
								price.Manufaturer = db.OrganizationsViews.First(m => m.Id == price.PriceProject.ManufacturerOrganizationId);
								price.Proxy = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ProxyOrganizationId);
                                price.CurrentPrice = db.PricesViews.FirstOrDefault(m => m.PriceProjectId == price.PriceProject.Id && m.Type == (int)PriceType.ImnCurrentPrice && m.MtPartsId == null);
								return PartialView("FormPriceImnDetails", price);
							}
							break;
                        // RePriceLsDetails
                        case 2:
                            price.PriceProject = db.PriceProjectsViews.FirstOrDefault(m => m.Id == price.Id);
                            if (price.PriceProject != null)
                            {
                                price.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.HolderOrganizationId);
                                price.Manufaturer = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ManufacturerOrganizationId);
                                price.Proxy = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ProxyOrganizationId);
                                price.CurrentPrice = db.PricesViews.FirstOrDefault(m => m.PriceProjectId == price.PriceProject.Id && m.Type == (int)PriceType.ReLsCurrentPrice);
                                return PartialView("FormRePriceLsDetails", price);
                            }
                            break;
                        // RePriceImnDetails
                        case 3:
							price.PriceProject = db.PriceProjectsViews.FirstOrDefault(m => m.Id == price.Id);
							if (price.PriceProject != null) {
								price.Holder = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.HolderOrganizationId);
								price.Manufaturer = db.OrganizationsViews.First(m => m.Id == price.PriceProject.ManufacturerOrganizationId);
								price.Proxy = db.OrganizationsViews.FirstOrDefault(m => m.Id == price.PriceProject.ProxyOrganizationId);
                                price.CurrentPrice = db.PricesViews.FirstOrDefault(m => m.PriceProjectId == price.PriceProject.Id && m.Type == (int)PriceType.ReImnCurrentPrice && m.MtPartsId == null);
                                return PartialView("FormRePriceImnDetails", price);
							}
							break;
					}
				}
			}
			return null;
		}

		// GET: /IncomingDoc/Edit/5
		public ActionResult Edit(Guid? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Document document = db.Documents.Find(id);
			if (document == null)
			{
				return HttpNotFound();
			}
			
			if (document.ProjectType == 1)
				return PartialView("Card2", new[] { id.Value });
			return PartialView("Card", new[] { id.Value });
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private IQueryable GetIncomingDocument(IQueryable<Document> docs)
		{
			return docs.OrderByDescending(o=>o.CreatedDate).Select(columns => new
			{
				DocumentId = columns.Id,
				columns.IsAttachments,
				columns.Number,
				columns.DocumentDate,
				columns.SortNumber,
				columns.AutoDocumentDate,
				columns.StateType,
				columns.MonitoringType,
				columns.ExecutionDate,
				columns.MonitoringNote,
				columns.CorrespondentsValue,
				columns.CorrespondentsInfo,
				columns.OutgoingNumber,
				columns.AutoOutgoingDate,
				columns.Summary,
				columns.ResponsibleValue,
				columns.ExecutorsValue,
				columns.AnswersValue,
				columns.AutoAnswersValue,
				columns.AutoAnswersTempValue,
				columns.LanguageDictionaryValue,
				columns.QuestionDesignDictionaryValue,
				columns.DocumentKindDictionaryValue,
				columns.DocumentKindDictionaryId,
				columns.Counters,
				columns.FormDeliveryDictionaryValue,
				columns.NomenclatureDictionaryValue,
				columns.CompleteDocumentsValue,
				columns.RegistratorValue,
				columns.Note,
				columns.CreatedDate,
				columns.PageCount,
				columns.CopiesCount,
				columns.OrganizationId
			});
		}

		public ActionResult ExtensionExecution(Guid id)
		{
			Document document = db.Documents.Find(id);

            PW.Ncels.Database.DataModel.ExtensionExecution execution = new ExtensionExecution();
			execution.Id = Guid.NewGuid();
			execution.Date = document.DocumentDate.HasValue ?  document.DocumentDate.Value: DateTime.Now;
			execution.Number = document.Number;
			execution.DocumentId = document.Id;
            execution.ExecutionDate = document.ExecutionDate.HasValue ? document.ExecutionDate.Value : DateTime.Now;
            return PartialView(execution);
		}
        public ActionResult ExtensionReview(Guid id) {
            Document document = db.Documents.Find(id);
            
            PW.Ncels.Database.DataModel.Report execution = new Report();
            execution.Id = Guid.NewGuid();
            var userid = UserHelper.GetCurrentEmployee().Id.ToString();
            var task = db.Tasks.OrderByDescending(m=>m.CreatedDate).First(o => o.DocumentId == id && o.ExecutorId == userid);
            execution.TaskId = task.Id;
            execution.DicId = new List<Item>();
            execution.DocumentId = document.Id;
            execution.Type = 4;
            execution.ExecutionDate = document.ExecutionDate.HasValue ? document.ExecutionDate.Value : DateTime.Now;
            return PartialView(execution);
        }
        public ActionResult ExtensionReviewConfirm(Report execution) {

            execution.TitleDicValue= DictionaryHelper.GetItemsName(execution.DicId);
            execution.TitleDicId = DictionaryHelper.GetItemsName(execution.DicId);
            db.Reports.Add(execution);

            db.SaveChanges();
            return Content(bool.TrueString);
        }
        public ActionResult ExtensionExecutionConfirm(ExtensionExecution execution)
		{
			Document document = db.Documents.Find(execution.DocumentId);
			document.ExecutionDate = execution.ExecutionDate;
			db.ExtensionExecutions.Add(execution);

			db.SaveChanges();
			return Content(bool.TrueString);
		}
		public ActionResult ListDocument([DataSourceRequest] DataSourceRequest request) {

            string umcId = "8F0B91F3-AF29-4D3C-96D6-019CBBDFC8BE";
            if (request.Filters.SelectMemberDescriptors().Any(descriptor => descriptor != null && descriptor.Member == "OrganizationId" && descriptor.Value.ToString().ToLower() == umcId.ToLower()))
            {
                var docFond = DocumentHelper.GetFondDocuments(db, 0);
                DataSourceResult resultFond = GetIncomingDocument(docFond.AsQueryable()).ToDataSourceResult(request);
                return Json(resultFond);
            }
            var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 0);
                Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
                docs = docs.Where(o => o.OrganizationId == organizationId);

                DataSourceResult result = GetIncomingDocument(docs).ToDataSourceResult(request);

                return Json(result);
         
		}
		public ActionResult ListRegionsDocument([DataSourceRequest] DataSourceRequest request) {
			var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 0);
			Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
			docs = docs.Where(o => o.OrganizationId != organizationId);

			DataSourceResult result = GetIncomingDocument(docs).ToDataSourceResult(request);

			return Json(result);

		}
		public FileStreamResult ExportFile()
		{

			StiReport report = new StiReport();
			report.Load(Server.MapPath("../Reports/InDocList.mrt"));
			foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>()) {
				data.ConnectionString = UserHelper.GetCnString();
			}

            if (report.Dictionary.Variables.Contains("OrganizationId"))
            {
                report.Dictionary.Variables["OrganizationId"].ValueObject = UserHelper.GetCurrentEmployee().OrganizationId;
            }
            report.Render(false);
			var stream = new MemoryStream();
			report.ExportDocument(StiExportFormat.Excel, stream);
			stream.Position = 0;
			return File(stream, "application/excel", "входящие.xls");
		}

		public FileStreamResult ExportEdList() {
			return ExportRequestsFile(1);
		}

		public FileStreamResult ExportAloList() {
			return ExportRequestsFile(2);
		}

		public FileStreamResult ExportKnfList() {
			return ExportRequestsFile(3);
		}

		public FileStreamResult ExportOthList() {
			return ExportRequestsFile(4);
		}

		private FileStreamResult ExportRequestsFile(int listType) {
			StiReport report = new StiReport();
			report.Load(Server.MapPath("../Reports/List/ListRequests.mrt"));
			foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>()) {
				data.ConnectionString = UserHelper.GetCnString();
			}

			if (report.Dictionary.Variables.Contains("ListType")) {
				report.Dictionary.Variables["ListType"].ValueObject = listType;
			}
			report.Render(false);
			var stream = new MemoryStream();
			report.ExportDocument(StiExportFormat.Excel, stream);
			stream.Position = 0;
			return File(stream, "application/excel", $"list_{listType}.xls");
		}

		public ActionResult DocumentRead(Guid id, Guid? repeatId)
		{
			Document document = db.Documents.Find(id);
			if (document == null)
			{
				document = new Document()
				{	Id = id,
					DocumentType = 0,
					DocumentDate = DateTime.Now,
					MonitoringType = 1,
					AttachPath = FileHelper.GetObjectPathRoot()
				};
			}

			if (repeatId.HasValue)
				document = GetRepeatDocument(document, repeatId.Value);

			DocumentModel model = new DocumentModel(document);

			return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings(){DateFormatString = "dd.MM.yyyy HH:mm"}));
		}

		[HttpPost]
		public ActionResult DocumentUpdate(DocumentModel document)
		{
			if (document != null)
			{
				Document baseDocument = db.Documents.Find(document.Id);
				if (baseDocument == null)
				{
					baseDocument = new Document()
					{
						Id = document.Id,
						DocumentType = 0,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now,
						DocumentDate = DateTime.Now,
					};
					db.Documents.Add(baseDocument);
				}
				baseDocument = document.GetDocument(baseDocument);

				db.SaveChanges();
				return Content(bool.TrueString);
			}
			 return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult DocumentRegister(DocumentModel document) {
			try
			{
				if (document != null)
				{
					Document baseDocument = db.Documents.Find(document.Id);
					if (baseDocument == null)
					{
						baseDocument = new Document()
						{
							Id = document.Id,
							DocumentType = 0,
							CreatedDate = DateTime.Now,
							ModifiedDate = DateTime.Now,
                            CompareConterDate =DateTime.Now
                        };

                        db.Documents.Add(baseDocument);
					}
					baseDocument = document.GetDocument(baseDocument);
					baseDocument.OrganizationId = UserHelper.GetCurrentEmployee().OrganizationId;
					baseDocument.DocumentDate = DateTime.Now;
					baseDocument.ModifiedDate = DateTime.Now;
					baseDocument.CompareConterDate = DateTime.Now;
					baseDocument.RegistratorId = UserHelper.GetCurrentEmployee().Id.ToString();
					baseDocument.RegistratorValue = UserHelper.GetCurrentEmployee().DisplayName;
					baseDocument.FirstExecutionDate = baseDocument.ExecutionDate;
					//db.SaveChanges();
					baseDocument.StateType = 1;
                    //	baseDocument.ExecutionDate = baseDocument.ExecutionDate ?? baseDocument.DocumentDate.Value.AddDays(15);
				    var pp = db.PriceProjects.FirstOrDefault(x => x.Id == baseDocument.Id);
				    if (pp != null) {
				        pp.Status = (int) PriceProjectStatus.Registered;
				    }

                    //Проверка для того чтобы не менялся номер у изменений цены
				    if (pp == null 
                        || (pp.Type != (int) PriceProjectType.RePriceImn && pp.Type != (int) PriceProjectType.RePriceLs)
                        || pp.PriceProjectId == null)
                    {
				        if (baseDocument.RepeaterId.HasValue) {
				            Registrator.SetRepeaterNumber(baseDocument);
				        }
				        else {
				            Registrator.SetNumber(baseDocument);
				        }
				    }
				    else {

				    }


				    db.SaveChanges();

                    ncelsEntities dbVew = UserHelper.GetCn();
                    baseDocument = dbVew.Documents.Find(document.Id);
                    DocumentModel model = new DocumentModel(baseDocument);

					return
						Content(JsonConvert.SerializeObject(new {State = true, document = model}, Formatting.Indented,
							new JsonSerializerSettings() {DateFormatString = "dd.MM.yyyy HH:mm"}));
					//return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);
				}
				return Content(JsonConvert.SerializeObject(new { State = false }, Formatting.Indented,
							new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
			}
			catch (Exception ex)
			{
				return Content(JsonConvert.SerializeObject(new { State = false }, Formatting.Indented,
							new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
			}
		}

        public ActionResult RegisterOutgoingNumber(Guid? documentId){
            Document document = db.Documents.FirstOrDefault(x=>x.Id == documentId);
            if (document == null) {
                LogHelper.Log.ErrorFormat("Не удалось найти документ по идентификатору {0}", documentId);
                throw new Exception("Не удалось найти документ");
            }
            ViewBag.Data = new {
                document.OutgoingNumber
            };

            return PartialView(document.Id);
        }

        [HttpPost]
        public ActionResult RegisterOutgoingNumberSave(string docId, string number){
            Document document = db.Documents.Find(new Guid(docId));
            document.OutgoingNumber = number;
            document.OutgoingDate = DateTime.Now;

            db.SaveChanges();


            var pp = db.PriceProjects.FirstOrDefault(x => x.Id == document.Id);
            if (pp != null){
                new NotificationManager().SendNotification(
                    string.Format("Ваша заявка №{0} зарегистрирована", document.OutgoingNumber),
                    ObjectType.Letter, pp.Id, pp.OwnerId);
            }



            return Content(bool.TrueString);
        }

        public ActionResult DocumentReviewRef(Guid id) {
			Document baseDocument = db.Documents.Find(id);
			DocumentModel model = new DocumentModel(baseDocument);
            var pp = db.PriceProjects.FirstOrDefault(x => x.Id == baseDocument.Id);
            if (pp != null && pp.PriceProjectId != null) {
                var parentTask = db.Tasks.FirstOrDefault(x => x.DocumentId == pp.PriceProjectId);
                if (parentTask != null) {
                    ViewBag.ExecutorId = parentTask.ExecutorId;
                    ViewBag.ExecutorName = parentTask.ExecutorValue;
                }
                
            }
            
			return PartialView(model);
		}
		public ActionResult DocumentReview(Guid id) {
			Document baseDocument = db.Documents.Find(id);
			DocumentModel model = new DocumentModel(baseDocument);
            var setting = db.Settings.First(o => o.UniqueName == "Stage1");
		    var employe = db.Employees.First(o => o.Id == setting.UserId);
            model.CustomExecutorsId = DictionaryHelper.GetItems(employe.Id.ToString(), employe.DisplayName);

            return PartialView(model);
		}
		public ActionResult DocumentReview2(Guid id) {
			Document baseDocument = db.Documents.Find(id);
			DocumentModel model = new DocumentModel(baseDocument);
            var setting = db.Settings.First(o => o.UniqueName == "Stage8");
            var employe = db.Employees.First(o => o.Id == setting.UserId);
            model.CustomExecutorsId = DictionaryHelper.GetItems(employe.Id.ToString(), employe.DisplayName);

            return PartialView(model);
		}

	    public ActionResult ChangeStatus(Guid id) {
			    Document baseDocument = db.Documents.Find(id);
			    DocumentModel model = new DocumentModel(baseDocument);
                var setting = db.Settings.First(o => o.UniqueName == "Stage8");
                var employe = db.Employees.First(o => o.Id == setting.UserId);
                model.CustomExecutorsId = DictionaryHelper.GetItems(employe.Id.ToString(), employe.DisplayName);

                return PartialView(model);
		    }

	    public ActionResult ChangeStatusPrice(Guid id) {
            Document baseDocument = db.Documents.Find(id);
            DocumentModel model = new DocumentModel(baseDocument);

            return PartialView(model);
		    }
	    public ActionResult SendArchive(Guid id) {
			    Document baseDocument = db.Documents.Find(id);
			    DocumentModel model = new DocumentModel(baseDocument);
            return PartialView(model);
		    }

		public ActionResult DocumentReviewConfirm(DocumentModel document) {
			if (document != null) {
                LogHelper.Log.Debug("DocumentReviewConfirm");
				Document baseDocument = db.Documents.Find(document.Id);
			    _notificationManager.TrySendNotification(EventType.PriceAppRegistered, ObjectType.PriceProject, baseDocument);

			    var pp = db.PriceProjects.FirstOrDefault(x => x.Id == baseDocument.Id);
			    if (pp != null) {
			        pp.Status = (int) PriceProjectStatus.OnAnalize;
                    db.Entry(pp).State = EntityState.Modified;
			        db.SaveChanges();
			    }

			    baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 2;
			    var setting = db.Settings.First(o => o.UniqueName == "Stage1");

				if (!db.Activities.Any(o => o.DocumentId == document.Id && o.Type == 0))
				{
					Activity activity = new Activity()
					{
						Id = Guid.NewGuid(),
						IsParrent = true,
						ExecutorsId = DictionaryHelper.GetItemsId(document.CustomExecutorsId),
						ExecutorsValue = DictionaryHelper.GetItemsName(document.CustomExecutorsId),
						CreatedDate = DateTime.Now,
						ExecutionDate = DateTime.Now.AddDays(Convert.ToInt32(setting.Value)),
						DocumentId = baseDocument.Id,
						DocumentValue = baseDocument.DisplayName,
						Text = "Замечания на исправление",
						ResponsibleId = DictionaryHelper.GetItemsId(document.CustomExecutorsId),
						ResponsibleValue = DictionaryHelper.GetItemsName(document.CustomExecutorsId),
						Type = 0,
                        TypeEx = 1,
                        IsMainLine = true,
                       
						AuthorId = baseDocument.RegistratorId,
						AuthorValue = baseDocument.RegistratorValue,
					};
					db.Activities.Add(activity);
                    LogHelper.Log.Debug("add activity");


                    //костыль на время выяснения обстоятельств, почему при отправке заявки не создается таска для этого пользователя
                    //todo: Разобраться в логике создания тасков
                    //Task t = new Task()
                    //{
                    //    Id = Guid.NewGuid(),
                    //    DocumentId = baseDocument.Id,
                    //    Document = baseDocument,
                    //    Activity = activity,
                    //    ExecutorId = document.CustomExecutorsId.First().Id,
                    //    IsActive = true,
                    //    ExecutorValue = document.CustomExecutorsId.First().Name,
                    //    Text = activity.Text,
                    //    ExecutionDate = activity.ExecutionDate
                    //};
                    //LogHelper.Log.DebugFormat("create task {0}", t.Id);

                    //db.Tasks.Add(t);

                    //foreach (var item in document.CustomExecutorsId) {
                    //    var atId = Guid.NewGuid();
                    //    db.AccessTasks.Add(new AccessTask()
                    //    {
                    //        Id = atId,
                    //        Task = t,
                    //        UserId = Guid.Parse(item.Id)
                    //    });
                    //    LogHelper.Log.DebugFormat("create AccessTasks {0}", atId);
                    //}

                    //концец костыля

                    db.SaveChanges();
				}
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
			}
			return Content(bool.FalseString);
		}

		public ActionResult DocumentReviewConfirm2(DocumentModel document) {
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);
				
				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 2;
			    var setting = db.Settings.First(o => o.UniqueName == "Stage8");
			    var employee = db.Employees.FirstOrDefault(m=>m.Id == setting.UserId);
				
					Activity activity = new Activity()
					{
						Id = Guid.NewGuid(),
						IsParrent = true,
						ExecutorsId = employee.Id.ToString(),
						ExecutorsValue = employee.DisplayName,
						CreatedDate = DateTime.Now,
						ExecutionDate = DateTime.Now.AddDays(Convert.ToInt32(setting.Value)),
						DocumentId = baseDocument.Id,
						DocumentValue = baseDocument.DisplayName,
						Text = "Бухгалтерия на оплату",
						ResponsibleId = DictionaryHelper.GetItemsId(document.CustomExecutorsId),
						ResponsibleValue = DictionaryHelper.GetItemsName(document.CustomExecutorsId),
						Type = 2,
                        TypeEx = 1,
                        IsMainLine = true,
                       
						AuthorId = baseDocument.RegistratorId,
						AuthorValue = baseDocument.RegistratorValue,
					};
					db.Activities.Add(activity);
				DocumentModel model = new DocumentModel(baseDocument);
                db.SaveChanges();

                return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
			}
			return Content(bool.FalseString);
		}

         [HttpPost]
		public ActionResult ChangeStatusConfirm(Guid ? id, int  ? status) {
		
				if (id!= null && status != null) {
				    RegisterProject rp = db.RegisterProjects.FirstOrDefault(m => m.Id == id);
				    if (rp != null) {
				     rp.Status = status.Value;
					db.SaveChanges();
                }
            }
			return Json(status,JsonRequestBehavior.AllowGet);
		}


         [HttpPost]
		public ActionResult ChangeStatusPriceConfirm(Guid ? id, int  ? status) {
		
				if (id!= null && status != null) {
                Ncels.Database.DataModel.PriceProject rp = db.PriceProjects.FirstOrDefault(m => m.Id == id);
				    if (rp != null) {
				     rp.Status = status.Value;
					db.SaveChanges();
                }
            }
			return Json(status,JsonRequestBehavior.AllowGet);
		}

         [HttpPost]
		public ActionResult SendArchiveConfirm(Guid ? id) {
		
				if (id!= null ) {
				    Document d = db.Documents.FirstOrDefault(m => m.Id == id);
				    if (d != null) {
				     d.IsArchive = true;
					db.SaveChanges();
                }
            }
			return Json("Ok!",JsonRequestBehavior.AllowGet);
		}

		public ActionResult Execution(Guid id) {

			Document baseDocument = db.Documents.Find(id);

			return PartialView(baseDocument);
		}
		[HttpPost]
		public ActionResult DocumentExcludeConfirm(Guid id, string text, string nId, string nName) {

			Document baseDocument = db.Documents.Find(id);


			baseDocument.StateType = 9;
			baseDocument.FactExecutionDate = DateTime.Now;
			baseDocument.Note = text;
			Guid nomenclatureId;
			if (Guid.TryParse(nId, out nomenclatureId)) {
				baseDocument.NomenclatureDictionaryId = nomenclatureId.ToString();
				baseDocument.NomenclatureDictionaryValue = nName;
			}
			db.SaveChanges();
			return Content(bool.TrueString);
		}

		private Document GetRepeatDocument(Document document, Guid repeatId) {

			Document parentDocument = db.Documents.FirstOrDefault(x => x.Id == repeatId);

			if (parentDocument != null) {
				if (parentDocument.RepeaterId != null)
					parentDocument = db.Documents.FirstOrDefault(x => x.Id == parentDocument.RepeaterId);
				if (parentDocument != null) {
					document.RepeaterId = parentDocument.Id;
					document.MonitoringType = parentDocument.MonitoringType;
					document.CorrespondentsValue = parentDocument.CorrespondentsValue;
					document.CorrespondentsInfo = parentDocument.CorrespondentsInfo;
					document.ApplicantPhone = parentDocument.ApplicantPhone;
					document.ApplicantEmail = parentDocument.ApplicantEmail;
					document.ExecutorsId = parentDocument.ExecutorsId;
					document.ExecutorsValue = parentDocument.ExecutorsValue;
					document.DocumentKindDictionaryId = parentDocument.DocumentKindDictionaryId;
					document.DocumentKindDictionaryValue = parentDocument.DocumentKindDictionaryValue;
					document.QuestionDesignDictionaryId = parentDocument.QuestionDesignDictionaryId;
					document.QuestionDesignDictionaryValue = parentDocument.QuestionDesignDictionaryValue;
					document.Summary = parentDocument.Summary;
					document.Counters = parentDocument.Counters;
					document.LanguageDictionaryId = parentDocument.LanguageDictionaryId;
					document.LanguageDictionaryValue = parentDocument.LanguageDictionaryValue;
					document.FormDeliveryDictionaryId = parentDocument.FormDeliveryDictionaryId;
					document.FormDeliveryDictionaryValue = parentDocument.FormDeliveryDictionaryValue;
					document.NomenclatureDictionaryId = parentDocument.NomenclatureDictionaryId;
					document.NomenclatureDictionaryValue = parentDocument.NomenclatureDictionaryValue;
					document.Deed = parentDocument.Deed;
					document.Book = parentDocument.Book;
					document.Akt = parentDocument.Akt;
				}
			}
			return document;
		}
	}
}

