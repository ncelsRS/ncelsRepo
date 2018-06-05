using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;

namespace PW.Prism.Controllers
{
    public class OBK_Ref_LaboratoryMarkController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        // GET: OBK_LaboratoryMark
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.DictionaryList = DictionaryHelper.GetList();
            return PartialView(guid);
        }

        public ActionResult AllList([DataSourceRequest] DataSourceRequest request)
        {
            var data =
                db.OBK_Ref_LaboratoryMark.Where(o => !o.IsDeleted).Select(o => new
                {
                    o.Id,
                    o.NameRu,
                    o.NameKz,
                    o.IsDeleted,
                    RegulationList = o.OBK_Ref_LaboratoryRegulation_Mark.Select(x=> new
                    {
                        x.OBK_Ref_LaboratoryRegulation.Id,
                        x.OBK_Ref_LaboratoryRegulation.NameRu,
                        x.OBK_Ref_LaboratoryRegulation.NameKz
                    })
                });

            return Json(data.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicCreate([DataSourceRequest] DataSourceRequest request, OBKReferenceLaboratoryMarkModel dictionary, string type)
         {
            if (dictionary != null)
            {
                OBK_Ref_LaboratoryMark d = new OBK_Ref_LaboratoryMark()
                {
                    Id = Guid.NewGuid(),
                    NameRu = dictionary.NameRu,
                    NameKz = dictionary.NameKz,
                    IsDeleted = false,
                };
                db.OBK_Ref_LaboratoryMark.Add(d);
                dictionary.Id = d.Id;


                foreach (var q in dictionary.RegulationList)
                {
                    OBK_Ref_LaboratoryRegulation_Mark regulationMark = new OBK_Ref_LaboratoryRegulation_Mark();

                    regulationMark.Id = Guid.NewGuid();
                    regulationMark.laboratoryMark_id = d.Id;
                    regulationMark.laboratoryRegulation_id = q.Id;
                    db.OBK_Ref_LaboratoryRegulation_Mark.Add(regulationMark);

                }

                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicUpdate([DataSourceRequest] DataSourceRequest request, OBKReferenceLaboratoryMarkModel dictionary, string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                var d = db.OBK_Ref_LaboratoryMark.First(o => o.Id == dictionary.Id);
                d.NameRu = dictionary.NameRu;
                d.NameKz = dictionary.NameKz;

                var ex = db.OBK_Ref_LaboratoryRegulation_Mark.RemoveRange(d.OBK_Ref_LaboratoryRegulation_Mark);
                db.SaveChanges();

                if (ex.Count() != 0)
                {
                    foreach (var q in dictionary.RegulationList)
                    {
                        OBK_Ref_LaboratoryRegulation_Mark regulationMark = new OBK_Ref_LaboratoryRegulation_Mark();

                        regulationMark.Id = Guid.NewGuid();
                        regulationMark.laboratoryMark_id = d.Id;
                        regulationMark.laboratoryRegulation_id = q.Id;
                        db.OBK_Ref_LaboratoryRegulation_Mark.Add(regulationMark);

                    }
                }

                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicDestroy([DataSourceRequest] DataSourceRequest request, OBKReferenceLaboratoryMarkModel dictionary, string type)
        {
            if (dictionary != null)
            {
                var d = db.OBK_Ref_LaboratoryMark.First(o => o.Id == dictionary.Id);
                d.IsDeleted = true;
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
    }
}