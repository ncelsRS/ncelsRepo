using System;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers {
	public class HistoryController : Controller {
		private ncelsEntities db = UserHelper.GetCn();

		// GET: /History/
		public ActionResult Index(Guid id) {
			return View(id);
		}

	    public ActionResult HistoryGrid(Guid id)
	    {
            return View(id);
        }
        public JsonResult GetHistory([DataSourceRequest] DataSourceRequest request, Guid? id) {
			var histories = db.Histories.Where(x => x.ObjectId == id).OrderBy(x => x.CreatedTime).ToList();
			DataSourceResult result = histories.ToDataSourceResult(request);
			return Json(result);
		}

        public ActionResult ExtensionExecution(Guid id) {
			return View(id);
		}
        public JsonResult GetExtensionExecution([DataSourceRequest] DataSourceRequest request, Guid? id) {
			var histories = db.ExtensionExecutions.Where(x => x.DocumentId == id).OrderBy(x => x.CreatedDate).ToList();
			DataSourceResult result = histories.ToDataSourceResult(request);
			return Json(result);
		}



        protected override void Dispose(bool disposing) {
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
