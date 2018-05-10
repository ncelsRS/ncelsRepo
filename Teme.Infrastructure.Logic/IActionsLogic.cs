using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Infrastructure.Logic
{
    public interface IActionsLogic : IBaseLogic
    {
        Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId);
    }
}
