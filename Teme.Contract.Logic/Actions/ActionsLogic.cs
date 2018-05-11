using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Infrastructure;
using Teme.Contract.Logic.Clients;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.ContractLogic;
using WorkflowCore.Users.Models;

namespace Teme.Contract.Logic.Actions
{
    public class ActionsLogic : EntityLogic, IActionsLogic
    {
        public ActionsLogic(IEntityRepo repo) : base(repo)
        {
        }

        public async Task<IEnumerable<OpenUserAction>> OpenUserActions(string workflowId)
        {
            var result = await UserActions(workflowId);
            return result;
        }

        public async Task<IEnumerable<OpenUserAction>> UserActions(string workflowId)
        {
            var client = new ActionsClient() { BaseUrl = "http://localhost:24870" };
            var response = await client.CreateAsync(new CreateModel()
            {
                ContractType = ContractTypeEnum.OneToOne,
                ContractScope = "national"
            });
            return response as IEnumerable<OpenUserAction>;
        }
    }
}
