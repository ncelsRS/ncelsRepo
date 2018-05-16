using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.Equipment;

namespace PW.Prism.Controllers.Equipment
{
    public class EquipmentPlanController : Controller
    {
        // GET: EquipmentPlan
        public ActionResult Index()
        {
            DictionaryRepository dicRepository = new DictionaryRepository(false);

            Guid id = Guid.NewGuid();

            ViewBag.EquipmentPlanType =
                dicRepository.GetAsQuarable().Where(o => o.Type == Dictionary.DicEquipmentPlanType.DicCode && o.ExpireDate == null)
                    .ToList().OrderBy(o => o.Name)
                    .Select(o => new Item() { Id = o.Id.ToString(), Name = o.Name }).ToList();

            ViewBag.PlanType = dicRepository.GetDictionaryElementIdByTypeAndCode(
                Dictionary.DicEquipmentPlanType.DicCode, Dictionary.DicEquipmentPlanType.CheckPlan);

            EquipmentRepository eRepository = new EquipmentRepository(false);
            var equipment = eRepository.GetAsQuarable(e => e.DeleteDate == null).FirstOrDefault();
            if (equipment != null)
                ViewBag.EquipmentDefaultId = equipment.Id;

            return PartialView(id);
        }

        /// <summary>
        /// Получить список планов
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult ReadEquipmentPlanList([DataSourceRequest] DataSourceRequest request)
        {
            EquipmentPlanRepository eRepository = new EquipmentPlanRepository(false);

            var qr = eRepository.GetAsQuarable(e => e.DeleteDate == null)
                .Include(e => e.PlanTypeDic);

            qr = qr.OrderBy(m => m.CreateDate);

            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateEquipmentPlan([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Id")]LimsEquipmentPlan model)
        {
            if (ModelState.IsValid)
            {
                EquipmentPlanRepository eRepository = new EquipmentPlanRepository(false);
                model.Id = Guid.NewGuid();
                model.CreateDate = DateTime.Now;
                eRepository.Insert(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateEquipmentPlan([DataSourceRequest] DataSourceRequest request, LimsEquipmentPlan model)
        {
            if (ModelState.IsValid)
            {
                EquipmentPlanRepository eRepository = new EquipmentPlanRepository(false);

                eRepository.Update(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyEquipmentPlan([DataSourceRequest] DataSourceRequest request, LimsEquipmentPlan model)
        {
            if (model != null)
            {
                EquipmentPlanRepository eRepository = new EquipmentPlanRepository(false);
                eRepository.Delete(model.Id);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        /// <summary>
        /// Получить список оборудования для Плана
        /// </summary>
        /// <param name="request"></param>
        /// <param name="eqipmentPlanId"></param>
        /// <returns></returns>
        public ActionResult ReadPlanEquipmentList([DataSourceRequest] DataSourceRequest request, Guid eqipmentPlanId)
        {
            EquipmentPlanRepository eRepository = new EquipmentPlanRepository(false);

            var qr = eRepository.GetLimsEquipmentLink(e => e.DeleteDate == null && e.EquipmentPlanId == eqipmentPlanId)
                .Include(e => e.LimsEquipment);
                //.Include(e => e.LimsEquipment.CountryProductionDic)
                //.Include(e => e.LimsEquipment.EquipmentTypeDic)
                //.Include(e => e.LimsEquipment.LaboratoryDic)
                //.Include(e => e.LimsEquipment.ModelDic)
                //.Include(e => e.LimsEquipment.ProducerDic)
                //.Include(e => e.LimsEquipment.LaboratoryDic);

            qr = qr.OrderBy(m => m.CreateDate);

            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreatePlanEquipment([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Id")]LimsPlanEquipmentLink model)
        {
            ModelState.Remove("EquipmentPlanId");
            if (ModelState.IsValid)
            {
                EquipmentPlanRepository eRepository = new EquipmentPlanRepository(false);
                model.Id = Guid.NewGuid();
                model.CreateDate = DateTime.Now;
                if (model.IsSignLocal)
                {
                    model.SignDate = DateTime.Now;
                }
                eRepository.InsertLimsEquipmentLink(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdatePlanEquipment([DataSourceRequest] DataSourceRequest request, LimsPlanEquipmentLink model)
        {
            ModelState.Remove("EquipmentPlanId");
            ModelState.Remove("CreateDate");
            ModelState.Remove("LimsEquipment.CreateDate");

            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    var e = error;
                }
            }

            if (ModelState.IsValid)
            {
                EquipmentPlanRepository eRepository = new EquipmentPlanRepository(false);
                if (model.IsSignLocal)
                {
                    model.SignDate = DateTime.Now;
                }
                eRepository.UpdateLimsEquipmentLink(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyPlanEquipment([DataSourceRequest] DataSourceRequest request, LimsPlanEquipmentLink model)
        {
            if (model != null)
            {
                EquipmentPlanRepository eRepository = new EquipmentPlanRepository(false);
                eRepository.DeleteLimsEquipmentLink(model.Id);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}