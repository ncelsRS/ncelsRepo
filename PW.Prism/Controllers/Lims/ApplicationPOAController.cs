using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.Repository.Lims;

namespace PW.Prism.Controllers.Lims
{
    public class ApplicationPOAController : Controller
    {
        public JsonResult GetContractNumbers(string text)
        {
            ApplicationPoaRepository repository = new ApplicationPoaRepository();

            var listContractNumers = repository.GetI1cLimsContracts(c => c.ContractNumber.Contains(text)).ToList();

            return Json(listContractNumers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetContractData(string contractNumber)
        {
            ApplicationPoaRepository repository = new ApplicationPoaRepository();

            var contractObj = repository.GetI1cLimsContracts(c => c.ContractNumber == contractNumber).FirstOrDefault();

            return Json(contractObj, JsonRequestBehavior.AllowGet);
        }
    }
}