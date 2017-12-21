using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Helpers;
using WebApplicationCulture.Helper;
using System.Threading;

namespace PW.Ncels.Controllers
{
    public abstract class ACommonController : Controller
    {
        protected ACommonController()
        {
            var user = UserHelper.GetCurrentEmployee();
            int count;
            if (user==null)
            {
                count = 0;
            }else
            {
                count  = new NotificationManager().GetCountNotification(user.Id);
            }
            ViewBag.NotificationCount = count;
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
    }
}