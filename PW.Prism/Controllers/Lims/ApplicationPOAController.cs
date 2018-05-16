using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.Lims;
using PW.Prism.Controllers.Base;

namespace PW.Prism.Controllers.Lims
{
    public class ApplicationPOAController : LimsBaseController
    {
        public static class ApplicationListType
        {
            public static readonly int PowerOfA = 1;
        }


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

        public PartialViewResult ApplicationRequestListView()
        {
            Guid id = Guid.NewGuid();
            return PartialView(id);
        }

        public PartialViewResult ApplicationPoaListView()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        
        /// <summary>
        /// Получить список заявок на доверенность
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type">
        /// Тип запроса
        /// 0 - запрос заявок на доверенность
        /// 1 - 
        /// 2 - 
        /// </param>
        /// <returns></returns>
        public ActionResult ReadApplicationList([DataSourceRequest] DataSourceRequest request, int type)
        {
            ApplicationPoaRepository poaRepository = new ApplicationPoaRepository(false);

            Expression<Func<LimsTmcInView, bool>> exp = m => m.StateType != TmcIn.TmcInStatuses.Closed && m.StateType != TmcIn.TmcInStatuses.Received1C && m.StateType != TmcIn.TmcInStatuses.SendedAdmission1C;
            if (type == 1)
                exp = m => m.StateType == TmcIn.TmcInStatuses.Received1C || m.StateType == TmcIn.TmcInStatuses.SendedAdmission1C;
            else if (type == 2)
            {
                var currentUser = UserHelper.GetCurrentEmployee();
                var id = currentUser.Id;
                exp = m => (m.StateType == TmcIn.TmcInStatuses.AgreedResearchCenter && m.AgreementEmployeeId == id)
                || (m.StateType == TmcIn.TmcInStatuses.AgreedAccount && m.AccountantEmployeeId == id)
                || (m.StateType == TmcIn.TmcInStatuses.AgreedHead && m.ExecutorEmployeeId == id);
            }

            var qr = poaRepository.GetLimsTmcInViews(exp);
            if (type != 2)
            {
                qr = base.FilterByCurrentUser(qr, poaRepository.GetContext());
            }
            qr = qr.OrderBy(m => m.CreatedDate);

            var data = qr;
            
            return Json(data.ToDataSourceResult(request, o => new TmcInViewModel()
            {
                Id = o.Id,
                TmcInId = o.Id,
                ContractDate = o.ContractDate,
                ContractNumber = o.ContractNumber,
                IsFullDelivery = o.IsFullDelivery,
                LastDeliveryDate = o.LastDeliveryDate,
                OwnerEmployeeId = o.OwnerEmployeeId,
                Provider = o.Provider,
                ProviderBin = o.ProviderBin,
                StateType = o.StateType,
                CreatedEmployeeId = o.CreatedEmployeeId,
                StateTypeValue = o.StateTypeValue,
                //IsFullDeliveryValue = o.IsFullDeliveryValue,
                OwnerEmployeeValue = o.OwnerEmployeeValue,
                ExecutorEmployeeValue = o.ExecutorEmployeeValue,
                AgreementEmployeeValue = o.AgreementEmployeeValue,
                ExecutorEmployeeId = o.ExecutorEmployeeId,
                AgreementEmployeeId = o.AgreementEmployeeId,
                AccountantEmployeeId = o.AccountantEmployeeId,
                AccountantEmployeeValue = o.AccountantEmployeeValue,
                PowerOfAttorney = o.PowerOfAttorney,
                DayCount = o.DayCount,
                Comment = o.Comment
            }), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateApplication([DataSourceRequest] DataSourceRequest request, TmcInViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationPoaRepository repo = new ApplicationPoaRepository(false);
                TmcIn tmcIn = new TmcIn()
                {
                    Id = Guid.NewGuid(),
                    StateType = model.StateType,
                    ContractDate = model.ContractDate,
                    ContractNumber = model.ContractNumber,
                    CreatedDate = DateTime.Now,
                    CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                    IsFullDelivery = model.IsFullDelivery,
                    LastDeliveryDate = model.LastDeliveryDate,
                    OwnerEmployeeId = model.OwnerEmployeeId,
                    Provider = model.Provider,
                    ProviderBin = model.ProviderBin,
                    ExecutorEmployeeId = model.ExecutorEmployeeId,
                    AgreementEmployeeId = model.AgreementEmployeeId,
                    AccountantEmployeeId = model.AccountantEmployeeId
                };

                repo.Insert(tmcIn);
                repo.Save();

                var newDictionary = repo.GetTmcInViews(o => o.Id == tmcIn.Id).First();
                model.TmcInId = tmcIn.Id;
                model.OwnerEmployeeValue = newDictionary.OwnerEmployeeValue;
                model.StateTypeValue = newDictionary.StateTypeValue;
                model.PowerOfAttorney = newDictionary.PowerOfAttorney;
                model.IsFullDeliveryValue = newDictionary.IsFullDeliveryValue;
                model.ExecutorEmployeeValue = newDictionary.ExecutorEmployeeValue;
                model.AgreementEmployeeValue = newDictionary.AgreementEmployeeValue;
                model.AccountantEmployeeValue = newDictionary.AccountantEmployeeValue;
                model.Id = newDictionary.Id;


                TmcRepository tmcRepo = new TmcRepository();
                var dicRepo = new DictionaryRepository(false);
                var contractProducts = tmcRepo.GetI1cLimsContractProducts(lc => lc.ContractNumber == tmcIn.ContractNumber);
                foreach (var prod in contractProducts.ToList())
                {
                    var measureId = dicRepo.GetDictionaryIdByTypeAndDisplayName(Dictionary.MeasureType.DicCode, prod.Unit);
                    if (measureId == null || measureId == Guid.Empty)
                    {
                        measureId = dicRepo.GetDictionaryIdByTypeAndName(Dictionary.MeasureType.DicCode, prod.Unit);
                    }
                    
                    var tmc = new Tmc()
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                        TmcInId = tmcIn.Id,
                        StateType = 0,
                        Number = prod.ProductId,
                        Code = prod.ProductId,
                        Name = prod.Name,
                        Count = prod.QuantityVolume.Value,
                        CountActual = 0, //prod.QuantityVolume.Value,
                        CountFact = 0, //prod.QuantityVolume.Value,
                        CountConvert = 0, // prod.QuantityVolume.Value,
                        MeasureTypeDicId = measureId,
                        MeasureTypeConvertDicId = measureId
                    };


                    var tmcExist = tmcRepo.GetAsQuarable(t => t.Number == tmc.Number && t.StateType != Tmc.TmcStatuses.Writeoff).FirstOrDefault();
                    
                    if (tmcExist == null)
                    {
                        tmcRepo.Insert(tmc);

                        var limsTmc = new LimsTmcTemp()
                        {
                            TmcId = tmc.Id,
                            TmcInId = tmc.TmcInId,
                            CreatedDate = DateTime.Now,
                            CountRequest = tmc.Count,
                            IsSelected = false
                        };
                        repo.AddLimsTmcTemp(limsTmc);
                    }
                    else if (tmcExist.CountFact < tmcExist.Count)
                    {
                        var limsTmc = new LimsTmcTemp()
                        {
                            TmcId = tmcExist.Id,
                            TmcInId = tmcIn.Id,
                            CreatedDate = DateTime.Now,
                            CountRequest = prod.QuantityVolume.Value - tmcExist.CountFact,
                            IsSelected = false
                        };
                        if (prod.QuantityVolume.Value - tmcExist.CountFact > 0)
                            repo.AddLimsTmcTemp(limsTmc);
                    }
                    
                }
                tmcRepo.Save();

                repo.Save();
                
            }

            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateApplication([DataSourceRequest] DataSourceRequest request,
            TmcInView model)
        {

            if (model.Id == Guid.Empty)
                model.Id = model.TmcInId;

            ApplicationPoaRepository repo = new ApplicationPoaRepository(false);
            TmcIn d = repo.GetAsQuarable(o => o.Id == model.Id).FirstOrDefault();

            bool isContractChange = d.ContractNumber != model.ContractNumber;

            d.StateType = model.StateType;
            d.ContractDate = model.ContractDate;
            d.ContractNumber = model.ContractNumber;
            d.IsFullDelivery = model.IsFullDelivery;
            d.LastDeliveryDate = model.LastDeliveryDate;
            d.OwnerEmployeeId = model.OwnerEmployeeId;
            d.Provider = model.Provider;
            d.ProviderBin = model.ProviderBin;
            d.ExecutorEmployeeId = model.ExecutorEmployeeId;
            d.AgreementEmployeeId = model.AgreementEmployeeId;
            d.AccountantEmployeeId = model.AccountantEmployeeId;

            repo.Save();
            model = repo.GetTmcInViews(o => o.Id == d.Id).FirstOrDefault();
            if (isContractChange)
            {
                TmcRepository tmcRepo = new TmcRepository();

                tmcRepo.RemoveTmcByPoaId(model.Id);

                var dicRepo = new DictionaryRepository(false);
                var contractProducts = tmcRepo.GetI1cLimsContractProducts(lc => lc.ContractNumber == d.ContractNumber);
                foreach (var prod in contractProducts.ToList())
                {
                    var measureId = dicRepo.GetDictionaryIdByTypeAndDisplayName(Dictionary.MeasureType.DicCode, prod.Unit);

                    var tmc = new Tmc()
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                        TmcInId = d.Id,
                        StateType = 0,
                        Number = prod.ProductId,
                        Code = prod.ProductId,
                        Name = prod.Name,
                        Count = prod.QuantityVolume.Value,
                        CountActual = prod.QuantityVolume.Value,
                        CountFact = prod.QuantityVolume.Value,
                        CountConvert = prod.QuantityVolume.Value,
                        MeasureTypeDicId = measureId,
                        MeasureTypeConvertDicId = measureId
                    };
                    var tmcExist = tmcRepo.GetAsQuarable(t => t.Number == tmc.Number && t.CountFact < t.Count).FirstOrDefault();
                    if (tmcExist == null)
                    {
                        tmcRepo.Insert(tmc);

                        var limsTmc = new LimsTmcTemp()
                        {
                            TmcId = tmc.Id,
                            TmcInId = tmc.TmcInId,
                            CreatedDate = DateTime.Now,
                            CountRequest = tmc.Count,
                            IsSelected = false
                        };
                        repo.AddLimsTmcTemp(limsTmc);
                    }
                    else
                    {
                        var limsTmc = new LimsTmcTemp()
                        {
                            TmcId = tmc.Id,
                            TmcInId = tmc.TmcInId,
                            CreatedDate = DateTime.Now,
                            CountRequest = prod.QuantityVolume.Value - tmcExist.CountFact,
                            IsSelected = false
                        };
                        if (prod.QuantityVolume.Value - tmcExist.CountFact > 0)
                            repo.AddLimsTmcTemp(limsTmc);
                    }
                }
                tmcRepo.Save();

                repo.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyApplication([DataSourceRequest] DataSourceRequest request, TmcInView model)
        {
            if (model != null)
            {
                TmcRepository tRepo = new TmcRepository(false);
                tRepo.RemoveTmcByPoaId(model.Id);
                tRepo.Save();
                
                ApplicationPoaRepository repo = new ApplicationPoaRepository(false);
                TmcIn d = repo.GetAsQuarable(o => o.Id == model.Id).FirstOrDefault();
                repo.Delete(d);
                repo.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        
        
        /// <summary>
        ///  Получить список Реактив
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult ReadTmcList([DataSourceRequest] DataSourceRequest request, Guid tmcInId, int type = 0)
        {
            //TmcRepository repository = new TmcRepository(false);
            //var data = repository.TvGetAsQuarable().Where(m => m.TmcInId == tmcInId).OrderByDescending(m => m.CreatedDate);
            ApplicationPoaRepository repository = new ApplicationPoaRepository(false);
            var tmcIn = repository.GetById(tmcInId);
            var data = repository.GetLimsTmcTempViews(l => l.TmcInId == tmcInId);

            if (type == 1 || (tmcIn.StateType != TmcIn.TmcInStatuses.New && tmcIn.StateType != TmcIn.TmcInStatuses.Repeal && tmcIn.StateType != TmcIn.TmcInStatuses.Rejected))
            {
                data = data.Where(l => l.IsSelected.Value);
            }
            data = data.OrderByDescending(l => l.CreatedDate);
            return Json(data.ToDataSourceResult(request, tv =>
            {
                return new TmcViewModel()
                {
                    Id = tv.Id,
                    TmcInId = tv.TmcInId,
                    Name = tv.TmcName,
                    Count = tv.Count,
                    CountRequest = tv.CountRequest,
                    MeasureTypeDicValue = tv.MeasureTypeDicName,
                    MeasureTypeDicId = tv.MeasureTypeDicId,
                    IsSelected = tv.IsSelected != null && tv.IsSelected.Value,
                    CountFact = tv.StateType == Tmc.TmcStatuses.New ? 0 : tv.CountFact,
                    CountConvert = tv.StateType == Tmc.TmcStatuses.New ? 0 : tv.CountConvert,
                    //CountActual = tv.StateType == Tmc.TmcStatuses.New ? 0 : tv.CountActual,
                    MeasureTypeConvertDicId = tv.MeasureTypeConvertDicId,
                    MeasureTypeConvertDicValue = tv.MeasureTypeConvertDicName,
                    ReceivingDate = tv.ReceivingDate,
                    StateType = tv.StateType,
                    StateTypeValue = "",
                };
            }), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTmc([DataSourceRequest] DataSourceRequest request, TmcViewModel model)
        {
            TmcRepository repo = new TmcRepository(false);

            Tmc tmc = new Tmc()
            {
                Id = Guid.NewGuid(),
                StateType = model.StateType,
                Name = model.Name,
                Code = model.Code,
                CreatedDate = DateTime.Now,
                CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                Count = model.Count,
                CountActual = model.CountActual,
                OwnerEmployeeId = model.OwnerEmployeeId,
                Number = model.Number,
                CountFact = model.CountFact,
                CountConvert = model.CountFact,
                ExpiryDate = model.ExpiryDate,
                ManufactureDate = model.ManufactureDate,
                Manufacturer = model.Manufacturer,
                MeasureTypeConvertDicId = model.MeasureTypeConvertDicId,
                MeasureTypeDicId = model.MeasureTypeDicId,
                PackageDicId = model.PackageDicId,
                Rack = model.Rack,
                Safe = model.Safe,
                Serial = model.Serial,
                StorageDicId = model.StorageDicId,
                TmcTypeDicId = model.TmcTypeDicId,
                ReceivingDate = model.ReceivingDate,
                TmcInId = Guid.Parse(model.TmcInIdString),
            };
            repo.Insert(tmc);
            repo.Save();
            model.Id = tmc.Id;

            var newDictionary = repo.TvGetAsQuarable(o => o.Id == tmc.Id).First();
            model.MeasureTypeDicValue = newDictionary.MeasureTypeDicValue;

            var appRepo = new ApplicationPoaRepository(false);
            appRepo.AddLimsTmcTemp(new LimsTmcTemp()
            {
                TmcId = tmc.Id,
                TmcInId = tmc.TmcInId,
                CountRequest = model.CountRequest,
                CountReceived = null,
                IsSelected = model.IsSelected,
                CreatedDate = DateTime.Now
            });
            appRepo.Save();
            
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateTmc([DataSourceRequest] DataSourceRequest request, TmcViewModel model)
        {
            TmcRepository repo = new TmcRepository(false);
            Tmc tmc = repo.GetAsQuarable(o => o.Id == model.Id).FirstOrDefault();
            if (tmc != null)
            {
                //tmc.StateType = model.StateType;
                //tmc.Name = model.Name;
                //tmc.Code = model.Code;
                //tmc.Count = model.Count;
                //tmc.CountActual = model.CountRequest.Value;
                //tmc.OwnerEmployeeId = model.OwnerEmployeeId;
                //tmc.Number = model.Number;
                //tmc.CountFact = model.CountRequest.Value;
                //tmc.CountConvert = model.CountRequest.Value;
                //tmc.ExpiryDate = model.ExpiryDate;
                //tmc.ManufactureDate = model.ManufactureDate;
                //tmc.Manufacturer = model.Manufacturer;
                //tmc.MeasureTypeConvertDicId = model.MeasureTypeConvertDicId;
                //tmc.MeasureTypeDicId = model.MeasureTypeDicId;
                //tmc.PackageDicId = model.PackageDicId;
                //tmc.Rack = model.Rack;
                //tmc.Safe = model.Safe;
                //tmc.Serial = model.Serial;
                //tmc.StorageDicId = model.StorageDicId;
                //tmc.TmcTypeDicId = model.TmcTypeDicId;
                //tmc.ReceivingDate = model.ReceivingDate;
                tmc.MeasureTypeDicId = model.MeasureTypeDicId;
            }

            repo.Save();

            if (tmc != null)
            {
                var newDictionary = repo.TvGetAsQuarable(o => o.Id == tmc.Id).First();
                model.MeasureTypeDicValue = newDictionary.MeasureTypeDicValue;

                var appRepo = new ApplicationPoaRepository(false);
                var limsTmcTemp = appRepo.GetLimsTmcTemps(ltt => ltt.TmcId == tmc.Id && ltt.TmcInId == tmc.TmcInId).FirstOrDefault();
                if (limsTmcTemp != null)
                {
                    limsTmcTemp.CountRequest = model.CountRequest;
                    limsTmcTemp.CountReceived = model.CounReceived;
                }
                appRepo.Save();                
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyTmc([DataSourceRequest] DataSourceRequest request, TmcViewModel dictionary)
        {
            TmcRepository repo = new TmcRepository(false);
            ApplicationPoaRepository poaRepo = new ApplicationPoaRepository(false);
            if (dictionary != null)
            {
                var limsTmcTemp = poaRepo.GetLimsTmcTemps(ltt => ltt.TmcId == dictionary.Id && ltt.TmcInId == dictionary.TmcInId).FirstOrDefault();
                poaRepo.DeleteLimsTmcTemp(limsTmcTemp);
                poaRepo.Save();

                Tmc d = repo.GetAsQuarable(o => o.Id == dictionary.Id).First();
                repo.Delete(d);
                repo.Save();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }


        // Actions

        /// <summary>
        /// Select items 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult SelectTmcItem(TmcViewModel model)
        {
            ApplicationPoaRepository poaRepo = new ApplicationPoaRepository(false);
            //if (model.IsSelected)
            //{
            //    poaRepo.AddLimsTmcTemp(new LimsTmcTemp()
            //    {
            //        TmcId = model.Id.Value,
            //        TmcInId = model.TmcInId.Value,
            //        CreatedDate = DateTime.Now
            //    });
            //}
            //else
            //{
            //    poaRepo.DeleteLimsTmcTemp(new LimsTmcTemp()
            //    {
            //        TmcId = model.Id.Value,
            //        TmcInId = model.TmcInId.Value
            //    });
            //}
            var limsTmcTemp = poaRepo.GetLimsTmcTemps(ltt => ltt.TmcId == model.Id.Value && ltt.TmcInId == model.TmcInId.Value)
                .FirstOrDefault();
            if (limsTmcTemp != null)
            {
                limsTmcTemp.IsSelected = model.IsSelected;
                poaRepo.UpdateLimsTmcTemp(limsTmcTemp);
                poaRepo.Save();
            }
            
            return Json(new {IsSuccess = true}, JsonRequestBehavior.AllowGet);
        }
        
        // Отправить в 1С 
        public PartialViewResult ItemsFor1CView(Guid tmcInId, string containerId)
        {
            DialogParameter parameter = new DialogParameter()
            {
                Id = tmcInId,
                ContainerId = containerId
            };

            return PartialView(parameter);
        }
        
        public JsonResult ReadItemsFo1CList([DataSourceRequest] DataSourceRequest request, Guid tmcInId)
        {
           ApplicationPoaRepository repository = new ApplicationPoaRepository(false);
            var data = repository.GetLimsTmcTempViews(m => m.TmcInId == tmcInId && m.IsSelected.Value);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // Принять
        public PartialViewResult AcceptTmcView(Guid id, Guid tmcInId)
        {
            ApplicationPoaRepository repository = new ApplicationPoaRepository();
            var limsTmcTemp = repository.GetLimsTmcTempViews(l => l.Id == id && l.TmcInId == tmcInId).FirstOrDefault();

            return PartialView(new TmcViewModel()
            {
                Id = limsTmcTemp.Id,
                TmcInId = limsTmcTemp.TmcInId,
                CountRequest = limsTmcTemp.CountRequest.Value,
                Count = limsTmcTemp.CountRequest.Value,
                CountConvert = limsTmcTemp.CountRequest.Value,
                MeasureTypeConvertDicId = limsTmcTemp.MeasureTypeConvertDicId,
                MeasureTypeDicId = limsTmcTemp.MeasureTypeDicId,
                ReceivingDate = limsTmcTemp.ReceivingDate,
                MeasureTypeConvertDic = new MeasureType()
                {
                    Id = limsTmcTemp.MeasureTypeConvertDicId.Value,
                    Name = limsTmcTemp.MeasureTypeConvertDicName
                }
            });
        }

        [HttpPost]
        public ActionResult ConfirmAcceptTmc(Guid id, Tmc tmc)
        {
            TmcRepository repo = new TmcRepository(false);
            var tmcExist = repo.GetAsQuarable(o => o.Id == id).FirstOrDefault();
            if (tmcExist != null)
            {
                if (tmcExist.StateType == Tmc.TmcStatuses.Accepted)
                {
                    tmcExist.CountFact += tmc.Count;
                    tmcExist.CountActual += tmc.CountConvert;
                    tmcExist.CountConvert += tmc.CountConvert;
                }
                else
                {
                    tmcExist.CountFact = tmc.Count;
                    tmcExist.CountActual = tmcExist.CountConvert = tmc.CountConvert;
                }

                tmcExist.StateType = Tmc.TmcStatuses.Accepted;
                
                if (tmc.MeasureTypeConvertDicId != null)
                {
                    tmcExist.MeasureTypeConvertDicId = tmc.MeasureTypeConvertDicId;
                }
                else
                {
                    tmcExist.MeasureTypeConvertDicId = tmcExist.MeasureTypeDicId;
                    tmcExist.CountActual = tmcExist.CountConvert = tmcExist.CountFact = tmc.Count;
                }
                tmcExist.OwnerEmployeeId = UserHelper.GetCurrentEmployee().Id;
                tmcExist.ReceivingDate = tmc.ReceivingDate ?? DateTime.Now;
                repo.Save();

                ApplicationPoaRepository repository = new ApplicationPoaRepository(false);
                var limsTmcTemp = repository.GetLimsTmcTemps(ltt => ltt.TmcId == tmc.Id && ltt.TmcInId == tmc.TmcInId).FirstOrDefault();
                if (limsTmcTemp != null)
                {
                    limsTmcTemp.CountReceived = tmc.Count;
                }
                repository.Save();
            }

            return Json(id, JsonRequestBehavior.AllowGet);
        }

        // Отправить в 1С принятые ТМЦ
        public PartialViewResult SendTo1CTmcsView(Guid id)
        {
            return PartialView(id);
        }

        [HttpPost]
        public ActionResult ConfirmSendTo1CTmcs(Guid id)
        {
            ApplicationPoaRepository repo = new ApplicationPoaRepository(false);
            var tmcin = repo.GetAsQuarable(o => o.Id == id).FirstOrDefault();
            if (tmcin != null)
            {
                tmcin.StateType = TmcIn.TmcInStatuses.SendedAdmission1C;
                repo.Update(tmcin);
                repo.Save();
            }

            return Json(id, JsonRequestBehavior.AllowGet);
        }
    }
}