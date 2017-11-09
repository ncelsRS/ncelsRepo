using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ncels.Core.ActivityManager.DocumentActions;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Activity;

namespace Ncels.Core.ActivityManager
{
    public class ActivityManager
    {
        private ncelsEntities _context;
        private ActivityRepository _activityRepository;
        private static Dictionary<DocumentActionTypeKey, Func<ncelsEntities, DocumentAction>> _documentActions;

        static ActivityManager()
        {
            InitDocumentActions();
        }

        public ActivityManager()
        {
            _context = new ncelsEntities();
            _activityRepository = new ActivityRepository(_context);
        }
        
        private static void InitDocumentActions()
        {
            _documentActions = new Dictionary<DocumentActionTypeKey, Func<ncelsEntities, DocumentAction>>();
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.Contract, Dictionary.ExpActivityType.ContractWithProcCenterHead),
                c => new ContractAgreementWithProcHeadAction(c));
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.DirectionToPay, Dictionary.ExpActivityType.DirectionToPayAgrement),
                c => new DirectionToPayAgreementAction(c));
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.Contract, Dictionary.ExpActivityType.ContractSigningByApplicantAndCeo),
                c => new ContractSigningByApplicantAndCeoAction(c));
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.EXP_DrugFinalDocument, Dictionary.ExpActivityType.FinalDocAgreement),
                c => new DrugFinalDocumentAgreementAction(c));
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.Letter, Dictionary.ExpActivityType.ExpertiseLetterAgreement),
                c => new ExpertiseLetterAgreementAction(c));
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.Letter, Dictionary.ExpActivityType.ExpertiseLetterSigning),
                c => new ExpertiseLetterSigningAction(c));
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.TranslateFile, Dictionary.ExpActivityType.TranslateDocAgreement),
                c => new ExpertiseTranslateAgreementAction(c));
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.CertificateOfCompletion, Dictionary.ExpActivityType.CertificateOfComplitionAgreement),
                c => new CertificateOfCompletionAgreementAction(c));
            _documentActions.Add(new DocumentActionTypeKey(Dictionary.ExpAgreedDocType.MaketFile, Dictionary.ExpActivityType.MaketDocAgreement),
             c => new ExpertiseTranslateAgreementAction(c));
        }

        public void CreateActivity(Guid documentId, Guid documentTypeId, Guid activityTypeId, string docNumber, DateTime? docDate)
        {
            CreateActivity(documentId, documentTypeId, activityTypeId, null, null, null, null, docNumber, docDate);
        }

        public void CreateActivity(Guid documentId, string documentTypeCode, string activityTypeCode, string docNumber, DateTime? docDate)
        {

            var documentTypeId = _activityRepository.GetDocumentTypeIdByCode(documentTypeCode);
            var activityTypeId = _activityRepository.GetActivityTypeIdByCode(activityTypeCode);

            CreateActivity(documentId, documentTypeId.Value, activityTypeId.Value, null, null, null, null, docNumber, docDate);
        }

        public void CreateActivity(Guid documentId, Guid documentTypeId, Guid activityTypeId, string documentValue, string documentTypeValue, string activityTypeValue, string text, string docNumber, DateTime? docDate)
        {
            Func<ncelsEntities, DocumentAction> docActionInitizlizer;
            var documentType = _activityRepository.GetDocumentTypeById(documentTypeId);
            var activityType = _activityRepository.GetActivityTypeById(activityTypeId);
            _documentActions.TryGetValue(new DocumentActionTypeKey(documentType.Code, activityType.Code),
                out docActionInitizlizer);
            var documentAction = docActionInitizlizer != null ? docActionInitizlizer(_context) : null;

            var settings = _activityRepository.GetSettings(documentTypeId);

            var status = _activityRepository.GetActivityStatusByCode();
            EXP_Activities activity = new EXP_Activities()
            {
                Id = Guid.NewGuid(),
                Text = text ?? activityType.Name,
                AuthorId = UserHelper.GetCurrentEmployee().Id,
                AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                CreatedDate = DateTime.Now,
                DocumentId = documentId,
                DocumentValue = documentValue,
                DocumentTypeId = documentTypeId,
                DocumentTypeValue = documentTypeValue,
                TypeId = activityTypeId,
                TypeValue = activityTypeValue,
                StatusId = status.Id,
                StatusValue = status.Code,
                DocDate = docDate,
                DocNumber = docNumber
            };
            _activityRepository.Insert(activity);

            var activitySetting = settings.EXP_AgreementProcSettingsActivities.FirstOrDefault(a => a.ActivityTypeId == activityTypeId);
            if (activitySetting != null)
            {
                Guid? parentTaskId = null;
                foreach (var taskSetting in activitySetting.EXP_AgreementProcSettingsTasks.OrderBy(t => t.OrderNum))
                {
                    var executorId = taskSetting.ExecutorId;
                    if (taskSetting.ExecutorId == null && documentAction != null)
                        executorId = documentAction.GetExecutor(documentId, activityTypeId, taskSetting.OrderNum);

                    var taskInWorkStatus = _activityRepository.GetTaskStatusByCode(Dictionary.ExpTaskStatus.InWork);
                    var taskInQueueStatus = _activityRepository.GetTaskStatusByCode(Dictionary.ExpTaskStatus.InQueue);

                    EXP_Tasks task = new EXP_Tasks()
                    {
                        Id = Guid.NewGuid(),
                        Text = text,
                        OrderNumber = taskSetting.OrderNum,
                        Number = taskSetting.OrderNum.ToString(),
                        TypeId = taskSetting.TaskTypeId,
                        TypeValue = string.Empty,
                        EXP_Activities = activity,
                        AuthorId = activity.AuthorId,
                        AuthorValue = activity.AuthorValue,
                        CreatedDate = DateTime.Now,
                        ExecutorId = executorId.Value,
                        ExecutorValue = string.Empty,
                        ParentTaskId = parentTaskId
                    };

                    if (taskSetting.OrderNum == 1)
                    {
                        task.StatusId = taskInWorkStatus.Id;
                        // очень важно чтобы так заполнялось. не менять !!!!
                        task.StatusValue = taskInWorkStatus.Code;
                    }
                    else
                    {
                        task.StatusId = taskInQueueStatus.Id;
                        // очень важно чтобы так заполнялось. не менять !!!!
                        task.StatusValue = taskInQueueStatus.Code;
                    }

                    parentTaskId = task.Id;

                    activity.EXP_Tasks.Add(task);
                }
            }

            _activityRepository.Save();
            if (documentAction != null)
                documentAction.ProcessStarted(documentId, activity);
        }
        
        /// <summary>
        /// Простая отправка на согласование списку исполнителей
        /// </summary>
        /// <param name="activityTypeCode"></param>
        /// <param name="docId"></param>
        /// <param name="docTypeCode"></param>
        /// <param name="docNumber"></param>
        /// <param name="docDate"></param>
        /// <param name="executors"></param>
        /// <returns></returns>
        public EXP_Activities SendToExecution(string activityTypeCode, Guid docId, string docTypeCode, string taskTypeCode, string docNumber,
            DateTime? docDate, params Guid[] executors)
        {
            Func<ncelsEntities, DocumentAction> docActionInitizlizer;
            var docTypeId = _activityRepository.GetDocumentTypeIdByCode(docTypeCode);
            var documentType = _activityRepository.GetDocumentTypeById(docTypeId.Value);
            var activityTypeId = _activityRepository.GetActivityTypeIdByCode(activityTypeCode);
            var activityType = _activityRepository.GetActivityTypeById(activityTypeId.Value);
            _documentActions.TryGetValue(new DocumentActionTypeKey(documentType.Code, activityType.Code), out docActionInitizlizer);
            var documentAction = docActionInitizlizer != null ? docActionInitizlizer(_context) : null;

            var status = _activityRepository.GetActivityStatusByCode();
            EXP_Activities activity = new EXP_Activities()
            {
                Id = Guid.NewGuid(),
                Text = activityType.Name,
                AuthorId = UserHelper.GetCurrentEmployee().Id,
                AuthorValue = UserHelper.GetCurrentEmployee().DisplayName,
                CreatedDate = DateTime.Now,
                DocumentId = docId,
                DocumentValue = "",
                DocumentTypeId = docTypeId.Value,
                DocumentTypeValue = "",
                TypeId = activityTypeId.Value,
                TypeValue = "",
                StatusId = status.Id,
                StatusValue = status.Code,
                DocDate = docDate,
                DocNumber = docNumber
            };
            _activityRepository.Insert(activity);
            Guid? parentTaskId = null;
            int taskOrder = 1;
            var taskType = _activityRepository.GetTaskStatusByCode(taskTypeCode);
            foreach (var executorId in executors)
            {
                var taskInWorkStatus = _activityRepository.GetTaskStatusByCode(Dictionary.ExpTaskStatus.InWork);
                var taskInQueueStatus = _activityRepository.GetTaskStatusByCode(Dictionary.ExpTaskStatus.InQueue);

                EXP_Tasks task = new EXP_Tasks()
                {
                    Id = Guid.NewGuid(),
                    Text = taskType.Name,
                    OrderNumber = taskOrder,
                    Number = taskOrder.ToString(),
                    TypeId = taskType.Id,
                    TypeValue = string.Empty,
                    EXP_Activities = activity,
                    AuthorId = activity.AuthorId,
                    AuthorValue = activity.AuthorValue,
                    CreatedDate = DateTime.Now,
                    ExecutorId = executorId,
                    ExecutorValue = string.Empty,
                    ParentTaskId = parentTaskId
                };

                if (taskOrder == 1)
                {
                    task.StatusId = taskInWorkStatus.Id;
                    task.StatusValue = taskInWorkStatus.Code;
                }
                else
                {
                    task.StatusId = taskInQueueStatus.Id;
                    task.StatusValue = taskInQueueStatus.Code;
                }
                parentTaskId = task.Id;
                activity.EXP_Tasks.Add(task);
                taskOrder++;
            }
            _activityRepository.Save();
            if (documentAction != null)
                documentAction.ProcessStarted(docId, activity);
            return activity;
        }

        public EXP_Activities GetActivity(Guid documentId, string activityTypeCode)
        {
            var activity = _activityRepository.Get(a => a.DocumentId == documentId && a.ExpActivityType.Code == activityTypeCode).FirstOrDefault();
            return activity;
        }

        public EXP_Tasks GetCurrentTask(Guid documentId, string activityTypeCode)
        {
            var task =
                _activityRepository.GetTasks(
                    t =>
                        t.EXP_Activities.DocumentId == documentId
                        && t.EXP_Activities.ExpActivityType.Code == activityTypeCode
                        && t.StatusesDictionary.Code == Dictionary.ExpTaskStatus.InWork).FirstOrDefault();
            return task;
        }

        public List<EXP_Tasks> GetTasks(Guid documentId, string activityTypeCode)
        {
            var tasks =
                _activityRepository.GetTasks(
                    t => t.EXP_Activities.DocumentId == documentId
                         && t.EXP_Activities.ExpActivityType.Code == activityTypeCode).ToList();
            return tasks;
        }

        public Guid? GetCurrentTaskId(Guid documentId, string activityTypeCode)
        {
            var task =
                _activityRepository.GetTasks(
                    t =>
                        t.EXP_Activities.DocumentId == documentId
                        && t.EXP_Activities.ExpActivityType.Code == activityTypeCode
                        && t.StatusesDictionary.Code == Dictionary.ExpTaskStatus.InWork).FirstOrDefault();
            return task != null ? (Guid?)task.Id : null;
        }

        public Employee GetExecutor(Guid documentId, string documentTypeCode, string activityTypeCode, EXP_AgreementProcSettingsTasks taskSetting)
        {
            Func<ncelsEntities, DocumentAction> docActionInitizlizer;
            var documentTypeId = _activityRepository.GetDocumentTypeIdByCode(documentTypeCode);
            var activityTypeId = _activityRepository.GetActivityTypeIdByCode(activityTypeCode);

            //var documentType = _activityRepository.GetDocumentTypeById(documentTypeId.Value);
            //var activityType = _activityRepository.GetActivityTypeById(activityTypeId.Value);
            _documentActions.TryGetValue(new DocumentActionTypeKey(documentTypeCode, activityTypeCode),
                out docActionInitizlizer);
            var documentAction = docActionInitizlizer != null ? docActionInitizlizer(_context) : null;

            if (taskSetting == null || documentAction == null)
                return null;

            var executorId = documentAction.GetExecutor(documentId, activityTypeId.Value, taskSetting.OrderNum);
            return _activityRepository.GetExecutorById(executorId);
        }

        public IList<EXP_AgreementProcSettingsTasks> GetSettingsTasks(Guid documentId, string documentTypeCode, string activityTypeCode, string taskTypeCode = null)
        {
            var tasks = _activityRepository.GetSettingTasks(t => t.EXP_AgreementProcSettingsActivities.ActivityType.Code == activityTypeCode);
            if (!string.IsNullOrEmpty(taskTypeCode))
                tasks = tasks.Where(t => t.TaskType.Code == taskTypeCode);

            foreach (var settingTask in tasks.OrderBy(t => t.OrderNum))
            {
                if (settingTask.Executor == null)
                    settingTask.Executor = GetExecutor(documentId, documentTypeCode, activityTypeCode, settingTask);
            }

            return tasks.ToList();
        }

        // Actions
        public bool ExecuteTask(Guid taskId, string digSign = null, string comment = null)
        {
            try
            {
                if (_activityRepository.UpdateTaskStatus(taskId, Dictionary.ExpTaskStatus.Executed, comment ?? String.Empty, digSign))
                {
                    var task = _activityRepository.GetTasks(f => f.Id == taskId).FirstOrDefault();
                    var activity = _activityRepository.Get(f => f.Id == task.ActivityId).FirstOrDefault();

                    Func<ncelsEntities, DocumentAction> docActionInitizlizer;
                    var documentType = _activityRepository.GetDocumentTypeById(activity.DocumentTypeId);
                    var activityType = _activityRepository.GetActivityTypeById(activity.TypeId);
                    _documentActions.TryGetValue(new DocumentActionTypeKey(documentType.Code, activityType.Code),
                        out docActionInitizlizer);
                    var documentAction = docActionInitizlizer != null ? docActionInitizlizer(_context) : null;

                    var taskChild = _activityRepository.GetTasks(t => t.ParentTaskId == taskId).FirstOrDefault();
                    if (taskChild == null)
                    {
                        _activityRepository.UpdateActivityStatus(activity.Id, Dictionary.ExpActivityStatus.Executed);
                    }
                    else
                    {
                        _activityRepository.UpdateTaskStatus(taskChild.Id, Dictionary.ExpTaskStatus.InWork);
                    }
                    if (documentAction != null)
                    {
                        documentAction.TaskExecuted(activity.DocumentId, task);
                        if (taskChild == null)
                            documentAction.ProcessComplited(activity.DocumentId, activity);
                    }
                    _activityRepository.Save();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool CancelTask(Guid taskId)
        {
            try
            {
                if (_activityRepository.UpdateTaskStatus(taskId, Dictionary.ExpTaskStatus.Repealed))
                {
                    var task = _activityRepository.GetTasks(f => f.Id == taskId).FirstOrDefault();
                    var activity = _activityRepository.Get(f => f.Id == task.ActivityId).FirstOrDefault();

                    Func<ncelsEntities, DocumentAction> docActionInitizlizer;
                    var documentType = _activityRepository.GetDocumentTypeById(activity.DocumentTypeId);
                    var activityType = _activityRepository.GetActivityTypeById(activity.TypeId);

                    _documentActions.TryGetValue(new DocumentActionTypeKey(documentType.Code, activityType.Code),
                        out docActionInitizlizer);
                    var documentAction = docActionInitizlizer != null ? docActionInitizlizer(_context) : null;

                    _activityRepository.UpdateActivityStatus(activity.Id, Dictionary.ExpActivityStatus.Repealed);

                    if (documentAction != null)
                        documentAction.TaskRepealed(activity.DocumentId, task);
                }
                _activityRepository.Save();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool RejectTask(Guid taskId, string comment)
        {
            try
            {
                if (_activityRepository.UpdateTaskStatus(taskId, Dictionary.ExpTaskStatus.Rejected, comment, null))
                {
                    var task = _activityRepository.GetTasks(f => f.Id == taskId).FirstOrDefault();
                    var activity = _activityRepository.Get(f => f.Id == task.ActivityId).FirstOrDefault();

                    Func<ncelsEntities, DocumentAction> docActionInitizlizer;
                    var documentType = _activityRepository.GetDocumentTypeById(activity.DocumentTypeId);
                    var activityType = _activityRepository.GetActivityTypeById(activity.TypeId);

                    _documentActions.TryGetValue(new DocumentActionTypeKey(documentType.Code, activityType.Code),
                        out docActionInitizlizer);
                    var documentAction = docActionInitizlizer != null ? docActionInitizlizer(_context) : null;

                    _activityRepository.UpdateActivityStatus(activity.Id, Dictionary.ExpActivityStatus.Rejected);

                    if (documentAction != null)
                    {
                        documentAction.TaskRejected(activity.DocumentId, task);
                        documentAction.ProcessComplited(activity.DocumentId, activity);
                    }
                }
                _activityRepository.Save();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public string GetDataForSign(Guid taskId)
        {
            var task = _activityRepository.GetTasks(f => f.Id == taskId).FirstOrDefault();
            Func<ncelsEntities, DocumentAction> docActionInitizlizer;
            var documentType = _activityRepository.GetDocumentTypeById(task.EXP_Activities.DocumentTypeId);
            var activityType = _activityRepository.GetActivityTypeById(task.EXP_Activities.TypeId);

            _documentActions.TryGetValue(new DocumentActionTypeKey(documentType.Code, activityType.Code),
                out docActionInitizlizer);
            var documentAction = docActionInitizlizer != null ? docActionInitizlizer(_context) : null;
            if (documentAction != null)
                return documentAction.GetDataForSign(task.EXP_Activities.DocumentId);
            throw new NotSupportedException("Не найден метод получения данных для ЭЦП");
        }
    }
}