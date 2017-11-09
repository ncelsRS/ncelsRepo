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
	public class CompositionController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();
    

        public ActionResult CompositionSave(Composition model) {
            var project = db.Compositions.Any(o => o.Id == model.Id);
            if (project) {
                db.Entry(model).State = EntityState.Modified;
            }
            else {
                model.Id = Guid.NewGuid();
                db.Compositions.Add(model);
            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }
        public ActionResult CompositionDelete(Composition model) {
            var project = db.Compositions.First(o => o.Id == model.Id);
            db.Compositions.Remove(project);
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetComposition(ModelRequest request, Guid id) {
            var items = db.CompositionsViews.Where(o=>o.ObjectId ==id);
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