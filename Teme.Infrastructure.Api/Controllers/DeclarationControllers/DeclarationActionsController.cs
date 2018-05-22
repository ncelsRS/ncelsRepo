using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.Infrastructure.Logic.Declarations;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using Teme.SharedApi.Controllers;

namespace Teme.Infrastructure.Api.Controllers.DeclarationControllers
{
    [AllowAnonymous]
    public class DeclarationActionsController : BaseController<IDeclarationActionsLogic>
    {
        public DeclarationActionsController(IDeclarationActionsLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Создать заявление
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<object> Create()
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.Create();
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Список доступных действий
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [Route("UserActions")]
        [HttpGet]
        public async Task<object> UserActions([FromQuery] [Required] string workflowId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var r = await Logic.OpenUserActions(workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Удаление заявления
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendOrRemoveDelete")]
        public async Task<object> SendOrRemoveDelete([FromQuery][Required] string workflowId)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Coz, false }, { ScopeEnum.CozBoss, false }, { ScopeEnum.Ceo, false } };
                var executors = new[] { "BossCoz" };
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.Delete, null, workflowId, null, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Отпрвка заявления с подписью в ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendOrRemoveSendWithSign")]
        public async Task<object> SendOrRemoveSendWithSign([FromQuery][Required] string workflowId)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Coz, false }, { ScopeEnum.CozBoss, false }, { ScopeEnum.Ceo, false } };
                var executors = new[] { "BossCoz" };
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithSign, null, workflowId, null, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Отправка заявления без подписи в ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendOrRemoveSendWithoutSign")]
        public async Task<object> SendOrRemoveSendWithoutSign([FromQuery][Required] string workflowId)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Coz, false }, { ScopeEnum.CozBoss, false }, { ScopeEnum.Ceo, false } };
                var executors = new[] { "BossCoz" };
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithoutSign, null, workflowId, null, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }
    }
}
