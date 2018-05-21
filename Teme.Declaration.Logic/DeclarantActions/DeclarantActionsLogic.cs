using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teme.Declaration.Logic.DeclarantActions;
using Teme.Shared.Data.Repos;
using Teme.Shared.Logic;
using Teme.Shared.Logic.Clients;

namespace Teme.Declaration.Logic.DeclarationActions
{
    public class DeclarantActionsLogic : EntityLogic, IDeclarantActionsLogic
    {
        private readonly IConfiguration _config;
        public DeclarantActionsLogic(IEntityRepo repo, IConfiguration config) : base(repo)
        {
            _config = config;
        }

        /// <summary>
        /// Создание договора
        /// </summary>
        /// <returns></returns>
        public async Task<object> Create()
        {
            var client = new DeclarationActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            var workflowId = await client.CreateAsync();
            return workflowId;
        }

        /// <summary>
        /// Доступные действия для пользователя
        /// </summary>
        /// <returns></returns>
        public async Task<object> UserActions(string workflowId)
        {
            var client = new DeclarationActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.UserActionsAsync(workflowId);
        }

        /// <summary>
        /// Удаление заявления
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task<object> SendOrRemoveDelete(string workflowId)
        {
            var client = new DeclarationActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendOrRemoveDeleteAsync(workflowId);
        }

        /// <summary>
        /// ОТправка заявления в ЦОЗ с подписью
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task<object> SendOrRemoveSendWithSign(string workflowId)
        {
            var client = new DeclarationActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendOrRemoveSendWithSignAsync(workflowId);
        }

        /// <summary>
        /// ОТправка заявления в ЦОЗ без подписи
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task<object> SendOrRemoveSendWithoutSign(string workflowId)
        {
            var client = new DeclarationActionsClient() { BaseUrl = _config["Urls:InfrastructureApi"] };
            return await client.SendOrRemoveSendWithoutSignAsync(workflowId);
        }
    }
}
