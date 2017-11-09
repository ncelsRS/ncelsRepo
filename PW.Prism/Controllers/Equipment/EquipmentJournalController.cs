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
    public class EquipmentJournalController : Controller
    {
        // GET: EquipmentJournal
        public ActionResult Index()
        {
            Guid id = Guid.NewGuid();
            return PartialView(id);
        }

        /// <summary>
        /// Получить список Элементов Журнала Заявок
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult ReadApplicationJournalList([DataSourceRequest] DataSourceRequest request)
        {
            EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);

            var qr = eRepository.GetAsQuarable(e => e.DeleteDate == null)
                .Include(e => e.LimsEquipment)
                .Include(e => e.LimsEquipment.LaboratoryDic)
                .Include(e => e.LimsEquipment.LocationDic)
                .Include(e => e.AccepterEmp)
                .Include(e => e.EngineerEmp)
                .Include(e => e.ApplicationEmp);
            
            qr = qr.OrderBy(m => m.CreateDate);

            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadApplicationJournalViewList([DataSourceRequest] DataSourceRequest request)
        {
            EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);

            var qr = eRepository.GetAsQueryableApplicationJournalViews(e => e.DeleteDate == null);
            qr = qr.OrderBy(m => m.CreateDate);
            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateApplicationJournal([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Id")]LimsApplicationJournal model)
        {
            if (ModelState.IsValid)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);
                model.Id = Guid.NewGuid();
                model.CreateDate = DateTime.Now;
                eRepository.Insert(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateApplicationJournal([DataSourceRequest] DataSourceRequest request, LimsApplicationJournal model)
        {
            if (ModelState.IsValid)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);

                eRepository.Update(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyApplicationJournal([DataSourceRequest] DataSourceRequest request, LimsApplicationJournal model)
        {
            if (model != null)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);
                eRepository.Delete(model.Id);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        


        /// <summary>
        /// Журналы Проверок/Профилактических работ/Квалификации
        /// </summary>
        /// <returns></returns>
        public ActionResult EquipmentJournal(string journalType)
        {
            DictionaryRepository dicRepository = new DictionaryRepository(false);

            Guid id = Guid.NewGuid();
            /*
            ViewBag.EquipmentJournalType =
                dicRepository.GetAsQuarable().Where(o => o.Type == Dictionary.DicEquipmentJournalType.DicCode && o.ExpireDate == null)
                    .ToList().OrderBy(o => o.Name)
                    .Select(o => new Item() { Id = o.Id.ToString(), Name = o.Name }).ToList();
                    */
            ViewBag.JournalTypeCode = journalType;
            ViewBag.JournalTypeId = dicRepository.GetAsQuarable()
                .Where(o =>
                    o.Type == Dictionary.DicEquipmentJournalType.DicCode && o.ExpireDate == null &&
                    o.Code == journalType).Select(o => o.Id).FirstOrDefault();

            EquipmentRepository eRepository = new EquipmentRepository(false);
            var equipment = eRepository.GetAsQuarable(e => e.DeleteDate == null).FirstOrDefault();
            if (equipment != null)
                ViewBag.EquipmentDefaultId = equipment.Id;
            
            if (journalType == Dictionary.DicEquipmentJournalType.PreventiveWorkJournal)
            {
                ViewBag.Header1 = "Дата замены расходных материалов";
                ViewBag.Header2 = "Дата следующей замены расходных материалов";
                ViewBag.Header3 = "Дополнительные работы";
                ViewBag.Header4 = "Результаты профилактических работ";
            }
            else if(journalType == Dictionary.DicEquipmentJournalType.CheckJournal)
            {
                ViewBag.Header1 = "Дата поверки";
                ViewBag.Header2 = "Дата следующей поверки";
                ViewBag.Header3 = "Номер сертификата";
                ViewBag.Header4 = "Результаты поверки";

                return PartialView("CheckEquipmentJournal", id);
            }
            else if (journalType == Dictionary.DicEquipmentJournalType.QualificationJournal)
            {
                ViewBag.Header1 = "Дата квалификации";
                ViewBag.Header2 = "Дата следующей квалификации";
                ViewBag.Header3 = "Дополнительные работы";
                ViewBag.Header4 = "Результаты квалификации";
                return PartialView("QEquipmentJournal", id);
            }

            

            return PartialView(id);
        }

        public ActionResult ReadJournalViewList([DataSourceRequest] DataSourceRequest request, string journalTypeCode)
        {
            EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);

            var qr = eRepository.GetAsQueryableEquipmentJournals(e => e.DeleteDate == null && e.JournalTypeDic.Code == journalTypeCode)
                .Include(e => e.JournalTypeDic);
            qr = qr.OrderBy(m => m.CreateDate);
            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateEquipmentJournal([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Id")]LimsEquipmentJournal model)
        {
            if (ModelState.IsValid)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);
                model.Id = Guid.NewGuid();
                model.CreateDate = DateTime.Now;
                eRepository.InsertEquipmentJournal(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateEquipmentJournal([DataSourceRequest] DataSourceRequest request, LimsEquipmentJournal model)
        {
            if (ModelState.IsValid)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);

                eRepository.UpdateEquipmentJournal(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyEquipmentJournal([DataSourceRequest] DataSourceRequest request, LimsEquipmentJournal model)
        {
            if (model != null)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);
                eRepository.DeleteEquipmentJournal(model.Id);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        
        public ActionResult ReadJournalRecordList([DataSourceRequest] DataSourceRequest request, Guid eqipmentJournalId)
        {
            EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);

            var qr = eRepository.GetAsQueryableEquipmentJournalRecordViews(e => e.DeleteDate == null && e.JournalId == eqipmentJournalId);

            qr = qr.OrderBy(m => m.CreateDate);
            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateEquipmentJournalRecord([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Id")]LimsEquipmentJournalRecord model)
        {
            if (ModelState.IsValid)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);
                model.Id = Guid.NewGuid();
                model.CreateDate = DateTime.Now;
                eRepository.InsertEquipmentJournalRecord(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateEquipmentJournalRecord([DataSourceRequest] DataSourceRequest request, LimsEquipmentJournalRecord model)
        {
            if (ModelState.IsValid)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);

                eRepository.UpdateEquipmentJournalRecords(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyEquipmentJournalRecord([DataSourceRequest] DataSourceRequest request, LimsEquipmentJournalRecord model)
        {
            if (model != null)
            {
                EquipmentJournalRepository eRepository = new EquipmentJournalRepository(false);
                eRepository.DeleteEquipmentJournalRecord(model.Id);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}