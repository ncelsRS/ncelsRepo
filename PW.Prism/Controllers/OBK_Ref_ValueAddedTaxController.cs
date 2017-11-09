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
    public class OBK_Ref_ValueAddedTaxController : Controller
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
                db.OBK_Ref_ValueAddedTax.OrderBy(o => o.Value).Select(o => new
                {
                    Id = o.Id,
                    Value = o.Value,
                    Year = o.Year,
                });
            return Json(data.ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicCreate([DataSourceRequest] DataSourceRequest request, OBKReferenceValAdTaxModel dictionary,
            string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {

                OBK_Ref_ValueAddedTax d = new OBK_Ref_ValueAddedTax()
                {
                    Id = Guid.NewGuid(),
                    Year = dictionary.Year,
                    Value = dictionary.Value,
                };
                db.OBK_Ref_ValueAddedTax.Add(d);
                db.SaveChanges();
                dictionary.Id = d.Id;
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicUpdate([DataSourceRequest] DataSourceRequest request, OBKReferenceValAdTaxModel dictionary,
            string type)
        {
            if (dictionary != null && ModelState.IsValid)
            {
                OBK_Ref_ValueAddedTax d = db.OBK_Ref_ValueAddedTax.First(o => o.Id == dictionary.Id);
                d.Value = dictionary.Value;
                d.Year = dictionary.Year;
                
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DicDestroy([DataSourceRequest] DataSourceRequest request, OBKReferenceValAdTaxModel dictionary,
            string type)
        {
            if (dictionary != null)
            {
                OBK_Ref_ValueAddedTax d = db.OBK_Ref_ValueAddedTax.First(o => o.Id == dictionary.Id);
                db.OBK_Ref_ValueAddedTax.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

    }
}

