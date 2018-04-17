using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure
{
    public abstract class BaseContractStep : StepBody
    {
        public string AwaiterKey { get; set; }
    }
    public abstract class BaseContractStepAsync : StepBodyAsync
    {
        public string AwaiterKey { get; set; }
    }

    public class RealiseAwaiter : BaseContractStep
    {
        public object Value { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            AwaiterKey = (Value as ContractWorkflowEventData).AwaiterKey;
            TaskCompletionService.ReleaseTask(AwaiterKey);
            return ExecutionResult.Next();
        }
    }
}
