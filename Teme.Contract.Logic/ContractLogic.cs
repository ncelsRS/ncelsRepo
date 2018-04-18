using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
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
            var id = await _wflogic.Start(new ContractWorkflowTransitionData
            {
                ContractId = 0,
                ContractType = ContractTypeEnum.OneToOne,
            });
            return id;
        }

        public async Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId)
        {
            return await _wflogic.GetUserActions(workflowId);
        }

        public async Task<string> PublishUserAction(string key, string chosenValue, IEnumerable<string> executorsIds, object data)
        {
            return await _wflogic.PublishUserAction(key, chosenValue, executorsIds, data);
        }

    }
}
