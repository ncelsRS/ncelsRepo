using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Infrastructure.Primitives;
using Teme.Contract.Infrastructure.Primitives.Enums;
using Teme.Contract.Logic.Clients;
using Teme.ContractCoz.Logic;
using Teme.ContractCoz.Logic.Actions;
using Teme.SharedApi.Controllers;

namespace Teme.ContractCoz.Api.Controllers
{
    public class ActionsController : BaseController<IActionsLogic>
    {
        public ActionsController(IActionsLogic logic) : base(logic)
        {
        }

        /// <summary>
        /// Отправка договора исполнителю ЦОЗ
        /// </summary>
        /// <param name="dbem"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(UserPromts.SelectExecutors + "/" + UserOptions.SelectExecutors + "/{workflowId}/{userId}/{contractType}")]
        public async Task<IActionResult> DistributionByExecutors([FromRoute][Required] string workflowId, [FromRoute][Required] int userId, [FromRoute][Required] ContractTypeEnum contractType)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var r = await Logic.DistributionByExecutors(workflowId, userId, contractType);
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}
