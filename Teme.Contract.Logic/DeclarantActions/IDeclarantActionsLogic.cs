using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic.DeclarantActions
{
    public interface IDeclarantActionsLogic : IBaseLogic
    {
        Task<object> PublishUserAction(string userPromt, string userOption, ContractTypeEnum contractType, string workflowId, int contractId);
    }
}