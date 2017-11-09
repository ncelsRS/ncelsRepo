using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.Lims;
using PW.Prism.Controllers.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers
{
    public class TmcController : BaseController
    {
        private ncelsEntities db = new ncelsEntities();

        // GET: Tmc
        public ActionResult TmcList()
        {
            Guid guid = Guid.NewGuid();

            ViewBag.TmcTypes =
                db.Dictionaries.Where(o => o.Type == "TmcType" && o.ExpireDate == null)
                    .ToList().OrderBy(o => o.Name)
                    .Select(o => new Item() { Id = o.Id.ToString(), Name = o.Name }).ToList();
            
            return PartialView(guid);
        }

        public ActionResult RequestList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult RequestListIn()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        /// <summary>
        /// Выдача
        /// </summary>
        /// <param name="id">Реактив ИД</param>
        /// <param name="measureTypeConvertDicId"></param>
        /// <returns></returns>
        public ActionResult OutTmc(Guid id, Guid? measureTypeConvertDicId = null)
        {
            var tmcOutCount = db.TmcOutCounts.FirstOrDefault(t => t.Id == id);
            if (tmcOutCount == null)
            {
                tmcOutCount = new TmcOutCount()
                {
                    Id = id,
                    Count = 0
                };
            }
            tmcOutCount.MeasureTypeConvertDicId = measureTypeConvertDicId;
            return PartialView(tmcOutCount);
        }
        public ActionResult AddTmc(Guid id)
        {
            var tmc = db.Tmcs.FirstOrDefault(t => t.Id == id);
            if (tmc == null)
            {
                tmc = new Tmc()
                {
                    Id = id,
                    Count = 0
                };
            }
            return PartialView(tmc);
        }
        public ActionResult DeliveryList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult SpentList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }
        [HttpPost]
        public ActionResult SendTmcOut(Guid id)
        {
            var item = db.TmcOuts.First(o => o.Id == id);
            item.StateType = 1;
            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ConfirmAddTmc(Guid id, Tmc tmc)
        {
            var item = db.Tmcs.First(o => o.Id == id);
            item.StateType = 1;
	        item.CountFact = tmc.Count;
            item.CountConvert = tmc.CountConvert;
            if (tmc.MeasureTypeConvertDic != null)
            {
                item.MeasureTypeConvertDicId = tmc.MeasureTypeConvertDic.Id;
            }
            else
            {
                item.MeasureTypeConvertDicId = item.MeasureTypeDicId;
                item.CountConvert = item.CountFact = tmc.Count;
            }
            item.OwnerEmployeeId = UserHelper.GetCurrentEmployee().Id;
            
            item.ReceivingDate = tmc.ReceivingDate ?? DateTime.Now;


            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ConfirmOutTmc(TmcOutCount outCount, Guid tmcId)
        {
            var item = db.TmcOutCounts.First(o => o.Id == tmcId);
            item.StateType = 1;
	        item.CountFact = outCount.Count;

            var tOutCount = db.TmcOutCounts.Count(o => o.TmcOutId == item.TmcOutId);
            var tOutCountIssued = db.TmcOutCounts.Count(o => o.TmcOutId == item.TmcOutId && o.StateType == 1);

            var tmcOut = db.TmcOuts.First(to => to.Id == item.TmcOutId);
            if (tOutCount == (tOutCountIssued + 1))
                tmcOut.StateType = 2;

            db.SaveChanges();
			return Json(tmcId, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            List<Ref_MarketPrices> data = new List<Ref_MarketPrices>();
            return Json(data.ToDataSourceResult(request));
        }
        
        public ActionResult ReadRequestTakeChildrens([DataSourceRequest] DataSourceRequest request, Guid tmcInId)
        {
            var data = db.TmcViews.Where(m=>m.TmcInId == tmcInId).OrderByDescending(m => m.CreatedDate);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReadTmcOutCount([DataSourceRequest] DataSourceRequest request, Guid tmcInId)
        {
            var data = db.TmcOutCountViews.Where(m=>m.TmcId== tmcInId).OrderByDescending(m => m.Count).Select(o => new
            {
                o.Count,
                o.Id,
                MainOutId= o.Id,
                o.CountFact,
                o.MeasureTypeConvertDicId,
                o.MeasureTypeConvertDicValue,
                o.Name,
                o.Note,
                o.OwnerEmployeeValue,
                o.Rack,
                o.Safe,
                o.StateType,
                o.StateTypeValue
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReadTmcOff([DataSourceRequest] DataSourceRequest request, Guid tmcInId) {
            var data = db.TmcOffViews.Where(m => m.TmcOutId == tmcInId).OrderByDescending(m => m.Count);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // отправить в 1с зщаявку на доверенность 
        public ActionResult SendOneS(Guid id)
        {
            return PartialView(id);
        }

        public ActionResult ConfirmDialog(Guid id, string url, string text)
        {
            ConfirmDialogViewModel cdm = new ConfirmDialogViewModel()
            {
                Id = id,
                Url = url,
                Text = text
            };

            return PartialView(cdm);
        }

        [HttpPost]
        public ActionResult ConfirmASendOneS(Guid id) {
            var tmc = db.TmcIns.First(m => m.Id == id);
            tmc.StateType = 10;
            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ConfirmRepeal(Guid id)
        {
            var tmc = db.TmcIns.First(m => m.Id == id);
            tmc.StateType = 4;
            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        public ActionResult WriteOffTmcList()
        {
            Guid guid = Guid.NewGuid();

            ViewBag.TmcTypes =
                db.Dictionaries.Where(o => o.Type == "TmcType" && o.ExpireDate == null)
                    .ToList().OrderBy(o => o.Name)
                    .Select(o => new Item() { Id = o.Id.ToString(), Name = o.Name }).ToList();

            return PartialView(guid);
        }

        public ActionResult ReadWriteOffTmcListFirst([DataSourceRequest] DataSourceRequest request)
        {
            var data = db.TmcViews.Where(o => o.StateType == 2);
            //if (!PW.Ncels.Database.Helpers.EmployePermissionHelper.IsViewTmcList)
            //{
            //    var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
            //    data = data.Where(o => o.OwnerEmployeeId == currentEmployeeId || o.CreatedEmployeeId == currentEmployeeId);
            //}

            data = data.OrderByDescending(m => m.Name);
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult WriteoffDialog(Guid id, int type, string url)
        {
            var createEmployeeId = UserHelper.GetCurrentEmployee().Id;
            //            var applicationComment = db.TmcApplicationComments
            //                .Where(a => a.ApplicationId == id)
            //                .OrderByDescending(a => a.CreateDate)
            //                .FirstOrDefault();
            var applicationComment = new TmcApplicationComment()
            {
                Id = Guid.NewGuid(),
                ApplicationId = id,
                CreateEmployeeId = createEmployeeId,
                Type = type,
                Url = url
            };

            return PartialView(applicationComment);
        }
        
        [HttpPost]
        public ActionResult ConfirmWriteoff(TmcApplicationComment comment)
        {
            var tmc = db.Tmcs.First(m => m.Id == comment.ApplicationId);
            tmc.WriteoffDate = DateTime.Now;
            tmc.StateType = 2;

            var commentApp = db.TmcApplicationComments.FirstOrDefault(m => m.Id == comment.Id);
            if (commentApp == null)
            {
                commentApp = comment;
                db.TmcApplicationComments.Add(commentApp);
            }
            else
            {
                commentApp.Comment = comment.Comment;
            }

            db.SaveChanges();
            return Json(comment.Id, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Загрузка данных в таблицу доверенностей
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type">
        /// 0 - заявка на доверенность
        /// 1 - доверенность
        /// 2 - соглаование заявки на доверенность
        /// </param>
        /// <returns></returns>
        #region RequestTake
        public ActionResult ReadRequestTake([DataSourceRequest] DataSourceRequest request, 
            int type)
        {
            Expression<Func<TmcInView, bool>> exp = m => m.StateType != 3 && m.StateType != 2;
            if (type == 1)
                exp = m => m.StateType == 2;
            else if (type == 2)
            {
                var currentUser = UserHelper.GetCurrentEmployee();
                var id = currentUser.Id;
                exp = m => (m.StateType == 10 && m.AgreementEmployeeId == id)
                || (m.StateType == 11 && m.AccountantEmployeeId == id)
                || (m.StateType == 12 && m.ExecutorEmployeeId == id);
            }

            var qr = db.TmcInViews.OrderByDescending(m => m.CreatedDate)
                    .Where(exp);
            if (type != 2)
            {
                qr = base.FilterByCurrentUser(qr, db);
            }

            var data = qr.Select(o => new TmcInViewModel()
            {
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
                IsFullDeliveryValue = o.IsFullDeliveryValue,
                OwnerEmployeeValue = o.OwnerEmployeeValue,
                ExecutorEmployeeValue = o.ExecutorEmployeeValue,
                AgreementEmployeeValue = o.AgreementEmployeeValue,
                ExecutorEmployeeId = o.ExecutorEmployeeId,
                AgreementEmployeeId = o.AgreementEmployeeId,
                AccountantEmployeeId = o.AccountantEmployeeId,
                AccountantEmployeeValue = o.AccountantEmployeeValue,
                PowerOfAttorney = o.PowerOfAttorney,
                Func1 = o.Func1,
                Id = o.Id,
                TmcInId = o.Id,
                
            }).ToList();

            foreach (TmcInViewModel d in data)
            {
                d.AttachFiles = UploadHelper.GetFilesInfo(d.Id.ToString(), false);
                var tmcComment = db.TmcApplicationComments.Where(c => c.ApplicationId == d.Id)
                        .OrderByDescending(c => c.CreateDate)
                        .FirstOrDefault();
                if (tmcComment != null)
                    d.Comment = tmcComment.Comment;
            }

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateRequestTake([DataSourceRequest] DataSourceRequest request, TmcInViewModel dictionary)
        {
            if (ModelState.IsValid)
            {
                TmcIn tmc = new TmcIn()
                {
                    Id = Guid.NewGuid(),
                    StateType = dictionary.StateType,
                    ContractDate = dictionary.ContractDate,
                    ContractNumber = dictionary.ContractNumber,
                    CreatedDate = DateTime.Now,
                    CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                    IsFullDelivery = dictionary.IsFullDelivery,
                    LastDeliveryDate = dictionary.LastDeliveryDate,
                    OwnerEmployeeId = dictionary.OwnerEmployeeId,
                    Provider = dictionary.Provider,
                    ProviderBin = dictionary.ProviderBin,
                    ExecutorEmployeeId = dictionary.ExecutorEmployeeId,
                    AgreementEmployeeId = dictionary.AgreementEmployeeId,
                    AccountantEmployeeId = dictionary.AccountantEmployeeId
                };
                db.TmcIns.Add(tmc);
                db.SaveChanges();
                var newDictionary = db.TmcInViews.First(o => o.Id == tmc.Id);
                dictionary.TmcInId = tmc.Id;
                dictionary.OwnerEmployeeValue = newDictionary.OwnerEmployeeValue;
                dictionary.StateTypeValue = newDictionary.StateTypeValue;
                dictionary.PowerOfAttorney = newDictionary.PowerOfAttorney;
                dictionary.IsFullDeliveryValue = newDictionary.IsFullDeliveryValue;
                dictionary.ExecutorEmployeeValue = newDictionary.ExecutorEmployeeValue;
                dictionary.AgreementEmployeeValue = newDictionary.AgreementEmployeeValue;
                dictionary.AccountantEmployeeValue = newDictionary.AccountantEmployeeValue;
                dictionary.Id = newDictionary.Id;


                TmcRepository tmcRepo = new TmcRepository();
                var dicRepo = new DictionaryRepository(false);
                var contractProducts = tmcRepo.GetI1cLimsContractProducts(lc => lc.ContractNumber == tmc.ContractNumber);
                foreach (var prod in contractProducts.ToList())
                {
                    var measureId = dicRepo.GetDictionaryIdByTypeAndDisplayName(Dictionary.MeasureType.DicCode, prod.Unit);

                    var t = new Tmc()
                    {
                        Id = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                        TmcInId = tmc.Id,
                        StateType = 0,
                        Number = prod.ProductId,
                        Code = prod.ProductId,
                        Name =  prod.Name,
                        Count = prod.QuantityVolume.Value,
                        CountActual = prod.QuantityVolume.Value,
                        CountFact = prod.QuantityVolume.Value,
                        CountConvert = prod.QuantityVolume.Value,
                        MeasureTypeDicId = measureId,
                        MeasureTypeConvertDicId = measureId
                    };
                    tmcRepo.Insert(t);
                }
                tmcRepo.Save();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateRequestTake([DataSourceRequest] DataSourceRequest request,
            TmcInView dictionary)
        {

            if (dictionary.Id == Guid.Empty)
                dictionary.Id = dictionary.TmcInId;
            
            TmcIn d = db.TmcIns.First(o => o.Id == dictionary.Id);

            bool isContractChange = d.ContractNumber != dictionary.ContractNumber;

            d.StateType = dictionary.StateType;
            d.ContractDate = dictionary.ContractDate;
            d.ContractNumber = dictionary.ContractNumber;
            d.IsFullDelivery = dictionary.IsFullDelivery;
            d.LastDeliveryDate = dictionary.LastDeliveryDate;
            d.OwnerEmployeeId = dictionary.OwnerEmployeeId;
            d.Provider = dictionary.Provider;
            d.ProviderBin = dictionary.ProviderBin;
            d.ExecutorEmployeeId = dictionary.ExecutorEmployeeId;
            d.AgreementEmployeeId = dictionary.AgreementEmployeeId;
            d.AccountantEmployeeId = dictionary.AccountantEmployeeId;
            
            db.SaveChanges();
            dictionary = db.TmcInViews.First(o => o.Id == d.Id);
            if (isContractChange)
            {
                TmcRepository tmcRepo = new TmcRepository();

                tmcRepo.RemoveTmcByPoaId(dictionary.Id);

                var dicRepo = new DictionaryRepository(false);
                var contractProducts = tmcRepo.GetI1cLimsContractProducts(lc => lc.ContractNumber == d.ContractNumber);
                foreach (var prod in contractProducts.ToList())
                {
                    var measureId = dicRepo.GetDictionaryIdByTypeAndDisplayName(Dictionary.MeasureType.DicCode, prod.Unit);

                    var t = new Tmc()
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
                    tmcRepo.Insert(t);
                }
                tmcRepo.Save();
            }
            return Json(new[] {dictionary}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyRequestTake([DataSourceRequest] DataSourceRequest request, TmcInView dictionary) {
            if (dictionary != null) {
                TmcIn d = db.TmcIns.First(o => o.Id == dictionary.Id);
                db.TmcIns.Remove(d);
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Delivery
        public ActionResult ReadDelivery([DataSourceRequest] DataSourceRequest request)
        {
            var qr = db.TmcOutViews.Where(o => o.StateType == 1 || o.StateType == 2);
            qr = base.FilterOwnerByCurrentUser(qr, db);
            var data = qr.OrderByDescending(m => m.CreatedDate).ToList();
                /*
                .Select(o => new {
                Id = o.Id,
                CreatedDate = o.CreatedDate,
                CreatedEmployeeId = o.CreatedEmployeeId,
                CreatedEmployeeValue = o.CreatedEmployeeValue,
                Note = o.Note,
                OwnerEmployeeId = o.OwnerEmployeeId,
                OwnerEmployeeValue = o.OwnerEmployeeValue,
                StateType = o.StateType,
                Rack = o.Rack,
                Safe = o.Safe,
                OutTypeDicId = o.OutTypeDicId,
                OutTypeDicValue = o.OutTypeDicValue,
                StateTypeValue = o.StateTypeValue,
                StorageDicId = o.StorageDicId,
                StorageDicValue = o.StorageDicValue,
                //TmcOutId = o.Id,
            }).ToList();*/

            foreach (var d in data)
            {
                var tmcComment = db.TmcApplicationComments.Where(c => c.ApplicationId == d.Id)
                        .OrderByDescending(c => c.CreateDate)
                        .FirstOrDefault();
                if (tmcComment != null)
                    d.Comment = tmcComment.Comment;
                d.TmcOutId = d.Id;
            }

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateDelivery([DataSourceRequest] DataSourceRequest request, TmcOutView dictionary)
        {
            TmcOut tmc = new TmcOut()
            {
                Id = Guid.NewGuid(),
                StateType = dictionary.StateType,
                 Note= dictionary.Note,
                OutTypeDicId = dictionary.OutTypeDicId,
                CreatedDate = DateTime.Now,
                CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                StorageDicId = dictionary.StorageDicId,
                OwnerEmployeeId = dictionary.OwnerEmployeeId,
                Safe = dictionary.Safe,
                Rack = dictionary.Rack,
            };

            db.TmcOuts.Add(tmc);
            db.SaveChanges();
            dictionary.Id = tmc.Id;
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateDelivery([DataSourceRequest] DataSourceRequest request, TmcOutView dictionary)
        {
            TmcOut d = db.TmcOuts.First(o => o.Id == dictionary.Id);
            d.StateType = dictionary.StateType;
            d.Note = dictionary.Note;
            d.OutTypeDicId = dictionary.OutTypeDicId;
            d.StorageDicId = dictionary.StorageDicId;
            d.OwnerEmployeeId = dictionary.OwnerEmployeeId;
            d.Rack = dictionary.Rack;
            d.Safe = dictionary.Safe;
            db.SaveChanges();
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyDelivery([DataSourceRequest] DataSourceRequest request, TmcOutView dictionary)
        {
            if (dictionary != null)
            {
                TmcOut d = db.TmcOuts.First(o => o.Id == dictionary.Id);
                db.TmcOuts.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region DeliveryChildrens
        public ActionResult ReadDeliveryChildrens([DataSourceRequest] DataSourceRequest request,Guid tmcOutId)
        {
            var data = db.TmcOutCountViews.OrderByDescending(m => m.Name).Where(m=>m.TmcOutId== tmcOutId).Select(o => new
            {
                o.Id,
                o.Name,
                o.Count,
                o.Note,
                o.CountFact,
                o.OwnerEmployeeValue,
                o.StateType,
                o.Rack,
                o.Safe,
                o.MeasureTypeConvertDicId,
                o.MeasureTypeConvertDicValue,
                o.StateTypeValue,
                o.TmcId,
                o.TmcOutId,
                o.StorageDicValue,
                o.CountConvert,
                o.ApplicationStateType
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateDeliveryChildrens([DataSourceRequest] DataSourceRequest request, TmcOutCountViewModel dictionary)
        {

            TmcOutCount tmc = new TmcOutCount()
            {
                Id = Guid.NewGuid(),
                StateType = dictionary.StateType,
                Note = dictionary.Note,
                CountFact = dictionary.CountFact,
                Count = dictionary.Count,
                TmcOutId = Guid.Parse(dictionary.TmcOutIdString)
            };

            db.TmcOutCounts.Add(tmc);
            db.SaveChanges();
            dictionary.Id = tmc.Id;


            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateDeliveryChildrens([DataSourceRequest] DataSourceRequest request, TmcOutCountViewModel dictionary)
        {

            TmcOutCount d = db.TmcOutCounts.First(o => o.Id == dictionary.Id);
            d.StateType = dictionary.StateType;
            d.Note = dictionary.Note;
            d.Count = dictionary.Count;
            d.CountFact = dictionary.CountFact;
            db.SaveChanges();


            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyDeliveryChildrens([DataSourceRequest] DataSourceRequest request, TmcOutCountViewModel dictionary)
        {
            if (dictionary != null)
            {
                TmcOutCount d = db.TmcOutCounts.First(o => o.Id == dictionary.Id);
                db.TmcOutCounts.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region RequestTakeChildrens

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateRequestTakeChildrens([DataSourceRequest] DataSourceRequest request, TmcViewModel dictionary) {

            Tmc tmc = new Tmc() {
                Id = Guid.NewGuid(),
                StateType = dictionary.StateType,
                Name = dictionary.Name,
                Code = dictionary.Code,
                CreatedDate = DateTime.Now,
                CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                Count = dictionary.Count,
                CountActual = dictionary.CountActual,
                OwnerEmployeeId = dictionary.OwnerEmployeeId,
                Number = dictionary.Number,
                CountFact = dictionary.CountFact,
                CountConvert = dictionary.CountFact,
                ExpiryDate = dictionary.ExpiryDate,
                ManufactureDate = dictionary.ManufactureDate,
                Manufacturer = dictionary.Manufacturer,
                MeasureTypeConvertDicId = dictionary.MeasureTypeConvertDicId,
                MeasureTypeDicId = dictionary.MeasureTypeDicId,
                PackageDicId = dictionary.PackageDicId,
                Rack = dictionary.Rack,
                Safe = dictionary.Safe,
                Serial = dictionary.Serial,
                StorageDicId = dictionary.StorageDicId,
                TmcTypeDicId = dictionary.TmcTypeDicId,
                ReceivingDate = dictionary.ReceivingDate,
                TmcInId = Guid.Parse(dictionary.TmcInIdString),

            };

            db.Tmcs.Add(tmc);
            db.SaveChanges();
            dictionary.Id = tmc.Id;

            var newDictionary = db.TmcViews.First(o => o.Id == tmc.Id);
            dictionary.MeasureTypeDicValue = newDictionary.MeasureTypeDicValue;

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateRequestTakeChildrens([DataSourceRequest] DataSourceRequest request, TmcView dictionary) {

            Tmc d = db.Tmcs.First(o => o.Id == dictionary.Id);
            d.StateType = dictionary.StateType;
            d.Name = dictionary.Name;
            d.Code = dictionary.Code;
            d.Count = dictionary.Count;
            d.CountActual = dictionary.CountActual;
            d.OwnerEmployeeId = dictionary.OwnerEmployeeId;
            d.Number = dictionary.Number;
            d.CountFact = dictionary.CountFact;
            d.CountConvert = dictionary.CountConvert;
            d.ExpiryDate = dictionary.ExpiryDate;
            d.ManufactureDate = dictionary.ManufactureDate;
            d.Manufacturer = dictionary.Manufacturer;
            d.MeasureTypeConvertDicId = dictionary.MeasureTypeConvertDicId;
            d.MeasureTypeDicId = dictionary.MeasureTypeDicId;
            d.PackageDicId = dictionary.PackageDicId;
            d.Rack = dictionary.Rack;
            d.Safe = dictionary.Safe;
            d.Serial = dictionary.Serial;
            d.StorageDicId = dictionary.StorageDicId;
            d.TmcTypeDicId = dictionary.TmcTypeDicId;
            d.ReceivingDate = dictionary.ReceivingDate;
            db.SaveChanges();

            var newDictionary = db.TmcViews.First(o => o.Id == d.Id);
            dictionary.MeasureTypeDicValue = newDictionary.MeasureTypeDicValue;

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyRequestTakeChildrens([DataSourceRequest] DataSourceRequest request, TmcView dictionary) {
            if (dictionary != null) {
                Tmc d = db.Tmcs.First(o => o.Id == dictionary.Id);
                db.Tmcs.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        #endregion RequestTakeChildrens

        #region TmcListFirst
        /// <summary>
        ///  Получить список Реактив
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult ReadTmcListFirst([DataSourceRequest] DataSourceRequest request)
        {
            /*
            var data = db.TmcViews.Where(o=>o.StateType==1).OrderByDescending(m => m.Name).Select(o => new
            {
                o.Id,
                o.Name,
                o.Count,
                o.CountActual,
                o.CountFact,
                o.OwnerEmployeeValue,
                o.StateType,
                o.Rack,
                o.Safe,
                o.MeasureTypeConvertDicId,
                o.MeasureTypeConvertDicValue,
                o.StateTypeValue,
                o.CountConvert,
                o.Manufacturer,
                o.CreatedDate,
                o.CreatedEmployeeId,
                o.ExpiryDate,
                o.ManufactureDate,
                o.MeasureTypeDicId,
                o.MeasureTypeDicValue,
                o.OwnerEmployeeId,
                o.Number,
                o.TmcTypeDicId,
                o.StorageDicValue,
                o.TmcTypeDicValue,
                o.StorageDicId,
                o.Serial,
                o.PackageDicId,
                o.PackageDicValue,
                o.Code,
                MainTmcId=o.Id
            });
            */

            var data = db.TmcViews.Where(o => o.StateType != 2 && o.CountActual > 0);
            if (!PW.Ncels.Database.Helpers.EmployePermissionHelper.IsViewTmcList)
            {
                var currentEmployeeId = UserHelper.GetCurrentEmployee().Id;
                data = data.Where(o => o.OwnerEmployeeId == currentEmployeeId || o.CreatedEmployeeId == currentEmployeeId);
            }
                
            data = data.OrderByDescending(m => m.Name);;
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTmcListFirst([DataSourceRequest] DataSourceRequest request, TmcView dictionary)
        {
            Tmc tmc = new Tmc()
            {
                Id = Guid.NewGuid(),
                Name = dictionary.Name,
                Count = dictionary.Count,
                //CountActual = dictionary.CountActual,
                //CountFact = dictionary.CountFact,
                // OwnerEmployeeValue = dictionary.OwnerEmployeeValue,
                StateType = 1,
                Rack = dictionary.Rack,
                Safe = dictionary.Safe,
                MeasureTypeConvertDicId = dictionary.MeasureTypeConvertDicId,
                //MeasureTypeConvertDicValue = dictionary.MeasureTypeConvertDicValue,
                //StateTypeValue = dictionary.StateTypeValue,
                CountConvert = dictionary.CountConvert,
                Manufacturer = dictionary.Manufacturer,
                CreatedDate = DateTime.Now, //dictionary.CreatedDate,
                CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id, //dictionary.CreatedEmployeeId,
                ExpiryDate = dictionary.ExpiryDate,
                ManufactureDate = dictionary.ManufactureDate,
                MeasureTypeDicId = dictionary.MeasureTypeDicId,
                //MeasureTypeDicValue = dictionary.MeasureTypeDicValue,
                OwnerEmployeeId = dictionary.OwnerEmployeeId,
                Number = dictionary.Number,
                TmcTypeDicId = dictionary.TmcTypeDicId,
                //StorageDicValue = dictionary.StorageDicValue,
                //TmcTypeDicValue = dictionary.TmcTypeDicValue,
                StorageDicId = dictionary.StorageDicId,
                Serial = dictionary.Serial,
                PackageDicId = dictionary.PackageDicId,
                //PackageDicValue = dictionary.PackageDicValue,
                Code = dictionary.Code,
            };

            db.Tmcs.Add(tmc);
            db.SaveChanges();
            dictionary = db.TmcViews.First(o => o.Id == tmc.Id);
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateTmcListFirst([DataSourceRequest] DataSourceRequest request, TmcView dictionary)
        {
            Tmc d = db.Tmcs.First(o => o.Id == dictionary.Id);
            d.Name = dictionary.Name;
            d.Count = dictionary.Count;
            //d.CountActual = dictionary.CountActual;
            //d.CountFact = dictionary.CountFact;
            // d.OwnerEmployeeValue = dictionary.OwnerEmployeeValue;
            //d.StateType = dictionary.StateType;
            d.Rack = dictionary.Rack;
            d.Safe = dictionary.Safe;
            //d.MeasureTypeConvertDicId = dictionary.MeasureTypeConvertDicId;
            //d.MeasureTypeConvertDicValue = dictionary.MeasureTypeConvertDicValue;
            //d.StateTypeValue = dictionary.StateTypeValue;
            //d.CountConvert = dictionary.CountConvert;
            d.Manufacturer = dictionary.Manufacturer;
            //d.CreatedDate = dictionary.CreatedDate;
            //d.CreatedEmployeeId = dictionary.CreatedEmployeeId;
            d.ExpiryDate = dictionary.ExpiryDate;
            d.ManufactureDate = dictionary.ManufactureDate;
            d.MeasureTypeDicId = dictionary.MeasureTypeDicId;
            //d.MeasureTypeDicValue = dictionary.MeasureTypeDicValue;
            d.OwnerEmployeeId = dictionary.OwnerEmployeeId;
            d.Number = dictionary.Number;
            d.TmcTypeDicId = dictionary.TmcTypeDicId;
            //d.StorageDicValue = dictionary.StorageDicValue;
            //d.TmcTypeDicValue = dictionary.TmcTypeDicValue;
            d.StorageDicId = dictionary.StorageDicId;
            d.Serial = dictionary.Serial;
            d.PackageDicId = dictionary.PackageDicId;
            //d.PackageDicValue = dictionary.PackageDicValue;
            d.Code = dictionary.Code;
            db.SaveChanges();

            dictionary = db.TmcViews.First(o => o.Id == d.Id);
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyTmcListFirst([DataSourceRequest] DataSourceRequest request, TmcView dictionary)
        {
            if (dictionary != null)
            {
                TmcView d = db.TmcViews.First(o => o.Id == dictionary.Id);
                db.TmcViews.Remove(d);
                db.SaveChanges();
            }
            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
        
        public FileStreamResult ExportFile()
        {
            StiReport report = new StiReport();

            report.Load(Server.MapPath("../Reports/TmcList.mrt"));
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }

            if (report.Dictionary.Variables.Contains("OrganizationId"))
            {
                report.Dictionary.Variables["OrganizationId"].ValueObject = UserHelper.GetCurrentEmployee().OrganizationId;
            }

            report.Render(false);
            var stream = new MemoryStream();

            report.ExportDocument(StiExportFormat.Excel, stream);
            stream.Position = 0;

            return File(stream, "application/excel", "Список Реактивов.xls");
        }

        #endregion

        #region TmcListSecond
        public ActionResult ReadTmcListSecond([DataSourceRequest] DataSourceRequest request, Guid tmcOutId)
        {
            var data = db.TmcOutCountViews.OrderByDescending(m => m.Name).Where(m => m.TmcOutId == tmcOutId).Select(o => new
            {
                o.Id,
                o.Name,
                o.Count,
                o.Note,
                o.CountFact,
                o.OwnerEmployeeValue,
                o.StateType,
                o.Rack,
                o.Safe,
                o.MeasureTypeConvertDicId,
                o.MeasureTypeConvertDicValue,
                o.StateTypeValue,
                o.TmcId,
                o.StorageDicValue,
                TmcOutId = o.Id
            });
            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateTmcListSecond([DataSourceRequest] DataSourceRequest request, TmcOutCountViewModel dictionary)
        {

            TmcOutCount tmc = new TmcOutCount()
            {
                Id = Guid.NewGuid(),
                StateType = dictionary.StateType,
                Note = dictionary.Note,
                CountFact = dictionary.CountFact,
                Count = dictionary.Count,
                TmcOutId = Guid.Parse(dictionary.TmcOutIdString)
            };

            db.TmcOutCounts.Add(tmc);
            db.SaveChanges();
            dictionary.Id = tmc.Id;


            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateTmcListSecond([DataSourceRequest] DataSourceRequest request, TmcOutCountViewModel dictionary)
        {

            TmcOutCount d = db.TmcOutCounts.First(o => o.Id == dictionary.Id);
            d.StateType = dictionary.StateType;
            d.Note = dictionary.Note;
            d.Count = dictionary.Count;
            d.CountFact = dictionary.CountFact;
            db.SaveChanges();


            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyTmcListSecond([DataSourceRequest] DataSourceRequest request, TmcOutCountView dictionary)
        {
            if (dictionary != null)
            {
                TmcOutCount d = db.TmcOutCounts.First(o => o.Id == dictionary.Id);
                db.TmcOutCounts.Remove(d);
                db.SaveChanges();
            }

            return Json(new[] { dictionary }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region Agreement 

        public ActionResult RequestAgreementList()
        {
            Guid guid = Guid.NewGuid();
            return PartialView(guid);
        }

        public ActionResult SendAgreement(Guid id, string url)
        {
            return PartialView(
                new SendAgreementViewModel()
                {
                    Id = id,
                    Url = url
                });
        }

        [HttpPost]
        public ActionResult ConfirmAgreement(Guid id)
        {
            var tmcin = db.TmcIns.First(m => m.Id == id);
            if (tmcin.StateType == 12)
            {
                tmcin.StateType = 1;
            }
            else if ( new[] { 10, 11 }.Contains(tmcin.StateType))
            {
                tmcin.StateType = tmcin.StateType + 1;
            }
            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult RejectAgreement(Guid id, int type, string url)
        {
            var createEmployeeId = UserHelper.GetCurrentEmployee().Id;
//            var applicationComment = db.TmcApplicationComments
//                .Where(a => a.ApplicationId == id)
//                .OrderByDescending(a => a.CreateDate)
//                .FirstOrDefault();
            var applicationComment = new TmcApplicationComment()
            {
                Id = Guid.NewGuid(),
                ApplicationId = id,
                CreateEmployeeId = createEmployeeId,
                Type = type,
                Url = url
            };
            
            return PartialView(applicationComment);
        }

        [HttpPost]
        public ActionResult ConfirmRejectAgreement(TmcApplicationComment comment)
        {
            var commentApp = db.TmcApplicationComments.FirstOrDefault(m => m.Id == comment.Id);

            if (commentApp == null)
            {
                commentApp = comment;
                db.TmcApplicationComments.Add(commentApp);
            }
            else
            {
                commentApp.Comment = comment.Comment;
            }


            if (comment.Type == 1)
            {
                var tmcIn = db.TmcIns.First(m => m.Id == comment.ApplicationId);
                tmcIn.StateType = -1;
            }
            else if (comment.Type == 2)
            {
                var tmcOut = db.TmcOuts.First(m => m.Id == comment.ApplicationId);
                tmcOut.StateType = -1;
            }
            
            db.SaveChanges();
            return Json(comment.Id, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Report List

        public ActionResult TmcReportList()
        {
            Guid guid = Guid.NewGuid();
            ViewBag.CurrentUserId = UserHelper.GetCurrentEmployee().Id;

            return PartialView(guid);
        }

        public ActionResult ReadTmcReportList([DataSourceRequest] DataSourceRequest request)
        {
            var user = UserHelper.GetCurrentEmployee();

            var qr = db.TmcReportsViews.Where(o => (o.ExecutorEmployeeId == user.Id && o.Stage == 1)
             || (o.CreateEmployeeId == user.Id));
            var data = qr.OrderByDescending(m => m.CreatedDate);
            ViewBag.CurrentUserId = user.Id;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmReportAgreement(Guid id)
        {
            var report = db.TmcReportTasks.First(m => m.refTmcReport == id);
            if (report.State == 0)
            {
                report.State = 1; // согласован
                report.Stage++; // следующий этап
                report.Operation = OperatorConsts.AgreedCode;
                //if (!string.IsNullOrEmpty(task.Note))
                //{
                //    Employee user = UserHelper.GetCurrentEmployee();
                //    Comment comment = new Comment()
                //    {
                //        Id = Guid.NewGuid(),
                //        CreatedDate = DateTime.Now,
                //        Value = task.Note,
                //        AuthorId = user.Id,
                //        AuthorValue = user.DisplayName,
                //        refObjectId = task.Id,
                //        refParentComment = null
                //    };
                //    db.Comments.Add(comment);
                //}
            }
            db.SaveChanges();
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmRejectReportAgreement(TmcApplicationComment comment)
        {
//            var commentApp = db.TmcApplicationComments.FirstOrDefault(m => m.Id == comment.Id);
//            if (commentApp == null)
//            {
//                commentApp = comment;
//                db.TmcApplicationComments.Add(commentApp);
//            }
//            else
//            {
//                commentApp.Comment = comment.Comment;
//            }

            var report = db.TmcReportTasks.First(m => m.Id == comment.ApplicationId);
            if (report.State == 0)
            {
                report.State = -1; //отклонить
                report.Note = comment.Comment;
            }
            db.SaveChanges();
            return Json(comment.Id, JsonRequestBehavior.AllowGet);
        }

        #endregion


        public ActionResult ListOfDrugDeclarationOnAnaliticStage()
        {
            IQueryable<EXP_ExpertiseStage> q = db.EXP_ExpertiseStage
                .Where(s => !s.IsHistory && s.EXP_DIC_Stage.Code == CodeConstManager.STAGE_ANALITIC.ToString());
            var analiticStages = q.Select(o => new
            {
                Id = o.EXP_DrugDeclaration.Id,
                Name = o.EXP_DrugDeclaration.Number + " - "  + o.EXP_DrugDeclaration.NameRu
            }).ToList();

            return Json(analiticStages, JsonRequestBehavior.AllowGet);
        }
    }
}