using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers {
	[Authorize()]
	public class HomeController : Controller {
		public ActionResult Index() {
			ViewBag.animation = "toggle";
			ViewBag.opacity = false;
			if (Request.Browser.IsMobileDevice)
			{
				return RedirectToAction("Index", "Mobile");
			}
			UserHelper.IsCheckData();
			return View();
		}

		public ActionResult Dashboard() {
			return PartialView();
		}

		public ActionResult BuildCardSuccess()
		{
			return PartialView();
		}
		public ActionResult ExcludeCardSuccess()
		{
			return PartialView();
		}
		public ActionResult RegisterCardSuccess(string number,string documentDate)
		{
			ViewBag.Number = number;
            ViewBag.DocumentDate = documentDate;
			return PartialView();
		}
		public ActionResult RejectCardSuccess()
		{
			return PartialView();
		}
		public ActionResult Review2CardSuccess()
		{
			return PartialView();
		}
		public ActionResult ReviewCardSuccess()
		{
			return PartialView();
		}
		public ActionResult SaveCardSuccess()
		{
			return PartialView();
		}
		public ActionResult TaskActionError()
		{
			return PartialView();
		}
		public ActionResult Error()
		{
			return View();
		}
		public ActionResult GenerationStamp(string id, string fileName)
		{
			string[] items = new string[2];
			items[0] = id;
			items[1] = fileName;
			return PartialView(items);
		}
		public ActionResult SaveCardError() {
			return PartialView();
		}

	    public ActionResult DigSign(Guid documentId)
	    {
	        return PartialView(documentId);
	    }
	}
}