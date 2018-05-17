using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCeo;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using Teme.Contract.Infrastructure.Workflow.ContractGv;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow
{
    public static class ContractOneToOneWorkflow
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> ContractOneToOne(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Verbose("Start If ContractType = OneToOne"))
                .While(d => d.Agreements.FirstOrDefault(x => x.Value == false).Value)
                    .Do(then1 => then1.StartWith(c => Log.Verbose("Start Coz and Gv"))
                        .Parallel()
                        .Do(t =>
                             t.StartWith(c => { })
                                 .If(d => d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Coz).Value == false)
                                     .Do(t1 => t1.CozExecutorAgreements()))
                        .Do(t => t.Gv())
                        .Join()

                        //UserTask для возврата на доработку
                        .If(d => d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Ceo).Value == false).Do(t =>
                             t.StartWith(x => { })
                                 .UserTask("", (d, c) => "")
                                     .WithOption("").Do(t1 => Log.Verbose("")))
                    )

                    .If(d => d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Coz).Value == true)
                        .Do(t2 => t2.StartWith(c => Log.Information("Start CozBossAgreements")))
                            .While(d => d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.CozBoss).Value == false)
                                .Do(t2 => t2.CozBossAgreements())
                            
                                    .If(d => d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.CozBoss).Value == true)
                                        .Do(t2 => t2.StartWith(c => Log.Information("Start CeoAgreements")))
                                            .While(d => d.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Ceo).Value == false)
                                                .Do(t3 => t3.CeoAgreements());
            return builder;
        }
    }
}
