using System.Threading.Tasks;

namespace Teme.Contract.Infrastructure.Workflow
{
    public interface IStepStatusLogic
    {
        Task SendToNcels(string workflowId);
        Task DeleteContract(string workflowId);
        Task SelectExecutorsFirst(string workflowId);
        Task CozExecutorMeetReq(string workflowId);
    }
}
