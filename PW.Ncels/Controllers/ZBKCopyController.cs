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
        private OBKPaymentRepository payRepo = new OBKPaymentRepository();
        private ncelsEntities db = UserHelper.GetCn();

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
            ViewData["ContractId"] = repository.ContractId(ZBKCopyId);
            return View(model);
        }

        public ActionResult CertOfComplectFilePdf(Guid zbkCopyId)
        {
            OBKExpDocumentRepository expRepo = new OBKExpDocumentRepository();
            string name = "Акт выполненных работ.pdf";

            var copy = db.OBK_ZBKCopy.FirstOrDefault(o => o.Id == zbkCopyId);
            var expDocument =  db.OBK_StageExpDocument.FirstOrDefault(o => o.Id == copy.OBK_StageExpDocumentId);

            StiReport report = new StiReport();
            try
            {
                if (copy.ExpApplication == false)
                {
                    report.Load(Server.MapPath("~/Reports/Mrts/OBK/1c/ObkCertificateOfCompletionCopyZbkApplication.mrt"));
                }
                else
                {
                    report.Load(Server.MapPath("~/Reports/Mrts/OBK/1c/ObkCertificateOfCompletionCopyZbk.mrt"));
                }
               
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["AssessmentDeclarationId"].ValueObject = expDocument.AssessmentDeclarationId;
                report.Dictionary.Variables["ContractId"].ValueObject = expRepo.GetAssessmentDeclaration((Guid)expDocument.AssessmentDeclarationId).ContractId;
                var totalCount = payRepo.GetTotalPriceZbkCopy(zbkCopyId);
                report.Dictionary.Variables["TotalCount"].ValueObject = totalCount;
                var priceText = RuDateAndMoneyConverter.CurrencyToTxtTenge(Convert.ToDouble(totalCount), false);
                report.Dictionary.Variables["TotalCountText"].ValueObject = priceText;
                report.Dictionary.Variables["ZBKCopyId"].ValueObject = copy.Id;
                report.Dictionary.Variables["PriceNds"].ValueObject = repository.GetZbkCopyNds(zbkCopyId);

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
            //return File(stream, "application/pdf", name);
        }

        public ActionResult Update(Guid Id, int quantity, DateTime letterDate, string letterNumber)
        {
            return Json(new { success = repository.Update(Id, quantity, letterDate, letterNumber) });
        }

        public ActionResult DocumentRead(string AttachPath)
        {
            OBKCertificateFileModel fileModel = new OBKCertificateFileModel();

            fileModel.AttachPath = AttachPath;
            fileModel.AttachFiles = UploadHelper.GetFilesInfo(AttachPath, false);

            return Content(JsonConvert.SerializeObject(fileModel, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
        }

        public ActionResult DocumentZbkCopyPdf(string zbkCopyId, string contractId)
        {
            string name = "Счет на оплату.pdf";
            StiReport report = new StiReport();
            try
            {
                var copyZBK = payRepo.GetZBKCopy(Guid.Parse(zbkCopyId));

                if (copyZBK.ExpApplication == false)
                {
                    report.Load(Server.MapPath("~/Reports/Mrts/OBK/1c/ObkInvoicePaymentCopyZbkApplication.mrt"));
                }
                else
                {
                    report.Load(Server.MapPath("~/Reports/Mrts/OBK/1c/ObkInvoicePaymentCopyZbk.mrt"));
                }
               
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["ContractId"].ValueObject = contractId;
                //итого
                report.Dictionary.Variables["ZBKCopyId"].ValueObject = zbkCopyId;
                var totalPriceWithCount = payRepo.GetTotalPriceZbkCopy(Guid.Parse(zbkCopyId));
                report.Dictionary.Variables["TotalPriceWithCount"].ValueObject = totalPriceWithCount;
                //в том числе НДС
                var totalPriceNDS = payRepo.GetTotalPriceNDS(payRepo.GetZbkCopyNds(Guid.Parse(zbkCopyId)));
                report.Dictionary.Variables["TotalPriceNDS"].ValueObject = totalPriceNDS;
                //прописью
                var priceText = RuDateAndMoneyConverter.CurrencyToTxtTenge(Convert.ToDouble(totalPriceWithCount), false);
                report.Dictionary.Variables["TotalPriceWithCountName"].ValueObject = priceText;
                report.Dictionary.Variables["ChiefAccountant"].ValueObject = null; //payRepo.GetEmpoloyee(Guid.Parse("E1EE3658-0C35-41EB-99FD-FDDC4D07CEC4"))?.ShortName;
                report.Dictionary.Variables["Executor"].ValueObject = null; //payRepo.GetEmpoloyee(Guid.Parse("55377FAC-A5F0-4093-BBB6-18BD28E53BE1"))?.ShortName;
                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            var stream = new MemoryStream();
            var directionToPayment = payRepo.GetDirectionToPaymentsByCopyZbk(Guid.Parse(zbkCopyId));
            var signPayment = payRepo.GetDirectionSignData(directionToPayment.Id);
            if (signPayment.ChiefAccountantSign != null && signPayment.ExecutorSign != null)
            {
                try
                {
                    report.ExportDocument(StiExportFormat.Word2007, stream);
                    stream.Position = 0;
                    Aspose.Words.Document doc = new Aspose.Words.Document(stream);
                    doc.InserQrCodesToEnd("ChiefAccountantSign", signPayment.ChiefAccountantSign);
                    var pdfFile = new MemoryStream();
                    doc.Save(pdfFile, Aspose.Words.SaveFormat.Pdf);
                    pdfFile.Position = 0;
                    stream.Close();
                    return new FileStreamResult(pdfFile, "application/pdf");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
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