using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Infrastructure;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.ContractLogic;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic.Actions
{
    public class ActionsLogic : EntityLogic, IActionsLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        public ActionsLogic(IEntityRepo repo, IContractWorkflowLogic wflogic) : base(repo)
        {
            _wflogic = wflogic;
        }

        public async Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId)
        {
            return await _wflogic.GetUserActions(workflowId);
        }
    }
}