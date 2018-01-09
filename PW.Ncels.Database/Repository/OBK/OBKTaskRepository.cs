using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.OBK;

namespace PW.Ncels.Database.Repository.OBK
{
    public class OBKTaskRepository : ARepository
    {
        public OBKTaskRepository()
        {
            AppContext = CreateDatabaseContext();
        }

        public OBKTaskRepository(bool isProxy)
        {
            AppContext = CreateDatabaseContext(isProxy);
        }

        public OBK_StageExpDocumentResult GetStageExpDocumentResult(Guid id)
        {
            return AppContext.OBK_StageExpDocumentResult.FirstOrDefault(e => e.AssessmetDeclarationId == id);
        }

        public OBK_AssessmentDeclaration GetAssessmentDeclaration(Guid id)
        {
            return AppContext.OBK_AssessmentDeclaration.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<OBK_Tasks> GetTasks(Guid id)
        {
            return AppContext.OBK_Tasks.Where(e => e.AssessmentDeclarationId == id);
        }

        public OBK_TaskExecutor GetTaskExecutor(Guid taskId, Guid userId)
        {
            return AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == taskId && e.ExecutorId == userId);
        }

        public IQueryable<OBK_TaskMaterial> GetTaskMaterial(IQueryable<OBK_Tasks> tasks)
        {
            return AppContext.OBK_TaskMaterial.Where(e => tasks.Any(x => x.Id == e.TaskId));
        }

        public IQueryable<OBK_TaskMaterial> OBK_TaskMaterial(Guid taskId)
        {
            return AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId);
        }

        public IQueryable<Unit> GetUnits(string[] code)
        {
            return AppContext.Units.Where(e => code.Contains(e.Code));
        }

        public Unit GetUnit(Guid unitId)
        {
            return AppContext.Units.FirstOrDefault(e => e.Id == unitId);
        }

        public IQueryable<OBK_Ref_LaboratoryType> GeLaboratoryTypes()
        {
            return AppContext.OBK_Ref_LaboratoryType.Where(e => !e.IsDeleted);
        }

        public IQueryable<OBKTaskListRegisterView> GetTaskList(Guid organizationId, Guid userId)
        {
            return AppContext.OBKTaskListRegisterViews.Where(e => e.UnitId == organizationId && e.ExecutorId == userId);
        }

        public IQueryable<OBK_Ref_MaterialCondition> GetMaterialCondition(string code)
        {
            return AppContext.OBK_Ref_MaterialCondition.Where(e => !e.IsDeleted && e.Code == code);
        }
        public List<Employee> GetEmployees(Guid unitId)
        {
            var units = AppContext.Units.Where(e => e.ParentId == unitId && e.EmployeeId != null);
            var employee = AppContext.Employees.Where(e => units.Any(x => x.EmployeeId == e.Id)).ToList();
            return employee;
        }

        public OBK_Ref_StageStatus GetStageStatusByCode(string code)
        {
            return AppContext.OBK_Ref_StageStatus.AsNoTracking().FirstOrDefault(e => e.Code == code);
        }

        public IQueryable<OBK_Ref_LaboratoryMark> GetLaboratoryMark()
        {
            return AppContext.OBK_Ref_LaboratoryMark.Where(e => !e.IsDeleted);
        }

        public IQueryable<OBK_Ref_LaboratoryRegulation> GetRegulation(Guid id)
        {
            return AppContext.OBK_Ref_LaboratoryRegulation.Where(e => !e.IsDeleted && e.LaboratoryMarkId == id);
        }

        public List<Employee> GetExecutors(string code)
        {
            var userOrgId = UserHelper.GetCurrentEmployee().OrganizationId;
            var unit = AppContext.Units.FirstOrDefault(e => e.Code == code && e.ParentId == userOrgId);
            if (unit == null)
                unit = AppContext.Units.FirstOrDefault(e => e.Code == code && e.Parent.ParentId == userOrgId);
            var units = AppContext.Units.Where(e => e.ParentId == unit.Id);
            var employee = AppContext.Employees.Where(e => units.Any(x => x.EmployeeId == e.Id)).ToList();
            if (!employee.Any())
            {
                if (unit.BossId != null)
                {
                    employee = AppContext.Employees.Where(e => unit.BossId == e.Id.ToString()).ToList();
                }
            }
            return employee;
        }

