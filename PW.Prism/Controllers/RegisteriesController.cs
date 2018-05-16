using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;

namespace PW.Prism.Controllers
{
    public class RegisteriesController : Controller {
        public ActionResult Index() {
            //ViewBag.TypeDoc = type;
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }
        public ActionResult Index1() {
            //ViewBag.TypeDoc = type;
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }
		public ActionResult Index8() {
			//ViewBag.TypeDoc = type;
			Guid guid = Guid.NewGuid();
			//ViewBag.ResponsibleValue =  db.Documents.
			//	Where(o => o.IsDeleted == false && o.DocumentType == 0).
			//	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
			//	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
			//var documents = db.Documents.Include(d => d.Template);
			return PartialView(guid);
		}

		public ActionResult Index2() {
            //ViewBag.TypeDoc = type;
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }
        public ActionResult Index3() {
            //ViewBag.TypeDoc = type;
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }
        public ActionResult Index4() {
            //ViewBag.TypeDoc = type;
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }

        public ActionResult Index5() {
            //ViewBag.TypeDoc = type;
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }
        public ActionResult Index6() {
            //ViewBag.TypeDoc = type;
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }

        public ActionResult Index7()
        {
            //ViewBag.TypeDoc = type;
            Guid guid = Guid.NewGuid();
            //ViewBag.ResponsibleValue =  db.Documents.
            //	Where(o => o.IsDeleted == false && o.DocumentType == 0).
            //	GroupBy(o => new { o.ResponsibleId, o.ResponsibleValue }).
            //	Select(o => new SelectListItem() { Text = o.Key.ResponsibleValue }).ToList();
            //var documents = db.Documents.Include(d => d.Template);
            return PartialView(guid);
        }
        private ncelsEntities db = new ncelsEntities();

        public ActionResult ObkRead([DataSourceRequest] DataSourceRequest request) {
            var data = db.ReesrtObks.ToList().OrderBy(o => o.name);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult RozRead([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.Ref_MarketPrices.Where(x=>x.Type == 0).ToList().OrderBy(o => o.Name);
            return Json(data.ToDataSourceResult(request));
        }
		public ActionResult OptRead([DataSourceRequest] DataSourceRequest request) {
			var data = db.Ref_MarketPrices.Where(x => x.Type == 0).ToList().OrderBy(o => o.Name);
			return Json(data.ToDataSourceResult(request));
		}


		public ActionResult ZakupRead([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.Ref_PurchasePrices.ToList().OrderBy(o => o.Name);
            return Json(data.ToDataSourceResult(request));
        }

		public ActionResult LimitsRead([DataSourceRequest] DataSourceRequest request) {
			var data = db.Ref_Limits.ToList().OrderBy(o => o.Id);
			return Json(data.ToDataSourceResult(request));
		}

		public ActionResult RegisterRead([DataSourceRequest] DataSourceRequest request, int type) {
			Guid organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
			var data = db.Registeries
				.Where(o => o.Type == type).Where(o => o.OrganizationId == organizationId)
				.OrderBy(o => o.Name);
			return Json(data.ToDataSourceResult(request));
		}

		[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterCreate([DataSourceRequest] DataSourceRequest request, Registery dictionary, int type) {
            if (dictionary != null)
            {

                dictionary.Type = type;
                dictionary.OrganizationId = UserHelper.GetCurrentEmployee().OrganizationId;
               
                db.Registeries.Add(dictionary);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterUpdate([DataSourceRequest] DataSourceRequest request, Registery dictionary, int type) {
            if (dictionary != null && ModelState.IsValid) {
                Registery d = db.Registeries.First(o => o.Id == dictionary.Id);
                d.Name = dictionary.Name;
                d.Code = dictionary.Code;
                d.Cost = dictionary.Cost;
                d.Count = dictionary.Count;
                d.Country = dictionary.Country;
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterDestroy([DataSourceRequest] DataSourceRequest request, Registery dictionary) {
            if (dictionary != null) {
                Dictionary d = db.Dictionaries.First(o => o.Id == dictionary.Id);
                db.Dictionaries.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
