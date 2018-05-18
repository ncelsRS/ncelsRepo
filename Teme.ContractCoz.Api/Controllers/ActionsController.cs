using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.ContractCoz.Logic;
using Teme.ContractCoz.Logic.Actions;
using Teme.SharedApi.Controllers;

namespace Teme.ContractCoz.Api.Controllers
{
    public class ActionsController : BaseController<IActionsLogic>
    {
        public ActionsController(IActionsLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Отправка договора исполнителю ЦОЗ
        /// </summary>
        /// <param name="dbem"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.SelectExecutors + "/" + UserOptions.SelectExecutors + "/{workflowId}/{userId}")]
        public async Task<IActionResult> DistributionByExecutors([FromRoute][Required] string workflowId, [FromRoute][Required] int userId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.DistributionByExecutors(workflowId, userId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// соотвествует требованиям исполнителя ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.IsMeetRequirements + "/" + UserOptions.MeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> CozExecutorAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.CozExecutorAgreementsRequest(workflowId, true);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// НЕ соотвествует требованиям исполнителя ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.IsMeetRequirements + "/" + UserOptions.NotMeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> CozExecutorNotAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.CozExecutorAgreementsRequest(workflowId, false);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// соотвествует требованиям Руководителя ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.CozBossAgreements + "/" + UserOptions.MeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> CozBossAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.CozBossAgreementsRequest(workflowId, true);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// НЕ соотвествует требованиям Руководителя ЦОЗ
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.CozBossAgreements + "/" + UserOptions.NotMeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> CozBossNotAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.CozBossAgreementsRequest(workflowId, false);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// соотвествует требованиям ЗамГенДир
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.CeoAgreements + "/" + UserOptions.MeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> CozCeoAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.CozCeoAgreementsRequest(workflowId, true);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// НЕ соотвествует требованиям ЗамГенДир
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.CeoAgreements + "/" + UserOptions.NotMeetRequirements + "/{workflowId}")]
        public async Task<IActionResult> CozCeoNotAgreedRequest([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.CozCeoAgreementsRequest(workflowId, false);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        /// <summary>
        /// Регистрация договора
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.RegisterContract + "/" + UserOptions.Register + "/{workflowId}")]
        public async Task<IActionResult> RegisterContract([FromRoute][Required] string workflowId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.RegisterContract(workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
