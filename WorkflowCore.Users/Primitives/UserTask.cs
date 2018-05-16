using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Primitives;
using WorkflowCore.Users.Models;

namespace WorkflowCore.Users.Primitives
{
    public class UserTask : ContainerStepBody
    {
        public string AssignedPrincipal { get; set; }

        public string Prompt { get; set; }

        public const string EventName = "UserAction";
        public const string ExtAssignPrincipal = "AssignedPrincipal";
        public const string ExtPrompt = "Prompt";
        public const string ExtUserOptions = "UserOptions";
        private readonly Dictionary<string, string> _options;
        private readonly IEnumerable<EscalateStep> _escalations;

        public UserTask(Dictionary<string, string> options, IEnumerable<EscalateStep> escalations)
        {
            _options = options;
            _escalations = escalations;
        }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            try
            {
                if (!context.ExecutionPointer.EventPublished)
                {
                    context.ExecutionPointer.ExtensionAttributes[ExtAssignPrincipal] = AssignedPrincipal;
                    context.ExecutionPointer.ExtensionAttributes[ExtPrompt] = Prompt;
                    context.ExecutionPointer.ExtensionAttributes[ExtUserOptions] = _options;

                    var effectiveDate = DateTime.Now.ToUniversalTime();
                    var eventKey = context.Workflow.Id + "." + context.ExecutionPointer.Id;

                    SetupEscalations(context);

                    return ExecutionResult.WaitForEvent(EventName, eventKey, effectiveDate);
                }

                if (!(context.ExecutionPointer.EventData is UserAction))
                    throw new ArgumentException();

                var action = (UserAction)context.ExecutionPointer.EventData;
                if (action.ExecutorsIds != null)
                    context.ExecutionPointer.ExtensionAttributes["ExecutorsIds"] = action.ExecutorsIds;
                if (action.Data != null)
                    context.ExecutionPointer.ExtensionAttributes["Data"] = action.Data;

                if (context.PersistenceData == null)
                {
                    var result = ExecutionResult.Branch(new List<object>() { null }, new ControlPersistenceData() { ChildrenActive = true });
                    result.OutcomeValue = action.OutcomeValue;
                    return result;
                }

                if ((context.PersistenceData is ControlPersistenceData) && ((context.PersistenceData as ControlPersistenceData).ChildrenActive))
                {
                    bool complete = true;
                    foreach (var childId in context.ExecutionPointer.Children)
                        complete = complete && IsBranchComplete(context.Workflow.ExecutionPointers, childId);

                    if (complete)
                        return ExecutionResult.Next();
                    else
                    {
                        var result = ExecutionResult.Persist(context.PersistenceData);
                        result.OutcomeValue = action.OutcomeValue;
                        return result;
                    }
                }
                throw new ArgumentException("PersistenceData");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
                throw ex;   
            }
        }

        private void SetupEscalations(IStepExecutionContext context)
        {
            foreach (var esc in _escalations)
            {
                context.Workflow.ExecutionPointers.Add(new ExecutionPointer()
                {
                    Active = true,
                    Id = Guid.NewGuid().ToString(),
                    PredecessorId = context.ExecutionPointer.Id,
                    StepId = esc.Id,
                    StepName = esc.Name
                });
            }
        }
    }
}
