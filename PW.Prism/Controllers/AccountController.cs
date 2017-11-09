using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json.Schema;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Recources;
using ncelsEntities = PW.Ncels.Database.DataModel.ncelsEntities;

namespace PW.Prism.Controllers
{
	public class AccountController : Controller
	{


		private void SaveUserName()
		{
			// Associate shopping cart items with logged-in user
			Employee employee = UserHelper.GetCurrentEmployee();
			if (employee != null)
			{
				Session[UserHelper.PrismSessionKey] = employee.DisplayName;
			}

		}

		//
		// GET: /Account/LogOn

		public ActionResult LogOn()
		{

                return View();
		}

		public ActionResult Profile() {

			ChangePasswordModel model = new ChangePasswordModel();
			model.DisplayName = UserHelper.GetCurrentName();

			return PartialView(model);
		}

		[Authorize]
		[HttpPost]
		
		public ActionResult ChangePassword(ChangePasswordModel model)
		{
			if (ModelState.IsValid)
			{

				Employee employee = UserHelper.GetCurrentEmployee();
				if (employee != null)
				{
					MembershipUser user = Membership.GetUser(employee.Login);
					if (user != null)
					{
						user.ChangePassword(model.OldPassword, model.NewPassword);
                        ActionLogger.WriteInt("Сменил себе пароль");
						return Content(bool.TrueString);
                    }
				}
			}
            ActionLogger.WriteInt("Пытался сменить себе пароль");
			return Content(bool.FalseString);


        }
        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            //if (DateTime.Now > new DateTime(2016, 12, 31))
            //	return RedirectToAction("LogOn", "Account");

            if (ModelState.IsValid)
            {
                ncelsEntities ncelsEntities = new ncelsEntities();


                Employee employee = ncelsEntities.Employees.Include("Position").FirstOrDefault(o => o.Login == model.UserName);

                if (employee != null && employee.Position.PositionState == 1)
                {
                    if (Membership.ValidateUser(model.UserName, model.Password))
                    {


                        FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                        Session[UserHelper.ConnectKey] = DateTime.Now.Year.ToString();
                        SaveUserName();

                        ActionLogger.WriteInt("Вход в систему: " + model.UserName, "Успех");

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        var user = Membership.GetUser(model.UserName);
                        if (user != null && (user.IsLockedOut || !user.IsApproved))
                        {
                            ModelState.AddModelError("", Messages.AccountController_LogOn_Пользователь_заблокирован_);
                            ActionLogger.WriteInt("Вход в систему: " + model.UserName, "Пользователь заблокирован");
                        }
                        else
                        {
                            ModelState.AddModelError("", Messages.AccountController_LogOn_Имя_пользователя_или_пароль_не_верны_);
                            ActionLogger.WriteInt("Вход в систему: " + model.UserName, "Неверное имя пользователя или пароль");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", Messages.AccountController_LogOn_Пользователь_заблокирован_);
                    ActionLogger.WriteInt("Вход в систему: " + model.UserName, "Пользователь заблокирован/не найден");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

		//
		// GET: /Account/LogOff

		public ActionResult LogOff()
		{
			FormsAuthentication.SignOut();
			Session[UserHelper.PrismSessionKey] = "";
			return RedirectToAction("Index", "Home");
		}

        public ActionResult GetIinOfUser()
        {
            string iin = null;
            Employee employee = UserHelper.GetCurrentEmployee();
            if (employee != null)
            {
                iin = employee.Iin;
            }
            return Json(iin != null ? iin : "", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDateTime()
        {
            return Json(DateTime.Now, JsonRequestBehavior.AllowGet);
        }
    }
}
