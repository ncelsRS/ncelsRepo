using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Logic;

namespace Teme.Contract.Api.Controllers
{
    [Route("[controller]")]
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

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
//                var result = await _logic.Create();
                return Json(null);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }

        [Route("sendToNcels/:contractId")]
        [HttpPut]
        public async Task<IActionResult> SendToNcels([FromRoute] [Required] string contractId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
//                var res = await _logic.SendToNcels(contractId);
                return Json(null);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, contractId);
            }
        }
    }
}