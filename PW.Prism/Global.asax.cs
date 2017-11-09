using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
//using Aspose.Pdf;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Ncels.Helpers;
using Ncels.Scheduler;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Prism.Global.Binders;
using PW.Prism.Properties;
using Stimulsoft.Base.Localization;

namespace PW.Prism {
	public class MvcApplication : System.Web.HttpApplication {
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			ModelBinders.Binders.Add(typeof(DateTime), new CustomDateBinder());
			ModelBinders.Binders.Add(typeof(DateTime?), new NullableCustomDateBinder());

			Stream stream = new MemoryStream(StrToByteArray(Resources.Licence));
			stream.Position = 0;
			Aspose.Pdf.License licensePdf = new Aspose.Pdf.License();
			Aspose.Words.License licenseWords = new Aspose.Words.License();
			Aspose.Cells.License licenseCells = new Aspose.Cells.License();
			Aspose.Slides.License licenseSlides = new Aspose.Slides.License();
            Aspose.BarCode.License licenseBarCode=new Aspose.BarCode.License();

			licensePdf.SetLicense(stream);
			stream.Position = 0;
			licenseWords.SetLicense(stream);
			stream.Position = 0;
			licenseCells.SetLicense(stream);
			stream.Position = 0;
			licenseSlides.SetLicense(stream);
		    stream.Position = 0;
		    licenseBarCode.SetLicense(stream);
            stream.Close();
            //   CreateLogins();

            //	CheckLogins();

            LogHelper.InitLogger();
            LogHelper.Log.Info("Application Start");

            EmployePermissionHelper.Init();


            QScheduler.Start();
		}
		public static byte[] StrToByteArray(string str) {
			return Convert.FromBase64String(str);
		}

		private void CheckLogins() {
			ncelsEntities db = new ncelsEntities();
			IQueryable<string> logins = db.Employees.Select(x => x.Login);
			foreach (string login in logins)
				if (login != null && Membership.GetUser(login) == null)
					Membership.CreateUser(login, "123456");
				
		}

        private void CreateLogins() {
            ncelsEntities db = new ncelsEntities();
            var employees = db.Employees.Where(x => x.Login == null).ToList();
            foreach (var employee in employees) {
                if (!employee.LastName.IsNullOrWhiteSpace() && !employee.FirstName.IsNullOrWhiteSpace()) {
                    string login = string.Format("{0}.{1}", employee.LastName, employee.FirstName.Substring(0, 1));

                    string loginLatin = Transliterate(login.ToLower());
                    if (!db.Employees.Any(m => m.Login == loginLatin)) {
                        employee.Login = loginLatin;
                    }
                    else {
                        login = string.Format("{0}.{1}", employee.LastName, employee.FirstName);
                        loginLatin = Transliterate(login.ToLower());
                        if (!db.Employees.Any(m => m.Login == loginLatin)) {
                            employee.Login = loginLatin;
                        }
                        else {
                            if (!employee.MiddleName.IsNullOrWhiteSpace()) {
                                login = string.Format("{0}.{1}{2}", employee.LastName, employee.FirstName.Substring(0, 1), employee.FirstName.Substring(0, 1));
                                loginLatin = Transliterate(login.ToLower());
                                employee.Login = loginLatin;
                            }
                        }
                    }

                }
                db.SaveChanges();
            }
        }

        private string Transliterate(string str) {
            string[] lat_low = { "a", "b", "v", "g", "d", "e", "yo", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "sh", "", "y", "", "e", "yu", "ya" };
            string[] rus_low = { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            for (int i = 0; i <= 32; i++) {
                str = str.Replace(rus_low[i], lat_low[i]);
            }
            return str;
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e) {
			CheckCookieAndSetCulture();
			
		}

		
		private void CheckCookieAndSetCulture() {
			try {
				UserHelper.IsCheckData();
				HttpCookie cookie = null;
	
				if (!String.IsNullOrEmpty(Request.QueryString["lang"])) {
					cookie = new HttpCookie("lang", Request.QueryString["lang"]);
					cookie.Expires = DateTime.Now.AddYears(1);
					Response.SetCookie(cookie);
				}

				if (Request.Cookies["lang"] != null | cookie != null) {
					if (cookie == null)
						cookie = Request.Cookies["lang"];

					CultureInfo culture = CultureInfo.CreateSpecificCulture(cookie.Value);
				    CultureInfo.DefaultThreadCurrentCulture = culture;
				    CultureInfo.DefaultThreadCurrentUICulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
					Thread.CurrentThread.CurrentCulture = culture;
                }
				if (!String.IsNullOrEmpty(Request.QueryString["archiv"]))
				{
					cookie = new HttpCookie("archiv", Request.QueryString["archiv"]);
					cookie.Expires = DateTime.Now.AddYears(1);
					Response.SetCookie(cookie);
				}
				if (Request.Cookies["archiv"] != null)
				{
					
					cookie = Request.Cookies["archiv"];
					Session[UserHelper.ConnectKey] = cookie.Value;
				}
				else
				{
					Session[UserHelper.ConnectKey] = null;
				}
			} catch {

			}

		}
	}
}
