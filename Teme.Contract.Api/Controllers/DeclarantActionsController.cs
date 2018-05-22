using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Logic.DeclarantActions;
using Teme.Shared.Data.Primitives.Contract;
using Teme.Shared.Data.Primitives.Workflow.Enums;
using Teme.SharedApi.Controllers;

namespace Teme.Contract.Api.Controllers
{
    public class DeclarantActionsController : BaseController<IDeclarantActionsLogic>
    {
        public DeclarantActionsController(IDeclarantActionsLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Удаление договора
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="contractType"></param>
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.Delete + "/{contractType}/{workflowId}")]
        public async Task<IActionResult> SendOrRemoveDelete([FromRoute] string workflowId, [FromRoute] ContractTypeEnum contractType)
        {
            try
            {
                var r = await Logic.SendOrRemoveDelete(workflowId, contractType);
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
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithSign + "/{contractType}/{workflowId}")]
        public async Task<IActionResult> SendOrRemoveSendWithSign([FromRoute] string workflowId, [FromRoute] ContractTypeEnum contractType)
        {
            try
            {
                var r = await Logic.SendOrRemoveSendWithSign(workflowId, contractType);
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
        /// <param name="contractId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithoutSign + "/{contractType}/{workflowId}")]
        public async Task<IActionResult> SendOrRemoveSendWithoutSign([FromRoute] string workflowId, [FromRoute] ContractTypeEnum contractType)
        {
            try
            {
                var r = await Logic.SendOrRemoveSendWithoutSign(workflowId, contractType);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }
    }
}
