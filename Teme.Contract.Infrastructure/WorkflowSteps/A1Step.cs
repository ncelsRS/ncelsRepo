using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure.WorkflowSteps
{
    public abstract class BaseContractStep : StepBody
    {
        public string AwaiterKey { get; set; }

        protected ContractWorkflowEventData GetEventData(IStepExecutionContext context)
        {
            var id = context.ExecutionPointer.Scope.Peek();
            if (id == null) return null;
            var data = context.Workflow.ExecutionPointers.Find(x => x.Id == id).EventData as UserAction;
            return data.Value as ContractWorkflowEventData;
        }
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
            var awaiterKey = GetEventData(context).AwaiterKey;
            Log.Information($"SendWithoutSign: {awaiterKey}");
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
            //TaskCompletionService.ReleaseTask(AwaiterKey, context.Workflow.Id);
            return ExecutionResult.Next();
        }
    }

    
}
