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
            builder.StartWith(c => Log.Information("Start Coz"))
                .UserTask(UserPromts.SelectExecutors, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].First())
                    .WithOption(UserOptions.SelectExecutors, "o1").Do(t1 =>
                        t1.StartWith<SelectExecutorsFirst>()
                            .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                    )
                .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].First())
                    .WithOption(UserOptions.MeetRequirements).Do(t =>
                        t.StartWith<CozBossMeetReq>()
                            .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                    )
                .If(d => true).Do(t => t.StartWith(c => { Log.Verbose("MeetRequirements"); }))
                .If(d => false).Do(t =>
                    t.StartWith(c => { Log.Verbose("NotMeetRequirements, do something"); })
                )
            ;
            return builder;
        }
    }
}
