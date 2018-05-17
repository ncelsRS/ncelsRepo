using System.Threading.Tasks;
using Teme.Shared.Logic;

namespace Teme.ContractCoz.Logic.PaymentActions
{
    public interface IPaymentActionLogic : IBaseLogic
    {
        Task<object> CreatePayment();
        Task<object> DefExecutorAgreementsRequest(string workflowId, bool agree);
        Task<object> GvBossAgreementsRequest(string workflowId, bool agree);
        Task<object> GvExecutorAgreementsRequest(string workflowId, bool agree);
        Task<object> RegisterPayment(string workflowId);
        Task<object> SendPaymentToNcels(string workflowId);
        Task<object> SendToGvExecutor(string workflowId, int userId);
    }
}
