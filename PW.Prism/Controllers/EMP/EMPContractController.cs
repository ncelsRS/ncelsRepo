using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Ncels.Teme.Contracts;
using Ncels.Teme.Contracts.ViewModels;
using Ncels.Teme.Infrastructure;
using PW.Ncels.Database.Helpers;

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
            return PartialView(new EmpContractIndexViewModel());
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            
            var model = _service.GetContractDetailsViewModel(id.Value);


            return PartialView("ContractDetails", model);
        }

        public ActionResult LoadContracts([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_service.GetContracts().ToDataSourceResult(request));
        }
    }
}