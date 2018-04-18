using Serilog;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure.ContractGv;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.WorkflowSteps;
using Teme.Shared.Data.Primitives.Contract;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure
{

    public class ContractWorkflow : IWorkflow<ContractWorkflowTransitionData>
    {
        public string Id => "Contract";

        public int Version => 0; // Test version

        public void Build(IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder
            .StartWith<SetWorkflowId>()
                .Input(step => step.AwaiterKey, data => data.Value)
            .UserTask("SendContract", data => "declarant")
                .WithOption(UserOptions.SendWithSign, UserOptions.SendWithSign).Do(then =>
                    then.StartWith(c => ExecutionResult.Next()).Output(d => d.IsSignedByDeclarant, s => true)
                )
                .WithOption(UserOptions.SendWithoutSign, UserOptions.SendWithoutSign).Do(then =>
                    then.StartWith(c => ExecutionResult.Next()).Output(d => d.IsSignedByDeclarant, s => false)
                )
            .If(d => d.ContractType == ContractTypeEnum.OneToOne).Do(then =>
                then.StartWith(c => { })
                    .Parallel()
                        .Do(t => t.ContractGv())
                        .Do(t => t.StartWith(c => ExecutionResult.Next()))
                    .Join()
            )
            .Then<RealiseAwaiter>()
                .Input(s => s.Value, d => d.Value);
        }
    }

}
