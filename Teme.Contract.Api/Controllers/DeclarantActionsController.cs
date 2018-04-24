using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Logic.DeclarantActions;

namespace Teme.Contract.Api.Controllers
{
    public class DeclarantActionsController : BaseController<IDeclarantActionsLogic>
    {
        protected DeclarantActionsController(IDeclarantActionsLogic logic) : base(logic)
        {
        }

        [HttpPost]
        [Route("{userPromt}/{userOption}/{contractId}")]
        public async Task<IActionResult> SendOrRemove(
            [FromRoute] string userPromt, [FromRoute] string userOption,
            [FromBody] string contractId)
        {
            try
            {
                var r = await Logic.PublishUserAction(userPromt, userOption, contractId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, contractId);
            }
        }
    }
}