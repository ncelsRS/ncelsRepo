using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;

namespace PW.Prism.Controllers
{
    public class OBK_Ref_LaboratoryRegulationController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        // GET: OBK_Ref_LaboratoryRegulation
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.DictionaryList = DictionaryHelper.GetList();
            return PartialView(guid);
        }

        public ActionResult AllList([DataSourceRequest] DataSourceRequest request)
        {
            var data =
                db.OBK_Ref_LaboratoryRegulation.Where(o => !o.IsDeleted).Select(o => new
                {
                    o.Id,
                    o.NameRu,
                    o.NameKz,
                    o.IsDeleted
                });
            return Json(data.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicCreate([DataSourceRequest] DataSourceRequest request, OBKReferenceLaboratoryRegulationModel dictionary, string type)
        {
            if (dictionary != null)
            {
                OBK_Ref_LaboratoryRegulation d = new OBK_Ref_LaboratoryRegulation()
                {
                    Id = Guid.NewGuid(),
                    NameRu = dictionary.NameRu,
                    NameKz = dictionary.NameKz,
                    IsDeleted = false
                };
                db.OBK_Ref_LaboratoryRegulation.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicUpdate([DataSourceRequest] DataSourceRequest request, OBKReferenceLaboratoryRegulationModel dictionary, string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                var d = db.OBK_Ref_LaboratoryRegulation.First(o => o.Id == dictionary.Id);
                d.NameRu = dictionary.NameRu;
                d.NameKz = dictionary.NameKz;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicDestroy([DataSourceRequest] DataSourceRequest request, OBKReferenceLaboratoryRegulationModel dictionary, string type)
        {
            if (dictionary != null)
            {
                var d = db.OBK_Ref_LaboratoryRegulation.First(o => o.Id == dictionary.Id);
                d.IsDeleted = true;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
    }
}