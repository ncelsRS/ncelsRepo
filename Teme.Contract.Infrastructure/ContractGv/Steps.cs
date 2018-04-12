using Serilog;
using Teme.Contract.Infrastructure.WorkflowSteps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.ContractGv
{
    public class SetGvBossUserTask : BaseContractStep
    {
        public string ExecuterId { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information($"SetGvBossUserTask: {ExecuterId}");
            ExecuterId = "BossGv";

            TaskCompletionService.ReleaseTask(AwaiterKey);
            return ExecutionResult.Next();
        }
    }

    public class SetExecuterStart : BaseContractStep
    {
        public string ExecutorId { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information($"SetExecuterStart: {ExecutorId}");

            TaskCompletionService.ReleaseTask(AwaiterKey, ExecutorId);
            return ExecutionResult.Next();
        }
    }
}
