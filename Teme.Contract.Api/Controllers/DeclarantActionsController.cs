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
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.Delete + "/{workflowId}")]
        public async Task<IActionResult> SendOrRemoveDelete([FromRoute] string workflowId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.Delete,
                    workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithSign + "/{workflowId}")]
        public async Task<IActionResult> SendOrRemoveSendWithSign([FromRoute] string workflowId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithSign,
                    workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }

        [HttpPost]
        [Route(UserPromts.Declarant.SendOrRemove + "/" + UserOptions.SendWithoutSign + "/{workflowId}")]
        public async Task<IActionResult> SendOrRemoveSendWithoutSign([FromRoute] string workflowId)
        {
            try
            {
                var r = await Logic.PublishUserAction(UserPromts.Declarant.SendOrRemove, UserOptions.SendWithoutSign,
                    workflowId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, workflowId);
            }
        }
    }
}