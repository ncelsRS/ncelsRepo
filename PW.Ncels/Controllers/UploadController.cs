using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Mvc;

namespace PW.Ncels.Controllers
{
    [Authorize()]
    public class UploadController : ACommonController
    {
        private ncelsEntities db = UserHelper.GetCn();


        public ActionResult FileView(Guid id)
        {
            ViewBag.FileId = id;
            ViewBag.FileName = "Документ";
            return PartialView();
        }

        public ActionResult DocumentAttachView(Guid id, string name)
        {
            ViewBag.FileId = id;
            ViewBag.FileName = name;
            return PartialView();
        }

        public ActionResult AttachView(string id, string name)
        {
            ViewBag.FileId = id;
            ViewBag.FileName = name;
            return PartialView();
        }

        public FileResult FileDownload(string id = null, string path = null, string name = null, string fileId = null)
        {
            string file = FileHelper.GetFilePath(path, id, name, fileId);
            string fileName = name;
            if (!string.IsNullOrEmpty(fileId))
            {
                var fileKey = Guid.Parse(Path.GetFileNameWithoutExtension(fileId));
                fileName = db.FileLinks.Where(e => e.Id == fileKey).Select(e => e.FileName).FirstOrDefault();
            }
            return File(file, "application/force-download", fileName);
        }
        [HttpPost]
        public ActionResult FilePost(string code = null, string path = null, bool saveMetadata = false, string originFileId = null)
        {
            if (!string.IsNullOrEmpty(originFileId))
                return Json(FileHelper.SaveAttachNewVersion(code, path, originFileId, Request, db),
                    JsonRequestBehavior.AllowGet);
            return Json(FileHelper.SaveAttach(code, path, Request, saveMetadata, originFileId, db), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAttachList(string id = null, string type = null, bool byMetadata = false, string excludeCodes = null)
        {
            return Json(FileHelper.GetAttachList(db, id, type, byMetadata, excludeCodes), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAttachListEdit(string id = null, string type = null, bool byMetadata = false, string excludeCodes = null, bool isShowComment = false)
        {
            return Json(FileHelper.GetAttachListEdit(db, id, type, byMetadata, excludeCodes, isShowComment), JsonRequestBehavior.AllowGet);
        }

        public bool CheckAttachList(string id, string type)
        {
            return FileHelper.CheckAttachList(db, id, type);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult FileDelete(string id = null, string path = null, string name = null, string fileId = null)
        {
            if (fileId != null)
                return Json(FileHelper.DeleteAttach(db, id, path, fileId), JsonRequestBehavior.AllowGet);
            return Json(FileHelper.DeleteAttach(path, id, name), JsonRequestBehavior.AllowGet);
        }

        public FileResult CardDownload(Guid id)
        {
            var stream = ReportHelper.GetReportPdf(id, db);
            return File(stream, "application/force-download", "Заявление.pdf");
        }

        public FileResult DocumentAttachDownload(Guid documentId){
            var data = FileHelper.GetDocumentAttachFile(db, documentId);
            return File(data, "application/force-download", "Документ.pdf");
        }

        public JsonResult FileReadonly(string docId)
        {
            Document document = db.Documents.FirstOrDefault(o => o.Id == new Guid(docId));
            if (document != null)
            {
                var files = UploadHelper.GetFilesInfo(document.AttachPath, false);
                return Json(files, JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult Download(string id, string name)
        {
            byte[] dataDownload = UploadHelper.Download(id, name, false);


            return File(
                dataDownload, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }

        [HttpGet]
        public ActionResult ShowDocComment(string documentId, string categoryId)
        {
            var repository = new UploadRepository();
            var model = repository.GetComments(documentId, categoryId);
            if (model == null)
            {
                model = new FileLinksCategoryCom();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }
        [HttpPost]
        public virtual ActionResult SaveComment(string documentId, string categoryId, bool isError, string comment)
        {
            var userId = UserHelper.GetCurrentEmployee().Id.ToString();
            new UploadRepository().SaveComment(documentId, categoryId, isError, comment, userId);
            return Json(new { Success = true });
        }

        [HttpPost]
        public virtual ActionResult GetCommentFileLink(Guid id)
        {
            var model = new UploadRepository().GetFileLinkById(id);
            if (model == null)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true, comment = model.Comment, fileId =model.Id });
        }
        [HttpPost]
        public virtual ActionResult AcceptFileConfirm(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { Success = false });
            }
            var  model = new UploadRepository().AcceptFileConfirm(new Guid(id));
            if (model == null)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true, statusCode = model.DIC_FileLinkStatus.Code, statusName = model.DIC_FileLinkStatus.NameRu });
        }

        public virtual ActionResult RejectFileConfirm(string id, string note)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { Success = false });
            }
            var model = new UploadRepository().RejectFileConfirm(new Guid(id), note);
            if (model == null)
            {
                return Json(new { Success = false });
            }
            return Json(new { Success = true, statusCode = model.DIC_FileLinkStatus.Code, statusName = model.DIC_FileLinkStatus.NameRu });
        }
        public FileStreamResult ExportSafetyReportFile(Guid id, bool isKz)
        {
            SafetyreportRepository repository = new SafetyreportRepository();
            var expertiseStageDosage = repository.GetExpertiseStageDosageById(id);
            string name;
            StiReport report = new StiReport();
            if (!isKz)
            {
                report.Load(Server.MapPath("~/Reports/SafetyReport/Registration_rus.mrt"));
                 name = "Заключение.pdf";
            }
            else
            {
                report.Load(Server.MapPath("~/Reports/SafetyReport/Registration_kaz.mrt"));
                name = "Заключение на государственном.pdf";
            }

            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            // имя и должность эксперта
            //            var currentEmployee = UserHelper.GetCurrentEmployee();
            //            var employeeName = currentEmployee.FullName;
            //            var employeePosition = currentEmployee.Position.Name;

            report.Dictionary.Variables["DeclarationId"].ValueObject = expertiseStageDosage.EXP_ExpertiseStage.DeclarationId;
            report.Dictionary.Variables["StageDosageId"].ValueObject = id;

            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }

    }
}