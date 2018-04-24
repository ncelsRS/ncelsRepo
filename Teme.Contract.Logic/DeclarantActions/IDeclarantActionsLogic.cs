using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic.DeclarantActions
{
    public interface IDeclarantActionsLogic
    {
        Task<object> PublishUserAction(string userPromt, string userOption, string contractId);
    }
}