using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using Aspose.Words;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.Common;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class DrugPrimaryRepository : ADrugDeclarationRepository
    {
        /*   public EXP_DrugPrimaryRemark GetPrimaryMarkById(long id)
           {
               return AppContext.EXP_DrugPrimaryRemark.FirstOrDefault(e => e.Id == id);
           }
           public EXP_DrugPrimaryRemark GetPrimaryMarkByDeclarationId(Guid? modelId)
           {
               return AppContext.EXP_DrugPrimaryRemark.FirstOrDefault(e => e.DrugDeclarationId == modelId);
           }*/

        public EXP_DrugPrimaryNTD GetPrimaryNtdById(long id)
        {
            return AppContext.EXP_DrugPrimaryNTD.FirstOrDefault(e => e.Id == id);
        }
        public EXP_DrugPrimaryNTD GetPrimaryNtdByDeclarationId(Guid? modelId)
        {
            return AppContext.EXP_DrugPrimaryNTD.FirstOrDefault(e => e.DrugDeclarationId == modelId);
        }

        public EXP_DrugPrimaryKind GetPrimaryKindByDeclarationId(Guid? modelId)
        {
            return AppContext.EXP_DrugPrimaryKind.FirstOrDefault(e => e.DrugDeclarationId == modelId);
        }
        public List<EXP_DrugPrimaryKind> GetPrimaryKindList(Guid? modelId)
        {
            return AppContext.EXP_DrugPrimaryKind.Where(e => e.DrugDeclarationId == modelId).ToList();
        }


        public bool CreateRemark(string id, Employee employee, int dicStageId)
        {
            var model = GetExpertiseStageById(new Guid(id));
            if (model == null)
            {
                return true;
            }

            var otdIds = (model.OtdIds ?? "").Split(',').ToList();
            var otds = AppContext.EXP_ExpertiseStageRemark.Where(e => e.OtdId > 0 && e.EXP_ExpertiseStage.StageId == dicStageId && e.EXP_ExpertiseStage.Id == model.Id).Select(e => e.OtdId);
            var dicRepo = new ReadOnlyDictionaryRepository();
            var dicOtdQuery = model.EXP_DrugDeclaration.EXP_DIC_Type.Code == EXP_DIC_Type.ReRegistration
                ? dicRepo.GetExpDicPrimaryOTDs(null, new[] { EXP_DIC_PrimaryOTD.Module4, EXP_DIC_PrimaryOTD.Module5 })
                : dicRepo.GetExpDicPrimaryOTDs();
            var dicotds = dicOtdQuery.Where(e => !otdIds.Contains(e.Id.ToString()) && !otds.Contains(e.Id) && e.EXP_DIC_PrimaryOTD1.Count == 0);
            var expDicPrimaryOtds = dicotds as EXP_DIC_PrimaryOTD[] ?? dicotds.ToArray();

            var deleteList = AppContext.EXP_ExpertiseStageRemark.Where(e => e.OtdId > 0 && e.EXP_ExpertiseStage.StageId == dicStageId && e.EXP_ExpertiseStage.Id == model.Id && otdIds.Contains(e.OtdId.ToString()));
            foreach (var expDrugPrimaryRemark in deleteList)
            {
                AppContext.EXP_ExpertiseStageRemark.Remove(expDrugPrimaryRemark);
            }
            AppContext.SaveChanges();
            if (!expDicPrimaryOtds.Any())
            {
                return true;
            }
            var otdRemarks = (model.OtdRemarks ?? "").Split(new[] { "+++" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e =>
                {
                    var items = e.Split(new[] { "-->" }, StringSplitOptions.RemoveEmptyEntries);
                    return new { id = int.Parse(items[0]), remark = items[1] };
                }).ToList();
            foreach (var dicotd in expDicPrimaryOtds)
            {
                if (dicotd.EXP_DIC_PrimaryOTD1.Count != 0) continue;
                var remark = otdRemarks.FirstOrDefault(e => e.id == dicotd.Id);
                var entity = new EXP_ExpertiseStageRemark
                {
                    OtdId = dicotd.Id,
                    NameRemark = remark != null ? string.Format("{0} - {1}", dicotd.FullName, remark.remark) : dicotd.FullName,
                    ExecuterId = employee.Id,
                    StageId = model.Id,
                    RemarkDate = DateTime.Now
                };
                AppContext.EXP_ExpertiseStageRemark.Add(entity);
            }


            AppContext.SaveChanges();
            return true;
        }


        public EXP_ExpertisePrimaryFinalDoc UpdateFinalDocument(string fieldName, string fieldValue, Guid objectId, Guid userId)
        {
            var appDosage = AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == objectId);
            if (appDosage == null)
            {
                return null;
            }
            var model = appDosage.PrimaryFinalDocs.FirstOrDefault() ??
                        new EXP_ExpertisePrimaryFinalDoc { DosageStageId = appDosage.Id };
            var property = model.GetType().GetProperty(fieldName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                var safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);
                property.SetValue(model, safeValue, null);
            }
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                AppContext.EXP_ExpertisePrimaryFinalDoc.Add(model);
            }
            var addLogInfo = "";
            addLogInfo += "field: '" + fieldName + "' value: " + fieldValue;
            ActionLogger.WriteInt(AppContext, "Заявка №" + appDosage.EXP_DrugDosage.RegNumber + " изменения результатов: ПЭ", addLogInfo);
            AppContext.SaveChanges();
            return model;
        }

        public void CloneDosageFinalDoc(Guid dosageId)
        {
            var dosage = AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == dosageId);
            if (dosage == null)
            {
                return;
            }
            var currentFinal = dosage.PrimaryFinalDocs.FirstOrDefault();
            if (currentFinal == null) return;
            var list = AppContext.EXP_ExpertiseStageDosage.Where(e => e.StageId == dosage.StageId && e.Id != dosageId);
            foreach (var expDrugDosage in list)
            {
                var expFinal = expDrugDosage.PrimaryFinalDocs.FirstOrDefault() ??
                               new EXP_ExpertisePrimaryFinalDoc(currentFinal);
                expFinal.DosageStageId = expDrugDosage.Id;
                if (expFinal.Id == Guid.Empty)
                {
                    expFinal.Id = Guid.NewGuid();
                    AppContext.EXP_ExpertisePrimaryFinalDoc.Add(expFinal);
                }
                else
                {
                    expFinal.ComplianceSeries = currentFinal.ComplianceSeries;
                    expFinal.ExpertOpinion = currentFinal.ExpertOpinion;
                    expFinal.ExpertiseNormDoc = currentFinal.ExpertiseNormDoc;
                    expFinal.IsAbilityMislead = currentFinal.IsAbilityMislead;
                    expFinal.IsAdvertising = currentFinal.IsAdvertising;
                    expFinal.IsColorModel = currentFinal.IsColorModel;
                    expFinal.IsDossierSection = currentFinal.IsDossierSection;
                    expFinal.IsForbiddenDyes = currentFinal.IsForbiddenDyes;
                    expFinal.IsFromBlood = currentFinal.IsFromBlood;
                    expFinal.IsMNNSimilar = currentFinal.IsMNNSimilar;
                    expFinal.IsNarcoticDrug = currentFinal.IsNarcoticDrug;
                    expFinal.IsPhoneticSimilar = currentFinal.IsPhoneticSimilar;
                    expFinal.IsRKProduct = currentFinal.IsRKProduct;
                    expFinal.IsSetDocument = currentFinal.IsSetDocument;
                    expFinal.MedicalInstruction = currentFinal.MedicalInstruction;
                    expFinal.ResidualShelfLife = currentFinal.ResidualShelfLife;
                    expFinal.SampleDrug = currentFinal.SampleDrug;
                    expFinal.SampleStandart = currentFinal.SampleStandart;
                    expFinal.SampleSubstance = currentFinal.SampleSubstance;
                    expFinal.TestLabRecommend = currentFinal.TestLabRecommend;
                }
            }
            AppContext.SaveChanges();
        }


        public EXP_DrugCorespondence GetCorespondence(string id)
        {
            return AppContext.EXP_DrugCorespondence.FirstOrDefault(e => e.Id == new Guid(id));
        }

        public void SaveCorespondence(string id, string numberLetter, string subject, string note, DateTime dateSend, Employee getCurrentEmployee)
        {
            var model = AppContext.EXP_DrugCorespondence.FirstOrDefault(e => e.Id == new Guid(id));
            if (model == null)
            {
                return;
            }
            model.NumberLetter = numberLetter;
            model.Subject = subject;
            model.Note = note;
            model.DateSend = dateSend;
            AppContext.SaveChanges();
        }

        public void DeleteCorespondence(string id, Employee getCurrentEmployee)
        {
            var model = AppContext.EXP_DrugCorespondence.FirstOrDefault(e => e.Id == new Guid(id));
            if (model == null)
            {
                return;
            }
            model.IsDeleted = true;
            var list = AppContext.EXP_DrugPrimaryRemark.Where(e => e.CorespondenceId == id);
            foreach (var expDrugPrimaryRemark in list)
            {
                expDrugPrimaryRemark.IsReadOnly = false;
                expDrugPrimaryRemark.CorespondenceId = null;
            }
            AppContext.SaveChanges();
        }

        public EXP_DrugCorespondence SendMailRemark(string id, Employee getCurrentEmployee)
        {
            try
            {
                var model = AppContext.EXP_DrugCorespondence.FirstOrDefault(e => e.Id == new Guid(id));
                if (model == null)
                {
                    return null;
                }
                if (!model.DateSend.HasValue) {
                    model.DateSend = DateTime.Now;
                }
                
                model.IsReadOnly = true;
                var status =
                 new ReadOnlyDictionaryRepository().GetDictionaries(CodeConstManager.STATUS_SEND).FirstOrDefault();

                if (status != null)
                {
                    model.StatusId = status.Id;
                }
                AppContext.SaveChanges();

                var expertiseStage = AppContext.EXP_ExpertiseStage.FirstOrDefault(x =>
                    !x.IsHistory
                    && x.StageId == model.StageId
                    && x.DeclarationId == model.DrugDeclarationId);
                if (expertiseStage != null)
                {
                    expertiseStage.IsSuspended = true;
                    expertiseStage.SuspendedStartDate = DateTime.Now;
                    AppContext.SaveChanges();
                }
                else
                {
                    LogHelper.Log.ErrorFormat("Не удалось найти expertiseStage по EXP_DrugCorespondence с id {0}", model.Id);
                }

                model.Dictionary = status;

                var declaraion = AppContext.EXP_DrugDeclaration.FirstOrDefault(e => e.Id == model.DrugDeclarationId);
                if (declaraion != null)
                {
                    //                declaraion.DesignNote = model.Note;
                    declaraion.DesignDate = DateTime.Now;
                    if (model.EXP_DIC_CorespondenceSubject.Code == EXP_DIC_CorespondenceSubject.Remarks)
                    {
                        declaraion.StatusId = CodeConstManager.STATUS_EXP_REJECT_ID;
                        new DrugDeclarationRepository().Update(declaraion);
                    }
                    var history = new EXP_DrugDeclarationHistory()
                    {
                        DateCreate = DateTime.Now,
                        DrugDeclarationId = declaraion.Id,
                        StatusId = declaraion.StatusId,
                        UserId = getCurrentEmployee.Id,
                        Note = model.Note
                    };
                    new DrugDeclarationRepository().SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
                }
                return model;
            }
            catch (DbEntityValidationException dbEx)
            {
                LogHelper.Log.Error("DbEntityValidationException", dbEx);
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        LogHelper.Log.ErrorFormat("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("SendMailRemark", ex);
                throw;
            }
        }

        public bool UpdateOtd(Guid stageId, int noteId, bool isChecked, Guid id)
        {
            var repository = new ReadOnlyDictionaryRepository().GetExpDicPrimaryOTDs();
            var model = GetExpertiseStageById(stageId);
            if (model == null)
            {
                return false;
            }
            var dic = repository.FirstOrDefault(e => e.Id == noteId);
            if (model.OtdIds == null)
            {
                model.OtdIds = "";
            }
            var otdIds = model.OtdIds.Split(',').ToList();
            var prevOtdIds = new List<string>(otdIds);

            GetOtdChildren(otdIds, dic, isChecked);
            GetOtdParent(otdIds, dic, isChecked);
            var uncheckedOtIds = prevOtdIds.Except(otdIds).ToList();
            var checkedOtIds = otdIds.Except(prevOtdIds).ToList();
            var addLogInfo = "";
            if (uncheckedOtIds.Count > 0)
            {
                var uncheckedOtIdsStr = String.Join(",", uncheckedOtIds);
                addLogInfo += "снята галочка с пунктов: " + uncheckedOtIdsStr;
            }
            if (checkedOtIds.Count > 0)
            {
                var checkedOtIdsStr = String.Join(",", checkedOtIds);
                addLogInfo += "поставлена галочка в пунктах: " + checkedOtIdsStr;
            }
            var stageName = ExpStageNameHelper.GetName(model.StageId);
            ActionLogger.WriteInt(AppContext, stageName + ": Заявление №" + model.EXP_DrugDeclaration.Number + ":  изменения значений экспертизы(галочки)", addLogInfo);

            model.OtdIds = string.Join(",", otdIds);
            AppContext.SaveChanges();

            return true;
        }

        public void UpdateOtdRemark(Guid stageId, int noteId, string remark)
        {
            var stage = AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == stageId);
            if (stage.OtdRemarks == null)
            {
                stage.OtdRemarks = "";
            }
            var otdRemarks = stage.OtdRemarks.Split(new[] { "+++" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e =>
                {
                    var items = e.Split(new[] { "-->" }, StringSplitOptions.RemoveEmptyEntries);
                    return new { id = int.Parse(items[0]), remark = items[1] };
                }).ToList();
            var otdRemark = otdRemarks.FirstOrDefault(e => e.id == noteId);
            if (otdRemark != null)
                otdRemarks.Remove(otdRemark);
            otdRemarks.Add(new { id = noteId, remark = remark ?? "" });
            stage.OtdRemarks = string.Join("+++", otdRemarks.Select(e => string.Format("{0}-->{1}", e.id, e.remark)));
            AppContext.SaveChanges();
        }

        public void CheckSuspensionPeriods() {
            try {
                LogHelper.Log.Debug("CheckSuspensionPeriods start");
                var date = DateTime.Now.AddDays((-1 * suspensionPeriod));
                var list = AppContext.EXP_ExpertiseStage.Where(x => 
                    !x.IsHistory
                    && x.IsSuspended
                    && x.SuspendedStartDate < date).ToList();
                if (list.Count > 0) {
                    var notificationManager = new NotificationManager();
                    foreach (var expertiseStage in list) {
                        var messageText = string.Format("По заявлению № {0} истек срок исправления замечаний",
                            expertiseStage.EXP_DrugDeclaration.Number);
                        LogHelper.Log.Debug(messageText);
                        var executors = expertiseStage.Executors.ToList();
                        foreach (var executor in executors) {
                            notificationManager.SendNotificationAnonymous(messageText, ObjectType.Letter, expertiseStage.EXP_DrugDeclaration.Id.ToString(), executor.Id);
                        }
                        expertiseStage.IsSuspended = false;
                        var suspendedDays = (int)(DateTime.Now - expertiseStage.SuspendedStartDate.Value).TotalDays;
                        LogHelper.Log.DebugFormat("Всего дней приостановки {0}", suspendedDays);
                        expertiseStage.EndDate = expertiseStage.EndDate.Value.AddDays(suspendedDays);
                        AppContext.SaveChanges();
                        LogHelper.Log.DebugFormat("По этапу {0} приостановка снята", expertiseStage.Id);
                    }
                }
                LogHelper.Log.Debug("CheckSuspensionPeriods end");
            }
            catch (Exception ex) {
                LogHelper.Log.Error("Ошибка проверки срока приостановки", ex);
            }

        }

        private int suspensionPeriod{
            get{
                //MaxSuspensionPeriod
                var maxDays = WebConfigurationManager.AppSettings["MaxSuspensionPeriod"];
                var result = 30;
                int val;
                if (!string.IsNullOrEmpty(maxDays)) {
                    if (int.TryParse(maxDays, out val)) {
                        result = val;
                    }
                }
                return result;
            }
        }

        public MemoryStream GetCorespondenceFilePreview(Guid id, string reportPath, out string fileType, out string fileName)
        {
            StiReport report = new StiReport();
            report.Load(reportPath);

            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }

            if (report.Dictionary.Variables.Contains("CorespondenceId"))
            {
                report.Dictionary.Variables["CorespondenceId"].ValueObject = id;
            }

            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Word2007, stream);
            stream.Position = 0;
            var signingActivity = AppContext.EXP_Activities.Where(e => e.DocumentId == id &&
                                                               e.ExpActivityStatus.Code == Dictionary.ExpActivityStatus
                                                                   .Executed
                                                               && e.ExpActivityType.Code == Dictionary.ExpActivityType
                                                                   .ExpertiseLetterSigning && e.ExpAgreedDocType.Code ==
                                                               Dictionary.ExpAgreedDocType.Letter)
                .OrderByDescending(e => e.CreatedDate).FirstOrDefault();
            if (signingActivity != null)
            {
                var task = signingActivity.EXP_Tasks.Where(e => e.DigSign != null)
                    .OrderByDescending(e => e.ExecutedDate).FirstOrDefault();
                Aspose.Words.Document docWord = new Aspose.Words.Document(stream);
                docWord.InserQrCodesToEnd("ExecutorSign", task.DigSign);
                var pdfFile = new MemoryStream();
                docWord.Save(pdfFile, SaveFormat.Pdf);
                pdfFile.Position = 0;
                stream.Close();
                fileType = "application/pdf";
                fileName = "Замечания.pdf";
                return pdfFile;
            }
            fileType = "application/word";
            fileName = "Замечания.doc";
            return stream;
        }

    }
}

