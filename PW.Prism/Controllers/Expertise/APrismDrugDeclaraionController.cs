using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Controller;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using PW.Prism.ViewModels.Expertise;

namespace PW.Prism.Controllers.Expertise
{
    public abstract class APrismDrugDeclaraionController : ADrugDeclarationController
    {
        public virtual ActionResult AppDosage(Guid? id)
        {
            return null;
        }
        public virtual ActionResult CreateMailRemark(string id)
        {
            var model = new DrugDeclarationRepository().CreateMailRemark(id, UserHelper.GetCurrentEmployee());
            return Json(new
            {
                isSuccess = model.IsSuccess,
                stageType = model.StageType
            });
        }

        protected virtual void FillDosageControl(EXP_ExpertiseStageDosage model)
        {
            var externalRepository = new ExternalRepository();
            var repository = new ReadOnlyDictionaryRepository();
            var measures = externalRepository.GetMeasures();

            var wrappingTypes = repository.GetWrappingTypes().ToArray();
            var boxes = externalRepository.GetBoxes().ToArray();
            //            var remarkTypes = repository.GetRemarkTypes().ToArray();
            var sales = repository.GetSaleType().ToList();
            var drugDeclarationRepository = new DrugDeclarationRepository();
            model.EXP_DrugDosage.ExpDrugWrappings = new List<EXP_DrugWrapping>();
            model.EXP_DrugDosage.ExpDrugPrices = new List<EXP_DrugPrice>();
            model.EXP_DrugDosage.ExpDrugSubstances = new List<EXP_DrugSubstance>();
            //            model.EXP_DrugDosage.ExpDrugAppDosageRemarks = new List<EXP_DrugAppDosageRemark>();
            //            model.EXP_DrugDosage.ExpDrugAppDosageResults = new List<EXP_DrugAppDosageResult>();

            /*  foreach (var expDrugAppDosageRemark in model.EXP_DrugDosage.EXP_DrugAppDosageRemark)
            {
                model.EXP_DrugDosage.ExpDrugAppDosageRemarks.Add(expDrugAppDosageRemark);
            }
            if (model.EXP_DrugDosage.ExpDrugAppDosageRemarks.Count == 0)
            {
                model.EXP_DrugDosage.ExpDrugAppDosageRemarks.Add(new EXP_DrugAppDosageRemark());
            }

            foreach (var expDrugAppDosageRemark in model.EXP_DrugDosage.EXP_DrugAppDosageResult)
            {
                model.EXP_DrugDosage.ExpDrugAppDosageResults.Add(expDrugAppDosageRemark);
            }
            if (model.EXP_DrugDosage.ExpDrugAppDosageResults.Count == 0)
            {
                model.EXP_DrugDosage.ExpDrugAppDosageResults.Add(new EXP_DrugAppDosageResult());
            }
*/
            /*  ViewData["RemarkTypes" + model.EXP_DrugDosage.DrugDeclarationId] = new SelectList(remarkTypes, "Id", "NameRu",
                 null);
            foreach (var wrap in model.EXP_DrugDosage.ExpDrugAppDosageResults)
            {
                ViewData["ResultTypes" + model.EXP_DrugDosage.DrugDeclarationId + "_" + wrap.Id] = new SelectList(remarkTypes, "Id", "NameRu",
                    wrap.RemarkTypeId);
            }
            foreach (var wrap in model.EXP_DrugDosage.ExpDrugAppDosageRemarks)
            {
                ViewData["RemarkTypes" + model.EXP_DrugDosage.DrugDeclarationId + "_" + wrap.Id] = new SelectList(remarkTypes, "Id", "NameRu",
                    wrap.RemarkTypeId);
            }*/

            foreach (var expDrugWrapping in model.EXP_DrugDosage.EXP_DrugWrapping)
            {
                model.EXP_DrugDosage.ExpDrugWrappings.Add(expDrugWrapping);
            }

            foreach (var expDrugWrapping in model.EXP_DrugDosage.EXP_DrugPrice)
            {
                model.EXP_DrugDosage.ExpDrugPrices.Add(expDrugWrapping);
            }
            for (var d = 0; d < model.EXP_DrugDosage.EXP_DrugSubstance.Count; d++)
            {
                model.EXP_DrugDosage.ExpDrugSubstances.Add(model.EXP_DrugDosage.EXP_DrugSubstance.ToList()[d]);
                if (model.EXP_DrugDosage.EXP_DrugSubstance.ToList()[d].ExpDrugSubstanceManufactures == null)
                    model.EXP_DrugDosage.EXP_DrugSubstance.ToList()[d].ExpDrugSubstanceManufactures = new List<EXP_DrugSubstanceManufacture>();

                foreach (var expDrugSubstanceManufacture in model.EXP_DrugDosage.EXP_DrugSubstance.ToList()[d].EXP_DrugSubstanceManufacture)
                {
                    model.EXP_DrugDosage.EXP_DrugSubstance.ToList()[d].ExpDrugSubstanceManufactures.Add(expDrugSubstanceManufacture);
                }
            }
           /* foreach (var expDrugWrapping in model.EXP_DrugDosage.EXP_DrugSubstance)
            {
                model.EXP_DrugDosage.ExpDrugSubstances.Add(expDrugWrapping);
                foreach (var expDrugSubstanceManufacture in expDrugWrapping.EXP_DrugSubstanceManufacture)
                {
                    model.EXP_DrugDosage.ExpDrugSubstances.Add(expDrugWrapping);
                }
            }*/
            if (model.EXP_DrugDosage.ExpDrugWrappings == null || model.EXP_DrugDosage.ExpDrugWrappings.Count == 0)
            {
                model.EXP_DrugDosage.ExpDrugWrappings = new List<EXP_DrugWrapping>();
            }
            if (model.EXP_DrugDosage.ExpDrugSubstances == null || model.EXP_DrugDosage.ExpDrugSubstances.Count == 0)
            {
              model.EXP_DrugDosage.ExpDrugSubstances = new List<EXP_DrugSubstance> ();
            }
            if (model.EXP_DrugDosage.ExpDrugPrices == null)
            {
                model.EXP_DrugDosage.ExpDrugPrices = new List<EXP_DrugPrice>();
            }
            ViewData["MeasureList" + model.EXP_DrugDosage.Id] = new SelectList(measures, "Id", "name",
                model.EXP_DrugDosage.DosageMeasureTypeId);
            ViewData["SaleTypeList" + model.EXP_DrugDosage.Id] = new SelectList(sales, "Id", "NameRu", model.EXP_DrugDosage.SaleTypeId);
            ViewData["BestBeforeMeasureTypeList" + model.EXP_DrugDosage.Id] = new SelectList(measures, "id", "short_name",
                model.EXP_DrugDosage.BestBeforeMeasureTypeDicId);
            ViewData["AppPeriodMixMeasureList" + model.EXP_DrugDosage.Id] = new SelectList(measures, "id", "short_name",
                model.EXP_DrugDosage.AppPeriodMixMeasureDicId);
            ViewData["AppPeriodOpenMeasureList" + model.EXP_DrugDosage.Id] = new SelectList(measures, "id", "short_name",
                model.EXP_DrugDosage.AppPeriodOpenMeasureDicId);
            for (var j = 0; j < model.EXP_DrugDosage.ExpDrugWrappings.Count; j++)
            {
                var wrap = model.EXP_DrugDosage.ExpDrugWrappings[j];
                ViewData["WrappingTypes" + wrap.Id] = new SelectList(wrappingTypes, "Id", "NameRu",
                    wrap.WrappingTypeId);
                ViewData["Boxes" + wrap.Id] = new SelectList(boxes, "Id", "name", wrap.WrappingKindId);
                ViewData["SizeMeasureList" + wrap.Id] = new SelectList(measures, "Id", "short_name",
                    wrap.SizeMeasureId);
                ViewData["VolumeMeasureList" + wrap.Id] = new SelectList(measures, "Id", "short_name",
                    wrap.VolumeMeasureId);
            }
            var origins = repository.GetOrigins().ToArray();
            var plantKinds = repository.GetPlantKinds().ToArray();
            var substanceTypes = externalRepository.GetSubstanceTypes().ToArray();
            var countries = externalRepository.GetCounties().ToArray();
            var booleans = repository.GetBooleanList();
            for (var j = 0; j < model.EXP_DrugDosage.ExpDrugSubstances.Count; j++)
            {
                var ids = model.EXP_DrugDosage.ExpDrugSubstances[j].Id.ToString();
                model.EXP_DrugDosage.ExpDrugSubstances[j].CategoryName =
                    GetCategoryName(model.EXP_DrugDosage.ExpDrugSubstances[j].sr_substances);
                model.EXP_DrugDosage.ExpDrugSubstances[j].CategoryPos =
                    model.EXP_DrugDosage.ExpDrugSubstances[j].sr_substances?.category_pos;
                ViewData["SubstanceTypes" + ids] = new SelectList(substanceTypes, "Id", "name",
                    model.EXP_DrugDosage.ExpDrugSubstances[j].SubstanceTypeId);
                ViewData["Origins" + ids] = new SelectList(origins, "Id", "NameRu",
                    model.EXP_DrugDosage.ExpDrugSubstances[j].OriginId);
                ViewData["PlantKinds" + ids] = new SelectList(plantKinds, "Id", "NameRu",
                    model.EXP_DrugDosage.ExpDrugSubstances[j].PlantKindId);
                ViewData["SubstanceMeasureList" + ids] = new SelectList(measures, "Id", "short_name",
                    model.EXP_DrugDosage.ExpDrugSubstances[j].MeasureId);
                ViewData["SubstanceCounties" + ids] = new SelectList(countries, "Id", "name",
                    model.EXP_DrugDosage.ExpDrugSubstances[j].CountryId);
                ViewData["IsControlList" + ids] = new SelectList(booleans, "IsSign", "NameRu",
                    model.EXP_DrugDosage.ExpDrugSubstances[j].IsControl);
                ViewData["NormDocs" + ids] = new SelectList(repository.GetExpDicNormDocFarms().ToArray(), "Id", "NameRu",
                    model.EXP_DrugDosage.ExpDrugSubstances[j].NormDocFarmId);
                ViewData["IsPoisonList" + ids] = new SelectList(booleans, "IsSign", "NameRu",
                    model.EXP_DrugDosage.ExpDrugSubstances[j].IsPoison);
                /*      ViewData["SubstanceCounties" + ids] = new SelectList(countries, "Id", "name",
                          model.EXP_DrugDosage.ExpDrugSubstances[j].CountryId);*/
                if (model.EXP_DrugDosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures != null)
                {
                    for (var k = 0;
                        k < model.EXP_DrugDosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures.Count;
                        k++)
                    {
                        ViewData["SubstanceCounties" + model.EXP_DrugDosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures[k].Id.ToString()] = new SelectList(countries, "Id", "name",
                            model.EXP_DrugDosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures[k].CountryId);
                    }
                }
            }

            for (var j = 0; j < model.EXP_DrugDosage.ExpDrugPrices.Count; j++)
            {
                var price = model.EXP_DrugDosage.ExpDrugPrices[j];
                model.EXP_DrugDosage.ExpDrugPrices[j].PrimaryText =
                    drugDeclarationRepository.GetNameByWrappingNames(price.PrimaryValue);
                model.EXP_DrugDosage.ExpDrugPrices[j].SecondaryText =
                    drugDeclarationRepository.GetNameByWrappingNames(price.SecondaryValue);
                model.EXP_DrugDosage.ExpDrugPrices[j].IntermediateText =
                    drugDeclarationRepository.GetNameByWrappingNames(price.IntermediateValue);
            }
        }

