using System.Threading.Tasks;

namespace Teme.Contract.Infrastructure.Workflow
{
    public interface IStepStatusLogic
    {
        Task SendToNcels(bool isSignedByDeclarant, string workflowId);
    }
}