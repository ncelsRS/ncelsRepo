using Serilog;
using System.Linq;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using Teme.Contract.Infrastructure.Workflow.ContractGv;
using Teme.Shared.Data.Primitives.Contract;
using WorkflowCore.Interface;
using WorkflowCore.Users;

namespace Teme.Contract.Infrastructure.Workflow
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
            .UserTask(UserPromts.Declarant.SendToNcels, (d, c) => "declarant")
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
                then.StartWith(c => Log.Verbose("Start Coz and Gv"))
                    .Parallel()
                        .Do(t => t.Coz())
                        .Do(t => t.Gv())
                    .Join()
            )
            .If(d => d.ContractType == ContractTypeEnum.OneToMore).Do(t => t.Coz())
            .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Root].First())
                .WithOption(UserOptions.MeetRequirements)
                    .Do(t => { })
                .WithOption(UserOptions.NotMeetRequirements)
                    .Do(t => { })
            .If(d => d.IsSignedByDeclarant).Do(t => t
                .StartWith(c => { Log.Verbose("Sign"); })
            )
            .Then(c =>
            {
                for (var i = 0; i < 10; i++)
                    TaskCompletionService.TryReleaseTask(c.Workflow.Id);
                Log.Verbose($"End workflow: {c.Workflow.Id}");
            })
            ;
        }
    }

}
