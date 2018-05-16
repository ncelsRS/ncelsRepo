using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.ContractCoz
{
    public static class CozBossWorkflow
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> CozBossAgreements(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Information("Start CozBossAgreements"))
                .UserTask(UserPromts.CozBossAgreements, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].FirstOrDefault())
                    .WithOption(UserOptions.MeetRequirements).Do(t =>
                        t.StartWith<CozBossMeetReq>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))
                    .WithOption(UserOptions.NotMeetRequirements).Do(t=>
                        t.StartWith<CozBossNotMeetReq>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))

                // возврат договора заявителю
                .If(d => !d.Agreements.FirstOrDefault(e => e.Key == ScopeEnum.CozBoss).Value).Do(t => t.ReturnToDeclarant());
            return builder;
        }
    }
}
