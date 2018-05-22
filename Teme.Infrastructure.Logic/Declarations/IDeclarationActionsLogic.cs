using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Infrastructure.Logic.Declarations
{
    public interface IDeclarationActionsLogic : IBaseLogic
    {
        Task<object> Create();
        Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId);
        Task<object> PublishUserAction(string userPromt, string userOption, object value, string workflowId, string userId = null, IEnumerable<string> executors = null, Dictionary<string, bool> agreements = null);
    }
}
