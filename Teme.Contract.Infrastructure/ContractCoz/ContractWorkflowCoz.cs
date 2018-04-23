using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.ContractCoz
{
    public static class ContractWorkflowCoz
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> ContractCoz(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            //builder
            //    .StartWith<BossCoz>()
            //        .Input(step => step.AwaiterKey, data => data.Value);
                // Назначение Юристконсульта
                //.UserTask("SendContractToLegalAdviser", data => "legalAdviser")
                //    .WithOption("sendToCozExecutor", "sendToCozExecutor").Do(then =>
                //        then.StartWith(c => ExecutionResult.Next())
                //    );
                    //.WithOption(UserOptions.SendWithSign, UserOptions.SendWithSign).Do(then =>
                    //    then.StartWith(c => ExecutionResult.Next()).Output(d => d.IsSignedByDeclarant, s => true)
                    //)
                //// Дествия юристконсульта
                //.UserTask("LegalAdviserEvents", data => data.ExecutorId)
                //    .WithOption("", "").Do(x => x
                //        .StartWith<SendContractToCozExecutor>()
                //        )
                //    .WithOption("", "").Do(x => x
                //        .StartWith<SendContractToCozExecutor>()
                //        );
            return builder;
        }
    }
}
