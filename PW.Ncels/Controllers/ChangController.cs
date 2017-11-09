using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;

namespace PW.Ncels.Controllers
{
	[Authorize()]
    public class ChangController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();
        public ActionResult Index() {
            return View();
        }

        public ActionResult ChangeSave(Change model) {
            var project = db.Changes.Any(o => o.Id == model.Id);
            if (project) {
                db.Entry(model).State = EntityState.Modified;
            }
            else {
                model.Id = Guid.NewGuid();
                db.Changes.Add(model);
            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangeDelete(Change model) {
            var project = db.Changes.First(o => o.Id == model.Id);
            db.Changes.Remove(project);
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChange(ModelRequest request, Guid id) {
            var items = db.Changes.Where(o => o.RegisterProjectId == id );
            var count = items.Count();

            var data = new {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}