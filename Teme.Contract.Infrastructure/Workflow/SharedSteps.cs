using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow
{
    public class CancelParallelUserTasks : StepBodyAsync
    {
        private readonly IWorkflowHost _host;

        public CancelParallelUserTasks(IWorkflowHost host)
        {
            _host = host;
        }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var pointers = context.Workflow.ExecutionPointers;
            var forEachStep = pointers.Find(x => x.Id == context.ExecutionPointer.Scope.ToArray()[3]);
            forEachStep.Children.ForEach(async child =>
            {
                await _host.GetOpenUserActions(context.Workflow.Id);
            });

            return ExecutionResult.Next();
        }
    }
}