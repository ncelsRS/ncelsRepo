using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure
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
            data.AwaiterKey = key;
            await _host.StartWorkflow(_workflowId, data);
            return await awaiter;
        }

        [Obsolete("Используем UserExtension")]
        public async Task PublishEvent(string name, string eventKey, object data = null)
        {
            await _host.PublishEvent(name, eventKey, data);
        }

        public async Task<IEnumerable<OpenUserAction>> GetUserActions(string workflowId, string userId = null)
        {
            var actions = await _host.GetOpenUserActions(workflowId);
            if (userId == null) return actions;
            return actions.Where(x => x.AssignedPrincipal == userId);
        }

        public async Task<string> PublishUserAction(string key, string username, string chosenValue, object value)
        {
            var data = new ContractWorkflowEventData
            {
                AwaiterKey = Guid.NewGuid().ToString(),
                Value = value
            };
            var awaiter = TaskCompletionService.AddTask(data.AwaiterKey);
            await _host.PublishUserAction(key, username, chosenValue, data);
            return await awaiter;
        }
    }
}
