using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using log4net.Repository.Hierarchy;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Recources;
using PW.Ncels.Models;
using PW.Ncels.Services;
using UnitModel = PW.Ncels.Models.UnitModel;

namespace PW.Ncels.Controllers
{
    //[Authorize]
    public class AccountController : ACommonController
    {


        private ncelsEntities _context;

        public AccountController()
        {
            _context = new ncelsEntities();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginRsa(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

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
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _context.Employees.FirstOrDefault(o => o.Login == model.Email);

                if (employee != null)
                {
                    if (Membership.ValidateUser(model.Email, model.Password))
                    {
                        FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                        Session[UserHelper.ConnectKey] = DateTime.Now.Year.ToString();
                        SaveUserName();
                        ActionLogger.WriteExt("Вход в систему: " + model.Email, "Успех");

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
                        var user = Membership.GetUser(model.Email);
                        if (user != null && (user.IsLockedOut || !user.IsApproved))
                        {
                            ModelState.AddModelError("", Messages.AccountController_LogOn_Пользователь_заблокирован_);
                            ActionLogger.WriteExt("Вход в систему: " + model.Email, "Пользователь заблокирован");
                        }
                        else
                        {
                            ModelState.AddModelError("", Messages.AccountController_LogOn_Имя_пользователя_или_пароль_не_верны_);
                            ActionLogger.WriteExt("Вход в систему: " + model.Email, "Неверное имя пользователя или пароль");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", Messages.AccountController_LogOn_Пользователь_заблокирован_);
                    ActionLogger.WriteExt("Вход в систему: "+ model.Email, "Пользователь заблокирован/не найден");
                }

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LoginRsa(LoginViewModel model, string returnUrl)
        {
            //if (ModelState.IsValid) {
            var login = model.Email.Replace("IIN", "");
            Employee employee = _context.Employees.FirstOrDefault(o => o.Login == login);

            if (employee != null)
            {
                MembershipUser membershipUser = Membership.GetUser(login);
                string password = string.Empty;
                if (membershipUser != null)
                    password = membershipUser.GetPassword();
                if (Membership.ValidateUser(login, password))
                {
                    FormsAuthentication.SetAuthCookie(login, model.RememberMe);
                    Session[UserHelper.ConnectKey] = DateTime.Now.Year.ToString();
                    SaveUserName();
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
                    var user = Membership.GetUser(login);
                    if (user != null && (user.IsLockedOut || !user.IsApproved))
                        ModelState.AddModelError("", Messages.AccountController_LogOn_Пользователь_заблокирован_);
                    else
                        ModelState.AddModelError("", Messages.AccountController_LogOn_Имя_пользователя_или_пароль_не_верны_);
                }
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", "Пользователь не найден!");
            }

            //}

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private string GenerateLoginCode(UnitModel request) {
            try {
                LogHelper.Log.Debug("GenerateLoginCode start");
                var source = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                var country = _context.Dictionaries.FirstOrDefault(x => x.Id == request.CountryId.Value);
                if (country == null)
                {
                    LogHelper.Log.Error("Не удалось найти Гражданство в справочнике");
                    throw new Exception("Не удалось найти Гражданство в справочнике");
                }


                var i1 = source.IndexOf(country.Code[0]);
                var i2 = source.IndexOf(country.Code[1]);
                var code = (i1 * source.Length) + i2;
                var codeStr = code.ToString().PadLeft(3, '0');

                var users = Membership.GetAllUsers();
                //LogHelper.Log.DebugFormat("users.Count: {0}", users.Count);
                var mUsers = users.Cast<MembershipUser>();
                //LogHelper.Log.DebugFormat("mUsers.Count: {0}", mUsers.Count());
                var count = mUsers.Count(x => x.CreationDate > DateTime.Today) + 1;
                //LogHelper.Log.DebugFormat("count: {0}", count);
                //var count = _context.Units.Count(x => x.CreatedDate > DateTime.Today) + 1;
                var dd = DateTime.Today.ToString("yyyyMMdd").Substring(2);
                return codeStr + dd + count.ToString().PadLeft(3, '0');
            }
            catch (Exception ex) {
                LogHelper.Log.Error("GenerateLoginCode Exception", ex);
                throw;
            }

        }

        [HttpPost]
        public async Task<JsonResult> PostRequest(UnitModel request, int personType, string password, bool isEcp){
            LogHelper.Log.Debug("PostRequest start");
            string login = null;

            if (isEcp) {
                password = Membership.GeneratePassword(8, 2);
            }

            if (!isEcp && (personType == 2 || personType == 3)) {
                request.Iin = GenerateLoginCode(request);
                LogHelper.Log.DebugFormat("Сгенерирован 12значный код {0} для пользователя {1} {2} {3}", request.Iin, request.LastName, request.FirstName, request.MiddleName);
            }

            login = request.Iin;

            if (_context.Employees.Count(x => x.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase)) > 0) {
                return Json(new MessageModel {
                    IsError = true,
                    Message = "Пользователь с таким емэйлом уже есть!"
                }, JsonRequestBehavior.AllowGet);
            }

            var user = AccountServices.Instance.CreateUser(login, password);

            if (!user.IsError){
                var result = AccountServices.Instance.PostRequest(_context, request, personType);
                if (!result.IsError){

                    var template = EmailService.Instance.GetTemplate(new EmailTemplateModel{
                        Type = 2,
                        HeadMessage = "Регистрация",
                        Title = "Экспертиза лекарственных средств, изделий медицинского назначения и медицинской техники",
                        Message = $"Ваш логин для входа {Environment.NewLine} логин: {login}"
                        // , {Environment.NewLine} пароль: {user.Message}
                    });
                    LogHelper.Log.DebugFormat("Успешно зарегистрирован пользователь с логином {0}, ФИО {1} {2} {3}", request.Iin, request.LastName, request.FirstName, request.MiddleName);

                    await EmailService.Instance.SendEmail(request.Email, "Экспертиза лекарственных средств, изделий медицинского назначения и медицинской техники", template);
                }
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(user, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult RegisterSuccess()
        {
            return View();
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterFl()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register2()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterUl()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register2Ul()
        {
            return View();
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _context.Employees.FirstOrDefault(m => m.Email == model.Email);

                if (employee != null)
                {
                    MembershipUser user = Membership.GetUser(employee.Login);
                    if (user != null)
                    {
                        try
                        {
                            string newPassword = user.ResetPassword();
                            var template = EmailService.Instance.GetTemplate(new EmailTemplateModel()
                            {
                                Type = 0,
                                HeadMessage = "Востоновление пароля",
                                Title = "Экспертиза лекарственных средств, изделий медицинского назначения и медицинской техники",
                                Message =
                             $"Ваши данные для входа {Environment.NewLine} логин: {employee.Login} , {Environment.NewLine} пароль: {newPassword}"
                            });

                            LogHelper.Log.DebugFormat("Восстановление пароля для {0}", employee.Login);

                            await EmailService.Instance.SendEmail(employee.Email, "Экспертиза лекарственных средств, изделий медицинского назначения и медицинской техники", template);
                            return RedirectToAction("ForgotPasswordConfirmation", "Account");
                        }
                        catch (Exception e)
                        {
                            return Json(new MessageModel() { IsError = true, Message = e.Message });
                        }
                    }
                }
            }

            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [Authorize]
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult ResetPassword(ChangePasswordModel model)
        {

            if (ModelState.IsValid)
            {
                Employee employee = UserHelper.GetCurrentEmployee();

                if (employee != null)
                {
                    MembershipUser user = Membership.GetUser(employee.Login);
                    if (user != null)
                    {
                        try
                        {
                            user.ChangePassword(model.OldPassword, model.NewPassword);

                            LogHelper.Log.DebugFormat("Смена пароля для {0}", employee.Login);
                            ActionLogger.WriteExt("Сменил себе пароль");
                            return RedirectToAction("ResetPasswordConfirmation", "Account");
                        }
                        catch (Exception e)
                        {
                            ActionLogger.WriteExt("Пытался сменить себе пароль");
                            return Json(new MessageModel() { IsError = true, Message = e.Message });
                        }
                    }

                }
            }
            ActionLogger.WriteExt("Пытался сменить себе пароль");
            return View(model);
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session[UserHelper.PrismSessionKey] = "";
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult RegisterEcp(int? type, string bin, string orgName, string cn, string surename, string obl, string g, string email)
        {
            if (!string.IsNullOrEmpty(orgName))
            {
                orgName = orgName.Replace("\\", "");
            }
            var user = _context.Employees.FirstOrDefault(o => o.Login == bin);
            if (user != null)
            {
                MembershipUser membershipUser = Membership.GetUser(user.Login);
                string password = string.Empty;
                if (membershipUser != null)
                    password = membershipUser.GetPassword();
                if (Membership.ValidateUser(user.Login, password))
                {
                    LogHelper.Log.DebugFormat("Вход по ЭЦП. Логин: {0}", user.Login);
                    FormsAuthentication.SetAuthCookie(user.Login, false);
                    Session[UserHelper.ConnectKey] = DateTime.Now.Year.ToString();
                    SaveUserName();
                    return RedirectToAction("Index", "Home");
                }
            }

            LogHelper.Log.DebugFormat("Регистрация по ЭЦП. BINIIN: {0}", bin);

            var model = new EcpUserModel();
            if (type != null)
            {
                model.TypePerson = (PersonTypeEnum) type.Value;
            }
            else
            {
                model.TypePerson = PersonTypeEnum.LegalPerson;
            }
            if (email == "null")
            {
                email = null;
            }
            model.Email = email;
            model.BINIIN = bin;
            model.JuridicalName = orgName;

            if (!string.IsNullOrEmpty(cn))
            {
                var names = cn.Split(' ');
                if (names.Length > 1)
                {
                    model.FirstName = names[1];
                }
                if ((string.IsNullOrEmpty(surename) || surename.Equals("null", StringComparison.InvariantCultureIgnoreCase)) && names.Length > 0) {
                    surename = names[0];
                }
            }
            model.LastName = surename;
            model.MiddleName = g;
            return View(model);
        }
        /*   [Authorize]
       [HttpPost]
         public ActionResult RegisterEcp(EcpUserModel model)
         {
             if (ModelState.IsValid)
             {

                     MembershipUser user = Membership.GetUser(model.BINIIN);
                     if (user != null)
                     {
                         try
                         {
                             return RedirectToAction("ResetPasswordConfirmation", "Account");
                         }
                         catch (Exception e)
                         {
                             return Json(new MessageModel() { IsError = true, Message = e.Message });
                         }
                     }

             }
             return View(model);
         }*/

        public ActionResult GetLoginName()
        {
            return Json(User.Identity.Name, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDateTime()
        {
            return Json(DateTime.Now, JsonRequestBehavior.AllowGet);
        }
    }
}