using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO;
using System.Linq;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Entities;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Common;
using PW.Ncels.Database.Repository.Common;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class SafetyreportRepository : ADrugDeclarationRepository
    {
        private const string FILE_CODE = "FILE";
        private const string FINALDOC_CODE = "FINALDOC";
        private const string ACCEPTED_STATUS = "Принят";
        private const string FOR_REVISION_STATUS = "На доработку";
        private const string KZ_CODE = "kz";
        private const string RU_CODE = "ru";
        public void CloneDosageFinalDoc(Guid dosageId)
        {
            var dosage = AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == dosageId);
            if (dosage == null)
            {
                return;
            }
            var currentFinal = dosage.EXP_ExpertiseSafetyreportFinalDoc.FirstOrDefault();
            if (currentFinal == null) return;

            var list = AppContext.EXP_ExpertiseStageDosage.Where(e => e.StageId == dosage.StageId && e.Id != dosageId);
            foreach (var expDrugDosage in list)
            {
                var expFinal = expDrugDosage.EXP_ExpertiseSafetyreportFinalDoc.FirstOrDefault() ??
                               new EXP_ExpertiseSafetyreportFinalDoc(currentFinal);
                expFinal.DosageStageId = expDrugDosage.Id;
                if (expFinal.Id == Guid.Empty)
                {
                    expFinal.Id = Guid.NewGuid();
                    AppContext.EXP_ExpertiseSafetyreportFinalDoc.Add(expFinal);
                }
            }
            AppContext.SaveChanges();
        }

        public EXP_ExpertiseSafetyreportFinalDoc UpdateFinalDocument(string fieldName, string fieldValue, Guid objectId, Guid userId, bool isFromTranslateController = false)
        {
            var appDosage = AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == objectId);
            if (appDosage == null)
            {
                return null;
            }
            var model = appDosage.EXP_ExpertiseSafetyreportFinalDoc.FirstOrDefault() ??
                        new EXP_ExpertiseSafetyreportFinalDoc { DosageStageId = appDosage.Id };
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
                AppContext.EXP_ExpertiseSafetyreportFinalDoc.Add(model);
            }
            var addLogInfo = "";
            addLogInfo += "field: '" + fieldName + "' value: " + fieldValue;
            if (isFromTranslateController)//ну сорян, почемуто у перевода своего репозитория нету
            {
                ActionLogger.WriteInt(AppContext, "Заявка №" + appDosage.EXP_DrugDosage.RegNumber + " изменения результатов: Перевод", addLogInfo);
            }
            else
            {
                ActionLogger.WriteInt(AppContext, "Заявка №" + appDosage.EXP_DrugDosage.RegNumber + " изменения результатов: ЗОБ", addLogInfo);
            }
            AppContext.SaveChanges();
            return model;
        }

        public void CreateExpertiseSafetyreportFinalDoc(EXP_ExpertiseSafetyreportFinalDoc model)
        {
            AppContext.EXP_ExpertiseSafetyreportFinalDoc.Add(model);
            AppContext.SaveChanges();
        }

        public void UpdateExpertiseSafetyreportFinalDoc(EXP_ExpertiseSafetyreportFinalDoc model)
        {
            AppContext.EXP_ExpertiseSafetyreportFinalDoc.AddOrUpdate(model);
            AppContext.SaveChanges();
        }

        public List<ConclusionSafetyReport> ConclusionSafetyReportsFromFiles(Guid modelId, Employee getCurrentEmployee)
        {
           
            var list = new List<ConclusionSafetyReport>();
            var files =
                AppContext.FileLinks.Where(
                    e => !e.IsDeleted && e.DocumentId == modelId && e.OwnerId != getCurrentEmployee.Id );
            var dicListQuery = AppContext.Dictionaries.Where(o => o.Type == CodeConstManager.ATTACH_DRUG_FILE_CODE);
            list.Add(CreateConclusionSafetyReport(modelId.ToString(), files, dicListQuery, CodeConstManager.FILE_INSTRUCTION_CODE));
            list.Add(CreateConclusionSafetyReport(modelId.ToString(), files, dicListQuery, CodeConstManager.FILE_INSTRUCTION_CODE, true));
            list.Add(CreateConclusionSafetyReport(modelId.ToString(), files, dicListQuery, CodeConstManager.FILE_MAKET_CODE));
            list.Add(CreateConclusionSafetyReport(modelId.ToString(), files, dicListQuery, CodeConstManager.FILE_AND_CODE));
            return list;
        }

        private ConclusionSafetyReport CreateConclusionSafetyReport(string doc, IQueryable<FileLink> files, IQueryable<Dictionary> dicListQuery, string code, bool isKz =false)
        {
            var report = new ConclusionSafetyReport();
            switch (code)
            {
                case CodeConstManager.FILE_INSTRUCTION_CODE:
                {
                    report.Title = isKz ? "Инструкция на казахском языке" : "Инструкция на русском языке";
                    break;
                }
                case CodeConstManager.FILE_MAKET_CODE:
                    {
                        report.Title = "Макет";
                        break;
                    }
                case CodeConstManager.FILE_AND_CODE:
                    {
                        report.Title = "АНД";
                        break;
                    }
            }
            var dictionary = dicListQuery.FirstOrDefault(e => e.Code == code);
            if (dictionary == null)
            {
                return null;
            }
            var info = new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], UploadRepository.Root, doc ?? ""));
            if (!info.Exists)
                info.Create();
            var fileMetadatas = files.Where(e => e.CategoryId == dictionary.Id);
            if (code == CodeConstManager.FILE_INSTRUCTION_CODE)
            {
                if (isKz)
                {
                    fileMetadatas = fileMetadatas.Where(e => e.Language == KZ_CODE);
                }
                else
                {
                    fileMetadatas = fileMetadatas.Where(e => e.Language != KZ_CODE);
                }
            }

            var fileGroupItems =
                (new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], UploadRepository.Root,
                    doc ?? "", dictionary.Id.ToString()))).Exists
                    ? new DirectoryInfo(Path.Combine(ConfigurationManager.AppSettings["AttachPath"], UploadRepository.Root,
                            doc ?? "", dictionary.Id.ToString())).GetFiles()
                        .Join(fileMetadatas, f => f.Name,
                            f => string.Format("{0}{1}", f.Id, Path.GetExtension(f.FileName)),
                            (f, fm) => new { File = f, FileMetadata = fm })
                        .ToList().Select(k => new FileGroupItem()
                        {
                            AttachId =
                            string.Format("id={0}&path={1}&fileId={2}", dictionary.Id, doc,
                                string.Format("{0}{1}", k.FileMetadata.Id,
                                    Path.GetExtension(k.FileMetadata.FileName))),
                            AttachName = k.FileMetadata.FileName,
                            AttachSize = k.File.Length,
                            Version = k.FileMetadata.Version,
                            OriginFileId = k.FileMetadata.ParentId,
                            OwnerName = k.FileMetadata.OwnerName,
                            OwnerId = (Guid)k.FileMetadata.OwnerId,
                            CreateDate = k.FileMetadata.CreateDate.ToString(CultureInfo.InvariantCulture),
                            MetadataId = k.FileMetadata.Id,
                            Language = k.FileMetadata.Language,
                            Comment = k.FileMetadata.Comment,
                            DicFileLinkStatus = k.FileMetadata.DIC_FileLinkStatus,
                            Stage = k.FileMetadata.EXP_DIC_Stage != null ? k.FileMetadata.EXP_DIC_Stage.NameRu : ""
                        }).ToList()
                    : new List<FileGroupItem>();
           var fileGroup = fileGroupItems.OrderByDescending(e => e.Version).FirstOrDefault();
            if (fileGroup != null)
            {
                report.Id = fileGroup.MetadataId.ToString();
                report.Category = FILE_CODE;
                report.FileName = fileGroup.AttachName;
                if (fileGroup.StatusCode == CodeConstManager.STATUS_FILE_CODE_ACCEPTED)
                {
                    report.IsAccepted = true;
                }
                if (fileGroup.StatusCode == CodeConstManager.STATUS_FILE_CODE_FOR_REVISION)
                {
                    report.IsAccepted = false;
                }
                report.StatusName = fileGroup.StatusName;
                report.Remark = fileGroup.Comment;
                report.Language = fileGroup.Language;
                report.Url = "/Upload/FileDownload?" + fileGroup.AttachId;
            }
           
            return report;
        }

        public ConclusionSafetyReport ConclusionSafetyReportsFromDosage(long recordId, bool isKz)
        {
            var model =
                AppContext.EXP_ExpertiseSafetyreportFinalDoc.FirstOrDefault(e => e.EXP_ExpertiseStageDosage.DosageId == recordId);
            var report = new ConclusionSafetyReport();
            if (model == null)
            {
                return report;
            }
            var title = isKz ? "Заключения на казахском языке" : "Заключения на русском языке";
            var code = isKz ? KZ_CODE : RU_CODE;
            if (isKz)
            {
                report.IsAccepted = model.IsAcceptedKz;
                report.Remark = model.RemarkKz;
                if (model.IsAcceptedKz != null)
                {
                    report.StatusName = model.IsAcceptedKz.Value ? ACCEPTED_STATUS : FOR_REVISION_STATUS;
                }
            }
            else
            {
                report.IsAccepted = model.IsAccepted;
                report.Remark = model.Remark;
                if (model.IsAccepted != null)
                {
                    report.StatusName = model.IsAccepted.Value ? ACCEPTED_STATUS : FOR_REVISION_STATUS;
                }

            }
            report.Title = title;
            report.Id = model.Id.ToString();
            report.Language = code;
            report.Category = FINALDOC_CODE;
            report.FileName = "Заявка: " + model.EXP_ExpertiseStageDosage.EXP_DrugDosage.RegNumber+" ("+ title+")";
            report.Url = "/Upload/ExportSafetyReportFile?id=" + model.EXP_ExpertiseStageDosage.Id+ "&isKz="+ isKz;
            return report;
        }

        public string ConfirmConclusion(string modelId, string category, bool isAccept, string comment, string culture)
        {
            if (category == FILE_CODE)
            {
                if (isAccept)
                {
                    new UploadRepository().AcceptFileConfirm(new Guid(modelId));
                    return ACCEPTED_STATUS;
                }
                new UploadRepository().RejectFileConfirm(new Guid(modelId), comment);
                return FOR_REVISION_STATUS;
            }
            var dosage =
               AppContext.EXP_ExpertiseSafetyreportFinalDoc.FirstOrDefault(e => e.Id == new Guid(modelId));
            if (dosage == null)
            {
                return null;
            }
            if (culture == KZ_CODE)
            {
                dosage.IsAcceptedKz = isAccept;
                dosage.RemarkKz = isAccept ? "" : comment;
            }else
            {
                dosage.IsAccepted = isAccept;
                dosage.Remark = isAccept ? "" : comment;
            }
            AppContext.SaveChanges();
            return isAccept ? ACCEPTED_STATUS : FOR_REVISION_STATUS;
        }
    }
}