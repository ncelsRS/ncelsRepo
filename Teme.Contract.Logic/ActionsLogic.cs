using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic
{
    public class ActionsLogic : IActionsLogic
    {
        public async Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId)
        {
            return null;
        }
    }
}