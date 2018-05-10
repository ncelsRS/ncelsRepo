using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Infrastructure.Logic
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
