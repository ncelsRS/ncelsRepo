using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Logic;

namespace Teme.Contract.Api.Controllers
{
    public class ContractController : BaseController
    {
        private readonly IContractLogic _logic;

        public ContractController(IContractLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [Route(":contractId")]
        public async Task<IActionResult> Create()
        {
            try
            {
                var r = await _logic.Create();
                return Json(r);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}