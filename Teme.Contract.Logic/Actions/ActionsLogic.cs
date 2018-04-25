using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic.Actions
{
    public class ActionsLogic : IActionsLogic
    {
        private readonly IContractWorkflowLogic _wflogic;

        public ActionsLogic(IContractWorkflowLogic wflogic)
        {
            _wflogic = wflogic;
        }

        public async Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId)
        {
            return await _wflogic.GetUserActions(workflowId);
        }
    }
}