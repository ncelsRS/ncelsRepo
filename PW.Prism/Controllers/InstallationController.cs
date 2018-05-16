using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;

namespace PW.Prism.Controllers
{
    public class InstallationController : Controller
    {
        private ncelsEntities db = new ncelsEntities();

        // GET: Installation
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.Installations.OrderByDescending(m => m.Type).Select(o => new
            {
                o.Id,
                o.Name,
                o.Note,
                o.BuhList,
                o.CheckDate,
                o.Country,
                o.FactoryNumber,
                o.InstallationYear,
                o.InvertoryNumber,
                o.Laboratory,
                o.LaboratoryRoom,
                o.ManufacturerFactory,
                o.Model,
                o.State,
                o.StateIn,
                o.Type,
                o.TypeIn
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

    }
}