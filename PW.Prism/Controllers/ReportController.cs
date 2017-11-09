using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Export;

namespace PW.Prism.Controllers
{
    public class ReportController : Controller
    {
        private ncelsEntities db = UserHelper.GetCn();

        // GET: /Report/
        public ActionResult Index()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.ReportList = ReportHelper.GetList();

            return PartialView(guid);
        }
        public ActionResult IndexDepartment()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.ReportList = ReportHelper.GetListDepartment();

            return PartialView(guid);
        }
        public ActionResult IndexDepartmentTMC()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.ReportList = ReportHelper.GetListDepartmentTMC();

            return PartialView(guid);
        }

        public ActionResult IndexReport(string type)
        {
            Guid guid = Guid.NewGuid();
            ViewBag.ReportList = ReportHelper.GetListReport(type);

            return PartialView(guid);
        }

        public ActionResult LsPriceComparisonReportParams()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        public ActionResult PriceLsList([DataSourceRequest]DataSourceRequest request)
        {
            var data = (IEnumerable<PriceProjectsView>) db.PriceProjectsViews
                .Where(e=>e.Type==0 || e.Type==2)
                .ToDataSourceResult(request).Data;
            var projects = data.Select(e => new
            {
                e.Id,
                NameRu = string.Format("{0}, {1}, {2}", e.CreatedDate.ToString("dd.MM.yyyy"), e.NameRu, e.Dosage)
            });
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
        // GET: /Report/Details/5
        public ActionResult Details(string name, DateTime? dateStart, DateTime? dateEnd, int? stage, string stageName)
        {

            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/List/" + name));
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            if (!dateStart.HasValue)
            {
                dateStart = DateTime.Now.AddMonths(-1);
            }
            if (!dateEnd.HasValue)
            {
                dateEnd = DateTime.Now;
            }
            if (report.Dictionary.Variables.Contains("DateStart"))
            {
                report.Dictionary.Variables["DateStart"].ValueObject = dateStart;
            }
            if (report.Dictionary.Variables.Contains("DateEnd"))
            {
                report.Dictionary.Variables["DateEnd"].ValueObject = dateEnd;
            }
            if (report.Dictionary.Variables.Contains("Stage"))
            {
                report.Dictionary.Variables["Stage"].ValueObject = stage ?? -1;
            }
            if (report.Dictionary.Variables.Contains("StageName"))
            {
                report.Dictionary.Variables["StageName"].ValueObject = stageName;
            }

            if (report.Dictionary.Variables.Contains("OrganizationId"))
            {
                report.Dictionary.Variables["OrganizationId"].ValueObject = UserHelper.GetCurrentEmployee().OrganizationId;
            }
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.HtmlDiv, stream);

            stream.Position = 0;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return Content(reader.ReadToEnd());
            }

        }

        public ActionResult NcelsDetails(Guid id)
        {
            var project = db.ProjectsViews.FirstOrDefault(m => m.Id == id);
            StiReport report = new StiReport();
            if (project != null)
            {
                if (project.IsRegisterProject != null && project.IsRegisterProject.Value)
                {
                    switch (project.Type)
                    {
                        case 0:
                            report.Load(Server.MapPath("../Reports/Mrts/RegisterLs.mrt"));
                            break;
                        case 1:
                            report.Load(Server.MapPath("../Reports/Mrts/ReRegisterLs.mrt"));
                            break;
                        case 2:
                            report.Load(Server.MapPath("../Reports/Mrts/ChRegisterLs.mrt"));
                            break;
                    }
                    var registerProject = db.RegisterProjects.First(m => m.Id == id);
                    var contract = db.Contracts.First(m => m.Id == registerProject.ContractId.Value);
                    report.Dictionary.Variables["RegisterProjectId"].ValueObject = id;
                    report.Dictionary.Variables["ManufacturerId"].ValueObject = contract.ManufacturerOrganizationId;
                    report.Dictionary.Variables["HolderId"].ValueObject = contract.HolderOrganizationId;
                    report.Dictionary.Variables["ProxyId"].ValueObject = contract.ApplicantOrganizationId;
                }
                else
                {
                    switch (project.Type)
                    {
                        case 0:
                            report.Load(Server.MapPath("../Reports/Mrts/PriceLs.mrt"));
                            break;
                        case 1:
                            report.Load(Server.MapPath("../Reports/Mrts/PriceImn.mrt"));
                            break;
                        case 2:
                            report.Load(Server.MapPath("../Reports/Mrts/RePriceLs.mrt"));
                            break;
                        case 3:
                            report.Load(Server.MapPath("../Reports/Mrts/RePriceImn.mrt"));
                            break;
                    }
                    var priceProject = db.PriceProjects.FirstOrDefault(m => m.Id == id);
                    report.Dictionary.Variables["PriceProjectsId"].ValueObject = id;
                    report.Dictionary.Variables["ManufacturerId"].ValueObject = priceProject.ManufacturerOrganizationId;
                    report.Dictionary.Variables["HolderId"].ValueObject = priceProject.HolderOrganizationId;
                    report.Dictionary.Variables["ProxyId"].ValueObject = priceProject.ProxyOrganizationId;
                }
            }
            else
            {
                report.Load(Server.MapPath("../Reports/Mrts/Contract.mrt"));
                report.Dictionary.Variables["ContractId"].ValueObject = id;
            }
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.HtmlDiv, stream);

            stream.Position = 0;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return Content(reader.ReadToEnd());
            }
        }

        public ActionResult DepartamentDetails(string name, DateTime? dateStart, DateTime? dateEnd, string departmentId, string lsPriceComparisonItem1, string lsPriceComparisonItem2)
        {

            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/List/" + name));
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            if (!dateStart.HasValue)
            {
                dateStart = DateTime.Now.AddMonths(-1);
            }
            if (!dateEnd.HasValue)
            {
                dateEnd = DateTime.Now;
            }
            if (report.Dictionary.Variables.Contains("DateStart"))
            {
                report.Dictionary.Variables["DateStart"].ValueObject = dateStart;
            }
            if (report.Dictionary.Variables.Contains("DateEnd"))
            {
                report.Dictionary.Variables["DateEnd"].ValueObject = dateEnd;
            }
            if (report.Dictionary.Variables.Contains("DepartmentId") && !string.IsNullOrEmpty(departmentId))
            {
                report.Dictionary.Variables["DepartmentId"].Value = departmentId;  //UserHelper.GetDepartment().Id.ToString();
            }
            if (report.Dictionary.Variables.Contains("UserId"))
            {
                report.Dictionary.Variables["UserId"].Value = UserHelper.GetCurrentEmployee().Id.ToString();
            }
            if (report.Dictionary.Variables.Contains("OrganizationId"))
            {
                report.Dictionary.Variables["OrganizationId"].ValueObject = UserHelper.GetCurrentEmployee().OrganizationId;
            }
            if (report.Dictionary.Variables.Contains("PriceProjectId1") && !string.IsNullOrEmpty(lsPriceComparisonItem1))
            {
                report.Dictionary.Variables["PriceProjectId1"].ValueObject = Guid.Parse(lsPriceComparisonItem1);
            }
            if (report.Dictionary.Variables.Contains("PriceProjectId2") && !string.IsNullOrEmpty(lsPriceComparisonItem2))
            {
                report.Dictionary.Variables["PriceProjectId2"].ValueObject = Guid.Parse(lsPriceComparisonItem2);
            }
            report.Render(false);

            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.HtmlDiv, stream);
            stream.Position = 0;
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return Content(reader.ReadToEnd());
            }
		}

        /// <summary>
        /// Send button Handler
        /// </summary>
        /// <param name="name">название отчета</param>
        /// <param name="dateStart">начало периода</param>
        /// <param name="dateEnd">конец периода</param>
        /// <param name="departmentId">ид департамента</param>
        /// <returns></returns>
        public ActionResult DepartamentDetailsSend(string name, DateTime? dateStart, DateTime? dateEnd, string departmentId)
        {
            bool isSuccess = true;
            string reason = string.Empty;

            var user = UserHelper.GetCurrentEmployee();
            Guid depId = Guid.Parse(departmentId);
            Guid reportId = Guid.NewGuid();

            var department = db.Units.FirstOrDefault(dep => dep.Id == depId);
            if (!dateStart.HasValue)
            {
                dateStart = new DateTime(DateTime.Now.AddMonths(-1).Year
                    , DateTime.Now.AddMonths(-1).Month
                    , 1);
            }
            if (!dateEnd.HasValue)
            {
                dateEnd = new DateTime(DateTime.Now.AddMonths(-1).Year
                    , DateTime.Now.AddMonths(-1).Month
                    , DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month));
            }
            

            try
            {

                string reportName = "Отчет от " + DateTime.Now.ToString("dd.MM.yyyy");
                if (name == "applicationD_5.mrt")
                {
                    reportName = "Сводный отчет от " + DateTime.Now.ToString("dd.MM.yyyy");
                    department = db.Units.FirstOrDefault(dep => dep.Code == OrganizationConsts.ReasearchCenterCode);
                }
                if (department == null)
                {
                    department = db.Units.First(dep => dep.Id == user.OrganizationId);
                }


                using (var dLocal = new ncelsEntities())
                {
                    TmcReport report = new TmcReport()
                    {
                        Id = reportId,
                        CreateEmployeeId = user.Id,
                        CreateEmployeeValue = user.DisplayName,
                        DepartmentId = depId,
                        DepartmentValue = department.DisplayName,
                        Name = reportName,
                        CreatedDate = DateTime.Now,
                        PeriodStartDate = dateStart.Value,
                        PeriodEndDate = dateEnd.Value,
                        ReportType = name
                    };
                    dLocal.TmcReports.Add(report);
                    dLocal.SaveChanges();
                    // согласование начальником отдела
                    
                    Guid bossId;
                    bool isParse = Guid.TryParse(department.BossId, out bossId);


                    TmcReportTask task = new TmcReportTask()
                    {
                        Id = Guid.NewGuid(),
                        Operation = OperatorConsts.AgreeCode,
                        CreateDate = DateTime.Now,
                        CreateEmployeeId = user.Id,
                        CreateEmployeeValue = user.DisplayName,
                        ExecutorEmployeeId = isParse ? bossId : user.Id,
                        ExecutorEmployeeValue = isParse ? department.BossValue : user.DisplayName,
                        Stage = 1,
                        State = 0,
                        refTmcReport = report.Id,
                    };
                    dLocal.TmcReportTasks.Add(task);
                    dLocal.SaveChanges();
                }
            }
            catch (Exception e)
            {
                isSuccess = false;
                reason = e.ToString();
            }

            return Json(new { Id = reportId, IsSuccess = isSuccess, Reason = reason }, JsonRequestBehavior.AllowGet);
        }

        public FileStreamResult ExportFile(string name, DateTime? dateStart, DateTime? dateEnd, string departmentId, string lsPriceComparisonItem1, string lsPriceComparisonItem2) {

            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/List/" + name));
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            if (!dateStart.HasValue)
            {
                dateStart = DateTime.Now.AddMonths(-1);
            }
            if (!dateEnd.HasValue)
            {
                dateEnd = DateTime.Now;
            }
            if (report.Dictionary.Variables.Contains("DateStart"))
            {
                report.Dictionary.Variables["DateStart"].ValueObject = dateStart;
            }
            if (report.Dictionary.Variables.Contains("DateEnd"))
            {
                report.Dictionary.Variables["DateEnd"].ValueObject = dateEnd;
            }
            if (report.Dictionary.Variables.Contains("DepartamentId"))
            {
                report.Dictionary.Variables["DepartamentId"].Value = UserHelper.GetDepartment().Id.ToString();
            }
            if (report.Dictionary.Variables.Contains("UserId"))
            {
                report.Dictionary.Variables["UserId"].Value = UserHelper.GetCurrentEmployee().Id.ToString();
            }
            if (report.Dictionary.Variables.Contains("OrganizationId"))
            {
                report.Dictionary.Variables["OrganizationId"].ValueObject = UserHelper.GetCurrentEmployee().OrganizationId;
            }
            if (report.Dictionary.Variables.Contains("PriceProjectId1") && !string.IsNullOrEmpty(lsPriceComparisonItem1))
            {
                report.Dictionary.Variables["PriceProjectId1"].ValueObject = Guid.Parse(lsPriceComparisonItem1);
            }
            if (report.Dictionary.Variables.Contains("PriceProjectId2") && !string.IsNullOrEmpty(lsPriceComparisonItem2))
            {
                report.Dictionary.Variables["PriceProjectId2"].ValueObject = Guid.Parse(lsPriceComparisonItem2);
            }
            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;
            return File(stream, "application/excel", "отчет.xls");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}