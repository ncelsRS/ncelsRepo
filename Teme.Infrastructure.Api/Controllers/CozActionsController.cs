using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Infrastructure.Logic;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using Teme.SharedApi.Controllers;

namespace Teme.Infrastructure.Api.Controllers
{
    /// <summary>
    /// Действия руководителя и исполнителя ЦОЗ
    /// </summary>
    [AllowAnonymous]
    public class CozActionsController : BaseController<IActionsLogic>
    {
        public CozActionsController(IActionsLogic logic) : base(logic)
        {
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
        public async Task<object> SendToCozExecutor([FromQuery][Required] string workflowId, [FromQuery][Required] int userId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.SelectExecutors, UserOptions.SelectExecutors, null, workflowId, null, new string[] { userId.ToString() }, new Dictionary<string, bool> { { ScopeEnum.Coz, false }, { ScopeEnum.CozBoss, false }, { ScopeEnum.Ceo, false } });
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Согласование исполнителем ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executors"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CozExecutorAgreementsRequest")]
        public async Task<object> CozExecutorAgreementsRequest([FromQuery][Required] string workflowId, [FromQuery][Required] bool agree)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Coz, agree }, { ScopeEnum.CozBoss, false }, { ScopeEnum.Ceo, false } };
                var executors = new[] { "BossCoz" };
                var userOption = agree ? UserOptions.MeetRequirements : UserOptions.NotMeetRequirements;
                var r = await Logic.PublishUserAction(UserPromts.IsMeetRequirements, userOption, null, workflowId, null, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Возврат заявителю договора
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ReturnToDeclarant")]
        public async Task<object> ReturnToDeclarant([FromQuery][Required] string workflowId)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Coz, false }, { ScopeEnum.CozBoss, false }, { ScopeEnum.Ceo, false } };
                var executors = new[] { "CozExecutor" };
                var r = await Logic.PublishUserAction(UserPromts.ReturnToDeclarant, UserOptions.ReturnToDeclarant, null, workflowId, null, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }


        /// <summary>
        /// Согласование Руководителем ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executors"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CozBossAgreementsRequest")]
        public async Task<object> CozBossAgreementsRequest([FromQuery][Required] string workflowId, [FromQuery][Required] bool agree)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Coz, true }, { ScopeEnum.CozBoss, agree }, { ScopeEnum.Ceo, false } };
                var executors = new[] { "BossCoz" };
                var userOption = agree ? UserOptions.MeetRequirements : UserOptions.NotMeetRequirements;
                var r = await Logic.PublishUserAction(UserPromts.CozBossAgreements, userOption, null, workflowId, null, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Согласование ЗамГенДир
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="executors"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CozCeoAgreementsRequest")]
        public async Task<object> CozCeoAgreementsRequest([FromQuery][Required] string workflowId, [FromQuery][Required] bool agree)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Coz, true }, { ScopeEnum.CozBoss, true }, { ScopeEnum.Ceo, agree } };
                var executors = new[] { "BossCoz" };
                var userOption = agree ? UserOptions.MeetRequirements : UserOptions.NotMeetRequirements;
                var r = await Logic.PublishUserAction(UserPromts.CeoAgreements, userOption, null, workflowId, null, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        /// <summary>
        /// Регистрация договора
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegisterContract")]
        public async Task<object> RegisterContract([FromQuery][Required] string workflowId)
        {
            try
            {
                var agreements = new Dictionary<string, bool> { { ScopeEnum.Coz, true }, { ScopeEnum.CozBoss, true }, { ScopeEnum.Ceo, true } };
                var executors = new[] { "BossCoz" };
                var r = await Logic.PublishUserAction(UserPromts.RegisterContract, UserOptions.Register, null, workflowId, null, executors, agreements);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }
    }
}
