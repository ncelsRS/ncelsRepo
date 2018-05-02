using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using Teme.Shared.Logic;

namespace Teme.SharedApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class BaseController<TILogic> : Controller where TILogic : IBaseLogic
    {
        protected TILogic Logic { get; set; }

        public BaseController(TILogic logic)
        {
            Logic = logic;
        }

        private int _userId = 0;
        protected int CurrentUserId
        {
            get
            {
                if (_userId > 0) return _userId;
                var claimValue = User
                    .Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)
                    .Value;
                return int.Parse(claimValue);
            }
        }

        protected IActionResult ExceptionResult(Exception ex, object args = null)
        {
            var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            var actionName = ControllerContext.ActionDescriptor.ActionName;
            var msg = $"{controllerName} {actionName} {ex.Message}";

            if (ex is ArgumentException)
            {
                Log.Warning(ex, msg, args);
#if DEBUG
                return BadRequest(ex);
#else
            return BadRequest(msg);
#endif
            }

            Log.Error(ex, msg, args);
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
#if DEBUG
            return Json(ex);
#else
            return Json(msg);
#endif
        }
    }
}