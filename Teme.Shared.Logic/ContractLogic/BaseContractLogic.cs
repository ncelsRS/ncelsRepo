using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.Shared.Logic.ContractLogic
{
    public class BaseContractLogic<TIRepo> : BaseLogic<TIRepo, Data.Context.Contract>, IBaseContractLogic where TIRepo : IContractBaseRepo
    {
        public BaseContractLogic(TIRepo repo) : base(repo)
        {
        }
    }
}
