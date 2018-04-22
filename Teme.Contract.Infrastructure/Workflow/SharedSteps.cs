using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow
{
    public class CancelParallelUserTasks : StepBody
    {
        private IWorkflowHost _host { get; }

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

            });

            return ExecutionResult.Next();
        }
    }
}
