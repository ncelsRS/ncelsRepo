using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Pdf;
using Newtonsoft.Json;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Material;
using PW.Ncels.Database.Repository.DictionaryRep;
using PW.Ncels.Database.Repository.Expertise;

namespace PW.Ncels.Controllers
{
    public class MaterialController : ACommonController
    {
        // GET: Material
        public ActionResult MaterialListPartialView(Guid? drugDeclarationId)
        {
            return PartialView(drugDeclarationId);
        }

        public JsonResult ReadMaterialListData(ModelRequest request, Guid? drugDeclarationId)
        {
            MaterialRepository repository = new MaterialRepository(false);
            ModelResult result = new ModelResult();
            var q = repository.GetAsQuarable(mp => mp.DeleteDate == null);
            if (drugDeclarationId != null)
            {
                q = q.Where(mp => mp.DrugDeclarationId == drugDeclarationId.Value);

                result = q
                    .Include(r => r.DIC_Storages)
                    .Include(r => r.Unit)
                    .Include(r => r.MaterialType)
                    .ToDataSourceResult(request);
            }
                
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateMaterial(Guid? id)
        {
            MaterialRepository repository = new MaterialRepository(false);
            EXP_DrugDeclaration drugDeclaration = null;
            CreateMaterialViewModel vm = new CreateMaterialViewModel()
            {
                DrugDeclarationId = id,
                Id = null
            };

            if (id.HasValue)
            {
                //EXP_Materials material = new EXP_Materials(){};
                drugDeclaration = repository.GetDrugDeclaration(d => d.Id == id).Include(d => d.sr_dosage_forms).First();
                if (drugDeclaration.DrugFormId != null)
                {
                    vm.DrugFormId = drugDeclaration.DrugFormId;
                    vm.DrugFormName = drugDeclaration.sr_dosage_forms.name;

                    vm.IsNpp = repository.GetIsControlFormDrugDeclaration(id.Value);
                }
            }
            
            return View(vm);
        }

        public ActionResult EditMaterial(Guid id)
        {
            MaterialRepository repository = new MaterialRepository(false);
            var DrugDeclarationId = repository.GetAsQuarable(m => m.Id == id).Select(m => m.DrugDeclarationId).FirstOrDefault();

            CreateMaterialViewModel vm = new CreateMaterialViewModel()
            {
                DrugDeclarationId = DrugDeclarationId,
                Id = id
            };

            return View("CreateMaterial", vm);
        }

        public ActionResult ViewDetailMaterial(Guid id)
        {
            MaterialRepository repository = new MaterialRepository();
            var material = repository.GetById(id);
            return View(material);
        }

        public ActionResult RemoveMaterial(Guid id)
        {
            MaterialRepository repository = new MaterialRepository(false);
            repository.Delete(id);

            return Json(new {IsSuccess = true}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadMaterial(Guid id)
        {
            MaterialRepository repository = new MaterialRepository(false);
            var material = repository.GetAsQuarable(m => m.Id == id)
                .Include(m => m.MaterialType)
                .Include(m => m.Producer)
                .Include(m => m.Country)
                .Include(m => m.DIC_Storages)
                .Include(m => m.DosageUnit)
                .Include(m => m.Unit)
                .Include(m => m.VolumeUnit)
                .Include(m => m.sr_dosage_forms)
                .FirstOrDefault();

            return Content(JsonConvert.SerializeObject(material
                , Formatting.Indented
                , new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ" }));
        }

        public JsonResult GetMaterialType(string code)
        {
            MaterialRepository repository = new MaterialRepository(false);
            var materialType = repository.GetMaterialType(mp => mp.Code == code).FirstOrDefault();

            return Json(materialType, JsonRequestBehavior.AllowGet);
        }
        
        
        // DrugForm Dic For TreeView

        public JsonResult GetRootDrugFormNodes()
        {
            MaterialRepository repository = new MaterialRepository(false);
            var drugFormQuarable = repository.GetDosageForm(df=> df.parent_id == null);
            var drugForms = from o in drugFormQuarable
                            select new { id = o.id, text = o.name, children = true };
            return Json(drugForms.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDrugFormNodes(int? parentId)
        {
            var repository = new MaterialRepository(false);
            var list = repository.GetDosageForm(df => df.parent_id == parentId);
            var customers = from o in list
                            select new { id = o.id, text = o.name, children = o.sr_dosage_forms1.Any() };
            return Json(customers.ToArray(), JsonRequestBehavior.AllowGet);

        }


        // Storage Dic For TreeView

        public JsonResult GetRootStorageNodes()
        {
            MaterialRepository repository = new MaterialRepository(false);
            var listQueryable = repository.GetStorage(df => df.ParentId == null);
            var drugForms = from o in listQueryable
                            select new { id = o.Id, text = o.NameRu, children = true };
            return Json(drugForms.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStorageNodes(Guid? parentId)
        {
            var repository = new MaterialRepository(false);
            var list = repository.GetStorage(df => df.ParentId == parentId);
            var storages = from o in list
                            select new { id = o.Id, text = o.NameRu, children = o.DIC_Storages1.Any() };
            return Json(storages.ToArray(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveMaterial(MaterialViewModel model)
        {
            var errors = ModelState.Where(x => x.Value.Errors.Any());
            if (ModelState.IsValid)
            {
                MaterialRepository repository = new MaterialRepository(false);
                var material = repository.GetAsQuarable(m => m.Id == model.Material.Id).FirstOrDefault();

                if (material == null)
                {
                    if (model.Material.TypeId == Guid.Empty && model.Material.MaterialType != null)
                    {
                        model.Material.TypeId = model.Material.MaterialType.Id;
                    }
                    model.Material.MaterialType = null;

                    if (model.Material.CountryId == Guid.Empty && model.Material.Country != null)
                    {
                        model.Material.CountryId = model.Material.Country.Id;
                    }
                    model.Material.Country = null;

                    if (model.Material.ProducerId == Guid.Empty && model.Material.Producer != null)
                    {
                        model.Material.ProducerId = model.Material.Producer.Id;
                    }
                    else
                    {
                        var contractId = repository.GetDrugDeclaration(d => d.Id == model.Material.DrugDeclarationId)
                            .Select(d => d.ContractId).FirstOrDefault();
                        if (contractId != null)
                        {
                            var organization = repository.GetManufactureOrganization(contractId.Value);
                            model.Material.ProducerId = organization?.Id;

                            if (model.Material.CountryId == Guid.Empty && organization?.CountryDicId != null)
                                model.Material.CountryId = organization.CountryDicId;
                        }
                    }
                    model.Material.Producer = null;
                    
                    model.Material.Unit = null;
                    model.Material.DosageUnit = null;
                    model.Material.DIC_Storages = null;
                    model.Material.VolumeUnit = null;
                    model.Material.Id = Guid.NewGuid();
                    model.Material.RegistrationDate = DateTime.Now;
                    model.Material.CreatedDate = DateTime.Now;
                    
                    repository.Insert(model.Material);
                }
                else
                {
                    if (model.Material.MaterialType != null)
                    {
                        material.TypeId = model.Material.MaterialType.Id;
                    }
                    model.Material.MaterialType = null;

                    if (model.Material.Country != null)
                    {
                        material.CountryId = model.Material.Country.Id;
                    }
                    model.Material.Country = null;

                    if (model.Material.Producer != null)
                    {
                        material.ProducerId = model.Material.Producer.Id;
                    }
                    model.Material.Producer = null;


                    material.Name = model.Material.Name;
                    material.DrugFormId = model.Material.DrugFormId;
                    material.Dosage = model.Material.Dosage;
                    material.DosageUnitId = model.Material.DosageUnitId;
                    material.DosageQuantity = model.Material.DosageQuantity;
                    material.Concentration = model.Material.Concentration;
                    material.Volume = model.Material.Volume;
                    material.VolumeUnitId = model.Material.VolumeUnitId;
                    material.IsContainNPP = model.Material.IsContainNPP;
                    material.Quantity = model.Material.Quantity;
                    material.Quantity = model.Material.Quantity;
                    material.Batch = model.Material.Batch;
                    material.DateOfManufacture = model.Material.DateOfManufacture;
                    material.ExpirationDate = model.Material.ExpirationDate;
                    material.RetestDate = model.Material.RetestDate;
                    material.IsCertificatePassport = model.Material.IsCertificatePassport;
                    material.StorageTemperatureFrom = model.Material.StorageTemperatureFrom;
                    material.StorageTemperatureTo = model.Material.StorageTemperatureTo;
                    material.StorageTemperatureTo = model.Material.StorageTemperatureTo;
                    material.ActiveSubstancePercent = model.Material.ActiveSubstancePercent;
                    material.WaterContentPercent = model.Material.WaterContentPercent;
                    material.IsAdditional = model.Material.IsAdditional;
                    
                    repository.Update(material);
                }
                
                repository.Save();

                return Json(new {IsSuccess = true, model}, JsonRequestBehavior.AllowGet);
            }
            return Json(new {IsSuccess = false, model}, JsonRequestBehavior.AllowGet);
        }
    }
}