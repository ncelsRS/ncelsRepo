using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Repository.OBK;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.IO;
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

        public FileStreamResult ExportDeclarationReport([DataSourceRequest] DataSourceRequest request)
        {
            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/Mrts/OBK/DeclarationReport.mrt"));
            report.Compile();
            var data = repository.DeclarationReportList().ToDataSourceResult(request);
            report.RegBusinessObject("DeclarationList", data.Data);
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;
            return File(stream, "application/excel", "Список Передачи ЗБК.xls");
        }
    }
}