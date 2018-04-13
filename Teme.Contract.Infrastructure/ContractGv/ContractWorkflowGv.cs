using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.ContractGv
{
    public static class ContractWorkflowGv
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> ContractGv(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => ExecutionResult.Next())
                .UserTask("Set executor", data => data.ExecutorId)
                    .WithOption("setExecuter", "setExecuter").Do(then => then
                        .StartWith<GvSetExecuter>()
                    );
            return builder;
        }
    }
}