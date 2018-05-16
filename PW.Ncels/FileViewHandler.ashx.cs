using System;
using System.IO;
using System.Web;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels
{
    /// <summary>
    /// Summary description for FileViewHandler
    /// </summary>
    public class FileViewHandler : IHttpHandler
    {
        private ncelsEntities db = UserHelper.GetCn();

        public void ProcessRequest(HttpContext context) {
            MemoryStream stream = null;
            if (!string.IsNullOrEmpty(context.Request["documentId"])) {
                var documentId = Guid.Parse(context.Request["documentId"]);
                var data = FileHelper.GetDocumentAttachFile(db, documentId);
                if (data != null) {
                    context.Response.ContentType = "application/pdf";
                    context.Response.BinaryWrite(data);
                }
                else {
                    context.Response.Write("Документ отсутствует");
                }

            }
            else {
                Guid id = Guid.Parse(context.Request["id"]);
                stream = ReportHelper.GetReportPdf(id, db);
                context.Response.ContentType = "application/pdf";
                context.Response.BinaryWrite(stream.ToArray());
            }

            return;
        }

        public bool IsReusable{
            get{
                return false;
            }
        }
    }
}