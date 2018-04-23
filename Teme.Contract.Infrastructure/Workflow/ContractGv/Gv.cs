using Serilog;
using Teme.Contract.Infrastructure.Primitives;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.ContractGv
{
    public static class GvExtensions
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> Gv(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Verbose("Start Gv"))
                .ForEach(d => d.ExecutorsIds["Gv"])
                    .Do(t => t.StartWith(c => Log.Information("ForEach"))
                        .UserTask(UserPromts.SelectExecutors, (d, c) => c.Item as string)
                            .WithOption(UserOptions.SelectExecutors, "o1").Do(t1 =>
                                t1.StartWith(c => Log.Information("EndGv"))
                            )
                    )
                ;

            return builder;
        }
    }
}