using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Notifications;

namespace Ncels.Core.ActivityManager.DocumentActions
{
    public class ExpertiseTranslateAgreementAction : DocumentAction
    {
        private const string ON_AGREEMENT_CODE = "ON_AGREEMENT";//На согласовании
        private const string AGREED_CODE = "AGREED";//Согласован
        private const string REFUSED_CODE = "REFUSED";//Отказ
        public ExpertiseTranslateAgreementAction(ncelsEntities context) : base(context)
        {
        }
        public override Guid GetExecutor(Guid documentId, Guid activityTypeId, int orderNum)
        {
            throw new NotImplementedException();
        }

        public override void ProcessStarted(Guid documentId, EXP_Activities activity)
        {
            var letter = _context.FileLinks.FirstOrDefault(e => e.Id == documentId);
            if (letter == null)
            {
                   return; 
            }
            var status = _context.DIC_FileLinkStatus.FirstOrDefault(e => e.Code == ON_AGREEMENT_CODE);

            letter.DIC_FileLinkStatus = status;
            var task = activity.EXP_Tasks.FirstOrDefault();
            _context.SaveChanges();
            var notificationManager = new NotificationManager();
            notificationManager.SendNotification("Вам поступило письмо на согласование", ObjectType.Translate, documentId, task.ExecutorId);
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
            var letter = _context.FileLinks.FirstOrDefault(e => e.Id == documentId);
            var positiveStatus = DictionaryHelper.GetDicIdByCode(Dictionary.ExpActivityStatus.DicCode,
                Dictionary.ExpActivityStatus.Executed);
            if (activity.StatusId == positiveStatus)
                letter.DIC_FileLinkStatus = _context.DIC_FileLinkStatus.FirstOrDefault(e => e.Code == AGREED_CODE);
            else
                letter.DIC_FileLinkStatus = _context.DIC_FileLinkStatus.FirstOrDefault(e => e.Code == REFUSED_CODE);
        }

        public override string GetDataForSign(Guid documentId)
        {
            throw new NotSupportedException();
        }
    }
}