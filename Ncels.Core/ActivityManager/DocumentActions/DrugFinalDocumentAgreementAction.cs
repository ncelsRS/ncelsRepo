using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Notifications;

namespace Ncels.Core.ActivityManager.DocumentActions
{
    public class DrugFinalDocumentAgreementAction : DocumentAction
    {
        public DrugFinalDocumentAgreementAction(ncelsEntities context) : base(context)
        {
        }

        public override Guid GetExecutor(Guid documentId, Guid activityTypeId, int orderNum)
        {
            throw new NotImplementedException();
        }

        public override void ProcessStarted(Guid documentId, EXP_Activities activity)
        {
            var document = _context.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == documentId);
            document.FinalDocStatusId = DictionaryHelper.GetDicIdByCode(Dictionary.EXP_DocumentStatus.DicCode,
                Dictionary.EXP_DocumentStatus.StatusOnAgreement);
            var documentType = DictionaryHelper.GetDicItemById(activity.DocumentTypeId);
            var task = activity.EXP_Tasks.FirstOrDefault();
            var agreementTaskType = DictionaryHelper.GetDicItemById(task.TypeId);
            var taskTypeName = agreementTaskType.Code == Dictionary.ExpTaskType.Agreement ? "на согласование" : "для утверждения";
            _context.SaveChanges();
            var notificationText = string.Format("К вам поступил \"{0}\" {1}", documentType.Name, taskTypeName);
            var notificationManager = new NotificationManager();
            notificationManager.SendNotification(notificationText, ObjectType.PrimaryFinalDoc, documentId, task.ExecutorId);
        }

        public override void TaskExecuted(Guid documentId, EXP_Tasks task)
        {
        }

        public override void TaskRejected(Guid documentId, EXP_Tasks task)
        {
        }

        public override void TaskRepealed(Guid documentId, EXP_Tasks task)
        {
        }

        public override void ProcessComplited(Guid documentId, EXP_Activities activity)
        {
            var document = _context.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == documentId);
            var positiveStatus = DictionaryHelper.GetDicIdByCode(Dictionary.ExpActivityStatus.DicCode,
                Dictionary.ExpActivityStatus.Executed);
            if (activity.StatusId == positiveStatus)
                document.FinalDocStatusId = DictionaryHelper.GetDicIdByCode(Dictionary.EXP_DocumentStatus.DicCode,
                    Dictionary.EXP_DocumentStatus.StatusApproved);
            else
                document.FinalDocStatusId = DictionaryHelper.GetDicIdByCode(Dictionary.EXP_DocumentStatus.DicCode,
                    Dictionary.EXP_DocumentStatus.StatusRejected);
        }

        public override string GetDataForSign(Guid documentId)
        {
            throw new NotImplementedException();
        }
    }
}