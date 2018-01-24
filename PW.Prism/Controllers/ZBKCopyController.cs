using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Antlr.Runtime;
using Aspose.Pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using Document = PW.Ncels.Database.DataModel.Document;
using PW.Prism.ViewModels.OBK;
using PW.Ncels.Database.Repository.OBK;
using PW.Ncels.Database.Notifications;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using System.IO;
using System.Data.SqlTypes;

namespace PW.Prism.Controllers
{
    [Authorize]
    public class ZBKCopyController : Controller
    {
        private NcelsEntities db = UserHelper.GetCn();
        private ZBKCopyRepository repository = new ZBKCopyRepository();

        // GET: /Reference/
        public ActionResult Index(int type)
        {
            var model = new OBKEntity
            {
                Guid = Guid.NewGuid(),
                DicStageId = type
            };
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult SendToPayment(Guid id)
        {
            repository.SendToPayment(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult InitBlankNumber(Guid zbkCopyId)
        {
            var blank = repository.InitBlankNumber(zbkCopyId);
            if (blank == null)
            {
                return Json(new { success = false });
            }

            return Json(new
            {
                success = true,
                result = new
                {
                    StartNumber = repository.FormatBlankNumber(blank.StartNumber),
                    EndPrimeNumber = repository.FormatBlankNumber(blank.EndPrimeNumber),
                    StartApplicationNumber = repository.FormatBlankNumber(blank.StartApplicationNumber),
                    EndApplicationNumber = repository.FormatBlankNumber(blank.EndApplicationNumber)
                }
            });
        }

        [HttpPost]
        public ActionResult GetCorruptedBlanks(Guid zbkCopyId)
        {
            return Json(new { success = true, result = repository.GetReplacedBlanks(zbkCopyId) });
        }

        [HttpPost]
        public ActionResult ZBKTransferRegisterList([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.ZBKTransferRegister();
            return Json(data.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult ZBKTransferRegister()
        {
            return PartialView("~/Views/ZBKCopy/ZBKTransferRegister.cshtml", Guid.NewGuid());
        }

        [HttpGet]
        public ActionResult ZBKRegister()
        {
            return PartialView("~/Views/ZBKCopy/ZBKRegister.cshtml", Guid.NewGuid());
        }

        [HttpPost]
        public ActionResult ZBKRegisterList([DataSourceRequest] DataSourceRequest request)
        {
            var data = repository.ZBKRegister();
            return Json(data.ToDataSourceResult(request));
        }

        public FileStreamResult ExportFile(DateTime? from, DateTime? to)
        {
            StiReport report = new StiReport();
            report.Load(Server.MapPath("../Reports/Mrts/OBK/ZBKList.mrt"));
            var currentEmployee = UserHelper.GetCurrentEmployee();
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }

            if (from == null)
            { report.Dictionary.Variables["fromPeriod"].ValueObject = (DateTime)SqlDateTime.MinValue; }
            else { report.Dictionary.Variables["fromPeriod"].ValueObject = from; }

            if (to == null)
            { report.Dictionary.Variables["toPeriod"].ValueObject = (DateTime)SqlDateTime.MaxValue; }
            else { report.Dictionary.Variables["toPeriod"].ValueObject = to; }

            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;
            return File(stream, "application/excel", "СписокЗБК.xls");
        }

        [HttpPost]
        public ActionResult SaveReceiver(string receiver, DateTime? receiveDate, Guid zbkCopyId, bool zbkCopiesReady)
        {
            return Json(new
            {
                success =
            repository.SaveReceiver(receiver, receiveDate, zbkCopyId, zbkCopiesReady)
            });
        }

        public ActionResult SaveStartBlankNumber(int startNumber, Guid zbkCopyId, bool expApplication)
        {
            var blank = repository.SaveStartBlankNumber(startNumber, zbkCopyId, expApplication);

            return Json(new
            {
                success = true,
                result = new
                {
                    StartNumber = blank.StartNumber,
                    EndPrimeNumber = blank.EndPrimeNumber,
                    StartApplicationNumber = blank.StartApplicationNumber,
                    EndApplicationNumber = blank.EndApplicationNumber
                }
            });
        }

        [HttpPost]
        public ActionResult ReplaceBlank(int blankForReplace, int newBlank, Guid zbkCopyId)
        {
            var response = repository.ReplaceBlank(blankForReplace, newBlank, zbkCopyId);

            if (response.response == false)
            {
                return Json(new { success = false, message = response.message });
            }

            return Json(new { success = true, message = response.message, result = repository.GetReplacedBlanks(zbkCopyId) });

        }

        [HttpPost]
        public ActionResult SendToOBK(Guid id)
        {
            repository.SendToOBK(id);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult GetZBKCopyCost(Guid id)
        {
            return Json(new { isSuccess = true, result = repository.GetZBKCopyCost(id) });
        }

        /// <summary>
        /// назначить исполнителя
        /// </summary>
        /// <param name="id">список заявлении</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetExecuter()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult Products(Guid contractId)
        {
            return Json(new { success = true, result = repository.Products(contractId) });
        }

        [HttpPost]
        public ActionResult SetExecuter(Guid[] stages, Guid[] executors)
        {
            repository.SendToWork(stages, executors);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetStageSign(Guid zbkCopyStageId)
        {
            return Json(new
            {
                IsSuccess = true,
                preambleXml = repository.GetStageSign(zbkCopyStageId)
            });
        }

        [HttpPost]
        public ActionResult SaveSignedZbkCopyStage(Guid id, string signedData)
        {
            var message = repository.SaveSignedZbkCopyStage(id, signedData);
            return Json(new { success = true, message = message });
        }


        public ActionResult SendNote(Guid zbkCopyId, string notes)
        {
            var zbkCopy = db.OBK_ZBKCopy.FirstOrDefault(o => o.Id == zbkCopyId);
            zbkCopy.Notes = notes;
            zbkCopy.StatusId = CodeConstManager.STATUS_OBK_ONCORRECTION;

            var zbkCopyStage = db.OBK_ZBKCopyStage.FirstOrDefault(o => o.OBK_ZBKCopyId == zbkCopyId && o.StageId == 1);
            var stageStatus = db.OBK_Ref_StageStatus.FirstOrDefault(o => OBK_Ref_StageStatus.OnCorrection.Equals(o.Code));
            zbkCopyStage.StageStatusId = stageStatus.Id;

            db.SaveChanges();

            var employee = db.Employees.FirstOrDefault(o => o.Id == zbkCopy.EmployeeId);

            NotificationManager notification = new NotificationManager();

            var text = "Уведомляем Вас о том, что в запросе ЗБК были найдены ошибки.";
            notification.SendNotificationFromCompany(text, ObjectType.OBK_ZBKCopy, zbkCopy.Id.ToString(), employee.Id);

            return Json(new { success = true });
        }

        public ActionResult DocumentRead(string AttachPath)
        {
            OBKCertificateFileModel fileModel = new OBKCertificateFileModel();

            fileModel.AttachPath = AttachPath;
            fileModel.AttachFiles = UploadHelper.GetFilesInfo(AttachPath, false);

            return Content(JsonConvert.SerializeObject(fileModel, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
        }

        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = repository.GetZBKViewModel(id);

            var stage = db.OBK_ZBKCopyStage.FirstOrDefault(o => o.Id == id);
            var zbkCopy = db.OBK_ZBKCopy.FirstOrDefault(o => o.Id == stage.OBK_ZBKCopyId);
            var payment = db.OBK_DirectionToPayments.FirstOrDefault(o => o.ZBKCopy_id == zbkCopy.Id);
            if (payment != null && payment.IsPaid)
            {
                ViewData["ShowOriginals"] = true;
            }

            return PartialView(model);
        }

        public virtual ActionResult EditBlanks(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = repository.GetZBKViewModel(id);

            var stage = db.OBK_ZBKCopyStage.FirstOrDefault(o => o.Id == id);
            var zbkCopy = db.OBK_ZBKCopy.FirstOrDefault(o => o.Id == stage.OBK_ZBKCopyId);
            var payment = db.OBK_DirectionToPayments.FirstOrDefault(o => o.ZBKCopy_id == zbkCopy.Id);
            if (payment != null && payment.IsPaid)
            {
                ViewData["ShowOriginals"] = true;
            }

            return PartialView(model);
        }

        public ActionResult SaveOriginals(Guid zbkCopyId)
        {
            return Json(new { success = repository.SaveOriginals(zbkCopyId) });
        }

        public ActionResult ListZBKCopies([DataSourceRequest] DataSourceRequest request, int stage)
        {
            var data = repository.ListZBKCopies(stage).ToDataSourceResult(request);
            return Json(data);
        }

        public ActionResult ConfirmPaperCopy(Guid zbkCopyId)
        {
            return Json(new { success = repository.ConfirmPaperCopy(zbkCopyId) });
        }
    }
}
