using System;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Teme.Contracts;
using Ncels.Teme.Contracts.ViewModels;
using Ncels.Teme.Infrastructure;

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
            _service.RegisterContract(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}