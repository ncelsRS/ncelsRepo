using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure.Workflow
{
    public class SetWorkflowId : StepBody
    {
        public string AwaiterKey { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information($"SetWorkflowId: {context.Workflow.Id}");
            TaskCompletionService.TryReleaseTask(AwaiterKey, context.Workflow.Id);
            return ExecutionResult.Next();
        }

    }

    public class SendToNcels : StepBody
    {
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var pointerId = ""; // Helpers.GetUserTaskPointer(context);
            var pointer = context.Workflow.ExecutionPointers.Find(x => x.Id == pointerId);

            if (pointer.ExtensionAttributes.TryGetValue("ExecutorsIds", out var executorsValue))
                ExecutorsIds = executorsValue as Dictionary<string, IEnumerable<string>>;

            Log.Information("SendToNcels");
            return ExecutionResult.Next();
        }
    }

}
