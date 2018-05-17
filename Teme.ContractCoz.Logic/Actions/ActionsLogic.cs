using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.Clients;

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
        /// <returns></returns>
        public async Task<object> DistributionByExecutors(string workflowId, int userId)
        {
            var client = new CozActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendToCozExecutorAsync(workflowId, userId);
        }

        /// <summary>
        /// Согласование исполнителем ЦОЗ 
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree">true Согласовано, false Отказ</param>
        /// <returns></returns>
        public async Task<object> CozExecutorAgreementsRequest(string workflowId, bool agree)
        {
            var client = new CozActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.CozExecutorAgreementsRequestAsync(workflowId, agree);
        }

        /// <summary>
        /// Возврат на доработку заявителю
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task<object> ReturnToDeclarant(string workflowId)
        {
            var client = new CozActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.ReturnToDeclarantAsync(workflowId);
        }

        /// <summary>
        /// Согласование Руководителем ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree">true Согласовано, false Отказ</param>
        /// <returns></returns>
        public async Task<object> CozBossAgreementsRequest(string workflowId, bool agree)
        {
            var client = new CozActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.CozBossAgreementsRequestAsync(workflowId, agree);
        }

        /// <summary>
        /// Согласование ЗамГенДир
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        public async Task<object> CozCeoAgreementsRequest(string workflowId, bool agree)
        {
            var client = new CozActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.CozCeoAgreementsRequestAsync(workflowId, agree);
        }
    }
}
