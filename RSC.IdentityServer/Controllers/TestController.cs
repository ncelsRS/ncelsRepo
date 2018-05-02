using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teme.Identity.Logic;
using Teme.SharedApi.Controllers;

namespace RSC.IdentityServer.Controllers
{
    public class TestController : BaseController<IIdentityLogic>
    {
        public TestController(IIdentityLogic logic) : base(logic)
        {
        }

        [Route("")]
        public async Task<IActionResult> Test()
        {
            return Json(await Logic.Test(CurrentUserId));
        }
    }
}