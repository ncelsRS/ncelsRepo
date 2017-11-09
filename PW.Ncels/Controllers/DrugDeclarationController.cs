using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Controller;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Entities;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Contract;
using PW.Ncels.Database.Repository.Expertise;
using PW.Ncels.Database.Repository.Security;
using PW.Ncels.Helpers;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Ncels.Controllers
{
    public class DrugDeclarationController : ADrugDeclarationController
    {
        public DrugDeclarationController()
        {
            var user = UserHelper.GetCurrentEmployee();
            int count;
            if (user == null)
            {
                count = 0;
            }
            else
            {
                count = new NotificationManager().GetCountNotification(user.Id);
            }
            ViewBag.NotificationCount = count;
        }

        public ActionResult RegisterDrugList()
        {
            return View();
        }

        /// <summary>
        /// Создание заявление
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            var model = new EXP_DrugDeclaration
            {
                OwnerId = UserHelper.GetCurrentEmployee().Id,
                Id = Guid.NewGuid(),
                EditorId = UserHelper.GetCurrentEmployee().Id.ToString(),
                StatusId = CodeConstManager.STATUS_DRAFT_ID,
                ExpDrugExportTrades = new List<EXP_DrugExportTrade>(),
                ExpDrugPatents = new List<EXP_DrugPatent>(),
                ExpDrugTypes = new List<EXP_DrugType>(),
                ExpDrugProtectionDocs = new List<EXP_DrugProtectionDoc>(),
                ExpDrugOtherCountries = new List<EXP_DrugOtherCountry>(),
                ExpDrugOrganizationses = new List<EXP_DrugOrganizations>(),
                ExpDrugDosages = new List<EXP_DrugDosage>(),
                ExpDrugChangeTypes = new List<EXP_DrugChangeType>(),
                ConclusionSafetyReports = new List<ConclusionSafetyReport>()
            };
            FillViewBag(model);
            return View(model);
        }

        [HttpGet]
        public ActionResult ShowDetails(string id)
        {
            var model = GetDrugDeclarationById(id);
            return View(model);
        }

        public ActionResult Delete(string id)
        {
            new DrugDeclarationRepository().DeleteReport(id, UserHelper.GetCurrentEmployee().Id);
            return RedirectToAction("RegisterDrugList");
        }
        public ActionResult DublicateDrug(string id)
        {
            var model = new DrugDeclarationRepository().DublicateDrug(id, UserHelper.GetCurrentEmployee().Id);
            model.IsExist = true;
            FillDeclarationControl(model);
            return View("Create", model);
        }

        

        [HttpGet]
        public ActionResult Edit(string id, bool? isLoadReest)
        {
            if (isLoadReest != null && isLoadReest.Value)
            {
                new DrugDeclarationRepository().LoadFromReestr(id);
            }
            var model = GetDrugDeclarationById(id);
            return View("Create", model);
        }
        public ActionResult SignOperation(string id)
        {

            var repository = new DrugDeclarationRepository();
            var model = repository.GetPreamble(new Guid(id));
            bool IsSuccess = true;
            string preambleXml = string.Empty;
            try
            {
                preambleXml = SerializeHelper.SerializeDataContract(model);
                preambleXml = preambleXml.Replace("utf-16", "utf-8");
            }
            catch (Exception e)
            {
                IsSuccess = false;
            }

            return Json(new
            {
                IsSuccess,
                preambleXml
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SignForm(string preambleId, string xmlAuditForm)
        {
            var success = true;
            var repository = new DrugDeclarationRepository();
            var model = repository.GetById(preambleId);
            new SignDocumentRepository().SaveSignDocument(UserHelper.GetCurrentEmployee().Id, xmlAuditForm, model);

            if (model.StatusId == CodeConstManager.STATUS_DRAFT_ID)
            {
                model.FirstSendDate = DateTime.Now;
            }

            model.StatusId = CodeConstManager.STATUS_SEND_ID;
            model.SendDate = DateTime.Now;
            model.IsSigned = true;
            var history = new EXP_DrugDeclarationHistory()
            {
                DateCreate = DateTime.Now,
                DrugDeclarationId = model.Id,
                XmlSign = xmlAuditForm,
                StatusId = CodeConstManager.STATUS_SEND_ID,
                UserId = UserHelper.GetCurrentEmployee().Id,
                Note = "Отчет предоставлен. Дата отправки:" + DateTime.Now
            };
            if (string.IsNullOrEmpty(model.Number))
            {
                model.Number = repository.GetAppNumber();
            }
            var indexWrapping = 1;
            foreach (var expDrugDosage in model.EXP_DrugDosage)
            {
                if (string.IsNullOrEmpty(expDrugDosage.RegNumber))
                {
                    expDrugDosage.RegNumber = model.Number + "." + indexWrapping;
                    indexWrapping++;
                }
            }
            repository.SaveOrUpdate(model, UserHelper.GetCurrentEmployee().Id);
            repository.SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            // Set Status Sended
            return Json(new
            {
                success
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult SendNotSign(string modelId)
        {
            var repository = new DrugDeclarationRepository();
            var model = repository.GetById(modelId);

            if (model.StatusId == CodeConstManager.STATUS_DRAFT_ID)
            {
                model.FirstSendDate = DateTime.Now;
            }

            model.StatusId = CodeConstManager.STATUS_SEND_ID;
            model.SendDate = DateTime.Now;
            model.IsSigned = false;
            var history = new EXP_DrugDeclarationHistory()
            {
                DateCreate = DateTime.Now,
                DrugDeclarationId = model.Id,
                StatusId = CodeConstManager.STATUS_SEND_ID,
                UserId = UserHelper.GetCurrentEmployee().Id,
                Note = "Отчет предоставлен без подписи. Дата отправки:" + DateTime.Now
            };
            if (string.IsNullOrEmpty(model.Number))
            {
                model.Number = repository.GetAppNumber();
            }
            var indexWrapping = 1;
            foreach (var expDrugDosage in model.EXP_DrugDosage)
            {
                if (string.IsNullOrEmpty(expDrugDosage.RegNumber))
                {
                    expDrugDosage.RegNumber = model.Number + "." + indexWrapping;
                    indexWrapping++;
                }
            }
            repository.SaveOrUpdate(model, UserHelper.GetCurrentEmployee().Id);
            repository.SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            return Json(new
            {
                isSuccess = true
            });
        }
        [HttpPost]
        public ActionResult FileUpload(EXP_ExpertiseStageRemark document, IEnumerable<HttpPostedFileBase> files)
        {
            var repository = new DrugDeclarationRepository();
            var remark = repository.GetRemarksListFiles(document.Id);
            var model = GetDrugDeclarationById(remark.EXP_ExpertiseStage.DeclarationId.ToString());
            if (files == null)
            {
                files = new List<HttpPostedFileBase>();
            }
            var httpPostedFileBases = files as HttpPostedFileBase[] ?? files.ToArray();

            for (int i = 0; i < httpPostedFileBases.Count(); i++)
            {
                HttpPostedFileBase file = httpPostedFileBases[i];
                Guid fileId = Guid.NewGuid();
                var code = repository.GetRemarkCode();
                var path = model.ObjectId;
                string savedFileName = file.FileName;

                var ownId = UserHelper.GetCurrentEmployee().Id;
                int? currentStageId = null;

                string root = ConfigurationManager.AppSettings["AttachPath"];
                string fullName = Path.Combine(root, FileHelper.Root, model.ObjectId ?? "", code ?? "", savedFileName);
                DirectoryInfo info =
                    new DirectoryInfo(Path.Combine(root, FileHelper.Root, model.ObjectId ?? "", code ?? ""));
                if (!info.Exists)
                    info.Create();
                file.SaveAs(fullName);

                var fileLink = new FileLink()
                {
                    Id = fileId,
                    CreateDate = DateTime.Now,
                    CategoryId = !string.IsNullOrEmpty(code) ? (Guid?) Guid.Parse(code) : null,
                    DocumentId = !string.IsNullOrEmpty(path) ? (Guid?) Guid.Parse(path) : null,
                    FileName = savedFileName,
                    OwnerId = ownId,
                    Version = 1,
                    Comment = remark.Id.ToString(),
                    Language = "",
                    PageNumbers = null,
                    StageId = null

                };
                repository.SaveRemarkFile(fileLink, remark);
            }
            return RedirectToAction("Edit", new { Id = model.ObjectId });
        }

        public ActionResult ShowFile(string id, string filename)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Create");
            }

            return null;

        }

        public ActionResult ExportFileTemplate(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult ExportFilePdf(Guid id)
        {
            var db = new ncelsEntities();
            string name = "Заявление на проведение экспертизы лс.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/DrugDeclaration.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["DrugDeclarationId"].ValueObject = id;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            var drugDeclaration = db.EXP_DrugDeclaration.FirstOrDefault(dd => dd.Id == id);
            var drugDeclarationHistory = drugDeclaration.EXP_DrugDeclarationHistory.Where(dh => dh.XmlSign != null)
                .OrderByDescending(dh => dh.DateCreate).FirstOrDefault();
            if (drugDeclarationHistory != null)
            {
                Aspose.Words.Document doc = new Aspose.Words.Document(stream);
                doc.InserQrCodesToEnd("ExecutorSign", drugDeclarationHistory.XmlSign);
                var pdfFile = new MemoryStream();
                pdfFile.Position = 0;
                stream.Close();
                return new FileStreamResult(pdfFile, "application/pdf");
            }
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult LetterPreview(Guid id)
        {
            var repo = new DrugPrimaryRepository();
            var correspondence = repo.GetCorespondence(id.ToString());            
            var reportTemplate = "";
            switch (correspondence.EXP_DIC_CorespondenceSubject.Code)
            {
                case EXP_DIC_CorespondenceSubject.Remarks:
                    reportTemplate = "~/Reports/DrugPrimary/Corespondence.mrt";
                    break;
                case EXP_DIC_CorespondenceSubject.RefuseByPayment:
                    reportTemplate = "~/Reports/DrugPrimary/RefuseByPaymentLetter.mrt";
                    break;
                default:
                    reportTemplate = "";
                    break;
            }
            var reportPath = Server.MapPath(reportTemplate);
            string fileType;
            string fileName;
            var file = repo.GetCorespondenceFilePreview(id, reportPath, out fileType, out fileName);
            return File(file, fileType, fileName);
        }
        [HttpPost]
        public virtual ActionResult ConfirmConclusion(string modelId, string category, bool isAccept, string comment, string culture)
        {
            var statusName = new SafetyreportRepository().ConfirmConclusion(modelId, category, isAccept, comment, culture);
            return Json(new { Success = true, statusName });
        }
        public ActionResult DublcateDoage(string modelId, long dosageId, string userId)
        {
            if (modelId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            new AppDosageRepository().DublcateDoage(modelId, dosageId, userId);
            var model = GetDrugDeclarationById(modelId);

            return PartialView("~/Views/DrugDeclaration/DosageView.cshtml", model);
        }
        public ActionResult CreateDublicateOrg(string modelId, long orgId)
        {
            if (modelId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            new AppDosageRepository().CreateDublicateOrg(modelId, orgId);
            var model = GetDrugDeclarationById(modelId);

            return PartialView("~/Views/DrugDeclaration/OrganizationView.cshtml", model);
        }

        public ActionResult SetDrugReestr(string modelId, long dosageId, int? reestrId)
        {
            if (modelId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            new AppDosageRepository().SetDrugReestr(new Guid(modelId), dosageId, reestrId);
            var model = GetDrugDeclarationById(modelId);

            return PartialView("~/Views/DrugDeclaration/DeclarationView.cshtml", model);
        }
        public ActionResult RemoveDrugReestr(string modelId, long dosageId)
        {
            if (modelId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            new AppDosageRepository().RemoveDrugReestr(dosageId);
            var model = GetDrugDeclarationById(modelId);

            return PartialView("~/Views/DrugDeclaration/DeclarationView.cshtml", model);
        }

        
    }
  

}