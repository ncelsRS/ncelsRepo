using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Logic.DeclarantActions;
using Teme.Shared.Data.Primitives.Contract;

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
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.Delete + "/{contractType}/{workflowId}/{contractId}")]
        public async Task<IActionResult> SendOrRemoveDelete([FromRoute] string workflowId, [FromRoute] ContractTypeEnum contractType, [FromRoute] int contractId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.Delete, contractType,
                    workflowId, contractId);
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
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithSign + "/{contractType}/{workflowId}/{contractId}")]
        public async Task<IActionResult> SendOrRemoveSendWithSign([FromRoute] string workflowId, [FromRoute] ContractTypeEnum contractType, [FromRoute] int contractId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove,  UserOptions.SendWithSign, contractType,
                    workflowId, contractId);
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
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithoutSign + "/{contractType}/{workflowId}/{contractId}")]
        public async Task<IActionResult> SendOrRemoveSendWithoutSign([FromRoute] string workflowId, [FromRoute] ContractTypeEnum contractType, [FromRoute] int contractId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithoutSign, contractType,
                    workflowId, contractId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }
    }
}