        public virtual ActionResult Index()
        {
            var model = new ExpertiseEntity
            {
                Guid = Guid.NewGuid(),
                DicStageId = GetStage()
            };
            return PartialView("~/Views/DrugDeclaration/Index.cshtml", model);
        }

        public virtual ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = GetExpertiseStage(id);
            FillDeclarationControl(model.EXP_DrugDeclaration);
            return PartialView("~/Views/DrugDeclaration/Edit.cshtml", model);
        }

        protected virtual EXP_ExpertiseStage GetExpertiseStage(Guid? id)
        {
            return new ExpertiseStageRepository().GetById(id);
        }

        public virtual ActionResult SetExpertiseStageDosageResult(Guid dosageStageId, int resultId)
        {
            var repository = new ExpertiseStageRepository();
            repository.SetExpertiseStageDosageResult(dosageStageId, resultId);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public virtual int GetStage()
        {
            return 0;
        }
        public virtual string GetTreeData(Guid id, bool withRemarks = false, bool disabled = false)
        {
            var model = GetExpertiseStage(id);
            var dicRepo = new ReadOnlyDictionaryRepository();
            var flatObjects = model.EXP_DrugDeclaration.EXP_DIC_Type.Code == EXP_DIC_Type.ReRegistration
                ? dicRepo.GetExpDicPrimaryOTDs(new[] { EXP_DIC_PrimaryOTD.Module4, EXP_DIC_PrimaryOTD.Module5 }).ToArray()
                : dicRepo.GetExpDicPrimaryOTDs().ToArray();
            if (model.OtdIds == null)
            {
                model.OtdIds = "";
            }
            var odtIds = model.OtdIds.Split(',');
            Dictionary<long, string> otdRemarks = new Dictionary<long, string>();
            if (withRemarks && !string.IsNullOrEmpty(model.OtdRemarks))
            {
                foreach (var rmStr in model.OtdRemarks.Split(new[] { "+++" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var items = rmStr.Split(new[] { "-->" }, StringSplitOptions.None);
                    otdRemarks.Add(long.Parse(items[0]), items[1]);
                }
            }
            List<RecursiveObject> recursiveObjects = FillRecursive(flatObjects, null, odtIds, withRemarks, otdRemarks, disabled);
            return new JavaScriptSerializer().Serialize(recursiveObjects);
        }
        public virtual ActionResult UpdateOtdRemark(Guid stageId, int noteId, string remark)
        {
            new DrugPrimaryRepository().UpdateOtdRemark(stageId, noteId - 100000, remark);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        protected static List<RecursiveObject> FillRecursive(EXP_DIC_PrimaryOTD[] list, long? parentId, string[] listPermissions, bool withRemarks, Dictionary<long, string> remarks, bool disabled)
        {
            var recursiveObjects = new List<RecursiveObject>();
            var queryList = list.Where(x => x.ParentId == parentId).ToList();
            if (queryList.Count == 0 && withRemarks)
            {
                var otdRemark = remarks.ContainsKey(parentId.Value) ? remarks[parentId.Value] : null;
                recursiveObjects.Add(new RecursiveObject()
                {
                    id = parentId.Value + 100000,
                    text = otdRemark ?? "",
                    title = otdRemark ?? "",
                    type = "remark",
                    state = new FlatTreeAttribute()
                    {
                        opened = true,
                        selected = false,
                        disabled = disabled
                    },
                  
                });
            }
            foreach (EXP_DIC_PrimaryOTD item in queryList)
            {
                bool isChecked;
                var secRolespermissions = listPermissions.Contains(item.Id.ToString());
                if (secRolespermissions)
                {
                    isChecked = true;
                }
                else
                {
                    isChecked = false;
                }
                var recursiveObject = new RecursiveObject
                {
                    text = "[" + item.Code + "]-" + item.NameRu,
                    title = "[" + item.Code + "]-" + item.NameRu,
                    id = item.Id,
                    //                    icon = "fa x icon-state-danger",
                    state =
                        new FlatTreeAttribute
                        {
                            selected = isChecked,
                            opened = true,
                            disabled = disabled
                        },
                    children = FillRecursive(list, item.Id, listPermissions, withRemarks, remarks, disabled)
                };
                if (recursiveObject.children.Count == 0 || recursiveObject.children.Any(e => e.type == "remark"))
                    recursiveObject.type = "leaf";
                recursiveObjects.Add(recursiveObject);
            }
            return recursiveObjects;
        }

    }
}