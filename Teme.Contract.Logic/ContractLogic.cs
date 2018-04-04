using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using WorkflowCore.Interface;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic
{
    public class ContractLogic
    {
        private readonly IWorkflowHost _host;

        public ContractLogic(IWorkflowHost host)
        {
            _host = host;
        }

        public async Task<string> Test()
        {
            var id = await _host.StartWorkflow("Contract");
            return await Task.FromResult(id);
        }

        public async Task<string> PublishEvent(string name, string key, object data = null)
        {
            await _host.PublishEvent(name, key, data);
            return "Event published";
        }

        public async Task<IEnumerable<OpenUserAction>> GetUserActions(string workflowId)
        {
            return await Task.Run(() => _host.GetOpenUserActions(workflowId));
        }

        public async Task<string> PublishUserAction(string key, string username, string chosenValue)
        {
            await _host.PublishUserAction(key, username, chosenValue);
            return "published";
        }
    }
}
