using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Users.Primitives
{
    public class UserTaskStep : WorkflowStep<UserTask>
    {

        public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

        public List<EscalateStep> Escalations { get; set; } = new List<EscalateStep>();

        public override IStepBody ConstructBody(IServiceProvider serviceProvider)
        {
            return new UserTask(Options, Escalations);
        }

        public override void AfterExecute(WorkflowExecutorResult executorResult, IStepExecutionContext context, ExecutionResult stepResult, ExecutionPointer executionPointer)
        {
            base.AfterExecute(executorResult, context, stepResult, executionPointer);
            if (executorResult.Subscriptions.Count > 0) // TODO Сделать более красиво
                TaskCompletionService.TryReleaseTask(context.Workflow.Id);
        }
    }
}