using System.Threading.Tasks;

namespace Teme.Contract.Infrastructure.Workflow
{
    public interface IStepStatusLogic
    {
        Task SendToNcels(string workflowId);
        Task DeleteContract(string workflowId);
        Task SelectExecutorsFirst(string workflowId);
        Task CozExecutorMeetReq(string workflowId);
        Task CozExecutorNotMeetReq(string workflowId);
        Task CozBossMeetReq(string workflowId);
        Task CozBossNotMeetReq(string workflowId);
        Task CeoMeetReq(string workflowId);
        Task CeoNotMeetReq(string workflowId);
        Task CozReturnToDeclarant(string workflowId);
        Task RegisterContract(string workflowId);
        Task SendPaymentToNcels(string workflowId);
        Task SelectPaymentExecutors(string workflowId);
        Task GvExecutorMeet(string workflowId);
        Task GvExecutorNotMeet(string workflowId);
        Task GvBossMeet(string workflowId);
        Task GvBossNotMeet(string workflowId);
        Task DefExecutorMeet(string workflowId);
        Task DefExecutorNotMeet(string workflowId);
        Task RegisterPayment(string workflowId);
    }
}
