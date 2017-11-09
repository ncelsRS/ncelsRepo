using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Antlr.Runtime;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class OBK_Ref_PriceListController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();

        // GET: /Reference/
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.DictionaryList = DictionaryHelper.GetList();
            return PartialView(guid);
        }

        public ActionResult AllList([DataSourceRequest] DataSourceRequest request)
        {
            var data =
                db.OBK_Ref_PriceList.OrderBy(o => o.NameRu)
                .Include(o => o.OBK_Ref_Type)
                .Include(o => o.OBK_Ref_ServiceType)
                .Include(o => o.OBK_Ref_DegreeRisk)
                .Include(o => o.Dictionary)
                .Select(o => new
                {
                    Id = o.Id,
                    NameRu = o.NameRu,
                    NameKz = o.NameKz,
                    Type = o.OBK_Ref_Type.NameRu,
                    TypeId = o.TypeId,
                    Unit = o.Dictionary.Name,
                    UnitId = o.UnitId,
                    Price = o.Price,
                    ServiceType = o.OBK_Ref_ServiceType.NameRu,
                    ServiceTypeId = o.ServiceTypeId,
                    Degree = o.OBK_Ref_DegreeRisk.NameRu,
                    DegreeRiskId = o.DegreeRiskId
                });
            return Json(data.ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicCreate([DataSourceRequest] DataSourceRequest request, OBKReferencePriceListModel dictionary,
            string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {

                OBK_Ref_PriceList d = new OBK_Ref_PriceList()
                {
                    Id = Guid.NewGuid(),
                    NameRu = dictionary.NameRu,
                    NameKz = dictionary.NameKz,
                    TypeId = dictionary.TypeId,
                    UnitId = dictionary.UnitId,
                    Price = dictionary.Price,
                    ServiceTypeId = dictionary.ServiceTypeId,
                    DegreeRiskId = dictionary.DegreeRiskId
                };
                db.OBK_Ref_PriceList.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicUpdate([DataSourceRequest] DataSourceRequest request, OBKReferencePriceListModel dictionary,
            string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                OBK_Ref_PriceList d = db.OBK_Ref_PriceList.First(o => o.Id == dictionary.Id);
                d.NameRu = dictionary.NameRu;
                d.NameKz = dictionary.NameKz;
                d.TypeId = dictionary.TypeId;
                d.UnitId = dictionary.UnitId;
                d.Price = dictionary.Price;
                d.ServiceTypeId = dictionary.ServiceTypeId;
                d.DegreeRiskId = dictionary.DegreeRiskId;

                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicDestroy([DataSourceRequest] DataSourceRequest request, OBKReferencePriceListModel dictionary,
            string type)
        {
            if (dictionary != null)
            {
                OBK_Ref_PriceList d = db.OBK_Ref_PriceList.First(o => o.Id == dictionary.Id);
                db.OBK_Ref_PriceList.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult TypeList()
        {
            var data = db.OBK_Ref_Type.Where(e => e.IsDeleted == false).Select(o => new { Id = o.Id, Name = o.NameRu }).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnitList()
        {
            var data = db.Dictionaries.Select(o => new { Id = o.Id, Name = o.Name }).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ServiceTypeList()
        {
            var data = db.OBK_Ref_ServiceType.Select(o => new { Id = o.Id, Name = o.NameRu }).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DegreeRiskList()
        {
            var data = db.OBK_Ref_DegreeRisk.Select(o => new { Id = o.iD, Name = o.NameRu }).OrderBy(x => x.Name).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
