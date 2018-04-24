using System.Threading.Tasks;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.Contract.Logic
{
    public interface IContractLogic : IBaseContractLogic
    {
        Task<object> Create();
    }
}