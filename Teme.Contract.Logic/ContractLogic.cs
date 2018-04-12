using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
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
            var id = await _wflogic.Start(new ContractWorkflowTransitionData {
                ExecutorId = "declarantId",
                ContractType = 0
            });
            return id;
        }

        public async Task<string> PublishUserAction(string key, string username, string chosenValue, object data)
        {
            return await _wflogic.PublishUserAction(key, username, chosenValue, data);
        }

    }
}
