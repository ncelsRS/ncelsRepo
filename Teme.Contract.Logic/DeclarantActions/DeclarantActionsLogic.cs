using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.ContractLogic;

namespace Teme.Contract.Logic.DeclarantActions
{
    public class DeclarantActionsLogic : EntityLogic, IDeclarantActionsLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        public DeclarantActionsLogic(IEntityRepo repo, IContractWorkflowLogic wflogic) : base(repo)
        {
            _wflogic = wflogic;
        }

        public async Task<object> PublishUserAction(string userPromt, string userOption, ContractTypeEnum contractType, string workflowId)
        {
            var actions = await _wflogic.GetUserActions(workflowId, "declarant");
            var action = actions
                .FirstOrDefault(x => x.Prompt == userPromt && x.Options.ContainsKey(userOption));
            if (action == null) throw new ArgumentException("Action not found");
            var executorsIds = new Dictionary<string, IEnumerable<string>> { { ScopeEnum.Coz, new[] { "BossCoz" } } };
            var r = await _wflogic.PublishUserAction(action.Key, userOption, executorsIds, contractType);
            return new { r };
        }
    }
}