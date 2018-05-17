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
using Teme.Contract.Infrastructure.Workflow.ContractCeo;

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

                // отправка договора в ЦОЗ
                .UserTask(UserPromts.Declarant.SendOrRemove, (d, c) => "declarant")
                    .WithOption(UserOptions.SendWithoutSign).Do(then =>
                        then.StartWith<SendToNcels>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.ContractType, s => s.ContractType)
                            .Output(d => d.Agreements, s => s.Agreements)
                    )
                    .WithOption(UserOptions.SendWithSign).Do(then =>
                        then.StartWith<SendToNcels>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.ContractType, s => s.ContractType)
                            .Output(d => d.Agreements, s => s.Agreements)
                    )
                    .WithOption(UserOptions.Delete).Do(then =>
                        then.StartWith<Delete>()
                            .EndWorkflow()
                    )

                // договор один к одному
                .If(d => d.ContractType == ContractTypeEnum.OneToOne)
                    .Do(then => then.ContractOneToOne()
                )

                // договор один ко многим
                .If(d => d.ContractType == ContractTypeEnum.OneToMore)
                    .Do(then => then.ContractOneToMore()
                )

                .Then(c =>
                {
                    TaskCompletionService.ReleaseAll(c.Workflow.Id);
                    Log.Verbose($"End workflow: {c.Workflow.Id}");
                });
        }
    }
}
