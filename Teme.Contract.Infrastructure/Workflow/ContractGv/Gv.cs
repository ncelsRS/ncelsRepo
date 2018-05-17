using Serilog;
using System.Collections.Generic;
using System.Linq;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Infrastructure.Workflow.ContractCoz;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Teme.Contract.Infrastructure.Workflow.ContractGv
{
    public static class GvExtensions
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> Gv(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            builder.StartWith(c => Log.Verbose("Start Gv"))

                // согласование ГВ
                .If(t => t.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Gv).Value == false).Do(t1 => t1.StartWith(x => { }))
                    // распределение договора руководителем ГВ
                    .UserTask(UserPromts.SelectExecutors, (d, c) => c.Item as string)
                        .WithOption(UserOptions.SelectExecutors, "o1").Do(t1 =>
                            t1.StartWith<TestClass>()
                                .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                                .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)

                    // согласование исполнителем
                    .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Gv].First())
                        .WithOption(UserOptions.MeetRequirements).Do(t =>
                            t.StartWith<TestClass>()
                                .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                                .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                                .Output(d => d.Value, s => s.Agreed))

                    // согласование руководителем ГВ
                    .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Gv].First())
                        .WithOption(UserOptions.MeetRequirements).Do(t2 =>
                            t2.StartWith<TestClass>()
                                .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                                .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                                .Output(d => d.Value, s => s.Agreed)))

                    // согласование ДЭФ
                    .If(t => t.Agreements.FirstOrDefault(x => x.Key == ScopeEnum.Def).Value == false).Do(t1 => t1.StartWith(x => { }))

                        //согласование исполнителем ДЭФ
                        .UserTask(UserPromts.IsMeetRequirements, (d, c) => d.ExecutorsIds[ScopeEnum.Def].First())
                            .WithOption(UserOptions.MeetRequirements).Do(t2 =>
                                t2.StartWith<TestClass>()
                                    .Input(s => s.ExecutorsIds, d => d.ExecutorsIds)
                                    .Output(d => d.ExecutorsIds, s => s.ExecutorsIds)
                                    .Output(d => d.Value, s => s.Agreed));
            return builder;
        }
    }


    public class TestClass : StepBody
    {
        public ContractWorkflowTransitionData Data { get; set; }
        public Dictionary<string, IEnumerable<string>> ExecutorsIds { get; set; }
        public Dictionary<string, bool> Agreed { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            return ExecutionResult.Next();
        }
    }
}
