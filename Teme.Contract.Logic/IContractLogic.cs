using System.Threading.Tasks;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.Contract.Logic
{
    public interface IContractLogic : IBaseContractLogic
    {
        Task<string> Create();
        Task<object> SaveModel(int contractId, string workflowId, string code, string fieldName, string fieldValue);
    }
}