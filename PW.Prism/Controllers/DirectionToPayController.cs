using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Core.ActivityManager;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.DirectionToPay;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Repository.Contract;
using PW.Ncels.Database.Repository.DirectionToPay;
using PW.Ncels.Database.Repository.Expertise;

namespace PW.Prism.Controllers
{
    public class DirectionToPayController : Controller
    {
        //DirectionToPayRepository _repository = new DirectionToPayRepository();

        // GET: DirectionToPay
        public ActionResult Index()
        {
            var guid = Guid.NewGuid();
            DirectionToPayRepository repository = new DirectionToPayRepository();

            ViewBag.DirectionToPayStatuses =
                repository.GetDirectionToPayStatuses().ToList().OrderBy(o => o.Name)
                    .Select(o => new Item() {Id = o.Id.ToString(), Name = o.Name}).ToList();

            return PartialView(guid);
        }

        public JsonResult ReadDirectionList([DataSourceRequest] DataSourceRequest request)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository();
            var currentUserId = UserHelper.GetCurrentEmployee().Id;

            var data = repository.GetDirectionToPaysViews (
                    d => d.CreateEmployeeId == currentUserId || d.ExecutorId == currentUserId);
            //var list = data.ToList();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateDirectionForm()
        {
            var model = new EXP_DirectionToPaysView()
            {
                Id = Guid.NewGuid()
            };
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult CreateDirection(EXP_DirectionToPaysView model)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository();
            if (ModelState.IsValid)
            {
                EXP_DirectionToPays m = new EXP_DirectionToPays()
                {
                    Id = Guid.NewGuid(),
                    Number = Registrator.GetNumber("DirectionToPay").ToString(),
                    DirectionDate = model.DirectionDate,
                    Type = model.Type,
                    TypeValue = string.Empty,
                    //PayerId = ,
                    //PayerValue = ,
                    CreateEmployeeId = UserHelper.GetCurrentEmployee().Id,
                    CreateEmployeeValue = UserHelper.GetCurrentEmployee().DisplayName,
                    CreateDate = DateTime.Now
                };
                var status = repository.GetStatusByCode(Dictionary.ExpDirectionToPayStatus.Created);
                m.StatusId = status.Id;
                m.StatusValue = status.Name;

                repository.Insert(m);
                //repository.Save();
                if (model.Type == 1)
                {
                    if (model.DrugDeclarations != null)
                    {
                        foreach (var modelDrugDeclaration in model.DrugDeclarations)
                        {
                            var drugDeclaration = repository.GetDrugDeclarationById(modelDrugDeclaration.Id);
                            if (drugDeclaration != null)
                            {
                                m.EXP_DrugDeclaration.Add(drugDeclaration);
                                if (drugDeclaration.ContractId != null)
                                {
                                    var payer = repository.GetPayerByContractId(drugDeclaration.ContractId.Value);
                                    if (payer != null)
                                    {
                                        m.PayerId = payer.Id;
                                        m.PayerValue = payer.Name;
                                    }
                                }
                                m.EXP_DirectionToPays_PriceList = CreateDefaultPriceLists(drugDeclaration);
                            }
                        }
                    }

                    m.TotalPrice = 0;
                    foreach (var expDirectionToPaysPriceList in m.EXP_DirectionToPays_PriceList)
                    {
                        m.TotalPrice += expDirectionToPaysPriceList.Total;
                    }
                }
                else
                {
                    if (model.DrugDeclarations != null)
                    {
                        foreach (var modelDrugDeclaration in model.DrugDeclarations)
                        {
                            var drugDeclaration = repository.GetDrugDeclarationById(modelDrugDeclaration.Id);
                            if (drugDeclaration != null)
                            {
                                m.EXP_DrugDeclaration.Add(drugDeclaration);
                                if (drugDeclaration.ContractId != null)
                                {
                                    var payer = repository.GetPayerByContractId(drugDeclaration.ContractId.Value);
                                    if (payer != null)
                                    {
                                        m.PayerId = payer.Id;
                                        m.PayerValue = payer.Name;
                                    }
                                }
                            }
                        }
                    }

                    m.TotalPrice = model.TotalPrice;
                    m.PageCount = model.PageCount;
                    m.PriceForPage = 2277;
                }

                repository.Save();

                model.Id = m.Id;
                model.Number = m.Number;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateDirection(EXP_DirectionToPaysView model)
        {
            if (ModelState.IsValid)
            {
                DirectionToPayRepository repository = new DirectionToPayRepository();
                EXP_DirectionToPays m = repository.GetById(model.Id);

                m.Number = model.Number;
                m.DirectionDate = model.DirectionDate;
                m.Type = model.Type;
                m.TotalPrice = model.PageCount * m.PriceForPage;
                m.PageCount = model.PageCount;

                repository.Update(m);
                repository.Save();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteDirection(EXP_DirectionToPaysView model)
        {
            if (model != null)
            {
                DirectionToPayRepository repository = new DirectionToPayRepository();
                repository.Delete(model.Id);
                repository.Save();
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDrugDeclarationList([DataSourceRequest] DataSourceRequest request, int type)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository(false);

            var data = repository.GetDrugDeclarations(dd => dd.Number != null && !dd.EXP_DirectionToPays.Any(d => d.Type == type && d.DeleteDate == null));            
            if (type == EXP_DirectionToPaysView.AdditionalTranslateType)
                data = repository.GetDrugDeclarations(dd => dd.Number != null);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadDrugDeclarationListView([DataSourceRequest] DataSourceRequest request
            , Guid? directionId)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository(false);
            var data = directionId == null
                ? repository.GetDrugDeclarationViews()
                : repository.GetDrugDeclarationViews(dd => dd.DirectionToPayId == directionId.Value);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadPriceListView([DataSourceRequest] DataSourceRequest request
            , Guid? directionId)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository();
            var data = directionId == null
                ? repository.GetPriceListViews()
                : repository.GetPriceListViews(dd => dd.DirectionToPayId == directionId.Value);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadPriceList([DataSourceRequest] DataSourceRequest request)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository(false);
            var data = repository.GetPriceList();
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GridOfExpertiseMultiselect(string containerId, int type)
        {
            var dp = new DialogParameter()
            {
                Id = Guid.NewGuid(),
                ContainerId = containerId,
                Type = type
            };
            return PartialView(dp);
        }

        public ActionResult GridOfPriceListMultiselect(string containerId)
        {
            var dp = new DialogParameter()
            {
                Id = Guid.NewGuid(),
                ContainerId = containerId
            };
            return PartialView(dp);
        }

        public ActionResult GetPayerInfo(Guid directionId)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository();

            Organization payer = repository.GetPayerByDirectionId(directionId);

            return PartialView(payer);
        }

        public ActionResult GetAgreementList(Guid directionId)
        {
            ActivityManager manager = new ActivityManager();
            var tasks = manager.GetTasks(directionId, Dictionary.ExpActivityType.DirectionToPayAgrement);

            return PartialView(tasks);
        }

        [HttpGet]
        public JsonResult GetPriceList(Guid directionId)

        {
            DirectionToPayRepository repository = new DirectionToPayRepository();
            var priceListQuerable = repository.GetPriceList();
            string countryCode = string.Empty;
            List<string> countryCodes = new List<string>();
            string typeCode = string.Empty;
            //
            var expDirectionToPays = repository.Get(d => d.Id == directionId).FirstOrDefault();
            if (expDirectionToPays != null)
            {
                var drugDeclaration = expDirectionToPays.EXP_DrugDeclaration.FirstOrDefault();

                GetCountryCodeAndTypeCode(repository, drugDeclaration, out countryCode, out typeCode, ref countryCodes);
                
                /*
                // Страна
                List<string> countryCodes = new List<string>();
                if (drugDeclaration != null && drugDeclaration.EXP_DrugOrganizations != null)
                {
                    foreach (var drugOrg in drugDeclaration.EXP_DrugOrganizations)
                    {
                        if (drugOrg.CountryDicId != null)
                        {
                            var cCode = repository.GetCountryCode(drugOrg.CountryDicId.Value);
                            countryCodes.Add(cCode);
                            countryCode = cCode;
                        }
                    }
                }

                // Тип заявления
                typeCode = drugDeclaration.EXP_DIC_Type.Code;
                */
            }


            if (countryCode == CountryCodeConts.KZ)
            {
                if (typeCode == TypeCodeConts.Registration)
                {
                    return Json(priceListQuerable.Where(p => p.Category == null).Select(p => new PriceListElementModel()
                    {
                        Id = p.Id
                        , Number = p.Number
                        , Name = p.NameRu 
                        , Price = p.PriceRegisterKzNds
                    }).OrderBy(p=>p.Number).ToList(), JsonRequestBehavior.AllowGet);
                }
                else if (typeCode == TypeCodeConts.ReRegistration)
                {
                    return Json(priceListQuerable.Where(p => p.Category == null).Select(p => new PriceListElementModel()
                    {
                        Id = p.Id
                        , Number = p.Number
                        , Name = p.NameRu
                        , Price = p.PriceReRegisterKzNds
                    }).OrderBy(p => p.Number).ToList(), JsonRequestBehavior.AllowGet);
                }
                else
                { 
                    return Json(priceListQuerable.Where(p => p.Category == "Внесение изменений в ЛС").Select(p => new PriceListElementModel()
                    {
                        Id = p.Id
                        , Number = p.Number
                        , Name = p.NameRu
                        , Price = p.PriceRegisterKzNds
                    }).OrderBy(p => p.Number).ToList(), JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                if (typeCode == TypeCodeConts.Registration)
                {
                    return Json(priceListQuerable.Where(p => p.Category == null).Select(p => new PriceListElementModel()
                    {
                        Id = p.Id
                        , Number = p.Number
                        , Name = p.NameRu
                        , Price = p.PriceRegisterForeignNds
                    }).OrderBy(p => p.Number).ToList(), JsonRequestBehavior.AllowGet);
                }
                else if (typeCode == TypeCodeConts.ReRegistration)
                {
                    return Json(priceListQuerable.Where(p => p.Category == null).Select(p => new PriceListElementModel()
                    {
                        Id = p.Id
                        , Number = p.Number
                        , Name = p.NameRu
                        , Price = p.PriceReRegisterForeignNds
                    }).OrderBy(p => p.Number).ToList(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(priceListQuerable.Where(p => p.Category == "Внесение изменений в ЛС").Select(p => new PriceListElementModel()
                    {
                        Id = p.Id
                        , Number = p.Number
                        , Name = p.NameRu
                        , Price = p.PriceRegisterForeignNds
                    }).OrderBy(p => p.Number).ToList(), JsonRequestBehavior.AllowGet);

                }
            }
        }

        [HttpPost]
        public JsonResult CreateDirectionPriceList(EXP_PriceListDirectionToPayView model)
        {
            DirectionToPayRepository repository = new DirectionToPayRepository();
//            if (ModelState.IsValid)
            {
                EXP_DirectionToPays_PriceList m = new EXP_DirectionToPays_PriceList()
                {
                    DirectionToPayId = model.DirectionToPayId.Value,
                    PriceListId = model.PriceListId.Value,
                    Price = model.Price,
                    Count = model.Count,
                    Total = model.Price * model.Count
                };

                var diretionToPay = repository.GetAsQuarable(d => d.Id == model.DirectionToPayId.Value).FirstOrDefault();
                if (diretionToPay != null)
                {
                    diretionToPay.TotalPrice += m.Total;
                    repository.Update(diretionToPay);
                }

                repository.InsertDirectionPriceList(m);
                repository.Save();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateDirectionPriceList(EXP_PriceListDirectionToPayView model)
        {
            if (ModelState.IsValid)
            {
                DirectionToPayRepository repository = new DirectionToPayRepository();
                EXP_DirectionToPays_PriceList m = repository.GetDirectionPriceList(dp => dp.DirectionToPayId == model.DirectionToPayId && dp.PriceListId == model.Id).FirstOrDefault();
                if (m != null)
                {
                    m.Count = model.Count;
                    m.Price = model.Price;
                    m.Total = model.Total;


                    repository.UpdateDirectionPriceList(m);
                    repository.Save();
                }

                var diretionToPay = repository.GetAsQuarable(d => d.Id == model.DirectionToPayId.Value).FirstOrDefault();
                if (diretionToPay != null)
                {
                    diretionToPay.TotalPrice = 0;
                    foreach (var pl in diretionToPay.EXP_DirectionToPays_PriceList)
                    {
                        diretionToPay.TotalPrice += pl.Total;
                    }

                    repository.Update(diretionToPay);
                    repository.Save();
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteDirectionPriceList(EXP_PriceListDirectionToPayView model)
        {
            if (model != null)
            {
                DirectionToPayRepository repository = new DirectionToPayRepository();
                repository.DeleteDirectionPriceList(model.DirectionToPayId.Value, model.Id);
                repository.Save();

                var diretionToPay = repository.GetAsQuarable(d => d.Id == model.DirectionToPayId.Value).FirstOrDefault();
                if (diretionToPay != null)
                {
                    diretionToPay.TotalPrice = 0;
                    foreach (var pl in diretionToPay.EXP_DirectionToPays_PriceList)
                    {
                        diretionToPay.TotalPrice += pl.Total;
                    }

                    repository.Update(diretionToPay);
                    repository.Save();
                }

            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        #region BL

        private List<EXP_DirectionToPays_PriceList> CreateDefaultPriceLists(EXP_DrugDeclaration drugDeclaration)
        {
            var priceLists = new List<EXP_DirectionToPays_PriceList>();
            DirectionToPayRepository repository = new DirectionToPayRepository();
            decimal? price = 0;

            string countryCode, typeCode;
            List<string> countryCodes = new List<string>();
            
            /*
            // Страна
            
            if (drugDeclaration.EXP_DrugOrganizations != null)
            {
                foreach (var drugOrg in drugDeclaration.EXP_DrugOrganizations)
                {
                    if (drugOrg.CountryDicId != null)
                    {
                        var cCode = repository.GetCountryCode(drugOrg.CountryDicId.Value);
                        countryCodes.Add(cCode);
                        countryCode = cCode;
                    }
                }
            }

            // Тип заявления
            string typeCode = drugDeclaration.EXP_DIC_Type.Code;
            */
            GetCountryCodeAndTypeCode(repository, drugDeclaration, out countryCode, out typeCode, ref countryCodes);

            if (typeCode == TypeCodeConts.Alteration)
            {
                return priceLists;
            }

            // Тип ЛС
            var drugType = drugDeclaration.EXP_DrugType?.FirstOrDefault();
            if (drugType != null)
            {
                var drugMapping = repository.GetPriceListMappingByDrugFormCode(drugType.EXP_DIC_DrugType.Code);
                if (drugMapping != null)
                {
                    if (drugDeclaration.EXP_DrugDosage != null)
                    {
                        // Многокомпонентный или однокомпонентный
                        int activeLsCount = 0;
                        int packageCount = 0;
                        foreach (var dosage in drugDeclaration.EXP_DrugDosage)
                        {
                            activeLsCount += dosage.EXP_DrugSubstance.Count;
                            packageCount += dosage.EXP_DrugWrapping.Count;
                        }
                        // однокомпонентный
                        string priceListNumber = drugMapping.PriceListCode;
                        // Многокомпонентный
                        if (activeLsCount > 1)
                            priceListNumber = drugMapping.PriceListMulticomponentCode;

                        var priceList = repository.GetPriceList(p => p.Category == null && p.Number == priceListNumber)
                                .FirstOrDefault();
                        if (priceList != null)
                        {
                            price = priceList.GetPrice(countryCode, typeCode);

                            priceLists.Add(new EXP_DirectionToPays_PriceList()
                            {
                                PriceListId = priceList.Id,
                                Count = 1,
                                Price = price,
                                Total = 1 * price
                            });
                        }

                        // Дополнительно каждой лекарственной дозы
                        var dosageCount = drugDeclaration.EXP_DrugDosage.Count;
                        if (dosageCount > 1)
                        {
                            var priceListNumberDc = priceListNumber + ".1";
                            var priceListAddDosage =
                                repository.GetPriceList(p => p.Category == null && p.Number == priceListNumberDc)
                                    .FirstOrDefault();
                            if (priceListAddDosage != null)
                            {
                                price = priceListAddDosage.GetPrice(countryCode, typeCode);

                                priceLists.Add(new EXP_DirectionToPays_PriceList()
                                {
                                    PriceListId = priceListAddDosage.Id,
                                    Count = dosageCount,
                                    Price = price,
                                    Total = dosageCount * price
                                });
                            }
                        }

                        // Дополнительно каждой фасовки
                        if (packageCount > 1)
                        {
                            var priceListNumberDc = priceListNumber + ".2";
                            var priceListAddPackage =
                                repository.GetPriceList(p => p.Category == null && p.Number == priceListNumberDc)
                                    .FirstOrDefault();
                            if (priceListAddPackage != null)
                            {
                                price = priceListAddPackage.GetPrice(countryCode, typeCode);

                                priceLists.Add(new EXP_DirectionToPays_PriceList()
                                {
                                    PriceListId = priceListAddPackage.Id,
                                    Count = packageCount,
                                    Price = price,
                                    Total = packageCount * price
                                });
                            }
                        }
                    }
                }
            }

            return priceLists;
        }

        /// <summary>
        /// Получить код Страны и Типа заявления
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        /// <param name="drugDeclaration">ИД Заявления</param>
        /// <param name="countryCode">Код страны</param>
        /// <param name="typeCode">Код типа</param>
        /// <param name="countryCodes">Коды стран </param>
        private void GetCountryCodeAndTypeCode(DirectionToPayRepository repository, EXP_DrugDeclaration drugDeclaration, out string countryCode, out string typeCode, ref List<string> countryCodes)
        {
            // Страна
            countryCode = string.Empty;
            if (drugDeclaration.EXP_DrugOrganizations != null)
            {
                foreach (var drugOrg in drugDeclaration.EXP_DrugOrganizations)
                {
                    var isManufacturer = repository.GetOrgManufactureTypes().Any(mt => mt.Id == drugOrg.OrgManufactureTypeDicId
                    && mt.Code == Dictionary.OrgManufactureType.Manufacturer);
                    if (drugOrg.CountryDicId != null && isManufacturer)
                    {
                        var cCode = repository.GetCountryCode(drugOrg.CountryDicId.Value);
                        countryCodes.Add(cCode);
                        countryCode = cCode;
                    }
                }
            }

            // Тип заявления
            typeCode = drugDeclaration.EXP_DIC_Type.Code;
        }

        #endregion
    }
}