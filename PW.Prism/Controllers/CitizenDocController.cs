using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc;
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
    public class CitizenDocController : Controller
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

		private IQueryable GetCitizenDocument(IQueryable<Document> docs) {
			return docs.OrderByDescending(o => o.CreatedDate).Select(columns => new {

				DocumentId = columns.Id,
				columns.IsAttachments,
				columns.Number,
				columns.SortNumber,
				columns.AutoDocumentDate,
				columns.ApplicantType,
				columns.ApplicantName,
				columns.StateType,
				columns.MonitoringType,
				columns.ExecutionDate,
				columns.DocumentDate,
				columns.MonitoringNote,
				columns.CorrespondentsInfo,
				columns.Summary,
				columns.OutgoingNumber,
				columns.AutoOutgoingDate,
				columns.ApplicantAddress,
				columns.Counters,
				columns.ResponsibleValue,
				columns.ExecutorsValue,
				columns.AnswersValue,
				columns.AutoAnswersValue,
				columns.AutoAnswersTempValue,
				columns.CitizenTypeDictionaryValue,
				columns.FormDeliveryDictionaryValue,
				columns.LanguageDictionaryValue,
				columns.NomenclatureDictionaryValue,
				columns.QuestionDesignDictionaryValue,
				columns.CompleteDocumentsValue,
				columns.RegistratorValue,
				columns.Note,
				columns.PageCount,
				columns.CopiesCount,
				columns.CreatedDate,
				columns.OrganizationId,
                columns.CitizenTypeDictionaryId
			});
		}

	    public ActionResult ListDocument([DataSourceRequest] DataSourceRequest request) {

	        string umcId = "8F0B91F3-AF29-4D3C-96D6-019CBBDFC8BE";
            if (request.Filters.SelectMemberDescriptors().Any(descriptor => descriptor != null && descriptor.Member == "OrganizationId" && descriptor.Value.ToString().ToLower() == umcId.ToLower())) {
                var docFond = DocumentHelper.GetFondDocuments(db, 2);
                DataSourceResult resultFond = GetCitizenDocument(docFond.AsQueryable()).ToDataSourceResult(request);
                return Json(resultFond);
            }

            var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 2);
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            docs = docs.Where(o => o.OrganizationId == organizationId);
            DataSourceResult result = GetCitizenDocument(docs).ToDataSourceResult(request);
            return Json(result);
        }

	    public FileStreamResult ExportFile() {

			StiReport report = new StiReport();

			report.Load(Server.MapPath("../Reports/CitizenDocList.mrt"));
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

			return File(stream, "application/excel", "обращения.xls");


		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="parentId">Повторный</param>
		/// <returns></returns>
		public ActionResult DocumentRead(Guid id, Guid? repeatId) {
			Document document = db.Documents.Find(id);
			if (document == null)
			{
				document = new Document()
				{
					Id = id,
					DocumentType = 2,
					DocumentDate = DateTime.Now,
					MonitoringType = 1,
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
						DocumentType = 2,
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
						DocumentType = 2,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now
					};
					db.Documents.Add(baseDocument);
				}
				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 1;
				baseDocument.DocumentDate = baseDocument.DocumentDate ?? DateTime.Now;
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

		public ActionResult DocumentReview(Guid id) {
			Document baseDocument = db.Documents.Find(id);
			DocumentModel model = new DocumentModel(baseDocument);
			return PartialView(model);
		}

		public ActionResult DocumentReviewConfirm(DocumentModel document) {
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);

				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 2;
				Activity activity = new Activity() {
					Id = Guid.NewGuid(),
					IsParrent = true,
					ExecutorsId = DictionaryHelper.GetItemsId(document.CustomExecutorsId),
					ExecutorsValue = DictionaryHelper.GetItemsName(document.CustomExecutorsId),
					CreatedDate = DateTime.Now,
					ExecutionDate = baseDocument.ExecutionDate,
					DocumentId = baseDocument.Id,
					DocumentValue = baseDocument.DisplayName,
					Text = "На рассмотрение",
					ResponsibleId = DictionaryHelper.GetItemsId(document.CustomExecutorsId),
					ResponsibleValue = DictionaryHelper.GetItemsName(document.CustomExecutorsId),
					Type = 0,
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
		public ActionResult DocumentExcludeConfirm(Guid id, string text) {

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
					document.ApplicantType = parentDocument.ApplicantType;
					document.ApplicantName = parentDocument.ApplicantName;
					document.MonitoringType = parentDocument.MonitoringType;
					document.CorrespondentsInfo = parentDocument.CorrespondentsInfo;
					document.ApplicantAddress = parentDocument.ApplicantAddress;
					document.ApplicantCategoryDictionaryId = parentDocument.ApplicantCategoryDictionaryId;
					document.ApplicantCategoryDictionaryValue = parentDocument.ApplicantCategoryDictionaryValue;
					document.ApplicantEmail = parentDocument.ApplicantEmail;
					document.ExecutorsId = parentDocument.ExecutorsId;
					document.ExecutorsValue = parentDocument.ExecutorsValue;
					document.CitizenTypeDictionaryId = parentDocument.CitizenTypeDictionaryId;
					document.CitizenTypeDictionaryValue = parentDocument.CitizenTypeDictionaryValue;
					document.QuestionDesignDictionaryId = parentDocument.QuestionDesignDictionaryId;
					document.QuestionDesignDictionaryValue = parentDocument.QuestionDesignDictionaryValue;
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
					document.Summary = parentDocument.Summary;
				}
			}
			return document;
		}

	
	}
}
