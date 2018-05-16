using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow
{
    public static class ReturnToDeclarantWorkflow
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> ReturnToDeclarant(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Information("Start ReturnToDeclarant"))
                //отпрвка договора заявителю
                .UserTask(UserPromts.ReturnToDeclarant, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].FirstOrDefault())
                    .WithOption(UserOptions.ReturnToDeclarant).Do(t1 =>
                        t1.StartWith<CozReturnToDeclarant>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements))

                // возврат заявителем в ЦОЗ
                .UserTask(UserPromts.Declarant.SendOrRemove, (d, c) => "declarant")
                    .WithOption(UserOptions.SendWithoutSign).Do(then =>
                        then.StartWith<SendToNcels>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.ContractType, s => s.ContractType)
                            .Output(d => d.Agreements, s => s.Agreements)
                    )
                    .WithOption(UserOptions.SendWithSign).Do(then =>
                        then.StartWith<SendToNcels>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.ContractType, s => s.ContractType)
                            .Output(d => d.Agreements, s => s.Agreements)
                    );
            return builder;
        }
    }
}
