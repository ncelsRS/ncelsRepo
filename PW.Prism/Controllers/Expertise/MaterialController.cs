using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using PW.Ncels.Database.Repository.Expertise;
using Kendo.Mvc.Extensions;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Models.Material;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.DirectionToPay;

namespace PW.Prism.Controllers.Expertise
{
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MaterialListPartialVew(Guid? ddId)
        {
            DictionaryRepository repository = new DictionaryRepository();
            Guid? mpSample = repository.GetDictionaryElementIdByTypeAndCode(Dictionary.MaterialRdType.DicCode, Dictionary.MaterialRdType.MpSample);
            CreateMaterialViewModel model = new CreateMaterialViewModel()
            {
                DrugDeclarationId = ddId
            };
            if (mpSample.HasValue)
                model.TypeIdDefaultValue = mpSample.Value;

            return PartialView(model);
        }

        public JsonResult ReadMaterialList([DataSourceRequest] DataSourceRequest request, Guid? ddId)
        {
            MaterialRepository repository = new MaterialRepository(false);

            var data = repository.GetAsQuarable(mp => mp.DrugDeclarationId == ddId)
                .Include(mp => mp.Unit)
                .Include(mp => mp.DIC_Storages)
                .Include(mp => mp.ExternalState)
                .Include(mp => mp.EXP_DrugDeclaration)
                .Include(mp => mp.EXP_DrugDeclaration.EXP_DIC_Type);

            MaterialDirectionRepository drep = new MaterialDirectionRepository(false);
            var direction = drep.GetAsQuarable(d => d.DrugDeclarationId == ddId).FirstOrDefault();

            return Json(data.ToDataSourceResult(request, m => new EXP_Materials()
            {
                Id = m.Id,
                RegistrationDate = m.RegistrationDate,
                CreatedDate = m.CreatedDate,
                DeleteDate = m.DeleteDate,
                Name = m.Name,
                TypeId = m.TypeId,
                DrugFormId = m.DrugFormId,
                Dosage = m.Dosage,
                DosageUnitId = m.DosageUnitId,
                DosageQuantity = m.DosageQuantity,
                Concentration = m.Concentration,
                Volume = m.Volume,
                VolumeUnitId = m.VolumeUnitId,
                IsContainNPP = m.IsContainNPP,
                ProducerId = m.ProducerId,
                CountryId = m.CountryId,
                Quantity = m.Quantity,
                UnitId = m.UnitId,
                Batch = m.Batch,
                DateOfManufacture = m.DateOfManufacture,
                ExpirationDate = m.ExpirationDate,
                RetestDate = m.RetestDate,
                IsCertificatePassport = m.IsCertificatePassport,
                StorageId = m.StorageId,
                StorageTemperatureFrom = m.StorageTemperatureFrom,
                StorageTemperatureTo = m.StorageTemperatureTo,
                ActiveSubstancePercent = m.ActiveSubstancePercent,
                WaterContentPercent = m.WaterContentPercent,
                DrugDeclarationId = m.DrugDeclarationId,
                IsAdditional = m.IsAdditional,
                StorageConditionId = m.StorageConditionId,
                StatusId = m.StatusId,
                ExternalStateId = m.ExternalStateId,
                ConcordanceStatementId = m.ConcordanceStatementId,
                OpeningDate = m.OpeningDate,
                ExpirationAfterOpeningDate = m.ExpirationAfterOpeningDate,
                DirectionNumber = direction?.Number,

                DIC_Storages = m.DIC_Storages,
                Unit = m.Unit,
                ConcordanceStatement = m.ConcordanceStatement,
                Country = m.Country,
                DosageUnit = m.DosageUnit,
                ExternalState = m.ExternalState,
                Status = m.Status,
                StorageCondition = m.StorageCondition,
                MaterialType = m.MaterialType,
                VolumeUnit = m.VolumeUnit,
                EXP_DrugDeclaration = m.EXP_DrugDeclaration,
                Producer = m.Producer,
                sr_dosage_forms = m.sr_dosage_forms
            }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateMaterial(EXP_Materials model)
        {
            var errors = ModelState.Values.Select(v => v.Errors);
            if (ModelState.IsValid)
            {
                MaterialRepository repository = new MaterialRepository(false);
                EXP_Materials m = new EXP_Materials()
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    TypeId = model.TypeId,
                    RegistrationDate = model.RegistrationDate ?? DateTime.Now,
                    CreatedDate = DateTime.Now,
                    DrugFormId = model.DrugFormId,
                    Dosage = model.Dosage,
                    DosageUnitId = model.DosageUnitId,
                    DosageQuantity = model.DosageQuantity,
                    Concentration = model.Concentration,
                    Volume = model.Volume,
                    VolumeUnitId = model.VolumeUnitId,
                    IsContainNPP = model.IsContainNPP,
                    ProducerId = model.ProducerId,
                    CountryId = model.CountryId,
                    Quantity = model.Quantity,
                    UnitId = model.UnitId,
                    Batch = model.Batch,
                    DateOfManufacture = model.DateOfManufacture,
                    ExpirationDate = model.ExpirationDate,
                    RetestDate = model.RetestDate,
                    IsCertificatePassport = model.IsCertificatePassport,
                    StorageId = model.StorageId,
                    StorageTemperatureFrom = model.StorageTemperatureFrom,
                    StorageTemperatureTo = model.StorageTemperatureTo,
                    ActiveSubstancePercent = model.ActiveSubstancePercent,
                    WaterContentPercent = model.WaterContentPercent,
                    DrugDeclarationId = model.DrugDeclarationId,
                    IsAdditional = model.IsAdditional,
                    // DeleteDate = model.DeleteDate
                };
                DictionaryRepository drep = new DictionaryRepository(false);
                var statusId = drep.GetDictionaryElementIdByTypeAndCode(Dictionary.MaterialRdStatus.DicCode, Dictionary.MaterialRdStatus.Registered);
                m.StatusId = statusId;

                repository.Insert(m);

                repository.Save();
                model.Id = m.Id;
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateMaterial(EXP_Materials model)
        {
            if (ModelState.IsValid)
            {
                MaterialRepository repository = new MaterialRepository();
                EXP_Materials m = repository.GetById(model.Id);

                m.Name = model.Name;
                m.TypeId = model.TypeId;
                //m.RegistrationDate = model.RegistrationDate;
                m.DrugFormId = model.DrugFormId;
                m.Dosage = model.Dosage;
                m.DosageUnitId = model.DosageUnitId;
                m.DosageQuantity = model.DosageQuantity;
                m.Concentration = model.Concentration;
                m.Volume = model.Volume;
                m.VolumeUnitId = model.VolumeUnitId;
                m.IsContainNPP = model.IsContainNPP;
                m.ProducerId = model.ProducerId;
                m.CountryId = model.CountryId;
                m.Quantity = model.Quantity;
                m.UnitId = model.UnitId;
                m.Batch = model.Batch;
                m.DateOfManufacture = model.DateOfManufacture;
                m.ExpirationDate = model.ExpirationDate;
                m.RetestDate = model.RetestDate;
                m.IsCertificatePassport = model.IsCertificatePassport;
                m.StorageId = model.StorageId;
                m.StorageTemperatureFrom = model.StorageTemperatureFrom;
                m.StorageTemperatureTo = model.StorageTemperatureTo;
                m.ActiveSubstancePercent = model.ActiveSubstancePercent;
                m.WaterContentPercent = model.WaterContentPercent;
                m.StorageConditionId = model.StorageConditionId;
                m.IsAdditional = model.IsAdditional;
                m.OpeningDate = model.OpeningDate;
                m.ExpirationAfterOpeningDate = model.ExpirationAfterOpeningDate;
                m.ConcordanceStatementId = model.ConcordanceStatementId;
                m.ExternalStateId = model.ExternalStateId;
                
                
//                                var status = repository.GetStatusByCode(Dictionary.ExpDirectionToPayStatus.Created);
//                                m.StatusId = status.Id;
//                                m.StatusValue = status.Name;

                repository.Update(m);

                repository.Save();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteMaterial(EXP_Materials model)
        {
            if (ModelState.IsValid)
            {
                MaterialRepository repository = new MaterialRepository();
                repository.Delete(model.Id);
                repository.Save();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AcceptMaterials(Guid modelId)
        {
            MaterialRepository repository = new MaterialRepository(false);
            EXP_Materials m = repository.GetById(modelId);

            DictionaryRepository drep = new DictionaryRepository(false);
            var statusId = drep.GetDictionaryElementIdByTypeAndCode(Dictionary.MaterialRdStatus.DicCode, Dictionary.MaterialRdStatus.Accepted);
            m.StatusId = statusId;

            repository.Update(m);
            repository.Save();

            return Json(modelId, JsonRequestBehavior.AllowGet);
        }

        #region Material Editor

        public ActionResult MaterialForm(Guid? id)
        {
            DictionaryRepository drepository = new DictionaryRepository(false);

            var mpsampleId =
                drepository.GetAsQuarable(
                        d => d.Type == Dictionary.MaterialRdType.DicCode && d.Code == Dictionary.MaterialRdType.MpSample)
                    .Select(d => d.Id)
                    .FirstOrDefault();

            EXP_Materials material = new EXP_Materials()
            {
                Id = Guid.NewGuid(),
                TypeId = mpsampleId,
            };

            if (id.HasValue)
            {
                MaterialRepository repository = new MaterialRepository(false);
                material = repository.GetById(id.Value);
            }
            
            return PartialView("_MaterialFormView", material);
        }

        #endregion

        #region Dictionaries

        public JsonResult GetDrugFormNodes() //int? parentId
        {
            var repository = new MaterialRepository(false);
            var list = repository.GetDosageForm(); //df => df.parent_id == parentId
            var customers = from o in list
                select new { Id = o.id, Name = o.name, children = o.sr_dosage_forms1.Any()};
            return Json(customers.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDrugForm(Guid drugDeclarationId)
        {
            var repository = new MaterialRepository(false);
            var dd = repository.GetDrugDeclaration(d => d.Id == drugDeclarationId).First();
            return Json(new {DrugFormId = dd.DrugFormId}, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
    }
}