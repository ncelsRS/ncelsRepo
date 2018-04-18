using Serilog;
using System.Collections.Generic;
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
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var scope = context.ExecutionPointer.Scope.ToArray();
            var pointerId = scope[scope.Length - 2];
            var pointer = context.Workflow.ExecutionPointers.Find(x => x.Id == pointerId);

            ExecutorsIds = pointer.ExtensionAttributes.ToList().Where(x => x.Key == "ExecutorsIds") as Dictionary<string, IEnumerable<string>>;

            Log.Information("SendToNcels");
            return ExecutionResult.Next();
        }
    }

}
