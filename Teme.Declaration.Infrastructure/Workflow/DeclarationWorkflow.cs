using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Teme.Declaration.Infrastructure.TransitionDatas;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using WorkflowCore.Interface;
using WorkflowCore.Users;

namespace Teme.Declaration.Infrastructure.Workflow
{
    public class DeclarationWorkflow : IWorkflow<DeclarationTransitionData>
    {
        public string Id => "Declaration";

        public int Version => 0;

        public void Build(IWorkflowBuilder<DeclarationTransitionData> builder)
        {
            builder.StartWith(c => Log.Information("Start DeclarationWorkflow"))
                .UserTask(UserPromts.Declarant.SendOrRemove, (d, c) => "declarant")
                    .WithOption(UserOptions.SendWithoutSign).Do(then =>
                        then.StartWith(c => Log.Information("SendWithoutSign Step"))
                    )
                    .WithOption(UserOptions.SendWithSign).Do(then =>
                        then.StartWith(c => Log.Information("SendWithSign Step"))
                    )
                    .WithOption(UserOptions.Delete).Do(then =>
                        then.StartWith(c => Log.Information("Delete Step"))
                    )
                .Then(c =>
                 {
                     TaskCompletionService.ReleaseAll(c.Workflow.Id);
                     Log.Verbose($"End workflow: {c.Workflow.Id}");
                 });
        }
    }
}
