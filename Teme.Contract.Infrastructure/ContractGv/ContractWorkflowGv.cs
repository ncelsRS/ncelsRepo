using Serilog;
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
                .ForEach(d => d.ExecutorsIds.Keys)
                    .Do(t => t.StartWith(c => Log.Information("ForEach"))
                        .UserTask("ChoiseExecutors", (d, c) => c.Item as string)
                            .WithOption(UserOptions.SelectExecutors, "o1").Do(then => then.StartWith(c => Log.Information("EndGv")))
                    );
            return builder;
        }
    }
}