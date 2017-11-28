using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.EMP;
using PW.Ncels.Database.Repository.EMP;

namespace PW.Ncels.Controllers
{
    [Authorize]
    public class EMPContractController : Controller
    {
        EMPContractRepository emp = new EMPContractRepository();

        // GET: EMPContract
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contract()
        {
            ViewBag.ListAction = "Index";
            return View();
        }

        [HttpGet]
        public ActionResult GetHolderTypes()
        {
            var result = emp.GetHolderTypes();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetExpertOrganizations()
        {
            var expertOrganizations = emp.GetExpertOrganizations();
            return Json(expertOrganizations.ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetBanks()
        {
            var result = emp.GetBanks();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveNewBank(string bankNameRu, string bankNameKz)
        {
            var result = emp.SaveNewBank(bankNameRu, bankNameKz);
            return Json(result);
        }
        [HttpGet]
        public ActionResult GetServiceType()
        {
            var result = emp.GetServiceType();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetServiceTypeParentId(string id)
        {
            var result = emp.GetServiceTypeParentId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetPriceType(string id)
        {
            var result = emp.GetPriceTypeWhithPriceList(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetCalculation(string serviceTypeId, bool isImport, int count)
        {
            var result = emp.GetPriceList(Guid.Parse(serviceTypeId), isImport, count);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult FindDeclarant(string bin)
        {
            if (bin != null && bin.Length == 12)
            {
                var organization = emp.FindOrganization(bin);
                if (organization != null)
                {
                    var declarantJson = GetDeclarantJson(organization);
                    return Json(declarantJson, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        private object GetDeclarantJson(OBK_Declarant organization)
        {
            if (organization != null)
            {
                var declarantContact = organization.OBK_DeclarantContact.OrderByDescending(x => x.CreateDate).FirstOrDefault();
                if (declarantContact != null)
                {
                    var declarantJson = new
                    {
                        organization.Id,
                        organization.OrganizationFormId,
                        organization.IsResident,
                        organization.NameKz,
                        organization.NameRu,
                        organization.NameEn,
                        organization.CountryId,
                        declarantContact.AddressLegalRu,
                        declarantContact.AddressLegalKz,
                        declarantContact.AddressFact,
                        declarantContact.Phone,
                        declarantContact.Email,
                        declarantContact.BossLastName,
                        declarantContact.BossFirstName,
                        declarantContact.BossMiddleName,
                        declarantContact.BossPosition,
                        declarantContact.BossPositionKz,
                        declarantContact.BossDocType,
                        declarantContact.IsHasBossDocNumber,
                        declarantContact.BossDocNumber,
                        declarantContact.BossDocCreatedDate,
                        declarantContact.BossDocEndDate,
                        declarantContact.BossDocUnlimited,
                        declarantContact.SignerIsBoss,
                        declarantContact.SignLastName,
                        declarantContact.SignFirstName,
                        declarantContact.SignMiddleName,
                        declarantContact.SignPosition,
                        declarantContact.SignPositionKz,
                        declarantContact.SignDocType,
                        declarantContact.IsHasSignDocNumber,
                        declarantContact.SignDocNumber,
                        declarantContact.SignDocCreatedDate,
                        declarantContact.SignDocEndDate,
                        declarantContact.SignDocUnlimited,
                        declarantContact.BankIik,
                        declarantContact.BankBik,
                        declarantContact.CurrencyId,
                        declarantContact.BankNameRu,
                        declarantContact.BankNameKz
                    };
                    return declarantJson;
                }
                else
                {
                    var declarantJson = new
                    {
                        organization.Id,
                        organization.OrganizationFormId,
                        organization.IsResident,
                        organization.NameKz,
                        organization.NameRu,
                        organization.NameEn,
                        organization.CountryId
                    };
                    return declarantJson;
                }
            }
            return null;
        }


        public ActionResult ContractSave(Guid Guid, EMPContractViewModel contractViewModel)
        {
            EMPContractViewModel savedContract = emp.SaveContract(Guid, contractViewModel);
            return Json(savedContract);
        }

        public ActionResult GetContractPriceList(Guid modelGuid)
        {
            object result = new object();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}