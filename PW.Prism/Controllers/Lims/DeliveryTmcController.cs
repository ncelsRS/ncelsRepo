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
    public class DeliveryTmcController : LimsBaseController
    {
        // GET: DeliveryTmc
        public PartialViewResult DeliveryTmcList()
        {
            OrderTmcListViewModel viewModel = new OrderTmcListViewModel()
            {
                Id = Guid.NewGuid()
            };

            ViewBag.StatusTypes = new List<Item>()
            {
                new Item()
                {
                    Id = TmcOut.TmcOutStatuses.Issued.ToString(),
                    Name = LimsTmcOutView.StateTypeValueStatic(TmcOut.TmcOutStatuses.Issued)
                },
                new Item()
                {
                    Id = TmcOut.TmcOutStatuses.Ordered.ToString(),
                    Name = LimsTmcOutView.StateTypeValueStatic(TmcOut.TmcOutStatuses.Ordered)
                },
                new Item()
                {
                    Id = TmcOut.TmcOutStatuses.Rejected.ToString(),
                    Name = LimsTmcOutView.StateTypeValueStatic(TmcOut.TmcOutStatuses.Rejected)
                }
            };
                
              //  repo.GetAsQuarable(o => o.Type == Dictionary.Dic && o.ExpireDate == null)
              //      .ToList().OrderBy(o => o.Name)
              //      .Select(o => new Item() { Id = o.Id.ToString(), Name = o.Name }).ToList();

            return PartialView(viewModel);
        }

        public JsonResult ReadData([DataSourceRequest] DataSourceRequest request)
        {
            OrderTmcRepository repository = new OrderTmcRepository();
            var qr = repository.LimsTmcOutViewGetAsQuarable();
            qr = base.FilterOwnerByCurrentUser(qr, repository.GetContext());
            qr = qr.OrderByDescending(m => m.CreatedDate);
            var data = qr.Where(lto => lto.StateType == TmcOut.TmcOutStatuses.Ordered 
            || lto.StateType == TmcOut.TmcOutStatuses.Issued || lto.StateType == TmcOut.TmcOutStatuses.Rejected);
            // || lto.StateType == TmcOut.TmcOutStatuses.Issued

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult DeliveryTmcIssueView(Guid id)
        {
            OrderTmcRepository repository = new OrderTmcRepository(false);
            var limsTmcOutModel = repository.LimsTmcOutViewGetAsQuarable(lto => lto.Id == id).FirstOrDefault();

            if (limsTmcOutModel != null)
            {
                TmcRepository tmcRepo = new TmcRepository(false);
                var tmcState = tmcRepo.LtaGetAsQuarable(lta => lta.Id == limsTmcOutModel.TmcId).FirstOrDefault();
                
                
                if (EmployeePermissionHelper.IsFrpCenterTmc)
                {
                    if (tmcState == null)
                    {
                        var tmcView = tmcRepo.TvGetAsQuarable(tv => tv.Id == limsTmcOutModel.TmcId).FirstOrDefault();

                        limsTmcOutModel.CountFact = tmcView != null
                            ? Math.Min(tmcView.CountActual, limsTmcOutModel.Count)
                            : limsTmcOutModel.Count;

                        limsTmcOutModel.CountActual = tmcView?.CountActual ?? 0;
                    }
                    else
                    {
                        limsTmcOutModel.CountFact = tmcState.TmcCountActual != null
                            ? Math.Min(tmcState.TmcCountActual.Value, limsTmcOutModel.Count)
                            : limsTmcOutModel.Count;

                        limsTmcOutModel.CountActual = tmcState.TmcCountActual ?? 0;
                    }
                }
                else
                {
                    limsTmcOutModel.CountFact = Math.Min(tmcState?.CountActual ?? 0, limsTmcOutModel.Count);

                    if (tmcState?.CountActual != null)
                        limsTmcOutModel.CountActual = tmcState.CountActual.Value;
                }
            }
            return PartialView(limsTmcOutModel);
        }

        #region Actions

        /// <summary>
        /// Выдать ТМЦ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult IssueOrder(LimsTmcOutView model)
        {
            string msg = string.Empty;
            try
            {
                OrderTmcRepository repository = new OrderTmcRepository();
                var tmcOut = repository.GetById(model.TmcOutId);
                tmcOut.StateType = TmcOut.TmcOutStatuses.Issued;
                repository.Update(tmcOut);

                var tmcOutCount = repository.TmcOutCountGetAsQuarable(toc => toc.Id == model.Id).FirstOrDefault();
                if (tmcOutCount != null)
                {
                    tmcOutCount.StateType = TmcOut.TmcOutStatuses.Issued;
                    tmcOutCount.CountFact = model.CountFact;
                    repository.TocUpdate(tmcOutCount);
                }

                if (EmployeePermissionHelper.IsFrpCenterTmc)
                {
                    UpdateTmcResidue(model.TmcId, model.CountFact);
                }
                
                repository.Save();
            }
            catch (Exception e)
            {
                msg = e.ToString();
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Отказать в выдаче ТМЦ
        /// </summary>
        /// <param name="tmcOutId">ид заказа</param>
        /// <param name="comment">комментарий</param>
        /// <returns></returns>
        public JsonResult RejectOrder(Guid tmcOutId, string comment)
        {
            string msg = ApplyAction(tmcOutId, TmcOut.TmcOutStatuses.Rejected, comment);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        /// <summary>
        /// Изменить остаток на складе ИЦ или главном складе
        /// </summary>
        /// <param name="tmcId"></param>
        /// <param name="countOut"></param>
        private void UpdateTmcResidue(Guid tmcId, decimal countOut)
        {
            TmcRepository tmcRepository = new TmcRepository(false);
            var tmc = tmcRepository.GetById(tmcId);
            tmc.CountActual = (tmc.CountActual == 0 ? tmc.CountConvert : tmc.CountActual) - countOut;
            tmcRepository.Update(tmc);
            tmcRepository.Save();
        }

    }
}