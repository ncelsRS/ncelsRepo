using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Recources;
using PW.Ncels.Database.Repository.Common;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class ExpertiseStageRepository : ARepository
    {
        public ExpertiseStageRepository()
        {

        }
        public ExpertiseStageRepository(ncelsEntities context) : base(context)
        {

        }
        /// <summary>
        /// Отправка этапов экспертизы в работу выбранным исполнителям
        /// </summary>
        /// <param name="stageIds"></param>
        /// <param name="executorIds"></param>
        public void SendToWork(Guid[] stageIds, Guid[] executorIds)
        {
            var stages = AppContext.EXP_ExpertiseStage.Include(e => e.Executors).Where(e => stageIds.Contains(e.Id)).ToList();
            var executors = AppContext.Employees.Where(e => executorIds.Contains(e.Id)).ToList();
            foreach (var stage in stages)
            {
                stage.Executors.AddRange(executors);
                stage.StatusId = GetStageStatusByCode(EXP_DIC_StageStatus.InWork).Id;
            }
            AppContext.SaveChanges();
        }

        public EXP_DIC_StageStatus GetStageStatusByCode(string code)
        {
            return AppContext.EXP_DIC_StageStatus.AsNoTracking().FirstOrDefault(e => e.Code == code);
        }
        public EXP_DIC_Stage GetStageByCode(string code)
        {
            return AppContext.EXP_DIC_Stage.AsNoTracking().FirstOrDefault(e => e.Code == code);
        }

        /// <summary>
        /// Проверка есть ли у заявления указанный этап
        /// </summary>
        /// <param name="declarationId"></param>
        /// <param name="stageCode">Код этапа</param>
        /// <returns></returns>
        public bool HasStage(Guid declarationId, int stageId)
        {
            return AppContext.EXP_ExpertiseStage.Any(e => e.DeclarationId == declarationId &&
                                                          e.StageId == stageId);
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
                EXP_DIC_StageStatus.New, EXP_DIC_StageStatus.InWork,
                EXP_DIC_StageStatus.InReWork
            };
            var declaration = AppContext.EXP_DrugDeclaration.FirstOrDefault(e => e.Id == declarationId);
            if (declaration.EXP_DIC_Type.Code != EXP_DIC_Type.Registration)
            {
                return ToNextStage(declaration, fromStageId, nextStageIds, out resultDescription);
            }
            var currentStage = fromStageId != null
                ? AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == fromStageId)
                : AppContext.EXP_ExpertiseStage.FirstOrDefault(
                    e => e.DeclarationId == declarationId && activeStageCodes.Contains(e.EXP_DIC_StageStatus.Code));
            var dosageIds = AppContext.EXP_DrugDosage.Where(e => e.DrugDeclarationId == declarationId)
                .Select(e => e.Id)
                .ToList();
            var stageStatusNew = GetStageStatusByCode(EXP_DIC_StageStatus.New);
            //закрываем предыдущий этап
            if (currentStage != null && CanCloseStage(currentStage, nextStageIds))
            {
                currentStage.StatusId = GetStageStatusByCode(EXP_DIC_StageStatus.Completed).Id;
                currentStage.FactEndDate = DateTime.Now;
            }
            var isAnalitic = false;
            foreach (var nextStageId in nextStageIds)
            {
                if (!CanSendToStep(declarationId, fromStageId, nextStageId, out resultDescription)) return false;
                //если имеется уже выполняющийся этап то продолжаем его дальше
                if (AppContext.EXP_ExpertiseStage.Any(e => e.DeclarationId == declarationId
                                                           && e.StageId == nextStageId &&
                                                           e.EXP_DIC_StageStatus.Code != EXP_DIC_StageStatus.Completed &&
                                                           !e.IsHistory)) continue;
                var daysOnStage = GetExpStageDaysOnExecution(declaration.TypeId, nextStageId);
                var startDate = DateTime.Now;
                var newStage = new EXP_ExpertiseStage()
                {
                    Id = Guid.NewGuid(),
                    DeclarationId = declarationId,
                    StageId = nextStageId,
                    ParentStageId = currentStage != null ? (Guid?) currentStage.Id : null,
                    StatusId = stageStatusNew.Id,
                    StartDate = startDate,
                    EndDate = daysOnStage != null ? (DateTime?) startDate.AddDays(daysOnStage.Value) : null
                };
                //todo брать руководителя цоз из настроек
                newStage.Executors.Add(GetExecutorByDicStageId(nextStageId));
                foreach (var dosageId in dosageIds)
                {
                    newStage.EXP_ExpertiseStageDosage.Add(new EXP_ExpertiseStageDosage()
                    {
                        Id = Guid.NewGuid(),
                        DosageId = dosageId,
                        EXP_ExpertiseStage = newStage
                    });
                }
                AppContext.EXP_ExpertiseStage.Add(newStage);
                if (nextStageId == CodeConstManager.STAGE_ANALITIC)
                {
                    isAnalitic = true;
                }
            }
            AppContext.SaveChanges();
            if (isAnalitic)
            {
                CreateDirectionMaterial(declarationId, GetExecutorByDicStageId(CodeConstManager.STAGE_ANALITIC));
            }
            return true;
        }

        private bool ToNextStage(EXP_DrugDeclaration declaration, Guid? fromStageId, int[] nextStageIds, out string resultDescription)
        {
            resultDescription = null;
            string[] activeStageCodes = { EXP_DIC_StageStatus.New, EXP_DIC_StageStatus.InWork, EXP_DIC_StageStatus.InReWork };
            var currentStage = fromStageId != null ? AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == fromStageId)
                : AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.DeclarationId == declaration.Id && activeStageCodes.Contains(e.EXP_DIC_StageStatus.Code));
            var dosageIds = AppContext.EXP_DrugDosage.Where(e => e.DrugDeclarationId == declaration.Id)
                .Select(e => e.Id)
                .ToList();
            var stageStatusNew = GetStageStatusByCode(EXP_DIC_StageStatus.New);
            var stageStatusRework = GetStageStatusByCode(EXP_DIC_StageStatus.InReWork);
            //закрываем предыдущий этап
            if (currentStage != null)
            {
                currentStage.StatusId = GetStageStatusByCode(EXP_DIC_StageStatus.Completed).Id;
                currentStage.FactEndDate = DateTime.Now;
            }
            foreach (var nextStageId in nextStageIds)
            {
                if (!CanSendToStep(declaration.Id, fromStageId, nextStageId, out resultDescription)) return false;
                //если имеется уже выполняющийся этап то продолжаем его дальше
                if (AppContext.EXP_ExpertiseStage.Any(e => e.DeclarationId == declaration.Id
                                                           && e.StageId == nextStageId && e.EXP_DIC_StageStatus.Code != EXP_DIC_StageStatus.Completed && !e.IsHistory)) continue;
                var oldStage = AppContext.EXP_ExpertiseStage.FirstOrDefault(es => es.IsHistory == false
                                                                                  && es.DeclarationId == declaration.Id && es.StageId == nextStageId);
                if (oldStage == null)
                {
                    var daysOnStage = GetExpStageDaysOnExecution(declaration.TypeId, nextStageId);
                    var startDate = DateTime.Now;
                    var newStage = new EXP_ExpertiseStage()
                    {
                        Id = Guid.NewGuid(),
                        DeclarationId = declaration.Id,
                        StageId = nextStageId,
                        ParentStageId = currentStage != null ? (Guid?)currentStage.Id : null,
                        StatusId = stageStatusNew.Id,
                        StartDate = startDate,
                        EndDate = daysOnStage != null ? (DateTime?)startDate.AddDays(daysOnStage.Value) : null
                    };
                    //todo брать руководителя цоз из настроек
                    newStage.Executors.Add(GetExecutorByDicStageId(nextStageId));
                    foreach (var dosageId in dosageIds)
                    {
                        newStage.EXP_ExpertiseStageDosage.Add(new EXP_ExpertiseStageDosage()
                        {
                            Id = Guid.NewGuid(),
                            DosageId = dosageId,
                            EXP_ExpertiseStage = newStage
                        });
                    }
                    AppContext.EXP_ExpertiseStage.Add(newStage);
                }
                else
                {
                    var reworkStage = new EXP_ExpertiseStage()
                    {
                        Id = Guid.NewGuid(),
                        DeclarationId = declaration.Id,
                        StageId = nextStageId,
                        ParentStageId = currentStage != null ? (Guid?)currentStage.Id : null,
                        StatusId = stageStatusRework.Id,
                        StartDate = oldStage.StartDate,
                        EndDate = oldStage.EndDate,
                        OtdIds = oldStage.OtdIds,
                        OtdRemarks = oldStage.OtdRemarks
                    };
                    reworkStage.Executors.AddRange(oldStage.Executors);
                    foreach (var dossageStage in oldStage.EXP_ExpertiseStageDosage)
                    {
                        var reworkDosage = new EXP_ExpertiseStageDosage()
                        {
                            Id = Guid.NewGuid(),
                            DosageId = dossageStage.DosageId,
                            EXP_ExpertiseStage = reworkStage
                        };
                        foreach (var primaryFinalDoc in dossageStage.PrimaryFinalDocs)
                        {
                            var docClone = new EXP_ExpertisePrimaryFinalDoc(primaryFinalDoc);
                            docClone.Id = Guid.NewGuid();
                            docClone.EXP_ExpertiseStageDosage = reworkDosage;
                            docClone.DosageStageId = reworkStage.Id;
                            reworkDosage.PrimaryFinalDocs.Add(docClone);
                        }
                        foreach (var pharmaceuticalFinalDoc in dossageStage.EXP_ExpertisePharmaceuticalFinalDoc)
                        {
                            var docClone = new EXP_ExpertisePharmaceuticalFinalDoc(pharmaceuticalFinalDoc);
                            docClone.Id = Guid.NewGuid();
                            docClone.EXP_ExpertiseStageDosage = reworkDosage;
                            docClone.DosageStageId = reworkStage.Id;
                            reworkDosage.EXP_ExpertisePharmaceuticalFinalDoc.Add(docClone);
                        }
                        foreach (var pharmacologicalFinalDoc in dossageStage.EXP_ExpertisePharmacologicalFinalDoc)
                        {
                            var docClone = new EXP_ExpertisePharmacologicalFinalDoc(pharmacologicalFinalDoc);
                            docClone.Id = Guid.NewGuid();
                            docClone.EXP_ExpertiseStageDosage = reworkDosage;
                            docClone.DosageStageId = reworkStage.Id;
                            reworkDosage.EXP_ExpertisePharmacologicalFinalDoc.Add(docClone);
                        }
                        foreach (var safetyreportFinalDoc in dossageStage.EXP_ExpertiseSafetyreportFinalDoc)
                        {
                            var docClone = new EXP_ExpertiseSafetyreportFinalDoc(safetyreportFinalDoc);
                            docClone.Id = Guid.NewGuid();
                            docClone.EXP_ExpertiseStageDosage = reworkDosage;
                            docClone.DosageStageId = reworkStage.Id;
                            reworkDosage.EXP_ExpertiseSafetyreportFinalDoc.Add(docClone);
                        }
                        foreach (var analiseIndicator in dossageStage.EXP_DrugAnaliseIndicator)
                        {
                            var docClone = new EXP_DrugAnaliseIndicator(analiseIndicator);
                            docClone.Id = Guid.NewGuid();
                            docClone.EXP_ExpertiseStageDosage = reworkDosage;
                            docClone.DosageStageId = reworkStage.Id;
                            reworkDosage.EXP_DrugAnaliseIndicator.Add(docClone);
                        }
                        reworkStage.EXP_ExpertiseStageDosage.Add(reworkDosage);
                    }
                    AppContext.EXP_ExpertiseStage.Add(reworkStage);
                    oldStage.IsHistory = true;
                }

            }
            AppContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Возвращать на предыдущий этап
        /// </summary>
        /// <param name="declarationId"></param>
        /// <param name="fromStageId"></param>
        /// <param name="nextStageIds"></param>
        /// <param name="resultDescription"></param>
        /// <returns></returns>
        public bool ToBackStage(Guid declarationId, Guid? fromStageId, int[] nextStageIds, out string resultDescription)
        {
            resultDescription = null;
            var stageStatusInRework = GetStageStatusByCode(EXP_DIC_StageStatus.InReWork);

            string[] activeStageCodes = { EXP_DIC_StageStatus.InWork, EXP_DIC_StageStatus.InReWork };
            var currentStage = fromStageId != null ? AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == fromStageId)
               : AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.DeclarationId == declarationId && activeStageCodes.Contains(e.EXP_DIC_StageStatus.Code));
            if (currentStage != null)
            {
                currentStage.StatusId = GetStageStatusByCode(EXP_DIC_StageStatus.Completed).Id;
                currentStage.FactEndDate = DateTime.Now;
            }

            foreach (var nextStageId in nextStageIds)
            {
                if (!CanSendToStep(declarationId, fromStageId, nextStageId, out resultDescription)) return false;

                var oldStage = AppContext.EXP_ExpertiseStage.FirstOrDefault(es => es.IsHistory == false
                                        && es.DeclarationId == declarationId && es.StageId == nextStageId);
                var dosageIds = AppContext.EXP_DrugDosage.Where(e => e.DrugDeclarationId == declarationId)
                    .Select(e => e.Id)
                    .ToList();

                if (oldStage != null)
                {
                    var newStage = new EXP_ExpertiseStage()
                    {
                        Id = Guid.NewGuid(),
                        DeclarationId = declarationId,
                        StageId = nextStageId,
                        ParentStageId = oldStage?.Id,
                        StatusId = stageStatusInRework.Id,
                        OtdIds = oldStage.OtdIds
                    };
                    newStage.Executors = oldStage.Executors;
                    AppContext.EXP_ExpertiseStage.Add(newStage);

                    oldStage.IsHistory = true;

                    foreach (var dosageId in dosageIds)
                    {
                        var newDosageStage = new EXP_ExpertiseStageDosage()
                        {
                            Id = Guid.NewGuid(),
                            DosageId = dosageId,
                            EXP_ExpertiseStage = newStage
                        };
                        newStage.EXP_ExpertiseStageDosage.Add(newDosageStage);

                        switch (oldStage.EXP_DIC_Stage.Code)
                        {
                            case EXP_DIC_Stage.PharmacologicalExp:
                                var oldpcolFinalDoc = AppContext.EXP_ExpertisePharmacologicalFinalDoc.FirstOrDefault(
                                    pc =>
                                        pc.EXP_ExpertiseStageDosage.DosageId == dosageId &&
                                        pc.EXP_ExpertiseStageDosage.StageId == oldStage.Id);
                                if (oldpcolFinalDoc != null)
                                {
                                    var newpcolFinalDoc = new EXP_ExpertisePharmacologicalFinalDoc(oldpcolFinalDoc);
                                    newpcolFinalDoc.DosageStageId = newDosageStage.Id;
                                    newpcolFinalDoc.EXP_ExpertiseStageDosage = newDosageStage;
                                    newDosageStage.EXP_ExpertisePharmacologicalFinalDoc.Add(newpcolFinalDoc);
                                }
                                break;
                            case EXP_DIC_Stage.PharmaceuticalExp:
                                var oldpceuFinalDoc = AppContext.EXP_ExpertisePharmaceuticalFinalDoc.FirstOrDefault(
                                    pc =>
                                        pc.EXP_ExpertiseStageDosage.DosageId == dosageId &&
                                        pc.EXP_ExpertiseStageDosage.StageId == oldStage.Id);
                                if (oldpceuFinalDoc != null)
                                {
                                    var newpceulFinalDoc = new EXP_ExpertisePharmaceuticalFinalDoc(oldpceuFinalDoc);
                                    newpceulFinalDoc.DosageStageId = newDosageStage.Id;
                                    newpceulFinalDoc.EXP_ExpertiseStageDosage = newDosageStage;
                                    newDosageStage.EXP_ExpertisePharmaceuticalFinalDoc.Add(newpceulFinalDoc);
                                }
                                break;
                        }

                    }
                    AppContext.EXP_ExpertiseStage.Add(newStage);
                }
            }
            AppContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Проверка возможности отправки на следующий этап
        /// </summary>
        /// <param name="declaration"></param>
        /// <param name="stageCode"></param>
        /// <returns></returns>
        private bool CanSendToStep(Guid declarationId, Guid? fromStageId, int stageCode, out string resultDescription)
        {
            resultDescription = null;
            return true;
        }

        public EXP_ExpertiseStage GetById(Guid? id)
        {
            return AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == id);
        }

        public void SetStageResult(Guid stageId, int resultId)
        {
            var stage = AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == stageId);
            stage.ResultId = resultId;
            AppContext.SaveChanges();
        }

        public Guid GetDeclarationIdByStage(Guid expStageId)
        {
            return AppContext.EXP_ExpertiseStage.Where(e => e.Id == expStageId)
                .Select(e => e.DeclarationId)
                .FirstOrDefault();
        }

        public EXP_DrugDeclaration GetDeclarationByStage(Guid expStageId)
        {
            return AppContext.EXP_ExpertiseStage.Where(e => e.Id == expStageId)
                .Select(e => e.EXP_DrugDeclaration)
                .FirstOrDefault();
        }

        public void SetExpertiseStageDosageResult(Guid dosageStageId, int resultId)
        {
            var creatorId = UserHelper.GetCurrentEmployee().Id;
            var dosageStage = AppContext.EXP_ExpertiseStageDosage.First(e => e.Id == dosageStageId);
            dosageStage.ResultId = resultId;
            var r = new EXP_ExpertiseStageDosageResult();
            r.ResultDate = DateTime.Now;
            r.ResultId = resultId;
            r.ResultCreatorId = creatorId;
            r.StageDosageId = dosageStageId;
            //todo тут похорошему ещё говнопроверок надо напихать, если заявка на совете, похорошему её нельзя редактировать
            var lastResultInCommission = AppContext.CommissionDrugDosages.FirstOrDefault(x => x.DrugDosageId == dosageStage.DosageId && x.StageId == dosageStage.StageId && x.ConclusionTypeId == null);
            if (lastResultInCommission != null)
            {
                lastResultInCommission.EXP_ExpertiseStageDosageResult = r;
            }
            AppContext.EXP_ExpertiseStageDosageResult.Add(r);
            var addLogInfo = "";
            addLogInfo += "resultId: " + resultId;
            ActionLogger.WriteInt(AppContext, creatorId, "Заявка №" + dosageStage.EXP_DrugDosage.RegNumber + " выставление результата ", addLogInfo);
            AppContext.SaveChanges();
        }

        public EXP_DIC_Stage GetDicStageById(int id)
        {
            return AppContext.EXP_DIC_Stage.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }
        /// <summary>
        /// Получение исполнителя этапа
        /// </summary>
        /// <param name="stageId"></param>
        /// <returns></returns>
        public Employee GetExecutorByDicStageId(int stageId)
        {
            string login;
            var stage = GetDicStageById(stageId);
            switch (stage.Code)
            {
                case EXP_DIC_Stage.ProcCenter:
                    login = "orazbai.k";
                    break;
                case EXP_DIC_Stage.PrimaryExp:
                    login = "abdukayumov.i";
                    break;
                case EXP_DIC_Stage.PharmaceuticalExp:
                    login = "kalelova.R";
                    break;
                case EXP_DIC_Stage.PharmacologicalExp:
                    login = "zhubanturiyeva.a";
                    break;
                case EXP_DIC_Stage.SafetyConclusion:
                    login = "sultangazieva.s";
                    break;
                case EXP_DIC_Stage.AnalyticalExp:
                    login = "sadyk.a";
                    break;
                case EXP_DIC_Stage.Translate:
                    login = "nesipbaeva.g";
                    break;
                default:
                    login = "grebennikova.v";
                    break;
            }
            var employee = AppContext.Employees.FirstOrDefault(e => e.Login == login) ??
                           AppContext.Employees.FirstOrDefault(e => e.Login == "grebennikova.v");
            return employee;
        }

        private int? GetExpStageDaysOnExecution(int expTypeId, int dicStageId)
        {
            var expType = AppContext.EXP_DIC_Type.AsNoTracking().FirstOrDefault();
            var dicStage = GetDicStageById(dicStageId);
            var resitrationType = AppContext.EXP_RegistrationTypes.AsNoTracking()
                .FirstOrDefault(e => e.Code == expType.Code);
            if (resitrationType == null) return null;
            var registrationExpSteps =
                AppContext.EXP_RegistrationExpSteps.FirstOrDefault(e => e.RegistrationId == resitrationType.Id &&
                                                                        e.Name.ToLower() == dicStage.NameRu.ToLower());
            return registrationExpSteps != null ? (int?)registrationExpSteps.Duration : null;
        }

        private bool CanCloseStage(EXP_ExpertiseStage stage, int[] nextDicStageIds)
        {
            var nextStages = AppContext.EXP_DIC_Stage.AsNoTracking().Where(e => nextDicStageIds.Contains(e.Id)).ToList();
            switch (stage.EXP_DIC_Stage.Code)
            {
                case EXP_DIC_Stage.PharmaceuticalExp:
                    return !nextStages.Any(e => e.Code == EXP_DIC_Stage.AnalyticalExp);
                case EXP_DIC_Stage.Translate:
                    return AppContext.EXP_ExpertiseStage.Any(e => e.DeclarationId == stage.DeclarationId
                                                                  && e.EXP_DIC_Stage.Code == EXP_DIC_Stage
                                                                      .SafetyConclusion && !e.IsHistory);
                case EXP_DIC_Stage.SafetyConclusion:
                    return !AppContext.EXP_ExpertiseStage.Any(e => e.DeclarationId == stage.DeclarationId
                                                                  && e.EXP_DIC_Stage.Code == EXP_DIC_Stage.Translate && !e.IsHistory
                                                                  && e.EXP_DIC_StageStatus.Code != EXP_DIC_StageStatus.Completed);
            }
            return true;
        }

        public void EndExpertise(Guid lastStageId)
        {
            var stage = AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == lastStageId);
            stage.StatusId = GetStageStatusByCode(EXP_DIC_StageStatus.Completed).Id;
            stage.EXP_DrugDeclaration.StatusId = AppContext.EXP_DIC_Status.FirstOrDefault(e => e.Code == EXP_DIC_Status.Completed).Id;
            AppContext.SaveChanges();
        }
        public void RefuseExpertise(Guid lastStageId, bool withSave)
        {
            var stage = AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == lastStageId);
            stage.StatusId = GetStageStatusByCode(EXP_DIC_StageStatus.Completed).Id;
            stage.FactEndDate=DateTime.Now;
            stage.EXP_DrugDeclaration.StatusId = AppContext.EXP_DIC_Status.FirstOrDefault(e => e.Code == EXP_DIC_Status.Refused).Id;
            if (withSave)
                AppContext.SaveChanges();
        }

        public void StartRefuseInExpertise(Guid stageId, bool withSave)
        {
            var stage = AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == stageId);
            stage.EXP_DrugDeclaration.StatusId = CodeConstManager.STATUS_EXP_ON_REFUSING_ID;
            var primaryExpRepo = new DrugPrimaryRepository();
            primaryExpRepo.CreateLetter(stage.StageId, UserHelper.GetCurrentEmployee().Id, stage.DeclarationId, EXP_DIC_CorespondenceSubject.RefuseByPayment,
                Messages.RefuseRegistrationMsg.Replace("\\t", "\t").Replace("\\r","\r").Replace("\\n","\n"));
            if (withSave)
                AppContext.SaveChanges();
        }
        private void CreateDirectionMaterial(Guid drugDeclarationId, Employee getExecutorByDicStageId)
        {
          /*  try
            {*/
                MaterialDirectionRepository mRepository = new MaterialDirectionRepository();
                mRepository.CreateMaterialDirectionAsync(drugDeclarationId, getExecutorByDicStageId);
           /* }
            catch (Exception e)
            {

            }*/
        }

        public EXP_ExpertiseStage GetStage(Guid stageId)
        {
            return AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == stageId);
        }

        public EXP_ExpertiseStage GetStage(Guid declarationId, int dicStageId)
        {
            return AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.DeclarationId == declarationId && e.StageId == dicStageId && !e.IsHistory);
        }

        public void CompleteStage(Guid stageId, bool withSave)
        {
            var stage = AppContext.EXP_ExpertiseStage.FirstOrDefault(e => e.Id == stageId);
            stage.StatusId = GetStageStatusByCode(EXP_DIC_StageStatus.Completed).Id;
            stage.FactEndDate=DateTime.Now;
            if (withSave)
                AppContext.SaveChanges();
        }

        public IQueryable<EXP_DIC_Stage> ListStages(int currentDicStageId, int regTypeId, int stageResultId = 0)
        {
            var query = AppContext.EXP_DIC_Stage.Where(e => !e.IsDeleted && e.Code != EXP_DIC_Stage.ProcCenter && e.Code != EXP_DIC_Stage.ExpertCouncil
                                                    && e.Id != currentDicStageId);
            var regType = AppContext.EXP_DIC_Type.FirstOrDefault(e => e.Id == regTypeId);
            if (regType.Code != EXP_DIC_Type.Registration)
                return query;
            var rep = new ExpertiseStageRepository();
            var stage = rep.GetDicStageById(currentDicStageId);
            switch (stage.Code)
            {
                case EXP_DIC_Stage.PharmacologicalExp:
                    query = query.Where(e => e.Code == EXP_DIC_Stage.Translate);
                    break;
                case EXP_DIC_Stage.PharmaceuticalExp:
                    query = query.Where(e => e.Code == EXP_DIC_Stage.AnalyticalExp || e.Code == EXP_DIC_Stage.Translate);
                    break;
                case EXP_DIC_Stage.AnalyticalExp:
                    query = query.Where(e => e.Code == EXP_DIC_Stage.PharmaceuticalExp);
                    break;
                case EXP_DIC_Stage.Translate:
                    query = query.Where(e => e.Code == EXP_DIC_Stage.SafetyConclusion);
                    break;
                case EXP_DIC_Stage.SafetyConclusion:
                    var dictionaryRepo = new ReadOnlyDictionaryRepository();
                    var stageResult = dictionaryRepo.GetStageResultById(stageResultId);
                    if (stageResult.Code == EXP_DIC_StageResult.MatchesCode)
                        query = query.Where(e => e.Code == EXP_DIC_Stage.Translate);
                    else
                        query = query.Where(e => e.Code == EXP_DIC_Stage.Translate || e.Code == EXP_DIC_Stage.PharmaceuticalExp || e.Code == EXP_DIC_Stage.PharmacologicalExp);
                    break;
            }
            return query;
        }

        public List<EXP_ExpertiseStage> GetExpExpertiseStagesByDeclarationId(Guid declarationId)
        {
            return
                AppContext.EXP_ExpertiseStage.Where(
                    e => e.DeclarationId == declarationId && !e.IsHistory).ToList();
        }
    }
}