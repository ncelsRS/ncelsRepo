using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RSC.IdentityServer4.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RedirectTo([FromQuery]string returnUrl, [FromQuery] string authData)
        {
            Response.Cookies.Append("auth", authData, new CookieOptions { Domain = new Uri(returnUrl).AbsoluteUri });
            Response.StatusCode = (int)HttpStatusCode.Redirect;
            return Redirect(returnUrl);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
