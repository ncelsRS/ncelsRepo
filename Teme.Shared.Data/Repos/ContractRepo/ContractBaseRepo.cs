using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.ContractRepo
{
    public abstract class ContractBaseRepo : BaseRepo<Contract>, IContractBaseRepo
    {
        protected ContractBaseRepo(TemeContext context) : base(context)
        {
        }
    }
}