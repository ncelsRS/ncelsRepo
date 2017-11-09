using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Aspose.Pdf;
using Ncels.Helpers;
using PW.Ncels.Database.Helpers;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Web;

namespace PW.Prism
{
    /// <summary>
    /// Summary description for FileViewHandler
    /// </summary>
    public class FileViewHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request["report"] != null)
            {
                StiReport report = new StiReport();

                report.Load(context.Server.MapPath("/Reports/List/" + context.Request["report"]));
                foreach (var item in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    item.ConnectionString = UserHelper.GetCnString();
                }
                if (report.Dictionary.Variables.Contains("OrganizationId"))
                {
                    report.Dictionary.Variables["OrganizationId"].ValueObject = UserHelper.GetCurrentEmployee().OrganizationId;
                }
                if (report.Dictionary.Variables.Contains("Code"))
                {
                    report.Dictionary.Variables["Code"].ValueObject = context.Request["code"];
                }
                if (report.Dictionary.Variables.Contains("DateStart"))
                {
                    report.Dictionary.Variables["DateStart"].ValueObject = DateTime.Parse(context.Request["DateStart"]);
                }
                if (report.Dictionary.Variables.Contains("DateEnd"))
                {
                    report.Dictionary.Variables["DateEnd"].ValueObject = DateTime.Parse(context.Request["DateEnd"]);
                }
                if (report.Dictionary.Variables.Contains("DepartmentId"))
                {
                    report.Dictionary.Variables["DepartmentId"].ValueObject = context.Request["DepartmentId"];
                }
                if (report.Dictionary.Variables.Contains("CurrentUserName"))
                {
                    report.Dictionary.Variables["CurrentUserName"].ValueObject = UserHelper.GetCurrentEmployee().ShortName;
                }
                //report.Compile();
                report.Render(false);

                var stream = new MemoryStream();

                report.ExportDocument(StiExportFormat.Pdf, stream,new StiPdfExportSettings()
                {
                   
                });
                stream.Position = 0;
                
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(stream.ToArray());
                return;
            }
            string id = context.Request["id"];
            string name = context.Request["name"];
            string type = context.Request["type"];

            LogHelper.Log.DebugFormat("FilePreview, id={0}, name={1}", id, name);

            if (type == "Download")
            {
                byte[] dataDownload = UploadHelper.GetPreview(id, name, false);
                context.Response.AddHeader("content-disposition", "attachment; filename=" + name);
                //context.Response.ContentType = "application/stream";
                context.Response.OutputStream.Write(dataDownload, 0, dataDownload.Length);
                context.Response.BinaryWrite(dataDownload);
                return;
            }
            context.Response.ContentType = "application/pdf";
            byte[] data = UploadHelper.GetPreview(id, name, false);
            context.Response.BinaryWrite(data);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}