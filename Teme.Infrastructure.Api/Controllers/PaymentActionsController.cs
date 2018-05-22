using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Infrastructure.Logic;
using Teme.Infrastructure.Logic.Payments;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using Teme.SharedApi.Controllers;

namespace Teme.Infrastructure.Api.Controllers
{
    [AllowAnonymous]
    public class PaymentActionsController : BaseController<IPaymentActionLogic>
    {
        public PaymentActionsController(IPaymentActionLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Созадение заявки на платеж
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<object> CreatePayment()
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
        /// <returns></returns>
        [HttpPost]
        [Route("SendPaymentToNcels")]
        public async Task<object> SendPaymentToNcels([FromQuery][Required] string workflowId)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Gv, false }, { ScopeEnum.Def, false } };
                var executors = new[] { "BossGv" };
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithoutSign, null, workflowId, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Отправка заявки исполнителю ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SendToGvExecutor")]
        public async Task<object> SendToGvExecutor([FromQuery][Required] string workflowId, [FromQuery][Required] int userId)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Gv, false }, { ScopeEnum.Def, false } };
                var executors = new string[] { userId.ToString() };
                var r = await Logic.PublishUserAction(UserPromts.SelectExecutors, UserOptions.SelectExecutors, null, workflowId, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Согласование исполнителем ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree">true - соответствует требованиясм, false - не соответствует требованиям</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GvExecutorAgreementsRequest")]
        public async Task<object> GvExecutorAgreementsRequest([FromQuery][Required] string workflowId, [FromQuery][Required] bool agree)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Gv, false }, { ScopeEnum.Def, false } };
                var executors = new[] { "BossGv" };
                var userOption = agree ? UserOptions.MeetRequirements : UserOptions.NotMeetRequirements;
                var r = await Logic.PublishUserAction(UserPromts.GvExecutorAgreements, userOption, null, workflowId, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Согласование руководителем ГВ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree">true - соответствует требованиясм, false - не соответствует требованиям</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GvBossAgreementsRequest")]
        public async Task<object> GvBossAgreementsRequest([FromQuery][Required] string workflowId, [FromQuery][Required] bool agree)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Gv, agree }, { ScopeEnum.Def, false } };
                var executors = new[] { "BossGv" };
                var userOption = agree ? UserOptions.MeetRequirements : UserOptions.NotMeetRequirements;
                var r = await Logic.PublishUserAction(UserPromts.GvBossAgreements, userOption, null, workflowId, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Согласование исполнителем Деф
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="agree">true - соответствует требованиясм, false - не соответствует требованиям</param>
        /// <returns></returns>
        [HttpPost]
        [Route("DefExecutorAgreementsRequest")]
        public async Task<object> DefExecutorAgreementsRequest([FromQuery][Required] string workflowId, [FromQuery][Required] bool agree)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Gv, true }, { ScopeEnum.Def, agree } };
                var executors = new[] { "BossDef" };
                var userOption = agree ? UserOptions.MeetRequirements : UserOptions.NotMeetRequirements;
                var r = await Logic.PublishUserAction(UserPromts.DefExecutorAgreements, userOption, null, workflowId, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }


        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterPayment")]
        public async Task<object> RegisterPayment([FromQuery][Required] string workflowId)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Gv, true }, { ScopeEnum.Def, true } };
                var executors = new[] { "CozExecutor" };
                var r = await Logic.PublishUserAction(UserPromts.RegisterPayment, UserOptions.Register, null, workflowId, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

    }
}
