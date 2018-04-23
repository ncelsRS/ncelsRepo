using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow
{
    public class CancelParallelUserTasks : StepBody
    {
        private readonly IWorkflowHost _host;

        public CancelParallelUserTasks(IWorkflowHost host)
        {
            _host = host;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var pointers = context.Workflow.ExecutionPointers;
            var forEachStep = pointers.Find(x => x.Id == context.ExecutionPointer.Scope.ToArray()[3]);
            forEachStep.Children.ForEach(child =>
            {
                
                _host.GetOpenUserActions(context.Workflow.Id);
            });

            return ExecutionResult.Next();
        }
    }
}
