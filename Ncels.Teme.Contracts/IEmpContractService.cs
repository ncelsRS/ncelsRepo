using System;
using System.Collections.Generic;
using System.Linq;
using Ncels.Teme.Contracts.ViewModels;

namespace Ncels.Teme.Contracts
{
    public interface IEmpContractService
    {
        IQueryable<EmpContractViewModel> GetContracts();
        EmpContractDetailsViewModel GetContractDetailsViewModel(Guid contractId);
        IEnumerable<EmpContractPaymentViewModel> GetPayments();
        EmpContractPaymentDetailsViewModel GetPayment(Guid contractId);
        IEnumerable<EmpContractPaymentPriceViewModel> GetContractPrice(Guid contractId);
        void SendToWork(Guid stageId, Guid executorId);
        IEnumerable<EmpContractHistoryViewModel> GetContractHistory(Guid id);
        void Approve(Guid stageId, bool result);
        bool CanApprove(Guid stageId);
        string GetStageCode();
        void SendToAdjustment(Guid stageId);
        string RegisterContract(Guid contractId);
        object GetContractTemplatePdf(Guid contractId);
    }
}
