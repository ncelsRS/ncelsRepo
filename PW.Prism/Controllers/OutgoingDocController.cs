using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Notifications;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers
{
	[Authorize]
    public class OutgoingDocController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        private NotificationManager _notificationManager=new NotificationManager();

        // GET: /OutgoingDoc/
		public ActionResult Index()
		{
			//ViewBag.TypeDoc = type;
			Guid guid = Guid.NewGuid();
			//ViewBag.ResponsibleValue =  db.Documents.
			//	Where(o => o.IsDeleted == false && o.DocumentType == 0).
			//	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
			//	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
			//var documents = db.Documents.Include(d => d.Template);
			return PartialView(guid);
		}  // GET: /OutgoingDoc/
		public ActionResult Index2()
		{
			//ViewBag.TypeDoc = type;
			Guid guid = Guid.NewGuid();
			//ViewBag.ResponsibleValue =  db.Documents.
			//	Where(o => o.IsDeleted == false && o.DocumentType == 0).
			//	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
			//	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
			//var documents = db.Documents.Include(d => d.Template);
			return PartialView(guid);
		}

		// GET: /OutgoingDoc/
		public ActionResult IndexInit() {
			//ViewBag.TypeDoc = type;
			Guid guid = Guid.NewGuid();
			//ViewBag.ResponsibleValue =  db.Documents.
			//	Where(o => o.IsDeleted == false && o.DocumentType == 0).
			//	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
			//	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
			//var documents = db.Documents.Include(d => d.Template);
			return PartialView(guid);
		}

		// GET: /IncomingDoc/Create
		public ActionResult Card(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}	
        
		public ActionResult Card2(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		// GET: /IncomingDoc/Create
		public ActionResult CardInit(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult Form(Guid? id) {


			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}
		public ActionResult Form2(Guid? id) {


			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormInit(Guid? id) {


			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		// GET: /IncomingDoc/Edit/5
		public ActionResult Edit(Guid? id) {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			Document document = db.Documents.Find(id);

			if (document == null)
				return HttpNotFound();

			//if (document.OutgoingType == 0)
			//	return PartialView("CardInit", id.Value);
			return PartialView("Card", id.Value);
		}

        public ActionResult DetailCoz(Guid? id) {
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			Document document = db.Documents.Find(id);

			if (document == null)
				return HttpNotFound();

			//if (document.OutgoingType == 0)
			//	return PartialView("CardInit", id.Value);
			return PartialView("Card2", id.Value);
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private IQueryable GetOutgoingDocument(IQueryable<Document> docs) {
			return docs.OrderByDescending(o => o.CreatedDate).Select(columns => new {
				DocumentId = columns.Id,
				columns.IsAttachments,
				columns.Number,
				columns.DocumentDate,
				columns.SortNumber,
				columns.AutoDocumentDate,
				columns.StateType,
				columns.OutgoingType,
				columns.CorrespondentsValue,
				columns.CorrespondentsInfo,
				columns.Summary,
				columns.SignerValue,
				columns.ExecutorsValue,
				columns.DocumentKindDictionaryValue,
				columns.QuestionDesignDictionaryValue,
				columns.Counters,
				columns.LanguageDictionaryValue,
				columns.FormSendingDictionaryValue,
				columns.BlankNumber,
				columns.NomenclatureDictionaryValue,
				columns.AnswersValue,
				columns.AutoAnswersValue,
				columns.SourceValue,
				columns.RegistratorValue,
				columns.CreatedUserValue,
				columns.Note,
				columns.CreatedDate,
				columns.PageCount,
				columns.CopiesCount,
				columns.OrganizationId,
				columns.MonitoringType
			});
		}

		public ActionResult ListDocument([DataSourceRequest] DataSourceRequest request) {

            //Инициативные
            //if (type == "Initiative")
            //{
            //	docs = docs.Where(o => o.OutgoingType == 0);
            //}
            //Ответный
            //if (type == "Responding") {
            //	docs = docs.Where(o => o.OutgoingType == 1 || o.OutgoingType == 2);
            //}
            //      if (type == "Responding" || type == "Initiative")
            //{
            //    docs = docs.Where(o => o.OutgoingType == 0 || o.OutgoingType == 1 || o.OutgoingType == 2);
            //}
            var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 1);
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
			docs = docs.Where(o => o.OrganizationId == organizationId).OrderByDescending(x => x.SortNumber);
			DataSourceResult result = GetOutgoingDocument(docs).ToDataSourceResult(request);

			return Json(result);
		}

		public FileStreamResult ExportFile() {

			StiReport report = new StiReport();

			report.Load(Server.MapPath("../Reports/OutDocList.mrt"));
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

			return File(stream, "application/excel", "исходящие.xls");
		}

		public ActionResult DocumentRead(Guid id) {
			Document document = db.Documents.Find(id);
			if (document == null) {
				document = new Document() {
					Id = id,
					DocumentType = 1,
					DocumentDate = DateTime.Now,
					OutgoingType = 0,
					AttachPath = FileHelper.GetObjectPathRoot()

				};
			}

			DocumentModel model = new DocumentModel(document);
			return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

			return Json(model, JsonRequestBehavior.AllowGet);
		}

		public ActionResult DocumentReadInit(Guid id) {
			Document document = db.Documents.Find(id);
			if (document == null) {
				document = new Document() {
					Id = id,
					DocumentType = 1,
					DocumentDate = DateTime.Now,
					OutgoingType = 0,
					AttachPath = FileHelper.GetObjectPathRoot()

				};
			}

			DocumentModel model = new DocumentModel(document);
			return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

			return Json(model, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult DocumentUpdate(DocumentModel document) {
			if (document != null) {

				Document baseDocument = db.Documents.Find(document.Id);
				if (baseDocument == null) {
					baseDocument = new Document() {
						Id = document.Id,
						DocumentType = 1,
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

			//IList<Document> teachers = new JavaScriptSerializer().Deserialize<IList<Document>>(models);
			return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult DocumentRegister(DocumentModel document) {
			try
			{

			
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);
				if (baseDocument == null) {
					baseDocument = new Document() {
						Id = document.Id,
						DocumentType = 1,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now
					};
					db.Documents.Add(baseDocument);
				}
				baseDocument = document.GetDocument(baseDocument);
				baseDocument.DocumentDate = DateTime.Now;
				baseDocument.StateType = 1;
			    SetEditDoc(baseDocument);



                db.SaveChanges();
				ncelsEntities dbVew = UserHelper.GetCn();
				baseDocument = dbVew.Documents.Find(document.Id);
				DocumentModel model = new DocumentModel(baseDocument);
				UploadHelper.ReplaceDocument(baseDocument.Id.ToString(), "Проект.docx", "DocumentNumber", baseDocument.Number);
				UploadHelper.ReplaceDocument(baseDocument.Id.ToString(), "Проект.docx", "DocumentDate", baseDocument.DocumentDate.Value.ToString("dd.MM.yyyy HH:mm"));
			
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

			
			}
			return Content(JsonConvert.SerializeObject(new { State = false }, Formatting.Indented,
				new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
			} catch (Exception) {

				return Content(JsonConvert.SerializeObject(new { State = false }, Formatting.Indented,
					new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
			}
			
		}

	    public void SetEditDoc(Document doc) {
	        if (doc.AnswersId != null) {
	            foreach (var item in DictionaryHelper.GetItems(doc.AnswersId, doc.AnswersValue)) {
	            var docId = Guid.Parse(item.Id);
                    var doc2 = db.Documents.FirstOrDefault(m => m.Id == docId);
                    if (doc2 != null)
                    {
                        doc2.IsTradeSecret = true;
                    }
	            }
            }
        }

		[HttpPost]
		public ActionResult DocumentReview(DocumentModel document) {
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);

				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 3;

				db.SaveChanges();
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

				return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);
			}
			return Content(bool.FalseString);
		}



		public ActionResult DocumentSend(Guid id) {

				Document baseDocument = db.Documents.Find(id);
				MailSenderModel action = new MailSenderModel() {
					Id = baseDocument.Id,
					Title = "Документ № " + baseDocument.DisplayName,
					DocumenetNumber = baseDocument.Number,
					Email = baseDocument.ApplicantEmail,
					DocumenetDate = baseDocument.DocumentDate.HasValue ? baseDocument.DocumentDate.Value.ToString("dd.MM.yyyy HH:mm") : string.Empty
				};

				//MailSender mailSender = new MailSender();
				//mailSender.SendMail("ovseikin@mail.ru", "тутвсе просто", "все очень просто");

				return PartialView(action);
		}

		[HttpPost]
		public ActionResult DocumentSendConfirm(MailSenderModel mailSenderModel) {
			if (mailSenderModel != null) {


				try
				{
					Document baseDocument = db.Documents.Find(mailSenderModel.Id);
                    _notificationManager.TrySendNotification(EventType.CommentForPriceApp, ObjectType.PriceProject, baseDocument);

                    DocumentManager.CreateIncomingDocs(baseDocument);				    


					List<string> listEmail = db.Units.Where(o => o.Type == 0).Select(o => o.Email).ToList();
					MailSender mailSender = new MailSender();
					mailSender.SendMail(mailSenderModel.Email, mailSenderModel.Title, mailSenderModel.Text,
						mailSenderModel.Id.ToString(), mailSenderModel.Files, baseDocument.Id, listEmail);
			

					return Content(bool.TrueString);

				}
				catch (Exception ex)
				{
					if (ex.InnerException != null)
					{

						return Content(ex.Message + ex.InnerException.Message);
					}
					else
					{
						return Content(ex.Message );
					}
				}
			}
			return Content(bool.FalseString);
		}
    }
}
