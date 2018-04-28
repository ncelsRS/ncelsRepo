using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Logic;
using Teme.Contract.Logic.Actions;

namespace Teme.Contract.Api.Controllers
{
    public class ActionsController : BaseController<IActionsLogic>
    {
        public ActionsController(IActionsLogic logic) : base(logic)
        {
        }

        [Route("contract/{workflowId}")]
        [HttpGet]
        public async Task<IActionResult> List([FromRoute] [Required] string workflowId)
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
    }
}