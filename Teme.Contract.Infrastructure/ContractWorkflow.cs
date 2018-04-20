using Serilog;
using System;
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
            .UserTask("SendContract", (d, c) => "declarant")
                .WithOption(UserOptions.SendWithSign, "o1").Do(then =>
                    then.StartWith<SendToNcels>()
                        .Output(d => d.IsSignedByDeclarant, s => true)
                        .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                )
                .WithOption(UserOptions.SendWithoutSign, "o2").Do(then =>
                    then.StartWith<SendToNcels>()
                        .Output(d => d.IsSignedByDeclarant, s => false)
                        .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                )
            .If(d => d.ContractType == ContractTypeEnum.OneToOne).Do(then =>
                then.StartWith(c => { })
                    .Parallel()
                        .Do(t => t.StartWith(c => Log.Information("StartEndCoz")))
                        .Do(t => t.ContractGv())
                    .Join()
            )
            .If(d => d.ContractType == ContractTypeEnum.OneToMore).Do(t => t.StartWith(c => { }));
        }
    }

}
