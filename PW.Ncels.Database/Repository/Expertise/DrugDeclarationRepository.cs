using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using Kendo.Mvc.UI;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Contract;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class DrugDeclarationRepository : ADrugDeclarationRepository
    {
        public EXP_DrugDeclaration GetPreamble(Guid id)
        {
            var context = CreateDatabaseContext(false);
            var preamble = context.EXP_DrugDeclaration
                .Include(e => e.EXP_DrugOtherCountry)
                .Include(e => e.EXP_DrugExportTrade)
                .Include(e => e.EXP_DrugOrganizations)
                .Include(e => e.EXP_DrugPatent)
                //                .Include(e => e.EXP_DrugPrice)
                .Include(e => e.EXP_DrugProtectionDoc)
                .Include(e => e.EXP_DrugType)
                .Include(e => e.EXP_DrugUseMethod)
                //                .Include(e => e.EXP_DrugWrapping).
                .AsNoTracking()
               .FirstOrDefault(e => e.Id == id);
            return preamble;
        }
        public IEnumerable<EXP_DrugDeclarationView> GetDeclarationViews(int? type)
        {
            if (type != null && type != 0)
            {
                return AppContext.EXP_DrugDeclarationView
                     .Where(m => m.TypeId == type).AsQueryable();

            }
            return AppContext.EXP_DrugDeclarationView
                .AsQueryable();
        }

        public IQueryable<EXP_DrugDeclarationRegisterView> DrugDeclarationRegisterByStatus(string status, int stage, Guid userId, DeclarationRegistryFilter customFilter)
        {
            var query =
                AppContext.EXP_DrugDeclarationRegisterView.Where(m => m.ExpertId == userId && m.DicStageId == stage);
            if (customFilter != null)
            {
                if (!string.IsNullOrEmpty(customFilter.Number))
                    query = query.Where(e => e.Number.Contains(customFilter.Number));
                if (customFilter.DeclarationDateFrom != null)
                    query = query.Where(e => e.FirstSendDate >= customFilter.DeclarationDateFrom);
                if (customFilter.DeclarationDateTo != null)
                    query = query.Where(e => e.FirstSendDate <= customFilter.DeclarationDateTo);
                if (customFilter.TypeId != null)
                    query = query.Where(e => e.TypeId == customFilter.TypeId);
                if (!string.IsNullOrEmpty(customFilter.ProducerName))
                {
                    customFilter.ProducerName = customFilter.ProducerName.ToLower();
                    query = query.Where(e => e.ProducerRu.ToLower().Contains(customFilter.ProducerName)
                    || e.ProducerKz.ToLower().Contains(customFilter.ProducerName)
                    || e.ProducerEn.ToLower().Contains(customFilter.ProducerName));
                }
                if (!string.IsNullOrEmpty(customFilter.ProducerCountry))
                {
                    customFilter.ProducerCountry = customFilter.ProducerCountry.ToLower();
                    query = query.Where(e => e.CountryRu.ToLower().Contains(customFilter.ProducerCountry)
                    || e.CountryKz.ToLower().Contains(customFilter.ProducerCountry));
                }
                if (!string.IsNullOrEmpty(customFilter.DrugName))
                {
                    customFilter.DrugName = customFilter.DrugName.ToLower();
                    query = query.Where(e => e.NameRu.ToLower().Contains(customFilter.DrugName)
                                             || e.NameKz.ToLower().Contains(customFilter.DrugName)
                                             || e.NameEn.ToLower().Contains(customFilter.DrugName));
                }
                if (!string.IsNullOrEmpty(customFilter.Mnn))
                {
                    customFilter.Mnn = customFilter.Mnn.ToLower();
                    query = query.Where(e => e.MnnRu.ToLower().Contains(customFilter.Mnn)
                                             || e.MnnKz.ToLower().Contains(customFilter.Mnn)
                                             || e.MnnEn.ToLower().Contains(customFilter.Mnn));
                }

                if (!string.IsNullOrEmpty(customFilter.ApplicantName))
                {
                    customFilter.ApplicantName = customFilter.ApplicantName.ToLower();
                    query = query.Where(e => e.ApplicantRu.ToLower().Contains(customFilter.ApplicantName)
                                             || e.ApplicantKz.ToLower().Contains(customFilter.ApplicantName)
                                             || e.ApplicantEn.ToLower().Contains(customFilter.ApplicantName));
                }
                if (!string.IsNullOrEmpty(customFilter.ApplicantCountry))
                {
                    customFilter.ApplicantCountry = customFilter.ApplicantCountry.ToLower();
                    query = query.Where(e => e.ApplicantCountryRu.ToLower().Contains(customFilter.ApplicantCountry)
                                             || e.ApplicantCountryKz.ToLower().Contains(customFilter.ApplicantCountry));
                }

                if (!string.IsNullOrEmpty(customFilter.HolderName))
                {
                    customFilter.HolderName = customFilter.HolderName.ToLower();
                    query = query.Where(e => e.HolderRu.ToLower().Contains(customFilter.HolderName)
                                             || e.HolderKz.ToLower().Contains(customFilter.HolderName)
                                             || e.HolderEn.ToLower().Contains(customFilter.HolderName));
                }
                if (!string.IsNullOrEmpty(customFilter.HolderCountry))
                {
                    customFilter.HolderCountry = customFilter.HolderCountry.ToLower();
                    query = query.Where(e => e.HolderCountryRu.ToLower().Contains(customFilter.HolderCountry)
                                             || e.HolderCountryKz.ToLower().Contains(customFilter.HolderCountry));
                }

            }
            return query;
        }

        public async Task<object> GetDrugDeclarationList(ModelRequest request, bool isRegisterProject, int? type)
        {
            try
            {
                //Database query
                var employeeId = UserHelper.GetCurrentEmployee().Id;
                var org = UserHelper.GetCurrentEmployee();
                var v = type != null ? AppContext.EXP_DrugDeclarationView.Where(m => m.OwnerId == employeeId).OrderByDescending(m => m.SortDate).AsQueryable()
                    : AppContext.EXP_DrugDeclarationView.Where(m => m.OwnerId == employeeId).AsQueryable();
                //search
                if (!string.IsNullOrEmpty(request.SearchValue))
                {
                    v =
                        v.Where(
                            a =>
                                a.Number.Contains(request.SearchValue) || a.NameRu.Contains(request.SearchValue)
                              );
                }

                //sort
                if (!(string.IsNullOrEmpty(request.SortColumn) && string.IsNullOrEmpty(request.SortColumnDir)))
                {
                    v = v.OrderBy(request.SortColumn + " " + request.SortColumnDir);
                }


                int recordsTotal = await v.CountAsync();
                var expertiseViews = v.Skip(request.Skip).Take(request.PageSize);
                return
                    new
                    {
                        draw = request.Draw,
                        recordsFiltered = recordsTotal,
                        recordsTotal = recordsTotal,
                        Data = await expertiseViews.ToListAsync()
                    };
            }

            catch (Exception e)
            {
                return new { IsError = true, Message = e.Message };
            }

        }


        public SubUpdateField UpdateModel(string code, string modelId, string userId, long? recordId, string fieldName,
            string fieldValue, string fieldDisplay)
        {
            bool isNew = false;
            var model = GetById(modelId);
            if (model == null)
            {
                model = new EXP_DrugDeclaration
                {
                    OwnerId = new Guid(userId),
                    TypeId = CodeConstManager.DRUG_REGISTER_ID,
                    Id = new Guid(modelId),
                    StatusId = CodeConstManager.STATUS_DRAFT_ID,
                    CreatedDate = DateTime.Now,
                };
                isNew = true;
            }

            if (fieldName == "MethodUseIds")
            {
                return UpdateMethodUseIds(model, fieldName, fieldValue, userId, fieldDisplay);
            }

            switch (code)
            {
                case "main":
                    {
                        return UpdateMain(isNew, model, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugPrimaryKindCode:
                    {
                        return UpdatePrimaryKind(model, fieldName, fieldValue, userId);
                    }
                case CodeConstManager.EXP_DrugExportTradeCode:
                    {
                        return UpdateCollection<EXP_DrugExportTrade>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugPatentCode:
                    {
                        return UpdateCollection<EXP_DrugPatent>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugTypeCode:
                    {
                        return UpdateCollection<EXP_DrugType>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugDosageCode:
                    {
                        return UpdateCollection<EXP_DrugDosage>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                /* case CodeConstManager.EXP_DrugWrappingCode:
                     {
                         return UpdateCollection<EXP_DrugWrapping>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                     }*/

                case CodeConstManager.EXP_DrugProtectionDoc:
                    {
                        return UpdateCollection<EXP_DrugProtectionDoc>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugOtherCountry:
                    {
                        return UpdateCollection<EXP_DrugOtherCountry>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugOrganizationsCode:
                    {
                        return UpdateCollection<EXP_DrugOrganizations>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugChangeTypeCode:
                    {
                        return UpdateCollection<EXP_DrugChangeType>(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugPrimaryNTDCode:
                    {
                        return UpdatePrimaryNTD(model, fieldName, fieldValue, userId);
                    }
                /*  case CodeConstManager.EXP_DrugPrimaryOTDCode:
                      {
                          return UpdateOtd(model, fieldName, fieldValue, userId, fieldDisplay);
                      }*/
                case CodeConstManager.EXP_ExpertiseStageRemarkCode:
                    {
                        return UpdateExpertiseStageRemark(code, model, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
            }
            return null;
        }
        public SubUpdateField UpdateExpertiseStageRemark(string code, EXP_DrugDeclaration model, long? recordId, string fieldName, string fieldValue, string userId, string fieldDisplay)
        {
            EXP_ExpertiseStageRemark entity = null;
            if (recordId > 0)
            {
                entity = AppContext.Set<EXP_ExpertiseStageRemark>().FirstOrDefault(e => e.Id == recordId);
            }
            int otdId = 0;
            if (entity == null)
            {
                entity = new EXP_ExpertiseStageRemark { StageId = model.Id, ExecuterId = new Guid(userId) };
                var otdIdName = fieldName.Split('_')[2];
                entity.StageId = new Guid(fieldName.Split('_')[1]);
                if (int.TryParse(otdIdName, out otdId) && otdId > 0)
                {
                    entity.OtdId = otdId;
                }
            }


            var columnName = fieldName.Split('_')[3];
            var property = entity.GetType().GetProperty(columnName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }
            if (entity.Id == 0)
            {
                AppContext.EXP_ExpertiseStageRemark.Add(entity);
            }
            AppContext.SaveChanges();
            var idControl = code + "_" + entity.Id + "_" + otdId + "_" + columnName;
            SaveHistoryField(model.Id, idControl, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField
            {
                ModelId = model.ObjectId,
                RecordId = entity.Id,
                ControlId = idControl
            };
            return subUpdateField;
        }


        /*    private SubUpdateField UpdateOtd(EXP_DrugDeclaration model, string fieldName, string fieldValue, string userId, string fieldDisplay)
            {
                var subUpdateField = new SubUpdateField();
                subUpdateField.ModelId = model.ObjectId;
                int otdId;
                if (!int.TryParse(fieldName, out otdId))
                {
                    return subUpdateField;
                }
                var isChecked = bool.Parse(fieldValue);
                var repository = new ReadOnlyDictionaryRepository().GetExpDicPrimaryOTDs();
                var dic = repository.FirstOrDefault(e => e.Id == otdId);
                if (model.OtdIds == null)
                {
                    model.OtdIds = "";
                }
                var otdIds = model.OtdIds.Split(',').ToList();
                GetOtdChildren(otdIds, dic, isChecked);

                model.OtdIds = string.Join(",", otdIds);
                AppContext.SaveChanges();
                //            SaveHistoryField(model.Id, "OtdIds", model.OtdIds, new Guid(userId), model.OtdIds);

                return subUpdateField;
            }
    */

        private SubUpdateField UpdatePrimaryNTD(EXP_DrugDeclaration model, string fieldName, string fieldValue, string userId)
        {
            var ntd = AppContext.EXP_DrugPrimaryNTD.FirstOrDefault(e => e.DrugDeclarationId == model.Id);
            if (ntd == null)
            {
                ntd = new EXP_DrugPrimaryNTD();
                ntd.DrugDeclarationId = model.Id;
            }

            var columnName = fieldName.Split('_')[2];
            var property = ntd.GetType().GetProperty(columnName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(ntd, safeValue, null);
            }
            if (ntd.Id == 0)
            {
                AppContext.Set<EXP_DrugPrimaryNTD>().Add(ntd);
            }
            AppContext.SaveChanges();
            SaveHistoryField(model.Id, fieldName, fieldValue, new Guid(userId), fieldValue);
            var idControl = CodeConstManager.EXP_DrugPrimaryNTDCode + "_" + model.Id + "_"  + columnName;

            var subUpdateField = new SubUpdateField
            {
                ModelId = model.ObjectId,
                RecordId = ntd.Id,
                ControlId = idControl
            };
            return subUpdateField;
        }

        private SubUpdateField UpdatePrimaryKind(EXP_DrugDeclaration model, string fieldName, string fieldValue, string userId)
        {
            var subUpdateField = new SubUpdateField();
            var check = bool.Parse(fieldValue);
            var kindId = Convert.ToInt32(fieldName);
            var drugPrimaryKind = AppContext.EXP_DrugPrimaryKind.FirstOrDefault(e => e.DrugDeclarationId == model.Id && e.PrimaryKindId == kindId);
            if (drugPrimaryKind != null && check)
            {
                return subUpdateField;
            }
            if (drugPrimaryKind != null)
            {
                var mark = drugPrimaryKind.EXP_DIC_PrimaryMark.NameRu;
                AppContext.EXP_DrugPrimaryKind.Remove(drugPrimaryKind);
                SaveHistoryField(model.Id, mark, fieldValue, new Guid(userId), "Нет");
                return subUpdateField;
            }

            if (!check) return subUpdateField;
            {
                drugPrimaryKind = new EXP_DrugPrimaryKind
                {
                    DrugDeclarationId = model.Id,
                    PrimaryKindId = kindId
                };
                AppContext.EXP_DrugPrimaryKind.Add(drugPrimaryKind);

                var drugKind = AppContext.EXP_DIC_PrimaryMark.FirstOrDefault(e => e.Id == kindId);
                if (drugKind != null) SaveHistoryField(model.Id, drugKind.NameRu, fieldValue, new Guid(userId), "Да");
                return subUpdateField;
            }
        }

        private SubUpdateField UpdateCollection<T>(string code, EXP_DrugDeclaration model, long? recordId,
            string fieldName, string fieldValue, string userId, string fieldDisplay) where T : class, IDrugDeclarationCollection, new()
        {

            T entity = null;
            if (recordId > 0)
            {
                entity = AppContext.Set<T>().FirstOrDefault(e => e.Id == recordId);
            }
            if (entity == null)
            {
                entity = new T { EXP_DrugDeclaration = model };
            }
            var columnName = fieldName.Split('_')[2];
            var property = entity.GetType().GetProperty(columnName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }
            if (entity.Id == 0)
            {
                AppContext.Set<T>().Add(entity);
            }
            AppContext.SaveChanges();
            var idControl = code + "_" + entity.Id + "_" + columnName;
            SaveHistoryField(model.Id, idControl, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField
            {
                ModelId = model.ObjectId,
                RecordId = entity.Id,
                ControlId = idControl
            };
            return subUpdateField;
        }


        private SubUpdateField UpdateMethodUseIds(EXP_DrugDeclaration model, string fieldName, string fieldValue, string userId, string fieldDisplay)
        {
            var list = new List<int>();
            if (fieldValue != null)
            {
                var array = fieldValue.Split(',');
                foreach (var s in array)
                {
                    int val;
                    if (int.TryParse(s, out val))
                    {
                        list.Add(val);
                    }
                }
            }
            fieldDisplay = string.Join(",", AppContext.sr_use_methods.Where(e => list.Contains(e.id)).Select(e => e.name));
            var apps = model.EXP_DrugUseMethod.Where(e => !list.Contains(e.UseMethodsId)).ToArray();
            if (apps.Any())
            {
                AppContext.EXP_DrugUseMethod.RemoveRange(apps);
            }
            var appsAdd = list.Where(e => !model.EXP_DrugUseMethod.Select(m => m.UseMethodsId).Contains(e));

            foreach (var l in appsAdd)
            {
                AppContext.EXP_DrugUseMethod.Add(new EXP_DrugUseMethod
                {
                    DrugDeclarationId = model.Id,
                    UseMethodsId = l
                });
            }
            AppContext.SaveChanges();
            SaveHistoryField(model.Id, fieldName, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField { ModelId = model.ObjectId };
            return subUpdateField;

        }

        private SubUpdateField UpdateMain(bool isNew, EXP_DrugDeclaration model, string fieldName, string fieldValue, string userId,
            string fieldDisplay)
        {

            var property = model.GetType().GetProperty(fieldName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);
                }
                property.SetValue(model, safeValue, null);
            }
            if (isNew)
            {
                AppContext.EXP_DrugDeclaration.Add(model);
                AppContext.SaveChanges();
            }
            SaveHistoryField(model.Id, fieldName, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField();
            subUpdateField.ModelId = model.ObjectId;
            return subUpdateField;
        }


        public EXP_DrugDeclarationCom GetComments(string modelId, string idControl)
        {
            return
                AppContext.EXP_DrugDeclarationCom.FirstOrDefault(
                    e => e.ControlId == idControl && modelId == e.DrugDeclarationId.ToString());
        }

        public List<EXP_DrugDeclarationFieldHistory> GetFieldHistories(string modelId, string idControl)
        {
            return
                AppContext.EXP_DrugDeclarationFieldHistory.Where(
                    e => e.ControlId == idControl && e.DrugDeclarationId.ToString() == modelId).ToList();
        }

        public void SaveComment(string modelId, string idControl, bool isError, string comment, string fieldValue,
            string userId, string fieldDisplay)
        {
            var entityId = new Guid(modelId);
            var model =
                AppContext.EXP_DrugDeclarationCom.FirstOrDefault(
                    e => e.ControlId == idControl && e.DrugDeclarationId.Equals(entityId)) ??
                new EXP_DrugDeclarationCom
                {
                    DateCreate = DateTime.Now,
                    DrugDeclarationId = entityId,
                    ControlId = idControl,
                };

            model.IsError = isError;
            model.EXP_DrugDeclarationComRecord.Add(new EXP_DrugDeclarationComRecord
            {
                CreateDate = DateTime.Now,
                Note = comment,
                UserId = new Guid(userId),
                EXP_DrugDeclarationCom = model,
                ValueField = fieldValue,
                DisplayField = fieldDisplay
            });
            if (model.Id == 0)
            {

                AppContext.EXP_DrugDeclarationCom.Add(model);
            }
            AppContext.SaveChanges();
        }

        public void DeleteRecord(string code, long recordId)
        {
            switch (code)
            {
                case CodeConstManager.EXP_DrugExportTradeCode:
                    {
                        var form4 = AppContext.EXP_DrugExportTrade.FirstOrDefault(e => e.Id == recordId);
                        if (form4 != null)
                        {
                            AppContext.EXP_DrugExportTrade.Remove(form4);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugPatentCode:
                    {
                        var form5 = AppContext.EXP_DrugPatent.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugPatent.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugTypeCode:
                    {
                        var form5 = AppContext.EXP_DrugType.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugType.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugWrappingCode:
                    {
                        var form5 = AppContext.EXP_DrugWrapping.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugWrapping.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugSubstanceCode:
                    {
                        var form5 = AppContext.EXP_DrugSubstance.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugSubstance.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugOrganizationsCode:
                    {
                        var form5 = AppContext.EXP_DrugOrganizations.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugOrganizations.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugPrice:
                    {
                        var form5 = AppContext.EXP_DrugPrice.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugPrice.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugAppDosageRemarkCode:
                    {
                        var form5 = AppContext.EXP_DrugAppDosageRemark.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugAppDosageRemark.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugAppDosageResultCode:
                    {
                        var form5 = AppContext.EXP_DrugAppDosageResult.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugAppDosageResult.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugChangeTypeCode:
                    {
                        var form5 = AppContext.EXP_DrugChangeType.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugChangeType.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_ExpertiseStageRemarkCode:
                    {
                        var form5 = AppContext.EXP_ExpertiseStageRemark.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_ExpertiseStageRemark.Remove(form5);
                            if (form5.OtdId != null)
                            {
                                var stage = AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == form5.StageId);
                                if (stage != null)
                                {
                                    var otdIds = stage.OtdIds.Split(',').ToList();
                                    if (!otdIds.Contains(form5.OtdId.ToString()))
                                    {
                                        otdIds.Add(form5.OtdId.ToString());
                                    }
                                    stage.OtdIds = string.Join(",", otdIds);
                                }
                            }
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugSubstanceManufactureCode:
                    {
                        var form5 = AppContext.EXP_DrugSubstanceManufacture.FirstOrDefault(e => e.Id == recordId);
                        if (form5 != null)
                        {
                            AppContext.EXP_DrugSubstanceManufacture.Remove(form5);
                            AppContext.SaveChanges();
                        }
                        break;
                    }
                case CodeConstManager.EXP_DrugDosageCode:
                    {
                        var dosage = AppContext.EXP_DrugDosage.FirstOrDefault(e => e.Id == recordId);
                        if (dosage != null)
                        {
                            var deleteWrap = new List<EXP_DrugWrapping>();
                            foreach (var wrap in dosage.EXP_DrugWrapping)
                            {
                                deleteWrap.Add(wrap);

                            }
                            foreach (var expDrugWrapping in deleteWrap)
                            {
                                AppContext.EXP_DrugWrapping.Remove(expDrugWrapping);
                            }
                            var deletePrice = new List<EXP_DrugPrice>();
                            foreach (var price in dosage.EXP_DrugPrice)
                            {
                                deletePrice.Add(price);
                            }
                            foreach (var expDrugWrapping in deletePrice)
                            {
                                AppContext.EXP_DrugPrice.Remove(expDrugWrapping);
                            }

                            var deleteSubstance = new List<EXP_DrugSubstance>();
                            foreach (var price in dosage.EXP_DrugSubstance)
                            {
                                deleteSubstance.Add(price);
                            }
                            foreach (var expDrugWrapping in deleteSubstance)
                            {
                                var deleteManufacture = new List<EXP_DrugSubstanceManufacture>();
                                foreach (var wrap in expDrugWrapping.EXP_DrugSubstanceManufacture)
                                {
                                    deleteManufacture.Add(wrap);

                                }
                                foreach (var subman in deleteManufacture)
                                {
                                    AppContext.EXP_DrugSubstanceManufacture.Remove(subman);
                                }

                                AppContext.EXP_DrugSubstance.Remove(expDrugWrapping);
                            }
                            AppContext.EXP_DrugDosage.Remove(dosage);

                            AppContext.SaveChanges();
                        }
                        break;
                    }
            }
        }

        public Organization GetInfoFromContract(long recordId, string type)
        {
            var model = AppContext.EXP_DrugOrganizations.FirstOrDefault(e => e.Id == recordId);
            if (model == null)
            {
                return null;
            }
            var contractId = model.EXP_DrugDeclaration.ContractId;
            var contract = new ContractRepository(true).GetContractById(contractId);
            Organization organization = null;
            switch (type)
            {
                case CodeConstManager.ORG_APPLICANT_ID:
                    {
                        organization = contract.ApplicantOrganization;
                        break;
                    }
                case CodeConstManager.ORG_HOLDER_ID:
                    {
                        organization = contract.HolderOrganization;
                        break;
                    }
                case CodeConstManager.ORG_MANUFACTURE_ID:
                    {
                        organization = contract.ManufacturerOrganization;
                        break;
                    }
            }
            if (organization == null)
            {
                model.AddressFact = null;
                model.AddressLegal = null;
                model.OpfTypeDicId = null;
                model.CountryDicId = null;
                model.NameRu = null;
                model.NameKz = null;
                model.NameEn = null;
                model.DocNumber = null;
                model.DocDate = null;
                model.DocExpiryDate = null;
                model.BossPosition = null;
                model.BossFirstName = null;
                model.BossLastName = null;
                model.BossMiddleName = null;
                model.Phone = null;
                model.Email = null;
                model.ContactFio = null;
                model.ContactPosition = null;
            }
            else
            {
                model.AddressFact = organization.AddressFact;
                model.AddressLegal = organization.AddressFact;
                model.OpfTypeDicId = organization.OpfTypeDicId;
                model.CountryDicId = organization.CountryDicId;
                model.NameRu = organization.NameRu;
                model.NameKz = organization.NameKz;
                model.NameEn = organization.NameEn;
                model.DocNumber = organization.DocNumber;
                model.DocDate = organization.DocDate;
                model.DocExpiryDate = organization.DocExpiryDate;
                model.BossPosition = organization.BossPosition;
                model.BossFirstName = organization.BossFirstName;
                model.BossLastName = organization.BossLastName;
                model.BossMiddleName = organization.BossMiddleName;
                model.Phone = organization.Phone;
                model.Email = organization.Email;
                model.ContactFio = organization.ContactFio;
                model.ContactPosition = organization.ContactPosition;

            }
            AppContext.SaveChanges();
            return organization;

        }

        public EXP_DrugPrice SetPrice(long dosageId, string userId, string kinds1, string kinds2, string kinds3, double? calc)
        {
            var model = new EXP_DrugPrice();
            //            model.DrugDeclarationId = new Guid(modelId);
            model.PrimaryValue = kinds1;
            model.SecondaryValue = kinds2;
            model.IntermediateValue = kinds3;
            model.CountUnit = calc;
            model.DrugDosageId = dosageId;
            AppContext.EXP_DrugPrice.Add(model);
            AppContext.SaveChanges();
            model.PrimaryText = GetNameByWrappingNames(kinds1);
            model.SecondaryText = GetNameByWrappingNames(kinds2);
            model.IntermediateText = GetNameByWrappingNames(kinds3);
            return model;
        }

        public string GetNameByWrappingNames(string kinds)
        {
            if (string.IsNullOrEmpty(kinds))
            {
                return "";
            }
            var kindId = kinds.Split(',');
            if (kindId.Length == 0)
            {
                return "";
            }
            var ints = new List<int>();
            foreach (var s in kindId)
            {
                ints.Add(Convert.ToInt32(s));
            }
            var list = AppContext.sr_boxes.Where(e => ints.Contains(e.id)).Select(e => e.name);
            return string.Join(";", list);

        }
        public string GetAppNumber()
        {
            int year = DateTime.Now.Year;
            var numer = AppContext.EXP_DrugDeclaration.Where(e => e.SendDate.Value.Year == year).Max(e => e.Number);
            if (numer == null)
            {
                return year + "000001";
            }
            long numberConvert;
            if (long.TryParse(numer, out numberConvert))
            {
                var newNumber = numberConvert + 1;
                return newNumber.ToString();
            }
            return null;
        }
        public void SaveHisotry(EXP_DrugDeclarationHistory history, Guid? getCurrentUserId)
        {
            AppContext.EXP_DrugDeclarationHistory.Add(history);
            AppContext.SaveChanges();
        }
        public virtual EXP_DrugDeclaration SaveOrUpdate(EXP_DrugDeclaration entity, Guid? userId)
        {

            if (entity.Id == Guid.Empty)
            {
                try
                {
                    entity.CreatedDate = DateTime.Now;
                    AppContext.MarkAsAdded(entity);
                    AppContext.Commit(true);
                    return entity;
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

            var suspendedStage = AppContext.EXP_ExpertiseStage.FirstOrDefault(x =>
                !x.IsHistory
                && x.DeclarationId == entity.Id
                && x.IsSuspended
            );
            if (suspendedStage != null)
            {
                LogHelper.Log.DebugFormat("Найден приостановленный этап {0}", suspendedStage.Id);
                if (suspendedStage.SuspendedStartDate.HasValue)
                {
                    suspendedStage.IsSuspended = false;
                    var suspendedDays = (DateTime.Now - suspendedStage.SuspendedStartDate.Value).TotalDays;
                    LogHelper.Log.DebugFormat("Всего дней приостановки {0}", suspendedDays);
                    if (suspendedStage.EndDate.HasValue)
                    {
                        suspendedStage.EndDate = suspendedStage.EndDate.Value.AddDays(suspendedDays);
                    }
                    else
                    {
                        LogHelper.Log.DebugFormat("У этапа {0} не указана дата завершения исполнения EndDate", suspendedStage.Id);
                    }
                    AppContext.Commit(true);
                }
                else
                {
                    LogHelper.Log.DebugFormat("У этапа {0} почему-то не указана дата начала приостановки", suspendedStage.Id);
                }
            }
            else
            {
                LogHelper.Log.Debug("Заявление не содержит приостановленных этапов");
            }

            var attachedEntity = AppContext.Set<EXP_DrugDeclaration>().Find(entity.Id);
            AppContext.Entry(attachedEntity).CurrentValues.SetValues(entity);
            AppContext.Commit(true);
            //Отправка заявления на этап ЦОЗ
            if (entity.StatusId != CodeConstManager.STATUS_DRAFT_ID)
            {
                string resultDescription;
                var stageRepository = new ExpertiseStageRepository();
                if (!stageRepository.HasStage(entity.Id, CodeConstManager.STAGE_COZ))
                    stageRepository.ToNextStage(entity.Id, null, new[] { CodeConstManager.STAGE_COZ }, out resultDescription);
            }
            return entity;
        }

        public EXP_DrugDeclaration Update(EXP_DrugDeclaration declaration)
        {
            var attachedEntity = AppContext.Set<EXP_DrugDeclaration>().Find(declaration.Id);
            AppContext.Entry(attachedEntity).CurrentValues.SetValues(declaration);
            AppContext.Commit(true);
            return declaration;
        }

        public void DeleteReport(string id, Guid guid)
        {
            var model = GetById(id);
            model.IsDeleted = true;
            AppContext.SaveChanges();
        }
        public EXP_DrugDeclarationView GetByViewId(string modelId)
        {
            return AppContext.EXP_DrugDeclarationView.FirstOrDefault(e => e.Id == new Guid(modelId));
        }

        public SubUpdateField UpdateSubModel(string code, string modelId, long? subModelId, string userId, long? recordId, string fieldName, string fieldValue, string fieldDisplay)
        {
            EXP_DrugDeclaration model;
            bool isNew = false;
            if (string.IsNullOrEmpty(modelId))
            {
                model = new EXP_DrugDeclaration
                {
                    OwnerId = new Guid(userId),
                    StatusId = CodeConstManager.STATUS_DRAFT_ID,
                    CreatedDate = DateTime.Now,
                    TypeId = CodeConstManager.DRUG_REGISTER_ID,
                    Id = Guid.NewGuid(),
                };
            }
            else
            {
                model = GetById(modelId);
                if (model == null)
                {
                    return null;
                }
            }
            switch (code)
            {
                case CodeConstManager.EXP_DrugWrappingCode:
                    {
                        return UpdateWrapping(model, subModelId, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugPrice:
                    {
                        return UpdatePrice(model, subModelId, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugSubstanceManufactureCode:
                    {
                        return UpdateSubstanceManufacture(model, subModelId, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugAppDosageRemarkCode:
                    {
                        return UpdateDosageCollection<EXP_DrugAppDosageRemark>(code, model, subModelId, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugAppDosageResultCode:
                    {
                        return UpdateDosageCollection<EXP_DrugAppDosageResult>(code, model, subModelId, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
                case CodeConstManager.EXP_DrugSubstanceCode:
                    {
                        return UpdateDosageCollection<EXP_DrugSubstance>(code, model, subModelId, recordId, fieldName, fieldValue, userId, fieldDisplay);
                    }
            }
            return null;
        }

        private SubUpdateField UpdateSubstanceManufacture(EXP_DrugDeclaration model, long? subModelId, long? recordId, string fieldName, string fieldValue, string userId, string fieldDisplay)
        {

            EXP_DrugSubstanceManufacture entity = null;
            if (recordId > 0)
            {
                entity = AppContext.Set<EXP_DrugSubstanceManufacture>().FirstOrDefault(e => e.Id == recordId);
            }
            if (entity == null)
            {
                entity = new EXP_DrugSubstanceManufacture();
                EXP_DrugSubstance dosage;
                if (subModelId > 0)
                {
                    dosage = AppContext.EXP_DrugSubstance.FirstOrDefault(e => e.Id == subModelId);
                    if (dosage != null)
                    {
                        entity.DrugSubstanceId = subModelId.Value;
                    }
                }

            }
            var columnName = fieldName.Split('_')[1];
            var property = entity.GetType().GetProperty(columnName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }
            if (entity.Id == 0)
            {
                AppContext.Set<EXP_DrugSubstanceManufacture>().Add(entity);
            }

            AppContext.SaveChanges();
            var idControl = entity.DrugSubstanceId + "_" + entity.Id + "_" + columnName;
            SaveHistoryField(model.Id, idControl, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField
            {
                ModelId = model.ObjectId,
                RecordId = entity.Id,
                SubModelId = entity.DrugSubstanceId,
                ControlId = idControl
            };
            return subUpdateField;
        }

        private SubUpdateField UpdateWrapping(EXP_DrugDeclaration model, long? subModelId, long? recordId, string fieldName, string fieldValue, string userId, string fieldDisplay)
        {
            EXP_DrugWrapping entity = null;
            if (recordId > 0)
            {
                entity = AppContext.Set<EXP_DrugWrapping>().FirstOrDefault(e => e.Id == recordId);
            }
            if (entity == null)
            {
                entity = new EXP_DrugWrapping();
                EXP_DrugDosage dosage;
                if (subModelId > 0)
                {
                    dosage = AppContext.EXP_DrugDosage.FirstOrDefault(e => e.Id == subModelId);
                    if (dosage != null)
                    {
                        entity.DrugDosageId = subModelId.Value;
                    }
                }
                else
                {
                    dosage = new EXP_DrugDosage { EXP_DrugDeclaration = model };
                    entity.EXP_DrugDosage = dosage;
                }

            }
            var columnName = fieldName.Split('_')[3];
            var property = entity.GetType().GetProperty(columnName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }
            if (entity.Id == 0)
            {
                AppContext.Set<EXP_DrugWrapping>().Add(entity);
            }

            AppContext.SaveChanges();
            var idControl = CodeConstManager.EXP_DrugWrappingCode + "_" + entity.DrugDosageId + "_" + entity.Id + "_" + columnName;
            SaveHistoryField(model.Id, idControl, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField
            {
                ModelId = model.ObjectId,
                RecordId = entity.Id,
                SubModelId = entity.DrugDosageId,
                ControlId = idControl
            };
            return subUpdateField;
        }
        private SubUpdateField UpdatePrice(EXP_DrugDeclaration model, long? subModelId, long? recordId, string fieldName, string fieldValue, string userId, string fieldDisplay)
        {
            EXP_DrugPrice entity = null;
            if (recordId > 0)
            {
                entity = AppContext.Set<EXP_DrugPrice>().FirstOrDefault(e => e.Id == recordId);
            }
            if (entity == null)
            {
                return null;
            }
            var columnName = fieldName.Split('_')[3];
            var property = entity.GetType().GetProperty(columnName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                object safeValue;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }
            if (entity.Id == 0)
            {
                AppContext.Set<EXP_DrugPrice>().Add(entity);
            }

            AppContext.SaveChanges();
            var idControl = CodeConstManager.EXP_DrugPrice + "_" + entity.DrugDosageId + "_" + entity.Id + "_" + columnName;
            SaveHistoryField(model.Id, idControl, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField
            {
                ModelId = model.ObjectId,
                RecordId = entity.Id,
                SubModelId = entity.DrugDosageId,
                ControlId = idControl
            };
            return subUpdateField;
        }

        private SubUpdateField UpdateDosageCollection<T>(string code, EXP_DrugDeclaration model, long? subModelId, long? recordId, string fieldName, string fieldValue, string userId, string fieldDisplay) where T : class, IEXP_DrugDosageCollection, new()
        {
            T entity = null;
            if (recordId > 0)
            {
                entity = AppContext.Set<T>().FirstOrDefault(e => e.Id == recordId);
            }
            if (entity == null)
            {
                entity = new T();
                EXP_DrugDosage dosage;
                if (subModelId > 0)
                {
                    dosage = AppContext.EXP_DrugDosage.FirstOrDefault(e => e.Id == subModelId);
                    if (dosage != null)
                    {
                        entity.DrugDosageId = subModelId.Value;
                    }
                }
                else
                {
                    dosage = new EXP_DrugDosage { EXP_DrugDeclaration = model };
                    entity.EXP_DrugDosage = dosage;
                }
            }
            var columnName = fieldName.Split('_')[3];
            var property = entity.GetType().GetProperty(columnName);
            object safeValue = null;
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                if (t == typeof(Guid))
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(new Guid(fieldValue), t);
                }
                else
                {
                    safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);

                }
                property.SetValue(entity, safeValue, null);
            }
            if (code == CodeConstManager.EXP_DrugSubstanceCode && columnName == "SubstanceId" && safeValue != null)
            {
                var substance = new ExternalRepository().GetSubstanceById(Convert.ToInt32(safeValue));
                if (substance != null && substance.category_id != null)
                {
                    var propertyType = entity.GetType().GetProperty("IsControl");
                    propertyType.SetValue(entity, true, null);
                }
            }
            if (entity.Id == 0)
            {
                AppContext.Set<T>().Add(entity);
            }

            AppContext.SaveChanges();
            var idControl = code + "_" + entity.DrugDosageId + "_" + entity.Id + "_" + columnName;
            SaveHistoryField(model.Id, idControl, fieldValue, new Guid(userId), fieldDisplay);
            var subUpdateField = new SubUpdateField
            {
                ModelId = model.ObjectId,
                RecordId = entity.Id,
                SubModelId = entity.DrugDosageId,
                ControlId = idControl
            };
            return subUpdateField;
        }

        public void SetExecuter(ExpParameter document, Guid userId, string displayName)
        {
            var declarationsId = document.DeclarationIds.Split(',');
            foreach (var s in declarationsId)
            {
                var model = GetById(s.Replace("'", ""));
                model.ExecuterId = new Guid(document.CustomExecutorsId);
                var executer = AppContext.Employees.FirstOrDefault(e => e.Id == model.ExecuterId);
                if (executer == null) continue;
                var history = new EXP_DrugDeclarationHistory()
                {
                    DateCreate = DateTime.Now,
                    DrugDeclarationId = model.Id,
                    StatusId = model.StatusId,
                    UserId = userId,
                    Note = "Назначен исполнителем: " + executer.DisplayName
                };
                AppContext.EXP_DrugDeclarationHistory.Add(history);
            }
            AppContext.SaveChanges();
        }


        public string GenerateNd(string modelId, string userId, int? typeId)
        {
            var model = GetById(modelId);
            var number = GetNdNumber(model, typeId);
            UpdatePrimaryNTD(model, "nt_0_NumberNd", number, userId);
            return number;
        }


        public string GetNdNumber(EXP_DrugDeclaration model, int? typeId)
        {
            if (model.TypeId > 1)
            {
                foreach (var expDrugDosage in model.EXP_DrugDosage)
                {

                    var reestrDrug = new ExternalRepository().GEtRegisterDrugById(expDrugDosage.RegisterId);
                    if (reestrDrug != null)
                    {
                        return reestrDrug.nd_number;
                    }
                }
            }

            var year = DateTime.Now.Year.ToString().Substring(2, 2);
            var list = AppContext.EXP_DrugPrimaryNTD.Where(e => e.TypeNDId == typeId && !string.IsNullOrEmpty(e.NumberNd));
            var numer = list.Max(e => e.NumberNd.Replace("-", ""));
            if (numer == null)
            {
                return "42-0001-" + year;
            }
            var nn = numer.Substring(0, 2) + "-" + numer.Substring(2, 4) + "-" + numer.Substring(6, 2);
            var numberArray = nn.Split('-');
            if (numberArray.Length != 3)
            {
                return "42-0001-" + year;
            }
            if (year != numberArray[2])
            {
                return "42-0001-" + year;
            }
            var num = numberArray[0] + numberArray[1];
            long numberConvert;
            if (long.TryParse(num, out numberConvert))
            {
                var newNumber = numberConvert + 1;
                var strNumber = newNumber.ToString();
                var str = strNumber.Substring(0, 2) + "-" + strNumber.Substring(2, 4) + "-" + year;
                return str;
            }
            return null;
        }

        public void LoadFromReestr(string id)
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

            var model = GetById(id);
            if (model?.sr_register == null)
            {
                return;
            }
            var drug = new ExternalRepository().GEtRegisterDrugById(model.RegisterId);
            var reestr = model.sr_register;
            model.NameRu = reestr.name;
            model.NameKz = reestr.name_kz;
            model.AtxId = drug.atc_id;
            model.MnnId = drug.int_name_id;
            model.DrugFormId = drug.dosage_form_id;
            model.ConcentrationRu = drug.concentration;
            model.ConcentrationKz = drug.concentration_kz;
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
                //                model.EXP_DrugUseMethod.Clear();
                AppContext.EXP_DrugUseMethod.RemoveRange(model.EXP_DrugUseMethod);
                foreach (var drugSrRegisterUseMethod in drug.sr_register_use_methods)
                {
                    var useMethod = new EXP_DrugUseMethod { UseMethodsId = drugSrRegisterUseMethod.use_method_id };
                    model.EXP_DrugUseMethod.Add(useMethod);
                }
            }
            var repository = new ReadOnlyDictionaryRepository();
            var orgManufactureTypes = repository.GetDictionaries(CodeConstManager.DIC_ORG_MANUFACTURE_TYPE);
            var countyDics = repository.GetDictionaries(CodeConstManager.DIC_COUNTRY_TYPE);

            if (reestr.sr_register_producers.Count > 0)
            {
                //                model.EXP_DrugOrganizations.Clear();
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
                foreach (var expDrugDosage in model.EXP_DrugDosage)
                {
                    //                    expDrugDosage.EXP_DrugSubstance.Clear();
                    AppContext.EXP_DrugSubstance.RemoveRange(expDrugDosage.EXP_DrugSubstance);
                    AppContext.EXP_DrugPrice.RemoveRange(expDrugDosage.EXP_DrugPrice);
                    AppContext.EXP_DrugWrapping.RemoveRange(expDrugDosage.EXP_DrugWrapping);
                    //                    expDrugDosage.EXP_DrugAppDosageRemark.Clear();
                    //                    expDrugDosage.EXP_DrugPrice.Clear();
                    //                    expDrugDosage.EXP_DrugWrapping.Clear();
                    //                    expDrugDosage.EXP_DrugPrimaryFinalDocument.Clear();
                    //                    expDrugDosage.EXP_ExpertiseStageDosage.Clear();
                }
                AppContext.EXP_DrugDosage.RemoveRange(model.EXP_DrugDosage);
                //                model.EXP_DrugDosage.Clear();

                var dosage = new EXP_DrugDosage();
                //                dosage.DrugDeclarationId = model.Id;
                dosage.ConcentrationRu = drug.concentration;
                dosage.ConcentrationKz = drug.concentration_kz;

                if (drug.dosage_value != null) dosage.Dosage = drug.dosage_value.Value;
                dosage.DosageMeasureTypeId = drug.dosage_measure_id;
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
                model.EXP_DrugDosage.Add(dosage);
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

        public Dictionary GetPrimaryFinalDocumentStatus(Guid docId)
        {
            return AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == docId).FinalDocStatus;
        }

        public EXP_ExpertiseStageRemark GetRemarksListFiles(long recordId)
        {
            var model = AppContext.EXP_ExpertiseStageRemark.FirstOrDefault(e => e.Id == recordId);
            if (model == null)
            {
                return null;
            }
            model.FileLinks = AppContext.FileLinks.Where(
                    e =>
                        e.DocumentId == model.EXP_ExpertiseStage.DeclarationId &&
                        e.Comment == model.Id.ToString()).ToList();
            model.RemarkCodeFile = GetRemarkCode();
            return model;
        }

        public void SaveRemarkFile(FileLink fileLink, EXP_ExpertiseStageRemark remark)
        {
            AppContext.FileLinks.Add(fileLink);
            remark.AtthachId = fileLink.Id;
            AppContext.SaveChanges();
        }

        public string GetRemarkCode()
        {
            var model =
                AppContext.Dictionaries.FirstOrDefault(
                    e =>
                        e.Type == CodeConstManager.ATTACH_DRUG_FILE_CODE &&
                        e.Code == CodeConstManager.ATTACH_REMARK_FILE_CODE);

            return model?.Id.ToString();
        }

        public EXP_DrugDeclaration DublicateDrug(string id, Guid guid)
        {
            var oldModel = GetById(id);
            if (oldModel == null)
            {
                return null;
            }
            var model = new EXP_DrugDeclaration
            {
                Id = Guid.NewGuid(),
                OwnerId = guid,
                StatusId = CodeConstManager.STATUS_DRAFT_ID,
                CreatedDate = DateTime.Now,
                AccelerationDate = oldModel.AccelerationDate,
                AccelerationNote = oldModel.AccelerationNote,
                AccelerationNumber = oldModel.AccelerationNumber,
                AccelerationTypeId = oldModel.AccelerationTypeId,
                AppPeriodMix = oldModel.AppPeriodMix,
                AppPeriodMixMeasureDicId = oldModel.AppPeriodMixMeasureDicId,
                AppPeriodOpen = oldModel.AppPeriodOpen,
                AppPeriodOpenMeasureDicId = oldModel.AppPeriodOpenMeasureDicId,
                AtxComment = oldModel.AtxComment,
                AtxId = oldModel.AtxId,
                BestBefore = oldModel.BestBefore,
                BestBeforeMeasureTypeDicId = oldModel.BestBeforeMeasureTypeDicId,
                ConcentrationKz = oldModel.ConcentrationKz,
                ConcentrationRu = oldModel.ConcentrationRu,
                ContractId = oldModel.ContractId,
                Dosage = oldModel.Dosage,
                DosageMeasureTypeId = oldModel.DosageMeasureTypeId,
                DosageNoteKz = oldModel.DosageNoteKz,
                DosageNoteRu = oldModel.DosageNoteRu,
                DrugFormId = oldModel.DrugFormId,
                DrugFormKz = oldModel.DrugFormKz,
                DrugFormRu = oldModel.DrugFormRu,
                ExpeditedType = oldModel.ExpeditedType,
                GmpExpiryDate = oldModel.GmpExpiryDate,
                GrlsNote = oldModel.GrlsNote,
                IsConvention = oldModel.IsConvention,
                IsGmp = oldModel.IsGmp,
                IsGrls = oldModel.IsGrls,
                ManufactureTypeId = oldModel.ManufactureTypeId,
                MnnComment = oldModel.MnnComment,
                MnnId = oldModel.MnnId,
                NameEn = oldModel.NameEn,
                NameRu = oldModel.NameRu,
                NameKz = oldModel.NameKz,
                NumberNd = oldModel.NumberNd,
                OriginalName = oldModel.OriginalName,
                ProposedShelfLife = oldModel.ProposedShelfLife,
                RegisterId = oldModel.RegisterId,
                SaleTypeId = oldModel.SaleTypeId,
                StorageConditions1 = oldModel.StorageConditions1,
                StorageConditions2 = oldModel.StorageConditions2,
                ProposedShelfLifeMeasureId = oldModel.ProposedShelfLifeMeasureId,
                TypeId = oldModel.TypeId,
                Transportation = oldModel.Transportation,

            };
            foreach (var oldModelExpDirectionToPay in oldModel.EXP_DrugChangeType)
            {
                var change = new EXP_DrugChangeType
                {
                    ChangeTypeId = oldModelExpDirectionToPay.ChangeTypeId,
                    Condition = oldModelExpDirectionToPay.Condition,
                    EXP_DrugDeclaration = model
                };
                model.EXP_DrugChangeType.Add(change);
            }
            foreach (var expDrugExportTrade in oldModel.EXP_DrugExportTrade)
            {
                var trade = new EXP_DrugExportTrade()
                {
                    CountryId = expDrugExportTrade.CountryId,
                    NameEn = expDrugExportTrade.NameEn,
                    NameRu = expDrugExportTrade.NameRu,
                    NameKz = expDrugExportTrade.NameKz,
                    EXP_DrugDeclaration = model
                };
                model.EXP_DrugExportTrade.Add(trade);
            }
            foreach (var org in oldModel.EXP_DrugOrganizations)
            {
                var newDosage = new EXP_DrugOrganizations
                {
                    EXP_DrugDeclaration = model,
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
                model.EXP_DrugOrganizations.Add(newDosage);
            }
            foreach (var expDrugOtherCountry in oldModel.EXP_DrugOtherCountry)
            {
                var country = new EXP_DrugOtherCountry()
                {
                    EXP_DrugDeclaration = model,
                    CountryId = expDrugOtherCountry.CountryId,
                    ExpireDate = expDrugOtherCountry.ExpireDate,
                    IssueDate = expDrugOtherCountry.IssueDate,
                    RegNumber = expDrugOtherCountry.RegNumber
                };
                model.EXP_DrugOtherCountry.Add(country);
            }

            foreach (var expDrugPatent in oldModel.EXP_DrugPatent)
            {
                var patent = new EXP_DrugPatent()
                {
                    EXP_DrugDeclaration = model,
                    NameDocument = expDrugPatent.NameDocument,
                    NameOwner = expDrugPatent.NameOwner,
                    PatentDate = expDrugPatent.PatentDate,
                    PatentExpiryDate = expDrugPatent.PatentDate,
                    PatentNumber = expDrugPatent.PatentNumber
                };
                model.EXP_DrugPatent.Add(patent);
            }
            CopyDosage(oldModel, model);
            AppContext.EXP_DrugDeclaration.Add(model);
            AppContext.SaveChanges();
            return model;
        }

        private static void CopyDosage(EXP_DrugDeclaration oldModel, EXP_DrugDeclaration model)
        {
            foreach (var dosage in oldModel.EXP_DrugDosage)
            {
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
                    EXP_DrugDeclaration = model,
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
                model.EXP_DrugDosage.Add(newDosage);
            }
        }
    }
}
