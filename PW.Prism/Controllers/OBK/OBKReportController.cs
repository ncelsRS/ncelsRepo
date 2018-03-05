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
                case OBKReportCodes.DIRECTION_PAYMENTS_REPORT:
                    ViewData["reportType"] = OBKReportCodes.DIRECTION_PAYMENTS_REPORT;
                    return PartialView("DirectionToPaymentsReport", model);
                case OBKReportCodes.ZBK_DETAIL_REPORT:
                    ViewData["reportType"] = OBKReportCodes.ZBK_DETAIL_REPORT;
                    return PartialView("ZBKDetailReport", model);
                case OBKReportCodes.ZBK_DEFECTIVE_PRODUCT:
                    ViewData["reportType"] = OBKReportCodes.ZBK_DEFECTIVE_PRODUCT;
                    return PartialView("ZBKDefectiveProductReport", model);
                case OBKReportCodes.GMP_REPORT:
                    ViewData["reportType"] = OBKReportCodes.GMP_REPORT;
                    return PartialView("GMPReport", model);
                case OBKReportCodes.SUMMARY_REPORT_TF:
                    ViewData["reportType"] = OBKReportCodes.SUMMARY_REPORT_TF;
                    return PartialView("SummaryReportTF", model);
                case OBKReportCodes.LABORATORY_TESTS_REPORT:
                    ViewData["reportType"] = OBKReportCodes.LABORATORY_TESTS_REPORT;
                    return PartialView("LaboratoryTestReport", model);
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
                case OBKReportCodes.DIRECTION_PAYMENTS_REPORT:
                    data = repository.DirectionPaymentsReportList();
                    return Json(data.ToDataSourceResult(request));
                case OBKReportCodes.ZBK_DETAIL_REPORT:
                    data = repository.ZBKDetailReportView();
                    return Json(data.ToDataSourceResult(request));
                case OBKReportCodes.ZBK_DEFECTIVE_PRODUCT:
                    data = repository.OBK_DefectiveProductsReportView();
                    return Json(data.ToDataSourceResult(request));
                case OBKReportCodes.GMP_REPORT:
                    data = repository.OBK_GMPReportView();
                    return Json(data.ToDataSourceResult(request));
            }

            return Json("");
        }

        public ActionResult LaboratoriesReport()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult SpecialistsReportList([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.OBK_SpecialistsReport();
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult OBK_LaboratoryList([DataSourceRequest] DataSourceRequest request, Guid? unitLaboratoryId)
        {
            var data = repository.OBK_LaboratoryFunction(unitLaboratoryId);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult LaboratoryNamesList()
        {
            var data = repository.LaboratoryListFunction_Result();
            return Json(data.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SummaryReportTF([DataSourceRequest] DataSourceRequest request, DateTime? dateFrom, DateTime? dateTo)
        {
            var data = repository.OBK_SummaryReportTF(dateFrom, dateTo);
            return Json(data.ToDataSourceResult(request)); 
        }

        public ActionResult ExportSummaryReportTF(DateTime? dateFrom, DateTime? dateTo)
        {
            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/Mrts/OBK/SummaryReportTF.mrt"));
            report.Compile();
            var data = repository.OBK_SummaryReportTF(dateFrom, dateTo);
            report.RegBusinessObject("List", data);
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;
            return File(stream, "application/excel", "Отчет.xls");
        }

        public ActionResult ExportOBKLaboratoryReport([DataSourceRequest] DataSourceRequest request, Guid? unitLaboratoryId)
        {
            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/Mrts/OBK/OBKLaboratoryReport.mrt"));
            report.Compile();
            var data = repository.OBK_LaboratoryFunction(unitLaboratoryId);
            var result = data.ToDataSourceResult(request);
            report.RegBusinessObject("List", result.Data);
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;
            return File(stream, "application/excel", "Отчет.xls");
        }

        public ActionResult OBK_Ref_Type([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.OBK_Ref_Type();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductTypes([DataSourceRequest] DataSourceRequest request)
        {
            var data = new[] { new { Id = 1, Name = "ЛС" }, new { Id = 2, Name = "ИМН" } } ;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LaboratoryWorkers()
        {
            var data = repository.LaboratoryWorkers();
            return Json(data, JsonRequestBehavior.AllowGet);
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
                case OBKReportCodes.DIRECTION_PAYMENTS_REPORT:
                    report.Load(Server.MapPath("../Reports/Mrts/OBK/DirectionToPaymentsReport.mrt"));
                    report.Compile();
                    data = repository.DirectionPaymentsReportList().ToDataSourceResult(request);
                    report.RegBusinessObject("List", data.Data);
                    break;
                case OBKReportCodes.ZBK_DETAIL_REPORT:
                    report.Load(Server.MapPath("../Reports/Mrts/OBK/ZBKDetailReport.mrt"));
                    report.Compile();
                    data = repository.ZBKDetailReportView().ToDataSourceResult(request);
                    report.RegBusinessObject("List", data.Data);
                    break;
                case OBKReportCodes.ZBK_DEFECTIVE_PRODUCT:
                    report.Load(Server.MapPath("../Reports/Mrts/OBK/OBK_DefectiveProductsReport.mrt"));
                    report.Compile();
                    data = repository.OBK_DefectiveProductsReportView().ToDataSourceResult(request);
                    report.RegBusinessObject("List", data.Data);
                    break;
                case OBKReportCodes.GMP_REPORT:
                    report.Load(Server.MapPath("../Reports/Mrts/OBK/GMPReport.mrt"));
                    report.Compile();
                    data = repository.OBK_GMPReportView().ToDataSourceResult(request);
                    report.RegBusinessObject("List", data.Data);
                    break;
                case OBKReportCodes.SPECIALISTS_REPORT:
                    report.Load(Server.MapPath("../Reports/Mrts/OBK/SpecialistsReport.mrt"));
                    report.Compile();
                    data = repository.OBK_SpecialistsReport().ToDataSourceResult(request);
                    report.RegBusinessObject("List", data.Data);
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