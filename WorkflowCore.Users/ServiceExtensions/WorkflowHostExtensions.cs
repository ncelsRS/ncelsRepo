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
        public static async Task PublishUserAction(this IWorkflowHost host, string actionKey, object chosenValue, string awaiterKey, Dictionary<string, IEnumerable<string>> ExecutorsIds = null, object data = null)
        {
            UserAction eventData = new UserAction()
            {
                OutcomeValue = chosenValue,
                AwaiterKey = awaiterKey,
                ExecutorsIds = ExecutorsIds,
                Data = data
            };

            await host.PublishEvent(UserTask.EventName, actionKey, eventData);
        }

        public static IEnumerable<OpenUserAction> GetOpenUserActions(this IWorkflowHost host, string workflowId)
        {
            var workflow = host.PersistenceStore.GetWorkflowInstance(workflowId).Result;
            return workflow.GetOpenUserActions();
        }
    }
}
