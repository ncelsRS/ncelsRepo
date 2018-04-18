using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Infrastructure.WorkflowSteps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.ContractCoz
{
    public class BossCoz : BaseContractStep
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            return ExecutionResult.Next();
        }
    }

    public class SendContractToCozExecutor : BaseContractStep
    {
        public string ExecutorId { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            //var data = GetEventData(context);
            //TaskCompletionService.ReleaseTask(data.AwaiterKey);
            //var id = data.Value as string;
            //Log.Information($"Setted executor: {id}");
            return ExecutionResult.Next();
        }
    }
}
