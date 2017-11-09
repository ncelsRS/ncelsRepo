using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Notifications;

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
    }
}