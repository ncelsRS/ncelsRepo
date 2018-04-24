using System.Threading.Tasks;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Contract.Data
{
    public interface IContractRepo
    {
        Task<ContractTypeEnum> GetContractType(int contractId);
    }
}