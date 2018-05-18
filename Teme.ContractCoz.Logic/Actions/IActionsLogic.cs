using System.Threading.Tasks;
using Teme.Shared.Logic;

namespace Teme.ContractCoz.Logic.Actions
{
    public interface IActionsLogic : IBaseLogic
    {
        Task<object> CozBossAgreementsRequest(string workflowId, bool agree);
        Task<object> CozCeoAgreementsRequest(string workflowId, bool agree);
        Task<object> CozExecutorAgreementsRequest(string workflowId, bool agree);
        Task<object> DistributionByExecutors(string workflowId, int userId);
        Task<object> ReturnToDeclarant(string workflowId);
        Task<object> RegisterContract(string workflowId);
    }
}
