using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers
{
	[Authorize]
    public class AdministrativeDocController : Controller
    {
		private ncelsEntities db = UserHelper.GetCn();

		// GET: /OutgoingDoc/
		public ActionResult Index() {
			Guid guid = Guid.NewGuid();
			//ViewBag.ResponsibleValue =  db.Documents.
			//	Where(o => o.IsDeleted == false && o.DocumentType == 0).
			//	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
			//	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
			//var documents = db.Documents.Include(d => d.Template);
			ViewBag.AdministrativeMain = db.Dictionaries.Where(o => o.Type == "AdministrativeTypeDictionary").ToList().Select(o => new Item() { Id = o.Id.ToString(), Name = LocalizationHelper.GetString(o.Name, o.NameKz) }).ToList();

			return PartialView(guid);
		}


		// GET: /IncomingDoc/Create
		[HttpGet]
		public ActionResult Card(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(new[] { guid });
		}

		[HttpGet]
		public ActionResult RepeatCard(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.Empty;
			if (guid == Guid.Empty)
				return PartialView("Card", new[] { guid });
			return PartialView("Card", new[] { Guid.NewGuid(), guid });
		}

		public ActionResult Form(Guid[] id) {
			return PartialView(id);
		}

		// GET: /IncomingDoc/Edit/5
		public ActionResult Edit(Guid? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Document document = db.Documents.Find(id);
			if (document == null) {
				return HttpNotFound();
			}
			return PartialView("Card", new[] { id.Value });
		}
		public ActionResult ExtensionExecution(Guid id) {
			Document document = db.Documents.Find(id);

            PW.Ncels.Database.DataModel.ExtensionExecution execution = new ExtensionExecution();
			execution.Id = Guid.NewGuid();
			execution.Date = document.DocumentDate.HasValue ? document.DocumentDate.Value : DateTime.Now;
			execution.Number = document.Number;
			execution.DocumentId = document.Id;

			return PartialView(execution);
		}

		public ActionResult ExtensionExecutionConfirm(ExtensionExecution execution) {
			Document document = db.Documents.Find(execution.DocumentId);
			document.ExecutionDate = execution.ExecutionDate;
			db.ExtensionExecutions.Add(execution);

			db.SaveChanges();
			return Content(bool.TrueString);
		}
		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private IQueryable GetAdministrativeDocument(IQueryable<Document> docs) {
			return docs.OrderByDescending(o => o.CreatedDate).Select(columns => new {

				DocumentId = columns.Id,
				columns.IsAttachments,
				columns.Number,
				columns.SortNumber,
				columns.AutoDocumentDate,
				columns.DocumentDate,
				columns.StateType,
				columns.MonitoringType,
				columns.ExecutionDate,
				columns.MonitoringNote,
				columns.SignerValue,
				columns.SigningFormDictionaryValue,
				columns.Summary,
				columns.ReadersValue,
				columns.AgreementsValue,
				columns.ExecutorsValue,
				columns.ResponsibleValue,
				columns.AdministrativeTypeDictionaryId,
				columns.AdministrativeTypeDictionaryValue,
				columns.NomenclatureDictionaryValue,
				columns.CompleteDocumentsValue,
				columns.EditDocumentsValue,
				columns.RepealDocumentsValue,
				columns.AutoCompleteDocumentsValue,
				columns.AutoEditDocumentsValue,
				columns.AutoRepealDocumentsValue,
				columns.RegistratorValue,
				columns.Note,
				columns.PageCount,
				columns.CopiesCount,
				columns.CreatedDate,
				columns.CreatedUserValue,
			});
		}

		public ActionResult ListDocument([DataSourceRequest] DataSourceRequest request) {
			var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 3 && o.ProjectType == 3);
			Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
			docs = docs.Where(o => o.OrganizationId == organizationId);
			DataSourceResult result = GetAdministrativeDocument(docs).ToDataSourceResult(request);

			return Json(result);

		}

		public ActionResult ListDocumentForEmployee([DataSourceRequest] DataSourceRequest request, Guid? employeId) {
			string employeeId = employeId.HasValue ? employeId.ToString() : Guid.Empty.ToString();
			var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 3 && o.ProjectType == 3)
				.Where(x => x.ReadersId.ToLower().Contains(employeeId.ToLower()));
			Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
			docs = docs.Where(o => o.OrganizationId == organizationId);
			DataSourceResult result = GetAdministrativeDocument(docs).ToDataSourceResult(request);
			return Json(result);
		}

		public FileStreamResult ExportFile() {

			StiReport report = new StiReport();

			report.Load(Server.MapPath("../Reports/AdminDocList.mrt"));
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

			return File(stream, "application/excel", "ОРД.xls");


		}

		public ActionResult DocumentRead(Guid id, Guid? repeatId) {
			Document document = db.Documents.Find(id);
			if (document == null) {

				document = new Document() {
					Id = id,
					DocumentType = 3,
					DocumentDate = DateTime.Now,
					MonitoringType = 1,
					ProjectType = 3,
					AttachPath = FileHelper.GetObjectPathRoot()
				};
			}

			if (repeatId.HasValue)
				document = GetRepeatDocument(document, repeatId.Value);


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
						DocumentType = 3,
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
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);
				if (baseDocument == null) {
					baseDocument = new Document() {
						Id = document.Id,
						DocumentType = 3,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now
					};
					db.Documents.Add(baseDocument);
				}
				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 1;
				baseDocument.DocumentDate =  DateTime.Now;
				//baseDocument.ExecutionDate = baseDocument.ExecutionDate ?? baseDocument.DocumentDate.Value.AddDays(15);
				baseDocument.RegistratorId = UserHelper.GetCurrentEmployee().Id.ToString();
				baseDocument.RegistratorValue = UserHelper.GetCurrentEmployee().DisplayName;
				baseDocument.FirstExecutionDate = baseDocument.ExecutionDate;
				baseDocument.ModifiedDate = DateTime.Now;
				Registrator.SetNumber(baseDocument);

				db.SaveChanges();
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

				return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);
			}
			return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult DocumentReview(DocumentModel document) {
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);

				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 2;
				Activity activity = new Activity() {
					Id = Guid.NewGuid(),
					IsParrent = true,
					ExecutorsId = baseDocument.ExecutorsId,
					ExecutorsValue = baseDocument.ExecutorsValue,
					CreatedDate = DateTime.Now,
					ExecutionDate = baseDocument.ExecutionDate,
					DocumentId = baseDocument.Id,
					DocumentValue = baseDocument.DisplayName,
					Text = "На рассмотрение",
					ResponsibleId = DictionaryHelper.GetItems(baseDocument.ExecutorsId, baseDocument.ExecutorsValue)[0].Id,
					ResponsibleValue = DictionaryHelper.GetItems(baseDocument.ExecutorsId, baseDocument.ExecutorsValue)[0].Name,
					Type = 2,
					IsMainLine = true,
					AuthorId = baseDocument.RegistratorId,
					AuthorValue = baseDocument.RegistratorValue,
				};
				db.Activities.Add(activity);
				db.SaveChanges();
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

				return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);
			}
			return Content(bool.FalseString);
		}


		public ActionResult Execution(Guid id) {

			Document baseDocument = db.Documents.Find(id);

			return PartialView(baseDocument);
		}
		[HttpPost]
		public ActionResult DocumentExcludeConfirm(Guid id, string text)
		{

			Document baseDocument = db.Documents.Find(id);


			baseDocument.StateType = 9;
			baseDocument.FactExecutionDate = DateTime.Now;
			baseDocument.Note = text;
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
					document.MonitoringNote = parentDocument.MonitoringNote;
					document.ReadersId = parentDocument.ReadersId;
					document.ReadersValue = parentDocument.ReadersValue;
					document.RegistratorId = parentDocument.RegistratorId;
					document.RegistratorValue = parentDocument.RegistratorValue;
					document.Summary = parentDocument.Summary;
					document.SignerId = parentDocument.SignerId;
					document.SignerValue = parentDocument.SignerValue;
					document.ResponsibleId = parentDocument.ResponsibleId;
					document.ResponsibleValue = parentDocument.ResponsibleValue;
					document.AgreementsId = parentDocument.AgreementsId;
					document.AgreementsValue = parentDocument.AgreementsValue;
					document.ExecutorsId = parentDocument.ExecutorsId;
					document.ExecutorsValue = parentDocument.ExecutorsValue;
					document.AdministrativeTypeDictionaryId = parentDocument.AdministrativeTypeDictionaryId;
					document.AdministrativeTypeDictionaryValue = parentDocument.AdministrativeTypeDictionaryValue;
					document.ProtocolDate = parentDocument.ProtocolDate;
					document.LanguageDictionaryId = parentDocument.LanguageDictionaryId;
					document.LanguageDictionaryValue = parentDocument.LanguageDictionaryValue;
					document.Counters = parentDocument.Counters;
					document.BlankNumber = parentDocument.BlankNumber;
					document.SigningFormDictionaryId = parentDocument.SigningFormDictionaryId;
					document.SigningFormDictionaryValue = parentDocument.SigningFormDictionaryValue;
					document.SourceId = parentDocument.SourceId;
					document.SourceValue = parentDocument.SourceValue;
					document.NomenclatureDictionaryId = parentDocument.NomenclatureDictionaryId;
					document.NomenclatureDictionaryValue = parentDocument.NomenclatureDictionaryValue;
					document.Deed = parentDocument.Deed;
					document.Book = parentDocument.Book;
					document.Akt = parentDocument.Akt;
					document.ProjectType = parentDocument.ProjectType;
				}
			}
			return document;
		}
    }
}
