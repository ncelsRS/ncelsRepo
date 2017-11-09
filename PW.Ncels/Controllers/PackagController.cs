using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;

namespace PW.Ncels.Controllers {
	[Authorize()]
	public class PackagController : ACommonController
    {
        // GET: Packag
        private ncelsEntities db = UserHelper.GetCn();
        // GET: Organization
        public ActionResult Index() {
            return View();
        }


        public ActionResult GetPackageType(ModelRequest request, Guid id) {
            var items = db.PackagesViews.Where(o => o.ObjectId == id);
            var count = items.Count();
            var data = new {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PackageSave(Package model) {
            var project = db.Packages.Any(o => o.Id == model.Id);
            if (project) {
                db.Entry(model).State = EntityState.Modified;
            }
            else {
                
                db.Packages.Add(model);
            }
            db.SaveChanges();
            return Json(Guid.NewGuid(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PackageDelete(Organization model) {
            var project = db.Packages.First(o => o.Id == model.Id);
            db.Packages.Remove(project);
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }
    }
}