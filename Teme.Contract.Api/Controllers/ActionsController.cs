using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Logic;
using Teme.Contract.Logic.Actions;

namespace Teme.Contract.Api.Controllers
{
    public class ActionsController : BaseController
    {
        private readonly IActionsLogic _logic;

        public ActionsController(IActionsLogic logic)
        {
            _logic = logic;
        }

        [Route("contract/:contractId")]
        [HttpGet]
        public async Task<IActionResult> List([FromRoute] [Required] string contractId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var r = await _logic.OpenUserActions(contractId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, contractId);
            }
        }

    }
}