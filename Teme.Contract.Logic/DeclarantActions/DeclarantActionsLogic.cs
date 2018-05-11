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

namespace Teme.Contract.Logic.DeclarantActions
{
    public class DeclarantActionsLogic : EntityLogic, IDeclarantActionsLogic
    {
        public DeclarantActionsLogic(IEntityRepo repo) : base(repo)
        {
        }

        public async Task<object> PublishUserAction(string userPromt, string userOption, ContractTypeEnum contractType, string workflowId)
        {

            //var actions = await _wflogic.GetUserActions(workflowId, "declarant");
            //var action = actions
            //    .FirstOrDefault(x => x.Prompt == userPromt && x.Options.ContainsKey(userOption));
            //if (action == null) throw new ArgumentException("Action not found");
            //var executorsIds = new Dictionary<string, IEnumerable<string>> { { ScopeEnum.Coz, new[] { "BossCoz" } } };
            //var r = await _wflogic.PublishUserAction(action.Key, userOption, executorsIds, contractType);
            //return new { r };
            return new { r = "" };
        }

        //public async Task<object> SendOrRemoveDelete(string workflowId, ContractTypeEnum contractType)
        //{
        //    var client = new ActionsClient() { BaseUrl = "http://localhost:24870" };
        //    var response = await client.CreateAsync(new CreateModel()
        //    {
        //        ContractType = contractType,
        //        ContractScope = "national"
        //    });
        //    return response;
        //}
    }
}
