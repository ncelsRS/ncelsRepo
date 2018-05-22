using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Declaration.Infrastructure.TransitionDatas;
using WorkflowCore.Interface;
using WorkflowCore.Users;
using WorkflowCore.Users.Models;

namespace Teme.Declaration.Infrastructure
{
    public class DeclarationWorkflowLogic : IDeclarationWorkflowLogic
    {
        private const string WorkflowShemeId = "Declaration";

        private readonly IWorkflowHost _host;

        public DeclarationWorkflowLogic(IWorkflowHost host)
        {
            _host = host;
        }

        public async Task<object> Create()
        {
            var workflowId = await _host.StartWorkflow(WorkflowShemeId, new DeclarationTransitionData());
            await TaskCompletionService.AddTask(workflowId);
            return workflowId;
        }

        public async Task<IEnumerable<OpenUserAction>> GetUserActions(string workflowId, string userId = null)
        {
            var actions = await _host.GetOpenUserActions(workflowId);
            return userId == null
                ? actions
                : actions
                    .Where(x => x.AssignedPrincipal == userId);
        }

        public async Task<string> PublishUserAction(string key, string chosenValue,
            Dictionary<string, IEnumerable<string>> executorsIds = null, object value = null, Dictionary<string, bool> agreements = null)
        {
            var workflowId = key.Split('.')[0];
            var awaiter = TaskCompletionService.AddTask(workflowId);
            await _host.PublishUserAction(key, chosenValue, executorsIds, value, agreements);
            return await awaiter;
        }
    }
}
