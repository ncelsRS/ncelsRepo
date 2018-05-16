using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;
        public ActionsLogic(IEntityRepo repo, IConfiguration config) : base(repo)
        {
            _config = config;
        }

        public async Task<object> OpenUserActions(string workflowId)
        {
            var client = new ActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.ListAsync(workflowId);
        }
    }
}
