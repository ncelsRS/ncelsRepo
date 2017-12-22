using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Ncels.Helpers;
using Newtonsoft.Json;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.Contract;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Helpers;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.Web.Script.Serialization;
using PW.Ncels.Database.Controller;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Notifications;

namespace PW.Ncels.Controllers
{
    [Authorize]
    public class ZBKCopyController : ACommonController
    {

        private ZBKCopyRepository repository = new ZBKCopyRepository();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create(Guid stageExpDocId, Guid? ZBKCopyId)
        {
            var model = repository.CreateCopy(stageExpDocId, ZBKCopyId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid ZBKCopyId)
        {
            var model = repository.EditCopy(ZBKCopyId);

            return View(model);
        }

        public ActionResult Update(Guid Id, int quantity)
        {
            return Json(new { success = repository.Update(Id, quantity) });
        }

        public ActionResult DocumentRead(string AttachPath)
        {
            OBKCertificateFileModel fileModel = new OBKCertificateFileModel();

            fileModel.AttachPath = AttachPath;
            fileModel.AttachFiles = UploadHelper.GetFilesInfo(AttachPath, false);

            return Content(JsonConvert.SerializeObject(fileModel, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
        }

        public ActionResult Products(Guid contractId)
        {
            return Json(new { success = true, result = repository.Products(contractId) });
        }

        public ActionResult ZbkList()
        {
            ViewData["AssessmentDeclarationNumbers"] =
            new SelectList(repository.AssessmentDeclarationNumbers(), "Id", "Number");

            ViewData["OBK_Ref_Type"] =
            new SelectList(repository.OBK_Ref_Type(), "Id", "NameRu");

            return View();
        }

        //public ActionResult GetSignZBKCopy(Guid ZBKCopyId)
        //{
        //    var signData = repository.GetSignData(ZBKCopyId);
        //    return Json(new { data = signData }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult ZBKCopiesCreated()
        {
            var result = repository.ZBKCopiesCreated();

            if (result != null)
            {
                return Json(new { success = true, result = result });
            }

            return Json(new { success = false });
        }

        public ActionResult ZBKCopies(Guid? declarationNumber, int? declarationType, string decisionNumber, DateTime? decisionDate)
        {
            var result = repository.ZBKCopies(declarationNumber, declarationType, decisionNumber, decisionDate);

            if (result != null)
            {
                return Json(new { success = true, result = result });
            }

            return Json(new { success = false });
        }

        public ActionResult SaveSignedZBKCopy(Guid id, string signedData)
        {
            var message = repository.SaveSignZBKCopy(id, signedData);
            return Json(new { message });
        }

        public ActionResult SignData(Guid id)
        {
            return Json(new
            {
                IsSuccess = true,
                preambleXml = repository.GetSignData(id)
            }, JsonRequestBehavior.AllowGet);
        }

    }
}