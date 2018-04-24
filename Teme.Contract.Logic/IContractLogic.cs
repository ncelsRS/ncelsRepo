using System.Collections.Generic;
using System.Threading.Tasks;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic
{
    public interface IContractLogic
    {
        /// <summary>
        /// Создать договор
        /// </summary>
        /// <returns>Id договора</returns>
        Task<string> Create();
    }
}