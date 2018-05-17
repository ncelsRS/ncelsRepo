using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using WorkflowCore.Interface;
using WorkflowCore.Users;

namespace Teme.Contract.Infrastructure.Workflow.Payment
{
    /// <summary>
    /// Workflow для Заявки на платеж
    /// </summary>
    public class PaymentWorkflow : IWorkflow<PaymentTransitionData>
    {
        public string Id => "Payment";

        public int Version => 0;

        public void Build(IWorkflowBuilder<PaymentTransitionData> builder)
        {
            builder
                .StartWith(c => { Log.Verbose($"New payment, workflowId: {c.Workflow.Id}"); })

                // отправка заявки на платеж в ЦОЗ
                .UserTask(UserPromts.Declarant.SendOrRemove, (d, c) => "declarant")
                    .WithOption(UserOptions.SendWithoutSign).Do(then =>
                        then.StartWith<SendPaymentToNcels>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))

                // согласование ГВ
                .While(t => t.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Gv).Value == false).Do(t => t.PaymentGv())

                // согласование ДЭФ
                .While(t => t.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Def).Value == false).Do(t => t.PaymentDef())

                // регистрация
                .UserTask(UserPromts.RegisterPayment, (d, c) => "Coz")
                    .WithOption(UserOptions.Register).Do(t =>
                        t.StartWith<RegisterPayment>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))

                .Then(c =>
                {
                    TaskCompletionService.ReleaseAll(c.Workflow.Id);
                    Log.Verbose($"End workflow: {c.Workflow.Id}");
                });
        }
    }
}