        public void SaveTask(OBKCreateTaskViewModel model)
        {
            OBK_Tasks obkTasks = new OBK_Tasks
            {
                Id = Guid.NewGuid(),
                ActReceptionId = model.ActReceptionId,
                //ExecutorId = UserHelper.GetCurrentEmployee().Id,
                RegisterDate = model.RegisterDate,
                TaskNumber = model.TaskNumber,
                AssessmentDeclarationId = model.AssessmentDeclarationId,
                UnitId = model.UnitId
            };
            AppContext.OBK_Tasks.Add(obkTasks);

            OBK_TaskExecutor taskExecutor = new OBK_TaskExecutor
            {
                Id = Guid.NewGuid(),
                ExecutorId = UserHelper.GetCurrentEmployee().Id,
                TaskId = obkTasks.Id,
                StageId = 3
            };
            AppContext.OBK_TaskExecutor.Add(taskExecutor);

            List<OBK_TaskMaterial> taskLists = new List<OBK_TaskMaterial>();
            foreach (var modal in model.ModalViewModels)
            {
                foreach (var laboratory in modal.LaboratoryTypeIds)
                {
                    OBK_TaskMaterial taskList = new OBK_TaskMaterial
                    {
                        Id = Guid.NewGuid(),
                        LaboratoryTypeId = Guid.Parse(laboratory),
                        TaskId = obkTasks.Id,
                        ProductSeriesId = modal.Id
                    };
                    taskLists.Add(taskList);
                }
            }
            AppContext.OBK_TaskMaterial.AddRange(taskLists);
            AppContext.SaveChanges();
        }

        public string GetSignTask(Guid id)
        {
            var signDatas = AppContext.OBK_Tasks.Where(e => e.AssessmentDeclarationId == id);
            List<OBK_Tasks> tasks = new List<OBK_Tasks>();
            foreach (var signData in signDatas)
            {
                OBK_Tasks task = new OBK_Tasks
                {
                    Id = signData.Id,
                    TaskNumber = signData.TaskNumber,
                    AssessmentDeclarationId = signData.AssessmentDeclarationId,
                    UnitId = signData.UnitId,
                    //ExecutorId = signData.ExecutorId,
                    RegisterDate = signData.RegisterDate,
                    ActReceptionId = signData.ActReceptionId
                };
                tasks.Add(task);
            }
            var xmlData = SerializeHelper.SerializeDataContract(tasks);
            var data = xmlData.Replace("utf-16", "utf-8");
            return data;
        }

