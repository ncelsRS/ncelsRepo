using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure.Implementation;
using Kendo.Mvc.UI;
using Ncels.Helpers;
using Newtonsoft.Json;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Prism.Controllers {
	[Authorize]
	public class ProjectDocController : Controller {
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
		public ActionResult CardOut(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardCor(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardCorInit(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardAdm(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardAdmMain(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardPrt(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardIsp(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardOutByTask(Guid? id, Guid taskId) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = taskId;
			return PartialView("CardOut", guid);
		}

		public ActionResult CardCorByTask(Guid? id, Guid taskId) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = taskId;
			return PartialView("CardCor", guid);
		}

		public ActionResult CardZkl(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardAkt(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardExp(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardFkl(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardFct(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult CardNap(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}
		public ActionResult CardPer(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}
		public ActionResult CardPes(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}
		public ActionResult CardPfc(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}
		public ActionResult CardPfk(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}
		public ActionResult CardZfc(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}
		public ActionResult CardZfk(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			ViewBag.TaskId = null;
			return PartialView(guid);
		}

		public ActionResult FormOut(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormCor(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormCorInit(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormAdm(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormAdmMain(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormPrt(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormIsp(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult FormZkl(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult FormAkt(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult FormExp(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult FormFkl(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult FormFct(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult FormNap(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult FormPer(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult FormPes(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult FormPfc(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult FormPfk(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult FormZfc(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}
		public ActionResult FormZfk(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			return PartialView(guid);
		}

		public ActionResult FormBp(Guid? id) {
			string[] res = new string[2];
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();
			res[0] = guid.ToString();
			res[1] = "";
			if (id.HasValue) {
				Document document = db.Documents.Find(id.Value);
				if (document != null) {
					res[1] = document.StateType.ToString();
				}
			}
			return PartialView(res);
		}

		public ActionResult FormBps(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			return PartialView(guid);
		}

		public ActionResult FormBpOut(Guid? id) {
			Guid guid = id.HasValue ? id.Value : Guid.NewGuid();

			Document doc = db.Documents.FirstOrDefault(x => x.Id == guid);

			if (doc != null && !string.IsNullOrEmpty(doc.SourceId) && Guid.TryParse(doc.SourceId, out guid)) {
				return PartialView("FormBpOut", guid);
			}

			return PartialView("FormBpOut", Guid.Empty);
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
			switch (document.ProjectType) {
				case 1:
					return PartialView("CardOut", id.Value);
				case 2:
					return PartialView("CardCor", id.Value);
				case 3:
					return PartialView("CardAdm", id.Value);
				case 4:
					return PartialView("CardPrt", id.Value);
				case 5:
					return PartialView("CardCorInit", id.Value);
				case 6:
					return PartialView("CardAdmMain", id.Value);
				case 10:
					return PartialView("CardZkl", id.Value);
				case 11:
					return PartialView("CardAkt", id.Value);
				case 12:
					return PartialView("CardExp", id.Value);
				case 13:
					return PartialView("CardFct", id.Value);
				case 14:
					return PartialView("CardFkl", id.Value);
				case 15:
					return PartialView("CardIsp", id.Value);
				case 16:
					return PartialView("CardNap", id.Value);
				case 17:
					return PartialView("CardPer", id.Value);
				case 18:
					return PartialView("CardPes", id.Value);
				case 19:
					return PartialView("CardPfc", id.Value);
				case 20:
					return PartialView("CardPfk", id.Value);
				case 21:
					return PartialView("CardZfc", id.Value);
				case 22:
					return PartialView("CardZfk", id.Value);
			}
			return PartialView("CardOut", id.Value);
		}

		protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
		private IQueryable GetProjectDocument(IQueryable<Document> docs) {
			return docs.OrderByDescending(o => o.CreatedDate).Select(columns => new {
				DocumentId = columns.Id,
				columns.IsAttachments,
				columns.Number,
				columns.SortNumber,
				columns.AutoDocumentDate,
				columns.DocumentDate,
				columns.StateType,
				columns.Summary,
				columns.ExecutorsValue,
				columns.ResponsibleValue,
				columns.CorrespondentsValue,
				columns.DocumentType,
				columns.SignerValue,
				columns.DestinationValue,
				columns.RegistratorValue,
				DocumentDictionaryTypeValue =
					columns.ProjectType == 10 ? "Заключение о безопасности" :
					columns.ProjectType == 11 ? "Акт выполненных работ" :
					columns.ProjectType == 12 ? "Протокол Экспертного совета" :
					columns.ProjectType == 13 ? "Протокол фармацевтической комиссии" :
					columns.ProjectType == 14 ? "Протокол фармакологической комиссии" :
					columns.ProjectType == 15 ? "Протокол испытаний" :
					columns.ProjectType == 16 ? "Направление по результатам первичной экспертизы" :
					columns.ProjectType == 17 ? "Заключение по первичной экспертизе" :
					columns.ProjectType == 18 ? "Повестка Экспертного совета" :
					columns.ProjectType == 19 ? "Повестка фармацевтической комиссии" :
					columns.ProjectType == 20 ? "Повестка фармакологической комиссии" :
					columns.ProjectType == 21 ? "Заключение эксперта фармацевтической экспертизы" :
					columns.ProjectType == 22 ? "Заключение эксперта фармакологической экспертизы" : 
					"Протокол",
				columns.PageCount,
				columns.CopiesCount,
				columns.CreatedDate
			});
		}

		public ActionResult ListDocument([DataSourceRequest] DataSourceRequest request) {
			var docs = db.Documents.Where(o => o.IsDeleted == false && o.DocumentType == 4 && o.ProjectType > 9);
			IEnumerable<string> employeeList = new List<string>();
			Employee currentEmployee = UserHelper.GetCurrentEmployee();
			if (currentEmployee != null) {
				employeeList = employeeList.Union(new[] { currentEmployee.Id.ToString() });
				employeeList = employeeList.Union(UserHelper.GetDeputy(currentEmployee.Id.ToString()).Select(o => o.ToString()));
				employeeList = employeeList.Union(UserHelper.GetAssistants(currentEmployee.Id.ToString()).Select(o => o.ToString()));
			}
			docs = docs.Where(x => employeeList.Contains(x.OwnerId));
			Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
			docs = docs.Where(o => o.OrganizationId == organizationId);
			DataSourceResult result = GetProjectDocument(docs).ToDataSourceResult(request);

			return Json(result);
		}

		public FileStreamResult ExportFile() {

			StiReport report = new StiReport();


			report.Load(Server.MapPath("../Reports/ProjectDocList.mrt"));
			foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>()) {
				data.ConnectionString = UserHelper.GetCnString();
			}
			if (report.Dictionary.Variables.Contains("EmployeeId")) {
				report.Dictionary.Variables["EmployeeId"].Value = UserHelper.GetCurrentEmployee().Id.ToString();
			}
			report.Render(false);
			var stream = new MemoryStream();

			report.ExportDocument(StiExportFormat.Excel, stream);
			stream.Position = 0;

			return File(stream, "application/excel", "проекты.xls");
		}

		public ActionResult DocumentRead(Guid id, Guid? taskId, string type) {

			Document document = db.Documents.Find(id);
			if (document == null) {

				document = new Document() {
					Id = id,
					DocumentType = 4,
					DocumentDate = DateTime.Now,
					CreatedUserId = UserHelper.GetCurrentEmployee().Id.ToString(),
					CreatedUserValue = UserHelper.GetCurrentEmployee().DisplayName,
					TemplateId = new Guid("C3292589-A25B-4CEF-8CB5-C7E64946C1D3"),
					MonitoringType = 1,
					AttachPath = FileHelper.GetObjectPathRoot()

				};

				switch (type) {
					case "Outgoing":
						document.ProjectType = 1;
						break;
					case "Inner":
						document.ProjectType = 2;
						break;
					case "Adm":
						document.ProjectType = 3;
						break;
					case "Protocol":
						document.ProjectType = 4;
						break;
					case "InnerInit":
						document.ProjectType = 5;
						break;
					case "AdmMain":
						document.ProjectType = 6;
						break;
					case "Zkl":
						document.ProjectType = 10;
						break;
					case "Akt":
						document.ProjectType = 11;
						break;
					case "Exp":
						document.ProjectType = 12;
						break;
					case "Fct":
						document.ProjectType = 13;
						break;
					case "Fkl":
						document.ProjectType = 14;
						break;
					case "Isp":
						document.ProjectType = 15;
						break;
					case "Nap":
						document.ProjectType = 16;
						break;
					case "Per":
						document.ProjectType = 17;
						break;
					case "Pes":
						document.ProjectType = 18;
						break;
					case "Pfc":
						document.ProjectType = 19;
						break;
					case "Pfk":
						document.ProjectType = 20;
						break;
					case "Zfc":
						document.ProjectType = 21;
						break;
					case "Zfk":
						document.ProjectType = 22;
						break;
				}

				if (taskId.HasValue && type == "Outgoing") {
					Task task = db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId.Value);
					if (task != null) {
						document.CorrespondentsValue = task.Document.CorrespondentsValue;
						document.ApplicantEmail = task.Document.ApplicantEmail;
						document.AnswersId = task.Document.Id.ToString();
						document.AnswersValue = task.Document.DisplayName;
						document.LanguageDictionaryId = task.Document.LanguageDictionaryId;
						document.LanguageDictionaryValue = task.Document.LanguageDictionaryValue;
						document.DocumentKindDictionaryId = task.Document.DocumentKindDictionaryId;
						document.DocumentKindDictionaryValue = task.Document.DocumentKindDictionaryValue;
						document.QuestionDesignDictionaryId = task.Document.QuestionDesignDictionaryId;
						document.QuestionDesignDictionaryValue = task.Document.QuestionDesignDictionaryValue;
						document.ExecutionDate = task.Document.ExecutionDate;
						document.CorrespondentsId = task.Document.CorrespondentsId;
						document.CorrespondentsValue = task.Document.CorrespondentsValue;


						if (task.Document.DocumentType == 2) {
							document.CorrespondentsInfo = task.Document.CorrespondentsInfo;
						}
						document.MainTaskId = taskId;
						document.MainDocumentId = task.DocumentId;
						document.OutgoingType = 1;

					}
				}

				if (taskId.HasValue && type == "Inner") {
					Task task = db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId.Value);
					if (task != null) {

						document.AnswersId = task.Document.Id.ToString();
						document.AnswersValue = task.Document.DisplayName;
						document.ApplicantEmail = task.Document.ApplicantEmail;
						document.LanguageDictionaryId = task.Document.LanguageDictionaryId;
						document.LanguageDictionaryValue = task.Document.LanguageDictionaryValue;
						document.DocumentKindDictionaryId = task.Document.DocumentKindDictionaryId;
						document.DocumentKindDictionaryValue = task.Document.DocumentKindDictionaryValue;
						document.QuestionDesignDictionaryId = task.Document.QuestionDesignDictionaryId;
						document.QuestionDesignDictionaryValue = task.Document.QuestionDesignDictionaryValue;
						document.MainTaskId = taskId;
						document.MainDocumentId = task.DocumentId;
						document.ExecutionDate = task.Document.ExecutionDate;
					}
				}
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
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now,
						DocumentDate = DateTime.Now,
						ProjectType = document.ProjectType,
						DocumentType = 4,
						TemplateId = new Guid("C3292589-A25B-4CEF-8CB5-C7E64946C1D3")
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
						DocumentType = 4,
						ProjectType = document.ProjectType,
						CreatedDate = DateTime.Now,
						ModifiedDate = DateTime.Now,
						TemplateId = new Guid("C3292589-A25B-4CEF-8CB5-C7E64946C1D3")
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
				if ((baseDocument.ProjectType == 5 || baseDocument.ProjectType == 2) && baseDocument.ApplicantType != 1) {
					if (UserHelper.CheckGuide(baseDocument.ExecutorsId)) {
						baseDocument.ApplicantType = 1;
					}
				}
			    baseDocument.RemarkId = document.RemarkId;
			    baseDocument.RemarkText1 = document.RemarkText1;
                baseDocument.RemarkText2 = document.RemarkText2;
                baseDocument.RemarkText3 = document.RemarkText3;
                db.SaveChanges();
				GenerateActivity(baseDocument);
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

				return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);
			}
			return Content(bool.FalseString);
		}

		public void GenerateActivity(Document document) {
			List<Item> items = new List<Item>();
			if (document.MainTaskId.HasValue) {
                LogHelper.Log.DebugFormat("documentid {0} has maintaskId {1}", document.Id, document.MainTaskId);
				Task task = db.Tasks.Include(o => o.Activity).FirstOrDefault(x => x.Id == document.MainTaskId);
				if (task != null) {
					items.Add(new Item { Id = task.Activity.AuthorId, Name = task.Activity.AuthorValue });
                    LogHelper.Log.DebugFormat("add main activity AuthorId {0}, AuthorValue {1}", task.Activity.AuthorId, task.Activity.AuthorValue);
                    RecycleActivity(task.Activity.ParentId, items);
				}
			    foreach (var item in items) {
                    LogHelper.Log.DebugFormat("add task AuthorId {0}, AuthorValue {1}", item.Id, item.Name);
                }
                
                Guid? parentId = null;
				foreach (var item in items) {
					Activity activity = new Activity {
						Id = Guid.NewGuid(),
						//	ParentTask = task.Id,
						DocumentId = document.Id,
						AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
						AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
						ExecutorsId = item.Id,
						ExecutorsValue = item.Name,
						//ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
						//ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
						Type = 3,
						IsParrent = !parentId.HasValue,
						CreatedDate = DateTime.Now,
						ParentId = !parentId.HasValue ? (Guid?)null : parentId.Value,
						ExecutionDate = document.ExecutionDate,
						Text = "Согласовать",
						IsNotActive = true,
						//IsMainLine = task.IsMainLine
					};
					parentId = activity.Id;
					db.Activities.Add(activity);

				}
				db.SaveChanges();
			}
			else {
                LogHelper.Log.Debug("documentid not has main task");
                GenerateDefaultActivity(document);
			}
		}

		public void GenerateDefaultActivity(Document document) {
			List<Item> items = new List<Item>();

			var employee = db.Employees.Find(Guid.Parse(document.RegistratorId));
            LogHelper.Log.Debug("RecycleEmployees start");
            RecycleEmployees(employee.PositionId, items);
			Guid? parentId = null;

			foreach (var item in items) {
				Activity activity = new Activity {
					Id = Guid.NewGuid(),
					//	ParentTask = task.Id,
					DocumentId = document.Id,
					AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
					AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
					ExecutorsId = item.Id,
					ExecutorsValue = item.Name,
					//ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
					//ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
					Type = 3,
					IsParrent = !parentId.HasValue,
					CreatedDate = DateTime.Now,
					ParentId = !parentId.HasValue ? (Guid?)null : parentId.Value,
					ExecutionDate = document.ExecutionDate,
					Text = "Согласовать",
					IsNotActive = true,
					//IsMainLine = task.IsMainLine
				};
				parentId = activity.Id;
				db.Activities.Add(activity);

			}
			db.SaveChanges();

		}

		public void RecycleEmployees(Guid? parentId, List<Item> items) {

			if (parentId != null) {
				Unit unit = db.Units.Find(parentId);
				if (unit.BossId.HasValue()) {
					items.Add(new Item { Id = unit.BossId, Name = unit.BossValue });
                    LogHelper.Log.DebugFormat("RecycleEmployees add item AuthorId {0}, AuthorValue {1}", unit.BossId, unit.BossValue);
                }
				RecycleEmployees(unit.ParentId, items);
			}
		}


		public void RecycleActivity(Guid? parentId, List<Item> items) {
			if (parentId.HasValue) {
				Activity activity = db.Activities.Find(parentId.Value);
				if (activity.Type != 0) {
                    LogHelper.Log.DebugFormat("RecycleActivity add item AuthorId {0}, AuthorValue {1}", activity.AuthorId, activity.AuthorValue);
                    items.Add(new Item { Id = activity.AuthorId, Name = activity.AuthorValue });
					RecycleActivity(activity.ParentId, items);
				}
			}
		}
		[HttpPost]
		public ActionResult DocumentReview(DocumentModel document) {
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);

				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 4;

				Activity activity = db.Activities.FirstOrDefault(x => x.DocumentId == document.Id && x.IsParrent && x.Branch == 0);

				if (activity != null)
					activity.IsCurrent = true;

				db.SaveChanges();
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

			}
			return Content(bool.FalseString);
		}

		public ActionResult DocumentBpReview(Guid id) {

			Document baseDocument = db.Documents.Find(id);


			baseDocument.StateType = 4;

			Activity activity = db.Activities.FirstOrDefault(x => x.DocumentId == id && x.IsParrent && x.Branch == 0);

			if (activity != null)
				activity.IsCurrent = true;

			db.SaveChanges();
			DocumentModel model = new DocumentModel(baseDocument);
			return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));


			return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult DocumentBuild(DocumentModel document) {
			if (document != null) {
				Document baseDocument = db.Documents.Include(o => o.Template).First(o => o.Id == document.Id);
				baseDocument = document.GetDocument(baseDocument);
				db.SaveChanges();

				TemplateDocument templateDocument = new TemplateDocument(baseDocument);
                List<ReplaceItem> items = new List<ReplaceItem>(templateDocument.ItemsValue.Select(o => new ReplaceItem { Key = o.Key, Value = o.Value }));

                FileStream fileStream = System.IO.File.OpenRead(HttpContext.Server.MapPath("/Content/Projects/Project" + baseDocument.ProjectType + ".docx"));
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);

                if (document.RemarkId == 1)
			    {
                    items.FirstOrDefault(i => i.Key == "Comment1").Value = !string.IsNullOrEmpty(document.RemarkText1)?"1) "+ document.RemarkText1:"";
                    items.FirstOrDefault(i => i.Key == "Comment2").Value = !string.IsNullOrEmpty(document.RemarkText2) ? "2) " + document.RemarkText2 : "";
                    items.FirstOrDefault(i => i.Key == "Comment3").Value = !string.IsNullOrEmpty(document.RemarkText3) ? "3) " + document.RemarkText3 : "";
                    UploadHelper.UploadReplace(data, "Замечания.docx", baseDocument.AttachPath, items);
                }
                else if (document.RemarkId == 2)
                {
                    fileStream = System.IO.File.OpenRead(HttpContext.Server.MapPath("/Content/Projects/Parley.docx"));
                    data = new byte[fileStream.Length];
                    fileStream.Read(data, 0, data.Length);
                    items.AddRange(new List<ReplaceItem>
                    {
                        new ReplaceItem
                        {
                            Key = "ParleyStartDate",
                            Value = document.ParleyStartDate.ToString("d MMM yyyy", CultureInfo.CurrentCulture)
                        },
                        new ReplaceItem
                        {
                            Key = "ParleyEndDate",
                            Value = document.ParleyEndDate.ToString("d MMM yyyy", CultureInfo.CurrentCulture)
                        }
                    });
                    UploadHelper.UploadReplace(data, "Приглашение на переговоры.docx", baseDocument.AttachPath, items);
                }
                else
                {
                    UploadHelper.UploadReplace(data, "Проект.docx", baseDocument.AttachPath, items);
                }

                fileStream.Close();
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

				return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);
			}
			return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult DocumentBuildWithParam(DocumentModel document, string type) {
			if (document != null) {
				Document baseDocument = db.Documents.Include(o => o.Template).First(o => o.Id == document.Id);
				baseDocument = document.GetDocument(baseDocument);
				db.SaveChanges();

				TemplateDocument templateDocument = new TemplateDocument(baseDocument);
				List<ReplaceItem> items = new List<ReplaceItem>(templateDocument.ItemsValue.Select(o => new ReplaceItem { Key = o.Key, Value = o.Value }));

				FileStream fileStream = System.IO.File.OpenRead(HttpContext.Server.MapPath("/Content/Projects/templates/" + type + ".docx"));
				byte[] data = new byte[fileStream.Length];
				fileStream.Read(data, 0, data.Length);
				UploadHelper.UploadReplace(data, "протокол.docx", baseDocument.AttachPath, items);

				fileStream.Close();
				DocumentModel model = new DocumentModel(baseDocument);
				return Content(JsonConvert.SerializeObject(new { State = true, document = model }, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));

				return Json(new { State = true, document = model }, JsonRequestBehavior.AllowGet);
			}
			return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult DocumentReject(DocumentModel document) {
			if (document != null) {
				Document baseDocument = db.Documents.Find(document.Id);

				baseDocument = document.GetDocument(baseDocument);
				baseDocument.StateType = 6;

				db.SaveChanges();
				int branch = db.Activities.Where(d => d.DocumentId == baseDocument.Id).Max(o => o.Branch);

				var list = db.Activities.Where(d => d.DocumentId == baseDocument.Id && d.Branch == branch);

				var listItem = list.FirstOrDefault(o => o.ParentId == null);
				if (listItem != null) {

					Guid? parentId = null;
					for (int i = 0; i < list.Count(); i++) {
						Activity activity = new Activity {
							Id = Guid.NewGuid(),
							//	ParentTask = task.Id,
							DocumentId = document.Id,
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
							ExecutionDate = document.ExecutionDate,
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
			}
			return Content(bool.FalseString);
		}

		[HttpPost]
		public ActionResult DocumentBpReject(Guid id) {

			Document baseDocument = db.Documents.Find(id);

			baseDocument.StateType = 6;

			db.SaveChanges();
			int branch = db.Activities.Where(d => d.DocumentId == baseDocument.Id).Max(o => o.Branch);

			var list = db.Activities.Where(d => d.DocumentId == baseDocument.Id && d.Branch == branch).Where(o => o.Type != 4);

			var listItem = list.FirstOrDefault(o => o.ParentId == null);
			if (listItem != null) {

				Guid? parentId = null;
				for (int i = 0; i < list.Count(); i++) {
					Activity activity = new Activity {
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
	}
}
