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
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers
{
	[Authorize]
    public class CorrespondenceDocController : Controller
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
        public ActionResult Department() {
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }
        public ActionResult CurrentList() {
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

		public ActionResult CardInit(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

            ViewBag.IsToolbarAllow = !db.Documents.Any(m => m.Id == id) || DocumentHelper.IsToolBarAllow(db, guid);

            return PartialView(guid);
		}

		public ActionResult CardAdmin(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
            ViewBag.IsToolbarAllow = !db.Documents.Any(m => m.Id == id) || DocumentHelper.IsToolBarAllow(db, guid);
            return PartialView(guid);
		}

		public ActionResult Form(Guid? id) {


			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormInit(Guid? id) {

          
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

            ViewBag.IsToolbarAllow = !db.Documents.Any(m => m.Id == id) || DocumentHelper.IsToolBarAllow(db, guid);


            return PartialView(guid);
		}

		public ActionResult FormAdmin(Guid? id) {


			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
		    ViewBag.IsToolbarAllow = !db.Documents.Any(m => m.Id == id) || DocumentHelper.IsToolBarAllow(db, guid);

            return PartialView(guid);
		}

		// GET: /IncomingDoc/Edit/5
		public ActionResult Edit(Guid? id) {
			if (id == null) {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Document document = db.Documents.Find(id);

            ViewBag.IsToolbarAllow = !db.Documents.Any(m => m.Id == id) || DocumentHelper.IsToolBarAllow(db, document.Id);


            if (document == null) {
				return HttpNotFound();
			}
			if (document.ProjectType == 2)
				return PartialView("Card", id.Value);
			if (document.ProjectType == 5) {
				if (document.ApplicantType == 0)
					return PartialView("CardInit", id.Value);
				if (document.ApplicantType == 1)
					return PartialView("CardAdmin", id.Value);
			}
			return PartialView("Card", id.Value);
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private IQueryable GetInnerDocument(IQueryable<Document> docs) {
			return docs.OrderByDescending(o => o.CreatedDate).Select(columns => new {
				DocumentId = columns.Id,
				columns.IsAttachments,
				columns.Number,
				columns.DocumentDate,
				columns.SortNumber,
				columns.AutoDocumentDate,
				columns.StateType,
                columns.MonitoringType,
                columns.Summary,
				columns.ExecutorsValue,
				columns.ResponsibleValue,
				columns.CorrespondentsValue,
				columns.DocumentType,
				columns.SignerValue,
				columns.SourceValue,
				columns.RegistratorValue,
				columns.PageCount,
				columns.CopiesCount,
				columns.OrganizationId,
				columns.NomenclatureDictionaryValue,
				columns.CreatedDate
			});
		}

		public ActionResult ListDocument([DataSourceRequest] DataSourceRequest request) {
            string umcId = "8F0B91F3-AF29-4D3C-96D6-019CBBDFC8BE";
            if (request.Filters.SelectMemberDescriptors().Any(descriptor => descriptor != null && descriptor.Member == "OrganizationId" && descriptor.Value.ToString().ToLower() == umcId.ToLower()))
            {
                var docFond = DocumentHelper.GetFondDocuments(db, 5);
                docFond = docFond.Where(o => o.ApplicantType == 1).ToList();
                DataSourceResult resultFond = GetInnerDocument(docFond.AsQueryable()).ToDataSourceResult(request);
                return Json(resultFond);
            }

            var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 5);
			docs = docs.Where(o => o.ApplicantType == 1);
			Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
			docs = docs.Where(o => o.OrganizationId == organizationId);
			DataSourceResult result = GetInnerDocument(docs).ToDataSourceResult(request);

			return Json(result);
		}
        public ActionResult ListDocumentDepartment([DataSourceRequest] DataSourceRequest request) {
            Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 5 && o.StateType > 0);
             docs = docs
                .Join(db.Employees, d => d.CreatedUserId, e => e.Id.ToString(), (d, e) => new {d, e})
                .Where( m =>  m.d.ApplicantType == 0 ? (m.d.OrganizationId == organizationId)
                  : (m.d.OrganizationId != organizationId && m.e.OrganizationId==organizationId)).Select(m=>m.d);
            DataSourceResult result = GetInnerDocument(docs).ToDataSourceResult(request);
            return Json(result);
        }
        public ActionResult CurrentListDocument([DataSourceRequest] DataSourceRequest request) {
            Employee currentEmployee = UserHelper.GetCurrentEmployee();
            var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 5 && ((o.ApplicantType==1 && o.StateType>0) || o.ApplicantType!=1));
			IEnumerable<Guid> employeeList = new List<Guid>();
			if (currentEmployee != null) {
				employeeList = employeeList.Union(new[] { currentEmployee.Id });
				employeeList = employeeList.Union(UserHelper.GetDeputy(currentEmployee.Id.ToString()));
				employeeList = employeeList.Union(UserHelper.GetAssistants(currentEmployee.Id.ToString()));
			}
			List<string> list = employeeList.Select(o => o.ToString()).ToList();
			docs = docs.Where(x=>list.Contains(x.CreatedUserId));

			DataSourceResult result = GetInnerDocument(docs).ToDataSourceResult(request);

			return Json(result);
		}

		public FileStreamResult ExportFile() {

			StiReport report = new StiReport();

			report.Load(Server.MapPath("../Reports/InnerDocList.mrt"));
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

			return File(stream, "application/excel", "Внутренние.xls");
		}

		public ActionResult DocumentRead(Guid id) {
			Document document = db.Documents.Find(id);
			if (document == null) {
				document = new Document() {
					Id = id,
					DocumentType = 5,
					ProjectType = 5,
					DocumentDate = DateTime.Now,
					MonitoringType = 1,
					OutgoingType = 1,
					AttachPath = FileHelper.GetObjectPathRoot()

				};
			}

			DocumentModel model = new DocumentModel(document);
			return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
		}

		public ActionResult DocumentReadInit(Guid id) {
			Document document = db.Documents.Find(id);
			if (document == null) {
				document = new Document() {
					Id = id,
					DocumentType = 5,
					ProjectType = 5,
					DocumentDate = DateTime.Now,
					MonitoringType = 1,
					OutgoingType = 0,
					AttachPath = FileHelper.GetObjectPathRoot()

				};
			}

			DocumentModel model = new DocumentModel(document);
			return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
		}

		[HttpPost]
		public ActionResult DocumentUpdate(DocumentModel document) {
			if (document != null) {
				if (!CheckSecurity(document))
					return Content(bool.FalseString);

				Document baseDocument = db.Documents.Find(document.Id);
				if (baseDocument == null) {
					baseDocument = new Document() {
						Id = document.Id,
						DocumentType = 5,
						ProjectType = document.ProjectType,
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
				if (!CheckSecurity(document))
					return Content(JsonConvert.SerializeObject(new { State = false, document = "" }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

				Document baseDocument = db.Documents.Find(document.Id);
				if (baseDocument == null) {
					baseDocument = new Document() {
						Id = document.Id,
						ProjectType = document.ProjectType,
						DocumentType = 5,
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
                if (baseDocument.ApplicantType != 1)
                {
                    if (UserHelper.CheckGuide(baseDocument.ExecutorsId))
                    {
                        baseDocument.ApplicantType = 1;
                    }
                }
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
					ResponsibleId = document.ExecutorsId[0].Id,
					ResponsibleValue = document.ExecutorsId[0].Name,
					Type = 1,
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

		[HttpPost]
		public ActionResult DocumentExclude(DocumentModel document) {
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);

				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 9;
				baseDocument.FulfilledDate = DateTime.Now;
				db.SaveChanges();
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

				return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);
			}
			return Content(bool.FalseString);
		}

		private bool CheckSecurity(DocumentModel document) {
			if (EmployePermissionHelper.IsCreateCorDoc)
				return true;
			Guid nomenclatureId;
			if (Guid.TryParse(DictionaryHelper.GetItemsId(document.NomenclatureDictionaryId), out nomenclatureId)) {
				Dictionary nomenclature = db.Dictionaries.FirstOrDefault(x => x.Id == nomenclatureId);
				if (nomenclature != null && nomenclature.IsGuide)
					return false;
			}
			return true;
		}
    }
}
