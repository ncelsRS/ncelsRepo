using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Teme.Contract.Data;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.ContractLogic;
using Teme.Contract.Logic.Clients;
using Microsoft.Extensions.Configuration;

namespace Teme.Contract.Logic.DeclarantActions
{
    public class DeclarantActionsLogic : EntityLogic, IDeclarantActionsLogic
    {
        private readonly IConfiguration _config;
        public DeclarantActionsLogic(IEntityRepo repo, IConfiguration config) : base(repo)
        {
            _config = config;
        }

        public async Task<object> SendOrRemoveDelete(string workflowId, Shared.Data.Primitives.Contract.ContractTypeEnum contractType)
        {
            var client = new ActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendOrRemoveDeleteAsync(workflowId, (Clients.ContractTypeEnum)contractType);
        }

        public async Task<object> SendOrRemoveSendWithSign(string workflowId, Shared.Data.Primitives.Contract.ContractTypeEnum contractType)
        {
            var client = new ActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendOrRemoveSendWithSignAsync(workflowId, (Clients.ContractTypeEnum)contractType);
        }

        public async Task<object> SendOrRemoveSendWithoutSign(string workflowId, Shared.Data.Primitives.Contract.ContractTypeEnum contractType)
        {
            var client = new ActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendOrRemoveSendWithoutSignAsync(workflowId, (Clients.ContractTypeEnum)contractType);
        }
    }
}
