using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.Payment
{
    public static class PaymentGvWorkflow
    {
        public static IWorkflowBuilder<PaymentTransitionData> PaymentGv(this IWorkflowBuilder<PaymentTransitionData> builder)
        {
            builder.StartWith(c => Log.Information("Start PaymentGv"))

                // распределение договора руководителем ГВ
                .UserTask(UserPromts.SelectExecutors, (d, c) => d.ExecutorsIds[ScopeEnum.Gv].FirstOrDefault())
                    .WithOption(UserOptions.SelectExecutors).Do(t1 =>
                        t1.StartWith<SelectPaymentExecutors>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds))

                // согласование исполнителем
                .UserTask(UserPromts.GvExecutorAgreements, (d, c) => d.ExecutorsIds[ScopeEnum.Gv].FirstOrDefault())
                    .WithOption(UserOptions.MeetRequirements).Do(t =>
                        t.StartWith<GvExecutorMeet>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))
                    .WithOption(UserOptions.NotMeetRequirements).Do(t =>
                        t.StartWith<GvExecutorNotMeet>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))

                // согласование руководителем ГВ
                .UserTask(UserPromts.GvBossAgreements, (d, c) => d.ExecutorsIds[ScopeEnum.Gv].FirstOrDefault())
                    .WithOption(UserOptions.MeetRequirements).Do(t2 =>
                        t2.StartWith<GvBossMeet>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))
                    .WithOption(UserOptions.NotMeetRequirements).Do(t2 =>
                        t2.StartWith<GvBossNotMeet>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))

                // возврат заявителю
                .If(t => t.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Gv).Value == false).Do(t => t.ReturnPaymentToDeclarant());
            return builder;
        }
    }
}
