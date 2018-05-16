using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.Contract;

namespace Ncels.Core.ActivityManager.DocumentActions
{
    public class ContractSigningByApplicantAndCeoAction : DocumentAction
    {
        private NotificationManager _notificationManager = new NotificationManager();
        public ContractSigningByApplicantAndCeoAction(ncelsEntities context) : base(context)
        {
        }

        public override Guid GetExecutor(Guid documentId, Guid activityTypeId, int orderNum)
        {
            var contract = _context.Contracts.FirstOrDefault(e => e.Id == documentId);
            return orderNum == 1 ? contract.OwnerId : contract.SignerId.Value;
        }

        public override void ProcessStarted(Guid documentId, EXP_Activities activity)
        {
            var contract = _context.Contracts.FirstOrDefault(e => e.Id == documentId);
            var contractOwner = _context.Employees.FirstOrDefault(e => e.Id == contract.OwnerId);
            contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                Contract.StatusOnApplicantSigningt);
            _context.SaveChanges();
            _notificationManager.SendNotification("Вам отправлен договор для подписания ЭЦП", ObjectType.Contract,
                documentId, contractOwner.Id, contractOwner.Email);
        }

        public override void TaskExecuted(Guid documentId, EXP_Tasks task)
        {
            if (task.OrderNumber == 1)
            {
                var contract = _context.Contracts.FirstOrDefault(e => e.Id == documentId);
                var signer = _context.Employees.FirstOrDefault(e => e.Id == task.ExecutorId);
                contract.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ContractStatus.DicCode,
                    Contract.StatusOnSigning);
                _context.SaveChanges();
                _notificationManager.SendNotification("Вам отправлен договор для подписания ЭЦП", ObjectType.Contract,
                    documentId, signer.Id, signer.Email);
            }
        }

        public override void TaskRejected(Guid documentId, EXP_Tasks task)
        {
        }

        public override void TaskRepealed(Guid documentId, EXP_Tasks task)
        {
        }

        public override void ProcessComplited(Guid documentId, EXP_Activities activity)
        {
            var contractRepository=new ContractRepository(_context);
            contractRepository.SendToRegistration(documentId);
            _context.SaveChanges();

        }

        public override string GetDataForSign(Guid documentId)
        {
            throw new NotImplementedException();
        }
    }
}