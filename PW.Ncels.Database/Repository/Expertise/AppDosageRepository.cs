using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Repository.Common;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class AppDosageRepository : ARepository
    {
        public EXP_DrugDosage GetById(long? id)
        {
            return AppContext.EXP_DrugDosage.FirstOrDefault(e => e.Id == id);
        }

        public EXP_ExpertiseStageDosage GetStageByAppDosageId(Guid id)
        {
            return AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == id);
        }

        public EXP_ExpertiseStageDosage GetStageDosage(long dosageId, int dicStageId)
        {
            return AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(
                e => e.DosageId == dosageId && e.EXP_ExpertiseStage.StageId == dicStageId);
        }

        public EXP_ExpertiseSafetyreportFinalDoc GetExpExpertiseSafetyreportFinalDoc(long recordId)
        {
            return AppContext.EXP_ExpertiseSafetyreportFinalDoc.FirstOrDefault(e => e.EXP_ExpertiseStageDosage.DosageId == recordId);
        }

        public void DublcateDoage(string modelId, long dosageId, string userId)
        {

            var model = AppContext.EXP_DrugDeclaration.FirstOrDefault(e => e.Id == new Guid(modelId)) ??
                        new EXP_DrugDeclaration
            {
                OwnerId = new Guid(userId),
                TypeId = CodeConstManager.DRUG_REGISTER_ID,
                Id = new Guid(modelId),
                StatusId = CodeConstManager.STATUS_DRAFT_ID,
                CreatedDate = DateTime.Now,
            };

            if (dosageId == 0)
            {
                var addDosage = new EXP_DrugDosage
                {
                    EXP_DrugDeclaration = model
                };
                AppContext.EXP_DrugDosage.Add(addDosage);
                AppContext.SaveChanges();
                return;
            }

            var dosage = AppContext.EXP_DrugDosage.FirstOrDefault(e => e.Id == dosageId);
            if (dosage == null)
            {
                return;
            }
            var newDosage = new EXP_DrugDosage
            {
                AppPeriodMix = dosage.AppPeriodMix,
                AppPeriodMixMeasureDicId = dosage.AppPeriodMixMeasureDicId,
                AppPeriodOpen = dosage.AppPeriodOpen,
                AppPeriodOpenMeasureDicId = dosage.AppPeriodOpenMeasureDicId,
                BestBefore = dosage.BestBefore,
                BestBeforeMeasureTypeDicId = dosage.BestBeforeMeasureTypeDicId,
                ConcentrationKz = dosage.ConcentrationKz,
                ConcentrationRu = dosage.ConcentrationRu,
                Dosage = dosage.Dosage,
                DosageMeasureTypeId = dosage.DosageMeasureTypeId,
                DosageNoteKz = dosage.DosageNoteKz,
                DosageNoteRu = dosage.DosageNoteRu,
                DrugDeclarationId = dosage.DrugDeclarationId,
                SaleTypeId = dosage.SaleTypeId,
            };
            foreach (var expDrugPrice in dosage.EXP_DrugPrice)
            {
                var price = new EXP_DrugPrice()
                {
                    Barcode = expDrugPrice.Barcode,
                    CountUnit = expDrugPrice.CountUnit,
                    EXP_DrugDosage = newDosage,
                    IntermediateText = expDrugPrice.IntermediateText,
                    IntermediateValue = expDrugPrice.IntermediateValue,
                    ManufacturePrice = expDrugPrice.ManufacturePrice,
                    PrimaryText = expDrugPrice.PrimaryText,
                    PrimaryValue = expDrugPrice.PrimaryValue,
                    RefPrice = expDrugPrice.RefPrice,
                    RegPrice = expDrugPrice.RegPrice,
                    SecondaryText = expDrugPrice.SecondaryText,
                    SecondaryValue = expDrugPrice.SecondaryValue

                };
                newDosage.EXP_DrugPrice.Add(price);
            }
            foreach (var entity in dosage.EXP_DrugSubstance)
            {
                var substance = new EXP_DrugSubstance()
                {
                    EXP_DrugDosage = newDosage,
                    CategoryName = entity.CategoryName,
                    CategoryPos = entity.CategoryPos,
                    CountryId = entity.CountryId,
                    IsControl = entity.IsControl,
                    IsNotFound = entity.IsNotFound,
                    IsPoison = entity.IsPoison,
                    Locus = entity.Locus,
                    MeasureId = entity.MeasureId,
                    NewName = entity.NewName,
                    NormDocFarmId = entity.NormDocFarmId,
                    NormativeDocument = entity.NormativeDocument,
                    OriginId = entity.OriginId,
                    PlantKindId = entity.PlantKindId,
                    ProducerAddress = entity.ProducerAddress,
                    ProducerName = entity.ProducerName,
                    SubstanceCount = entity.SubstanceCount,
                    SubstanceId = entity.SubstanceId,
                    SubstanceName = entity.SubstanceName,
                    SubstanceTypeId = entity.SubstanceTypeId,

                };
                newDosage.EXP_DrugSubstance.Add(substance);
            }
            foreach (var entity in dosage.EXP_DrugWrapping)
            {
                var wrapping = new EXP_DrugWrapping()
                {
                    EXP_DrugDosage = newDosage,
                    CountUnit = entity.CountUnit,
                    Note = entity.Note,
                    SizeMeasureId = entity.SizeMeasureId,
                    VolumeMeasureId = entity.VolumeMeasureId,
                    WrappingKindId = entity.WrappingKindId,
                    WrappingSize = entity.WrappingSize,
                    WrappingTypeId = entity.WrappingTypeId,
                    WrappingVolume = entity.WrappingVolume,
                    WrappingSizeStr = entity.WrappingSizeStr,
                    WrappingVolumeStr = entity.WrappingVolumeStr
                };
                newDosage.EXP_DrugWrapping.Add(wrapping);
            }
            AppContext.EXP_DrugDosage.Add(newDosage);
            AppContext.SaveChanges();
        }

        public void CreateDublicateOrg(string modelId, long orgId)
        {
            var org = AppContext.EXP_DrugOrganizations.FirstOrDefault(e => e.Id == orgId);
            if (org == null)
            {
                return;
            }
            var newDosage = new EXP_DrugOrganizations
            {
                DrugDeclarationId = new Guid(modelId),
                AddressFact = org.AddressFact,
                AddressLegal = org.AddressLegal,
                BankBik = org.BankBik,
                BankCurencyDicId = org.BankCurencyDicId,
                BankIik = org.BankIik,
                BankName = org.BankName,
                BankSwift = org.BankSwift,
                Bin = org.Bin,
                BossFio = org.BossFio,
                BossFirstName = org.BossFirstName,
                BossLastName = org.BossLastName,
                BossMiddleName = org.BossMiddleName,
                BossPosition = org.BossPosition,
                ContactEmail = org.ContactEmail,
                ContactFax = org.ContactFax,
                ContactFio = org.ContactFio,
                ContactPhone = org.ContactPhone,
                ContactPosition = org.ContactPosition,
                CountryDicId = org.CountryDicId,
                DocDate = org.DocDate,
                DocExpiryDate = org.DocExpiryDate,
                DocNumber = org.DocNumber,
                Email = org.Email,
                Fax = org.Fax,
                Iin = org.Iin,
                IsResident = org.IsResident,
                ManufactureName = org.ManufactureName,
                NameEn = org.NameEn,
                NameKz = org.NameKz,
                NameRu = org.NameRu,
                ObjectId = org.ObjectId,
                OpfTypeDicId = org.OpfTypeDicId,
                OrgManufactureTypeDicId = org.OrgManufactureTypeDicId,
                PayerTypeDicId = org.PayerTypeDicId,
                PaymentBill = org.PaymentBill,
                Phone = org.Phone,
                Position = org.Position,
                Type = org.Type
            };
            AppContext.EXP_DrugOrganizations.Add(newDosage);
            AppContext.SaveChanges();
        }

        public void SetDrugReestr(Guid modelId, long dosageId, int? reestrId)
        {
            var drugTypes = new Dictionary<int, int>();
            drugTypes.Add(1, 1); //Лекарственный препарат
            drugTypes.Add(2, 2); //Иммунобиологический препарат
            drugTypes.Add(3, 3); //Лекарственное растительное сырье (сборы)
            drugTypes.Add(4, 4); //Гомеопатический препарат 
            drugTypes.Add(6, 5); //Лекарственная субстанция
            drugTypes.Add(7, 6); //Лекарственный балк-продукт
            drugTypes.Add(8, 7); //Иммунобиологический балк-продукт
            drugTypes.Add(9, 8); //Радиопрепарат
            drugTypes.Add(10, 9); //Не фармакопейное лекарственное растительное сырье
            drugTypes.Add(11, 10); //Лекарственный препарат биологического происхождения

            var monufactureType = new Dictionary<int, string>();
            monufactureType.Add(1, "1"); //Производитель
            monufactureType.Add(2, "2"); //Держатель лицензии
                                         //            monufactureType.Add(3, "1"); //Дистрибьютор
            monufactureType.Add(4, "4"); //Предприятие-упаковщик
            monufactureType.Add(5, "5"); //Заявитель
                                         //            monufactureType.Add(6, "1"); //Производитель субстанции
                                         //            monufactureType.Add(7, "1"); //Разработчик
                                         //            monufactureType.Add(8, "3"); //Владелец регистрационного удостоверения
            monufactureType.Add(9, "7"); //Выпускающий контроль
            monufactureType.Add(10, "3"); //Держатель регистрационного удостоверения

            var model = AppContext.EXP_DrugDeclaration.FirstOrDefault(e => e.Id == modelId);
            if (model == null)
            {
                return;
            }
            var drug = new ExternalRepository().GEtRegisterDrugById(reestrId);
            var reestr = new ExternalRepository().GetReestrById(reestrId.Value);
            if (!string.IsNullOrEmpty(model.NameRu))
            {
                model.NameRu = reestr.name;
            }
            if (!string.IsNullOrEmpty(model.NameKz))
            {
                model.NameKz = reestr.name_kz;
            }
            if (!string.IsNullOrEmpty(model.ConcentrationRu))
            {
                model.ConcentrationRu = drug.concentration;
            }
            if (!string.IsNullOrEmpty(model.ConcentrationKz))
            {
                model.ConcentrationKz = drug.concentration_kz;
            }
            if (model.AtxId == null)
            {
                model.AtxId = drug.atc_id;
            }
            if (model.MnnId == null)
            {
                model.MnnId = drug.int_name_id;
            }
            if (model.DrugFormId == null)
            {
                model.DrugFormId = drug.dosage_form_id;
            }

            if (drugTypes.ContainsKey(drug.drug_type_id))
            {
                if (model.EXP_DrugType.Count == 0)
                {
                    var type = new EXP_DrugType();
                    type.DrugTypeId = drugTypes[drug.drug_type_id];
                    model.EXP_DrugType.Add(type);
                }
                else
                {
                    model.EXP_DrugType.First().DrugTypeId = drugTypes[drug.drug_type_id];
                }
            }
            if (drug.sr_register_use_methods.Count > 0)
            {
                var usemethod = model.EXP_DrugUseMethod.Select(e => e.UseMethodsId) ?? new List<int>();
//                AppContext.EXP_DrugUseMethod.RemoveRange(model.EXP_DrugUseMethod);
                foreach (var drugSrRegisterUseMethod in drug.sr_register_use_methods)
                {
                    if (!usemethod.Contains(drugSrRegisterUseMethod.use_method_id))
                    {
                        var useMethod = new EXP_DrugUseMethod {UseMethodsId = drugSrRegisterUseMethod.use_method_id};
                        model.EXP_DrugUseMethod.Add(useMethod);
                    }
                }
            }
            var repository = new ReadOnlyDictionaryRepository();
            var orgManufactureTypes = repository.GetDictionaries(CodeConstManager.DIC_ORG_MANUFACTURE_TYPE);
            var countyDics = repository.GetDictionaries(CodeConstManager.DIC_COUNTRY_TYPE);

            if (reestr.sr_register_producers.Count > 0)
            {
                AppContext.EXP_DrugOrganizations.RemoveRange(model.EXP_DrugOrganizations);
                foreach (var registerProducer in reestr.sr_register_producers)
                {
                    var producer = new EXP_DrugOrganizations();
                    if (registerProducer.sr_producers != null)
                    {
                        producer.NameRu = registerProducer.sr_producers.name;
                        producer.NameEn = registerProducer.sr_producers.name_eng;
                        producer.NameKz = registerProducer.sr_producers.name_kz;
                        producer.Bin = registerProducer.sr_producers.bin;

                        if (monufactureType.ContainsKey(registerProducer.sr_producers.type_id))
                        {
                            var orgManufactureType =
                                orgManufactureTypes.FirstOrDefault(
                                    e => e.Code == monufactureType[registerProducer.sr_producers.type_id]);
                            if (orgManufactureType != null)
                            {
                                producer.OrgManufactureTypeDicId = orgManufactureType.Id;
                            }
                        }
                    }
                    if (registerProducer.sr_countries != null)
                    {
                        var country =
                            countyDics.FirstOrDefault(
                                e => e.Name.ToLower() == registerProducer.sr_countries.name.ToLower());
                        if (country != null)
                        {
                            producer.CountryDicId = country.Id;
                        }
                    }
                    model.EXP_DrugOrganizations.Add(producer);
                }
            }
            if (reestr.sr_register_substances.Count > 0)
            {
                EXP_DrugDosage dosage;
                if (dosageId == 0)
                {
                    dosage = new EXP_DrugDosage
                    {
                        ConcentrationRu = drug.concentration,
                        ConcentrationKz = drug.concentration_kz,
                    };
                }
                else
                {
                    dosage = AppContext.EXP_DrugDosage.FirstOrDefault(e => e.Id == dosageId);
                    AppContext.EXP_DrugSubstance.RemoveRange(dosage.EXP_DrugSubstance);
                    AppContext.EXP_DrugPrice.RemoveRange(dosage.EXP_DrugPrice);
                    AppContext.EXP_DrugWrapping.RemoveRange(dosage.EXP_DrugWrapping);
                }

              /*  foreach (var expDrugDosage in model.EXP_DrugDosage)
                {
                    AppContext.EXP_DrugSubstance.RemoveRange(expDrugDosage.EXP_DrugSubstance);
                    AppContext.EXP_DrugPrice.RemoveRange(expDrugDosage.EXP_DrugPrice);
                    AppContext.EXP_DrugWrapping.RemoveRange(expDrugDosage.EXP_DrugWrapping);
                }
                AppContext.EXP_DrugDosage.RemoveRange(model.EXP_DrugDosage);
*/

                if (drug.dosage_value != null)
                {
                    dosage.Dosage = drug.dosage_value.Value;
                }
                dosage.DosageMeasureTypeId = drug.dosage_measure_id;
                dosage.RegisterId = reestrId;

                foreach (var reestrSrRegisterSubstance in reestr.sr_register_substances)
                {
                    var substance = new EXP_DrugSubstance
                    {
                        SubstanceId = reestrSrRegisterSubstance.substance_id,
                        SubstanceTypeId = reestrSrRegisterSubstance.substance_type_id,
                        CountryId = reestrSrRegisterSubstance.country_id,
                        MeasureId = reestrSrRegisterSubstance.measure_id
                    };
                    if (reestrSrRegisterSubstance.substance_count != null)
                    {
                        substance.SubstanceCount = reestrSrRegisterSubstance.substance_count.ToString();
                    }
                    dosage.EXP_DrugSubstance.Add(substance);
                }

                foreach (var registerBoxes in reestr.sr_register_boxes)
                {
                    var wrapping = new EXP_DrugWrapping()
                    {
                        WrappingKindId = registerBoxes.box_id,
                        VolumeMeasureId = registerBoxes.volume_measure_id,
                        CountUnit = registerBoxes.unit_count,
                        Note = registerBoxes.description,
                    };

                    if (registerBoxes.volume != null)
                    {
                        wrapping.WrappingVolume = double.Parse(registerBoxes.volume.ToString());
                    }
                    if (!string.IsNullOrEmpty(registerBoxes.box_size))
                    {
                        double size;
                        if (double.TryParse(registerBoxes.box_size, out size))
                        {
                            wrapping.WrappingSize = size;
                        }
                    }
                    dosage.EXP_DrugWrapping.Add(wrapping);
                }
                if (dosage.Id == 0)
                {
                    model.EXP_DrugDosage.Add(dosage);
                }
            }
            try
            {
                AppContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


    }

        public void RemoveDrugReestr(long id)
        {
            var model = AppContext.EXP_DrugDosage.FirstOrDefault(e => e.Id == id);
            if (model != null)
            {
                AppContext.EXP_DrugSubstance.RemoveRange(model.EXP_DrugSubstance);
                AppContext.EXP_DrugPrice.RemoveRange(model.EXP_DrugPrice);
                AppContext.EXP_DrugWrapping.RemoveRange(model.EXP_DrugWrapping);
                AppContext.EXP_DrugDosage.Remove(model);
                AppContext.SaveChanges();
            }
        }
    }
}
