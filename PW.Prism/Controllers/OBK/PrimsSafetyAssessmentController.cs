using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Controller;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.OBK;
using PW.Prism.ViewModels.OBK;

namespace PW.Prism.Controllers.OBK
{
    public abstract class PrimsSafetyAssessmentController : ComAssessmentController
    {
        // GET: PrimsSafetyAssessment
        public virtual ActionResult Index(int type)
        {
            var model = new OBKEntity
            {
                Guid = Guid.NewGuid(),
                DicStageId = type
            };
            return PartialView("~/Views/SafetyAssessment/Index.cshtml", model);
        }

        //public int GetStage()
        //{
        //    return CodeConstManager.STAGE_OBK_COZ;
        //}
        /// <summary>
        /// выбор заявления
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = GetAssessmentStage(id);
            FillDeclarationControl(model.OBK_AssessmentDeclaration);
            return PartialView("~/Views/SafetyAssessment/Edit.cshtml", model);
        }

        protected virtual OBK_AssessmentStage GetAssessmentStage(Guid? id)
        {
            return new AssessmentStageRepository().GetById(id);
        }
        /// <summary>
        /// получение заявления
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual OBK_AssessmentDeclaration GetAssessmentDeclaration(string id)
        {
            return new SafetyAssessmentRepository().GetById(id);
        }

