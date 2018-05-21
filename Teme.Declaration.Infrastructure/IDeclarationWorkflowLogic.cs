using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Users.Models;

namespace Teme.Declaration.Infrastructure
{
    public interface IDeclarationWorkflowLogic
    {
        Task<object> Create();
        Task<IEnumerable<OpenUserAction>> GetUserActions(string workflowId, string userId = null);
        Task<string> PublishUserAction(string key, string chosenValue, Dictionary<string, IEnumerable<string>> executorsIds = null, object value = null, Dictionary<string, bool> agreements = null);
    }
}
