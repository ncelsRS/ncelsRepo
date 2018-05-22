using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.Declaration.Logic;
using Teme.Declaration.Logic.DeclarantActions;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using Teme.SharedApi.Controllers;

namespace Teme.Declaration.Api.Controllers
{
    public class DeclarantActionsController : BaseController<IDeclarantActionsLogic>
    {
        public DeclarantActionsController(IDeclarantActionsLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Созадение заявления
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create()
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
        /// ДОступные действия для пользователя
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UserActions([FromQuery][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.UserActions(workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Удаление заявления
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.Delete + "/{contractType}")]
        public async Task<IActionResult> SendOrRemoveDelete([FromRoute][Required] string workflowId)
        {
            try
            {
                var r = await Logic.SendOrRemoveDelete(workflowId);
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
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithSign + "/{workflowId}")]
        public async Task<IActionResult> SendOrRemoveSendWithSign([FromRoute][Required] string workflowId)
        {
            try
            {
                var r = await Logic.SendOrRemoveSendWithSign(workflowId);
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
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithoutSign + "/{workflowId}")]
        public async Task<IActionResult> SendOrRemoveSendWithoutSign([FromRoute] string workflowId)
        {
            try
            {
                var r = await Logic.SendOrRemoveSendWithoutSign(workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }
    }
}
