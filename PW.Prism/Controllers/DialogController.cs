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
using PW.Ncels.Database.Repository.Activity;
using PW.Ncels.Database.Repository.Expertise;
using PW.Prism.ViewModels.Dialog;

namespace PW.Prism.Controllers
{
    public class DialogController : Controller
    {
        /// <summary>
        /// Функция наполнения для диалога подтверждения
        /// </summary>
        /// <param name="id">Объекта для обновления статуса</param>
        /// <param name="url">Url работает при подтверждении</param>
        /// <param name="text">Надпись в контент диалога </param>
        /// <returns>Контент диалога</returns>
        public ActionResult ConfirmDialog(Guid id, string url, string text)
        {
            ConfirmDialogViewModel cdm = new ConfirmDialogViewModel()
            {
                Id = id,
                Url = url,
                Text = text
            };

            return PartialView(cdm);
        }

        /// <summary>
        /// Вызов диалога для отказа
        /// </summary>
        /// <param name="id">Ид объекта для внесения изменения</param>
        /// <param name="url">Url работает при отклонении</param>
        /// <param name="text">Надпись в контент диалога</param>
        /// <param name="payload">Дополнительная информация</param>
        /// <returns>Контент диалога</returns>
        public ActionResult RejectDialog(Guid id, string url, string text, string payload)
        {
            var applicationComment = new RejectDialogViewModel()
            {
                Id = Guid.NewGuid(),
                Payload = payload,
                Url = url,
                Text = text
            };

            return PartialView(applicationComment);
        }

        [HttpGet]
        public ActionResult SendToAgreement(Guid docId, string documentType, string taskType = null)
        {
            ViewBag.UiId = Guid.NewGuid();
            ViewBag.DocumentTypeCode = documentType;
            ViewBag.TaskType = taskType;
            var viewModel = new SendAgreementViewModel()
            {
                Id = docId,
                DocumentType = documentType,
                TaskType = taskType
            };

            return PartialView(viewModel);
        }

        [HttpPost]
        public ActionResult SendDocumentToAgreement(Guid docId, Guid executorId, string documentType, string taskType = null)
        {
            taskType = string.IsNullOrEmpty(taskType) ? null : taskType;
            var db = new ncelsEntities();
            var repository = new ActivityRepository();
            var activityManager = new ActivityManager();
            /*
            switch (documentType)
            {
                case Dictionary.ExpAgreedDocType.DirectionToPay:
                    var declarationInfo = db.EXP_ExpertiseStageDosage.Where(e => e.Id == docId)
                        .Select(e => new { e.EXP_DrugDosage.RegNumber, e.EXP_DrugDosage.EXP_DrugDeclaration.CreatedDate }).FirstOrDefault();
                    activityManager.SendToExecution(Dictionary.ExpActivityType.FinalDocAgreement, docId,
                        Dictionary.ExpAgreedDocType.EXP_DrugFinalDocument, taskType ?? Dictionary.ExpTaskType.Agreement,
                        declarationInfo.RegNumber, declarationInfo.CreatedDate, executorId);
                    var primaryDocStatus = repository.GetPrimaryFinalDocumentStatus(docId);
                    return Json(primaryDocStatus.Name, JsonRequestBehavior.AllowGet);
                case Dictionary.ExpAgreedDocType.Letter:
                    var primaryRepo = new DrugPrimaryRepository();
                    bool isSigning = taskType == Dictionary.ExpTaskType.Signing;
                    var letter = primaryRepo.GetCorespondence(docId.ToString());
                    if (isSigning)
                    {
                        activityManager.SendToExecution(Dictionary.ExpActivityType.ExpertiseLetterSigning, docId,
                            Dictionary.ExpAgreedDocType.Letter, Dictionary.ExpTaskType.Signing,
                            letter.NumberLetter, letter.DateCreate, executorId);
                        return Json(
                            DictionaryHelper.GetDicItemByCode(CodeConstManager.STATUS_ONSIGNING,
                                CodeConstManager.STATUS_ONSIGNING).Name, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        activityManager.SendToExecution(Dictionary.ExpActivityType.ExpertiseLetterAgreement, docId,
                            Dictionary.ExpAgreedDocType.Letter, Dictionary.ExpTaskType.Agreement,
                            letter.NumberLetter, letter.DateCreate, executorId);
                        return Json(
                            DictionaryHelper.GetDicItemByCode(CodeConstManager.STATUS_ONAGREEMENT,
                                CodeConstManager.STATUS_ONAGREEMENT).Name, JsonRequestBehavior.AllowGet);
                    }

            }
            */
            return null;
        }


        /// <summary>
        /// Диалог назначения 
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="documentTypeCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReassignmentDialog(Guid id, string documentTypeCode, string activityTypeCode = null, string taskTypeCode = null)
        {
            ActivityRepository repository = new ActivityRepository(false);
            var settingTask = repository.GetSettingTasks(st => st.Id == id).FirstOrDefault();
            
            var rvm = new ReassignmentViewModel()
            {
                Id = settingTask?.Id ?? Guid.NewGuid(),
                ExecutorId = new Item() {Id = settingTask?.ExecutorId.ToString()},
                ExecutionDate = DateTime.Now,
                DocumentTypeCode = documentTypeCode,
                ActivityTypeCode = activityTypeCode ?? AgreementController.GetActivityTypeByDocTypeCode(documentTypeCode),
                TaskTypeCode = string.IsNullOrEmpty(taskTypeCode) ? Dictionary.ExpTaskType.Agreement: taskTypeCode,
            };

            return PartialView(rvm);
        }

        [HttpPost]
        public JsonResult ReassignmentConfirm(ReassignmentViewModel model)
        {
            bool isSuccess = true; 
            try
            {
                ActivityRepository repository = new ActivityRepository(false);
                var settingTask = repository.GetSettingTasks(st => st.Id == model.Id).FirstOrDefault();
                if (settingTask != null)
                {
                    settingTask.ExecutorId = Guid.Parse(model.ExecutorId.Id);
                    //settingTask.TaskTypeId = model.TaskTypeId;
                    repository.UpdateSettingTask(settingTask);
                }
                else
                {
                    var activityId = repository.GetActivityTypeIdByCode(model.ActivityTypeCode);
                    var taskTypeId = repository.GetTaskTypeIdByCode(model.TaskTypeCode);
                    
                    var parentSettingTask = repository.GetSettingTasks(st => st.EXP_AgreementProcSettingsActivities.ActivityTypeId == activityId && st.TaskTypeId == taskTypeId).OrderByDescending(st => st.OrderNum).FirstOrDefault();

                    settingTask = new EXP_AgreementProcSettingsTasks()
                    {
                        Id = Guid.NewGuid(),
                        ActivityId = parentSettingTask.ActivityId,
                        TaskTypeId = taskTypeId.Value,
                        ExecutorId = Guid.Parse(model.ExecutorId.Id),
                        ParentTaskId = parentSettingTask.Id,
                        OrderNum = parentSettingTask.OrderNum + 1
                    };
                    repository.AddSettingTask(settingTask);
                }

                repository.Save();
            }
            catch (Exception e)
            {
                isSuccess = false;
            }

            return Json(new {IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult DeleteReassignment(Guid id)
        {
            bool isSuccess = true;
            try
            {
                ActivityRepository repository = new ActivityRepository(false);
                repository.DeleteSettingTask(id);
                repository.Save();
            }
            catch (Exception e)
            {
                isSuccess = false;
            }
            return Json(new { IsSuccess = isSuccess }, JsonRequestBehavior.AllowGet);
        }
    }
}