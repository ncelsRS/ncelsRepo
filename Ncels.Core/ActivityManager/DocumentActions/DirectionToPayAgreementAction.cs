using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.DirectionToPay;

namespace Ncels.Core.ActivityManager.DocumentActions
{
    public class DirectionToPayAgreementAction : DocumentAction
    {
        public DirectionToPayAgreementAction(ncelsEntities context) : base(context)
        {
        }

        public override Guid GetExecutor(Guid documentId, Guid activityTypeId, int orderNum)
        {
            //return Guid.NewGuid();

            const string ai = "abdukayumov.i"; //"19c786af-da7a-499d-b9ef-f17055029e3e";
            const string nep = "nesipbaeva.g"; //2E9F5803-DE7F-4784-A391-FAE9C9E4E74D

            DirectionToPayRepository repository = new DirectionToPayRepository(false);
            var directionToPay = repository.GetAsQuarable(d => d.Id == documentId).FirstOrDefault();

            if (directionToPay == null)
            {
                return Guid.Empty;
            }
            if (directionToPay.Type == EXP_DirectionToPaysView.ExpertWorkType)
            {
                var emp = repository.GetEmployList().FirstOrDefault(e => e.Login == ai);
                if (emp != null) return emp.Id;
            }
            else
            {
                var emp = repository.GetEmployList().FirstOrDefault(e => e.Login == nep);
                if (emp != null) return emp.Id;
            }
            
            return directionToPay.CreateEmployeeId;
        }

        public override void ProcessStarted(Guid documentId, EXP_Activities activity)
        {
            var directionToPay = _context.EXP_DirectionToPays.FirstOrDefault(e => e.Id == documentId);
            if (directionToPay != null)
                directionToPay.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ExpDirectionToPayStatus.DicCode,
                    Dictionary.ExpDirectionToPayStatus.OnAgreement);

            _context.SaveChanges();
        }

        public override void TaskExecuted(Guid documentId, EXP_Tasks task)
        {
            var directionToPay = _context.EXP_DirectionToPays.FirstOrDefault(e => e.Id == documentId);
            if (directionToPay != null && task.EXP_TasksChild == null)
                directionToPay.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ExpDirectionToPayStatus.DicCode,
                    Dictionary.ExpDirectionToPayStatus.Agreed);

            _context.SaveChanges();
        }

        public override void TaskRejected(Guid documentId, EXP_Tasks task)
        {
            var directionToPay = _context.EXP_DirectionToPays.FirstOrDefault(e => e.Id == documentId);
            if (directionToPay != null)
                directionToPay.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ExpDirectionToPayStatus.DicCode,
                    Dictionary.ExpDirectionToPayStatus.OnСorrection);

            _context.SaveChanges();
        }

        public override void TaskRepealed(Guid documentId, EXP_Tasks task)
        {
            var directionToPay = _context.EXP_DirectionToPays.FirstOrDefault(e => e.Id == documentId);
            if (directionToPay != null)
                directionToPay.StatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ExpDirectionToPayStatus.DicCode,
                    Dictionary.ExpDirectionToPayStatus.Canceled);

            _context.SaveChanges();
        }

        public override void ProcessComplited(Guid documentId, EXP_Activities activity)
        {
            var directionToPay = _context.EXP_DirectionToPays.FirstOrDefault(e => e.Id == documentId);
            if (directionToPay != null)
            {
                var positiveStatusId = DictionaryHelper.GetDicIdByCode(Dictionary.ExpActivityStatus.DicCode,
                    Dictionary.ExpActivityStatus.Executed);
                if (activity.StatusId == positiveStatusId)
                    directionToPay.StatusId = DictionaryHelper.GetDicIdByCode(
                        Dictionary.ExpDirectionToPayStatus.DicCode,
                        Dictionary.ExpDirectionToPayStatus.SendedTo1C);
            }

            _context.SaveChanges();
        }

        public override string GetDataForSign(Guid documentId)
        {
            throw new NotImplementedException();
        }
    }
}