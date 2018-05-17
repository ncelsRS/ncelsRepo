using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.Payment
{
    public static class PaymentDefWorkflow
    {
        public static IWorkflowBuilder<PaymentTransitionData> PaymentDef(this IWorkflowBuilder<PaymentTransitionData> builder)
        {
            builder.StartWith(x => Log.Information("Start PaymentDef"))
                .UserTask(UserPromts.DefExecutorAgreements, (d, c) => d.ExecutorsIds[ScopeEnum.Def].FirstOrDefault())
                    .WithOption(UserOptions.MeetRequirements).Do(t2 =>
                        t2.StartWith<DefExecutorMeet>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))
                    .WithOption(UserOptions.NotMeetRequirements).Do(t2 =>
                        t2.StartWith<DefExecutorNotMeet>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))
                 // возврат заявителю
                 .If(t => t.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Def).Value == false).Do(t => t.ReturnPaymentToDeclarant());
            return builder;
        }
    }
}
