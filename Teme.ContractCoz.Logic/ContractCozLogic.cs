using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.ContractCoz.Data;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.ContractCoz.Logic
{

    public class ContractCozLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        private readonly IContractStatePolicyLogic _contractSp;
        private readonly IContractCozRepo _repo;
        public ContractCozLogic(IContractWorkflowLogic wflogic, IContractStatePolicyLogic contractSp, IContractCozRepo repo)
        {
            _wflogic = wflogic;
            _contractSp = contractSp;
            _repo = repo;
        }

        /// <summary>
        /// Отправка договора выбранному испольнителю ЦОЗ
        /// </summary>
        /// <param name="userPromt"></param>
        /// <param name="userOption"></param>
        /// <param name="workflowId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<object> CozDistributionByExecutors(string userPromt, string userOption, string workflowId, int contractId, int userId)
        {
            var actions = await _wflogic.GetUserActions(workflowId, "SelectExecutors");
            var action = actions.FirstOrDefault(x => x.Prompt == userPromt && x.Options.ContainsKey(userOption));
            var executorsIds = new Dictionary<string, IEnumerable<string>>() {{ ScopeEnum.Coz, new[] { userId.ToString() }}};
            var result = await _wflogic.PublishUserAction(action.Key, userOption, executorsIds);
            await _repo.SaveStatePolice(_contractSp.GetStatePolicy("DeclarantSendContractOneToMore", contractId), contractId);
            return result;
        }
    }
}
