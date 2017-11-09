using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Common;
using Stimulsoft.Report;
using Document = PW.Ncels.Database.DataModel.Document;

namespace PW.Prism.Controllers
{
	[Authorize]
    public class UploadController : Controller
    {
		private ncelsEntities db = UserHelper.GetCn();
        //
        // GET: /Upload/
		
		public ActionResult Download(string id, string name) {
			byte[] dataDownload = UploadHelper.Download(id, name, false);


			return File(
				dataDownload, System.Net.Mime.MediaTypeNames.Application.Octet, name);
		}


	    public JsonResult FileRead([DataSourceRequest] DataSourceRequest request,string taskId)
	    {
		    Task task = db.Tasks.FirstOrDefault(o=>o.Id== new Guid(taskId)); 
		    Document document = db.Documents.FirstOrDefault(o=>o.Id== task.DocumentId); 
		   var files = UploadHelper.GetFilesInfo(document.AttachPath.ToString(), false);
		    return Json(files.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
	    }

        public JsonResult FileReadTask([DataSourceRequest] DataSourceRequest request,string taskId)
	    {
	
		   var files = UploadHelper.GetFilesInfo(taskId, false);
		    return Json(files.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
	    }

		public JsonResult FileTaskRead( string taskId) {
			Task task = db.Tasks.FirstOrDefault(o => o.Id == new Guid(taskId));
			Document document = db.Documents.FirstOrDefault(o => o.Id == task.DocumentId);
			var files = UploadHelper.GetFilesInfo(document.AttachPath.ToString(), false);
			return Json(files, JsonRequestBehavior.AllowGet);
		}
        public JsonResult FileReadonlyById([DataSourceRequest] DataSourceRequest request, string docId) {
   
                var files = UploadHelper.GetFilesInfo(docId, false);
                return Json(files.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        
     
        }
        public JsonResult FileReadonly([DataSourceRequest] DataSourceRequest request, string docId)
        {
            Document document = db.Documents.FirstOrDefault(o => o.Id == new Guid(docId));
            if (document != null)
            {
                var files = UploadHelper.GetFilesInfo(document.AttachPath, false);
                return Json(files.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            return Json(JsonRequestBehavior.AllowGet);
        }

		public JsonResult SampleFileRead([DataSourceRequest] DataSourceRequest request, string objId) {
			var files = UploadHelper.GetFilesInfo(objId, false);
			return Json(files.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
		}

		public JsonResult FileProjectReadonly([DataSourceRequest] DataSourceRequest request, string docId, string dicId)
        {
           
                var files = FileHelper.GetAttachListByDoc(db,docId,dicId).ToDataSourceResult(request);
                return Json(files, JsonRequestBehavior.AllowGet);
            
            return Json(JsonRequestBehavior.AllowGet);
        }

        public JsonResult Files([DataSourceRequest] DataSourceRequest request, string id) {
			Document document = db.Documents.FirstOrDefault(o => o.Id == new Guid(id));
			var files = UploadHelper.GetFilesInfo(document.AttachPath.ToString(), false);
			return Json(files.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
		}
		public ActionResult FileView(string id, string name)
		{
			ViewBag.FileId = id;
			ViewBag.FileName = name;
			return PartialView();
		}
        public ActionResult ReportFileView(string code, string name, string report)
        {
            ViewBag.FileId = code;
            ViewBag.FileName = name;
            ViewBag.Report = report;
            return PartialView();
        }

        public ActionResult ReportTmcView(string code, string name, string report,
            DateTime dateStart, DateTime dateEnd, string departmentId)
        {
            ViewBag.FileId = code;
            ViewBag.FileName = name;
            ViewBag.Report = report;
            ViewBag.DateStart = dateStart;
            ViewBag.DateEnd = dateEnd;
            ViewBag.DepartmentId = departmentId;
            return PartialView();
        }

        public ActionResult FileDataView(string id, string name) {
			byte[] dataDownload = UploadHelper.GetPreview(id, name, false);
            
			return File(dataDownload, System.Net.Mime.MediaTypeNames.Application.Pdf, name);
		}

		public ActionResult GenerationStamp(string id, string name)
		{
			UploadHelper.GenerationStamp(id, name);
			return Content(bool.TrueString);

		}

		public ActionResult Save(IEnumerable<HttpPostedFileBase> files, string documentId) {
			// The Name of the Upload component is "files"
			if (files != null) {
				foreach (var file in files) {

					UploadHelper.Upload(file.InputStream,file.FileName,documentId);
					// Some browsers send file names with full path.
					// We are only interested in the file name.
					//var fileName = Path.GetFileName(file.FileName);
					//var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

					//// The files are not actually saved in this demo
					// file.SaveAs(physicalPath);
				}
			}

			// Return an empty string to signify success
			return Content("");
		}

		public ActionResult Remove(string[] fileNames, string documentId) {
			// The parameter of the Remove action must be called "fileNames"

			if (fileNames != null) {
				foreach (var fullName in fileNames) {
					var fileName = Path.GetFileName(fullName);
					UploadHelper.DeleteFile(documentId,fileName);
					//var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

					//// bbnrrf TODO: Verify user permissions

					//if (System.IO.File.Exists(physicalPath)) {
					//	// The files are not actually removed in this demo
					//	// System.IO.File.Delete(physicalPath);
					//}
				}
			}

			// Return an empty string to signify success
			return Content("");
		}

        public JsonResult GetAttachList([DataSourceRequest] DataSourceRequest request, string id, string type) {
            return Json(FileHelper.GetAttachListEdit(db, id, type).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeletePreview(string documentId, string fileName)
		{
			if (!string.IsNullOrEmpty(documentId) && !string.IsNullOrEmpty(fileName))
			{
				UploadHelper.DeletePreview(documentId, fileName);
			}
			return Content("");
		}

        public FileResult CardDownload(Guid id)
        {
            var stream = ReportHelper.GetReportPdf(id, db);
            return File(stream, "application/force-download", "Заявление.pdf");
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

        [HttpGet]
        public ActionResult FileDelete(string id = null, string path = null, string name = null, string fileId = null)
        {
            if (fileId != null)
                return Json(FileHelper.DeleteAttach(db, id, path, fileId), JsonRequestBehavior.AllowGet);
            return Json(FileHelper.DeleteAttach(path, id, name), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FilePost(string code = null, string path = null, bool saveMetadata = false, string originFileId = null)
        {
            if (!string.IsNullOrEmpty(originFileId))
                return Json(FileHelper.SaveAttachNewVersion(code, path, originFileId, Request, db),
                    JsonRequestBehavior.AllowGet);
            return Json(FileHelper.SaveAttach(code, path, Request, saveMetadata, originFileId, db), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FilePostv2(string code = null, string path = null, bool saveMetadata = false, string originFileId = null, string comment = "", string lang = "", string numOfPages = "")
        {
            int? numOfPagesInt = null;
            if (!String.IsNullOrEmpty(numOfPages))
            {
                int tmp;
                var result = Int32.TryParse(numOfPages, out tmp);
                if (result)
                {
                    numOfPagesInt = tmp;
                }
            }

            if (!string.IsNullOrEmpty(originFileId))
                return Json(FileHelper.SaveNextFileVersion(code, path, originFileId, Request, db, lang, comment, numOfPagesInt),
                    JsonRequestBehavior.AllowGet);
            return Json(FileHelper.SaveAttach(code, path, Request, saveMetadata, originFileId, db, lang, comment, numOfPagesInt), JsonRequestBehavior.AllowGet);
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
    }
}