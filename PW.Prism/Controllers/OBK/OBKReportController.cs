using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Constants;
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

        public ActionResult OBKReport(int reportType)
        {
            var model = Guid.NewGuid();

            switch (reportType)
            {
                case OBKReportCodes.DECLARATION_REPORT:
                    ViewData["reportType"] = OBKReportCodes.DECLARATION_REPORT;
                    return PartialView("DeclarationReport", model);
                case OBKReportCodes.ZBK_REPORT:
                    ViewData["reportType"] = OBKReportCodes.ZBK_REPORT;
                    return PartialView("ZBKReport", model);
                case OBKReportCodes.ZBK_COPY_REPORT:
                    ViewData["reportType"] = OBKReportCodes.ZBK_COPY_REPORT;
                    return PartialView("ZBKCopyReport", model);
            }


            return PartialView();
        }

        public ActionResult OBKReportList([DataSourceRequest] DataSourceRequest request, int reportType)
        {
            IQueryable<object> data = null;

            switch (reportType)
            {
                case OBKReportCodes.DECLARATION_REPORT:
                    data = repository.DeclarationReportList();
                    return Json(data.ToDataSourceResult(request));
                case OBKReportCodes.ZBK_REPORT:
                    data = repository.ZBKReportList();
                    return Json(data.ToDataSourceResult(request));
                case OBKReportCodes.ZBK_COPY_REPORT:
                    data = repository.ZBKCopyReportList();
                    return Json(data.ToDataSourceResult(request));

            }
            
            return Json("");
        }

        public FileStreamResult ExportOBKReport([DataSourceRequest] DataSourceRequest request, int reportType)
        {
            StiReport report = new StiReport();
            DataSourceResult data = null;
            switch (reportType)
            {
                case OBKReportCodes.DECLARATION_REPORT:
                    report.Load(Server.MapPath("../Reports/Mrts/OBK/DeclarationReport.mrt"));
                    report.Compile();
                    data = repository.DeclarationReportList().ToDataSourceResult(request);
                    report.RegBusinessObject("DeclarationList", data.Data);
                    break;
                case OBKReportCodes.ZBK_REPORT:
                    report.Load(Server.MapPath("../Reports/Mrts/OBK/ZBKReport.mrt"));
                    report.Compile();
                    data = repository.ZBKReportList().ToDataSourceResult(request);
                    report.RegBusinessObject("ZBKList", data.Data);
                    break;
                case OBKReportCodes.ZBK_COPY_REPORT:
                    report.Load(Server.MapPath("../Reports/Mrts/OBK/ZBKCopyReport.mrt"));
                    report.Compile();
                    data = repository.ZBKCopyReportList().ToDataSourceResult(request);
                    report.RegBusinessObject("ZBKCopyList", data.Data);
                    break;
            }
            
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;
            return File(stream, "application/excel", "Отчет.xls");
        }
    }
}