using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using PW.Ncels.Database.DataModel;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Task = System.Threading.Tasks.Task;

namespace PW.Ncels.Database.Helpers {
	public class DashboardService {

		private static string _connectionString;
		private static string _currentEmployeeId;
		private static string _currentDepartmentId;
		
		private const string IdGercen = "10BC6297-D086-4F99-8F10-94DF6D160067";
		private const string IdAytuev = "4FFCCBE5-B5A9-4D88-BBDB-BDF0D9650EE0";
		private const string IdLevin = "D220C755-05AE-489A-B6B5-8B48550CB3B2";

		public DashboardService() {
		    if (UserHelper.GetCurrentEmployee() != null) {
                _currentEmployeeId = UserHelper.GetCurrentEmployee().Id.ToString();
                _currentDepartmentId = UserHelper.GetCurrentEmployee().Position.ParentId.ToString();
            }
			_connectionString = GetCnString();
		}

		public async Task<string> GetHtml(string name) {
			return await Task.Run(() => ConvertToHtml(name));
		}
		public async Task<string> GetHtmlParams(string name,params string[] param) {
			return await Task.Run(() => ConvertToHtml(name, param));
		}
		public async Task<string> GetImage(string name) {
			return await Task.Run(() => ConvertToImage(name));
		}

		private static string ConvertToHtml(string name) {
			StiReport report = new StiReport();
			report.Load(name);
			if (report.Dictionary.Variables.Contains("EmployeeId"))
				report.Dictionary.Variables["EmployeeId"].Value = _currentEmployeeId;
			if (report.Dictionary.Variables.Contains("DepId"))
				report.Dictionary.Variables["DepId"].Value = _currentDepartmentId;
			if (report.Dictionary.Variables.Contains("StateType"))
				report.Dictionary.Variables["StateType"].Value = "Work";
			foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
				data.ConnectionString = _connectionString;

		    if (CultureInfo.DefaultThreadCurrentCulture != null)
		        Thread.CurrentThread.CurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
            if (CultureInfo.DefaultThreadCurrentUICulture != null)
                Thread.CurrentThread.CurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
            report.Render(false);

			MemoryStream stream = new MemoryStream();
			report.ExportDocument(StiExportFormat.HtmlTable, stream);
			stream.Position = 0;
			var sr = new StreamReader(stream);
			var str = sr.ReadToEnd().Replace("Stimulsoft Reports - Demo Version", string.Empty);
			return str;
		}

		private static string ConvertToImage(string name) {
			StiReport report = new StiReport();
			report.Load(name);
			if (report.Dictionary.Variables.Contains("EmployeeId"))
				report.Dictionary.Variables["EmployeeId"].Value = _currentEmployeeId;
			foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
				data.ConnectionString = _connectionString;

            Thread.CurrentThread.CurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
            report.Render(false);
			
			MemoryStream stream = new MemoryStream();
			
			report.ExportDocument(StiExportFormat.ImagePng, stream);
			stream.Position = 0;
			var str = Convert.ToBase64String(stream.GetBuffer());
			return str;
		}

		private static string ConvertToHtml(string name, params  string[]  param) {
			StiReport report = new StiReport();
			report.Load(name);
			
			int i = 0;
            if (report.Dictionary.Variables.Contains("EmployeeId"))
                report.Dictionary.Variables["EmployeeId"].Value = _currentEmployeeId;
            foreach (var item in param.Where(x => !string.IsNullOrEmpty(x) && x != "undefined"))
				report.Dictionary.Variables[i++].Value = item;
			foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
				data.ConnectionString = _connectionString;

            Thread.CurrentThread.CurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
            report.Render(false);

			MemoryStream stream = new MemoryStream();
			report.ExportDocument(StiExportFormat.HtmlTable, stream);
			stream.Position = 0;
			var sr = new StreamReader(stream);
			var str = sr.ReadToEnd().Replace("Stimulsoft Reports - Demo Version", string.Empty);
			return str;
		}

		private string GetCnString() {
			ncelsEntities entities = new ncelsEntities();
			if (HttpContext.Current.Session == null)
				return ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
			if (HttpContext.Current.Session[UserHelper.ConnectKey] == null) {

				return System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
			} else {
				string key = HttpContext.Current.Session[UserHelper.ConnectKey].ToString();
				var connet = entities.Settings.FirstOrDefault(o => o.UniqueName == key);
				if (connet == null)
					return System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
				return connet.Value;
			}


		}
	}
}