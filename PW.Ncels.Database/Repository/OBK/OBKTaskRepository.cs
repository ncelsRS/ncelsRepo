using System;
using System.Collections.Generic;
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

        public List<Employee> GetExecutors(string code)
        {
            var userOrgId = UserHelper.GetCurrentEmployee().OrganizationId;
            var unit = AppContext.Units.FirstOrDefault(e => e.Code == code && e.ParentId == userOrgId);
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
            AppContext.SaveChanges();

            OBK_TaskExecutor taskExecutor = new OBK_TaskExecutor
            {
                Id = Guid.NewGuid(),
                ExecutorId = UserHelper.GetCurrentEmployee().Id,
                TaskId = obkTasks.Id,
                StageId = 3
            };
            AppContext.OBK_TaskExecutor.Add(taskExecutor);
            AppContext.SaveChanges();

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
                        OBK_TaskExecutor taskExecutor = new OBK_TaskExecutor
                        {
                            Id = Guid.NewGuid(),
                            ExecutorId = executorId,
                            TaskId = task.Id,
                            StageId = CodeConstManager.STAGE_OBK_COZ
                        };
                        taskExecutors.Add(taskExecutor);
                    }
                    break;
                case "researchcenter":
                    var tasks1 = AppContext.OBK_Tasks.Where(e => e.Id == id);
                    foreach (var task in tasks1)
                    {
                        OBK_TaskExecutor taskExecutor = new OBK_TaskExecutor
                        {
                            Id = Guid.NewGuid(),
                            ExecutorId = executorId,
                            TaskId = task.Id,
                            StageId = CodeConstManager.STAGE_OBK_ICL
                        };
                        taskExecutors.Add(taskExecutor);
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
          
            OBKTaskResearchCenter researchCenter = new OBKTaskResearchCenter
            {
                TaskId = task.Id,
                AssessmentDeclarationId = task.AssessmentDeclarationId,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName
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

                    //DimensionIMN = productSeries.OBK_TaskMaterial.FirstOrDefault(e=>e.ProductSeriesId == productSeries.Id)?.DimensionIMN,
                    //IdNumber = productSeries.OBK_TaskMaterial.FirstOrDefault(e => e.ProductSeriesId == productSeries.Id)?.IdNumber,

                    //StorageCondition = (Guid) productSeries.OBK_TaskMaterial.FirstOrDefault(e => e.ProductSeriesId == productSeries.Id)?.StorageConditionId,
                    //ExternalCondition = (Guid)productSeries.OBK_TaskMaterial.FirstOrDefault(e => e.ProductSeriesId == productSeries.Id)?.ExternalConditionId,

                    Laboratory = productSeries.OBK_TaskMaterial.Select(e=> new OBKLaboratory
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

        public void AcceptTaskList(OBKTaskListViewModel taskListViewModel)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskListViewModel.TaskId);
            if (task != null)
            {
                task.CozExecutorId = UserHelper.GetCurrentEmployee().Id;
                task.SendToIC = DateTime.Now;
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

        public void SaveManagerLaboratory(OBKManagerResearchCenter managerResearchCenter)
        {
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
    }
}
