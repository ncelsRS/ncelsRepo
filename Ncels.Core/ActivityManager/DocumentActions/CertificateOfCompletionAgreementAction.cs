using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.CertificateOfCompletion;

namespace Ncels.Core.ActivityManager.DocumentActions
{
    public class CertificateOfCompletionAgreementAction : DocumentAction
    {
        public CertificateOfCompletionAgreementAction(ncelsEntities context) : base(context)
        {
        }

        public override Guid GetExecutor(Guid documentId, Guid activityTypeId, int orderNum)
        {
            CertificateOfCompletionRepository repository = new CertificateOfCompletionRepository(false);
            //var act = repository.GetAsQuarable(coc => coc.Id == documentId).FirstOrDefault();
            
            
            return Guid.NewGuid();
        }

        public override void ProcessStarted(Guid documentId, EXP_Activities activity)
        {
            var coc = _context.EXP_CertificateOfCompletion.FirstOrDefault(e => e.Id == documentId);
            if (coc != null)
            {
                coc.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.CertificateOfCompletionStatusDic.DicCode,
                    Dictionary.CertificateOfCompletionStatusDic.OnAgreement);
                coc.SendDate = DateTime.Now;
            }

            _context.SaveChanges();
        }

        public override void TaskExecuted(Guid documentId, EXP_Tasks task)
        {
            var coc = _context.EXP_CertificateOfCompletion.FirstOrDefault(e => e.Id == documentId);
            if (coc != null)
            {
                coc.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.CertificateOfCompletionStatusDic.DicCode,
                    Dictionary.CertificateOfCompletionStatusDic.Sended);
            }

            _context.SaveChanges();
        }

        public override void TaskRejected(Guid documentId, EXP_Tasks task)
        {
            var coc = _context.EXP_CertificateOfCompletion.FirstOrDefault(e => e.Id == documentId);
            if (coc != null)
                coc.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.CertificateOfCompletionStatusDic.DicCode,
                    Dictionary.CertificateOfCompletionStatusDic.Rejected);

            _context.SaveChanges();
        }

        public override void TaskRepealed(Guid documentId, EXP_Tasks task)
        {
            
        }

        public override void ProcessComplited(Guid documentId, EXP_Activities activity)
        {
            var coc = _context.EXP_CertificateOfCompletion.FirstOrDefault(e => e.Id == documentId);
            if (coc != null)
            {
                var positiveStatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ExpActivityStatus.DicCode,
                    Dictionary.ExpActivityStatus.Executed);
                if (activity.StatusId == positiveStatusId)
                    coc.StatusId = DictionaryHelper.GetDicIdByCode(
                        Dictionary.CertificateOfCompletionStatusDic.DicCode,
                        Dictionary.CertificateOfCompletionStatusDic.Sended);
            }

            _context.SaveChanges();
        }

        public override string GetDataForSign(Guid documentId)
        {
            throw new NotImplementedException();
        }
    }
}