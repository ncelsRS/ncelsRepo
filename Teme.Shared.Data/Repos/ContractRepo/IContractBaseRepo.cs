using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.ContractRepo
{
    public interface IContractBaseRepo : IBaseRepo<Contract>
    {
        Task SaveStatePolice(List<StatePolicy> statePolicies, int contractId);
    }
}