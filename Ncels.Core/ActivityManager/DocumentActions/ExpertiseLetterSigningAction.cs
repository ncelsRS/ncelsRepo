using System;
using System.Linq;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.Expertise;

namespace Ncels.Core.ActivityManager.DocumentActions
{
    public class ExpertiseLetterSigningAction : DocumentAction
    {
        public ExpertiseLetterSigningAction(ncelsEntities context) : base(context)
        {
        }

        public override Guid GetExecutor(Guid documentId, Guid activityTypeId, int orderNum)
        {
            throw new NotImplementedException();
        }

        public override void ProcessStarted(Guid documentId, EXP_Activities activity)
        {
            var letter = _context.EXP_DrugCorespondence.FirstOrDefault(e => e.Id == documentId);
            letter.StatusId = DictionaryHelper.GetDicIdByCode(CodeConstManager.STATUS_ONSIGNING, CodeConstManager.STATUS_ONSIGNING);
            var task = activity.EXP_Tasks.FirstOrDefault();
            _context.SaveChanges();
            var notificationManager = new NotificationManager();
            notificationManager.SendNotification("Вам поступило письмо на подписанние", ObjectType.Letter, documentId, task.ExecutorId);
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
            var letter = _context.EXP_DrugCorespondence.FirstOrDefault(e => e.Id == documentId);
            var declaration = _context.EXP_DrugDeclaration.FirstOrDefault(e => e.Id == letter.DrugDeclarationId);
            var positiveStatus = DictionaryHelper.GetDicIdByCode(Dictionary.ExpActivityStatus.DicCode,
                Dictionary.ExpActivityStatus.Executed);
            if (activity.StatusId == positiveStatus)
            {
                letter.StatusId =
                    DictionaryHelper.GetDicIdByCode(CodeConstManager.STATUS_SIGNED, CodeConstManager.STATUS_SIGNED);
                if (letter.EXP_DIC_CorespondenceSubject.Code == EXP_DIC_CorespondenceSubject.RefuseByPayment)
                {
                    var stageRepo = new ExpertiseStageRepository(_context);
                    var stage = stageRepo.GetStage(declaration.Id, CodeConstManager.STAGE_PRIMARY);
                    stageRepo.RefuseExpertise(stage.Id, false);
                }
            }
            else
            {
                letter.StatusId =
                    DictionaryHelper.GetDicIdByCode(CodeConstManager.STATUS_REJECTED, CodeConstManager.STATUS_REJECTED);
                if (letter.EXP_DIC_CorespondenceSubject.Code == EXP_DIC_CorespondenceSubject.RefuseByPayment)
                    declaration.StatusId = CodeConstManager.STATUS_EXP_SEND_ID;
            }
        }

        public override string GetDataForSign(Guid documentId)
        {
            var letter = _context.EXP_DrugCorespondence.FirstOrDefault(e => e.Id == documentId);
            var dataForSign = new DrugCorespondenceSignData()
            {
                LetterId = letter.Id,
                LetterNumber = letter.NumberLetter,
                LetterDate = letter.DateCreate
            };
            var xmlData = SerializeHelper.SerializeDataContract(dataForSign);
            return xmlData.Replace("utf-16", "utf-8");
        }
    }
}