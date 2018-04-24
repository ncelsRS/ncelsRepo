using System.Collections.Generic;
using System.Threading.Tasks;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic.Actions
{
    public interface IActionsLogic : IBaseLogic
    {
        /// <summary>
        /// Возвращает открытые задачи текущего пользователя
        /// </summary>
        /// <param name="workflowId">Идентификатор воркфлоу договора</param>
        /// <returns>Открытые задачи для пользователя по текущему контракту</returns>
        Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId);
    }
}