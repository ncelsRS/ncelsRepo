using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Teme.Contracts;
using Ncels.Teme.Contracts.ViewModels;
using Ncels.Teme.Infrastructure;
using PW.Ncels.Database.Helpers;
using Stimulsoft.Report;

namespace PW.Prism.Controllers.EMP
{
    public class EMPContractController : Controller
    {
        private IEmpContractService _service;

        public EMPContractController()
        {
            // TODO: потом реализовать через DI
            _service = new EmpContractService();
        }

        // GET: EMPContract
        public ActionResult Index()
        {
            return PartialView(new EmpContractIndexViewModel
            {
                Id = Guid.NewGuid(),
                StageCode = _service.GetStageCode()
            });
        }

        public ActionResult PaymentsIndex()
        {
            return PartialView(new EmpContractPaymentIndexViewModel
            {
                Id = Guid.NewGuid(),
                EmployeeId = UserHelper.GetCurrentEmployee().Id
            });
        }

        public ActionResult Edit(Guid? id, Guid? stage)
        {
            if (id == null || stage == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = _service.GetContractDetailsViewModel(id.Value);
            model.StageId = stage.Value;
            model.CanApprove = _service.CanApprove(model.StageId);

            return PartialView("ContractDetails", model);
        }

        public ActionResult LoadContracts([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_service.GetContracts().ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult SetExecutor()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult SetExecutor(Guid executorId, Guid stageId)
        {
            _service.SendToWork(stageId, executorId);
            return Json("OK");
        }

        public ActionResult GetContractHistory([DataSourceRequest] DataSourceRequest request, Guid id)
        {
            return Json(_service.GetContractHistory(id).ToDataSourceResult(request));
        }

        public ActionResult Approve(Guid stage)
        {
            _service.Approve(stage, true);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult Reject(Guid stage)
        {
            _service.Approve(stage, false);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult SendToAdjustment(Guid stage)
        {
            _service.SendToAdjustment(stage);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult ContractRegister(Guid id)
        {
            var contractNumber = _service.RegisterContract(id);
            return Json(new {contractNumber});
        }

        public ActionResult GetPayments([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_service.GetPayments().ToDataSourceResult(request));
        }

        public ActionResult EditPayment(Guid contractId)
        {
            return PartialView(_service.GetPayment(contractId));
        }

        public ActionResult GetContractPrice(Guid id)
        {
            return Json(new { isSuccess = true, result = _service.GetContractPrice(id) });
        }

        public ActionResult GetContractTemplatePdf(Guid id)
        {
            var report = new StiReport();
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Mrts/EMP/EMP_ContractReport.mrt");
            report.Load(path);
            report.Compile();
            report.RegBusinessObject("vm", _service.GetContractTemplatePdf(id));
            report.Render(false);
            Stream stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}