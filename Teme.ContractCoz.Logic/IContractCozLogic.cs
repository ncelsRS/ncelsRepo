using System.Threading.Tasks;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.ContractCoz.Logic
{
    public interface IContractCozLogic : IBaseContractLogic
    {
        Task<object> GetContractById(int contractId);
        Task<object> GetDeclarantById(int id);
    }
}
