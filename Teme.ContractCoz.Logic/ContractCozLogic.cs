using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.ContractCoz.Logic
{

    public class ContractCozLogic : EntityLogic, IContractCozLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        public ContractCozLogic(IEntityRepo repo, IContractWorkflowLogic wflogic) : base(repo)
        {
            _wflogic = wflogic;
        }

        /// <summary>
        /// Отправка договора выбранному испольнителю ЦОЗ
        /// </summary>
        /// <param name="dbem"></param>
        /// <returns></returns>
        public async Task<object> DistributionByExecutors(string userPromt, string userOption, string workflowId, int userId)
        {
            var actions = await _wflogic.GetUserActions(workflowId, "SelectExecutors");
            var action = actions.FirstOrDefault(x => x.Prompt == userPromt && x.Options.ContainsKey(userOption));
            var executorsIds = new Dictionary<string, IEnumerable<string>>() {{ ScopeEnum.Coz, new[] { userId.ToString() }}};
            var result = await _wflogic.PublishUserAction(action.Key, userOption, executorsIds);
            return result;
        }
    }
}
