using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Infrastructure.WorkflowSteps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.ContractCoz.Steps
{
    public class BossCoz : BaseContractStep
    {
        public string ExecutorId { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var awaiterKey = GetEventData(context).AwaiterKey;
            Log.Information($"BossCoz: {awaiterKey}");
            Log.Information($"BossCoz ExecutorId : {ExecutorId}");
            return ExecutionResult.Next();
        }
    }

    public class SendContractToCozExecutor : BaseContractStep
    {
        public string ExecutorId { get; set; }
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var awaiterKey = GetEventData(context).AwaiterKey;
            Log.Information($"SendContractToCozExecutor: {awaiterKey}");
            Log.Information($"SendContractToCozExecutor ExecutorId : {ExecutorId}");
            return ExecutionResult.Next();
        }
    }
}
