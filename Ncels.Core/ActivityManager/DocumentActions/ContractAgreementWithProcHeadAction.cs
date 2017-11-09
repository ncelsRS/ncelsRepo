using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace Ncels.Core.ActivityManager.DocumentActions
{
    public class ContractAgreementWithProcHeadAction : DocumentAction
    {
        public ContractAgreementWithProcHeadAction(ncelsEntities context) : base(context)
        {
        }

        public override Guid GetExecutor(Guid documentId, Guid activityTypeId, int orderNum)
        {
            var procSettings = _context.ContractProcSettings.FirstOrDefault();
            if (procSettings != null) return procSettings.ProcCenterHeadId;
            return Guid.NewGuid();
        }

        public override void ProcessStarted(Guid documentId, EXP_Activities activity)
        {
            var contract = _context.Contracts.FirstOrDefault(e => e.Id == documentId);
            contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                Contract.StatusOnAgreement);
            _context.SaveChanges();
        }

        public override void TaskExecuted(Guid documentId, EXP_Tasks task)
        {
            var contract = _context.Contracts.FirstOrDefault(e => e.Id == documentId);
            contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                Contract.StatusApproved);
            _context.SaveChanges();
        }

        public override void TaskRejected(Guid documentId, EXP_Tasks task)
        {
            var contract = _context.Contracts.FirstOrDefault(e => e.Id == documentId);
            contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                Contract.StatusCorrected);
            _context.SaveChanges();
        }
        public override void TaskRepealed(Guid documentId, EXP_Tasks task)
        {
            
        }

        public override void ProcessComplited(Guid documentId, EXP_Activities activity)
        {
        }

        public override string GetDataForSign(Guid documentId)
        {
            throw new NotImplementedException();
        }
    }
}