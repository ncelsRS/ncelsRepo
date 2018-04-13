using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users.Models;
using WorkflowCore.Users.Primitives;

namespace WorkflowCore.Interface
{
    public static class WorkflowHostExtensions
    {
        public static async Task PublishUserAction(this IWorkflowHost host, string actionKey, string user, string selectedOption, object value)
        {
            UserAction data = new UserAction()
            {
                User = user,
                OutcomeValue = selectedOption,
                Value = value
            };
            await host.PublishEvent(UserTask.EventName, actionKey, data);
        }

        public static async Task<IEnumerable<OpenUserAction>> GetOpenUserActions(this IWorkflowHost host, string workflowId)
        {
            var workflow = await host.PersistenceStore.GetWorkflowInstance(workflowId);
            return workflow.GetOpenUserActions();
        }
    }
}
