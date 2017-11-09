using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ncels.Helpers;

namespace PW.Ncels
{
    public class LogInfoAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopWatch.Stop();
            var executionTime = _stopWatch.ElapsedMilliseconds;
            var name = HttpContext.Current.User.Identity.Name;
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            StringBuilder parameters = new StringBuilder();
            foreach (var p in filterContext.ActionDescriptor.GetParameters())
            {
                if (filterContext.Controller.ValueProvider.GetValue(p.ParameterName) != null)
                {
                    parameters.AppendFormat("\r\n\t{0}\t\t:{1}", p.ParameterName, filterContext.Controller.ValueProvider.GetValue(p.ParameterName).AttemptedValue);
                }
            }
            LogHelper.Log.Info("ip: " + ipAddress + " user:" + name + " OnActionExecuted - " + controllerName + "\\" + actionName + " time:" + executionTime + "ms" + parameters);
            base.OnActionExecuted(filterContext);
        }
    }
}