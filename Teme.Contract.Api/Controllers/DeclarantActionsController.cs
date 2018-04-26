using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Logic.DeclarantActions;

namespace Teme.Contract.Api.Controllers
{
    public class DeclarantActionsController : BaseController<IDeclarantActionsLogic>
    {
        public DeclarantActionsController(IDeclarantActionsLogic logic) : base(logic)
        {
        }

        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.Delete + "/{contractId}")]
        public async Task<IActionResult> SendOrRemoveDelete([FromRoute] string contractId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.Delete,
                    contractId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, contractId);
            }
        }

        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithSign + "/{contractId}")]
        public async Task<IActionResult> SendOrRemoveSendWithSign([FromRoute] string contractId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithSign,
                    contractId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, contractId);
            }
        }

        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithoutSign + "/{contractId}")]
        public async Task<IActionResult> SendOrRemoveSendWithoutSign([FromRoute] string contractId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithoutSign,
                    contractId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, contractId);
            }
        }
    }
}