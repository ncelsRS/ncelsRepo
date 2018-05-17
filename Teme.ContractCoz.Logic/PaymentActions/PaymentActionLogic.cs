using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.Clients;

namespace Teme.ContractCoz.Logic.PaymentActions
{
    public class PaymentActionLogic : EntityLogic, IPaymentActionLogic
    {
        private readonly IConfiguration _config;
        public PaymentActionLogic(IEntityRepo repo, IConfiguration config) : base(repo)
        {
            _config = config;
        }

        /// <summary>
        /// Созадание заявки на платеж
        /// </summary>
        /// <returns></returns>
        public async Task<object> CreatePayment()
        {
            var client = new PaymentActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.CreatePaymentAsync();
        }

        /// <summary>
        /// Отправка договора в НЦЭЛС
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task<object> SendPaymentToNcels(string workflowId)
        {
            var client = new PaymentActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendPaymentToNcelsAsync(workflowId);
        }

        /// <summary>
        /// Отправка исполнителю ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<object> SendToGvExecutor(string workflowId, int userId)
        {
            var client = new PaymentActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendToGvExecutorAsync(workflowId, userId);
        }

        /// <summary>
        /// Согласование исполнителем ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        public async Task<object> GvExecutorAgreementsRequest(string workflowId, bool agree)
        {
            var client = new PaymentActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.GvExecutorAgreementsRequestAsync(workflowId, agree);
        }

        /// <summary>
        /// Согласование руководителем ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        public async Task<object> GvBossAgreementsRequest(string workflowId, bool agree)
        {
            var client = new PaymentActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.GvBossAgreementsRequestAsync(workflowId, agree);
        }

        /// <summary>
        /// Согласование исполнителем ДЭФ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree"></param>
        /// <returns></returns>
        public async Task<object> DefExecutorAgreementsRequest(string workflowId, bool agree)
        {
            var client = new PaymentActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.DefExecutorAgreementsRequestAsync(workflowId, agree);
        }

        /// <summary>
        /// Регистрация заявки
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task<object> RegisterPayment(string workflowId)
        {
            var client = new PaymentActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.RegisterPaymentAsync(workflowId);
        }
    }
}
