using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<string> StartWorkflow()
        {
            return null;
            //var key = Guid.NewGuid().ToString();
            //var awaiter = TaskCompletionService.AddTask(key);
            //await _host.StartWorkflow("Contract", new Infrastructure.Data() { CompleteKey = key });
            //var id = await awaiter;
            //return await id;
        }

        public async Task<string> PublishEvent(string name, string key, object data = null)
        {
            await _host.PublishEvent(name, key, data);
            return "Event published";
        }

        public async Task<IEnumerable<OpenUserAction>> GetUserActions(string workflowId)
        {
            var workflow = await _host.PersistenceStore.GetWorkflowInstance(workflowId);
            await _host.PersistenceStore.GetWorkflowInstances(WorkflowCore.Models.WorkflowStatus.Runnable, "", null, null, 0, 10);
            return await Task.Run(() => _host.GetOpenUserActions(workflowId));
        }

        public async Task<string> PublishUserAction(string key, string username, string chosenValue)
        {
            await _host.PublishUserAction(key, username, chosenValue);
            return "published";
        }
    }
}
