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
    public class StateRegeditController : Controller
    {
        private ncelsEntities db = new ncelsEntities();

        /// Сначала я думал поиск производителя, это поиск производителя, а оказалось это поиск зарегистрированных ЛС и тд.. если не пригодится удалить
        public ActionResult ProducerList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView("ProducerList", guid);
        }

        public ActionResult ReadProducer([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.SrProducerViews.OrderByDescending(m => m.Id).Select(o => new
            {
                o.Id,
                o.Name,
                o.NameEng,
                o.NameKz,
                o.FormTypeName,
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReestrList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView("ReestrList", guid);
        }

        public ActionResult ReadReestr([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.SrReestrViews.OrderByDescending(m => m.reg_number).Select(o => new
            {
                o.reg_number,
                o.type_name,
                o.reg_action_name,
                o.name,
                o.name_kz,
                o.reg_date,
                o.C_producer_name,
                o.C_producer_name_en,
                o.C_producer_name_kz,
                o.C_country_name,
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}