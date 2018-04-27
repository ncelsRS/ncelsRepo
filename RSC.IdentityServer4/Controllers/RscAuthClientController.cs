using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Contract.Api.Controllers;
using Teme.Identity.Logic.IUser;

namespace RSC.IdentityServer4.Controllers
{
    public class RscAuthClientController : BaseController<IUserLogic>
    {
        public RscAuthClientController(IUserLogic logic) : base(logic)
        {
        }

        public async Task<IActionResult> Index()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Json(null);
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex);
            }
        }
    }
}