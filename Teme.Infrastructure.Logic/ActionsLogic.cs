using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Data.Model;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Infrastructure.Logic
{
    public class ActionsLogic : EntityLogic, IActionsLogic
    {
        private readonly IContractWorkflowLogic _wflogic;
        public ActionsLogic(IEntityRepo repo, IContractWorkflowLogic wflogic) : base(repo)
        {
            _wflogic = wflogic;
        }

        public async Task<object> Create(CreateModel createModel)
        {
            return await _wflogic.Create();
        }

        public async Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId)
        {
            return await _wflogic.GetUserActions(workflowId);
        }

        public async Task<object> PublishUserAction(string userPromt, string userOption, ContractTypeEnum contractType, string workflowId)
        {
            var actions = await _wflogic.GetUserActions(workflowId, "declarant");
            var action = actions
                .FirstOrDefault(x => x.Prompt == userPromt && x.Options.ContainsKey(userOption));
            if (action == null) throw new ArgumentException("Action not found");
            var executorsIds = new Dictionary<string, IEnumerable<string>> { { ScopeEnum.Coz, new[] { "BossCoz" } } };
            var result = await _wflogic.PublishUserAction(action.Key, userOption, executorsIds, contractType);
            return new { result };
        }
    }
}
