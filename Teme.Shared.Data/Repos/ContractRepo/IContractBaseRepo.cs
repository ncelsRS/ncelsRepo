using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.ContractRepo
{
    public interface IContractBaseRepo : IBaseRepo<Contract>
    {
        Task SaveStatePolice(List<StatePolicy> statePolicies);
        Task<List<StatePolicy>> GetListStatePolice(int contractId);
        Task RemoveStatePolice(List<StatePolicy> list);
    }
}