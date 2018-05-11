using Serilog;
using System.Linq;
using MongoDB.Bson;
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
                .StartWith(c => { Log.Verbose($"New contract, workflowId: {c.Workflow.Id}"); })
                .UserTask(UserPromts.Declarant.SendOrRemove, (d, c) => "declarant")
                    .WithOption(UserOptions.SendWithSign).Do(then =>
                        then.StartWith<SendToNcels>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.ContractType, s => s.ContractType)
                    )
                    .WithOption(UserOptions.SendWithoutSign).Do(then =>
                        then.StartWith<SendToNcels>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.ContractType, s => s.ContractType)
                    )
                    .WithOption(UserOptions.Delete).Do(then =>
                        then.StartWith<Delete>()
                            .EndWorkflow()
                    )
                .If(d => d.ContractType == ContractTypeEnum.OneToOne).Do(then =>
                    then.StartWith(c => Log.Verbose("Start Coz and Gv"))
                        .Parallel()
                        .Do(t => t.Coz())
                        .Do(t => t.Gv())
                        .Join()
                )
                .If(d => d.ContractType == ContractTypeEnum.OneToMore).Do(t => t.Coz())
                .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Root].FirstOrDefault())
                    .WithOption(UserOptions.MeetRequirements).Do(t => { })
                    .WithOption(UserOptions.NotMeetRequirements).Do(t => { })
                .If(d => d.IsSignedByDeclarant).Do(t => t
                    .StartWith(c => { Log.Verbose("Sign"); })
                )
                .Then(c =>
                {
                    TaskCompletionService.ReleaseAll(c.Workflow.Id);
                    Log.Verbose($"End workflow: {c.Workflow.Id}");
                });
        }
    }
}
