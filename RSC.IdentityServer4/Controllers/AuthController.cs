using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RSC.IdentityServer4.Model;

namespace RSC.IdentityServer4.Controllers
{
    public class AuthController : Controller
    {

        [HttpPost]
        [Route("api/Auth/Register")]
        public int Register([FromBody] User user)
        {
            return 1;
        }

        [HttpPost]
        [Route("api/Auth/Login")]
        public int Login([FromBody] User user)
        {
            return 1;
        }

    }
}
