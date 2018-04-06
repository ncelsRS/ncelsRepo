using System;
using System.Threading.Tasks;
using WorkflowCore.Interface;

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
            var instance = await _host.PersistenceStore.GetWorkflowInstance(id);
            return await Task.FromResult(instance.Status.ToString());
        }

        public async Task<string> PublishEvent(string name, string key)
        {
            await _host.PublishEvent(name, key, new { id = "id" });
            return "Event published";
        }
    }
}
