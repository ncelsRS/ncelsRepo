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
	public class OrganizationController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetOrganizationType(ModelRequest request, Guid id, int type) {
            var items = db.OrganizationsViews.Where(o => o.ObjectId == id && o.Type == type && o.OriginalOrgId==null);
            var count = items.Count();

            var data = new {
                draw = request.Draw,
                recordsFiltered = count,
                recordsTotal = count,
                Data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrganizationSave(Organization model) {
            var project = db.Organizations.Any(o => o.Id == model.Id);
            if (project) {
                db.Entry(model).State = EntityState.Modified;
            }
            else {
                model.Id = Guid.NewGuid();
                db.Organizations.Add(model);
            }
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrganizationDelete(Organization model) {
            var project = db.Organizations.First(o => o.Id == model.Id);
            db.Organizations.Remove(project);
            db.SaveChanges();
            return Json("Ок", JsonRequestBehavior.AllowGet);
        }
    }
}