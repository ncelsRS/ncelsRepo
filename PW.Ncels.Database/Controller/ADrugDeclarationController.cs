using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Entities;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Contract;
using PW.Ncels.Database.Repository.Expertise;

namespace PW.Ncels.Database.Controller
{
    public abstract class ADrugDeclarationController : System.Web.Mvc.Controller
    {
        [HttpPost]
        public async Task<JsonResult> ReadDrugDeclaration(ModelRequest request, int? type)
        {
            return Json(await new DrugDeclarationRepository().GetDrugDeclarationList(request, true, type), JsonRequestBehavior.AllowGet);
        }
        protected void FillViewBag(EXP_DrugDeclaration model)
        {
            var drugPrimaryRepo = new DrugPrimaryRepository();
            ViewData["ContractList"] =
                new SelectList(new ContractRepository(false).GetActiveContractListWithInfo(model.OwnerId), "Id",
                    "ContractInfo", model.ContractId);
            var repository = new ReadOnlyDictionaryRepository();

            ViewData["AccelerationTypeList"] = new SelectList(repository.GetAccelerationTypes(), "Id",
                "NameRu", model.AccelerationTypeId);

            ViewData["TypeList"] = new SelectList(repository.GetDicTypes(), "Id",
              "NameRu", model.TypeId);

            if (model.RegisterId > 0 && model.sr_register != null)
            {
                model.ReestrNumber = model.sr_register.reg_number + "/" + model.sr_register.reg_number_kz;
                model.ReestrRegDate = model.sr_register.reg_date.ToShortDateString();
                if (model.sr_register.expiration_date != null)
                    model.ReestrExpirationDate = model.sr_register.expiration_date.Value.ToShortDateString();
                var reestrDrug = new ExternalRepository().GEtRegisterDrugById(model.RegisterId);
                if (reestrDrug != null)
                {
                    model.NumberNd = reestrDrug.nd_number;
                }
            }

            if (model.ExpDrugChangeTypes.Count == 0)
            {
                model.ExpDrugChangeTypes.Add(new EXP_DrugChangeType());
            }
            if (model.ExpDrugProtectionDocs.Count == 0)
            {
                model.ExpDrugProtectionDocs.Add(new EXP_DrugProtectionDoc());
            }
            if (model.ExpDrugOtherCountries.Count == 0)
            {
                model.ExpDrugOtherCountries.Add(new EXP_DrugOtherCountry());
            }
            if (model.ExpDrugExportTrades.Count == 0)
            {
                model.ExpDrugExportTrades.Add(new EXP_DrugExportTrade());
            }
            if (model.ExpDrugPatents.Count == 0)
            {
                model.ExpDrugPatents.Add(new EXP_DrugPatent());
            }
            if (model.ExpDrugTypes.Count == 0)
            {
                model.ExpDrugTypes.Add(new EXP_DrugType());
            }
            /*  if (model.ExpDrugWrappings.Count == 0)
              {
                  model.ExpDrugWrappings.Add(new EXP_DrugWrapping());
              }*/

            if (model.ExpDrugOrganizationses.Count == 0)
            {
                model.ExpDrugOrganizationses.Add(new EXP_DrugOrganizations());
            }
            if (model.ExpDrugDosages.Count == 0)
            {
                model.ExpDrugDosages.Add(new EXP_DrugDosage());
            }
            /*if (model.ExpDrugPrices.Count == 0)
            {
                model.ExpDrugPrices.Add(new EXP_DrugPrice());
            }*/
            var externalRepository = new ExternalRepository();
            var countries = externalRepository.GetCounties().ToArray();
            ViewData["Counties"] = new SelectList(countries, "Id", "name", 0);
            for (var i = 0; i < model.ExpDrugExportTrades.Count; i++)
            {
                ViewData["Counties" + i] = new SelectList(countries, "Id", "name",
                    model.ExpDrugExportTrades[i].CountryId);
            }

            var types = repository.GetDrugType().ToArray();
            var typeKinds = repository.GetDrugTypeKinds().ToArray();
            ViewData["DrugTypes"] = new SelectList(types, "Id", "NameRu", 0);
            ViewData["DrugTypeKinds"] = new SelectList(typeKinds, "Id", "NameRu", 0);
            for (var i = 0; i < model.ExpDrugTypes.Count; i++)
            {
                ViewData["DrugTypes" + i] = new SelectList(types, "Id", "NameRu", model.ExpDrugTypes[i].DrugTypeId);
                ViewData["DrugTypeKinds" + i] = new SelectList(typeKinds, "Id", "NameRu",
                    model.ExpDrugTypes[i].DrugTypeKind);
            }

            model.AtcCodeses = externalRepository.GetAtcList();

            var methods = externalRepository.GetUseMethods();
            model.MethodUseList = new MultiSelectList(methods, "id", "name", model.MethodUseIds);

            /*  ViewData["SaleTypeList"] = new SelectList(repository.GetSaleType(), "Id",
                  "NameRu", model.SaleTypeId);*/

            var measures = externalRepository.GetMeasures();
            ViewData["MeasureList"] = new SelectList(measures, "Id",
                "name", null);
            var wrappingTypes = repository.GetWrappingTypes().ToArray();
            var boxes = externalRepository.GetBoxes().ToArray();

            ViewData["WrappingTypes"] = new SelectList(wrappingTypes, "Id", "NameRu", 0);
            ViewData["Boxes"] = new SelectList(boxes, "id", "name", 0);
            ViewData["MeasureShortList"] = new SelectList(measures, "id", "short_name", 0);
            ViewData["SizeMeasureList"] = new SelectList(measures, "id", "short_name", 0);
            ViewData["VolumeMeasureList"] = new SelectList(measures, "id", "short_name", 0);

            var booleans = repository.GetBooleanList();
            ViewData["Booleans"] = new SelectList(booleans, "IsSign", "NameRu", false);

            var origins = repository.GetOrigins().ToArray();
            var plantKinds = repository.GetPlantKinds().ToArray();
            var normDocs = repository.GetExpDicNormDocFarms().ToArray();
            var substanceTypes = externalRepository.GetSubstanceTypes().ToArray();
            ViewData["SubstanceTypes"] = new SelectList(substanceTypes, "Id", "name", null);
            ViewData["Origins"] = new SelectList(origins, "Id", "NameRu", null);
            ViewData["NormDocs"] = new SelectList(normDocs, "Id", "NameRu", null);
            ViewData["PlantKinds"] = new SelectList(plantKinds, "Id", "NameRu", null);
            ViewData["IsControlList"] = new SelectList(booleans, "IsSign", "NameRu", null);
            ViewData["IsPoisonList"] = new SelectList(booleans, "IsSign", "NameRu", null);
            ViewData["SubstanceCounties"] = new SelectList(countries, "Id", "name", 0);
            ViewData["SubstanceMeasureList"] = new SelectList(measures, "id", "short_name", 0);
            var sales = repository.GetSaleType().ToList();
            ViewData["SaleTypeList"] = new SelectList(sales, "Id", "NameRu", null);
            ViewData["BestBeforeMeasureTypeList"] = new SelectList(measures, "id", "short_name",
            null);
            ViewData["AppPeriodMixMeasureList"] = new SelectList(measures, "id", "short_name",
                null);
            ViewData["AppPeriodOpenMeasureList"] = new SelectList(measures, "id", "short_name",
                null);



            var drugDeclarationRepository = new DrugDeclarationRepository();
            for (var i = 0; i < model.ExpDrugDosages.Count; i++)
            {
                var dosage = model.ExpDrugDosages[i];
                if (dosage.ExpDrugWrappings == null || dosage.ExpDrugWrappings.Count == 0)
                {
                    dosage.ExpDrugWrappings = new List<EXP_DrugWrapping> { new EXP_DrugWrapping() };
                }
                if (dosage.ExpDrugSubstances == null || dosage.ExpDrugSubstances.Count == 0)
                {
                    dosage.ExpDrugSubstances = new List<EXP_DrugSubstance> { new EXP_DrugSubstance()
                    {
                        ExpDrugSubstanceManufactures = new List<EXP_DrugSubstanceManufacture>() {new EXP_DrugSubstanceManufacture()}
                    } };
                }
                if (dosage.ExpDrugPrices == null)
                {
                    dosage.ExpDrugPrices = new List<EXP_DrugPrice>();
                }

                ViewData["MeasureList" + dosage.Id] = new SelectList(measures, "Id", "name",
                    model.ExpDrugDosages[i].DosageMeasureTypeId);
                ViewData["SaleTypeList" + dosage.Id] = new SelectList(sales, "Id", "NameRu", model.ExpDrugDosages[i].SaleTypeId);
                ViewData["BestBeforeMeasureTypeList" + dosage.Id] = new SelectList(measures, "id", "short_name",
            model.ExpDrugDosages[i].BestBeforeMeasureTypeDicId);
                ViewData["AppPeriodMixMeasureList" + dosage.Id] = new SelectList(measures, "id", "short_name",
                     model.ExpDrugDosages[i].AppPeriodMixMeasureDicId);
                ViewData["AppPeriodOpenMeasureList" + dosage.Id] = new SelectList(measures, "id", "short_name",
                     model.ExpDrugDosages[i].AppPeriodOpenMeasureDicId);
                for (var j = 0; j < dosage.ExpDrugWrappings.Count; j++)
                {
                    var wrap = dosage.ExpDrugWrappings[j];
                    ViewData["WrappingTypes" + wrap.Id] = new SelectList(wrappingTypes, "Id", "NameRu",
                        wrap.WrappingTypeId);
                    ViewData["Boxes" + wrap.Id] = new SelectList(boxes, "Id", "name", wrap.WrappingKindId);
                    ViewData["SizeMeasureList" + wrap.Id] = new SelectList(measures, "Id", "short_name",
                        wrap.SizeMeasureId);
                    ViewData["VolumeMeasureList" + wrap.Id] = new SelectList(measures, "Id", "short_name",
                        wrap.VolumeMeasureId);
                }

                for (var j = 0; j < dosage.ExpDrugPrices.Count; j++)
                {
                    var price = dosage.ExpDrugPrices[j];
                    model.ExpDrugDosages[i].ExpDrugPrices[j].PrimaryText = drugDeclarationRepository.GetNameByWrappingNames(price.PrimaryValue);
                    model.ExpDrugDosages[i].ExpDrugPrices[j].SecondaryText = drugDeclarationRepository.GetNameByWrappingNames(price.SecondaryValue);
                    model.ExpDrugDosages[i].ExpDrugPrices[j].IntermediateText = drugDeclarationRepository.GetNameByWrappingNames(price.IntermediateValue);
                }

                for (var j = 0; j < dosage.ExpDrugSubstances.Count; j++)
                {
                    var id = dosage.ExpDrugSubstances[j].Id.ToString();
                    dosage.ExpDrugSubstances[j].CategoryName = GetCategoryName(dosage.ExpDrugSubstances[j].sr_substances);
                    dosage.ExpDrugSubstances[j].CategoryPos = dosage.ExpDrugSubstances[j].sr_substances?.category_pos;
                    ViewData["SubstanceTypes" + id] = new SelectList(substanceTypes, "Id", "name",
                        dosage.ExpDrugSubstances[j].SubstanceTypeId);
                    ViewData["Origins" + id] = new SelectList(origins, "Id", "NameRu", dosage.ExpDrugSubstances[j].OriginId);
                    ViewData["NormDocs" + id] = new SelectList(normDocs, "Id", "NameRu", dosage.ExpDrugSubstances[j].NormDocFarmId);
                    ViewData["PlantKinds" + id] = new SelectList(plantKinds, "Id", "NameRu",
                        dosage.ExpDrugSubstances[j].PlantKindId);
                    ViewData["SubstanceMeasureList" + id] = new SelectList(measures, "Id", "short_name",
                       dosage.ExpDrugSubstances[j].MeasureId);
          /*          ViewData["SubstanceCounties" + id] = new SelectList(countries, "Id", "name",
                        dosage.ExpDrugSubstances[j].CountryId);*/
                    ViewData["IsControlList" + id] = new SelectList(booleans, "IsSign", "NameRu",
                        dosage.ExpDrugSubstances[j].IsControl);
                    ViewData["IsPoisonList" + id] = new SelectList(booleans, "IsSign", "NameRu",
                        dosage.ExpDrugSubstances[j].IsPoison);
                    /*  ViewData["SubstanceCounties" + i] = new SelectList(countries, "Id", "name",
                          dosage.ExpDrugSubstances[j].CountryId);*/
                    if (dosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures != null)
                    {
                        for (var k = 0; k < dosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures.Count; k++)
                        {
                            ViewData[
                                    "SubstanceCounties" + dosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures[k].Id
                                ] =
                                new SelectList(countries, "Id", "name",
                                    dosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures[k].CountryId);
                        }
                    }
                    else
                    {
                        dosage.ExpDrugSubstances[j].ExpDrugSubstanceManufactures =
                            new List<EXP_DrugSubstanceManufacture> {new EXP_DrugSubstanceManufacture()};
                    }
                }
            }




            ViewData["IsGrlsList"] = new SelectList(booleans, "IsSign", "NameRu", model.IsGrls);
            ViewData["IsGmpList"] = new SelectList(booleans, "IsSign", "NameRu", model.IsGmp);

            var manufactures = repository.GetManufactureTypeList();
            ViewData["ManufactureTypeList"] = new SelectList(manufactures, "Id", "NameRu", model.ManufactureTypeId);

            /*    ViewData["BestBeforeMeasureTypeList"] = new SelectList(measures, "id", "short_name",
                    model.BestBeforeMeasureTypeDicId);
                ViewData["AppPeriodMixMeasureList"] = new SelectList(measures, "id", "short_name",
                    model.AppPeriodMixMeasureDicId);
                ViewData["AppPeriodOpenMeasureList"] = new SelectList(measures, "id", "short_name",
                    model.AppPeriodOpenMeasureDicId);
    */
            for (var i = 0; i < model.ExpDrugOtherCountries.Count; i++)
            {
                ViewData["OtherCounties" + i] = new SelectList(countries, "Id", "name",
                    model.ExpDrugOtherCountries[i].CountryId);
            }

            var orgManufactureTypes = repository.GetDictionaries(CodeConstManager.DIC_ORG_MANUFACTURE_TYPE);
            var countyDics = repository.GetDictionaries(CodeConstManager.DIC_COUNTRY_TYPE);
            var opfTypeDics = repository.GetDictionaries(CodeConstManager.DIC_OPF_TYPE);
            ViewData["OrgManufactureTypes"] = new SelectList(orgManufactureTypes, "Id", "Name", null);
            ViewData["CountryDics"] = new SelectList(countyDics, "Id", "Name", null);
            ViewData["OpfTypeDics"] = new SelectList(opfTypeDics, "Id", "Name", null);

            for (var i = 0; i < model.ExpDrugOrganizationses.Count; i++)
            {
                var id = model.ExpDrugOrganizationses[i].Id.ToString();
                ViewData["OrgManufactureTypes" + id] = new SelectList(orgManufactureTypes, "Id", "name",
                    model.ExpDrugOrganizationses[i].OrgManufactureTypeDicId);
                ViewData["CountryDics" + id] = new SelectList(countyDics, "Id", "name",
                    model.ExpDrugOrganizationses[i].CountryDicId);
                ViewData["OpfTypeDics" + id] = new SelectList(opfTypeDics, "Id", "name",
                    model.ExpDrugOrganizationses[i].OpfTypeDicId);
                var manufacture = repository.GetDictionaryById(CodeConstManager.DIC_ORG_MANUFACTURE_TYPE,
                    model.ExpDrugOrganizationses[i].OrgManufactureTypeDicId);
                if (manufacture != null)
                {
                    model.ExpDrugOrganizationses[i].ManufactureName = manufacture.Name;
                }
            }
            var changeTypes = repository.GetDicChangeTypes().ToArray();
            ViewData["ChangeTypes"] = new SelectList(changeTypes, "Id", "Code", 0);
            for (var i = 0; i < model.ExpDrugChangeTypes.Count; i++)
            {
                ViewData["ChangeTypes" + i] = new SelectList(changeTypes, "Id", "Code",
                    model.ExpDrugChangeTypes[i].ChangeTypeId);
            }
            
            var markList = drugPrimaryRepo.GetPrimaryMarkList(model.Id, null);

            var remarkTypes = repository.GetRemarkTypes().ToArray();
            ViewData["RemarkTypes" + model.Id] = new SelectList(remarkTypes, "Id", "NameRu",
                null);
            model.ExpExpertiseStageRemarks = new List<EXP_ExpertiseStageRemark>();
            foreach (var expDrugPrimaryRemark in markList)
            {
                model.ExpExpertiseStageRemarks.Add(expDrugPrimaryRemark);
            }

            if (model.ExpExpertiseStageRemarks.Count == 0)
            {
                model.ExpExpertiseStageRemarks.Add(new EXP_ExpertiseStageRemark());
            }
            else
            {
                model.IsShowRemark = true;
            }

            foreach (var expDrugPrimaryRemark in model.ExpExpertiseStageRemarks)
            {
                ViewData["RemarkTypes" + model.Id + "_" + expDrugPrimaryRemark.Id] = new SelectList(remarkTypes,
                    "Id", "NameRu",
                    expDrugPrimaryRemark.RemarkTypeId);
            }            
            ViewBag.PaymentOverdue =model.EXP_DirectionToPays.Any(e => e.Type == 1 &&
                                                   e.Status.Code == Dictionary.ExpDirectionToPayStatus.PaymentExpired);
            model.Letters=new List<EXP_DrugCorespondence>(drugPrimaryRepo.GetDrugCorespondences(model.Id, true));
        }

