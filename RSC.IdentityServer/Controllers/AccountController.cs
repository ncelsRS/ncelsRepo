using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Teme.Identity.Logic.Dtos;
using Teme.Identity.Logic.IUser;
using Teme.SharedApi.Controllers;

namespace RSC.IdentityServer.Controllers
{
    public class AccountController : BaseController<IUserLogic>
    {
        public AccountController(IUserLogic logic) : base(logic)
        {
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] InLoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Json(await Logic.Login(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}