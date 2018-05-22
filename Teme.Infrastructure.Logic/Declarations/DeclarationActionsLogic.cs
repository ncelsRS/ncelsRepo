using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Declaration.Infrastructure;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using WorkflowCore.Users.Models;

namespace Teme.Infrastructure.Logic.Declarations
{
    public class DeclarationActionsLogic : EntityLogic, IDeclarationActionsLogic
    {
        private readonly IDeclarationWorkflowLogic _wflogic;
        public DeclarationActionsLogic(IEntityRepo repo, IDeclarationWorkflowLogic wflogic) : base(repo)
        {
            _wflogic = wflogic;
        }

        public async Task<object> Create()
        {
            return await _wflogic.Create();
        }

        public async Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId)
        {
            return await _wflogic.GetUserActions(workflowId);
        }

        public async Task<object> PublishUserAction(string userPromt, string userOption, object value, string workflowId, string userId = null, IEnumerable<string> executors = null, Dictionary<string, bool> agreements = null)
        {
            var actions = await _wflogic.GetUserActions(workflowId, userId);
            var action = actions
                .FirstOrDefault(x => x.Prompt == userPromt && x.Options.ContainsKey(userOption));
            if (action == null) throw new ArgumentException("Action not found");
            var executorsIds = new Dictionary<string, IEnumerable<string>> { { ScopeEnum.Coz, executors } };
            var result = await _wflogic.PublishUserAction(action.Key, userOption, executorsIds, value, agreements);
            return new { result };
        }
    }
}
