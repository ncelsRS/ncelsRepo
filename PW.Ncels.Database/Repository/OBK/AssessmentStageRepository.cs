using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Repository.OBK
{
    public class AssessmentStageRepository : ARepository
    {
        public AssessmentStageRepository() { }

        public AssessmentStageRepository(ncelsEntities context) : base(context) { }

        public OBK_AssessmentStage GetById(Guid? id)
        {
            return AppContext.OBK_AssessmentStage.FirstOrDefault(e => e.Id == id);
        }

        public OBK_AssessmentStage GetByDeclarationId(string declarationId, int stage)
        {
            return AppContext.OBK_AssessmentStage.FirstOrDefault(
                e => e.DeclarationId == new Guid(declarationId) && e.StageId == stage);
        }

        public IEnumerable<OBK_Ref_Nomenclature> GetRefNomenclature()
        {
            return AppContext.OBK_Ref_Nomenclature.Where(e=>!e.IsDeleted).ToList();
        }

        /// <summary>
        /// Проверка есть ли у заявления указанный этап
        /// </summary>
        /// <param name="declarationId">id заявления</param>
        /// <param name="stageCode">Код этапа</param>
        /// <returns></returns>
        public bool HasStage(Guid declarationId, int stageCode)
        {
            return AppContext.OBK_AssessmentStage.Any(e => e.DeclarationId == declarationId &&
                                                          e.StageId == stageCode);
        }

        /// <summary>
        /// Перевод заявления на следующий этап
        /// </summary>
        /// <param name="declaration"></param>
        /// <param name="stageCode"></param>
        public bool ToNextStage(Guid declarationId, Guid? fromStageId, int[] nextStageIds, out string resultDescription)
        {
            resultDescription = null;
            string[] activeStageCodes =
            {
                OBK_Ref_StageStatus.New, OBK_Ref_StageStatus.InWork,
                OBK_Ref_StageStatus.InReWork
            };
            //var declaration = AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == declarationId);
            //if (declaration.EXP_DIC_Type.Code != EXP_DIC_Type.Registration)
            //{
            //  //return ToNextStage(declaration, fromStageId, nextStageIds, out resultDescription);
            //}
            var currentStage = fromStageId != null
                ? AppContext.OBK_AssessmentStage.FirstOrDefault(e => e.Id == fromStageId)
                : AppContext.OBK_AssessmentStage.FirstOrDefault(
                    e => e.DeclarationId == declarationId && activeStageCodes.Contains(e.OBK_Ref_StageStatus.Code));
            var stageStatusNew = GetStageStatusByCode(OBK_Ref_StageStatus.New);
            //закрываем предыдущий этап
            if (currentStage != null) //&& CanCloseStage(currentStage, nextStageIds)
            {
                currentStage.StageStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.Completed).Id;
                currentStage.FactEndDate = DateTime.Now;
            }
            var isAnalitic = false;
            foreach (var nextStageId in nextStageIds)
            {
                //if (!CanSendToStep(declarationId, fromStageId, nextStageId, out resultDescription)) return false;
                //если имеется уже выполняющийся этап то продолжаем его дальше
                if (AppContext.OBK_AssessmentStage.Any(e => e.DeclarationId == declarationId
                                                           && e.StageId == nextStageId &&
                                                           e.OBK_Ref_StageStatus.Code != OBK_Ref_StageStatus.Completed &&
                                                           !e.IsHistory)) continue;
                //todo переделать дату окончания этапа
                var daysOnStage = 0;//GetExpStageDaysOnExecution(declaration.TypeId, nextStageId);
                var startDate = DateTime.Now;
                var newStage = new OBK_AssessmentStage()
                {
                    Id = Guid.NewGuid(),
                    DeclarationId = declarationId,
                    StageId = nextStageId,
                    ParentStageId = currentStage != null ? (Guid?)currentStage.Id : null,
                    StageStatusId = stageStatusNew.Id,
                    StartDate = startDate,
                    EndDate = daysOnStage != null ? (DateTime?)startDate.AddDays(daysOnStage) : null
                };
                //todo брать руководителя цоз из настроек

                var newStageExecutor = new OBK_AssessmentStageExecutors
                {
                    AssessmentStageId = newStage.Id,
                    ExecutorId = GetExecutorByDicStageId(nextStageId).Id,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                };

                newStage.OBK_AssessmentStageExecutors.Add(newStageExecutor);
                AppContext.OBK_AssessmentStage.Add(newStage);
                //if (nextStageId == CodeConstManager.STAGE_ANALITIC)
                //{
                //    isAnalitic = true;
                //}
            }
            AppContext.SaveChanges();
            //if (isAnalitic)
            //{
            //    CreateDirectionMaterial(declarationId, GetExecutorByDicStageId(CodeConstManager.STAGE_ANALITIC));
            //}
            return true;
        }
        /// <summary>
        /// сохранение отказа
        /// </summary>
        /// <param name="stageId"></param>
        /// <param name="withSave"></param>
        public void StartRefuseInSafety(Guid stageId, bool withSave)
        {
            var stage = AppContext.OBK_AssessmentStage.FirstOrDefault(e => e.Id == stageId);
            stage.OBK_AssessmentDeclaration.StatusId = CodeConstManager.STATUS_OBK_COZ_REFUSED_ID;
            stage.FactEndDate = DateTime.Now;
            stage.StageStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.Completed).Id;

            var history = new OBK_AssessmentDeclarationHistory
            {
                DateCreate = DateTime.Now,
                AssessmentDeclarationId = stage.DeclarationId,
                StatusId = stage.OBK_AssessmentDeclaration.StatusId,
                UserId = UserHelper.GetCurrentEmployee().Id,
                Note = stage.OBK_AssessmentDeclaration.DesignNote
            };
            new SafetyAssessmentRepository().SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            if (withSave)
                AppContext.SaveChanges();
        }

        public OBK_Ref_StageStatus GetStageStatusByCode(string code)
        {
            return AppContext.OBK_Ref_StageStatus.AsNoTracking().FirstOrDefault(e => e.Code == code);
        }

        public Employee GetExecutorByDicStageId(int stageId)
        {
            //цоз
            if (stageId == 1)
            {
                var organization = AppContext.Units.FirstOrDefault(e => e.Id == new Guid("BBF0867E-E3EC-4B02-8B7D-B08FE96A893B"));
                return AppContext.Employees.FirstOrDefault(x => x.Id == new Guid(organization.BossId));
            }
            // уобк
            if (stageId == 2)
            {
                return AppContext.Employees.FirstOrDefault(
                    e => e.Id == new Guid("14D1A1F0-9501-4232-9C29-E9C394D88784"));
            }
            return null;
        }
    }
}
