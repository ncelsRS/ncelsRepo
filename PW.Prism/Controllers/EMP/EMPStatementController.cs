using System;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Teme.Contracts;
using Ncels.Teme.Infrastructure;

namespace PW.Prism.Controllers.EMP
{
    public class EMPStatementController : Controller
    {
        private IEmpStatementService _service;

        public EMPStatementController()
        {
            _service = new EmpStatementService();
        }

        // GET: EMPStatement
        public ActionResult Index()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult LoadStatements([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_service.GetStatements().ToDataSourceResult(request));
        }
    }
}