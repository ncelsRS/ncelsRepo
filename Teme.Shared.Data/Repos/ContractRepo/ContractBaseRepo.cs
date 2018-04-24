using Teme.Shared.Data.Context;

namespace Teme.Shared.Data.Repos.ContractRepo
{
    public class ContractBaseRepo : BaseRepo<Contract>, IContractBaseRepo
    {
        public ContractBaseRepo(TemeContext context) : base(context)
        {
        }
    }
}