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
			NcelsEntities entities = new NcelsEntities();
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
			NcelsEntities entities = new NcelsEntities();
			//return entities.Employees.Include("Position").First();
			return entities.Employees.Include("Position.Parent.Parent").
				Where(x => x.Login == HttpContext.Current.User.Identity.Name).
				FirstOrDefault().Position.Parent;
		}

        public static Unit GetDepartmentUpper()
        {
            NcelsEntities entities = new NcelsEntities();
            //return entities.Employees.Include("Position").First();
            var unit1 = entities.Employees.Include("Position.Parent.Parent").
                Where(x => x.Login == HttpContext.Current.User.Identity.Name).
                FirstOrDefault().Position.Parent;
            if (unit1 != null)
                unit1 = entities.Employees.Include("Position.Parent.Parent").
                    Where(x => x.Login == HttpContext.Current.User.Identity.Name).
                    FirstOrDefault().Position.Parent.Parent;

            return unit1;
        }

        // [Todo] нужно доделать обход по дереву оргструктуры
        public static bool IsUserInDepartmentByCode(string departmentCode)
        {
            NcelsEntities entities = new NcelsEntities();
            var lookup = entities.Units //.Where(un => un.Employee.Login == HttpContext.Current.User.Identity.Name)
                .ToLookup(x => x.ParentId);
            return lookup[null].SelectRecursive(x => lookup[x.ParentId]).Any(un => un.Code == departmentCode);
        }

        public static Unit GetCompany()
        {
            NcelsEntities entities = new NcelsEntities();
            //return entities.Employees.Include("Position").First();
            return entities.Employees.Include("Position.Parent.Parent").
                Where(x => x.Login == HttpContext.Current.User.Identity.Name).
                FirstOrDefault().Position.Parent.Parent;
        }

        public static string GetCurrentName() {
            if (HttpContext.Current.User == null)
                return "Не найден";

            NcelsEntities entities = new NcelsEntities();
			var user = entities.Employees.Include("Position").
				Where(x => x.Login == HttpContext.Current.User.Identity.Name).FirstOrDefault();
			return user != null ? user.DisplayName : "Не найден";
		}

		public static string GetCurrentShortName() {
			NcelsEntities entities = new NcelsEntities();
			var user = entities.Employees.Include("Position").
				Where(x => x.Login == HttpContext.Current.User.Identity.Name).FirstOrDefault();
			return user != null ? user.ShortName : "Не найден";
		}

		public static IEnumerable<Guid> GetDeputy(string currentEmployeeId) {
			NcelsEntities entities = new NcelsEntities();
			return
				entities.Employees.Where(
					x => x.DeputyId != null && x.DeputyId.Contains(currentEmployeeId) && x.Position != null).
					Select(employee => employee.Id).ToList();
		}

		public static IEnumerable<Guid> GetAssistants(string currentEmployeeId) {
			NcelsEntities entities = new NcelsEntities();
			return
				entities.Employees.Where(
					x => x.AssistantsId != null && x.AssistantsId.Contains(currentEmployeeId) && x.Position != null).
					Select(employee => employee.Id).ToList();
		}

		public const string ConnectKey = "ConnectKey";
		public static NcelsEntities GetCn()
		{
			NcelsEntities entities = new NcelsEntities();
			if (HttpContext.Current==null || HttpContext.Current.Session == null)
				return entities;
			if (HttpContext.Current.Session[ConnectKey] == null)
			{
				NcelsEntities cn = new NcelsEntities();
				return cn;
			}
			else
			{
				string key = HttpContext.Current.Session[ConnectKey].ToString();
				var connet = entities.Settings.FirstOrDefault(o => o.UniqueName == key);
				if (connet == null)
					return new NcelsEntities();
                return new NcelsEntities(connet.Value);
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
			NcelsEntities entities = new NcelsEntities();
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
	        using (var entities = new NcelsEntities()) {
	            var employees = entities.Employees.Where(m=>m.IsGuide).Select(m=>m.Id).ToList();
	            return employees.Any(item => id.Contains(item.ToString()));
	        }
	    }
        
    }
}