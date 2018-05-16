using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Expertise;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers
{
    public class ReestrController : Controller
    {
        // GET: Reestr
        public ActionResult Index()
        {
            var guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult ReadReestrDirectionToPay([DataSourceRequest] DataSourceRequest request)
        {
            var repository = new ReestrDirectionToPayRepository(false);
            var data = repository.GetAsQuarable();

            data = data.OrderByDescending(m => m.ModifyDate); ;
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult CocIndex()
        {
            var guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult ReadReestrCertificateOfComplete([DataSourceRequest] DataSourceRequest request)
        {
            var repository = new ReestrDirectionToPayRepository(false);
            var data = repository.GetAsQuarable();

            data = data.OrderByDescending(m => m.ModifyDate); ;
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public FileStreamResult ExportFile(string type, DateTime? dateStart, DateTime? dateEnd)
        {
            string path = string.Empty;
            string fileName = string.Empty;
            if (type == "directiontopay")
            {
                path = "../Reports/Reestrs/ReestrDirectionToPayReport.mrt";
                fileName = "Реестр направлений " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            }
            else if (type == "certificateofcomplete")
            {
                path = "../Reports/Reestrs/ReestrCertificateOfCompleteReport.mrt";
                fileName = "Реестр акта выполненных работ " + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            }


            StiReport report = new StiReport();

            report.Load(Server.MapPath(path));
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }

            if (report.Dictionary.Variables.Contains("DateStart"))
            {
                report.Dictionary.Variables["DateStart"].ValueObject = dateStart;
            }

            if (report.Dictionary.Variables.Contains("DateEnd"))
            {
                report.Dictionary.Variables["DateEnd"].ValueObject = dateEnd;
            }

            report.Render(false);
            var stream = new MemoryStream();

            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;

            return File(stream, "application/excel", fileName);
        }

    }
}