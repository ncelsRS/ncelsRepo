using Serilog;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.ContractGv
{
    public class GvSetExecuter : StepBody
    {
        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Log.Information($"Setted executor");
            return ExecutionResult.Next();
        }
    }
}
