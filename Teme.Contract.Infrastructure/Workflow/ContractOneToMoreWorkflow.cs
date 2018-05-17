using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCeo;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow
{
    public static class ContractOneToMoreWorkflow
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> ContractOneToMore(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Verbose("Start If ContractType = OneToMore"))

                .While(d => !d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Ceo).Value)
                    .Do(t => t.StartWith(c => Log.Information("While Ceo"))
                        .While(d => !d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.CozBoss).Value)
                            .Do(t1 => t1.StartWith(c => Log.Information("While CozBoss"))
                                .While(d => !d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Coz).Value)
                                    .Do(t2 => t2.CozExecutorAgreements())
                                        .If(d => d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Coz).Value)
                                            .Do(t3 => t3.CozBossAgreements()))
                                                .If(d => d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.CozBoss).Value)
                                                    .Do(t3 => t3.CeoAgreements()))

                .UserTask(UserPromts.RegisterContract, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].FirstOrDefault())
                    .WithOption(UserOptions.Register).Do(t =>
                        t.StartWith<RegisterContract>()
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Agreements, s => s.Agreements));
            return builder;
        }
    }
}
