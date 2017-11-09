using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ncels.Core.ActivityManager;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Prism.ViewModels.Agreement;

namespace PW.Prism.Controllers
{
    public class AgreementController : Controller
    {
        private readonly ActivityManager _activityManager = new ActivityManager();

        public ActionResult AgreementList(Guid docId, string documentTypeCode, string taskTypeCode = null)
        {
            AgreementListViewModel viewModel = new AgreementListViewModel()
            {
                Id = docId,
                DocumentTypeCode = documentTypeCode,
                ActivityTypeCode = GetActivityTypeByDocTypeCode(documentTypeCode),
                TaskTypeCode = taskTypeCode
            };
            
           // SendToAgreement(docId, documentTypeCode, GetActivityTypeByDocTypeCode(documentTypeCode));

            return PartialView(viewModel);
        }



        public ActionResult SendToAgreement(Guid documentId, string documentTypeCode, string activityTypeCode)
        {
            _activityManager.CreateActivity(documentId, documentTypeCode, activityTypeCode, null, null);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        
        private ActionResult SendDocumentToAgreement(Guid docId, Guid[] executorIds, string documentType, string taskType = null)
        {
            taskType = string.IsNullOrEmpty(taskType) ? null : taskType;
            var db = new ncelsEntities();
            var activityManager = new ActivityManager();
            switch (documentType)
            {
                case Dictionary.ExpAgreedDocType.DirectionToPay:
                    var coc = db.EXP_DirectionToPays.FirstOrDefault(e => e.Id == docId);
                    if (coc != null)
                    {
                        activityManager.SendToExecution(Dictionary.ExpActivityType.DirectionToPayAgrement, docId,
                           Dictionary.ExpAgreedDocType.DirectionToPay, taskType ?? Dictionary.ExpTaskType.Agreement,
                           string.Empty, coc.CreateDate, executorIds);
                        //var primaryDocStatus = repository.GetPrimaryFinalDocumentStatus(docId);
                    }
                    return Json(DictionaryHelper.GetDicItemByCode(CodeConstManager.STATUS_ONAGREEMENT,
                                CodeConstManager.STATUS_ONAGREEMENT).Name, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult Approve(Guid documentId, string activityTypeCode, string comment = null, string digSign = null)
        {
            var taskId = _activityManager.GetCurrentTaskId(documentId, activityTypeCode);
            if (taskId == null) throw new ArgumentException("Нет доступных заданий для выполнения");
            _activityManager.ExecuteTask(taskId.Value, digSign, comment);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reject(Guid documentId, string activityTypeCode, string comment)
        {
            var taskId = _activityManager.GetCurrentTaskId(documentId, activityTypeCode);
            if (taskId == null) throw new ArgumentException("Нет доступных заданий для отмены");
            _activityManager.RejectTask(taskId.Value, comment);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Repeal(Guid documentId, string activityTypeCode)
        {
            var taskId = _activityManager.GetCurrentTaskId(documentId, activityTypeCode);
            if (taskId == null) throw new ArgumentException("Нет доступных заданий для отмены");
            _activityManager.CancelTask(taskId.Value);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataForSign(Guid taskId)
        {
            return Json(_activityManager.GetDataForSign(taskId), JsonRequestBehavior.AllowGet);
        }


        
        public ActionResult GetTasks(Guid documentId, string documentTypeCode)
        {
            var tasks = _activityManager.GetTasks(documentId, GetActivityTypeByDocTypeCode(documentTypeCode));
            var tree = BuildTaskTree(tasks);
            return Json(tree, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSettingTasks(Guid documentId, string documentTypeCode, string taskTypeCode = null)
        {
            var tasks = _activityManager.GetSettingsTasks(documentId, documentTypeCode, GetActivityTypeByDocTypeCode(documentTypeCode), taskTypeCode);
            var tree = BuildTaskTree(tasks);
            return Json(tree, JsonRequestBehavior.AllowGet);
        }

        // from settings task
        private List<AgreementTreeItemViewModel> BuildTaskTree(IEnumerable<EXP_AgreementProcSettingsTasks> currentTasks)
        {
            List<AgreementTreeItemViewModel> tree = new List<AgreementTreeItemViewModel>();

            foreach (EXP_AgreementProcSettingsTasks stask in currentTasks.OrderBy(t => t.OrderNum))
            {
                AgreementTreeItemViewModel item = AgreementTreeItemViewModel.Find(tree, stask.Id.ToString());
                if (item == null)
                    tree.Add(ConvertSettingTaskToTreeItem(stask));
                else
                    item.Childrens.Add(ConvertSettingTaskToTreeItem(stask));
            }

            return tree;
        }

        private AgreementTreeItemViewModel ConvertSettingTaskToTreeItem(EXP_AgreementProcSettingsTasks task)
        {
            AgreementTreeItemViewModel model = new AgreementTreeItemViewModel
            {
                Id = task.Id.ToString(),
                OrderN = task.OrderNum,
                Executor = task.Executor != null ? task.Executor.FullName : string.Empty,
                CreatedDatetime = DateTime.Now.ToString("dd.MM.yyyy"),
                ExecutedDatetime = null,
                Comment = task.OrderNum.ToString(),
                Status = String.Empty
            };
            return model;
        }



        // form task
        private List<AgreementTreeItemViewModel> BuildTaskTree(IEnumerable<EXP_Tasks> currentTasks)
        {
            List<AgreementTreeItemViewModel> tree = new List<AgreementTreeItemViewModel>();

            foreach (EXP_Tasks task in currentTasks)
            {
                AgreementTreeItemViewModel item = AgreementTreeItemViewModel.Find(tree, task.Id.ToString());
                if (item == null)
                    tree.Add(ConvertTaskToTreeItem(task));
                else
                    item.Childrens.Add(ConvertTaskToTreeItem(task));
            }

            return tree;
        }

        private AgreementTreeItemViewModel ConvertTaskToTreeItem(EXP_Tasks task)
        {
            AgreementTreeItemViewModel model = new AgreementTreeItemViewModel
            {
                Id = task.Id.ToString(),
                Executor = task.ExecutorEmployee.FullName,
                CreatedDatetime = task.CreatedDate.Value.ToString("dd.MM.yyyy"),
                ExecutedDatetime = task.ExecutedDate?.ToString("dd.MM.yyyy HH:mm:ss"),
                Comment = task.Comment,
                Status = task.StatusesDictionary.Name
            };
            return model;
        }

        public static string GetActivityTypeByDocTypeCode(string documentTypeCode)
        {
            switch (documentTypeCode)
            {
                case Dictionary.ExpAgreedDocType.DirectionToPay:
                    return Dictionary.ExpActivityType.DirectionToPayAgrement;
            }

            return null;
        }

    }
}