using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow.Payment;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.Payment
{
    public static class ReturnPaymentToDeclarantWorkflow
    {
        public static IWorkflowBuilder<PaymentTransitionData> ReturnPaymentToDeclarant(this IWorkflowBuilder<PaymentTransitionData> builder)
        {
            builder.StartWith(c => Log.Information("Start ReturnPaymentToDeclarant"))

                // возврат заявителем в ЦОЗ
                .UserTask(UserPromts.Declarant.SendOrRemove, (d, c) => "declarant")
                    .WithOption(UserOptions.SendWithoutSign).Do(then =>
                        then.StartWith<SendPaymentToNcels>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements));
            return builder;
        }
    }
}
