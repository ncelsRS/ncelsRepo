using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure
{
    public class Helpers
    {
        public static string GetUserTaskPointer(IStepExecutionContext context)
        {
            var scope = context.ExecutionPointer.Scope.ToArray();
            return scope[1];
        }
    }
}
