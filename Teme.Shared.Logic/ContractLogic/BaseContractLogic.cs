using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.Shared.Logic.ContractLogic
{
    public class BaseContractLogic<TIRepo> : BaseLogic<TIRepo, Contract>, IBaseContractLogic where TIRepo : IContractBaseRepo
    {
        protected BaseContractLogic(TIRepo repo) : base(repo)
        {
        }
    }
}