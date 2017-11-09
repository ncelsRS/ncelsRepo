using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Repository.Common;

namespace PW.Ncels.Database.Repository.Expertise
{
    public abstract class ADrugDeclarationRepository : ARepository
    {
        public EXP_DrugDeclaration GetById(string modelId)
        {
            return AppContext.EXP_DrugDeclaration.FirstOrDefault(e => e.Id == new Guid(modelId));
        }
        /// <summary>
        /// Сохранение истории поле
        /// </summary>
        /// <param name="modelId">ид заявление</param>
        /// <param name="fieldName">изменяемое поле</param>
        /// <param name="fieldValue">значение</param>
        /// <param name="userId">автор</param>
        /// <param name="fieldDisplay"></param>
        protected void SaveHistoryField(Guid modelId, string fieldName, string fieldValue, Guid userId,
            string fieldDisplay)
        {
            var history = new EXP_DrugDeclarationFieldHistory
            {
                DrugDeclarationId = modelId,
                ControlId = fieldName,
                UserId = userId,
                ValueField = fieldValue,
                DisplayField = fieldDisplay,
                CreateDate = DateTime.Now
            };
            AppContext.EXP_DrugDeclarationFieldHistory.Add(history);
            AppContext.SaveChanges();
        }
        public CorespondenceEntity CreateMailRemark(string id, Employee employee)
        {

            var model = GetExpertiseStageById(new Guid(id));
            if (model == null)
            {
                return new CorespondenceEntity() { IsSuccess = false, StageType = 0 };
            }
            var coresponce = new CorespondenceEntity { StageType = model.StageId };

            var remarks = AppContext.EXP_ExpertiseStageRemark.Where(e => e.StageId == model.Id && !e.IsReadOnly && e.CorespondenceId == null);
            var modelID = Guid.NewGuid();
            var mail = new EXP_DrugCorespondence
            {
                Id = modelID,
                AuthorId = employee.Id,
                DateCreate = DateTime.Now,
                DrugDeclarationId = model.DeclarationId,
                KindId = CodeConstManager.CORESPONDENCE_KIND_ELECTRONIC,
                TypeId = CodeConstManager.CORESPONDENCE_TYPE_INBOX,
                StageId = model.StageId,
                SubjectId = AppContext.EXP_DIC_CorespondenceSubject.FirstOrDefault(e => e.Code == EXP_DIC_CorespondenceSubject.Remarks && !e.IsDeleted).Id
            };
            var status =
                new ReadOnlyDictionaryRepository().GetDictionaries(CodeConstManager.STATUS_DRAFT).FirstOrDefault();

            if (status != null)
            {
                mail.StatusId = status.Id;
            }
            var builder = new StringBuilder("Список замечании:");
            var index = 1;
            foreach (var expDrugPrimaryRemark in remarks)
            {
                var entity = new EXP_DrugCorespondenceRemark
                {
                    AnswerRemark = expDrugPrimaryRemark.AnswerRemark,
                    ExecuterId = expDrugPrimaryRemark.ExecuterId,
                    FixedDate = expDrugPrimaryRemark.FixedDate,
                    IsAccepted = expDrugPrimaryRemark.IsAccepted,
                    IsFixed = expDrugPrimaryRemark.IsFixed,
                    NameRemark = expDrugPrimaryRemark.NameRemark,
                    Note = expDrugPrimaryRemark.Note,
                    RemarkDate = expDrugPrimaryRemark.RemarkDate,
                    RemarkTypeId = expDrugPrimaryRemark.RemarkTypeId,
                };
                mail.EXP_DrugCorespondenceRemark.Add(entity);
                expDrugPrimaryRemark.IsReadOnly = true;
                expDrugPrimaryRemark.CorespondenceId = modelID.ToString();
                builder.AppendLine(index + ". " + expDrugPrimaryRemark.NameRemark + ";");
                index++;
            }
            var stageType = new ReadOnlyDictionaryRepository().GetDicStageById(model.StageId);
            mail.Subject = "Замечания с экспертизы (" + stageType.NameRu + ")";
            mail.Note = builder.ToString();
            AppContext.EXP_DrugCorespondence.Add(mail);
            AppContext.SaveChanges();
            coresponce.IsSuccess = true;
            return coresponce;
        }

