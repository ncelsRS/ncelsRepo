using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Data.Context;
using Teme.Shared.Logic;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.Shared.Logic.ContractLogic
{
    public interface IContractStatePolicyLogic : IBaseLogic
    {
        List<StatePolicy> GetStatePolicy(string stage, int contractId);
    }
}