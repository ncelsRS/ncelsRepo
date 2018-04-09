using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.ContractGv
{
    public static class ContractWorkflowGv
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> ContractGv(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder
                .StartWith<SetGvBossUserTask>()
                    .Input(step => step.AwaiterKey, data => data.AwaiterKey)
                    .Output(d => d.ExecutorId, s => s.ExecuterId)
                .UserTask("Set executor", data => data.ExecutorId)
                    .WithOption("setExecuter", "setExecuter").Do(then => then
                        .StartWith<SetExecuterStart>()
                    );
            return builder;
        }
    }
}