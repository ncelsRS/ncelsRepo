using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Logic;

namespace Teme.Contract.Api.Controllers
{
    public class ContractController : BaseController<IContractLogic>
    {
        public ContractController(IContractLogic logic) : base(logic)
        {
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var r = await Logic.Create();
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}