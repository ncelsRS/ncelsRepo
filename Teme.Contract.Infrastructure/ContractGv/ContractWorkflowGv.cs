using Serilog;
using System;
using System.Linq;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.WorkflowSteps;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.ContractGv
{
    public static class ContractWorkflowGv
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> ContractGv(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Information("StartWithGv"))
                .ForEach(d => d.ExecutorsIds["Gv"])
                    .Do(t => t.StartWith(c => Log.Information("ForEach"))
                        .UserTask("ChoiseExecutors", (d, c) => c.Item as string)
                            .WithOption(UserOptions.SelectExecutors, "o1").Do(t1 =>
                                t1.StartWith(c => Log.Information("EndGv"))
                            )
                            .WithEscalation(x => TimeSpan.FromMilliseconds(1), null, a =>
                                a.StartWith(c => { })
                            )
                    );
            return builder;
        }
    }
}