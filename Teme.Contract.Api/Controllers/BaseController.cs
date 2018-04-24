using System;
using System.Linq.Expressions;
using System.Net;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using Serilog;

namespace Teme.Contract.Api.Controllers
{
    [Route("")]
    public class BaseController : Controller
    {
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
            Response.StatusCode = (int) HttpStatusCode.InternalServerError;
#if DEBUG
            return Json(ex);
#else
            return Json(msg);
#endif
        }
    }
}