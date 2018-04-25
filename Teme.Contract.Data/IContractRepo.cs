using System.Threading.Tasks;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos.ContractRepo;

namespace Teme.Contract.Data
{
    public interface IContractRepo : IContractBaseRepo
    {
        Task<ContractTypeEnum> GetContractType(int contractId);
        Task<Shared.Data.Context.Contract> GetContract(int id);
    }
}