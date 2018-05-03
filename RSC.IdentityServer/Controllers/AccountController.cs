using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Teme.Identity.Logic;
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

        [AllowAnonymous]
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
                return ExceptionResult(ex);
            }
        }

        [HttpGet]
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                return Json(await Logic.UpdateToken(CurrentUserId));
            }
            catch (Exception ex)
            {
                return ExceptionResult(ex, CurrentUserId);
            }
        }
    }
}