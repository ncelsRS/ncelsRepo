using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.Equipment;
using PW.Ncels.Database.Repository.Lims;
using PW.Ncels.Database.Repository.Security;

namespace PW.Prism.Controllers.Equipment
{
    public class EquipmentController : Controller
    {
        // GET: Equipment
        public ActionResult Index()
        {
            DictionaryRepository dicRepository = new DictionaryRepository(false);

            Guid id = Guid.NewGuid();

            ViewBag.EquipmentLocation =
                dicRepository.GetAsQuarable().Where(o => o.Type == Dictionary.DicEquipmentLab.DicCode && o.ExpireDate == null)
                    .ToList().OrderBy(o => o.Name)
                    .Select(o => new Item() { Id = o.Id.ToString(), Name = o.Name }).ToList();

            ViewBag.UiId = Guid.NewGuid();

            return PartialView(id);
        }

        /// <summary>
        /// Получить список оборудования
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult ReadEquipmentList([DataSourceRequest] DataSourceRequest request)
        {
            EquipmentRepository eRepository = new EquipmentRepository(false);
            
            var qr = eRepository.GetAsQuarable(e => e.DeleteDate == null)
                .Include(e => e.ResponsiblePersonEmp)
                .Include(e => e.LaboratoryDic)
                .Include(e => e.CountryProductionDic)
                .Include(e => e.EquipmentTypeDic)
                .Include(e => e.LocationDic)
                .Include(e => e.ModelDic)
                .Include(e => e.ProducerDic)
                .Include(e => e.StatusDic);
            qr = qr.OrderBy(m => m.CreateDate);

            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateEquipment([DataSourceRequest] DataSourceRequest request, [Bind(Exclude = "Id")]LimsEquipment model)
        {
            if (ModelState.IsValid)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);
                model.Id = Guid.NewGuid();
                model.CreateDate = DateTime.Now;
                eRepository.Insert(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateEquipment([DataSourceRequest] DataSourceRequest request, LimsEquipment model)
        {
            if (ModelState.IsValid)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);

                eRepository.Update(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyEquipment([DataSourceRequest] DataSourceRequest request, LimsEquipment model)
        {
            if (model != null)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);
                eRepository.Delete(model.Id);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GetEquipmentList()
        {
            EquipmentRepository eRepository = new EquipmentRepository(false);

            var dataDic = eRepository.GetAsQuarable().Where(o => o.DeleteDate == null)
                .OrderBy(o => o.Name).ToList();
            return Json(dataDic.Select(o => new { o.Id, Name = o.Name }), JsonRequestBehavior.AllowGet);
        }
        

        /// <summary>
        /// Получить список актов
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult ReadEquipmentActList([DataSourceRequest] DataSourceRequest request, Guid equipmentId)
        {
            EquipmentRepository eRepository = new EquipmentRepository(false);

            var qr = eRepository.GetLimsEquipmentActs(e => e.EquipmentId == equipmentId && e.DeleteDate == null)
                .Include(e => e.ActTypeDic)
                .Include(e => e.DirectorRcEmp)
                .Include(e => e.HeadOfLaboratoryEmp)
                .Include(e => e.EngineerEmp)
                .Include(e => e.LimsEquipment)
                .Include(e => e.LimsEquipment.CountryProductionDic)
                .Include(e => e.LimsEquipment.ResponsiblePersonEmp)
                .Include(e => e.LimsEquipment.LaboratoryDic)
                .Include(e => e.LimsEquipment.EquipmentTypeDic)
                .Include(e => e.LimsEquipment.LocationDic)
                .Include(e => e.LimsEquipment.ModelDic)
                .Include(e => e.LimsEquipment.ProducerDic)
                .Include(e => e.LimsEquipment.StatusDic);
            qr = qr.OrderBy(m => m.CreateDate);

            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        /*
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateEquipmentAct([DataSourceRequest] DataSourceRequest request, LimsEquipmentAct model)
        {
            if (ModelState.IsValid)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);

                eRepository.InsertLimsEquipmentAct(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateEquipmentAct([DataSourceRequest] DataSourceRequest request, LimsEquipmentAct model)
        {
            if (ModelState.IsValid)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);

                eRepository.UpdateLimsEquipmentAct(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        */
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyEquipmentAct([DataSourceRequest] DataSourceRequest request, LimsEquipmentAct model)
        {
            if (model != null)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);
                eRepository.DeleteLimsEquipmentAct(model.Id);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
        
       
        /// <summary>
        /// Акт консервации
        /// </summary>
        /// <param name="id"></param>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public PartialViewResult ActOfConversationForm(Guid? id, Guid? equipmentId)
        {
            ViewBag.UiId = Guid.NewGuid().ToString();

            EquipmentRepository eRepository = new EquipmentRepository();
            LimsEquipmentAct act = new LimsEquipmentAct();
            act.Id = Guid.Empty;

            if (equipmentId.HasValue && !id.HasValue)
            {
                var equipment = eRepository.GetAsQuarable(e => e.Id == equipmentId)
                    .FirstOrDefault();
                act.LimsEquipment = equipment;
                act.EquipmentId = equipmentId.Value;
            }
            else if (id.HasValue)
            {
                act = eRepository.GetLimsEquipmentActs(ea => ea.Id == id).FirstOrDefault();
            }

            return PartialView("ActOfConversationView", act);
        }

        [HttpPost]
        public JsonResult SaveActOfConversation(LimsEquipmentAct model)
        {
            string msg = string.Empty;
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    EquipmentRepository eRepository = new EquipmentRepository();
                    DictionaryRepository dicRepo = new DictionaryRepository(false);
                    EmployeesRepository empRepo = new EmployeesRepository(false);
                    if (model.Id == Guid.Empty)
                    {
                        model.Id = Guid.NewGuid();
                        model.CreateDate = DateTime.Now;

                        Guid? actTypeId = dicRepo.GetDictionaryElementIdByTypeAndCode(Dictionary.DicEquipmentAct.DicCode,
                            Dictionary.DicEquipmentAct.ActOfConversation);
                        if (actTypeId != null) model.ActTypeId = actTypeId.Value;
                        
                        var headOfLab = empRepo.GetById(model.HeadOfLaboratoryId);
                        if (headOfLab != null) model.HeadOfLaboratoryName = headOfLab.FullName;
                        var director = empRepo.GetById(model.DirectorRCId);
                        if (director != null) model.DirectorRCName = director.FullName;

                        eRepository.InsertLimsEquipmentAct(model);
                    }
                    else
                    {
                        var headOfLab = empRepo.GetById(model.HeadOfLaboratoryId);
                        if (headOfLab != null) model.HeadOfLaboratoryName = headOfLab.FullName;
                        var director = empRepo.GetById(model.DirectorRCId);
                        if (director != null) model.DirectorRCName = director.FullName;

                        eRepository.UpdateLimsEquipmentAct(model);
                    }
                    eRepository.Save();
                }
                catch (Exception e)
                {
                    msg = e.ToString();
                }
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAct(Guid id)
        {
            if (id != Guid.Empty)
            {
                EquipmentRepository eRepository = new EquipmentRepository();
                eRepository.DeleteLimsEquipmentAct(id);
            }
            return Json(id, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Акт установки запасных частей
        /// </summary>
        /// <param name="id"></param>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public PartialViewResult ActOfSparePartsForm(Guid? id, Guid? equipmentId)
        {
            ViewBag.UiId = Guid.NewGuid().ToString();

            EquipmentRepository eRepository = new EquipmentRepository();
            LimsEquipmentAct act = new LimsEquipmentAct();
            act.Id = Guid.Empty;
            if (equipmentId.HasValue && !id.HasValue)
            {
                act.Id = Guid.NewGuid();
                act.IsNew = true;
                var equipment = eRepository.GetAsQuarable(e => e.Id == equipmentId)
                    .FirstOrDefault();
                act.LimsEquipment = equipment;
                act.EquipmentId = equipmentId.Value;
            }
            else if (id.HasValue)
            {
                act = eRepository.GetLimsEquipmentActs(ea => ea.Id == id).FirstOrDefault();
                if (act != null) act.IsNew = false;
            }

            return PartialView("ActOfSparePartsView", act);
        }

        [HttpPost]
        public JsonResult SaveActOfSpareParts(LimsEquipmentAct model)
        {
            string msg = string.Empty;
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    EquipmentRepository eRepository = new EquipmentRepository(false);
                    DictionaryRepository dicRepo = new DictionaryRepository(false);
                    EmployeesRepository empRepo = new EmployeesRepository(false);
                    var act = eRepository.GetLimsEquipmentActs(a => a.Id == model.Id).FirstOrDefault();

                    if (act == null)
                    {
                        model.Id = Guid.NewGuid();
                        model.CreateDate = DateTime.Now;

                        Guid? actTypeId = dicRepo.GetDictionaryElementIdByTypeAndCode(Dictionary.DicEquipmentAct.DicCode,
                            Dictionary.DicEquipmentAct.ActOfSpareParts);
                        if (actTypeId != null) model.ActTypeId = actTypeId.Value;

                        var headOfLab = empRepo.GetById(model.HeadOfLaboratoryId);
                        if (headOfLab != null) model.HeadOfLaboratoryName = headOfLab.FullName;
                        // var director = empRepo.GetById(model.DirectorRCId);
                        // model.DirectorRCName = director.FullName;
                        var engineer = empRepo.GetById(model.EngineerId);
                        if (engineer != null) model.EngineerName = engineer.FullName;

                        eRepository.InsertLimsEquipmentAct(model);
                    }
                    else
                    {
                        act.HeadOfLaboratoryId = model.HeadOfLaboratoryId;
                        var headOfLab = empRepo.GetById(model.HeadOfLaboratoryId);
                        if (headOfLab != null) act.HeadOfLaboratoryName = headOfLab.FullName;

                        act.EngineerId = model.EngineerId;
                        var engineer = empRepo.GetById(model.EngineerId);
                        if (engineer != null) act.EngineerName = engineer.FullName;

                        eRepository.UpdateLimsEquipmentAct(act);
                    }
                    eRepository.Save();
                }
                catch (Exception e)
                {
                    msg = e.ToString();
                }
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Акт установки запасных частей
        /// </summary>
        /// <param name="request"></param>
        /// <param name="actId"></param>
        /// <returns></returns>
        public ActionResult ReadEquipmentActSparePartList([DataSourceRequest] DataSourceRequest request, Guid actId)
        {
            EquipmentRepository eRepository = new EquipmentRepository(false);

            var qr = eRepository.GetLimsEquipmentActSpareParts(e => e.DeleteDate == null && e.EquipmentActId == actId)
                .Include(e => e.LocationDic);

            qr = qr.OrderBy(m => m.CreatedDate);

            var data = qr;

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateSparePart([DataSourceRequest] DataSourceRequest request,
            [Bind(Exclude = "Id")] LimsEquipmentActSparePart model, Guid actId, Guid equipmentId)
        {
            if (ModelState.IsValid)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);
                var act = eRepository.GetLimsEquipmentActs(a => a.Id == actId).FirstOrDefault();
                if (act == null)
                {
                    act = new LimsEquipmentAct()
                    {
                        Id = actId,
                        CreateDate = DateTime.Now,
                        EquipmentId = equipmentId
                    };

                    DictionaryRepository dicRepo = new DictionaryRepository(false);
                    Guid? actTypeId = dicRepo.GetDictionaryElementIdByTypeAndCode(Dictionary.DicEquipmentAct.DicCode,
                        Dictionary.DicEquipmentAct.ActOfSpareParts);
                    if (actTypeId != null) act.ActTypeId = actTypeId.Value;

                    eRepository.InsertLimsEquipmentAct(act);
                    eRepository.Save();
                }
                model.EquipmentActId = act.Id;


                model.Id = Guid.NewGuid();
                model.CreatedDate = DateTime.Now;

                eRepository.InsertLimsEquipmentActSparePart(model);
                eRepository.Save();
            }
            return Json(new[] {model}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateSparePart([DataSourceRequest] DataSourceRequest request, LimsEquipmentActSparePart model, Guid actId, Guid equipmentId)
        {
            if (ModelState.IsValid)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);

                eRepository.UpdateLimsEquipmentActSparePart(model);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroySparePart([DataSourceRequest] DataSourceRequest request, LimsEquipmentActSparePart model)
        {
            if (model != null)
            {
                EquipmentRepository eRepository = new EquipmentRepository(false);
                eRepository.DeleteLimsEquipmentActSparePart(model.Id);
                eRepository.Save();
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        
        /// <summary>
        /// Дефектный акт для ремонта
        /// </summary>
        /// <param name="id"></param>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public PartialViewResult ActForRepairForm(Guid? id, Guid? equipmentId)
        {
            ViewBag.UiId = Guid.NewGuid().ToString();

            EquipmentRepository eRepository = new EquipmentRepository();
            LimsEquipmentAct act = new LimsEquipmentAct();
            act.Id = Guid.Empty;
            if (equipmentId.HasValue && !id.HasValue)
            {
                var equipment = eRepository.GetAsQuarable(e => e.Id == equipmentId)
                    .FirstOrDefault();
                act.LimsEquipment = equipment;
                act.EquipmentId = equipmentId.Value;
            }
            else if (id.HasValue)
            {
                act = eRepository.GetLimsEquipmentActs(ea => ea.Id == id).FirstOrDefault();
            }

            return PartialView("ActForRepairView", act);
        }

        [HttpPost]
        public JsonResult SaveActForRepair(LimsEquipmentAct model)
        {
            string msg = string.Empty;
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    EquipmentRepository eRepository = new EquipmentRepository();
                    DictionaryRepository dicRepo = new DictionaryRepository(false);
                    EmployeesRepository empRepo = new EmployeesRepository(false);
                    var act = eRepository.GetLimsEquipmentActs(a => a.Id == model.Id).FirstOrDefault();

                    if (act == null)
                    {
                        model.Id = Guid.NewGuid();
                        model.CreateDate = DateTime.Now;

                        Guid? actTypeId = dicRepo.GetDictionaryElementIdByTypeAndCode(Dictionary.DicEquipmentAct.DicCode,
                            Dictionary.DicEquipmentAct.ActForRepair);
                        if (actTypeId != null) model.ActTypeId = actTypeId.Value;

                        var headOfLab = empRepo.GetById(model.HeadOfLaboratoryId);
                        if (headOfLab != null) model.HeadOfLaboratoryName = headOfLab.FullName;
                        var engineer = empRepo.GetById(model.EngineerId);
                        if (engineer != null) model.EngineerName = engineer.FullName;

                        eRepository.InsertLimsEquipmentAct(model);
                    }
                    else
                    {
                        act.State = model.State;
                        act.Reason = model.Reason;

                        act.HeadOfLaboratoryId = model.HeadOfLaboratoryId;
                        var headOfLab = empRepo.GetById(model.HeadOfLaboratoryId);
                        if (headOfLab != null) act.HeadOfLaboratoryName = headOfLab.FullName;

                        act.EngineerId = model.EngineerId;
                        var engineer = empRepo.GetById(model.EngineerId);
                        if (engineer != null) act.EngineerName = engineer.FullName;

                        eRepository.UpdateLimsEquipmentAct(act);
                    }
                    
                    eRepository.Save();
                }
                catch (Exception e)
                {
                    msg = e.ToString();
                }
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Протокол квалификации
        /// </summary>
        /// <param name="id"></param>
        /// <param name="equipmentId"></param>
        /// <returns></returns>
        public PartialViewResult ProtocolOfQualificationForm(Guid? id, Guid? equipmentId)
        {
            ViewBag.UiId = Guid.NewGuid().ToString();

            EquipmentRepository eRepository = new EquipmentRepository();
            LimsEquipmentAct act = new LimsEquipmentAct();
            act.Id = Guid.Empty;
            act.FileLinkData = new FileLink();
            if (equipmentId.HasValue && !id.HasValue)
            {
                var equipment = eRepository.GetAsQuarable(e => e.Id == equipmentId)
                    .FirstOrDefault();
                act.LimsEquipment = equipment;
                act.EquipmentId = equipmentId.Value;
            }
            else if (id.HasValue)
            {
                act = eRepository.GetLimsEquipmentActs(ea => ea.Id == id).FirstOrDefault();
                if (act != null)
                {
                    act.FileLinkData = eRepository.GetActualActFile(id.Value);
                    if (act.FileLinkData == null)
                    {
                        DictionaryRepository dicRepo = new DictionaryRepository(false);
                        act.FileLinkData = new FileLink();
                        act.FileLinkData.CategoryId = dicRepo.GetDictionaryElementIdByTypeAndCode(Dictionary.DicEquipmentAct.DicCode,
                                Dictionary.DicEquipmentAct.ProtocolOfQualification);
                        act.FileLinkData.DocumentId = act.Id;
                        act.FileLinkData.Id = Guid.NewGuid();
                    }
                    act.FileLinkData.AcceptFormats =
                        "application/msword, application/vnd.openxmlformats-officedocument.wordprocessingml.document, application/pdf, image/jpeg";
                }
            }

            return PartialView("ProtocolOfQualificationView", act);
        }

        [HttpPost]
        public JsonResult SaveProtocolOfQualification(LimsEquipmentAct model)
        {
            string msg = string.Empty;
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                try
                {
                    EquipmentRepository eRepository = new EquipmentRepository();
                    DictionaryRepository dicRepo = new DictionaryRepository(false);
                    if (model.Id == Guid.Empty)
                    {
                        model.Id = Guid.NewGuid();
                        model.CreateDate = DateTime.Now;

                        Guid? actTypeId = dicRepo.GetDictionaryElementIdByTypeAndCode(Dictionary.DicEquipmentAct.DicCode,
                            Dictionary.DicEquipmentAct.ProtocolOfQualification);
                        if (actTypeId != null) model.ActTypeId = actTypeId.Value;

                        eRepository.InsertLimsEquipmentAct(model);
                    }
                    else
                    {
                        eRepository.UpdateLimsEquipmentAct(model);
                    }
                    eRepository.Save();
                }
                catch (Exception e)
                {
                    msg = e.ToString();
                }
            }
            return Json(new { msg }, JsonRequestBehavior.AllowGet);
        }
    }
}