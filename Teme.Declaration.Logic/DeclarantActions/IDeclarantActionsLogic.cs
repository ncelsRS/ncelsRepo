using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Logic;

namespace Teme.Declaration.Logic.DeclarantActions
{
    public interface IDeclarantActionsLogic : IBaseLogic
    {
        Task<object> Create();
        Task<object> UserActions(string workflowId);
        Task<object> SendOrRemoveDelete(string workflowId);
        Task<object> SendOrRemoveSendWithoutSign(string workflowId);
        Task<object> SendOrRemoveSendWithSign(string workflowId);
    }
}
