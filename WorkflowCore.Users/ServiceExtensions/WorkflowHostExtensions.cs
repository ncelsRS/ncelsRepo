using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services;
using WorkflowCore.Users.Models;
using WorkflowCore.Users.Primitives;

namespace WorkflowCore.Interface
{
    public static class WorkflowHostExtensions
    {
        public static async Task PublishUserAction(this IWorkflowHost host, string actionKey, object chosenValue, Dictionary<string, IEnumerable<string>> ExecutorsIds = null, object data = null)
        {
            UserAction eventData = new UserAction()
            {
                OutcomeValue = chosenValue,
                ExecutorsIds = ExecutorsIds,
                Data = data
            };

            await host.PublishEvent(UserTask.EventName, actionKey, eventData);
        }

        public static async Task<IEnumerable<OpenUserAction>> GetOpenUserActions(this IWorkflowHost host, string workflowId)
        {
            var workflow = await host.PersistenceStore.GetWorkflowInstance(workflowId);
            return workflow.GetOpenUserActions();
        }
    }
}
