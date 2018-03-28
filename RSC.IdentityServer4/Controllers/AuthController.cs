using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Teme.Identity.Logic.Models;

namespace RSC.IdentityServer4.Controllers
{
    public class AuthController : Controller
    {

        [HttpPost]
        [Route("/Auth/Register")]
        public ActionResult Register([FromBody] Register register)
        {
            if (ModelState.IsValid)
            {
                return Ok(register);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}