        public void SendToNextStep(Guid id, Guid executorId, string code)
        {
            List<OBK_TaskExecutor> taskExecutors = new List<OBK_TaskExecutor>();
            switch (code)
            {
                case "coz":
                    var tasks = AppContext.OBK_Tasks.Where(e => e.AssessmentDeclarationId == id);
                    foreach (var task in tasks)
                    {
                        if (task.OBK_TaskExecutor.FirstOrDefault(
                                e => e.TaskId == task.Id && e.ExecutorId == executorId &&
                                     e.StageId == CodeConstManager.STAGE_OBK_COZ) == null)
                        {
                            OBK_TaskExecutor taskExecutor = new OBK_TaskExecutor
                            {
                                Id = Guid.NewGuid(),
                                ExecutorId = executorId,
                                TaskId = task.Id,
                                StageId = CodeConstManager.STAGE_OBK_COZ
                            };
                            taskExecutors.Add(taskExecutor);
                            task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskNew).Id;
                        }
                    }
                    //костыль, переносим в другой статус
                    var stage =
                        AppContext.OBK_AssessmentStage.FirstOrDefault(
                            e => e.DeclarationId == id && e.StageId == CodeConstManager.STAGE_OBK_EXPERTISE_DOC);
                    stage.StageStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.RequiresConclusion).Id;
                    break;
                    //отдел снажения ИЦл
                case "supplyDivision":
                    var tasks1 = AppContext.OBK_Tasks.Where(e => e.Id == id);
                    foreach (var task in tasks1)
                    {
                        if (task.OBK_TaskExecutor.FirstOrDefault(
                                e => e.TaskId == task.Id && e.ExecutorId == executorId &&
                                     e.StageId == CodeConstManager.STAGE_OBK_ICL) == null)
                        {
                            OBK_TaskExecutor taskExecutor = new OBK_TaskExecutor
                            {
                                Id = Guid.NewGuid(),
                                ExecutorId = executorId,
                                TaskId = task.Id,
                                StageId = CodeConstManager.STAGE_OBK_ICL
                            };
                            taskExecutors.Add(taskExecutor);
                            task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskSendRC).Id;
                        }
                    }
                    break;
            }
            AppContext.OBK_TaskExecutor.AddRange(taskExecutors);
            AppContext.SaveChanges();
        }

        public OBKTaskListViewModel EditTask(Guid taskId)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            if (task == null)
                return null;
            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId);
            var productSerieses = AppContext.OBK_Procunts_Series.Where(e => taskMaterials.GroupBy(q => q.ProductSeriesId).Select(s => s.Key).Contains(e.Id));

            OBKTaskListViewModel taskViewModel = new OBKTaskListViewModel
            {
                TaskId = task.Id,
                CozExecutorName = AppContext.Employees.FirstOrDefault(e => e.Id == task.CozExecutorId)?.ShortName,
                SentToIC = task.SendToIC != null ? task.SendToIC.ToString() : null,
                CheckBoxIsVisible = task.OBK_AssessmentDeclaration.OBKApplicantParty ?? false,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ExecutorName = task.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == task.Id && e.StageId == CodeConstManager.STAGE_OBK_UOBK)?.Employee.ShortName,
                ICExecutorName = task.OBK_TaskExecutor.FirstOrDefault(e=>e.TaskId==task.Id && e.StageId == CodeConstManager.STAGE_OBK_ICL)?.Employee.ShortName,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName
            };

            List<OBKProductViewModel> productSeriesListViewModels = new List<OBKProductViewModel>();
            foreach (var productSeries in productSerieses)
            {
                OBKProductViewModel productSeriesList = new OBKProductViewModel
                {
                    Id = productSeries.Id,
                    ProductNameRu = productSeries.OBK_RS_Products.NameRu,
                    Quantity = productSeries.Quantity,
                    Series = productSeries.Series,
                    SeriesMeasure = productSeries.sr_measures.name,
                    SeriesStartdate = productSeries.SeriesStartdate,
                    SeriesEndDate = productSeries.SeriesEndDate,
                    LaboratoryName = string.Join(", ", taskMaterials.Where(e => e.ProductSeriesId == productSeries.Id).Select(x => x.OBK_Ref_LaboratoryType.NameRu).GroupBy(x => x).Select(g => g.Key))
                };
                productSeriesListViewModels.Add(productSeriesList);
            }
            taskViewModel.ProductListViewModels = productSeriesListViewModels;
            return taskViewModel;
        }

        public OBKTaskResearchCenter EditTaskResearchCenter(Guid taskId)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            if (task == null)
                return null;

            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId);
            var productSerieses = AppContext.OBK_Procunts_Series.Where(e => taskMaterials.GroupBy(q => q.ProductSeriesId).Select(s => s.Key).Contains(e.Id));
            string[] codeResearchCenter = { "researchcenter3", "researchcenter4", "researchcenter5", "researchcenter6", "researchcenter7" };

            if (task.TaskStatusId == GetStageStatusByCode(OBK_Ref_StageStatus.TaskSendLab).Id)
            {
                OBKTaskResearchCenter researchCenter = new OBKTaskResearchCenter
                {
                    TaskId = task.Id,
                    AssessmentDeclarationId = task.AssessmentDeclarationId,
                    TaskNumber = task.TaskNumber,
                    RegisterDate = task.RegisterDate,
                    ActNumber = task.OBK_ActReception.Number,
                    UnitName = task.Unit.ShortName,
                    TaskStatusId = task.TaskStatusId,
                    IsShow = true
                };

                List<OBKTaskListResearchCenter> taskListResearchCenters = new List<OBKTaskListResearchCenter>();
                foreach (var productSeries in productSerieses)
                {
                    OBKTaskListResearchCenter productSeriesList = new OBKTaskListResearchCenter
                    {
                        ProductSeriesId = productSeries.Id,
                        ProductNameRu = productSeries.OBK_RS_Products.NameRu,
                        ProductNameKz = productSeries.OBK_RS_Products.NameKz,
                        Quantity = productSeries.Quantity,
                        Series = productSeries.Series,
                        SeriesMeasure = productSeries.sr_measures.name,
                        SeriesStartdate = productSeries.SeriesStartdate,
                        SeriesEndDate = productSeries.SeriesEndDate,
                        SeriesParty = productSeries.SeriesParty,

                        DimensionIMN = productSeries.OBK_TaskMaterial.First(e => e.ProductSeriesId == productSeries.Id && e.TaskId == task.Id).DimensionIMN,
                        IdNumber = productSeries.OBK_TaskMaterial.First(e => e.ProductSeriesId == productSeries.Id && e.TaskId == task.Id).IdNumber,
                        StorageConditionNameRu = productSeries.OBK_TaskMaterial.First(e=>e.ProductSeriesId == productSeries.Id && e.TaskId == task.Id).OBK_Ref_MaterialCondition.NameRu,
                        StorageConditionNameKz = productSeries.OBK_TaskMaterial.First(e => e.ProductSeriesId == productSeries.Id && e.TaskId == task.Id).OBK_Ref_MaterialCondition.NameKz,
                        ExternalConditionNameRu = productSeries.OBK_TaskMaterial.First(e => e.ProductSeriesId == productSeries.Id && e.TaskId == task.Id).OBK_Ref_MaterialCondition1.NameRu,
                        ExternalConditionNameKz = productSeries.OBK_TaskMaterial.First(e => e.ProductSeriesId == productSeries.Id && e.TaskId == task.Id).OBK_Ref_MaterialCondition1.NameKz,

                        Laboratory = productSeries.OBK_TaskMaterial.Select(e => new OBKLaboratory
                        {
                            LaboratoryId = e.OBK_Ref_LaboratoryType.Id,
                            LaboratoryName = e.OBK_Ref_LaboratoryType.NameRu,
                            ResearchcenterNameRu = e.Unit.DisplayName,
                            ResearchcenterNameKz = e.Unit.DisplayName,
                            Quantity = e.Quantity
                        }).ToList()
                    };
                    taskListResearchCenters.Add(productSeriesList);
                }
                researchCenter.TaskListResearchCenter = taskListResearchCenters;
                return researchCenter;
            }
            else
            {
                OBKTaskResearchCenter researchCenter = new OBKTaskResearchCenter
                {
                    TaskId = task.Id,
                    AssessmentDeclarationId = task.AssessmentDeclarationId,
                    TaskNumber = task.TaskNumber,
                    RegisterDate = task.RegisterDate,
                    ActNumber = task.OBK_ActReception.Number,
                    UnitName = task.Unit.ShortName,
                    TaskStatusId = task.TaskStatusId,
                    StorageConditions = new SelectList(GetMaterialCondition("storage"), "Id", "NameRu"),
                    ExternalConditions = new SelectList(GetMaterialCondition("external"), "Id", "NameRu"),
                    Researchcenters = new SelectList(GetUnits(codeResearchCenter), "Id", "Name"),
                    IsShow = false
                };

                List<OBKTaskListResearchCenter> taskListResearchCenters = new List<OBKTaskListResearchCenter>();
                foreach (var productSeries in productSerieses)
                {
                    OBKTaskListResearchCenter productSeriesList = new OBKTaskListResearchCenter
                    {
                        ProductSeriesId = productSeries.Id,
                        ProductNameRu = productSeries.OBK_RS_Products.NameRu,
                        ProductNameKz = productSeries.OBK_RS_Products.NameKz,
                        Quantity = productSeries.Quantity,
                        Series = productSeries.Series,
                        SeriesMeasure = productSeries.sr_measures.name,
                        SeriesStartdate = productSeries.SeriesStartdate,
                        SeriesEndDate = productSeries.SeriesEndDate,
                        SeriesParty = productSeries.SeriesParty,

                        Laboratory = productSeries.OBK_TaskMaterial.Select(e => new OBKLaboratory
                        {
                            LaboratoryId = e.OBK_Ref_LaboratoryType.Id,
                            LaboratoryName = e.OBK_Ref_LaboratoryType.NameRu
                        }).ToList()
                    };
                    taskListResearchCenters.Add(productSeriesList);
                }
                researchCenter.TaskListResearchCenter = taskListResearchCenters;
                return researchCenter;
            }
        }

        public void AcceptTaskList(OBKTaskListViewModel taskListViewModel)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskListViewModel.TaskId);
            if (task != null)
            {
                task.CozExecutorId = UserHelper.GetCurrentEmployee().Id;
                task.SendToIC = DateTime.Now;
                task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskAcceptCoz).Id;
                AppContext.SaveChanges();
            }
        }

        public bool SendToReseachCenter(OBKTaskResearchCenter taskResearchCenter)
        {
            try
            {
                var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskResearchCenter.TaskId);
                var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == task.Id);

                foreach (var taskMaterial in taskMaterials)
                {
                    foreach (var taskResearch in taskResearchCenter.TaskListResearchCenter)
                    {
                        if (taskResearch.ProductSeriesId == taskMaterial.ProductSeriesId)
                        {
                            taskMaterial.StorageConditionId = taskResearch.StorageCondition;
                            taskMaterial.ExternalConditionId = taskResearch.ExternalCondition;
                            taskMaterial.IdNumber = taskResearch.IdNumber;
                            taskMaterial.DimensionIMN = taskResearch.DimensionIMN;

                            foreach (var researchCenter in taskResearch.SelectedResearchCenter)
                            {
                                if (researchCenter.LaboratoryId == taskMaterial.LaboratoryTypeId)
                                {
                                    taskMaterial.UnitLaboratoryId = researchCenter.ResearchcenterId;
                                }
                            }
                            foreach (var quantity in taskResearch.SelectedQuantity)
                            {
                                if (quantity.LaboratoryId == taskMaterial.LaboratoryTypeId)
                                {
                                    taskMaterial.Quantity = quantity.Quantity;
                                }
                            }
                        }
                    }
                }
                AppContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public OBKManagerResearchCenter GetManagerResearchCenter(Guid taskId)
        {
            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId);
            var units = AppContext.Units.Where(e => taskMaterials.GroupBy(q => q.UnitLaboratoryId).Select(s => s.Key).Contains(e.Id));

            OBKManagerResearchCenter manager = new OBKManagerResearchCenter();
            List<UnitLaboratories> unitLaboratorieses = new List<UnitLaboratories>();
            foreach (var unit in units)
            {
                UnitLaboratories labs = new UnitLaboratories
                {
                    Id = unit.Id,
                    UnitDisplayName = unit.DisplayName,
                    ExecutorLaboratory = new SelectList(GetEmployees(unit.Id), "Id", "DisplayName")
                };
                unitLaboratorieses.Add(labs);
            }
            manager.TaskId = taskId;
            manager.UnitLaboratory = unitLaboratorieses;
            return manager;
        }

        public void AcceptTaskReseachCenter(Guid taskId)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            if(task != null)
                task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskAcceptRC).Id;
            AppContext.SaveChanges();
        }

        public void SaveManagerLaboratory(OBKManagerResearchCenter managerResearchCenter)
        {
            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == managerResearchCenter.TaskId);

            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == managerResearchCenter.TaskId);
            if(task != null)
                task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskSendLab).Id;

            foreach (var taskMaterial in taskMaterials)
            {
                taskMaterial.StatusId = GetStageStatusByCode(OBK_Ref_StageStatus.New).Id;
            }
           
            List<OBK_TaskExecutor> taskExecutors = new List<OBK_TaskExecutor>();
            foreach (var manager in managerResearchCenter.ManagerLaboratory)
            {
                OBK_TaskExecutor executor = new OBK_TaskExecutor
                {
                    Id = Guid.NewGuid(),
                    TaskId = managerResearchCenter.TaskId,
                    ExecutorId = manager.ExecutorId,
                    StageId = CodeConstManager.STAGE_OBK_RESEARCH_CENTER,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                };
                taskExecutors.Add(executor);
            }

            AppContext.OBK_TaskExecutor.AddRange(taskExecutors);
            AppContext.SaveChanges();
        }

        public List<OBKResearchCenterListView> GetResearchCenterListView(Guid departamentId, Guid userId, string filterStatus)
        {
            var tasks = AppContext.OBK_Tasks.Where(
                e => e.OBK_TaskExecutor.Where(x => x.ExecutorId == userId).Any(d => d.TaskId == e.Id) && e
                         .OBK_TaskMaterial.Where(x => x.UnitLaboratoryId == departamentId && x.OBK_Ref_StageStatus.Code == filterStatus)
                         .Any(f => f.TaskId == e.Id));

            List<OBKResearchCenterListView> researchCenterListViews = new List<OBKResearchCenterListView>();
            foreach (var task in tasks)
            {
                OBKResearchCenterListView researchCenterListView = new OBKResearchCenterListView
                {
                    TaskId = task.Id,
                    UnitLaboratoryId = departamentId,
                    TaskNumber = task.TaskNumber,
                    RegisterDate = task.RegisterDate,
                    TaskEndDate = task.TaskEndDate.ToString(),
                    StageStatusCode = filterStatus//task.OBK_TaskMaterial.Select(e=>e.OBK_Ref_StageStatus.Code).FirstOrDefault()
                };
                researchCenterListViews.Add(researchCenterListView);
            }

            return researchCenterListViews;
        }


        public OBKTaskResearchCenter EditResearchCenter(Guid taskId, Guid unitLabId)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e=>e.Id == taskId);
            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId && e.UnitLaboratoryId == unitLabId);

            OBKTaskResearchCenter researchCenter = new OBKTaskResearchCenter
            {
                TaskId = task.Id,
                AssessmentDeclarationId = task.AssessmentDeclarationId,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName,
                UnitLaboratoryId = unitLabId
            };
            List<OBKTaskListResearchCenter> taskListResearchCenters = new List<OBKTaskListResearchCenter>();
            foreach (var taskMaterial in taskMaterials)
            {
                OBKTaskListResearchCenter taskListResearchCenter = new OBKTaskListResearchCenter
                {
                    Id = taskMaterial.Id,
                    ProductSeriesId = taskMaterial.ProductSeriesId,
                    ProductNameRu = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                    ProductNameKz = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                    Series = taskMaterial.OBK_Procunts_Series.Series,
                    SeriesMeasure = taskMaterial.OBK_Procunts_Series.sr_measures.name,
                    SeriesStartdate = taskMaterial.OBK_Procunts_Series.SeriesStartdate,
                    SeriesEndDate = taskMaterial.OBK_Procunts_Series.SeriesEndDate,
                    SeriesParty = taskMaterial.OBK_Procunts_Series.SeriesParty,
                    ActQuantity = taskMaterial.OBK_Procunts_Series.Quantity,
                    Quantity = taskMaterial.Quantity,
                    IdNumber = taskMaterial.IdNumber,
                    LaboratoryName = taskMaterial.OBK_Ref_LaboratoryType.NameRu,
                    ExecutorLaboratoryList = new SelectList(GetEmployees(unitLabId), "Id", "DisplayName")
                };
                taskListResearchCenters.Add(taskListResearchCenter);
            }
            researchCenter.TaskListResearchCenter = taskListResearchCenters;
            return researchCenter;
        }

        public void SendToExecutorReseachCenter(OBKTaskResearchCenter taskResearchCenter)
        {
            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskResearchCenter.TaskId);

            List<OBK_TaskExecutor> taskExecutors = new List<OBK_TaskExecutor>();
            foreach (var tl in taskResearchCenter.TaskListResearchCenter)
            {
                var taskExe = new OBK_TaskExecutor
                {
                    Id = Guid.NewGuid(),
                    TaskId = taskResearchCenter.TaskId,
                    TaskMaterialId = tl.Id,
                    ExecutorId = tl.LaboratoryAssistantId,
                    StageId = CodeConstManager.STAGE_OBK_RESEARCH_CENTER,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                };
                taskExecutors.Add(taskExe);
            }

            foreach (var taskMaterial in taskMaterials)
            {
                foreach (var selectedExecutor in taskResearchCenter.TaskListResearchCenter)
                {
                    if (selectedExecutor.Id == taskMaterial.Id)
                    {
                        taskMaterial.LaboratoryAssistantId = selectedExecutor.LaboratoryAssistantId;
                        taskMaterial.StatusId = GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
                    }
                }
            }
            AppContext.OBK_TaskExecutor.AddRange(taskExecutors);
            AppContext.SaveChanges();
        }

        public OBKTaskResearchCenter EditExpertResearchCenter(Guid taskId, Guid unitLabId)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            var taskMaterials =
                AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId && e.UnitLaboratoryId == unitLabId &&
                                                       e.LaboratoryAssistantId == userId);
            OBKTaskResearchCenter researchCenter = new OBKTaskResearchCenter
            {
                TaskId = task.Id,
                AssessmentDeclarationId = task.AssessmentDeclarationId,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName,
                UnitLaboratoryId = unitLabId
            };
            List<OBKTaskListResearchCenter> taskListResearchCenters = new List<OBKTaskListResearchCenter>();
            foreach (var taskMaterial in taskMaterials)
            {
                OBKTaskListResearchCenter taskListResearchCenter = new OBKTaskListResearchCenter
                {
                    Id = taskMaterial.Id,
                    ProductSeriesId = taskMaterial.ProductSeriesId,
                    ProductNameRu = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                    ProductNameKz = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                    Series = taskMaterial.OBK_Procunts_Series.Series,
                    IdNumber = taskMaterial.IdNumber,
                    LaboratoryName = taskMaterial.OBK_Ref_LaboratoryType.NameRu,
                    ExecutorLaboratoryName = taskMaterial.Employee.DisplayName,
                    //ExecutorLaboratorySign = taskMaterial.OBK_TaskExecutor.FirstOrDefault(e=>e.TaskMaterialId == taskMaterial.Id)?.SignedData != null,
                    CreateBtnValid = taskMaterial.ExpertiseResult != null
                };
                taskListResearchCenters.Add(taskListResearchCenter);
            }
            researchCenter.TaskListResearchCenter = taskListResearchCenters;
            return researchCenter;
        }

        public OBKSubTaskResult SubTaskResult(Guid taskMaterialId, bool isNew)
        {
            var taskMaterial = AppContext.OBK_TaskMaterial.FirstOrDefault(e => e.Id == taskMaterialId);
            if (taskMaterial != null)
            {
                if (isNew)
                {
                    OBKSubTaskResult subTaskResult = new OBKSubTaskResult();
                    subTaskResult.Id = taskMaterial.Id;
                    subTaskResult.IsNew = isNew;
                    subTaskResult.ProductNameRu = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu;
                    subTaskResult.ProductNameKz = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz;
                    subTaskResult.NdProduct = taskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " +
                                              taskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdNumber;
                    subTaskResult.SubTaskNumber = taskMaterial.OBK_Tasks.TaskNumber + "/" + SubTaskCount(taskMaterialId);
                    return subTaskResult;
                }
                else
                {
                    OBKSubTaskResult str = new OBKSubTaskResult
                    {
                        Id = taskMaterial.Id,
                        IsNew = isNew,
                        ProductNameRu = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                        ProductNameKz = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                        NdProduct = taskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " +
                                    taskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdNumber,
                        Regulation = taskMaterial.Regulation,
                        ExpertiseResult = (bool) taskMaterial.ExpertiseResult,
                        SubTaskNumber = taskMaterial.SubTaskNumber,
                        SubTaskIndicator = taskMaterial.OBK_ResearchCenterResult.Select(e => new SubTaskIndicator
                            {
                                LaboratoryMarkNameRu = e.OBK_Ref_LaboratoryMark.NameRu,
                                LaboratoryMarkNameKz = e.OBK_Ref_LaboratoryMark.NameKz,
                                Claim = e.Claim,
                                FactResult = e.FactResult,
                                Humidity = e.Humidity,
                                LaboratoryRegulationNameRu = e.OBK_Ref_LaboratoryRegulation.NameRu,
                                LaboratoryRegulationNameKz = e.OBK_Ref_LaboratoryRegulation.NameKz
                        }).ToList()
                    };
                    return str;
                }
            }
            return null;
        }

        private int SubTaskCount(Guid taskMaterialId)
        {
            var tm = AppContext.OBK_TaskMaterial.FirstOrDefault(e => e.Id == taskMaterialId);
            var tmCount = AppContext.OBK_TaskMaterial.Count(e => e.TaskId == tm.TaskId && e.SubTaskNumber != null) + 1;
            return tmCount;
        }

        public bool SaveSubTaskResult(OBKSubTaskResult subTaskResult)
        {
            try
            {
                var taskMaterial = AppContext.OBK_TaskMaterial.FirstOrDefault(e => e.Id == subTaskResult.Id);
                if (taskMaterial != null)
                {
                    taskMaterial.ExpertiseResult = subTaskResult.ExpertiseResult;
                    taskMaterial.Regulation = subTaskResult.Regulation;
                    taskMaterial.SubTaskNumber = subTaskResult.SubTaskNumber;
                    taskMaterial.CreatedDate = DateTime.Now;
                    List<OBK_ResearchCenterResult> rcs = new List<OBK_ResearchCenterResult>();
                    foreach (var st in subTaskResult.SubTaskIndicator)
                    {
                        OBK_ResearchCenterResult rc = new OBK_ResearchCenterResult
                        {
                            Id = Guid.NewGuid(),
                            TaskMaterialId = taskMaterial.Id,
                            Claim = st.Claim,
                            FactResult = st.FactResult,
                            Humidity = st.Humidity,
                            LaboratoryMarkId = st.LaboratoryMarkId,
                            LaboratoryRegulationId = st.RegulationId
                        };
                        rcs.Add(rc);
                    }
                    AppContext.OBK_ResearchCenterResult.AddRange(rcs);
                    AppContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetSubTaskSignDataExpert(Guid id)
        {
            var reslutTaskMaterial = AppContext.OBK_TaskMaterial.FirstOrDefault(e=>e.Id == id);
            if (reslutTaskMaterial == null)
                return null;
            OBKSubTaskResult str = new OBKSubTaskResult
            {
                Id = reslutTaskMaterial.Id,
                ProductNameRu = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                ProductNameKz = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                NdProduct = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " +
                            reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdNumber,
                ExpertiseResult = (bool)reslutTaskMaterial.ExpertiseResult,
                SubTaskIndicator = reslutTaskMaterial.OBK_ResearchCenterResult.Select(e=> new SubTaskIndicator
                {
                    LaboratoryMarkId = e.LaboratoryMarkId,
                    Claim = e.Claim,
                    FactResult = e.FactResult,
                    Humidity = e.Humidity
                }).ToList()
            };
            var xmlData = SerializeHelper.SerializeDataContract(str);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public string GetTaskSignDataChief(Guid taskId)
        {
            var departamentId = UserHelper.GetDepartment().Id;
            var reslutTaskMaterials =
                AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId && e.UnitLaboratoryId == departamentId);
            if (!reslutTaskMaterials.Any())
                return null;

            List<OBKSubTaskResult> strs = new List<OBKSubTaskResult>();
            foreach (var reslutTaskMaterial in reslutTaskMaterials)
            {
                OBKSubTaskResult str = new OBKSubTaskResult
                {
                    Id = reslutTaskMaterial.Id,
                    ProductNameRu = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                    ProductNameKz = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                    NdProduct = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " +
                                reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdNumber,
                    ExpertiseResult = (bool)reslutTaskMaterial.ExpertiseResult,
                    SubTaskIndicator = reslutTaskMaterial.OBK_ResearchCenterResult.Select(e => new SubTaskIndicator
                    {
                        LaboratoryMarkId = e.LaboratoryMarkId,
                        Claim = e.Claim,
                        FactResult = e.FactResult,
                        Humidity = e.Humidity
                    }).ToList()
                };
                strs.Add(str);
            }
            var xmlData = SerializeHelper.SerializeDataContract(strs);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public bool SaveSignedSubTaskExpert(Guid id, string signedData)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var taskExecutor = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.TaskMaterialId == id && e.ExecutorId == userId && e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR);
            if (taskExecutor != null)
            {
                taskExecutor.SignedData = signedData;
                AppContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool SaveSignedTaskChief(Guid taskId, string signedData)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var taskExecutor = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == taskId && e.ExecutorId == userId ); //&& e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
            if (taskExecutor != null)
            {
                taskExecutor.SignedData = signedData;

                var departamentId = UserHelper.GetDepartment().Id;
                var taskMaterials =
                    AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId && e.UnitLaboratoryId == departamentId);
                foreach (var taskMaterial in taskMaterials)
                {
                    taskMaterial.StatusId = GetStageStatusByCode(OBK_Ref_StageStatus.Completed).Id;
                }
                AppContext.SaveChanges();
                return true;
            }
            return false;
        }

        public OBKTaskResearchCenter EditChiefResearchCenter(Guid taskId, Guid unitLabId)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            var taskMaterials =
                AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId && e.UnitLaboratoryId == unitLabId);
            OBKTaskResearchCenter researchCenter = new OBKTaskResearchCenter
            {
                TaskId = task.Id,
                AssessmentDeclarationId = task.AssessmentDeclarationId,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName,
                UnitLaboratoryId = unitLabId
            };
            List<OBKTaskListResearchCenter> taskListResearchCenters = new List<OBKTaskListResearchCenter>();
            foreach (var taskMaterial in taskMaterials)
            {
                OBKTaskListResearchCenter taskListResearchCenter = new OBKTaskListResearchCenter
                {
                    Id = taskMaterial.Id,
                    ProductSeriesId = taskMaterial.ProductSeriesId,
                    ProductNameRu = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                    ProductNameKz = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                    Series = taskMaterial.OBK_Procunts_Series.Series,
                    IdNumber = taskMaterial.IdNumber,
                    LaboratoryName = taskMaterial.OBK_Ref_LaboratoryType.NameRu,
                    ExecutorLaboratoryName = taskMaterial.Employee.DisplayName,
                    ExecutorLaboratorySign = taskMaterial.OBK_TaskExecutor.FirstOrDefault(e=>e.TaskMaterialId == taskMaterial.Id)?.SignedData != null,
                    SubTaskResult = taskMaterial.OBK_ResearchCenterResult.Select(e => new SubTaskIndicator
                    {
                        LaboratoryMarkNameRu = e.OBK_Ref_LaboratoryMark.NameRu,
                        LaboratoryMarkNameKz = e.OBK_Ref_LaboratoryMark.NameKz,
                        LaboratoryRegulationNameRu = e.OBK_Ref_LaboratoryRegulation.NameRu,
                        LaboratoryRegulationNameKz = e.OBK_Ref_LaboratoryRegulation.NameKz,
                        Claim = e.Claim,
                        FactResult = e.FactResult,
                        Humidity = e.Humidity
                    }).ToList()
                };
                taskListResearchCenters.Add(taskListResearchCenter);
            }
            researchCenter.TaskListResearchCenter = taskListResearchCenters;
            return researchCenter;
        }

        public bool SendToChief(Guid taskId)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var taskExecutors = AppContext.OBK_TaskExecutor.Where(e => e.ExecutorId == userId && e.TaskId == taskId);
            foreach (var taskExecutor in taskExecutors)
            {
                if (taskExecutor.SignedData == null)
                {
                    return false;
                }
            }
            var taskMaterials =
                AppContext.OBK_TaskMaterial.Where(e => e.LaboratoryAssistantId == userId && e.TaskId == taskId);
            foreach (var taskMaterial in taskMaterials)
            {
                taskMaterial.StatusId = GetStageStatusByCode(OBK_Ref_StageStatus.RequiresSigning).Id;
            }
            AppContext.SaveChanges();
            return true;
        }


        public object GetTaskReportData(Guid taskId)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            if (task == null)
                return null;

            var tms = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId);
            var empCoz = task.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == taskId && e.StageId == 1);
            var empIc = task.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == taskId && e.StageId == 7);
            var unitAddress =
                AppContext.UnitsAddresses.FirstOrDefault(e => e.UnitsId == task.UnitId && (bool) !e.IsDeleted);
            var obj = new
            {
                UnitNameRu = task.Unit.Name,
                UnitNameKz = task.Unit.NameKz,
                UnitAddressNameRu = unitAddress?.AddressNameRu,
                UnitAddressNameKz = unitAddress?.AddressNameKz,

                CozExecutorName = empCoz?.Employee.ShortName,
                CozExecutorPost = empCoz?.Employee.Position.Name,
                ICExecutorName = empIc?.Employee.ShortName,
                ICExecutorPost = empIc?.Employee.Position.Name,
                Products = GetProductReportData(tms)
            };
            return obj;
        }

        private object GetProductReportData(IQueryable<OBK_TaskMaterial> tms)
        {
            var pss = AppContext.OBK_Procunts_Series.Where(e => tms.GroupBy(q => q.ProductSeriesId).Select(s => s.Key).Contains(e.Id)).ToList();
            var obj = pss.Select(ps => new
            {
                ps.OBK_RS_Products.NameRu,
                ps.OBK_RS_Products.NameKz,
                ps.Series,
                ps.Quantity,
                ND = ps.OBK_RS_Products.NdName + " " + ps.OBK_RS_Products.NdNumber,
                LaboratoryName = string.Join(", ", ps.OBK_TaskMaterial.Where(e=>e.ProductSeriesId == ps.Id).Select(x=>x.OBK_Ref_LaboratoryType.NameRu))
            });
            return obj;
        }
    }
}
