using System;
using System.Linq;
using Ncels.Teme.Contracts;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace Ncels.Teme.Infrastructure
{
    public class EmpContractStageHistoryHandler
    {
        private IUnitOfWork _uow;

        public EmpContractStageHistoryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void SetStageApproved(EMP_ContractStage stage)
        {
            stage.StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.Approved).Select(x => x.Id)
                .FirstOrDefault();
        }

        public void SetStageRejected(EMP_ContractStage stage)
        {
            stage.StageStatusId = _uow.GetQueryable<EMP_Ref_StageStatus>()
                .Where(x => x.Code == CodeConstManager.EmpContractStageStatus.NotApproved).Select(x => x.Id)
                .FirstOrDefault();
        }

        public void AddHistoryRegistered(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Registered;
            AddHistory(contractId, historyStatusCode);
        }

        public void AddHistoryReturnedToAdjustment(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Returned;
            AddHistory(contractId, historyStatusCode);
        }

        public void AddHistorySentToWork(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.SentToWork;
            AddHistory(contractId, historyStatusCode);
        }

        public void AddHistoryApproved(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Approved;
            AddHistory(contractId, historyStatusCode);
        }

        public void AddHistoryApproveRequired(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.SentToApproval;
            AddHistory(contractId, historyStatusCode);
        }

        public void AddHistoryRejected(Guid contractId)
        {
            var historyStatusCode = OBK_Ref_ContractHistoryStatus.Refused;
            AddHistory(contractId, historyStatusCode);
        }

        private void AddHistory(Guid contractId, string historyStatusCode, string reason = null)
        {
            var currentEmployee = UserHelper.GetCurrentEmployee();

            var status = GetContractHistoryStatusByCode(historyStatusCode);

            var unitName = GetParentUnitName(currentEmployee);

            var history = new EMP_ContractHistory()
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                RefuseReason = reason,
                ContractId = contractId,
                EmployeeId = currentEmployee.Id,
                UnitName = unitName,
                StatusId = status.Id,
            };
            _uow.Insert(history);
        }

        public OBK_Ref_ContractHistoryStatus GetContractHistoryStatusByCode(string code)
        {
            return _uow.GetQueryable<OBK_Ref_ContractHistoryStatus>().Where(x => x.Code == code).FirstOrDefault();
        }

        private string GetParentUnitName(Employee employee)
        {
            string unitName = null;
            if (employee.Units != null && employee.Units.Count > 0)
            {
                foreach (var unit in employee.Units)
                {
                    if (unit.Parent != null)
                    {
                        unitName = unit.Parent.ShortName;
                    }
                    break;
                }
            }
            return unitName;
        }
    }
}