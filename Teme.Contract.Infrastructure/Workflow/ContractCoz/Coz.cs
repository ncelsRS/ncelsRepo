using Serilog;
using System.Collections.Generic;
using System.Linq;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.ContractCoz
{
    public static class CozExtensions
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> Coz(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Verbose("Start Coz"))
                // распределение договора руководителем ЦОЗ
                .UserTask(UserPromts.SelectExecutors, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].First())
                    .WithOption(UserOptions.SelectExecutors).Do(t1 =>
                        t1.StartWith<SelectExecutorsFirst>()
                            .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                    )
                // согласование исполнителем
                .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].First())
                    .WithOption(UserOptions.MeetRequirements).Do(t =>
                        t.StartWith<CozExecutorMeetReq>()
                            .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Value, s => s.Agreed)
                );
            return builder;
        }
    }
}
