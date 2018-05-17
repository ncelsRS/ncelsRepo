using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.Payment
{
    /// <summary>
    /// Workflow для Заявки на платеж
    /// </summary>
    public class PaymentWorkflow : IWorkflow<ContractWorkflowTransitionData>
    {
        public string Id => "Payment";

        public int Version => 0;

        public void Build(IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            //builder
            //    .StartWith(c => { Log.Verbose($"New payment, workflowId: {c.Workflow.Id}"); })

            //    // отправка заявки в ЦОЗ
            //    .UserTask(UserPromts.Declarant.SendOrRemove, (d, c) => "declarant")
            //        .WithOption(UserOptions.SendWithoutSign).Do(then =>
            //            then.StartWith<SendToNcels>()
            //                .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
            //                .Output(d => d.ContractType, s => s.ContractType)
            //                .Output(d => d.Agreements, s => s.Agreements)
            //        )
            //        .WithOption(UserOptions.SendWithSign).Do(then =>
            //            then.StartWith<SendToNcels>()
            //                .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
            //                .Output(d => d.ContractType, s => s.ContractType)
            //                .Output(d => d.Agreements, s => s.Agreements)
            //        )
            //        .WithOption(UserOptions.Delete).Do(then =>
            //            then.StartWith<Delete>()
            //                .EndWorkflow()
            //        )


            //builder.StartWith(c => Log.Verbose("Start Payment"))

            //    // согласование ГВ
            //    .If(t => t.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Gv).Value == false).Do(t1 => t1.StartWith(x => { }))
            //        // распределение договора руководителем ГВ
            //        .UserTask(UserPromts.SelectExecutors, (d, c) => c.Item as string)
            //            .WithOption(UserOptions.SelectExecutors, "o1").Do(t1 =>
            //                t1.StartWith<TestClass>()
            //                    .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
            //                    .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)

            //        // согласование исполнителем
            //        .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Gv].First())
            //            .WithOption(UserOptions.MeetRequirements).Do(t =>
            //                t.StartWith<TestClass>()
            //                    .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
            //                    .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
            //                    .Output(d => d.Value, s => s.Agreed))

            //        // согласование руководителем ГВ
            //        .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Gv].First())
            //            .WithOption(UserOptions.MeetRequirements).Do(t2 =>
            //                t2.StartWith<TestClass>()
            //                    .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
            //                    .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
            //                    .Output(d => d.Value, s => s.Agreed)))

            //        // согласование ДЭФ
            //        .If(t => t.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Def).Value == false).Do(t1 => t1.StartWith(x => { }))

            //            //согласование исполнителем ДЭФ
            //            .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Def].First())
            //                .WithOption(UserOptions.MeetRequirements).Do(t2 =>
            //                    t2.StartWith<TestClass>()
            //                        .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
            //                        .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
            //                        .Output(d => d.Value, s => s.Agreed));
        }
    }
}
