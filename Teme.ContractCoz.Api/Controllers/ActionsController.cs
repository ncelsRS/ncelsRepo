using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.ContractCoz.Logic;

namespace Teme.ContractCoz.Api.Controllers
{
    public class ActionsController : BaseController<IContractCozLogic>
    {
        public ActionsController(IContractCozLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Отправка договора исполнителю ЦОЗ
        /// </summary>
        /// <param name="dbem"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.SelectExecutors + "/" + UserOptions.SelectExecutors + "/{workflowId}/{userId}")]
        public async Task<IActionResult> DistributionByExecutors([FromRoute] string workflowId, [FromRoute] int userId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.DistributionByExecutors(UserPromts.SelectExecutors, UserOptions.SelectExecutors, workflowId, userId);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
