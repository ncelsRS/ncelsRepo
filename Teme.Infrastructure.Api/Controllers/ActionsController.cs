using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Data.Model;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Infrastructure.Logic;
using Teme.Shared.Data.Primitives.Contract;
using Teme.SharedApi.Controllers;

namespace Teme.Infrastructure.Api.Controllers
{
    [AllowAnonymous]
    public class ActionsController : BaseController<IActionsLogic>
    {
        public ActionsController(IActionsLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// UserActions
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [Route("List")]
        [HttpGet]
        public async Task<object> List([FromQuery] [Required] string workflowId)
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
        /// Созадение договора
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<object> Create([FromBody][Required]CreateModel createModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.Create(createModel);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Удаление договора
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="contractType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendOrRemoveDelete")]
        public async Task<object> SendOrRemoveDelete([FromQuery][Required] string workflowId, [FromQuery][Required] ContractTypeEnum contractType)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.Delete, contractType, workflowId, "declarant", new[] { "BossCoz" });
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Отпрвка договора с подписью в ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="contractType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendOrRemoveSendWithSign")]
        public async Task<object> SendOrRemoveSendWithSign([FromQuery][Required] string workflowId, [FromQuery][Required] ContractTypeEnum contractType)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithSign, contractType, workflowId, "declarant", new[] { "BossCoz" });
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Отправка договора без подписи в ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="contractType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendOrRemoveSendWithoutSign")]
        public async Task<object> SendOrRemoveSendWithoutSign([FromQuery][Required] string workflowId, [FromQuery][Required] ContractTypeEnum contractType)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithoutSign, contractType, workflowId, "declarant", new[] { "BossCoz" });
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Отправка договора исполнителю ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executors"></param>
        /// <param name="contractType"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendToCozExecutor")]
        public async Task<object> SendToCozExecutor([FromQuery][Required] string workflowId, [FromQuery][Required] IEnumerable<string> executors, [FromQuery][Required] ContractTypeEnum contractType)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.SelectExecutors, UserOptions.SelectExecutors, contractType, workflowId, null, executors);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Согласовано исполнителям ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executors"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CozExecutorAgreedRequest")]
        public async Task<object> CozExecutorAgreedRequest([FromQuery][Required] string workflowId, [FromQuery][Required] IEnumerable<string> executors)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.IsMeetRequirements, UserOptions.MeetRequirements, true, workflowId, null, executors);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// НЕ Согласовано исполнителям ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executors"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CozExecutorNotAgreedRequest")]
        public async Task<object> CozExecutorNotAgreedRequest([FromQuery][Required] string workflowId, [FromQuery][Required] IEnumerable<string> executors)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.SelectExecutors, UserOptions.SelectExecutors, false, workflowId, null, executors);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Согласование руководителем ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executors"></param>
        /// <param name="contractType"></param>
        /// <returns></returns>
        //[HttpPost]
        //[Route("CozBossAgreeRequest")]
        //public async Task<object> CozBossAgreeRequest([FromQuery][Required] string workflowId, [FromQuery][Required] IEnumerable<string> executors, [FromQuery][Required] ContractTypeEnum contractType)
        //{
        //    try
        //    {
        //        var r = await Logic.PublishUserAction(UserPromts.SelectExecutors, UserOptions.SelectExecutors, contractType, workflowId, null, executors);
        //        return Json(r);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ExceptionResult(ex, workflowId);
        //    }
        //}
    }
}
