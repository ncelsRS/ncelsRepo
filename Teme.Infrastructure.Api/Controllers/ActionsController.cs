using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure;
using Teme.Infrastructure.Logic;
using Teme.SharedApi.Controllers;

namespace Teme.Infrastructure.Api.Controllers
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
