using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure.Primitives;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Users;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure.Workflow
{
    public class ContractWorkflowLogic
    {
        private const string _workflowId = "Contract";

        private readonly IWorkflowHost _host;

        public ContractWorkflowLogic(IWorkflowHost host)
        {
            _host = host;
        }

        public async Task<string> Start(ContractWorkflowTransitionData data)
        {
            var key = Guid.NewGuid().ToString();
            var awaiter = TaskCompletionService.AddTask(key);
            data.Value = key;
            await _host.StartWorkflow(_workflowId, data);
            var workflowId = await awaiter;
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
