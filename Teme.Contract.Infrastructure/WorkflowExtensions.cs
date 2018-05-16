using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure
{
    public static class WorkflowExtensions
    {
        public static ExecutionPointer GetParentScope(this IStepExecutionContext context, int level = 0)
        {
            if (context.ExecutionPointer.Scope.Count <= level) return null;
            return context.Workflow.ExecutionPointers.Find(x =>
                context.ExecutionPointer.Scope.Contains(x.Id) && x.EventData != null);
        }
    }
}
