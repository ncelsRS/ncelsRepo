using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure.Workflow;
using WorkflowCore.Interface;
using WorkflowCore.Users;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure
{
    public class ContractWorkflowLogic : IContractWorkflowLogic
    {
        private const string WorkflowShemeId = "Contract";

        private readonly IWorkflowHost _host;

        public ContractWorkflowLogic(IWorkflowHost host)
        {
            _host = host;
        }

        public async Task<string> Create()
        {
            var workflowId = await _host.StartWorkflow(WorkflowShemeId);
            await TaskCompletionService.AddTask(workflowId);
            return workflowId;
        }

        [Obsolete("Используем UserExtension")]
        public async Task PublishEvent(string name, string eventKey, object data = null)
        {
            await _host.PublishEvent(name, eventKey, data);
        }

        public async Task<IEnumerable<OpenUserAction>> GetUserActions(string workflowId, string userId = null)
        {
            var actions = await Task.Run(() => _host.GetOpenUserActions(workflowId));
            if (userId == null) return actions;
            return actions.Where(x => x.AssignedPrincipal == userId);
        }

        public async Task<string> PublishUserAction(string key, string chosenValue, Dictionary<string, IEnumerable<string>> executorsIds = null, object value = null)
        {
            var workflowId = key.Split('.')[0];
            var awaiter = TaskCompletionService.AddTask(workflowId);
            await _host.PublishUserAction(key, chosenValue, executorsIds, value);
            return await awaiter;
        }
    }
}
