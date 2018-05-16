using Serilog;
using System.Linq;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow.ContractGv
{
    public static class GvExtensions
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> Gv(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Verbose("Start Gv"))

                // распределение договора руководителем ГВ
                .UserTask(UserPromts.SelectExecutors, (d, c) => c.Item as string)
                    .WithOption(UserOptions.SelectExecutors, "o1").Do(t1 =>
                        t1.StartWith<CozExecutorMeetReq>()
                            .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)

                // согласование исполнителем
                .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].First())
                    .WithOption(UserOptions.MeetRequirements).Do(t =>
                        t.StartWith<CozExecutorMeetReq>()
                            .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Value, s => s.Agreed))

                // согласование руководителем ГВ
                .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].First())
                    .WithOption(UserOptions.MeetRequirements).Do(t2 =>
                        t2.StartWith<CozExecutorMeetReq>()
                            .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                            .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                            .Output(d => d.Value, s => s.Agreed))

                            .If(d => !(bool)d.Value).Do(then => then.StartWith(c => Log.Verbose($"End workflowGV, UserTask = {UserPromts.IsMeetRequirements}")))
                            .If(d => (bool)d.Value).Do(then => then.StartWith(c => Log.Verbose("Send To DEF"))
                            // согласование ДЭФ
                            .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Coz].First())
                                .WithOption(UserOptions.MeetRequirements).Do(t2 =>
                                    t2.StartWith<CozExecutorMeetReq>()
                                        .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                                        .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                                        .Output(d => d.Value, s => s.Agreed))                            
                            ));
            return builder;
        }
    }
}
