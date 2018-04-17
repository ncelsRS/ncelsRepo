using Teme.Contract.Infrastructure.ContractCoz;
using WorkflowCore.Interface;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure.ContractCoz
{
    public static class ContractWorkflowCoz
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> ContractCoz(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder
                .StartWith<BossCoz>()
                .UserTask("SendContractToLegalAdviser", data => data.ExecutorId)
                    .WithOption("", "sendToCozExecutor").Do(then => then
                        .StartWith<SendContractToCozExecutor>()
                            .Input(step => step.ExecutorId, data => data.ExecutorId)
                    )
                .UserTask("LegalAdviserEvents", data => data.ExecutorId)
                    .WithOption("", "").Do(x => x
                        .StartWith<SendContractToCozExecutor>()
                        )
                    .WithOption("", "").Do(x => x
                        .StartWith<SendContractToCozExecutor>()
                        );
            return builder;
        }
    }
}
