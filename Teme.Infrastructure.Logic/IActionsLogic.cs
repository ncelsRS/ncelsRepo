using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Contract.Data.Model;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Infrastructure.Logic
{
    public interface IActionsLogic : IBaseLogic
    {
        Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId);
        Task<object> Create(CreateModel createModel);
        Task<object> PublishUserAction(string userPromt, string userOption, object value, string workflowId, string userId, IEnumerable<string> executors);
    }
}
