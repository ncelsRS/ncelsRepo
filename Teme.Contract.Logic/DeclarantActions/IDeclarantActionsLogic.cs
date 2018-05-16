using System.Threading.Tasks;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Logic;

namespace Teme.Contract.Logic.DeclarantActions
{
    public interface IDeclarantActionsLogic : IBaseLogic
    {
        Task<object> SendOrRemoveDelete(string workflowId, ContractTypeEnum contractType);
        Task<object> SendOrRemoveSendWithoutSign(string workflowId, ContractTypeEnum contractType);
        Task<object> SendOrRemoveSendWithSign(string workflowId, ContractTypeEnum contractType);
    }
}
