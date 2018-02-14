using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Repository.OBK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Prism.Controllers.OBK
{
    [Authorize]
    public class OBKReportController : Controller
    {
        private readonly OBKReportRepository repository;

        public OBKReportController()
        {
            repository = new OBKReportRepository();
        }

        public ActionResult DeclarationReport()
        {
            return PartialView();
        }

        public ActionResult DeclarationReportList([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.DeclarationReportList();
            return Json(data.ToDataSourceResult(request));
        }
    }
}