using Serilog;
using Teme.Contract.Infrastructure.WorkflowSteps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.ContractGv
{
    public class GvSetExecuter : BaseContractStep
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var data = GetEventData(context);
            TaskCompletionService.ReleaseTask(data.AwaiterKey);
            var id = data.Value as string;
            Log.Information($"Setted executor: {id}");
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