        public void CreateLetter(int stageId, Guid authorId, Guid declarationId, string subjectCode, string text)
        {
            var subject =
                AppContext.EXP_DIC_CorespondenceSubject.FirstOrDefault(e => e.Code == subjectCode && !e.IsDeleted);
            var mail = new EXP_DrugCorespondence
            {
                Id = Guid.NewGuid(),
                AuthorId = authorId,
                DateCreate = DateTime.Now,
                DrugDeclarationId = declarationId,
                KindId = CodeConstManager.CORESPONDENCE_KIND_ELECTRONIC,
                TypeId = CodeConstManager.CORESPONDENCE_TYPE_INBOX,
                StageId = stageId,
                SubjectId = subject.Id,
                Subject = subject.NameRu,
                Note = text
            };
            var status =
                new ReadOnlyDictionaryRepository().GetDictionaries(CodeConstManager.STATUS_DRAFT).FirstOrDefault();

            if (status != null)
            {
                mail.StatusId = status.Id;
            }
            AppContext.EXP_DrugCorespondence.Add(mail);
            AppContext.SaveChanges();
        }

        public List<EXP_DrugCorespondence> GetDrugCorespondences(Guid? modelId, bool sended = false)
        {
            var query = AppContext.EXP_DrugCorespondence.Where(e => e.DrugDeclarationId == modelId && !e.IsDeleted);
            if (sended)
            {
                var signedStatusId =
                    DictionaryHelper.GetDicIdByCode(CodeConstManager.STATUS_SEND, CodeConstManager.STATUS_SEND);
                query = query.Where(e => e.StatusId == signedStatusId);
            }
            return query.ToList();
        }

        public List<EXP_ExpertiseStageRemark> GetPrimaryMarkList(Guid? modelId, int? stageId)
        {
            if (stageId == null)
            {
                return AppContext.EXP_ExpertiseStageRemark.Where(e => e.EXP_ExpertiseStage.DeclarationId == modelId).ToList();
            }
            return AppContext.EXP_ExpertiseStageRemark.Where(e => e.EXP_ExpertiseStage.DeclarationId == modelId && e.EXP_ExpertiseStage.StageId == stageId).ToList();
        }

        public EXP_ExpertiseStage GetExpertiseStageById(Guid? id)
        {
            return AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == id);
        }

        public EXP_ExpertiseStageDosage GetExpertiseStageDosageById(Guid? id)
        {
            return AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == id);
        }

        protected static void GetOtdChildren(ICollection<string> otdIds, EXP_DIC_PrimaryOTD dic, bool isChecked)
        {
            var otdId = dic.Id.ToString();

            if (isChecked)
            {
                if (!otdIds.Contains(otdId))
                {
                    otdIds.Add(otdId);
                }
            }
            else
            {
                if (otdIds.Contains(otdId))
                {
                    otdIds.Remove(otdId);
                }
            }

            if (dic.EXP_DIC_PrimaryOTD1.Count == 0) return;

            foreach (var expDicPrimaryOtd in dic.EXP_DIC_PrimaryOTD1)
            {
                GetOtdChildren(otdIds, expDicPrimaryOtd, isChecked);
            }
        }

        protected static void GetOtdParent(ICollection<string> otdIds, EXP_DIC_PrimaryOTD dic, bool isChecked)
        {
            if (isChecked)
            {
                return;
            }
            if (dic.EXP_DIC_PrimaryOTD2 == null) return;

            var otdId = dic.EXP_DIC_PrimaryOTD2.Id.ToString();
            if (otdIds.Contains(otdId))
            {
                otdIds.Remove(otdId);
            }
            GetOtdParent(otdIds, dic.EXP_DIC_PrimaryOTD2, false);
        }

    }
}
