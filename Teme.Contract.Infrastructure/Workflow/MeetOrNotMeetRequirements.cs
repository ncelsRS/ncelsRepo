using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using WorkflowCore.Interface;

namespace Teme.Contract.Infrastructure.Workflow
{
    public static class MeetOrNotMeetRequirements
    {
        public static IWorkflowBuilder<ContractWorkflowTransitionData> MeetOrNotMeetRequirement(this IWorkflowBuilder<ContractWorkflowTransitionData> builder)
        {
            //Консалидация всех выявленных несоответствий
            builder.StartWith(c => Log.Verbose("Start MeetOrNotMeetRequirement"))
                .UserTask("MeetOrNotMeetRequirement", (d, c) => "asd")
                    .WithOption("Yes").Do(t => t.StartWith(c => Log.Verbose("All Meet Requirements")))
                    .WithOption("Not").Do(t => t.StartWith(c => Log.Verbose("Необходимо вернуть обрано")));
            return builder;
        }
    }
}
