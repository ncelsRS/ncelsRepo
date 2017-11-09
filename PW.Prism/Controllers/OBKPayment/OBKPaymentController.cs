using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Helpers;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.OBK;
using PW.Ncels.Database.Repository.DirectionToPay;
using PW.Ncels.Database.Repository.OBK;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;
using PW.Ncels.Database.Constants;

namespace PW.Prism.Controllers.OBKPayment
{
    public class OBKPaymentController : Controller
    {
        OBKPaymentRepository payRepo = new OBKPaymentRepository();

        // GET: OBKPayment
        public ActionResult Index()
        {
            var model = new OBKEntityPayment
            {
                Id = Guid.NewGuid(),
                EmployeeId = UserHelper.GetCurrentEmployee().Id
            };
            return PartialView(model);
        }

        public JsonResult ReadDirectionList([DataSourceRequest] DataSourceRequest request)
        {
            var organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var data = payRepo.GetDirectionToPaymentsList(organizationId);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(Guid contractId)
        {
            var model = payRepo.GetContract(contractId);
            return PartialView(model);
        }

        public ActionResult SendToDeclarant(Guid id)
        {
            payRepo.SendInvoiceToDeclarant(id);
            return Json(new { IsSuccess = true });
        }

        public ActionResult GetContractPrice(Guid id)
        {
            var model = payRepo.GetContract(id);
            List<ContractPrice> result = new List<ContractPrice>();
            int i = 1;
            foreach (var com in model.OBK_ContractPrice)
            {
                var cPrice = new ContractPrice();
                cPrice.Line = i;
                cPrice.Count = com.Count;
                cPrice.PriceWithTax = Math.Round(TaxHelper.GetCalculationTax(com.OBK_Ref_PriceList.Price), 2) ;
                cPrice.Price = Math.Round(com.Count * TaxHelper.GetCalculationTax(com.OBK_Ref_PriceList.Price), 2);
                cPrice.ServiceName = com.OBK_Ref_PriceList.NameRu;
                cPrice.ProductName = com.OBK_RS_Products.NameRu;
                result.Add(cPrice);
                i++;
            }
            return Json(new { isSuccess = true, result });
        }

        [HttpGet]
        public ActionResult GetSignDirectionToPayment(Guid id)
        {
            var signData = payRepo.GetSignData(id);
            return Json(new { data = signData }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSignedPayment(Guid paymentId, string signedData)
        {
            var message = payRepo.SaveSignPay(paymentId, signedData);
            return Json(new {message});
        }

        /// <summary>
        /// назначить руководителя
        /// </summary>
        /// <param name="id">список заявлении</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetChiefAccountant()
        {
            return PartialView(Guid.NewGuid());
        }

        [HttpPost]
        public ActionResult SetChiefAccountant(Guid executors, Guid id)
        {
            payRepo.SendToChief(executors, id);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult DocumentExportFilePdf(string contractId)
        {
            string name = "Счет на оплату.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/Mrts/OBK/1c/ObkInvoicePayment.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["ContractId"].ValueObject = contractId;
                //итого
                var totalPriceWithCount = payRepo.GetTotalPriceCount(Guid.Parse(contractId));
                report.Dictionary.Variables["TotalPriceWithCount"].ValueObject = totalPriceWithCount;
                //в том числе НДС
                var totalPriceNDS = payRepo.GetTotalPriceNDS(totalPriceWithCount);
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
            var directionToPayment = payRepo.GetDirectionToPayments(Guid.Parse(contractId));
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
                    return File(pdfFile, "application/pdf", name);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }

        public class ContractPrice
        {
            public int Line { get; set; }
            public string ServiceName { get; set; }
            public string ProductName { get; set; }
            public int Count { get; set; }
            public double PriceWithTax { get; set; }
            public double Price { get; set; }
        }
    }
}