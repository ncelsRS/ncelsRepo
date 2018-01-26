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
using PW.Prism.ViewModels.OBK;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.Database.Notifications;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.IO;
using System.Data.SqlTypes;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class BlankAccountingController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();
        private BlankAccountingRepository repository = new BlankAccountingRepository();

        // GET: /Reference/
        public ActionResult Index()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.List();

            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult Prediction([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.List();
            var total = data.ToDataSourceResult(request).Total;
            var corrupted = data.Where(o => o.Corrupted == true).ToDataSourceResult(request).Total;
            var fresh = data.Where(o => o.Corrupted != true).ToDataSourceResult(request).Total;

            return Json(new
            {
                success = true,
                result = new
                {
                    total = total,
                    corrupted = corrupted,
                    fresh = fresh
                }
            });
        }

        public ActionResult Decommission(List<Guid> list)
        {
            repository.Decommission(list);
            return Json(new { success = true });
        }

        public ActionResult CorruptedBlanks()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult CorruptedUserBlanks([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.CorruptedUserBlanks();
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult SaveCorrupted(int number, string date)
        {
            var response = repository.SaveCorrupted(number, DateTime.Parse(date));
            return Json(new { success = response.response, message = response.message });
        }

        public FileStreamResult ExportFile([DataSourceRequest] DataSourceRequest request)
        {
            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/Mrts/OBK/ZBKBlanks.mrt"));
            report.Compile();
            var data = repository.List().ToDataSourceResult(request);
            report.RegBusinessObject("List", data.Data);
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;
            return File(stream, "application/excel", "Отчет по бланкам.xls");
        }

    }
}
