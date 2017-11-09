using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Prism.Controllers.PriceProject{

    [Authorize]
    public class PriceProcessingController : Controller{

        private ncelsEntities db = UserHelper.GetCn();

        // GET: Processing
        public ActionResult Index()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult ListPpRpc([DataSourceRequest] DataSourceRequest request, int? type, int? status){
            var prices = db.PP_ProcessingJournal
            .Join(db.Documents, p => p.Id, d => d.Id, (p, d) => new { p, d })
            .Where(m => m.p.Status > 0 && !m.d.IsArchive).Select(m => m.p).AsQueryable();
            if (type != null) {
                prices = prices.Where(x => x.Type == type);
            }
            if (status != null) {
                //новые
                if (status == 1)
                    prices = prices.Where(x => x.Status == 1);
                //в работе
                if (status == 2 )
                    prices = prices.Where(x => x.Status > 1 && x.Status < 5);
                //отказные
                if (status == 3)
                    prices = prices.Where(x => x.Status == 6);
                //завершенные
                if (status == 4)
                    prices = prices.Where(x => x.Status == 5);
            }
            DataSourceResult result = prices.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult Edit(Guid? id){
            if (id == null){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.PriceProjects.FirstOrDefault(x=>x.Id == id);
            return PartialView(model);
        }

        public ActionResult EditRequest(Guid? id){
            if (id == null){
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = db.PriceProjects.FirstOrDefault(x => x.Id == id);
            return PartialView(model);
        }

        

    }
}