using System.Threading.Tasks;
using System.Web.Mvc;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers {
	public class DashboardController : Controller {

		private readonly DashboardService _dashboardService = new DashboardService();

		public async Task<JsonResult> BuildHtml(string name) {
			return Json(await _dashboardService.GetHtml(HttpContext.Server.MapPath("/Widgets/" + name)), JsonRequestBehavior.AllowGet);
		}


		public async Task<JsonResult> BuildImage(string name) {
			return Json(await _dashboardService.GetImage(HttpContext.Server.MapPath("/Widgets/" + name)), JsonRequestBehavior.AllowGet);
		}

		public async Task<JsonResult> BuildHtmlParams(string name, params string[] param) {
			return Json(await _dashboardService.GetHtmlParams(HttpContext.Server.MapPath("/Widgets/" + name), param), JsonRequestBehavior.AllowGet);
		}
	}
}