        /// <summary>
        /// подгузка данных
        /// </summary>
        /// <param name="model"></param>
        public void FillDeclarationControl(OBK_AssessmentDeclaration model)
        {
            var safetyRepository = new SafetyAssessmentRepository();
            ViewData["ContractList"] =
                new SelectList(safetyRepository.GetActiveContractListWithInfo(model.EmployeeId, model.TypeId), "Id",
                    "ContractInfo", model.ContractId);
            
            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_SERIAL))
            {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu",
                    model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_SERIAL).Id);
            }
            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_PARTY))
            {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu",
                    model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_PARTY).Id);
            }
            if (model.TypeId == int.Parse(CodeConstManager.OBK_SA_DECLARATION))
            {
                ViewData["TypeList"] = new SelectList(safetyRepository.GetObkRefTypes(), "Id", "NameRu",
                    model.TypeId = safetyRepository.GetObkRefTypes(CodeConstManager.OBK_SA_DECLARATION).Id);
            }

            if (model.ContractId != null)
            {
                var contract = safetyRepository.GetContractById(model.ContractId);
                var declarant = safetyRepository.GetDeclarantById(contract.DeclarantId);
                var declarantContact = safetyRepository.GetDeclarantContactById(contract.DeclarantContactId);
                var products = safetyRepository.GetRsProductsAndSeries(contract.Id);

                //справочник стран
                var countries = safetyRepository.GetCounties();
                if (declarant.CountryId == null)
                {
                    ViewData["Counties"] = new SelectList(countries, "Id", "Name");
                }
                else
                {
                    ViewData["Counties"] = new SelectList(countries, "Id", "Name",
                        model.CountryId = declarant.CountryId);
                }

                //Валюта
                var currency = safetyRepository.GetObkCurrencies();
                if (declarantContact.CurrencyId == null)
                {
                    ViewData["Courrency"] = new SelectList(currency, "Id", "Name");
                }
                else
                {
                    ViewData["Courrency"] = new SelectList(currency, "Id", "Name",
                        model.CurrencyId = declarantContact.CurrencyId);
                }

                //Наличие сертификата GMP
                var repository = new ReadOnlyDictionaryRepository();
                var booleans = repository.GetCertificateGMPCheck();
                ViewData["IsGMPList"] = new SelectList(booleans, "CertificateGMPCheck", "NameRu",
                    model.CertificateGMPCheck);

                //организационная форма
                var orgForm = safetyRepository.GetOrganizationForm();
                if (declarant.OrganizationFormId == null)
                {
                    ViewData["OrganizationForm"] = new SelectList(orgForm, "Id", "Name");
                }
                else
                {
                    ViewData["OrganizationForm"] = new SelectList(orgForm, "Id", "Name",
                        model.OrganizationFormId = declarant.OrganizationFormId);
                }

                var certType = safetyRepository.GetCertificateType();
                if (model.CertificateTypeId == null)
                {
                    ViewData["CertificateType"] = new SelectList(certType, "Id", "NameRu");
                }
                else
                {
                    ViewData["CertificateType"] = new SelectList(certType, "Id", "NameRu", model.CertificateTypeId);
                }

                model.StartDate = string.Format("{0:dd.MM.yyyy}", contract.StartDate);
                model.EndDate = string.Format("{0:dd.MM.yyyy}", contract.EndDate);
                model.NameKz = declarant?.NameKz ?? "нет данных";
                model.NameRu = declarant?.NameRu ?? "нет данных";
                model.NameEn = declarant?.NameEn ?? "нет данных";
                model.ChiefLastName = declarantContact?.BossLastName ?? "нет данных";
                model.ChiefFirstName = declarantContact?.BossFirstName ?? "нет данных";
                model.ChiefMiddleName = declarantContact?.BossMiddleName ?? "нет данных";
                model.ChiefPosition = declarantContact?.BossPosition ?? "нет данных";
                model.AddressFact = declarantContact?.AddressFact ?? "нет данных";
                model.AddressLegal = declarantContact?.AddressLegalRu ?? "нет данных";
                model.Phone = declarantContact?.Phone ?? "нет данных";
                model.Email = declarantContact?.Email ?? "нет данных";
                model.BankBik = declarantContact?.BankBik ?? "нет данных";
                model.BankIik = declarantContact?.BankIik ?? "нет данных";
                model.BankName = declarantContact?.BankNameRu ?? "нет данных";
                model.ObkContracts = contract;
                model.ObkContracts.ObkRsProductCount = products.Count();

                var resultProducts = new List<OBK_RS_Products>();
                foreach (var product in products)
                {
                    var prod = new OBK_RS_Products();
                    prod.Id = product.Id;
                    prod.NameRu = product.NameRu;
                    prod.NameKz = product.NameKz;
                    prod.ProducerNameRu = product.ProducerNameRu;
                    prod.ProducerNameKz = product.ProducerNameKz;
                    prod.CountryNameRu = product.CountryNameRu;
                    prod.CountryNameKZ = product.CountryNameKZ;
                    prod.TnvedCode = product.TnvedCode;
                    prod.KpvedCode = product.KpvedCode;
                    prod.Price = product.Price;
                    foreach (var productSeries in product.OBK_Procunts_Series)
                    {
                        var prodSeries = new OBK_Procunts_Series();
                        prodSeries.Id = productSeries.Id;
                        prodSeries.Series = productSeries.Series;
                        prodSeries.SeriesStartdate = productSeries.SeriesStartdate;
                        prodSeries.SeriesEndDate = productSeries.SeriesEndDate;
                        prodSeries.SeriesParty = productSeries.SeriesParty;
                        prodSeries.SeriesShortNameRu = productSeries.sr_measures.short_name;
                        prod.OBK_Procunts_Series.Add(prodSeries);
                    }
                    resultProducts.Add(prod);
                }
                model.ObkRsProductses = resultProducts;
            }
            else
            {
                var countries = safetyRepository.GetCounties();
                ViewData["Counties"] = new SelectList(countries, "Id", "Name");

                //Валюта
                var currency = safetyRepository.GetObkCurrencies();
                ViewData["Courrency"] = new SelectList(currency, "Id", "Name");

                var repository = new ReadOnlyDictionaryRepository();
                //Наличие сертификата GMP
                var booleans = repository.GetCertificateGMPCheck();
                ViewData["IsGMPList"] = new SelectList(booleans, "CertificateGMPCheck", "NameRu",
                    model.CertificateGMPCheck);

                var certType = safetyRepository.GetCertificateType();
                ViewData["CertificateType"] = new SelectList(certType, "Id", "NameRu");

                //организационная форма
                var orgForm = safetyRepository.GetOrganizationForm();
                ViewData["OrganizationForm"] = new SelectList(orgForm, "Id", "Name");
            }
        }
    }
}