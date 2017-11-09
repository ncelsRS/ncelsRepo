using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.Web.Security;
using Newtonsoft.Json.Schema;
using Document = PW.Ncels.Database.DataModel.Document;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers {

		[Authorize]
		public class MobileController : Controller {
		// GET: Mobile
		public ActionResult Index() {
			return View();
		}


		//
		// POST: /Account/LogOn

		private ncelsEntities _db = UserHelper.GetCn();

		//новые
		[HttpGet]
		public object GetNew(int page, int pageSize, string filterText) {
			object obj = GetTasks(0, page, pageSize, filterText);
			return obj;
		}

		// в работе 
		[HttpGet]
		public object GetInWork(int page, int pageSize, string filterText) {
			object obj = GetTasks(1, page, pageSize, filterText);
			return obj;
		}

		// отписанные
		[HttpGet]
		public object GetUsubscribed(int page, int pageSize, string filterText) {
			object obj = GetTasks(2, page, pageSize, filterText);
			return obj;
		}

		// исполненные
		[HttpGet]
		public object GetDone(int page, int pageSize, string filterText) {
			object obj = GetTasks(3, page, pageSize, filterText);
			return obj;
		}
		// все
		[HttpGet]
		public object GetAllTask(int page, int pageSize, string filterText) {
			object obj = GetTasks(4, page, pageSize, filterText);
			return obj;
		}

		[HttpGet]
		public JsonResult GetTaskCount()
		{
			List<int> countTasks = new List<int>();
			IEnumerable<Guid> employeeList = new List<Guid>();
			Employee currentEmployee = UserHelper.GetCurrentEmployee();
			if (currentEmployee != null) {
				employeeList = employeeList.Union(new[] { currentEmployee.Id });
				employeeList = employeeList.Union(UserHelper.GetDeputy(currentEmployee.Id.ToString()));
				employeeList = employeeList.Union(UserHelper.GetAssistants(currentEmployee.Id.ToString()));
			}
			IQueryable<Task> queryable = _db.Tasks.Include("Document.Template")
				.Where(o => !o.Document.IsDeleted)
				.Where(x => x.AccessTasks.Count(o => employeeList.Contains(o.UserId)) > 0);
				int newTask = queryable.Where(o => o.IsActive && o.State == 0).Count();
				countTasks.Add(newTask);
				int inworkTask = queryable.Where(o => o.IsActive && (o.State == 4)).Count();
				countTasks.Add(inworkTask);

				int unsubscribedTask = queryable.Where(o => o.IsActive && (o.State == 1)).Count();
				countTasks.Add(unsubscribedTask);

				int executed = queryable.Where(o => o.IsActive && (o.State == 2 || o.State == 3)).Count();
				countTasks.Add(executed);

				int allTask = queryable.Where(o => o.IsActive && (o.State == 0 || o.State == 1 || o.State == 2 || o.State == 3 || o.State == 4)).Count();
				countTasks.Add(allTask);
				return Json( countTasks, JsonRequestBehavior.AllowGet);
		}

			public object GetTasks(int type, int page, int pageSize, string filterText){
			IEnumerable<Guid> employeeList = new List<Guid>();
			Employee currentEmployee = UserHelper.GetCurrentEmployee();
			if (currentEmployee != null) {
				employeeList = employeeList.Union(new[] { currentEmployee.Id });
				employeeList = employeeList.Union(UserHelper.GetDeputy(currentEmployee.Id.ToString()));
				employeeList = employeeList.Union(UserHelper.GetAssistants(currentEmployee.Id.ToString()));
			}
			IQueryable<Task> queryable = _db.Tasks.Include("Document.Template")
				.Where(o => !o.Document.IsDeleted)
				.Where(x => x.AccessTasks.Count(o => employeeList.Contains(o.UserId)) > 0);


			switch (type) {
				//новые
				case 0:
					queryable = queryable.Where(o => o.IsActive && o.State == 0);
					break;

				// в работе
				case 1:
					queryable = queryable.Where(o => o.IsActive && (o.State == 4));
					break;

				// отписанные
				case 2:
					queryable = queryable.Where(o => o.IsActive && (o.State == 1));
					break;

				// исполненные
				case 3:
					queryable = queryable.Where(o => o.IsActive && (o.State == 2 || o.State == 3));
					break;

				// все
				case 4:
					queryable = queryable.Where(o => o.IsActive && (o.State == 0 || o.State == 1 || o.State == 2 || o.State == 3 || o.State == 4));
					break;

				//новые и в работе ; все
				default:
					queryable = queryable.Where(o => o.IsActive);
					break;
			}


			bool isAddBp = EmployePermissionHelper.IsAddBp;
			if(filterText=="") { 
			var data = queryable
				.Select(x => new {
					TaskId = x.Id,
					Author = x.AuthorValue,
					CreatedDate = x.CreatedDate,
					//
					TypeEx = x.TypeEx,
					Type = x.Type,
					State = x.State,
					DocumentMonitoringType = x.Document.MonitoringType,
					DocumentId = x.DocumentId,
					IsActive = x.IsActive,
					Summary = x.Document.Summary,
					//
					DocumentDate = x.Document.AutoDocumentDate,
					DocumentNumber = x.Document.Number,
					Text = x.Document.Summary,
					CorrespondentsValue = (x.Document.DocumentType == 0
						? x.Document.CorrespondentsValue
						: x.Document.DocumentType == 2
							? x.Document.CorrespondentsInfo
							: x.Document.DocumentType == 3
								? x.Document.RegistratorValue
								: x.Document.DocumentType == 4
									? x.Document.OwnerValue
									 : x.Document.DocumentType == 5
									? x.Document.CreatedUserValue
									: null)
				}).OrderByDescending(m => m.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
			int count = queryable
				 .Select(x => new {
					 TaskId = x.Id,
					 Author = x.AuthorValue,
					 CreatedDate = x.CreatedDate,
					 //
					 TypeEx = x.TypeEx,
					 Type = x.Type,
					 State = x.State,
					 DocumentMonitoringType = x.Document.MonitoringType,
					 DocumentId = x.DocumentId,
					 IsActive = x.IsActive,
					 	Summary = x.Document.Summary,
					 //
					 DocumentDate = x.Document.AutoDocumentDate,
					 DocumentNumber = x.Document.Number,
					 Text = x.Document.Summary,
					 CorrespondentsValue = (x.Document.DocumentType == 0
						 ? x.Document.CorrespondentsValue
						 : x.Document.DocumentType == 2
							 ? x.Document.CorrespondentsInfo
							  : x.Document.DocumentType == 3
								? x.Document.RegistratorValue
								: x.Document.DocumentType == 4
									? x.Document.OwnerValue
									 : x.Document.DocumentType == 5
									? x.Document.CreatedUserValue
									: null)
				 }).OrderByDescending(m => m.CreatedDate).Count();
			return Json(new { Data = data, Count = count }, JsonRequestBehavior.AllowGet);
			}
			else { 
				var data = queryable
				.Select(x => new {
					TaskId = x.Id,
					Author = x.AuthorValue,
					CreatedDate = x.CreatedDate,
					//
					TypeEx = x.TypeEx,
					Type = x.Type,
					State = x.State,
					DocumentMonitoringType = x.Document.MonitoringType,
					DocumentId = x.DocumentId,
					IsActive = x.IsActive,
						Summary = x.Document.Summary,
					//
					DocumentDate = x.Document.AutoDocumentDate,
					DocumentNumber = x.Document.Number,
					Text = x.Document.Summary,
					CorrespondentsValue = (x.Document.DocumentType == 0
						? x.Document.CorrespondentsValue
						: x.Document.DocumentType == 2
							? x.Document.CorrespondentsInfo
							: x.Document.DocumentType == 3
								? x.Document.RegistratorValue
								: x.Document.DocumentType == 4
									? x.Document.OwnerValue
									 : x.Document.DocumentType == 5
									? x.Document.CreatedUserValue
									: null)
				}).Where(x => x.DocumentNumber.Contains(filterText) || x.CorrespondentsValue.Contains(filterText) || x.Summary.Contains(filterText)).OrderByDescending(m => m.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
			int count = queryable
				 .Select(x => new {
					 TaskId = x.Id,
					 Author = x.AuthorValue,
					 CreatedDate = x.CreatedDate,
					 //
					 TypeEx = x.TypeEx,
					 Type = x.Type,
					 State = x.State,
					 DocumentMonitoringType = x.Document.MonitoringType,
					 DocumentId = x.DocumentId,
					 IsActive = x.IsActive,
					 Summary = x.Document.Summary,
					 //
					 DocumentDate = x.Document.AutoDocumentDate,
					 DocumentNumber = x.Document.Number,
					 Text = x.Document.Summary,
					 CorrespondentsValue = (x.Document.DocumentType == 0
						 ? x.Document.CorrespondentsValue
						 : x.Document.DocumentType == 2
							 ? x.Document.CorrespondentsInfo
							  : x.Document.DocumentType == 3
								? x.Document.RegistratorValue
								: x.Document.DocumentType == 4
									? x.Document.OwnerValue
									 : x.Document.DocumentType == 5
									? x.Document.CreatedUserValue
									: null)
				 }).Where(x => x.DocumentNumber.Contains(filterText) || x.CorrespondentsValue.Contains(filterText) || x.Summary.Contains(filterText)).OrderByDescending(m => m.CreatedDate).Count();
			return Json(new { Data = data, Count = count }, JsonRequestBehavior.AllowGet);
			}
		}


		

			public ActionResult ExecutionPathPopup(Guid taskId)
			{

				Guid? docId = _db.Tasks.Where(m => m.Id == taskId).Select(m => m.DocumentId).FirstOrDefault();
				if (docId != null)
				{
					var tasks =
						_db.Tasks.Where(m => m.DocumentId == docId && m.IsActive)
							.Select(
								o =>
									new TaskPathModel
									{
										AuthorId = o.AuthorId,
										ActivityId = o.ActivityId,
										Executor = o.ExecutorValue,
										ExecutorId = o.ExecutorId,
										IsMainLine = o.IsMainLine
									})
							.ToList();

					var activities = _db.Activities.Where(m => m.DocumentId == docId && !m.IsNotActive)
						.Select(
							o =>
								new TaskPathModel
								{
									ActivityId = o.Id,
									AuthorId = o.AuthorId,
									ParentId = o.ParentId,
									Executor = o.ExecutorsValue,
									ExecutorId = o.ExecutorsId,
									IsMainLine = o.IsMainLine
								})
						.ToList();
					List<TaskPathModel> tree = new List<TaskPathModel>();
					if (activities.Any())
					{
						var parent =
						activities.Where(m => m.ParentId == null)
						.Select(
							o =>
								new TaskPathModel {
									AuthorId = o.AuthorId,
									ActivityId = o.ActivityId,
									ParentId = o.ParentId,
									ExecutorId = o.ExecutorId,
									IsMainLine = o.IsMainLine
								})
						.First();
						var result = BuiltTree(tree, tasks, activities, parent);
						SetRank(activities, result);
						return PartialView(result);
					}
			
				}
				return PartialView();
			}

			public int GetIndex(List<TaskPathModel> list,TaskPathModel emp)
			{
				int index = 0;
				foreach (var item in list)
				{
					if (item.ActivityId == emp.ActivityId && emp.ExecutorId.Contains(item.ExecutorId))
					{
						return index;
					}
					index++;
				}
				return index;
			}

			public List<TaskPathModel> BuiltTree(List<TaskPathModel> tree, List<TaskPathModel> tasks, List<TaskPathModel> activities, TaskPathModel parent)
			{
				if (!tree.Any())
				{
					var employee =
						tasks.Where(m => m.ActivityId == parent.ActivityId && parent.ExecutorId.Contains(m.ExecutorId))
							.Select(o => new TaskPathModel { AuthorId = o.AuthorId, ActivityId = o.ActivityId, Executor = o.Executor, ExecutorId = o.ExecutorId,IsMainLine = o.IsMainLine})
							.ToList().OrderBy(m=>m.IsMainLine);
					foreach (var item in employee)
					{
						tree.Add(item);
					}

					BuiltTree(tree, tasks, activities, tree[0]);
				}
				else
				{
					int index = GetIndex(tree, parent);
					var employees =
						activities.Where(
							m => m.ParentId == parent.ActivityId && (parent.ExecutorId.Contains(m.AuthorId) || parent.AuthorId == m.AuthorId))
							.Select(
								o =>
									new TaskPathModel
									{
										IsMainLine = o.IsMainLine,
										AuthorId = o.AuthorId,
										ActivityId = o.ActivityId,
										Executor = o.Executor,
										ExecutorId = o.ExecutorId
									})
							.ToList();
					List<TaskPathModel> buf = new List<TaskPathModel>();
					foreach (var item in employees)
					{
						if (employees.Any())
						{
							buf = tasks.OrderBy(m => m.IsMainLine).Where(m => m.ActivityId == item.ActivityId)
								.Select(
									o =>
										new TaskPathModel
										{
											IsMainLine = o.IsMainLine,
											AuthorId = o.AuthorId,
											ActivityId = o.ActivityId,
											Executor = o.Executor,
											ParentId = o.ParentId,
											ExecutorId = o.ExecutorId
										})
								.ToList();
							;
							if (buf.Any())
							{
								foreach (var executor in buf) {
									tree.Insert(index + 1, executor);
								}
							}
							
						}
					}
					if (tree.Count != tasks.Count())
					{
							BuiltTree(tree, tasks, activities, tree[index+1]);
					}

				}
				return tree;
			}

			public List<TaskPathModel> SetRank(List<TaskPathModel>  activities, List<TaskPathModel> activity) {
				List<TaskPathModel> executors = new List<TaskPathModel>();
				foreach (var item in activity) {
					Guid? parentId = item.ActivityId;
					Guid? bufActivity = item.ActivityId;
					int count = 0;
					while (parentId != null) {
						parentId = activities.Where(m => m.ActivityId == bufActivity).Select(m => m.ParentId).FirstOrDefault();
						bufActivity = parentId;
						count++;
					}
					item.Rank = count;
				}
				return activity;
			}

			
	

			public ActionResult AssignmentPopup(Guid taskId) {

			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);

			TaskAction action = new TaskAction() {
				Id = taskId,
				ActionId = task.ActivityId,
				DocumenetNumber = task.Document.Number,
				DocumentId = task.DocumentId,

				Type = task.Type,
				State = task.State,
				ExecutionDate = task.Document.ExecutionDate,
				//ExecutionDate = Convert.ToDateTime(task.Document.ExecutionDate).ToString("dd.MM.yyyy"),
				//ExecutionDate = task.Document.ExecutionDate.Value.Date,

				DocumenetDate = task.Document.DocumentDate.HasValue ? task.Document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty

			};
			return PartialView(action);
		}

		public ActionResult AssignmentConfirm(TaskAction taskAction) {

			taskAction.ExecutorId = taskAction.ExecutorId.Select(x => new Item { Id = x.Id, Name = x.Name }).Distinct().ToList();
	
			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.Id);
			if ((task.Type == 1 || task.Type == 2) && (task.State == 0 || task.State == 1 || task.State == 4 || task.State == 3)) {
				Activity activity = new Activity {
					Id = Guid.NewGuid(),
					ParentTask = task.Id,
					DocumentId = task.DocumentId,
					AuthorId = task.ExecutorId,
					AuthorValue = task.ExecutorValue,
					ExecutorsId = DictionaryHelper.GetItemsId(taskAction.ExecutorId),
					ExecutorsValue = DictionaryHelper.GetItemsName(taskAction.ExecutorId),
					ResponsibleId = DictionaryHelper.GetItemId(taskAction.ExecutorId.FirstOrDefault()),
					ResponsibleValue = DictionaryHelper.GetItemName(taskAction.ExecutorId.FirstOrDefault()),
					ExecutionDate = taskAction.ExecutionDate,
					Type = 2,
					ParentId = task.ActivityId,

					Text = taskAction.Text,
					CreatedDate = DateTime.Now,

					IsMainLine = task.IsMainLine
				};
				_db.Activities.Add(activity);

				if (task.State != 1) {
					task.State = 1;
					task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;

				}
				_db.SaveChanges();
				return Json(
						new {
							State = true,
							Task =
								new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
						});
			} else {
				return Json(
						new {
							State = false
						});
			}
		}
		public void ExecuteProcedure(Guid taskId, string note) {
			try {
				object[] parameters = {
					new SqlParameter("@und", taskId),
					new SqlParameter("@n", note),
					new SqlParameter("@m", UserHelper.GetCurrentEmployee().ShortName)
				};
				_db.Database.ExecuteSqlCommand("EXEC CloseTask  @Id=@und , @Note=@n, @ModifiedUser=@m", parameters);
			} catch (Exception ex) { }
		}
		public ActionResult ExecutePopup(Guid taskId) {
			Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
			return PartialView(task);
		}

		public ActionResult ExecuteConfirm([DataSourceRequest] DataSourceRequest request, TaskListMobileModel product) {
			Guid taskId = product.TaskId;
			string note = product.Note;
			int typeReport = product.TypeReport;
			if (typeReport==0) 
			{
			product.PageCount=null;
			product.CopiesCount = null;
			 }
			int? pageCount = product.PageCount;
			int? copiesCount = product.CopiesCount;
			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);

			//if ((task.Type == 0 || task.Type == 1 || task.Type == 2) && ((task.State == 0 || task.State == 1 || task.State == 4 || task.State == 3) && (task.Document.MonitoringType != 3 && task.Document.MonitoringType != 4)))
			//{

			Report report = new Report() {
				Id = Guid.NewGuid(),
				TaskId = task.Id,
				ExecutionDate = DateTime.Now,
				Text = note,
				Type = typeReport,
				DocumentId = task.DocumentId,
				PageCount = pageCount,
				SymbolCount = copiesCount
			};
			task.State = 2;
			_db.Reports.Add(report);
			_db.SaveChanges();
			ExecuteProcedure(task.Id, report.Text);

			return
				Json(
					new {
						State = true,
						Task =
							new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
					});
			//}
			//else
			//{
			//	return
			//		Json(
			//			new {
			//				State = false
			//			});
			//}
		}

		public ActionResult AgreementPopup(Guid taskId) {
			Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
			return PartialView(task);
		}


		public ActionResult AgreementConfirm([DataSourceRequest] DataSourceRequest request, TaskListMobileModel product) {
			Guid taskId = product.TaskId;
			string note = product.Note;
			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
			if (task.Type == 3 && (task.State == 1 || task.State == 4 || task.State == 0)) {
				var dictionaries = _db.Dictionaries.Where(o => o.Type == "Nomenclature" && o.IsGuide).ToList().Select(o => o.Id.ToString());
				if (!_db.Activities.Any(o => o.Branch == 0 && o.IsNotActive && o.DocumentId == task.DocumentId) &&
					(task.Document.ProjectType == 1 || task.Document.ProjectType == 3 || task.Document.ProjectType == 6 || dictionaries.Contains(task.Document.NomenclatureDictionaryId))
					) {
					Activity parentActivity = _db.Activities.FirstOrDefault(x => x.DocumentId == task.DocumentId && x.Branch == 0 && !_db.Activities.Where(o => o.DocumentId == task.DocumentId && o.Branch == 0).Select(c => c.ParentId).Contains(x.Id));

					Activity activity = new Activity {
						Id = Guid.NewGuid(),
						//	ParentTask = task.Id,
						DocumentId = task.DocumentId,
						AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
						AuthorValue = UserHelper.GetCurrentEmployee().DisplayName.ToString(),
						ExecutorsId = task.Document.OwnerId,
						ExecutorsValue = task.Document.OwnerValue,
						//ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
						//ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
						Type = 4,
						IsParrent = parentActivity == null,
						CreatedDate = DateTime.Now,
						ParentId = parentActivity == null ? (Guid?)null : parentActivity.Id,
						ExecutionDate = task.Document.ExecutionDate,
						Text = task.Document.ProjectType == 1 ? "На регистрацию в канцелярию" : "На регистрацию",
						IsNotActive = true,
						//IsMainLine = task.IsMainLine
					};
					_db.Activities.Add(activity);
					_db.SaveChanges();

				}

				Report report = new Report() {
					Id = Guid.NewGuid(),
					TaskId = task.Id,
					ExecutionDate = DateTime.Now,
					Type = task.TypeEx == 1 ? 1 : 0,
					Text = note,
					//PageCount = pageCount,
					//SymbolCount = copiesCount,
					DocumentId = task.DocumentId
				};
				task.State = 2;
				task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;
				task.IsNotification = false;
				task.NotificationCount = 0;
				_db.Reports.Add(report);

				if (!_db.Activities.Any(o => o.Branch == 0 && o.IsNotActive && o.DocumentId == task.DocumentId)) {
					Document project = _db.Documents.Find(task.DocumentId);
					Document document = DocumentManager.Clone(project);
					document.Id = Guid.NewGuid();
					if (project.ProjectType == 2 || project.ProjectType == 5) {
						DocumentManager.ConvertInCorrespondentDocument(document, project, UserHelper.GetCurrentEmployee(),
							UserHelper.GetCurrentEmployee().DisplayName);
					} else {
						if (project.ProjectType == 4 || project.ProjectType == 3) {
							DocumentManager.ConvertInAdminDocument(document, project, UserHelper.GetCurrentEmployee(),
							UserHelper.GetCurrentEmployee().DisplayName);
						}
					}

					project.StateType = 9;
					project.ModifiedDate = DateTime.Now;

					//	_db.SaveChanges();
				}
				_db.SaveChanges();
				return
					Json(
						new {
							State = true,
							Task =
								new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
						});
			} else {
				return
					Json(
						new {
							State = false
						});
			}
		}
		public ActionResult RejectPopup(Guid taskId) {
			Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
			return PartialView(task);
		}
		/// <summary>
		/// Отказать подтверждение
		/// </summary>
		/// <param name="taskId"></param>
		/// <param name="note"></param>
		/// <returns></returns>
		//public ActionResult RejectConfirm(Guid taskId, string note) {
		public ActionResult RejectConfirm([DataSourceRequest] DataSourceRequest request, TaskListMobileModel product) {
			Guid taskId = product.TaskId;
			string note = product.Note;
			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
			if ((task.Type == 3 || task.Type == 4) && (task.State == 0 ||task.State == 1 || task.State == 4)) {
			if (task.State != 2) {
				Report report = new Report() {
					Id = Guid.NewGuid(),
					TaskId = task.Id,
					ExecutionDate = DateTime.Now,
					Text = note,
					Type = 0,
					DocumentId = task.DocumentId
				};
				task.State = 3;
				_db.Reports.Add(report);
				_db.SaveChanges();
			}
			return
				Json(
					new {
						State = true,
						Task =
							new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
					});
			} else {
				return
					Json(
						new {
							State = false
						});
			}
		}
		public ActionResult WorkToMovePopup(Guid taskId) {
			//aaa
			Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
			return PartialView(task);
		}
		public ActionResult WorkToMoveConfirm([DataSourceRequest] DataSourceRequest request, TaskListMobileModel product) {
			Guid taskId = product.TaskId;
			string note = product.Note;
			int? pageCount = product.PageCount;
			int? copiesCount = product.CopiesCount;
			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
			if (task.Type == 3 && (task.State == 0 || task.State == 1 || task.State == 4)) {
				var dictionaries = _db.Dictionaries.Where(o => o.Type == "Nomenclature" && o.IsGuide).ToList().Select(o => o.Id.ToString());
				if (!_db.Activities.Any(o => o.Branch == 0 && o.IsNotActive && o.DocumentId == task.DocumentId) &&
					(task.Document.ProjectType == 1 || task.Document.ProjectType == 3 || task.Document.ProjectType == 6 || dictionaries.Contains(task.Document.NomenclatureDictionaryId))
					) {
					Activity parentActivity = _db.Activities.FirstOrDefault(x => x.DocumentId == task.DocumentId && x.Branch == 0 && !_db.Activities.Where(o => o.DocumentId == task.DocumentId && o.Branch == 0).Select(c => c.ParentId).Contains(x.Id));

					Activity activity = new Activity {
						Id = Guid.NewGuid(),
						//	ParentTask = task.Id,
						DocumentId = task.DocumentId,
						AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
						AuthorValue = UserHelper.GetCurrentEmployee().DisplayName.ToString(),
						ExecutorsId = task.Document.OwnerId,
						ExecutorsValue = task.Document.OwnerValue,
						//ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
						//ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
						Type = 4,
						IsParrent = parentActivity == null,
						CreatedDate = DateTime.Now,
						ParentId = parentActivity == null ? (Guid?)null : parentActivity.Id,
						ExecutionDate = task.Document.ExecutionDate,
						Text = task.Document.ProjectType == 1 ? "На регистрацию в канцелярию" : "На регистрацию",
						IsNotActive = true,
						//IsMainLine = task.IsMainLine
					};
					_db.Activities.Add(activity);
					_db.SaveChanges();

				}

				Report report = new Report() {
					Id = Guid.NewGuid(),
					TaskId = task.Id,
					ExecutionDate = DateTime.Now,
					Type = task.TypeEx == 1 ? 1 : 0,
					Text = note,
					PageCount = pageCount,
					SymbolCount = copiesCount,
					DocumentId = task.DocumentId
				};
				task.State = 2;
				task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;
				task.IsNotification = false;
				task.NotificationCount = 0;
				_db.Reports.Add(report);

				if (!_db.Activities.Any(o => o.Branch == 0 && o.IsNotActive && o.DocumentId == task.DocumentId)) {
					Document project = _db.Documents.Find(task.DocumentId);
					Document document = DocumentManager.Clone(project);
					document.Id = Guid.NewGuid();
					if (project.ProjectType == 2 || project.ProjectType == 5) {
						DocumentManager.ConvertInCorrespondentDocument(document, project, UserHelper.GetCurrentEmployee(),
							UserHelper.GetCurrentEmployee().DisplayName);
					} else {
						if (project.ProjectType == 4 || project.ProjectType == 3) {
							DocumentManager.ConvertInAdminDocument(document, project, UserHelper.GetCurrentEmployee(),
							UserHelper.GetCurrentEmployee().DisplayName);
						}
					}

					project.StateType = 9;
					project.ModifiedDate = DateTime.Now;

					//	_db.SaveChanges();
				}
				_db.SaveChanges();
				return
					Json(
						new {
							State = true,
							Task =
								new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
						});
			} else {
				return
					Json(
						new {
							State = false
						});
			}
		}

		public ActionResult SigningPopup(Guid taskId) {
			Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
			return PartialView(task);
		}
		public ActionResult SigningConfirm([DataSourceRequest] DataSourceRequest request, TaskListMobileModel product) {
			Guid taskId = product.TaskId;
			string note = product.Note;
			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
			Document project = _db.Documents.First(o => o.Id == task.DocumentId);
			if (task.Type == 4 && (task.State == 0 || task.State == 1 || task.State == 4)) {

				Report report = new Report() {
					Id = Guid.NewGuid(),
					TaskId = task.Id,
					ExecutionDate = DateTime.Now,
					Text = note,
					Type = 0,
					DocumentId = project.Id
				};
				task.State = 2;
				_db.Reports.Add(report);

				Document document = DocumentManager.Clone(project);
				document.Id = Guid.NewGuid();
				if (project.ProjectType == 1)
					DocumentManager.ConvertInOutgouingDocument(document, project, UserHelper.GetCurrentEmployee(),
						UserHelper.GetCurrentEmployee().DisplayName);

				else {
					if (project.ProjectType == 4 || project.ProjectType == 3 || project.ProjectType == 6) {
						DocumentManager.ConvertInAdminDocument(document, project, UserHelper.GetCurrentEmployee(),
						UserHelper.GetCurrentEmployee().DisplayName);
					} else
						DocumentManager.ConvertInCorrespondentDocument(document, project, UserHelper.GetCurrentEmployee(),
							UserHelper.GetCurrentEmployee().DisplayName);
				}
				project.StateType = 9;
				project.ModifiedDate = DateTime.Now;

				_db.SaveChanges();

				return
					Json(
						new {
							State = true,
							Task =
								new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
						});
			}
			return
				Json(
					new {
						State = false
					});
		}
		public ActionResult TranslatePopup(Guid taskId) {
			Task currentTask = _db.Tasks.Include("Document").FirstOrDefault(o => o.Id == taskId);
			TaskAction taskAction = new TaskAction {
				Id = Guid.NewGuid(),
				ActionId = taskId,
				ExecutionDate = currentTask.Document.ExecutionDate,
				DocumentId = currentTask.Document.Id,
				DocumenetNumber = currentTask.Document.Number,
				DocumenetDate = currentTask.Document.DocumentDate.Value.ToString("dd.MM.yyyy")
			};
			return PartialView(taskAction);
		}

		/// <summary>
		/// Перевод подтвердить
		/// </summary>
		/// <param name="taskAction"></param>
		/// <returns></returns>
		public ActionResult TranslateConfirm([DataSourceRequest] DataSourceRequest request, TaskAction taskAction) {
			Task currentTask = _db.Tasks.FirstOrDefault(o => o.Id == taskAction.ActionId);
			currentTask.State = 2;

			Report report = new Report() {
				Id = Guid.NewGuid(),
				TaskId = currentTask.Id,
				ExecutionDate = DateTime.Now,
				Type = 0,
				Text = "Документ отправлен на перевод: " + DictionaryHelper.GetItemsName(taskAction.ExecutorId),
				DocumentId = currentTask.DocumentId
			};

			Activity activity = new Activity {
				Id = Guid.NewGuid(),
				CreatedDate = DateTime.Now,
				Type = 3,
				IsParrent = false,
				IsNotActive = false,
				AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
				AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,

				ExecutorsId = DictionaryHelper.GetItemsId(taskAction.ExecutorId),
				ExecutorsValue = DictionaryHelper.GetItemsName(taskAction.ExecutorId),
				Text = taskAction.Text,

				DocumentId = currentTask.DocumentId,
				ExecutionDate = currentTask.ExecutionDate,
				ParentId = currentTask.ActivityId,
				TypeEx = 1
			};
			Activity activitySign = new Activity {
				Id = Guid.NewGuid(),
				CreatedDate = DateTime.Now,
				Type = 4,
				IsParrent = false,
				IsNotActive = true,
				AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
				AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,

				ExecutorsId = UserHelper.GetCurrentEmployee().Id.ToString(),
				ExecutorsValue = UserHelper.GetCurrentEmployee().DisplayName,
				Text = "На регистрацию в канцелярию",

				DocumentId = currentTask.DocumentId,
				ExecutionDate = currentTask.ExecutionDate,
				ParentId = activity.Id
			};
			_db.Reports.Add(report);
			_db.Activities.Add(activity);
			_db.Activities.Add(activitySign);
			_db.SaveChanges();

			return Json(new { State = true });
		}

		public ActionResult ResolutionPopup(Guid taskId) {
			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
			TaskAction action = new TaskAction() {
				Id = taskId,
				ActionId = task.ActivityId,
				DocumenetNumber = task.Document.Number,
				DocumentId = task.DocumentId,
				Type = task.Type,
				State = task.State,
				ExecutionDate = task.Document.ExecutionDate,
				DocumenetDate = task.Document.DocumentDate.HasValue ? task.Document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty
			};
			return PartialView(action);
		}

		/// <summary>
		/// Резалюция
		/// </summary>
		/// <param name="taskAction"></param>
		/// <returns></returns>
		public ActionResult ResolutionConfirm(TaskAction taskAction) {
			taskAction.ExecutorId = taskAction.ExecutorId.Select(x => new Item { Id = x.Id, Name = x.Name }).Distinct().ToList();
			Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.Id);
			if (task.Type == 0 && (task.State == 0 || task.State == 4)) {
				Document document = _db.Documents.FirstOrDefault(o => o.Id == task.DocumentId);
				document.ResolutionValue = task.Text;
				Activity activity = new Activity {
					Id = Guid.NewGuid(),
					ParentTask = task.Id,
					DocumentId = task.DocumentId,
					AuthorId = task.ExecutorId,
					AuthorValue = task.ExecutorValue,
					ExecutorsId = DictionaryHelper.GetItemsId(taskAction.ExecutorId),
					ExecutorsValue = DictionaryHelper.GetItemsName(taskAction.ExecutorId),
					ResponsibleId = DictionaryHelper.GetItemId(taskAction.ExecutorId.FirstOrDefault()),
					ResponsibleValue = DictionaryHelper.GetItemName(taskAction.ExecutorId.FirstOrDefault()),
					ExecutionDate = task.Document.ExecutionDate,
					Type = 1,
					CreatedDate = DateTime.Now,
					ParentId = task.ActivityId,
					Text = taskAction.Text,

					IsMainLine = task.IsMainLine
				};
				_db.Activities.Add(activity);

				if (task.State != 1) {
					task.State = 1;
					task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;

				}
				if (document.StateType != 3) {
					document.StateType = 3;
					document.ResolutionId = activity.Id;
					document.ResolutionValue = activity.Text;

				}
				_db.SaveChanges();
				return Json(
					new {
						State = true,
						Task =
							new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
					});
			} else {
				return Json(
					new {
						State = false
					});
			}
		}

		[HttpGet]
		public ActionResult EditDetails(Guid productID) {
			var x = _db.Tasks.FirstOrDefault(p => p.Id == productID);
			var target = new TaskListItemModel {
				Author = x.AuthorValue,
				CreatedDate = x.CreatedDate,
				DocumentDate = x.Document.AutoDocumentDate,
				DocumentId = x.Document.Id,
				DocumentNumber = x.Document.Number,
				ProjectType = x.Document.ProjectType,
				DocumentMonitoringType = x.Document.MonitoringType,
				ExecutionDate = x.AutoExecutionDate,
				Executor = x.ExecutorValue,
				IsResponsible = x.FunctionType == 1,
				Number = x.Number,
				State = x.State,
				TaskId = x.Id,
				Text = x.Document.ResolutionValue,
				Type = x.Type.Value,
				TypeEx = x.TypeEx,
				ResponsibleValue = x.Document.ResponsibleValue,
				CorrespondentsValue = x.Document.CorrespondentsValue,
				OutgoingNumber = x.Document.OutgoingNumber,
				OutgoingDate = x.Document.AutoOutgoingDate,
				DestinationValue = x.Document.DestinationValue
			};
			return View(target);
		}




		public ActionResult GetFiles(string taskId) {
            //Task taskfile = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
            //var files = UploadHelper.GetFilesInfo(taskfile.DocumentId.ToString(), false);
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == new Guid(taskId));
            Document document = _db.Documents.FirstOrDefault(o => o.Id == task.DocumentId);
            var files = UploadHelper.GetFilesInfo(document.AttachPath.ToString(), false);
            ViewBag.Files = files;
			return PartialView();
		}

		public ActionResult GetProperty(Guid taskId) {
			Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
			int state = (int)task.State;
			int type = (int)task.Type;
			int typeEx = (int)task.TypeEx;
			var docId = task.DocumentId;
			int monitoringType = (int)_db.Documents.Where(o => o.Id == docId).Select(o => o.MonitoringType).FirstOrDefault();
			int stateType = (int)_db.Documents.Where(o => o.Id == docId).Select(o => o.StateType).FirstOrDefault();

			return Json(new { State = state, Type = type, TypeEx = typeEx, MonitoringType = monitoringType, StateType = stateType });
		}

		public ActionResult DocumentDetails(Guid id) {
			Task task = _db.Tasks.FirstOrDefault(x => x.Id == id);
			if (task != null) {
				if (task.State == 0) {
					task.State = 4;
					task.DateOfOperation = DateTime.Now;
					_db.SaveChanges();
				}
				Document document = _db.Documents.FirstOrDefault(x => x.Id == task.DocumentId);
				
				 if (document != null) {
					switch (document.DocumentType) {
						case 0:
							return PartialView("DocFormInc", document); //проверить
						case 1:
							return PartialView("DocFormOut", document);
						case 2:
							return PartialView("DocFormCit", document);
						case 3:
							switch (document.ProjectType) {
								case 3:
									return PartialView("DocFormAdm", document);
								case 4:
									return PartialView("DocFormPrt", document);
								case 6:
									return PartialView("DocFormAdmMain", document);
							}
							return PartialView(null);

						case 4:
							switch (document.ProjectType) {
								case 1:
									return PartialView("DocFormPrjOut", document);
								case 2:
									return PartialView("DocFormPrjCor", document);
								case 3:
									return PartialView("DocFormPrjAdm", document);
								case 4:
									return PartialView("DocFormPrjPrt", document);
								case 5:
									return PartialView("DocFormPrjCorInit", document);
								case 6:
									return PartialView("DocFormPrjAdmMain", document);
							}
							return PartialView(null);
						case 5:
							switch (document.ProjectType) {
								case 2:
									return PartialView("DocFormCor", document);
								case 5:
									return PartialView("DocFormCorInit", document);
							}
							return PartialView(null);
                        case 6: return PartialView("DocFormContract", document);
                    }
                }
			}
			return PartialView(null);
		}
	}
}