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
            TaskCompletionService.ReleaseTask(AwaiterKey, context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class SendWithoutSign : BaseContractStep
    {
        public bool IsSignedByDeclarant { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            IsSignedByDeclarant = false;
            TaskCompletionService.ReleaseTask(AwaiterKey, context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    public class SendWithSign : BaseContractStep
    {
        public bool IsSignedByDeclarant { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            IsSignedByDeclarant = true;
            TaskCompletionService.ReleaseTask(AwaiterKey, context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    
}
