using Serilog;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.WorkflowSteps
{
    public abstract class BaseContractStep : StepBody
    {
        public string AwaiterKey { get; set; }
    }

    public abstract class BaseContractStepAsync : StepBodyAsync
    {
        public string AwaiterKey { get; set; }
    }

    public class SetWorkflowId : BaseContractStep
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information($"SetWorkflowId: {context.Workflow.Id}");
            TaskCompletionService.ReleaseTask(AwaiterKey, context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class SendWithoutSign : BaseContractStep
    {
        public bool IsSignedByDeclarant { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information($"SendWithoutSign");
            IsSignedByDeclarant = false;
            return ExecutionResult.Next();
        }
    }

    public class SendWithSign : BaseContractStep
    {
        public bool IsSignedByDeclarant { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information($"SendWithSign");
            IsSignedByDeclarant = true;
            TaskCompletionService.ReleaseTask(AwaiterKey, context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    
}
