using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Prism.ViewModels;

namespace PW.Prism.Controllers
{
    public class GridController : Controller
    {
        // GET: Grid
        private readonly ncelsEntities _db = UserHelper.GetCn();

        #region Exp

        public ActionResult GetOrganizationType(Guid id, int type)
        {
            var items = _db.OrganizationsViews.Where(o => o.ObjectId == id && o.Type == type && o.OriginalOrgId == null);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPackageType(Guid id)
        {
            var items = _db.PackagesViews.Where(o => o.ObjectId == id);
            var count = items.Count();
            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetComposition(Guid id)
        {
            var items = _db.CompositionsViews.Where(o => o.ObjectId == id);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChange(Guid id)
        {
            var items = _db.Changes.Where(o => o.RegisterProjectId == id);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Settings(string gridName, Guid employeeId)
        {
            var settings = _db.GridSettings.SingleOrDefault(e => e.EmployeeId == employeeId && e.GridName == gridName);
            return Json(settings != null
                ? SerializeHelper.DeserializeDataContract<GridColumnsSetting>(settings.Model)
                : null, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Settings(string gridName, Guid employeeId, GridColumnsSetting columnModel)
        {
            var settings = _db.GridSettings.SingleOrDefault(e => e.EmployeeId == employeeId && e.GridName == gridName)
                ?? new GridSetting()
                {
                    EmployeeId = employeeId,
                    GridName = gridName
                };
            settings.Model = SerializeHelper.SerializeDataContract(columnModel);
            if (settings.Id == Guid.Empty)
                _db.GridSettings.Add(settings);
            _db.SaveChanges();
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        #endregion Exp

        #region Price

        public ActionResult GetPrice(Guid id)
        {
            var items = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 1);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPriceType2(Guid id)
        {
            var items = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 2);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPriceType3(Guid id)
        {
            var items = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 3);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPriceType4(Guid id)
        {
            var items = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 4);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPriceType5(Guid id)
        {
            var items = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 5);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPriceType6(Guid id)
        {
            var items = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 6);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPriceType7(Guid id)
        {
            var items = _db.PricesViews.Where(o => o.PriceProjectId == id && o.Type == 7);
            var count = items.Count();

            var data = new
            {
                draw = 1,
                recordsFiltered = count,
                recordsTotal = count,
                data = items.ToList()
            };

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion Price
    }
}