using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure.Primitives;
using Teme.ContractCoz.Logic.PaymentActions;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using Teme.SharedApi.Controllers;

namespace Teme.ContractCoz.Api.Controllers
{
    public class PaymentActionController : BaseController<IPaymentActionLogic>
    {
        public PaymentActionController(IPaymentActionLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Созадение заявки на плажет
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IActionResult> CreatePayment()
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.CreatePayment();
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Отправка заявки на платеж в ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithoutSign + "/{workflowId}")]
        public async Task<IActionResult> SendPaymentToNcels([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.SendPaymentToNcels(workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Отправка заявки исполнителю ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executors"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.SelectExecutors + "/" + UserOptions.SelectExecutors + "/{workflowId}/{userId}")]
        public async Task<object> SendToGvExecutor([FromRoute][Required] string workflowId, [FromRoute][Required] int userId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.SendToGvExecutor(workflowId, userId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// соотвествует требованиям исполнителя Гв
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.GvExecutorAgreements + "/" + UserOptions.MeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> GvExecutorAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.GvExecutorAgreementsRequest(workflowId, true);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// НЕ соотвествует требованиям исполнителя Гв
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.GvExecutorAgreements + "/" + UserOptions.NotMeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> GvExecutorNotAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.GvExecutorAgreementsRequest(workflowId, false);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// соотвествует требованиям руководителя Гв
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.GvBossAgreements + "/" + UserOptions.MeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> GvBossAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.GvBossAgreementsRequest(workflowId, true);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// НЕ соотвествует требованиям руководителя Гв
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.GvBossAgreements + "/" + UserOptions.NotMeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> GvBossNotAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.GvBossAgreementsRequest(workflowId, false);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// соотвествует требованиям исполнителя ДЭФ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.DefExecutorAgreements + "/" + UserOptions.MeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> DefExecutorAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.DefExecutorAgreementsRequest(workflowId, true);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// НЕ соотвествует требованиям исполнителя Гв
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.DefExecutorAgreements + "/" + UserOptions.NotMeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> DefExecutorNotAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.DefExecutorAgreementsRequest(workflowId, false);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.RegisterPayment + "/" + UserOptions.Register + "/{workflowId}")]
        public async Task<IActionResult> RegisterPayment([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.RegisterPayment(workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