        protected string GetCategoryName(sr_substances modelExpDrugSubstance)
        {
            if (modelExpDrugSubstance == null)
            {
                return null;
            }
            switch (modelExpDrugSubstance.category_id)
            {
                case 1:
                    {
                        return "Таблица I";
                    }
                case 2:
                    {
                        return "Таблица II";
                    }
                case 3:
                    {
                        return "Таблица III";
                    }
                case 4:
                    {
                        return "Таблица IV";
                    }
                case 5:
                    {
                        return "Таблица V";
                    }
            }
            return null;

        }

        /// <summary>
        /// Поиск Наименование вещества
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public ActionResult SelectSubstance(string searchTerm, int pageSize, int pageNum)
        {
            var founder = new ExternalRepository().SelectSubstance(searchTerm);
            var pagedAttendees = AttendeesToSelect2Format(founder, founder.Count);
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// При выборе значение Наименование вещества
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetSubstanceNames(int id)
        {
            var substance = new ExternalRepository().GetSubstanceById(id);

            if (substance == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }
            return Json(new
            {
                categoryName = GetCategoryName(substance),
                categoryPos = substance.category_pos,
                isAnimal = substance.animal_sign,
                subName = substance.name,
                isSuccess = true
            });
        }

        /// <summary>
        /// Поиск Лекарственная форма
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public ActionResult SelectDrugForm(string searchTerm, int pageSize, int pageNum)
        {
            var founder = new ExternalRepository().SearchDrugForm(searchTerm);
            var pagedAttendees = AttendeesToSelect2Format(founder, founder.Count);
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// При выборе значение форма ЛС
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetDrugFormNames(int id)
        {
            var mnn = new ExternalRepository().GetDrugFormById(id);
            if (mnn == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }

            return Json(new
            {
                nameRu = mnn.full_name,
                nameKz = mnn.full_name_kz,
                isSuccess = true
            });
        }

        /// <summary>
        /// При выборе значение АТХ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetAtcNames(int id)
        {
            var atc = new ExternalRepository().GeAtcById(id);
            if (atc == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }

            return Json(new
            {
                nameRu = "[" + atc.code + "] - " + atc.name,
                nameKz = "[" + atc.code + "] - " + atc.name_kz,
                isSuccess = true
            });
        }
        /// <summary>
        /// При выборе значение МНН
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetMnnNames(int id)
        {
            var mnn = new ExternalRepository().GetMnnById(id);
            if (mnn == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }

            return Json(new
            {
                mnnKz = mnn.name_kz,
                mnnRu = mnn.name_rus,
                mnnEng = mnn.name_lat,
                isSuccess = true
            });
        }

        /// <summary>
        /// Поиск по гос реестру
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public ActionResult SelectReestr(string searchTerm, int pageSize, int pageNum)
        {

            var founder = new ExternalRepository().SearchReestr(searchTerm, 1);

            Select2PagedResult pagedAttendees = AttendeesToSelect2Format(founder, founder.Count);

            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// При выборе значение МНН
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetReestrNames(int id, int? type)
        {
            var reestr = new ExternalRepository().GetReestrById(id);
            if (reestr == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }
            string expirationDate = "";
            string ndNumber = "";
            bool isShowChange = true;
            var reesrtDrug = new ExternalRepository().GEtRegisterDrugById(id);
            if (reesrtDrug != null)
            {
                ndNumber = reesrtDrug.nd_number;
            }
            if (reestr.expiration_date != null)
            {
                expirationDate = reestr.expiration_date.Value.ToShortDateString();
                if (type != null && type.Value == 2)
                {
                    var t = reestr.expiration_date.Value - DateTime.Now;
                    var month = t.TotalDays;

                    if (month < 90)
                    {
                        isShowChange = false;
                    }
                }
            }
            return Json(new
            {
                reg_date = reestr.reg_date.ToShortDateString(),
                expiration_date = expirationDate,
                nd_number = ndNumber,
                isShowChange,
                isSuccess = true
            });
        }
        /// <summary>
        /// Поиск по МНН
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public ActionResult SelectMnn(string searchTerm, int pageSize, int pageNum)
        {

            var founder = new ExternalRepository().SearchMnn(searchTerm);

            Select2PagedResult pagedAttendees = AttendeesToSelect2Format(founder, founder.Count);

            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        private static Select2PagedResult AttendeesToSelect2Format(IEnumerable<TermSearch> attendees, int totalAttendees)
        {
            var jsonAttendees = new Select2PagedResult { Results = new List<Select2Result>() };

            foreach (var a in attendees)
            {
                jsonAttendees.Results.Add(new Select2Result { id = a.Id.ToString(CultureInfo.InvariantCulture), text = a.Term });
            }
            jsonAttendees.Total = totalAttendees;

            return jsonAttendees;
        }
        public JsonResult GetRootAtcNodes()
        {
            var repository = new ExternalRepository();
            var list = repository.GetAtcListByParent(null);
            var customers = from o in list
                            select new { id = o.id, text = "[" + o.code + "]-" + o.name, children = true };
            return Json(customers.ToArray(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAtcNodes(int? id)
        {
            var repository = new ExternalRepository();
            var list = repository.GetAtcListByParent(id);
            var customers = from o in list
                            select new { id = o.id, text = "[" + o.code + "]-" + o.name, children = o.sr_atc_codes1.Any() };
            return Json(customers.ToArray(), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public virtual ActionResult UpdateModel(string code, string modelId, string userId, long? recordId, string fieldName, string fieldValue, string fieldDisplay)
        {
            var filter = new DrugDeclarationRepository().UpdateModel(code, modelId, userId, recordId, fieldName, fieldValue, fieldDisplay);
            return Json(new { Success = true, modelId = filter.ModelId, recordId = filter.RecordId, controlId = filter.ControlId });
        }
        [HttpPost]
        public virtual ActionResult UpdateSubModel(string code, string modelId, long? subModelId, string userId, long? recordId, string fieldName, string fieldValue, string fieldDisplay)
        {
            var filter = new DrugDeclarationRepository().UpdateSubModel(code, modelId, subModelId, userId, recordId, fieldName, fieldValue, fieldDisplay);
            return Json(new { Success = true, modelId = filter.ModelId, submodelId = filter.SubModelId, recordId = filter.RecordId, controlId = filter.ControlId });
        }
        [HttpPost]
        public virtual ActionResult DeleteRecord(string code, long recordId)
        {
            new DrugDeclarationRepository().DeleteRecord(code, recordId);
            return Json(new { Success = true });
        }
        [HttpGet]
        public ActionResult ShowComment(string modelId, string idControl)
        {
            var repository = new DrugDeclarationRepository();
            var model = repository.GetComments(modelId, idControl);
            if (model == null)
            {
                model = new EXP_DrugDeclarationCom();
            }
            model.ExpDrugDeclarationFieldHistories = repository.GetFieldHistories(modelId, idControl);
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }
        [HttpPost]
        public virtual ActionResult SaveComment(string modelId, string idControl, bool isError, string comment, string fieldValue, string userId, string fieldDisplay)
        {

            new DrugDeclarationRepository().SaveComment(modelId, idControl, isError, comment, fieldValue, userId, fieldDisplay);

            return Json(new { Success = true });

        }

        /// <summary>
        /// При выборе значение Наименование вещества
        /// </summary>
        /// <param name="recordId">ид записи</param>
        /// <param name="type"> вид заявителя</param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetInfoFromContract(long recordId, string type)
        {
            var org = new DrugDeclarationRepository().GetInfoFromContract(recordId, type);
            if (org == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }

            return Json(new
            {
                org.AddressFact,
                org.AddressLegal,
                org.OpfTypeDicId,
                org.CountryDicId,
                org.NameRu,
                org.NameKz,
                org.NameEn,
                org.DocNumber,
                DocDateStr = DateHelper.GetDate(org.DocDate),
                DocExpiryDateStr = DateHelper.GetDate(org.DocExpiryDate),
                org.BossPosition,
                org.BossFirstName,
                org.BossLastName,
                org.BossMiddleName,
                org.Phone,
                org.Email,
                org.ContactFio,
                org.ContactPosition,
                isSuccess = true
            });
        }
        [HttpPost]
        public virtual ActionResult SetPrice(long dosageId, string userId, string kinds1, string kinds2, string kinds3, double? calc)
        {
            var model = new DrugDeclarationRepository().SetPrice(dosageId, userId, kinds1, kinds2, kinds3, calc);

            if (model == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }

            return Json(new
            {
                recordId = model.Id,
                model.IntermediateText,
                model.PrimaryText,
                model.SecondaryText,
                model.CountUnit,
                isSuccess = true
            });
        }


        public EXP_DrugDeclaration GetDrugDeclarationById(string id)
        {
            var repository = new DrugDeclarationRepository();
            var model = repository.GetById(id);
            model.IsExist = true;
            FillDeclarationControl(model);
            return model;
        }

        protected void FillDeclarationControl(EXP_DrugDeclaration model)
        {
            model.EditorId = UserHelper.GetCurrentEmployee().Id.ToString();
            model.ExpDrugExportTrades = new List<EXP_DrugExportTrade>();
            var exportTrades = model.EXP_DrugExportTrade.OrderBy(e => e.Id);
            foreach (var record in exportTrades)
            {
                model.ExpDrugExportTrades.Add(record);
            }

            model.ExpDrugPatents = new List<EXP_DrugPatent>();
            var expPatent = model.EXP_DrugPatent.OrderBy(e => e.Id);
            foreach (var record in expPatent)
            {
                model.ExpDrugPatents.Add(record);
            }

            model.ExpDrugTypes = new List<EXP_DrugType>();
            var expDrugType = model.EXP_DrugType.OrderBy(e => e.Id);
            foreach (var record in expDrugType)
            {
                model.ExpDrugTypes.Add(record);
            }
            /*     model.ExpDrugWrappings = new List<EXP_DrugWrapping>();
                 var expDrugWrapping = model.EXP_DrugWrapping.OrderBy(e => e.Id);
                 foreach (var record in expDrugWrapping)
                 {
                     model.ExpDrugWrappings.Add(record);
                 }*/

            model.ExpDrugOtherCountries = new List<EXP_DrugOtherCountry>();
            var expDrugOtherCountries = model.EXP_DrugOtherCountry.OrderBy(e => e.Id);
            foreach (var record in expDrugOtherCountries)
            {
                model.ExpDrugOtherCountries.Add(record);
            }
            model.ExpDrugProtectionDocs = new List<EXP_DrugProtectionDoc>();
            var expDrugProtectionDocs = model.EXP_DrugProtectionDoc.OrderBy(e => e.Id);
            foreach (var record in expDrugProtectionDocs)
            {
                model.ExpDrugProtectionDocs.Add(record);
            }
            model.ExpDrugOrganizationses = new List<EXP_DrugOrganizations>();
            var expDrugOrganizationses = model.EXP_DrugOrganizations.OrderBy(e => e.Id);
            foreach (var record in expDrugOrganizationses)
            {
                model.ExpDrugOrganizationses.Add(record);
            }
            model.ExpDrugDosages = new List<EXP_DrugDosage>();
            var expDrugDosages = model.EXP_DrugDosage.OrderBy(e => e.Id);
            model.ConclusionSafetyReports = new List<ConclusionSafetyReport>();
            foreach (var record in expDrugDosages)
            {
                if (record.RegisterId > 0 && record.sr_register != null)
                {
                    record.ReestrNumber = record.sr_register.reg_number + "/" + record.sr_register.reg_number_kz;
                    record.ReestrRegDate = record.sr_register.reg_date.ToShortDateString();
                    if (record.sr_register.expiration_date != null)
                        record.ReestrExpirationDate = record.sr_register.expiration_date.Value.ToShortDateString();
                    var reestrDrug = new ExternalRepository().GEtRegisterDrugById(record.RegisterId);
                    if (reestrDrug != null)
                    {
                        record.NumberNd = reestrDrug.nd_number;
                    }
                }
                record.ExpDrugWrappings = new List<EXP_DrugWrapping>();
                foreach (var expDrugWrapping in record.EXP_DrugWrapping)
                {
                    record.ExpDrugWrappings.Add(expDrugWrapping);
                }
                record.ExpDrugPrices = new List<EXP_DrugPrice>();
                foreach (var drugPrice in record.EXP_DrugPrice)
                {
                    record.ExpDrugPrices.Add(drugPrice);
                }
                model.ExpDrugDosages.Add(record);

                record.ExpDrugSubstances = new List<EXP_DrugSubstance>();

                for (var d = 0; d < record.EXP_DrugSubstance.Count; d++)
                {
                    record.ExpDrugSubstances.Add(record.EXP_DrugSubstance.ToList()[d]);
                    record.EXP_DrugSubstance.ToList()[d].ExpDrugSubstanceManufactures = new List<EXP_DrugSubstanceManufacture>();
                    foreach (var expDrugSubstanceManufacture in record.EXP_DrugSubstance.ToList()[d].EXP_DrugSubstanceManufacture)
                    {
                        record.EXP_DrugSubstance.ToList()[d].ExpDrugSubstanceManufactures.Add(expDrugSubstanceManufacture);
                    }
                }

          /*      foreach (var substance in record.EXP_DrugSubstance)
                {
                    record.ExpDrugSubstances.Add(substance);

                }*/

                if (model.StatusId > 6)
                {
                    model.ConclusionSafetyReports.Add(new SafetyreportRepository().ConclusionSafetyReportsFromDosage(record.Id, false));
                    model.ConclusionSafetyReports.Add(new SafetyreportRepository().ConclusionSafetyReportsFromDosage(record.Id, true));
                }

            }
            if (model.StatusId > 6)
            {
                var item = new SafetyreportRepository().ConclusionSafetyReportsFromFiles(model.Id, UserHelper.GetCurrentEmployee());
                foreach (var conclusionSafetyReport in item)
                {
                    model.ConclusionSafetyReports.Add(conclusionSafetyReport);
                }
            }
            if (model.ConclusionSafetyReports.Count > 0)
            {
                model.IsShowConclision = true;
            }
            model.ExpDrugChangeTypes = new List<EXP_DrugChangeType>();
            var changeTypes = model.EXP_DrugChangeType.OrderBy(e => e.Id);
            foreach (var record in changeTypes)
            {
                model.ExpDrugChangeTypes.Add(record);
            }
            /*  model.ExpDrugPrices = new List<EXP_DrugPrice>();
              var expDrugPrices = model.EXP_DrugPrice.OrderBy(e => e.Id);
              foreach (var record in expDrugPrices)
              {
                  model.ExpDrugPrices.Add(record);
              }*/
            model.MethodUseIds =
                model.EXP_DrugUseMethod.Select(e => e.UseMethodsId.ToString(CultureInfo.InvariantCulture)).ToList();

            if (model.TypeId > 1)
            {
                if (model.TypeId == 3)
                {
                    model.IsShowChange = true;
                }
                else
                {
                    if (model.sr_register != null && model.sr_register.expiration_date != null)
                    {
                        if (model.TypeId != 2)
                        {
                            var t = model.sr_register.expiration_date.Value - model.CreatedDate;
                            var month = t.TotalDays;

                            if (month > 90)
                            {
                                model.IsShowChange = true;
                            }
                        }
                    }
                }
            }

            var primaryEntity = new PrimaryEntity
            {
                EXP_DrugDeclaration = model,
                DrugDeclarationId = model.ObjectId,
                Editor = UserHelper.GetCurrentEmployee(),
                ExpExpertiseStageRemarks = new List<EXP_ExpertiseStageRemark>(),
            };
            FillViewBag(model);
        }
        public virtual ActionResult GetChangeType(int? id)
        {
            var model = new ReadOnlyDictionaryRepository().GetDicChangeTypeById(id);

            if (model == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }

            return Json(new
            {
                changeName = model.ChangeName,
                changeType = model.ChangeType,
                isSuccess = true
            });
        }

        /// <summary>
        /// Поиск Лекарственная форма
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        [HttpGet]
        public ActionResult SelectDosageForm(string searchTerm, int pageSize, int pageNum)
        {
            var founder = new ExternalRepository().SearchDosageForm(searchTerm);
            var pagedAttendees = AttendeesToSelect2Format(founder, founder.Count);
            return new JsonpResult
            {
                Data = pagedAttendees,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// При выборе значение форма ЛС
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GetDosageFormNames(int id)
        {
            var mnn = new ExternalRepository().GetDosageFormById(id);
            if (mnn == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }

            return Json(new
            {
                nameRu = mnn.name,
                nameKz = mnn.name_kz,
                isSuccess = true
            });
        }
        [HttpGet]
        public ActionResult ShowAttach(long recordId)
        {
            var model = new DrugDeclarationRepository().GetRemarksListFiles(recordId);
            if (Request.IsAjaxRequest())
            {
                return PartialView(model);
            }

            return View(model);
        }        
    }
}
