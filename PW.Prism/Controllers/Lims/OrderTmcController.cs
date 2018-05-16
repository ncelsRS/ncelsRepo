using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.Lims;
using PW.Prism.Controllers.Base;
using PW.Prism.ViewModels.Lims;

namespace PW.Prism.Controllers.Lims
{
    public class OrderTmcController : LimsBaseController
    {
        // GET: OrderTmc
        public PartialViewResult OrderTmcView(Guid tmcId, Guid? id = null, string note="", bool isEdit=false)
        {
            TmcRepository tmcRepository = new TmcRepository(false);
            LimsTmcOutView toc = new LimsTmcOutView()
            {
                Id = tmcId,
                TmcId = tmcId,
                TmcOutId = id ?? Guid.Empty
            };

            if (id == null)
            {
                
                var tmcOrdering = tmcRepository.GetAsQuarable(tmc => tmc.Id == tmcId).FirstOrDefault();
                if (tmcOrdering != null)
                {
                    toc.Name = tmcOrdering.Name;
                    toc.Count = tmcOrdering.CountActual;
                    toc.OwnerEmployeeId = tmcOrdering.OwnerEmployeeId;
                    toc.MeasureTypeConvertDicId = tmcOrdering.MeasureTypeConvertDicId;
                }

                DictionaryRepository dicRep = new DictionaryRepository(false);
                var outTypeId = dicRep.GetAsQuarable(d => d.Type == Dictionary.OutTypes.DicCode && d.ExpireDate == null)
                    .Select(d => d.Id)
                    .FirstOrDefault();
                toc.OutTypeDicId = outTypeId;
                toc.Note = note;
            }
            else
            {
                OrderTmcRepository otRepo = new OrderTmcRepository(false);
                toc = otRepo.LimsTmcOutViewGetAsQuarable(lto => lto.TmcId == tmcId && lto.TmcOutId == id).FirstOrDefault();

                var tmcOrdering = tmcRepository.GetAsQuarable(tmc => tmc.Id == tmcId).FirstOrDefault();
                if (tmcOrdering != null && toc != null)
                    toc.MeasureTypeConvertDicId = tmcOrdering.MeasureTypeConvertDicId;
            }
            if (toc != null)
            {
                toc.IsEdit = isEdit;
            }
            return PartialView(toc);
        }

        [HttpPost]
        public JsonResult ConfirmOrderTmc(LimsTmcOutView outCount)
        {
            OrderTmcRepository repository = new OrderTmcRepository(false);

            // берем у ТМЦ
            var ownerId = outCount.OwnerEmployeeId;
            if (outCount.OwnerEmployeeId == null)
            {
                var tmcRepo = new TmcRepository(false);
                ownerId = tmcRepo.GetAsQuarable(t => t.Id == outCount.TmcId).Select(t => t.OwnerEmployeeId).FirstOrDefault();
            }


            TmcOut tmcOut = new TmcOut()
            {
                Id = Guid.NewGuid(),
                Note = outCount.Note,
                CreatedDate = DateTime.Now,
                CreatedEmployeeId = UserHelper.GetCurrentEmployee().Id,
                StateType = TmcOut.TmcOutStatuses.Ordered,
                OwnerEmployeeId = ownerId,
                OutTypeDicId = outCount.OutTypeDicId
            };
            repository.Insert(tmcOut);
            
            TmcOutCount tmcOutCount = new TmcOutCount()
            {
                Id = Guid.NewGuid(),
                TmcId = outCount.TmcId,
                Count = outCount.Count,
                Note = outCount.Note,
                TmcOutId = tmcOut.Id
            };
            repository.TocInsert(tmcOutCount);

            repository.Save();
            return Json(outCount.TmcId, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult OrderTmcListView()
        {
            OrderTmcListViewModel viewModel = new OrderTmcListViewModel()
            {
                Id = Guid.NewGuid()
            };

            return PartialView(viewModel);
        }

        public JsonResult ReadData([DataSourceRequest] DataSourceRequest request)
        {
            OrderTmcRepository repository = new OrderTmcRepository();
            var qr = repository.LimsTmcOutViewGetAsQuarable();
            qr = base.FilterByCurrentUser(qr, repository.GetContext());
            qr = qr.OrderByDescending(m => m.CreatedDate);
            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #region Actions

        /// <summary>
        /// Подтверждение отправки 
        /// </summary>
        /// <param name="tmcOutId">ид заказа</param>
        /// <returns></returns>
        public JsonResult ConfirmSendOrder(Guid tmcOutId)
        {
            string msg = ApplyAction(tmcOutId, TmcOut.TmcOutStatuses.Ordered);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Аннулирован заказ ТМЦ
        /// </summary>
        /// <param name="tmcOutId">ид заказа</param>
        /// <returns></returns>
        public JsonResult RepealOrder(Guid tmcOutId)
        {
            string msg = ApplyAction(tmcOutId, TmcOut.TmcOutStatuses.Repealed);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Edit(Guid id)
        {
            OrderTmcRepository repository = new OrderTmcRepository(false);
            var model = repository.LimsTmcOutViewGetAsQuarable(lto => lto.Id == id).FirstOrDefault();
            return PartialView("OrderTmcView", model);
        }
        
        [HttpPost]
        public JsonResult Update(LimsTmcOutView model)
        {
            // if (ModelState.IsValid)
            // {
            OrderTmcRepository repository = new OrderTmcRepository();

            TmcOut m = repository.GetById(model.TmcOutId);
            m.Note = model.Note;
            m.OutTypeDicId = model.OutTypeDicId;
            m.OwnerEmployeeId = model.OwnerEmployeeId;
            repository.Update(m);

            TmcOutCount mcount = repository.TmcOutCountGetAsQuarable(toc => toc.Id == model.Id).FirstOrDefault();
            if (mcount != null)
            {
                mcount.Count = model.Count;
                repository.Update(m);
            }

            repository.Save();
           // }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(LimsTmcOutView model)
        {
            //if (ModelState.IsValid)
            //{
            string msg = string.Empty;
            try
            {
                OrderTmcRepository repository = new OrderTmcRepository();
                if (model.StateType == TmcOut.TmcOutStatuses.Repealed ||
                    model.StateType == TmcOut.TmcOutStatuses.Rejected ||
                    model.StateType == TmcOut.TmcOutStatuses.New)
                {
                    repository.Delete(model.TmcOutId);
                    repository.TocDelete(model.Id);
                }
                repository.Save();
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            //}

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}