using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Logic.Clients;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;

namespace Teme.ContractCoz.Logic.Actions
{
    public class ActionsLogic : EntityLogic, IActionsLogic
    {
        private readonly IConfiguration _config;
        public ActionsLogic(IEntityRepo repo, IConfiguration config) : base(repo)
        {
            _config = config;
        }

        /// <summary>
        /// Отправка договора выбранному испольнителю ЦОЗ
        /// </summary>
        /// 
        /// <returns></returns>
        public async Task<object> DistributionByExecutors(string workflowId, int userId, ContractTypeEnum contractType)
        {
            var client = new CozActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendToCozExecutorAsync(workflowId, new string[] { userId.ToString() }, contractType);
        }
    }
}
