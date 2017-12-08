using System;
using System.Linq;
using Ncels.Teme.Contracts.ViewModels;

namespace Ncels.Teme.Contracts
{
    public interface IEmpContractService
    {
        IQueryable<EmpContractViewModel> GetContracts();

        EmpContractDetailsViewModel GetContractDetailsViewModel(Guid contractId);
    }
}
