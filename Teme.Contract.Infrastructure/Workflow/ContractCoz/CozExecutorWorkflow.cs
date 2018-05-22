using Serilog;
using System.Collections.Generic;
using System.Linq;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.ContractCoz
{
    public static class CozExecutorWorkflow
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> CozExecutorAgreements(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Verbose("Start Coz"))








                // распределение договора руководителем ЦОЗ
                .UserTask(UserPromts.SelectExecutors, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].FirstOrDefault())
                    .WithOption(UserOptions.SelectExecutors).Do(t1 =>
                        t1.StartWith<SelectExecutorsFirst>()
                            .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds))

                // согласование исполнителем
                .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].FirstOrDefault())
                    .WithOption(UserOptions.MeetRequirements).Do(t =>
                        t.StartWith<CozExecutorMeetReq>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))
                    .WithOption(UserOptions.NotMeetRequirements).Do(t =>
                        t.StartWith<CozExecutorNotMeetReq>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))

                // возврат договора заявителю
                .If(d => !d.Agreements.FirstOrDefault(e => e.Key == ScopeEnum.Coz).Value).Do(t => t.ReturnToDeclarant());
            return builder;
        }
    }
}
