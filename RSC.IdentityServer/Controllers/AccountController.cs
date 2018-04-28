using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RSC.IdentityServer.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Teme.Identity.Logic.IUser;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Repos;
using Teme.Shared.Data.Repos.IUser;
using Teme.Shared.Logic;
using Teme.Shared.Logic.IUser;
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
                return await Logic.Login(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
