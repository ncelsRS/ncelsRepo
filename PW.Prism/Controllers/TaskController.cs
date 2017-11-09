using System;
using System.Collections.Generic;
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
using Ncels.Helpers;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Enums;
using PW.Ncels.Database.Enums.PriceProject;
using PW.Ncels.Database.Notifications;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Document = PW.Ncels.Database.DataModel.Document;


namespace PW.Prism.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ncelsEntities _db = UserHelper.GetCn();
        private readonly NotificationManager _notificationManager = new NotificationManager();

        // GET: /Task/
        public ActionResult Index(string type)
        {
            Guid guid = Guid.NewGuid();
            if (type == "RejectTaskListDoc")
            {
                ViewBag.Type = 2;
            }
            if (type == "ExcludeTaskListDoc")
            {
                ViewBag.Type = 1;
            }
            if (type == "TaskListDoc")
            {
                ViewBag.Type = 3;
            }
            if (type == "NewTaskListDoc")
            {
                ViewBag.Type = 0;
            }

            if (type == "New")
            {
                ViewBag.Type = 7;
            }
            if (type == "InWork")
            {
                ViewBag.Type = 4;
            }
            if (type == "Expired")
            {
                ViewBag.Type = 5;
            }
            if (type == "Done")
            {
                ViewBag.Type = 6;
            }

            return PartialView(guid);
        }


        public ActionResult IndexStage1()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }
        public ActionResult IndexStage2()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }
        public ActionResult IndexStage3()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }
        public ActionResult IndexStage4()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }
        public ActionResult IndexStage5()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }
        public ActionResult IndexAgreement()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }
        public ActionResult IndexNotify()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }
        public ActionResult IndexStage6()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }

        public ActionResult IndexStage7()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }

        public ActionResult IndexStage8()
        {
            Guid guid = Guid.NewGuid();

            return PartialView(guid);
        }

        public ActionResult ReportList()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.ReportList = ReportHelper.GetListExpertise();

            return PartialView(guid);
        }
        public FileStreamResult ExportFile()
        {

            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/TaskList.mrt"));
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }

            if (report.Dictionary.Variables.Contains("EmployeeId"))
            {
                report.Dictionary.Variables["EmployeeId"].Value = UserHelper.GetCurrentEmployee().Id.ToString();
            }
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;
            return File(stream, "application/excel", "Задания.xls");
        }

        public FileContentResult DownloadAllFile(string id)
        {
            string pathName = Path.Combine(FileHelper.PathRoot, id) + ".zip";
            try
            {
                var doc = _db.Documents.Find(new Guid(id));
                var files = UploadHelper.GetFilesInfo(doc.AttachPath, false);
                //	Stream stream = System.IO.File.Create(pathName + ".zip");
                using (ZipArchive a = ZipFile.Open(pathName, ZipArchiveMode.Create))
                {

                    foreach (var item in files)
                    {
                        string fileNAme = Path.Combine(FileHelper.PathRoot, FileHelper.Root, doc.AttachPath, item.name);
                        a.CreateEntryFromFile(fileNAme, item.name, CompressionLevel.Optimal);
                    }
                }

                //stream.Position = 0;
                //	stream.Flush();
                byte[] bytes;
                using (Stream stream = System.IO.File.OpenRead(pathName))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        stream.CopyTo(memoryStream);
                        bytes = memoryStream.ToArray();
                    }

                }
                return File(bytes, "application/zip", "Документы" + DateTime.Now.ToString("_dd_MM_yyyy_hh_mm") + ".zip");
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                System.IO.File.Delete(pathName);
            }

        }

        // GET: /Task/Details/5
        public ActionResult Details(Guid id)
        {
            ViewBag.IsToolbarAllow = !_db.Documents.Any(m => m.Id == id) || DocumentHelper.IsToolBarAllow(_db, id);
            return PartialView(id);
        }

        public ActionResult DocumentDetails(Guid id)
        {
            Task task = _db.Tasks.FirstOrDefault(x => x.Id == id);
            if (task != null)
            {
                if (task.State == 0)
                {
                    task.State = 4;
                    task.DateOfOperation = DateTime.Now;
                    _db.SaveChanges();
                }
                Document document = _db.Documents.FirstOrDefault(x => x.Id == task.DocumentId);
                if (document != null)
                {
                    switch (document.DocumentType)
                    {
                        case 0:
                            return PartialView("DocFormInc", document);
                        case 1:
                            return PartialView("DocFormOut", document);
                        case 2:
                            return PartialView("DocFormCit", document);
                        case 3:
                            switch (document.ProjectType)
                            {
                                case 3:
                                    return PartialView("DocFormAdm", document);
                                case 4:
                                    return PartialView("DocFormPrt", document);
                                case 6:
                                    return PartialView("DocFormAdmMain", document);
                            }
                            return PartialView(null);

                        case 4:
                            switch (document.ProjectType)
                            {
                                case 1:
                                    return PartialView("DocFormPrjOut", document);
                                case 2:
                                    return PartialView("DocFormPrjCor", document);
                                case 3:
                                    return PartialView("DocFormPrjAdm", document);
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                case 22:
                                case 4:
                                    return PartialView("DocFormPrjPrt", document);
                                case 5:
                                    return PartialView("DocFormPrjCorInit", document);
                                case 6:
                                    return PartialView("DocFormPrjAdmMain", document);
                            }
                            return PartialView(null);
                        case 5:
                            switch (document.ProjectType)
                            {
                                case 10:
                                case 11:
                                case 12:
                                case 13:
                                case 14:
                                case 15:
                                case 16:
                                case 17:
                                case 18:
                                case 19:
                                case 20:
                                case 21:
                                case 22:
                                case 2:
                                    return PartialView("DocFormCor", document);
                                case 5:
                                    return PartialView("DocFormCorInit", document);
                            }
                            return PartialView(null);
                        case 6:
                            return PartialView("DocFormContract", document);
                    }
                }
            }
            return PartialView(null);
        }

        public ActionResult GetTasks([DataSourceRequest] DataSourceRequest request, int type)
        {
            IEnumerable<Guid> employeeList = new List<Guid>();
            Employee currentEmployee = UserHelper.GetCurrentEmployee();
            if (currentEmployee != null)
            {
                employeeList = employeeList.Union(new[] { currentEmployee.Id });
                employeeList = employeeList.Union(UserHelper.GetDeputy(currentEmployee.Id.ToString()));
                employeeList = employeeList.Union(UserHelper.GetAssistants(currentEmployee.Id.ToString()));
            }
            IQueryable<Task> queryable = _db.Tasks.Include("Document.Template")
                .Where(o => !o.Document.IsDeleted)
                .Where(x => x.AccessTasks.Count(o => employeeList.Contains(o.UserId)) > 0);


            switch (type)
            {
                //новые и в работе
                case 0:
                    queryable = queryable.Where(o => o.IsActive && o.State == 0 || o.State == 4);
                    break;
                //исполненные ; новые и в работе
                case 1:
                    queryable = queryable.Where(o => o.IsActive && o.State != 2 && o.State != 3);//.Where(o => o.State == 2 || o.State == 3);
                    break;
                //отозванные; исполненные
                case 2:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 2 || o.State == 3));// o.State == 3);
                    break;
                //новые
                case 7:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 0));
                    break;
                //в работе
                case 4:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 1 || o.State == 4));
                    break;
                //просрочено
                case 5:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 0 || o.State == 1 || o.State == 4) && (o.ExecutionDate < DateTime.Now));
                    break;
                //Исполненные
                case 6:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 2 || o.State == 3));
                    break;
                //новые и в работе ; все
                default:
                    queryable = queryable.Where(o => o.IsActive);// && o.State != 2 && o.State != 3);
                    break;
            }


            bool isAddBp = EmployePermissionHelper.IsAddBp;
            var tasks = queryable
                .Select(x => new TaskListItemModel
                {
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
                    Type = x.Type ?? -10,
                    TypeEx = x.TypeEx,
                    IsAddBp = isAddBp,
                    ResponsibleValue = x.Document.ResponsibleValue,
                    CorrespondentsValue = x.Document.CorrespondentsValue,
                    OutgoingNumber = x.Document.OutgoingNumber,
                    OutgoingDate = x.Document.AutoOutgoingDate,
                    DestinationValue = x.Document.DestinationValue,
                    Summary = x.Document.Summary,
                   //ProxyOrganizationName = DocumentHelper.GetProxyOrganizationName(_db, x.Document.Id)
                });
            var response = new List<TaskListItemModel>();
            foreach (var taskListItemModel in tasks) {
                taskListItemModel.ProxyOrganizationName = DocumentHelper.GetProxyOrganizationName(_db,
                    taskListItemModel.DocumentId);
                response.Add(taskListItemModel);
            }
            DataSourceResult result = response.ToDataSourceResult(request);
            return Json(result);
        }
        public ActionResult GetTasksStage1([DataSourceRequest] DataSourceRequest request)
        {
            return GetTaskStage(request, -1, 1);
        }
        public ActionResult GetTasksStage2([DataSourceRequest] DataSourceRequest request)
        {
            return GetTaskStage(request, -1, 2);
        }
        public ActionResult GetTasksStage3([DataSourceRequest] DataSourceRequest request)
        {
            return GetTaskStage(request, -1, 3);
        }
        public ActionResult GetTasksStage4([DataSourceRequest] DataSourceRequest request)
        {
            return GetTaskStage(request, -1, 4);
        }
        public ActionResult GetTasksStage5([DataSourceRequest] DataSourceRequest request)
        {
            return GetTaskStage(request, -1, 5);
        }
        public ActionResult GetTasksStage6([DataSourceRequest] DataSourceRequest request)
        {
            return GetTaskStage(request, -1, 6);
        }
        public ActionResult GetTasksStage7([DataSourceRequest] DataSourceRequest request)
        {
            return GetTaskStage(request, -1, 7);
        }
        public ActionResult GetTasksStage8([DataSourceRequest] DataSourceRequest request)
        {
            return GetTaskStage(request, 2, 8);
        }

        public ActionResult GetTasksAgreement([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Guid> employeeList = new List<Guid>();
            Employee currentEmployee = UserHelper.GetCurrentEmployee();
            if (currentEmployee != null)
            {
                employeeList = employeeList.Union(new[] { currentEmployee.Id });
                employeeList = employeeList.Union(UserHelper.GetDeputy(currentEmployee.Id.ToString()));
                employeeList = employeeList.Union(UserHelper.GetAssistants(currentEmployee.Id.ToString()));
            }
            IQueryable<Task> queryable = _db.Tasks.Include("Document.Template")
                .Where(o => !o.Document.IsDeleted)
                .Where(x => x.AccessTasks.Count(o => employeeList.Contains(o.UserId)) > 0);

            queryable = queryable.Where(o => o.IsActive && (o.Type == 3 || o.Type == 4));




            bool isAddBp = EmployePermissionHelper.IsAddBp;
            var tasks = queryable
                .Select(x => new TaskListItemModel
                {
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
                    IsAddBp = isAddBp,
                    ResponsibleValue = x.Document.ResponsibleValue,
                    CorrespondentsValue = x.Document.CorrespondentsValue,
                    OutgoingNumber = x.Document.OutgoingNumber,
                    OutgoingDate = x.Document.AutoOutgoingDate,
                    DestinationValue = x.Document.DestinationValue,
                    Summary = x.Document.Summary
                });
            DataSourceResult result = tasks.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult GetTasksNotify([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<Guid> employeeList = new List<Guid>();
            Employee currentEmployee = UserHelper.GetCurrentEmployee();
            if (currentEmployee != null)
            {
                employeeList = employeeList.Union(new[] { currentEmployee.Id });
                employeeList = employeeList.Union(UserHelper.GetDeputy(currentEmployee.Id.ToString()));
                employeeList = employeeList.Union(UserHelper.GetAssistants(currentEmployee.Id.ToString()));
            }

            //костыль: непонятно что за условие o.Document.DocumentType == 5 && o.Document.ProjectType > 9, поэтому его временно убираю 
            //todo: разобраться как работает
            // предыдущий вариант:
            //IQueryable<Task> queryable = _db.Tasks.Include("Document.Template")
            //    .Where(o => !o.Document.IsDeleted && o.Document.DocumentType == 5 && o.Document.ProjectType > 9)
            //    .Where(x => x.AccessTasks.Count(o => employeeList.Contains(o.UserId)) > 0);

            IQueryable<Task> queryable = _db.Tasks.Include("Document.Template")
                .Where(o => !o.Document.IsDeleted)
                .Where(x => x.AccessTasks.Count(o => employeeList.Contains(o.UserId)) > 0);
            //конец костыля

            bool isAddBp = EmployePermissionHelper.IsAddBp;
            var tasks = queryable
                .Select(x => new TaskListItemModel
                {
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
                    IsAddBp = isAddBp,
                    ResponsibleValue = x.Document.ResponsibleValue,
                    CorrespondentsValue = x.Document.CorrespondentsValue,
                    OutgoingNumber = x.Document.OutgoingNumber,
                    OutgoingDate = x.Document.AutoOutgoingDate,
                    DestinationValue = x.Document.DestinationValue,
                    Summary = x.Document.Summary
                });
            DataSourceResult result = tasks.ToDataSourceResult(request);
            return Json(result);
        }

        private ActionResult GetTaskStage(DataSourceRequest request, int type, int stage)
        {
            IEnumerable<Guid> employeeList = new List<Guid>();
            Employee currentEmployee = UserHelper.GetCurrentEmployee();
            if (currentEmployee != null)
            {
                employeeList = employeeList.Union(new[] { currentEmployee.Id });
                employeeList = employeeList.Union(UserHelper.GetDeputy(currentEmployee.Id.ToString()));
                employeeList = employeeList.Union(UserHelper.GetAssistants(currentEmployee.Id.ToString()));
            }
            IQueryable<Task> queryable = _db.Tasks.Include("Document.Template")
                .Where(o => !o.Document.IsDeleted)
                .Where(x => x.AccessTasks.Count(o => employeeList.Contains(o.UserId)) > 0);

            queryable = queryable.Where(o => o.Stage == stage);

            switch (type)
            {
                //новые и в работе
                case 0:
                    queryable = queryable.Where(o => o.IsActive && o.State == 0 || o.State == 4);
                    break;
                //исполненные ; новые и в работе
                case 1:
                    queryable = queryable.Where(o => o.IsActive && o.State != 2 && o.State != 3);
                    //.Where(o => o.State == 2 || o.State == 3);
                    break;
                //отозванные; исполненные
                case 2:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 2 || o.State == 3)); // o.State == 3);
                    break;
                //новые
                case 7:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 0));
                    break;
                //в работе
                case 4:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 1 || o.State == 4));
                    break;
                //просрочено
                case 5:
                    queryable =
                        queryable.Where(
                            o =>
                                o.IsActive && (o.State == 0 || o.State == 1 || o.State == 4) && (o.ExecutionDate < DateTime.Now));
                    break;
                //Исполненные
                case 6:
                    queryable = queryable.Where(o => o.IsActive && (o.State == 2 || o.State == 3));
                    break;
                //новые и в работе ; все
                default:
                    queryable = queryable.Where(o => o.IsActive); // && o.State != 2 && o.State != 3);
                    break;
            }


            bool isAddBp = EmployePermissionHelper.IsAddBp;
            var tasks = queryable
                   .Join(_db.RegisterProjectJournals, t => t.DocumentId.Value, rp => rp.Id, (t, rp) => new { t, rp })
                .Select(x => new TaskListItemModel
                {
                    Author = x.t.AuthorValue,
                    CreatedDate = x.t.CreatedDate,
                    DocumentDate = x.t.Document.AutoDocumentDate,
                    DocumentId = x.t.Document.Id,
                    DocumentNumber = x.t.Document.Number,
                    ProjectType = x.t.Document.ProjectType,
                    DocumentMonitoringType = x.t.Document.MonitoringType,
                    ExecutionDate = x.t.AutoExecutionDate,
                    Executor = x.t.ExecutorValue,
                    IsResponsible = x.rp.IsPayed.HasValue ? x.rp.IsPayed.Value : false,
                    Number = x.t.Number,
                    State = x.t.State,
                    TaskId = x.t.Id,
                    Text = x.t.Document.ResolutionValue,
                    Type = x.t.Type.Value,
                    TypeEx = x.t.TypeEx,
                    IsAddBp = isAddBp,
                    ResponsibleValue = x.t.Document.ResponsibleValue,
                    CorrespondentsValue = x.t.Document.CorrespondentsValue,
                    OutgoingNumber = x.t.Document.OutgoingNumber,
                    OutgoingDate = x.t.Document.AutoOutgoingDate,
                    DestinationValue = x.t.Document.DestinationValue,
                    Summary = x.t.Document.Summary,
                    PriceSum = x.t.Document.PriceSum,
                    DocumentDictionaryTypeId = x.t.Document.DocumentDictionaryTypeId,
                    DocumentDictionaryTypeValue = x.t.Document.DocumentDictionaryTypeValue,
                    NameRu = x.rp.NameRu,
                    RegisterType = x.rp.RegisterType,
                    StatusValue = x.rp.StatusValue
                });
            DataSourceResult result = tasks.ToDataSourceResult(request);
            return Json(result);
        }

        private readonly List<TaskTreeItemModel> _tree = new List<TaskTreeItemModel>();

        public JsonResult Tasks(Guid? id)
        {

            Document document = _db.Documents.FirstOrDefault(x => x.Id == id);
            IList<Activity> activities;
            IList<Task> tasks;
            IList<Report> reports;
            if (document != null && document.DocumentType == 4)
            {
                activities = _db.Activities.Where(x => x.DocumentId.Value == id && x.IsCurrent).ToList();
                tasks = _db.Tasks.Where(x => x.DocumentId.Value == id).ToList();
                reports = _db.Reports.Where(x => x.DocumentId.Value == id).ToList();
            }
            else
            {
                activities = _db.Activities.Where(x => x.DocumentId.Value == id).ToList();
                tasks = _db.Tasks.Where(x => x.DocumentId.Value == id).ToList();
                reports = _db.Reports.Where(x => x.DocumentId.Value == id).ToList();
            }
            BuildTaskTree(activities.Where(x => x.IsParrent), activities, tasks, reports, id);

            return Json(_tree, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BpTasks(Guid? id, Guid? taskId)
        {
            IList<Activity> activities = _db.Activities.Where(x => x.DocumentId.Value == id && x.Branch == 0).ToList();
            IList<Task> tasks = _db.Tasks.Where(x => x.DocumentId.Value == id).ToList();
            IList<Report> reports = _db.Reports.Where(x => x.DocumentId.Value == id).ToList();
            BuildTaskTree(activities.Where(x => x.IsParrent), activities, tasks, reports, taskId);

            return Json(_tree, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BpTasksMyTask(Guid? id, Guid? taskId)
        {
            IList<Activity> activities = _db.Activities.Where(x => x.DocumentId.Value == id && x.Branch == 0).ToList();
            IList<Task> tasks = _db.Tasks.Where(x => x.DocumentId.Value == id).ToList();
            IList<Report> reports = _db.Reports.Where(x => x.DocumentId.Value == id).ToList();
            BuildTaskTree(activities.Where(x => x.ParentTask == taskId), activities, tasks, reports, taskId);

            return Json(_tree, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BpsTasks(Guid? id)
        {
            IList<Activity> activities = _db.Activities.Where(x => x.DocumentId.Value == id && x.Branch != 0).ToList();
            IList<Task> tasks = _db.Tasks.Where(x => x.DocumentId.Value == id).ToList();
            IList<Report> reports = _db.Reports.Where(x => x.DocumentId.Value == id).ToList();
            Guid parent = new Guid();
            foreach (Activity activity in activities.OrderBy(x => x.Branch).ThenByDescending(x => x.IsParrent))
            {
                if (activity.IsParrent)
                {
                    parent = Guid.NewGuid();
                    _tree.Add(new TaskTreeItemModel
                    {
                        Type = -2,
                        Text = "Лист согласования от " + activity.CreatedDate.Value.ToString("dd.MM.yyyy HH:mm"),
                        Id = parent,
                        expanded = false
                    });
                }
                activity.ParentTask = parent;
            }
            BuildTaskTree(activities.Where(x => x.IsParrent).OrderBy(x => x.Branch), activities, tasks, reports, id);
            return Json(_tree, JsonRequestBehavior.AllowGet);
        }

        private void BuildTaskTree(IEnumerable<Activity> current, IList<Activity> activities, IList<Task> tasks, IList<Report> reports, Guid? id)
        {
            foreach (Activity activity in current)
            {
                TaskTreeItemModel item = Find(_tree, activity.ParentTask);
                if (item == null)
                    _tree.Add(ConvertActivity(activity, tasks, reports, id));
                else
                    item.Children.Add(ConvertActivity(activity, tasks, reports, id));

                current = activities.Where(x => x.ParentId == activity.Id);
                if (current.Count() != 0)
                    BuildTaskTree(current, activities, tasks, reports, id);
            }
        }

        private TaskTreeItemModel ConvertActivity(Activity activity, IEnumerable<Task> tasks, IEnumerable<Report> reports, Guid? id)
        {
            return new TaskTreeItemModel
            {
                Type = activity.Type == null ? 0 : activity.Type.Value,
                TypeEx = activity.TypeEx,
                Id = activity.Id,
                TaskId = id,

                IsAllowEdit = AllowEdit(activity, tasks),//.AuthorId == UserHelper.GetCurrentEmployee().Id.ToString(),
                CreateDateTime = activity.CreatedDate == null ? "" : activity.CreatedDate.Value.ToString("G"),
                ExecutionDateTime = activity.ExecutionDate == null ? "" : activity.ExecutionDate.Value.ToShortDateString(),
                Executor = (activity.Type == 3 || activity.Type == 4) ? activity.ExecutorsValue : activity.AuthorValue,
                IsMineLine = activity.IsMainLine,
                Text = activity.Text,
                Children = tasks.Where(x => x.ActivityId == activity.Id).OrderByDescending(x => x.FunctionType).ThenBy(x => x.Number).Select(x => ConvertTask(x, reports)).ToList(),
            };
        }


        public bool AllowEdit(Activity activity, IEnumerable<Task> tasks)
        {

            if (activity.Document.DocumentType == 0 && activity.AuthorId == UserHelper.GetCurrentEmployee().Id.ToString())
            {
                return true;
            }
            if (activity.Document.DocumentType == 2 && activity.AuthorId == UserHelper.GetCurrentEmployee().Id.ToString())
            {
                return true;
            }
            if (activity.Document.DocumentType == 3 && activity.AuthorId == UserHelper.GetCurrentEmployee().Id.ToString())
            {
                return true;
            }
            if (activity.Document.DocumentType == 5 && activity.AuthorId == UserHelper.GetCurrentEmployee().Id.ToString())
            {
                return true;
            }

            if (activity.Document.DocumentType == 4 && (activity.Document.ProjectType == 3 || activity.Document.ProjectType == 6))
            {
                return tasks.Where(o => o.ActivityId == activity.Id).Any(o => o.State == 0 || o.State == 4);
            }
            return false;
        }
        public bool AllowConfrm(Task task)
        {

            return (task.State == 0 || task.State == 4) && task.CreatedDate.HasValue &&
                   task.CreatedDate.Value.AddDays(1) < DateTime.Now;

            return false;

        }
        private TaskTreeItemModel ConvertTask(Task task, IEnumerable<Report> reports)
        {

            TaskTreeItemModel model = new TaskTreeItemModel
            {
                Type = -1,
                TypeEx = task.TypeEx,
                Id = task.Id,
                IsConfirm = AllowConfrm(task),
                Number = task.Number,
                State = task.State,
                IsResponsible = task.FunctionType == 1,
                IsMineLine = task.IsMainLine,
                Executor = task.ExecutorValue,
            };

            ReportTask[] reportTasks =
                reports.Where(x => x.TaskId == task.Id).OrderBy(x => x.ExecutionDate).Select(o => new ReportTask
                {
                    ReportDate = o.ExecutionDate != null ? o.ExecutionDate.Value.ToShortDateString() : string.Empty,
                    ReportText = o.Text
                }).ToArray();

            model.Reports = reportTasks;

            return model;
        }

        private TaskTreeItemModel Find(IEnumerable<TaskTreeItemModel> tree, Guid? id)
        {
            TaskTreeItemModel node = null;
            if (id == null)
                return null;

            foreach (TaskTreeItemModel item in tree)
            {
                if (item.Id == id)
                {
                    node = item;
                    break;
                }
                if (item.HasChildren)
                {
                    node = Find(item.Children, id);
                    if (node != null)
                    {
                        break;
                    }
                }
            }
            return node;
        }

        /// <summary>
        /// Удаление
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public ActionResult Delete(Guid actionId)
        {
            Activity activity = _db.Activities.Include(o => o.Document).FirstOrDefault(o => o.Id == actionId);
            TaskAction action = new TaskAction()
            {
                Id = actionId,
                ActionId = actionId,
                DocumenetNumber = activity.Document.Number,
                DocumentId = activity.DocumentId,
                Type = activity.Type,
                Text = activity.Text,
                ExecutorId = DictionaryHelper.GetItems(activity.ExecutorsId, activity.ExecutorsValue),
                ResponsibleId = DictionaryHelper.GetItems(activity.ResponsibleId, activity.ResponsibleValue),
                ExecutionDate = activity.ExecutionDate,
                DocumenetDate = activity.Document.DocumentDate.HasValue ? activity.Document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty
            };

            return PartialView(action);
        }

        public ActionResult DeleteDoc(Guid id)
        {


            return PartialView(id);
        }

        public ActionResult JobEditFile(Guid taskId)
        {
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);

            return PartialView(task);
        }

        [HttpPost]
        public ActionResult DeleteDocConfirm(Guid id)
        {
            Document doc = _db.Documents.Find(id);
            doc.IsDeleted = true;
            _db.SaveChanges();
            return Content(bool.TrueString);
        }
        /// <summary>
        /// Удаление подтверждение
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteConfirm(Guid actionId)
        {
            Activity task = _db.Activities.FirstOrDefault(o => o.Id == actionId);
            _db.Activities.Remove(task);
            _db.SaveChanges();
            return Content(bool.TrueString);
        }

        /// <summary>
        /// Удаление подтверждение
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteBpConfirm(Guid actionId)
        {
            Activity activity = _db.Activities.FirstOrDefault(o => o.Id == actionId);

            if (activity != null && activity.IsParrent)
            {
                Activity activity2 = _db.Activities.FirstOrDefault(o => o.ParentId == activity.Id);
                if (activity2 != null)
                {
                    activity2.IsParrent = true;
                    activity2.ParentId = null;
                }
            }
            if (activity != null && !activity.IsParrent)
            {
                Activity activity1 = _db.Activities.FirstOrDefault(o => o.Id == activity.ParentId);
                Activity activity2 = _db.Activities.FirstOrDefault(o => o.ParentId == activity.Id);
                if (activity1 != null && activity2 != null)
                {
                    activity2.ParentId = activity1.Id;
                }
            }

            _db.Activities.Remove(activity);
            _db.SaveChanges();
            return Content(bool.TrueString);
        }

        /// <summary>
        /// В работу
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult Job(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }
        public ActionResult JobEdit(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }
        /// <summary>
        /// Принять в работу 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult JobConfirm(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            if (task.State == 0)
            {
                task.State = 4;
                task.DateOfOperation = DateTime.Now;
                _db.SaveChanges();


                return Json(
                    new
                    {
                        State = true,
                        Task = new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                    });
            }
            else
            {
                return Json(new { State = false });
            }
        }
        /// <summary>
        /// Принять в работу 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult JobEditConfirm(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);

            task.State = 1;
            task.DateOfOperation = DateTime.Now;
            _db.SaveChanges();


            return Json(
                new
                {
                    State = true,
                    Task = new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                });

        }
        /// <summary>
        /// Согласование
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult Agreement(Guid taskId)
        {
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }

        /// <summary>
        /// Согласование
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public ActionResult AgreementConfirm(Guid taskId, string note, int? pageCount, int? copiesCount)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            if (task.Type == 3 && (task.State == 1 || task.State == 4))
            {
                //var dictionaries = _db.Dictionaries.Where(o => o.Type == "Nomenclature" && o.IsGuide).ToList().Select(o => o.Id.ToString());
                if (!_db.Activities.Any(o => o.Branch == 0 && o.IsNotActive && o.DocumentId == task.DocumentId) &&
                    (task.Document.ProjectType == 1 || task.Document.ProjectType == 3 || task.Document.ProjectType == 6 || ((task.Document.ProjectType == 5 || task.Document.ProjectType == 2) && task.Document.ApplicantType == 1))
                    )
                {
                    Activity parentActivity = _db.Activities.FirstOrDefault(x => x.DocumentId == task.DocumentId && x.Branch == 0 && !_db.Activities.Where(o => o.DocumentId == task.DocumentId && o.Branch == 0).Select(c => c.ParentId).Contains(x.Id));

                    Activity activity = new Activity
                    {
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

                Report report = new Report()
                {
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

                if (!_db.Activities.Any(o => o.Branch == 0 && o.IsNotActive && o.DocumentId == task.DocumentId))
                {
                    Document project = _db.Documents.Find(task.DocumentId);
                    Document document = DocumentManager.Clone(project);
                    document.Id = Guid.NewGuid();
                    if (project.ProjectType == 2 || project.ProjectType == 5 || project.ProjectType > 9)
                    {
                        DocumentManager.ConvertInCorrespondentDocument(document, project, UserHelper.GetCurrentEmployee(),
                            UserHelper.GetCurrentEmployee().DisplayName);
                    }
                    else
                    {
                        if (project.ProjectType == 4 || project.ProjectType == 3)
                        {
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
                        new
                        {
                            State = true,
                            Task =
                                new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                        });
            }
            else
            {
                return
                    Json(
                        new
                        {
                            State = false
                        });
            }
        }
        /// <summary>
        /// Исполнение
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult Exclude(Guid taskId)
        {
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }
        /// <summary>
        /// Исполнение
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult ExcludeTask(Guid taskId)
        {
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }
        /// <summary>
        /// Исполнение
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult ExcludeTaskComment(Guid taskId)
        {
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }
        /// <summary>
        /// Исполнение подтверждение
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public ActionResult ExcludeConfirm(Guid taskId, string note, int typeReport, int? pageCount, int? copiesCount)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);

            if ((task.Type == 0 || task.Type == 1 || task.Type == 2) && ((task.State == 0 || task.State == 1 || task.State == 4 || task.State == 3) && (task.Document.MonitoringType != 3 && task.Document.MonitoringType != 4))) {

                var pp = _db.PriceProjects.FirstOrDefault(x => x.Id == task.DocumentId);
                if (pp != null) {
                    //Завершено
                    if (typeReport == 2) {
                        pp.Status = (int) PriceProjectStatus.Finished;
                        //Если изменение цен, то меняем статус в родительском заявлении
                        if (pp.Type == (int) PriceProjectType.RePriceLs || pp.Type == (int) PriceProjectType.RePriceImn) {
                            var parentProject = _db.PriceProjects.FirstOrDefault(x => x.Id == pp.PriceProjectId);
                            if (parentProject != null && parentProject.Status != (int) PriceProjectStatus.Finished) {
                                parentProject.Status = (int) PriceProjectStatus.Finished;
                                _db.Entry(parentProject).State = EntityState.Modified;
                            }
                        }
                    }
                    //Переговоры
                    else {
                        pp.Status = (int)PriceProjectStatus.Conversation;
                        new NotificationManager().SendNotification(
                            string.Format("На заявку №{0} пришло письмо от эксперта НЦЭЛС", string.IsNullOrEmpty(task.Document.OutgoingNumber) ? task.Document.Number : task.Document.OutgoingNumber),
                            ObjectType.Letter, pp.Id, pp.OwnerId);
                    }
                    _db.Entry(pp).State = EntityState.Modified;
                    _db.SaveChanges();
                }

                Report report = new Report()
                {
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
                        new
                        {
                            State = true,
                            Task =
                                new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                        });
            }
            else
            {
                return
                    Json(
                        new
                        {
                            State = false
                        });
            }
        }/// <summary>
         /// Исполнение подтверждение
         /// </summary>
         /// <param name="taskId"></param>
         /// <param name="note"></param>
         /// <returns></returns>
        public ActionResult ExcludeCommentConfirm(Guid taskId, string note, int typeReport, int? pageCount, int? copiesCount)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);



            Report report = new Report()
            {
                Id = Guid.NewGuid(),
                TaskId = task.Id,
                ExecutionDate = DateTime.Now,
                Text = note,
                Type = typeReport,
                DocumentId = task.DocumentId,
                PageCount = pageCount,
                SymbolCount = copiesCount
            };

            _db.Reports.Add(report);
            _db.SaveChanges();


            return
                Json(
                    new
                    {
                        State = true,
                        Task =
                            new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                    });

        }

        /// <summary>
        /// исполнение документа
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult ExcludeDoc(Guid taskId)
        {
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }
        /// <summary>
        /// исполнение документа подтверждение 
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public ActionResult ExcludeConfirmDoc(Guid taskId, string note)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);

            if ((task.Type == 0 || task.Type == 1 || task.Type == 2) && ((task.State == 0 || task.State == 1 || task.State == 4 || task.State == 3)))
            {

                Report report = new Report()
                {
                    Id = Guid.NewGuid(),
                    TaskId = task.Id,
                    ExecutionDate = DateTime.Now,
                    Text = note,
                    Type = 0,
                    DocumentId = task.DocumentId
                };
                task.State = 2;
                _db.Reports.Add(report);
                _db.SaveChanges();

                ExecuteProcedure(task.Id, report.Text);

                return
                    Json(
                        new
                        {
                            State = true,
                            Task =
                                new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                        });
            }
            else
            {
                return
                    Json(
                        new
                        {
                            State = false
                        });
            }
        }
        /// <summary>
        /// Поручение 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult Reassignment(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            TaskAction action = new TaskAction()
            {
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
        public ActionResult ReassignmentRef(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            TaskAction action = new TaskAction()
            {
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
        /// Поручение подтверждение
        /// </summary>
        /// <param name="taskAction"></param>
        /// <returns></returns>
        public ActionResult ReassignmentConfirm(TaskAction taskAction)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.Id);

            Activity activity = new Activity
            {
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
                //TypeEx = taskAction.Type.Value,
                Text = taskAction.Text,
                CreatedDate = DateTime.Now,

                IsMainLine = task.IsMainLine
            };
            _db.Activities.Add(activity);

            if (task.State != 1)
            {
                task.State = 1;
                task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;

            }
            _db.SaveChanges();
            return Json(
                    new
                    {
                        State = true,
                        Task =
                            new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                    });

        }
        /// <summary>
        /// Поручение из документа 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult ReassignmentDoc(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            TaskAction action = new TaskAction()
            {
                Id = taskId,
                ActionId = task.ActivityId,
                DocumenetNumber = task.Document.Number,
                DocumentId = task.DocumentId,
                Type = task.Type,
                State = task.State,
                ExecutionDate = task.Document.ExecutionDate,
                Date = task.Document.DocumentDate,
                DocumenetDate = task.Document.DocumentDate.HasValue ? task.Document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty

            };

            return PartialView(action);
        }
        /// <summary>
        /// Поручение редактирование из документа
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public ActionResult ReassignmentEditDoc(Guid actionId)
        {
            Activity activity = _db.Activities.Include(o => o.Document).FirstOrDefault(o => o.Id == actionId);
            TaskAction action = new TaskAction()
            {
                Id = actionId,
                ActionId = actionId,
                DocumenetNumber = activity.Document.Number,
                DocumentId = activity.DocumentId,
                Type = activity.Type,
                Text = activity.Text,
                ExecutorId = DictionaryHelper.GetItems(activity.ExecutorsId, activity.ExecutorsValue),
                ResponsibleId = DictionaryHelper.GetItems(activity.ResponsibleId, activity.ResponsibleValue),
                ExecutionDate = activity.ExecutionDate,
                DocumenetDate = activity.Document.DocumentDate.HasValue ? activity.Document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty

            };

            return PartialView(action);
        }

        /// <summary>
        /// Поручение редактирование редактирование
        /// </summary>
        /// <param name="taskAction"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReassignmentEditConfirmDoc(TaskAction taskAction)
        {
            Activity activity = _db.Activities.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.ActionId);
            activity.ExecutorsId = DictionaryHelper.GetItemsId(taskAction.ExecutorId);
            activity.ExecutorsValue = DictionaryHelper.GetItemsName(taskAction.ExecutorId);
            activity.ResponsibleId = DictionaryHelper.GetItemId(taskAction.ExecutorId.FirstOrDefault());
            activity.ResponsibleValue = DictionaryHelper.GetItemName(taskAction.ExecutorId.FirstOrDefault());
            activity.Text = taskAction.Text;
            activity.ExecutionDate = taskAction.ExecutionDate;
            _db.SaveChanges();
            _db.ActivityIsMainLine(activity.DocumentId);

            return Content(bool.TrueString);
        }

        /// <summary>
        /// Поручение из документа
        /// </summary>
        /// <param name="taskAction"></param>
        /// <returns></returns>
        public ActionResult ReassignmentConfirmDoc(TaskAction taskAction)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.Id);
            if ((task.Type == 1 || task.Type == 2) && (task.State == 0 || task.State == 1 || task.State == 4 || task.State == 3))
            {
                Activity activity = new Activity
                {
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
                    CreatedDate = DateTime.Now,
                    ParentId = task.ActivityId,
                    Text = taskAction.Text,

                    IsMainLine = task.IsMainLine
                };
                _db.Activities.Add(activity);

                if (task.State != 1)
                {
                    task.State = 1;
                    task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;

                }
                _db.SaveChanges();
                return Json(
                        new
                        {
                            State = true,
                            Task =
                                new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                        });
            }
            else
            {
                return Json(
                        new
                        {
                            State = false
                        });
            }
        }
        /// <summary>
        /// Резолюция
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult Resolution(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            TaskAction action = new TaskAction()
            {
                Id = taskId,
                ActionId = task.ActivityId,
                DocumenetNumber = task.Document.Number,
                DocumentId = task.DocumentId,
                Type = task.Type,
                State = task.State,
                ExecutionDate = task.Document.ExecutionDate,
                DocumenetDate = task.Document.DocumentDate.HasValue ? task.Document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty
            };
            if (task.Document.ProjectType == 0)
            {
                return PartialView("ReassignmentRef", action);
            }
            return PartialView(action);

        }

        /// <summary>
        /// Резалюция
        /// </summary>
        /// <param name="taskAction"></param>
        /// <returns></returns>
        public ActionResult ResolutionConfirm(TaskAction taskAction)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.Id);
            if (task.Type == 0 && task.State == 4)
            {
                Document document = _db.Documents.FirstOrDefault(o => o.Id == task.DocumentId);
                document.ResolutionValue = task.Text;
                Activity activity = new Activity
                {
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
                    TypeEx = taskAction.Type.Value,
                    CreatedDate = DateTime.Now,
                    ParentId = task.ActivityId,
                    Text = taskAction.Text,

                    IsMainLine = task.IsMainLine
                };
                _db.Activities.Add(activity);

                if (task.State != 1)
                {
                    task.State = 1;
                    task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;

                }
                if (document.StateType != 3)
                {
                    document.StateType = 3;
                    document.ResolutionId = activity.Id;
                    document.ResolutionValue = activity.Text;

                }
                _db.SaveChanges();
                return Json(
                    new
                    {
                        State = true,
                        Task =
                            new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                    });
            }
            else
            {
                return Json(
                    new
                    {
                        State = false
                    });
            }
        }
        /// <summary>
        /// Резолюция
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult ResolutionDoc(Guid taskId)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            TaskAction action = new TaskAction()
            {
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
        public ActionResult ResolutionConfirmDoc(TaskAction taskAction)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.Id);
            if (task.Type == 0)
            {
                Document document = _db.Documents.FirstOrDefault(o => o.Id == task.DocumentId);
                document.ResolutionValue = task.Text;
                Activity activity = new Activity
                {
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
                    Type = 1,
                    CreatedDate = DateTime.Now,
                    ParentId = task.ActivityId,
                    Text = taskAction.Text,

                    IsMainLine = task.IsMainLine
                };
                _db.Activities.Add(activity);

                if (task.State != 1)
                {
                    task.State = 1;
                    task.DateOfOperation = task.DateOfOperation ?? DateTime.Now;

                }
                if (document.StateType != 3)
                {
                    document.StateType = 3;
                    document.ResolutionId = activity.Id;
                    document.ResolutionValue = activity.Text;

                }
                _db.SaveChanges();
                return Json(
                    new
                    {
                        State = true,
                        Task =
                            new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                    });
            }
            else
            {
                return Json(
                    new
                    {
                        State = false
                    });
            }
        }
        /// <summary>
        /// Отказать
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult Reject(Guid taskId)
        {
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }
        /// <summary>
        /// Отказать подтверждение
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public ActionResult RejectConfirm(Guid taskId, string note)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            if ((task.Type == 3 || task.Type == 4) && (task.State == 1 || task.State == 4))
            {
                if (task.State != 2)
                {
                    Report report = new Report()
                    {
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
                        new
                        {
                            State = true,
                            Task =
                                new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                        });
            }
            else
            {
                return
                    Json(
                        new
                        {
                            State = false
                        });
            }
        }
        /// <summary>
        /// полписание 
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult Signing(Guid taskId)
        {
            Task task = _db.Tasks.FirstOrDefault(o => o.Id == taskId);
            return PartialView(task);
        }

        /// <summary>
        /// подписание подтверждение
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public ActionResult SigningConfirm(Guid taskId, string note)
        {
            Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskId);
            Document project = _db.Documents.First(o => o.Id == task.DocumentId);
            if (task.Type == 4 && (task.State == 1 || task.State == 4))
            {

                Report report = new Report()
                {
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
                var employee = UserHelper.GetCurrentEmployee();

                document.Id = Guid.NewGuid();
                if (project.ProjectType == 1)
                    DocumentManager.ConvertInOutgouingDocument(document, project, employee, employee.DisplayName);

                else
                {
                    if (project.ProjectType == 4 || project.ProjectType == 3 || project.ProjectType == 6)
                    {
                        DocumentManager.ConvertInAdminDocument(document, project, employee, employee.DisplayName);
                    }
                    else
                        DocumentManager.ConvertInCorrespondentDocument(document, project, employee, employee.DisplayName);
                }
                project.StateType = 9;
                project.ModifiedDate = DateTime.Now;
                project.ReturnDate = DateTime.Now;
                var answer = _db.Documents.First(o => o.Id == new Guid(project.AnswersId));
                if (answer?.CompareConterDate != null)
                {
                    var span = answer.CompareConterDate.Value.AddDays(31) - DateTime.Now;
                    answer.CountDay = span.Days;
                    answer.ReturnDate =DateTime.Now;
                }
                _db.SaveChanges();

                if (document.AnswersId != null) {
                    SendPriceProjectNotification(document, project.RemarkId);
                }

                _notificationManager.TrySendNotification(EventType.ContractRejectedWithCommentByCounsel, ObjectType.Contract, project);
                return
                    Json(
                        new
                        {
                            State = true,
                            Task =
                                new { task.Id, task.Type, task.State, task.Document.MonitoringType, DocumentState = task.Document.StateType }
                        });
            }
            return
                Json(
                    new
                    {
                        State = false
                    });
        }

        private void SendPriceProjectNotification(Document document, int? remarkId) {
            var pp = _db.PriceProjects.FirstOrDefault(x => x.Id == new Guid(document.AnswersId));
            if (pp != null) {
                //Смена статуса только для замечания
                if (remarkId.HasValue && remarkId.Value == 1) {
                    pp.Status = (int)PriceProjectStatus.OnRevision;
                    _db.Entry(pp).State = EntityState.Modified;
                    _db.SaveChanges();
                }

                var d = _db.Documents.FirstOrDefault(x => x.Id == pp.Id);
                if (d != null) {
                    var empl = _db.Employees.FirstOrDefault(x => x.Id == pp.OwnerId);
                    if (empl != null) {
                        new NotificationManager().SendNotification(
                            string.Format("На заявку №{0} пришло письмо от эксперта НЦЭЛС", string.IsNullOrEmpty(d.OutgoingNumber) ? d.Number : d.OutgoingNumber),
                            ObjectType.Letter, pp.Id, pp.OwnerId, empl.Email);
                    }
                    else {
                        LogHelper.Log.Error("Не удалось найти Employee по OwnerId="+pp.OwnerId);
                    }
                }
                
            }
        }


        /// <summary>
        /// полписание 
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public ActionResult SigningDoc(Guid documentId)
        {
            Document document = _db.Documents.FirstOrDefault(o => o.Id == documentId);
            TaskAction action = new TaskAction()
            {
                Id = documentId,
                //ActionId = task.ActivityId,
                DocumenetNumber = document.Number,
                DocumentId = documentId,
                Type = 3,
                State = 0,
                ExecutionDate = DateTime.Now,
                DocumenetDate = document.DocumentDate.HasValue ? document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty
            };
            return PartialView(action);
        }

        /// <summary>
        /// подписание подтверждение
        /// </summary>
        /// <param name="taskAction"></param>
        /// <returns></returns>
        public ActionResult SigningConfirmDoc(TaskAction taskAction)
        {
            //	Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.Id);
            Activity parentActivity = _db.Activities.Where(x => x.DocumentId == taskAction.DocumentId).OrderByDescending(x => x.CreatedDate).FirstOrDefault();

            Activity activity = new Activity
            {
                Id = Guid.NewGuid(),
                //	ParentTask = task.Id,
                DocumentId = taskAction.DocumentId,
                AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                AuthorValue = UserHelper.GetCurrentEmployee().DisplayName.ToString(),
                ExecutorsId = DictionaryHelper.GetItemsId(taskAction.ExecutorId),
                ExecutorsValue = DictionaryHelper.GetItemsName(taskAction.ExecutorId),
                //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                Type = 4,
                IsParrent = parentActivity == null,
                CreatedDate = DateTime.Now,
                ParentId = parentActivity == null ? (Guid?)null : parentActivity.Id,
                ExecutionDate = taskAction.ExecutionDate,
                Text = taskAction.Text,
                IsNotActive = true,
                //IsMainLine = task.IsMainLine
            };
            _db.Activities.Add(activity);
            _db.SaveChanges();
            return Json(
                new
                {
                    State = true,

                });


        }


        /// <summary>
        /// полписание 
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public ActionResult AgreementDoc(Guid documentId)
        {
            Document document = _db.Documents.FirstOrDefault(o => o.Id == documentId);
            TaskAction action = new TaskAction()
            {
                Id = documentId,
                //ActionId = task.ActivityId,
                DocumenetNumber = document.Number,
                DocumentId = documentId,
                Type = 3,
                State = 0,
                ExecutionDate = DateTime.Now,
                DocumenetDate = document.DocumentDate.HasValue ? document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty
            };
            return PartialView(action);
        }

        /// <summary>
        /// подписание подтверждение
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public ActionResult AgreementConfirmDoc(TaskAction taskAction)
        {
            //	Task task = _db.Tasks.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.Id);
            Activity parentActivity = _db.Activities.Where(x => x.DocumentId == taskAction.DocumentId && x.Branch == 0 && x.IsParrent).OrderByDescending(x => x.CreatedDate).FirstOrDefault();


            Activity activity = new Activity
            {
                Id = Guid.NewGuid(),
                //	ParentTask = task.Id,
                DocumentId = taskAction.DocumentId,
                AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                ExecutorsId = DictionaryHelper.GetItemsId(taskAction.ExecutorId),
                ExecutorsValue = DictionaryHelper.GetItemsName(taskAction.ExecutorId),
                //ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId),
                //ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId),
                Type = 3,
                IsParrent = parentActivity == null,
                CreatedDate = DateTime.Now,
                ParentId = parentActivity == null ? (Guid?)null : parentActivity.Id,
                ExecutionDate = taskAction.ExecutionDate,
                Text = taskAction.Text,
                IsNotActive = true,
                //IsMainLine = task.IsMainLine
            };
            _db.Activities.Add(activity);


            _db.SaveChanges();
            return Json(
                new
                {
                    State = true,

                });

        }

        /// <summary>
        /// согласование редактирование из документа
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public ActionResult AgreementEditDoc(Guid actionId)
        {
            Activity activity = _db.Activities.Include(o => o.Document).FirstOrDefault(o => o.Id == actionId);
            TaskAction action = new TaskAction()
            {
                Id = actionId,
                ActionId = actionId,
                DocumenetNumber = activity.Document.Number,
                DocumentId = activity.DocumentId,
                Type = activity.Type,
                Text = activity.Text,
                ExecutorId = DictionaryHelper.GetItems(activity.ExecutorsId, activity.ExecutorsValue),
                //ResponsibleId = DictionaryHelper.GetItems(activity.ResponsibleId, activity.ResponsibleValue),
                ExecutionDate = activity.ExecutionDate,
                DocumenetDate = activity.Document.DocumentDate.HasValue ? activity.Document.DocumentDate.Value.ToString("dd.MM.yyyy") : string.Empty

            };

            return PartialView(action);
        }

        /// <summary>
        /// Поручение редактирование редактирование
        /// </summary>
        /// <param name="taskAction"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AgreementEditConfirmDoc(TaskAction taskAction)
        {
            Activity activity = _db.Activities.Include(o => o.Document).FirstOrDefault(o => o.Id == taskAction.ActionId);
            activity.ExecutorsId = DictionaryHelper.GetItemsId(taskAction.ExecutorId);
            activity.ExecutorsValue = DictionaryHelper.GetItemsName(taskAction.ExecutorId);
            activity.ExecutionDate = taskAction.ExecutionDate;
            //activity.ResponsibleId = DictionaryHelper.GetItemsId(taskAction.ResponsibleId);
            //	activity.ResponsibleValue = DictionaryHelper.GetItemsName(taskAction.ResponsibleId);
            activity.Text = taskAction.Text;
            _db.SaveChanges();
            return Content(bool.TrueString);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Исполнение документа
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="note"></param>
        public void ExecuteProcedure(Guid taskId, string note)
        {
            try
            {
                object[] parameters = {
                    new SqlParameter("@und", taskId),
                    new SqlParameter("@n", note),
                    new SqlParameter("@m", UserHelper.GetCurrentEmployee().ShortName)
                };
                _db.Database.ExecuteSqlCommand("EXEC CloseTask  @Id=@und , @Note=@n, @ModifiedUser=@m", parameters);
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// На перевод
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult Translate(Guid taskId)
        {
            Task currentTask = _db.Tasks.Include("Document").FirstOrDefault(o => o.Id == taskId);
            TaskAction taskAction = new TaskAction
            {
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
        public ActionResult TranslateConfirm(TaskAction taskAction)
        {
            Task currentTask = _db.Tasks.FirstOrDefault(o => o.Id == taskAction.ActionId);
            currentTask.State = 2;

            Report report = new Report()
            {
                Id = Guid.NewGuid(),
                TaskId = currentTask.Id,
                ExecutionDate = DateTime.Now,
                Type = 0,
                Text = "Документ отправлен на перевод: " + DictionaryHelper.GetItemsName(taskAction.ExecutorId),
                DocumentId = currentTask.DocumentId
            };

            Activity activity = new Activity
            {
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
            Activity activitySign = new Activity
            {
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

        /// <summary>
        /// Добавить согласование
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public ActionResult AddAgreement(Guid taskId)
        {
            Task currentTask = _db.Tasks.Include("Document").FirstOrDefault(o => o.Id == taskId);
            TaskAction taskAction = new TaskAction
            {
                Id = Guid.NewGuid(),
                ActionId = taskId,
                DocumentId = currentTask.Document.Id,
                DocumenetNumber = currentTask.Document.Number,
                DocumenetDate = currentTask.Document.DocumentDate.Value.ToString("dd.MM.yyyy")
            };
            return PartialView(taskAction);
        }
        /// <summary>
        /// Подтвертить согласование
        /// </summary>
        /// <param name="taskAction"></param>
        /// <returns></returns>
        public ActionResult AddAgreementConfirm(TaskAction taskAction)
        {
            Task currentTask = _db.Tasks.FirstOrDefault(o => o.Id == taskAction.ActionId);
            Activity currentActivity = _db.Activities.FirstOrDefault(x => x.Id == currentTask.ActivityId);
            Activity lastActivity = _db.Activities.FirstOrDefault(x => x.ParentId == currentActivity.Id);

            Activity activity = new Activity
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Type = 3,
                IsParrent = false,
                IsNotActive = true,
                AuthorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,

                ExecutorsId = DictionaryHelper.GetItemsId(taskAction.ExecutorId),
                ExecutorsValue = DictionaryHelper.GetItemsName(taskAction.ExecutorId),
                Text = taskAction.Text,

                DocumentId = currentTask.DocumentId,
                ExecutionDate = currentTask.ExecutionDate,
                ParentId = currentTask.ActivityId
            };

            if (lastActivity != null)
                lastActivity.ParentId = activity.Id;

            _db.Activities.Add(activity);
            _db.SaveChanges();

            return Json(new { State = true });
        }
    }
}
