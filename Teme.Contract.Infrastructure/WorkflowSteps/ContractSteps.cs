using Serilog;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure.WorkflowSteps
{
    public class SetWorkflowId : BaseContractStep
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information($"SetWorkflowId: {context.Workflow.Id}");
            TaskCompletionService.ReleaseTask(AwaiterKey, context.Workflow.Id);
            return ExecutionResult.Next();
        }

    }

    public class SendToNcels : BaseContractStep
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var prePoint = context.Workflow.ExecutionPointers.FirstOrDefault(x => x.Id == context.ExecutionPointer.PredecessorId);
            var eventData = prePoint.EventData as UserAction;
            //var data = eventData.Value;
            return ExecutionResult.Next();
        }
    }

}
