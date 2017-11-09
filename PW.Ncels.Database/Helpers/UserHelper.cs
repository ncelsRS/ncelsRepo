using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Ncels.Helpers;
using PW.Ncels.Database.DataModel;
using Stimulsoft.Report;

namespace PW.Ncels.Database.Helpers
{
	public class UserHelper
	{
		public const string PrismSessionKey = "1B3125F5-EF65-4CE0-85C9-2C78E2E579A5";

        /// <summary>
        /// может вернуть null, будь бдителен
        /// </summary>
        /// <returns></returns>
		public static Employee GetCurrentEmployee() {
			ncelsEntities entities = new ncelsEntities();
            try {
                //return entities.Employees.Include("Position").First();
                return entities.Employees.Include("Position").
                    Where(x => x.Login == HttpContext.Current.User.Identity.Name).
                    FirstOrDefault();
            }
            catch (Exception ex) {
                LogHelper.Log.Error("GetCurrentEmployee Exception", ex);
                return null;
            }

		}

		public static Unit GetDepartment() {
			ncelsEntities entities = new ncelsEntities();
			//return entities.Employees.Include("Position").First();
			return entities.Employees.Include("Position.Parent.Parent").
				Where(x => x.Login == HttpContext.Current.User.Identity.Name).
				FirstOrDefault().Position.Parent;
		}

        public static Unit GetCompany()
        {
            ncelsEntities entities = new ncelsEntities();
            //return entities.Employees.Include("Position").First();
            return entities.Employees.Include("Position.Parent.Parent").
                Where(x => x.Login == HttpContext.Current.User.Identity.Name).
                FirstOrDefault().Position.Parent.Parent;
        }

        public static string GetCurrentName()
		{
			ncelsEntities entities = new ncelsEntities();
			var user = entities.Employees.Include("Position").
				Where(x => x.Login == HttpContext.Current.User.Identity.Name).FirstOrDefault();
			return user != null ? user.DisplayName : "Не найден";
		}

		public static string GetCurrentShortName() {
			ncelsEntities entities = new ncelsEntities();
			var user = entities.Employees.Include("Position").
				Where(x => x.Login == HttpContext.Current.User.Identity.Name).FirstOrDefault();
			return user != null ? user.ShortName : "Не найден";
		}

		public static IEnumerable<Guid> GetDeputy(string currentEmployeeId) {
			ncelsEntities entities = new ncelsEntities();
			return
				entities.Employees.Where(
					x => x.DeputyId != null && x.DeputyId.Contains(currentEmployeeId) && x.Position != null).
					Select(employee => employee.Id).ToList();
		}

		public static IEnumerable<Guid> GetAssistants(string currentEmployeeId) {
			ncelsEntities entities = new ncelsEntities();
			return
				entities.Employees.Where(
					x => x.AssistantsId != null && x.AssistantsId.Contains(currentEmployeeId) && x.Position != null).
					Select(employee => employee.Id).ToList();
		}

		public const string ConnectKey = "ConnectKey";
		public static ncelsEntities GetCn()
		{
			ncelsEntities entities = new ncelsEntities();
			if (HttpContext.Current==null || HttpContext.Current.Session == null)
				return entities;
			if (HttpContext.Current.Session[ConnectKey] == null)
			{
				ncelsEntities cn = new ncelsEntities();
				return cn;
			}
			else
			{
				string key = HttpContext.Current.Session[ConnectKey].ToString();
				var connet = entities.Settings.FirstOrDefault(o => o.UniqueName == key);
				if (connet == null)
					return new ncelsEntities();
				return new ncelsEntities(connet.Value);
			}

		
		}

		public static void IsCheckData()
		{
			//if (DateTime.Now > new DateTime(2016,2,1))
			//if (!HttpContext.Current.Request.Url.AbsolutePath.Contains("/Home/Error"))
			//{

			//	HttpContext.Current.Response.Redirect("~/Home/Error");
			//}
		}

		public static string GetCnString() {
			ncelsEntities entities = new ncelsEntities();
			if (HttpContext.Current.Session == null)
				return System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
			if (HttpContext.Current.Session[ConnectKey] == null) {

				return System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
			} else {
				string key = HttpContext.Current.Session[ConnectKey].ToString();
				var connet = entities.Settings.FirstOrDefault(o => o.UniqueName == key);
				if (connet == null)
					return System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
				return connet.Value;
			}
		}

	    public static bool CheckGuide(string id) {
	        using (var entities = new ncelsEntities()) {
	            var employees = entities.Employees.Where(m=>m.IsGuide).Select(m=>m.Id).ToList();
	            return employees.Any(item => id.Contains(item.ToString()));
	        }
	    }
	}
}