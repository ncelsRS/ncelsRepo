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
            Log.Information($"Setted executor");
            return ExecutionResult.Next();
        }
    }
}
