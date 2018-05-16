using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.ContractCeo
{
    public static class CeoWorkflow
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> CeoAgreements(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Information("Start CeoAgreements"))
                .UserTask(UserPromts.CeoAgreements, (d, c) => "Ceo")
                    .WithOption(UserOptions.MeetRequirements).Do(t =>
                        t.StartWith<CeoMeetReq>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))
                     .WithOption(UserOptions.NotMeetRequirements).Do(t=>
                        t.StartWith<CeoNotMeetReq>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))

                // возврат договора заявителю
                .If(d => !d.Agreements.FirstOrDefault(e => e.Key == ScopeEnum.Ceo).Value).Do(t => t.ReturnToDeclarant());
            return builder;
        }
    }
}
