using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Workflow;
using Teme.Shared.Data.Primitives.Contract;
using WorkflowCore.Interface;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic
{
    public class ContractLogic
    {
        private readonly ContractWorkflowLogic _wflogic;

        public ContractLogic(ContractWorkflowLogic wflogic)
        {
            _wflogic = wflogic;
        }

        public async Task<string> StartWorkflow()
        {
            return await _wflogic.Create();
        }

        public async Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId)
        {
            return await _wflogic.GetUserActions(workflowId);
        }

        public async Task<IEnumerable<OpenUserAction>> PublishUserAction(string key, string chosenValue, Dictionary<string, IEnumerable<string>> executorsIds, object data)
        {
            await _wflogic.PublishUserAction(key, chosenValue, executorsIds, data);
            var workflowId = key.Split('.')[0];
            return await _wflogic.GetUserActions(workflowId);
        }

    }
}
