using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Logic;

namespace Teme.Contract.Api.Controllers
{
    public class ContractController : BaseController<IContractLogic>
    {
        protected ContractController(IContractLogic logic) : base(logic)
        {
        }

        [HttpGet]
        [Route(":contractId")]
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