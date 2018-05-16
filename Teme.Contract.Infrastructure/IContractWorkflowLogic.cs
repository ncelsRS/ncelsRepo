using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Infrastructure
{
    public interface IContractWorkflowLogic
    {
        Task<object> Create();
        Task PublishEvent(string name, string eventKey, object data = null);
        Task<IEnumerable<OpenUserAction>> GetUserActions(string workflowId, string userId = null);
        Task<string> PublishUserAction(string key, string chosenValue, Dictionary<string, IEnumerable<string>> executorsIds = null, object value = null, Dictionary<string, bool> agreements = null);
    }
}
