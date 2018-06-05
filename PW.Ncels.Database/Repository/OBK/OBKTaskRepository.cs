using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Aspose.Pdf.InteractiveFeatures.Forms;
using Aspose.Words;
using Aspose.Words.Drawing;
using Kendo.Mvc.Extensions;
using Ncels.Helpers;
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

        public IQueryable<Unit> GetUnits(List<string> code, Guid unitId)
        {
            var units = AppContext.Units.Where(e => code.Contains(e.Code) && e.ParentId == unitId);
            if (!units.Any())
                units = AppContext.Units.Where(e => code.Contains(e.Code) && e.Parent.ParentId == unitId);
            return units;
        }

        public IQueryable<Unit> GetUnits(List<string> code)
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

        public IQueryable<OBKTaskListRegisterView> GetTaskList(Guid userId) //Guid organizationId, 
        {
            return AppContext.OBKTaskListRegisterViews.Where(e => e.ExecutorId == userId && e.StatusNameRu != "Передано в лабораторию" && e.StatusNameRu != "Образцы приняты ЦОЗ").OrderByDescending(e => e.RegDate); // e.UnitId == organizationId &&
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

        /*public IQueryable<OBK_Ref_LaboratoryRegulation> GetRegulation(Guid id)
        {
            return AppContext.OBK_Ref_LaboratoryRegulation.Where(e => !e.IsDeleted && e.LaboratoryMarkId == id);
        }*/
        public IQueryable<OBK_Ref_LaboratoryRegulation_Mark> GetRegulation(Guid id)
        {
            var t = AppContext.OBK_Ref_LaboratoryRegulation_Mark.Where(e => e.laboratoryMark_id == id);
            return t;
        }

        public IQueryable<OBK_Tasks> GetsearchTask(Guid id)
        {
            var t = AppContext.OBK_Tasks.Where(e => e.Id == id);
            return t;
        }

        public IQueryable<OBK_Ref_LaboratoryRegulation> GetRegulationName(IQueryable<OBK_Ref_LaboratoryRegulation_Mark> regulation_mark)
        {
            List<OBK_Ref_LaboratoryRegulation> list = new List<OBK_Ref_LaboratoryRegulation>();

            foreach (var reg in regulation_mark)
            {
                list.Add(AppContext.OBK_Ref_LaboratoryRegulation.Where(r => r.Id == reg.laboratoryRegulation_id).FirstOrDefault());
            }

            return list.AsQueryable();
        }

        public IQueryable<OBK_Ref_LaboratoryRegulation> GetRegulation()
        {
            return AppContext.OBK_Ref_LaboratoryRegulation.Where(e => !e.IsDeleted);
        }
        public IQueryable<OBK_Tasks> GetSearchTaskId()
        {
            var rs = AppContext.OBK_ResearchCenterResult.Select(r => r.OBK_TaskMaterial);
            List<Guid> tasksId = new List<Guid>();

            foreach (var r in rs)
            {
                tasksId.Add(r.TaskId);
            }

            List<OBK_Tasks> tasks = new List<OBK_Tasks>();
            foreach (var taskId in tasksId)
            {
                if (!tasks.Contains(AppContext.OBK_Tasks.Where(r => r.Id == taskId).FirstOrDefault()))
                    tasks.Add(AppContext.OBK_Tasks.Where(r => r.Id == taskId).FirstOrDefault());
            }

            return tasks.AsQueryable();
        }

        /*public IQueryable<string> GetSearchTaskId()
        {
            var rs = AppContext.OBK_ResearchCenterResult.Select(r => r.OBK_TaskMaterial);
            List<Guid> tasksId = new List<Guid>();

            foreach (var r in rs)
            {
                tasksId.Add(r.TaskId);
            }

            List<string> tasks = new List<string>();
            foreach (var taskId in tasksId)
            {
                if (!tasks.Contains(AppContext.OBK_Tasks.Where(r => r.Id == taskId).FirstOrDefault().TaskNumber))
                    tasks.Add(AppContext.OBK_Tasks.Where(r => r.Id == taskId).FirstOrDefault().TaskNumber);
            }

            return tasks.AsQueryable();
        }*/

        public IQueryable<OBK_RS_Products> GetSearchProductId()
        {
            var rs = AppContext.OBK_ResearchCenterResult.Select(r => r.OBK_TaskMaterial);
            List<int> productIds = new List<int>();

            foreach (var r in rs)
            {
                productIds.Add(r.ProductSeriesId);
            }

            List<OBK_Procunts_Series> procount_series = new List<OBK_Procunts_Series>();
            foreach (var productId in productIds)
            {
                if (!procount_series.Contains(AppContext.OBK_Procunts_Series.Where(r => r.Id == productId).FirstOrDefault()))
                    procount_series.Add(AppContext.OBK_Procunts_Series.Where(r => r.Id == productId).FirstOrDefault());
            }

            List<OBK_RS_Products> rs_products = new List<OBK_RS_Products>();
            foreach (var procount_serie in procount_series)
            {
                if(!rs_products.Contains(AppContext.OBK_RS_Products.Where(r => r.Id == procount_serie.OBK_RS_ProductsId).FirstOrDefault()))
                rs_products.Add(AppContext.OBK_RS_Products.Where(r=>r.Id == procount_serie.OBK_RS_ProductsId).FirstOrDefault());
            }

                return rs_products.AsQueryable();
        }
        /*public IQueryable<string> GetSearchProductId()
        {
            var rs = AppContext.OBK_ResearchCenterResult.Select(r => r.OBK_TaskMaterial);
            List<int> ps_ids = new List<int>();

            foreach (var r in rs)
            {
                ps_ids.Add(r.ProductSeriesId);
            }

            List<OBK_Procunts_Series> pr_series = new List<OBK_Procunts_Series>();
            List<string> rs_products = new List<string>();

            foreach (var ps_id in ps_ids)
            {
                if (!pr_series.Contains(AppContext.OBK_Procunts_Series.Where(r => r.Id == ps_id).FirstOrDefault()))
                    pr_series.Add(AppContext.OBK_Procunts_Series.Where(r => r.Id == ps_id).FirstOrDefault());
            }

            foreach (var pr_serie in pr_series)
            {
                if(!rs_products.Contains(AppContext.OBK_RS_Products.Where(r => r.Id == pr_serie.OBK_RS_ProductsId).FirstOrDefault().NameRu))
                rs_products.Add(AppContext.OBK_RS_Products.Where(r => r.Id == pr_serie.OBK_RS_ProductsId).FirstOrDefault().NameRu);
            }

            return rs_products.AsQueryable();
        }*/
         public IQueryable<OBK_Ref_LaboratoryType> GetSearchLaboratoryTypeId()
         {
             var rs = AppContext.OBK_ResearchCenterResult.Select(r => r.OBK_TaskMaterial);
             List<Guid> lab_typeIds = new List<Guid>();

             foreach (var r in rs)
             {
                 lab_typeIds.Add(r.LaboratoryTypeId);
             }

             List<OBK_Ref_LaboratoryType> lab_type = new List<OBK_Ref_LaboratoryType>();
             foreach(var lab_typeId in lab_typeIds)
             {
                 if(!lab_type.Contains(AppContext.OBK_Ref_LaboratoryType.Where(r => r.Id == lab_typeId).FirstOrDefault()))
                 lab_type.Add(AppContext.OBK_Ref_LaboratoryType.Where(r => r.Id == lab_typeId).FirstOrDefault());
             }

             return lab_type.AsQueryable();
         }

        /*public IQueryable<string> GetSearchLaboratoryTypeId()
        {
            var rs = AppContext.OBK_ResearchCenterResult.Select(r => r.OBK_TaskMaterial);
            List<Guid> lab_typeIds = new List<Guid>();

            foreach (var r in rs)
            {
                lab_typeIds.Add(r.LaboratoryTypeId);
            }

            List<string> lab_type = new List<string>();
            foreach (var lab_typeId in lab_typeIds)
            {
                if (!lab_type.Contains(AppContext.OBK_Ref_LaboratoryType.Where(r => r.Id == lab_typeId).FirstOrDefault().NameRu))
                    lab_type.Add(AppContext.OBK_Ref_LaboratoryType.Where(r => r.Id == lab_typeId).FirstOrDefault().NameRu);
            }

            return lab_type.AsQueryable();
        }*/
        public IQueryable<string> GetSearchPRId()
        {
            var rs = AppContext.OBK_ResearchCenterResult.Select(r => r.OBK_TaskMaterial);
            List<string> subNumberList = new List<string>();

            foreach (var r in rs)
            {
                if(r.SubTaskNumber != null && !subNumberList.Contains(r.SubTaskNumber))
                subNumberList.Add(r.SubTaskNumber);
            }
            return subNumberList.AsQueryable();
        }
        public List<Employee> GetExecutors(string code)
        {
            var userOrgId = UserHelper.GetCurrentEmployee().OrganizationId;
            var unit = AppContext.Units.FirstOrDefault(e => e.Code == code && e.ParentId == userOrgId) ?? AppContext.Units.FirstOrDefault(e => e.Code == code && e.Parent.ParentId == userOrgId);
            var units = AppContext.Units.Where(e => e.ParentId == unit.Id);
            var employee = AppContext.Employees.Where(e => units.Any(x => x.EmployeeId == e.Id)).ToList();
            if (employee.Any()) return employee;
            if (unit?.BossId != null)
            {
                employee = AppContext.Employees.Where(e => unit.BossId == e.Id.ToString()).ToList();
            }
            return employee;
        }

        public List<Employee> GetExecutors(string code, Guid id)
        {
            //var userOrgId = UserHelper.GetCurrentEmployee().OrganizationId;
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == id);
            //var unit = AppContext.Units.FirstOrDefault(e => e.Code == code && e.ParentId == userOrgId) ?? AppContext.Units.FirstOrDefault(e => e.Code == code && e.Parent.ParentId == userOrgId);
            var unit = AppContext.Units.FirstOrDefault(e => e.Code == code && e.ParentId == task.UnitId) ?? AppContext.Units.FirstOrDefault(e => e.Code == code && e.Parent.ParentId == task.UnitId);
            var units = AppContext.Units.Where(e => e.ParentId == unit.Id);
            var employee = AppContext.Employees.Where(e => units.Any(x => x.EmployeeId == e.Id)).ToList();
            if (employee.Any()) return employee;
            if (unit?.BossId != null)
            {
                employee = AppContext.Employees.Where(e => unit.BossId == e.Id.ToString()).ToList();
            }
            return employee;
        }

        /// <summary>
        /// Отправка в ЦОЗ по ТФ
        /// </summary>
        /// <param name="adId"></param>
        public List<UnitLaboratories> GetFilialExecutors(Guid adId)
        {
            var uIds = AppContext.OBK_Tasks.Where(e => e.AssessmentDeclarationId == adId).GroupBy(x => x.UnitId);
            var uls = new List<UnitLaboratories>();
            foreach (var uId in uIds)
            {
                var unit = AppContext.Units.FirstOrDefault(e => e.ParentId == uId.Key && e.Code == OrganizationConsts.CozDepartament) ??
                           AppContext.Units.FirstOrDefault(e => e.Parent.ParentId == uId.Key && e.Code == OrganizationConsts.CozDepartament);
                UnitLaboratories ul;
                if (unit == null)
                {
                    unit = AppContext.Units.FirstOrDefault(e => e.Id == uId.Key);
                    ul = new UnitLaboratories
                    {
                        Id = (Guid)unit?.Parent.Id,
                        UnitDisplayName = unit?.Parent.DisplayName,
                        ExecutorLaboratory = new SelectList(GetEmployees((Guid)unit?.Id), "Id", "DisplayName")
                    };
                    uls.Add(ul);
                    continue;
                }
                ul = new UnitLaboratories
                {
                    Id = unit.Parent.Id,
                    UnitDisplayName = unit.Parent.DisplayName,
                    ExecutorLaboratory = new SelectList(GetEmployees(unit.Id), "Id", "DisplayName")
                };
                uls.Add(ul);
            }
            return uls;
        }

        public void SaveTask(OBKCreateTaskViewModel model)
        {
            var obkTasks = new OBK_Tasks
            {
                Id = Guid.NewGuid(),
                ActReceptionId = model.ActReceptionId,
                RegisterDate = model.RegisterDate,
                TaskNumber = model.TaskNumber,
                AssessmentDeclarationId = model.AssessmentDeclarationId,
                UnitId = model.UnitId
            };
            AppContext.OBK_Tasks.Add(obkTasks);



            var taskExecutor = new OBK_TaskExecutor
            {
                Id = Guid.NewGuid(),
                ExecutorId = UserHelper.GetCurrentEmployee().Id,
                TaskId = obkTasks.Id,
                StageId = 3
            };
            AppContext.OBK_TaskExecutor.Add(taskExecutor);

            var taskLists = new List<OBK_TaskMaterial>();
            foreach (var modal in model.ModalViewModels)
            {
                taskLists.AddRange(modal.LaboratoryTypeIds.Select(laboratory => new OBK_TaskMaterial
                {
                    Id = Guid.NewGuid(),
                    LaboratoryTypeId = Guid.Parse(laboratory),
                    TaskId = obkTasks.Id,
                    ProductSeriesId = modal.Id
                }));
            }
            AppContext.OBK_TaskMaterial.AddRange(taskLists);

            AppContext.SaveChanges();
        }

        public string GetSignTask(Guid id)
        {
            var signDatas = AppContext.OBK_Tasks.Where(e => e.AssessmentDeclarationId == id);
            var tasks = new List<OBK_Tasks>();
            foreach (var signData in signDatas)
            {
                var task = new OBK_Tasks
                {
                    Id = signData.Id,
                    TaskNumber = signData.TaskNumber,
                    AssessmentDeclarationId = signData.AssessmentDeclarationId,
                    UnitId = signData.UnitId,
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
            var taskExecutors = new List<OBK_TaskExecutor>();
            switch (code)
            {
                case "coz":
                    var tasks = AppContext.OBK_Tasks.Where(e => e.AssessmentDeclarationId == id);
                    foreach (var task in tasks)
                    {
                        if (task.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == task.Id && e.ExecutorId == executorId && e.StageId == CodeConstManager.STAGE_OBK_COZ) == null)
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
                    var userId = UserHelper.GetCurrentEmployee().Id;
                    var stage =
                        AppContext.OBK_AssessmentStage.FirstOrDefault(
                            e => e.DeclarationId == id && e.StageId == CodeConstManager.STAGE_OBK_EXPERTISE_DOC && e.OBK_AssessmentStageExecutors.Any(x => x.ExecutorId == userId));
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

                            task.ISLExecutorId = taskExecutor.ExecutorId;

                            taskExecutors.Add(taskExecutor);
                            task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskSendRC).Id;
                        }
                        else
                        {
                            var ex = AppContext.OBK_TaskExecutor.Where(e => e.TaskId == task.Id && e.ExecutorId == executorId && e.StageId == CodeConstManager.STAGE_OBK_ICL).FirstOrDefault();
                            var emp = AppContext.Employees.Where(r => r.Id == ex.ExecutorId).FirstOrDefault();

                            task.ISLExecutorId = emp.Id;

                        }
                    }
                    break;

            }
            AppContext.OBK_TaskExecutor.AddRange(taskExecutors);
            AppContext.SaveChanges();
        }

        public void SendToCoz(OBKManagerResearchCenter filialExecutors)
        {
            var taskExecutors = new List<OBK_TaskExecutor>();
            var tasks = AppContext.OBK_Tasks.Where(e => e.AssessmentDeclarationId == filialExecutors.AssessmentDeclarationId);
            foreach (var task in tasks)
            {
                foreach (var filialExecutor in filialExecutors.ManagerLaboratory)
                {
                    if (task.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == task.Id && e.ExecutorId == filialExecutor.ExecutorId && e.StageId == CodeConstManager.STAGE_OBK_COZ && e.OBK_Tasks.UnitId == filialExecutor.UnitLaboratoryId) != null) continue;
                    if (task.UnitId != filialExecutor.UnitLaboratoryId) continue;
                    var taskExecutor = new OBK_TaskExecutor
                    {
                        Id = Guid.NewGuid(),
                        ExecutorId = filialExecutor.ExecutorId,
                        TaskId = task.Id,
                        StageId = CodeConstManager.STAGE_OBK_COZ
                    };
                    taskExecutors.Add(taskExecutor);
                    task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskNew).Id;
                    new SafetyAssessmentRepository().AddHistory(filialExecutors.AssessmentDeclarationId, OBK_Ref_StageStatus.InWork, taskExecutor.ExecutorId);
                }
            }
            var userId = UserHelper.GetCurrentEmployee().Id;
            var stage = AppContext.OBK_AssessmentStage.FirstOrDefault(
                e => e.DeclarationId == filialExecutors.AssessmentDeclarationId && e.StageId == CodeConstManager.STAGE_OBK_EXPERTISE_DOC &&
                     e.OBK_AssessmentStageExecutors.Any(x => x.ExecutorId == userId));
            stage.StageStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.RequiresConclusion).Id;
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

            var taskViewModel = new OBKTaskListViewModel
            {
                TaskId = task.Id,
                CozExecutorName = AppContext.Employees.FirstOrDefault(e => e.Id == task.CozExecutorId)?.ShortName,
                SentToIC = task.SendToIC != null ? task.SendToIC.ToString() : null,
                CheckBoxIsVisible = task.OBK_AssessmentDeclaration.OBKApplicantParty ?? false,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ExecutorName = task.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == task.Id && e.StageId == CodeConstManager.STAGE_OBK_UOBK)?.Employee.ShortName,
                ICExecutorName = task.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == task.Id && e.StageId == CodeConstManager.STAGE_OBK_ICL)?.Employee.ShortName,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName
            };

            var productSeriesListViewModels = new List<OBKProductViewModel>();
            foreach (var productSeries in productSerieses)
            {
                var productSeriesList = new OBKProductViewModel
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
            //string[] codeResearchCenter = { "researchcenter3", "researchcenter4", "researchcenter5", "researchcenter6", "researchcenter7" };

            if (task.TaskStatusId == GetStageStatusByCode(OBK_Ref_StageStatus.TaskSendLab).Id)
            {
                var researchCenter = new OBKTaskResearchCenter
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

                var taskListResearchCenters = new List<OBKTaskListResearchCenter>();
                foreach (var productSeries in productSerieses)
                {
                    var productSeriesList = new OBKTaskListResearchCenter
                    {
                        ProductSeriesId = productSeries.Id,
                        RegisterId = productSeries.OBK_RS_Products.RegisterId,
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
                        StorageConditionNameRu = productSeries.OBK_TaskMaterial.First(e => e.ProductSeriesId == productSeries.Id && e.TaskId == task.Id).OBK_Ref_MaterialCondition.NameRu,
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
                var researchCenter = new OBKTaskResearchCenter
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
                    Researchcenters = new SelectList(GetUnits(OrganizationConsts.Researchcenters, task.UnitId), "Id", "Name"),
                    IsShow = false
                };

                var taskListResearchCenters = new List<OBKTaskListResearchCenter>();
                foreach (var productSeries in productSerieses)
                {
                    var productSeriesList = new OBKTaskListResearchCenter
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
            if (task == null) return;
            task.CozExecutorId = UserHelper.GetCurrentEmployee().Id;
            task.SendToIC = DateTime.Now;
            task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskAcceptCoz).Id;

            var executor = AppContext.Employees.FirstOrDefault(r => r.Id == task.CozExecutorId);

            new SafetyAssessmentRepository().AddHistory(task.AssessmentDeclarationId, OBK_Ref_StageStatus.TaskAcceptCoz, executor.Id);

            AppContext.SaveChanges();
        }

        public bool SendToReseachCenter(OBKTaskResearchCenter taskResearchCenter)
        {
            try
            {
                var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskResearchCenter.TaskId);
                var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == task.Id);
                var counter = 0;
                foreach (var taskMaterial in taskMaterials)
                {
                    foreach (var taskResearch in taskResearchCenter.TaskListResearchCenter)
                    {
                        if (taskResearch.ProductSeriesId != taskMaterial.ProductSeriesId) continue;
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
                // new SafetyAssessmentRepository().AddHistory(task.AssessmentDeclarationId, OBK_Ref_StageStatus.TaskSendLab, );

                AppContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void GenerateSubTaskNumber(Guid taskId)
        {
            var tms = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId).GroupBy(r => r.ProductSeriesId);
            int count = 0;
            foreach (var psId in tms.Select(e => e.Key))
            {
                var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.ProductSeriesId == psId);
                ++count;
                foreach (var taskMaterial in taskMaterials)
                {
                    taskMaterial.SubTaskNumber = taskMaterial.OBK_Tasks.TaskNumber + "/" + count;
                }
            }
            AppContext.SaveChanges();
        }

        public OBKManagerResearchCenter GetManagerResearchCenter(Guid taskId)
        {
            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId);
            var units = AppContext.Units.Where(e => taskMaterials.GroupBy(q => q.UnitLaboratoryId).Select(s => s.Key).Contains(e.Id));

            var manager = new OBKManagerResearchCenter();
            var unitLaboratorieses = new List<UnitLaboratories>();
            foreach (var unit in units)
            {
                var labs = new UnitLaboratories
                {
                    Id = unit.Id,
                    UnitDisplayName = unit.DisplayName,
                    ExecutorLaboratory = new SelectList(GetEmployees(unit.Id), "Id", "DisplayName")
                };
                unitLaboratorieses.Add(labs);
            }
            manager.TaskId = taskId;
            manager.UnitLaboratory = unitLaboratorieses;
            manager.BossSelectList = new SelectList(GetExecutors("researchcenter-01", taskId), "Id", "DisplayName");
            return manager;
        }

        public void AcceptTaskReseachCenter(Guid taskId)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);

            if (task != null)
                task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskAcceptRC).Id;

            new SafetyAssessmentRepository().AddHistory(task.AssessmentDeclarationId, OBK_Ref_StageStatus.TaskAcceptRC, UserHelper.GetCurrentEmployee().Id);

            AppContext.SaveChanges();
        }

        public void SaveManagerLaboratory(OBKManagerResearchCenter managerResearchCenter)
        {
            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == managerResearchCenter.TaskId);

            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == managerResearchCenter.TaskId);
            if (task != null)
                task.TaskStatusId = GetStageStatusByCode(OBK_Ref_StageStatus.TaskSendLab).Id;

            foreach (var taskMaterial in taskMaterials)
            {
                taskMaterial.StatusId = GetStageStatusByCode(OBK_Ref_StageStatus.New).Id;
            }

            var taskExecutors = new List<OBK_TaskExecutor>();
            var tems = new List<OBK_TaskMaterailExecutor>();
            foreach (var manager in managerResearchCenter.ManagerLaboratory)
            {
                var executor = new OBK_TaskExecutor
                {
                    Id = Guid.NewGuid(),
                    TaskId = managerResearchCenter.TaskId,
                    ExecutorId = manager.ExecutorId,
                    StageId = CodeConstManager.STAGE_OBK_RESEARCH_CENTER,
                    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING
                };
                taskExecutors.Add(executor);
                var tms = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == managerResearchCenter.TaskId && e.UnitLaboratoryId == manager.UnitLaboratoryId);
                foreach (var tm in tms)
                {
                    var tem = new OBK_TaskMaterailExecutor
                    {
                        Id = Guid.NewGuid(),
                        TaskExecutorId = executor.Id,
                        TaskMaterialId = tm.Id
                    };
                    tems.Add(tem);
                }

                new SafetyAssessmentRepository().AddHistory(task.AssessmentDeclarationId, OBK_Ref_StageStatus.TaskSendLab, manager.ExecutorId);
            }


            var boss = new OBK_TaskExecutor
            {
                Id = Guid.NewGuid(),
                TaskId = managerResearchCenter.TaskId,
                ExecutorId = managerResearchCenter.BossId,
                StageId = CodeConstManager.STAGE_OBK_RESEARCH_CENTER,
                ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER
            };
            taskExecutors.Add(boss);
            AppContext.OBK_TaskExecutor.AddRange(taskExecutors);
            AppContext.OBK_TaskMaterailExecutor.AddRange(tems);
            AppContext.SaveChanges();
        }

        public List<OBKResearchCenterListView> GetResearchCenterListView(Guid departamentId, Guid userId, string filterStatus)
        {
            var unitCode = AppContext.Units.FirstOrDefault(e => e.Id == departamentId)?.Code;
            Expression<Func<OBK_TaskExecutor, bool>> executor;
            Expression<Func<OBK_TaskMaterial, bool>> materail;
            switch (unitCode)
            {
                case OrganizationConsts.ReasearchCenterHeadCode:
                case OrganizationConsts.SupplyDivision:
                    materail = x => x.OBK_Ref_StageStatus.Code == filterStatus;
                    break;
                default:
                    materail = x => x.UnitLaboratoryId == departamentId && x.OBK_Ref_StageStatus.Code == filterStatus;
                    break;
            }
            switch (filterStatus)
            {
                case OBK_Ref_StageStatus.Completed:
                    executor = x => x.ExecutorId == userId;
                    break;
                default:
                    executor = x => x.ExecutorId == userId && x.IsCompleted != true;
                    break;
            }
            var tasks = AppContext.OBK_Tasks.Where(
                e => e.OBK_TaskExecutor.AsQueryable().Where(executor).Any(d => d.TaskId == e.Id) &&
                     e.OBK_TaskMaterial.AsQueryable().Where(materail).Any(f => f.TaskId == e.Id));
            var researchCenterListViews = new List<OBKResearchCenterListView>();
            foreach (var task in tasks)
            {
                var researchCenterListView = new OBKResearchCenterListView
                {
                    TaskId = task.Id,
                    UnitLaboratoryId = departamentId,
                    TaskNumber = task.TaskNumber,
                    RegisterDate = task.RegisterDate,
                    TaskEndDate = task.TaskEndDate.ToString(),
                    StageStatusCode = filterStatus
                };
                researchCenterListViews.Add(researchCenterListView);
            }
            return researchCenterListViews;
        }


        public OBKTaskResearchCenter EditResearchCenter(Guid taskId, Guid unitLabId)
        {
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            if (task == null) return null;
            var taskMaterials = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId && e.UnitLaboratoryId == unitLabId);

            var researchCenter = new OBKTaskResearchCenter
            {
                TaskId = task.Id,
                AssessmentDeclarationId = task.AssessmentDeclarationId,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName,
                UnitLaboratoryId = unitLabId
            };
            var taskListResearchCenters = new List<OBKTaskListResearchCenter>();
            foreach (var taskMaterial in taskMaterials)
            {
                var taskListResearchCenter = new OBKTaskListResearchCenter
                {
                    Id = taskMaterial.Id,
                    RegisterId = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.RegisterId,
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

            var taskExecutors = new List<OBK_TaskExecutor>();
            var tmes = new List<OBK_TaskMaterailExecutor>();
            foreach (var tl in taskResearchCenter.TaskListResearchCenter)
            {
                //taskExecutors.AddRange(tl.LaboratoryAssistantIds.Select(laId => new OBK_TaskExecutor
                //{
                //    Id = Guid.NewGuid(),
                //    TaskId = taskResearchCenter.TaskId,
                //    TaskMaterialId = tl.Id,
                //    ExecutorId = laId, //tl.LaboratoryAssistantId,
                //    StageId = CodeConstManager.STAGE_OBK_RESEARCH_CENTER,
                //    ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                //}));
                foreach (var laId in tl.LaboratoryAssistantIds)
                {
                    var te = new OBK_TaskExecutor
                    {
                        Id = Guid.NewGuid(),
                        TaskId = taskResearchCenter.TaskId,
                        TaskMaterialId = tl.Id,
                        ExecutorId = laId,
                        StageId = CodeConstManager.STAGE_OBK_RESEARCH_CENTER,
                        ExecutorType = CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR
                    };
                    taskExecutors.Add(te);
                    var tme = new OBK_TaskMaterailExecutor
                    {
                        Id = Guid.NewGuid(),
                        TaskMaterialId = tl.Id,
                        TaskExecutorId = te.Id
                    };
                    tmes.Add(tme);
                }
            }

            foreach (var taskMaterial in taskMaterials)
            {
                foreach (var selectedExecutor in taskResearchCenter.TaskListResearchCenter)
                {
                    //проверить правильное ли id
                    if (selectedExecutor.Id == taskMaterial.Id)
                    {
                        taskMaterial.StatusId = GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
                    }
                }
            }
            AppContext.OBK_TaskExecutor.AddRange(taskExecutors);
            AppContext.OBK_TaskMaterailExecutor.AddRange(tmes);
            AppContext.SaveChanges();
        }

        public OBKTaskResearchCenter EditExpertResearchCenter(Guid taskId, Guid unitLabId)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            if (task == null)
                return null;
            var taskMaterials =
                AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId && e.UnitLaboratoryId == unitLabId &&
                                                       e.OBK_TaskExecutor.Any(x => x.ExecutorId == userId));
            var researchCenter = new OBKTaskResearchCenter
            {
                TaskId = task.Id,
                AssessmentDeclarationId = task.AssessmentDeclarationId,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName,
                UnitLaboratoryId = unitLabId
            };
            var taskListResearchCenters = new List<OBKTaskListResearchCenter>();
            foreach (var taskMaterial in taskMaterials)
            {
                var taskListResearchCenter = new OBKTaskListResearchCenter
                {
                    Id = taskMaterial.Id,
                    ProductSeriesId = taskMaterial.ProductSeriesId,
                    ProductNameRu = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                    ProductNameKz = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                    Series = taskMaterial.OBK_Procunts_Series.Series,
                    IdNumber = taskMaterial.IdNumber,
                    LaboratoryName = taskMaterial.OBK_Ref_LaboratoryType.NameRu,
                    ExecutorLaboratoryName = taskMaterial.OBK_TaskExecutor.FirstOrDefault(e => e.ExecutorId == userId)?.Employee.DisplayName, //Employee.DisplayName,
                    ExecutorLaboratorySign = taskMaterial.OBK_TaskExecutor.FirstOrDefault(e => e.OBK_TaskMaterailExecutor.Any(x => x.TaskMaterialId == taskMaterial.Id) && e.ExecutorId == userId)?.SignedData != null, //e.TaskMaterialId == taskMaterial.Id
                    CreateBtnValid = taskMaterial.OBK_ResearchCenterResult.FirstOrDefault(e => e.ExecutorId == userId)?.ExpertiseResult != null //taskMaterial.ExpertiseResult != null
                };
                taskListResearchCenters.Add(taskListResearchCenter);
            }
            researchCenter.TaskListResearchCenter = taskListResearchCenters;
            return researchCenter;
        }

        public OBKSubTaskResult SubTaskResult(Guid taskMaterialId, string type)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var taskMaterial = AppContext.OBK_TaskMaterial.FirstOrDefault(e => e.Id == taskMaterialId);
            if (taskMaterial == null) return null;

            if (type == "create")
            {
                var subTaskResult = new OBKSubTaskResult();
                subTaskResult.Id = taskMaterial.Id;
                subTaskResult.Type = type;
                subTaskResult.RegisterId = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.RegisterId;
                subTaskResult.ProductNameRu = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu;
                subTaskResult.ProductNameKz = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz;
                subTaskResult.NdProduct = taskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " +
                                          taskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdNumber;
                subTaskResult.SubTaskNumber = taskMaterial.SubTaskNumber;//GenSubTaskNumber(taskMaterial.OBK_Tasks.TaskNumber, taskMaterialId);// taskMaterial.OBK_Tasks.TaskNumber + "/" + SubTaskCount();
                subTaskResult.Regulation = taskMaterial.Regulation;

                if (taskMaterial.OBK_TaskExecutor.Count(
                        e => e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR) <=
                    CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING) return subTaskResult;

                var stcers = new List<SubTaskCoExecutorResults>();
                foreach (var tm in taskMaterial.OBK_TaskExecutor.Where(e => e.ExecutorId != userId))
                {
                    var stcer = new SubTaskCoExecutorResults();
                    stcer.SubTaskCoExecutorName = tm.Employee.DisplayName;
                    stcer.SubTaskIndicatorCoExecutor = tm.OBK_TaskMaterial.OBK_ResearchCenterResult
                        .Where(e => e.ExecutorId == tm.ExecutorId)
                        .Select(
                            e => new SubTaskIndicator
                            {
                                LaboratoryMarkNameRu = e.OBK_Ref_LaboratoryMark.NameRu,
                                LaboratoryMarkNameKz = e.OBK_Ref_LaboratoryMark.NameKz,
                                Claim = e.Claim,
                                FactResult = e.FactResult,
                                Humidity = e.Humidity,
                                LaboratoryRegulationNameRu = e.OBK_Ref_LaboratoryRegulation.NameRu,
                                LaboratoryRegulationNameKz = e.OBK_Ref_LaboratoryRegulation.NameKz,
                                ExpertiseResult = (bool)e.ExpertiseResult,
                                ExecutorSign = tm.SignedData != null
                            })
                        .ToList();
                    stcers.Add(stcer);
                }
                subTaskResult.SubTaskCoExecutorResult = stcers;
                return subTaskResult;
            }

            var str = new OBKSubTaskResult();
            str.Id = taskMaterial.Id;
            str.TaskExecutorId = taskMaterial.Id;
            str.Type = type;
            str.RegisterId = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.RegisterId;
            str.ProductNameRu = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu;
            str.ProductNameKz = taskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz;
            str.NdProduct = taskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " +
                            taskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdNumber;
            str.Regulation = taskMaterial.Regulation;
            str.SubTaskNumber = taskMaterial.SubTaskNumber;
            str.SubTaskIndicator = taskMaterial.OBK_ResearchCenterResult.Where(e => e.ExecutorId == userId)
                .Select(e => new SubTaskIndicator
                {
                    ResearchCenterId = e.Id,
                    LaboratoryMarkId = e.OBK_Ref_LaboratoryMark.Id,
                    LaboratoryMarkNameRu = e.OBK_Ref_LaboratoryMark.NameRu,
                    LaboratoryMarkNameKz = e.OBK_Ref_LaboratoryMark.NameKz,
                    Claim = e.Claim,
                    FactResult = e.FactResult,
                    Humidity = e.Humidity,
                    RegulationId = e.OBK_Ref_LaboratoryRegulation.Id,
                    LaboratoryRegulationNameRu = e.OBK_Ref_LaboratoryRegulation.NameRu,
                    LaboratoryRegulationNameKz = e.OBK_Ref_LaboratoryRegulation.NameKz,
                    ExpertiseResult = (bool)e.ExpertiseResult,
                    TaskComment = e.OBK_ResearchCenterResultCom.Count(x => !x.Fixed) > 0
                })
                .ToList();
            if (taskMaterial.OBK_TaskExecutor.Count(e => e.ExecutorType == 2) <= 1) return str;

            var stcerss = new List<SubTaskCoExecutorResults>();
            foreach (var tm in taskMaterial.OBK_TaskExecutor.Where(e => e.ExecutorId != userId))
            {
                var stcer = new SubTaskCoExecutorResults();
                stcer.SubTaskCoExecutorName = tm.Employee.DisplayName;
                stcer.SubTaskIndicatorCoExecutor = tm.OBK_TaskMaterial.OBK_ResearchCenterResult
                    .Where(e => e.ExecutorId == tm.ExecutorId)
                    .Select(
                        e => new SubTaskIndicator
                        {
                            LaboratoryMarkId = e.OBK_Ref_LaboratoryMark.Id,
                            LaboratoryMarkNameRu = e.OBK_Ref_LaboratoryMark.NameRu,
                            LaboratoryMarkNameKz = e.OBK_Ref_LaboratoryMark.NameKz,
                            Claim = e.Claim,
                            FactResult = e.FactResult,
                            Humidity = e.Humidity,
                            RegulationId = e.OBK_Ref_LaboratoryRegulation.Id,
                            LaboratoryRegulationNameRu = e.OBK_Ref_LaboratoryRegulation.NameRu,
                            LaboratoryRegulationNameKz = e.OBK_Ref_LaboratoryRegulation.NameKz,
                            ExpertiseResult = (bool)e.ExpertiseResult,
                            ExecutorSign = tm.SignedData != null,
                        })
                    .ToList();
                stcerss.Add(stcer);
            }
            str.SubTaskCoExecutorResult = stcerss;
            return str;
        }

        public string SaveSubTaskResult(OBKSubTaskResult subTaskResult)
        {
            try
            {
                var userId = UserHelper.GetCurrentEmployee().Id;
                var taskMaterial = AppContext.OBK_TaskMaterial.FirstOrDefault(e => e.Id == subTaskResult.Id);
                if (taskMaterial == null) return "notFound";
                foreach (var rcr in taskMaterial.OBK_ResearchCenterResult)
                {
                    if (subTaskResult.SubTaskIndicator.Any(sti => rcr.LaboratoryMarkId == sti.LaboratoryMarkId))
                    {
                        return "Показатель '" + rcr.OBK_Ref_LaboratoryMark.NameRu + "' был заполнен исполнителем: " + rcr.Employee.DisplayName;
                    }
                }
                var te = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.ExecutorId == userId && e.OBK_TaskMaterailExecutor.Any(x => x.TaskMaterialId == taskMaterial.Id)); //&& e.TaskMaterialId == taskMaterial.Id
                taskMaterial.Regulation = subTaskResult.Regulation;
                taskMaterial.SubTaskNumber = subTaskResult.SubTaskNumber;
                taskMaterial.CreatedDate = DateTime.Now;
                var rcs = new List<OBK_ResearchCenterResult>();
                foreach (var st in subTaskResult.SubTaskIndicator)
                {
                    var rc = new OBK_ResearchCenterResult
                    {
                        Id = Guid.NewGuid(),
                        TaskMaterialId = taskMaterial.Id,
                        Claim = st.Claim,
                        FactResult = st.FactResult,
                        Humidity = st.Humidity,
                        LaboratoryMarkId = st.LaboratoryMarkId,
                        LaboratoryRegulationId = st.RegulationId,
                        ExpertiseResult = st.ExpertiseResult,
                        ExecutorId = userId,
                        TaskExecutorId = te?.Id
                    };
                    rcs.Add(rc);
                }
                AppContext.OBK_ResearchCenterResult.AddRange(rcs);
                AppContext.SaveChanges();
                return "save";
            }
            catch (Exception)
            {
                return "error";
            }
        }

        public string GetSubTaskSignDataExpert(Guid id)
        {
            var reslutTaskMaterial = AppContext.OBK_TaskMaterial.FirstOrDefault(e => e.Id == id);
            if (reslutTaskMaterial == null) return null;
            var str = new OBKSubTaskResult
            {
                Id = reslutTaskMaterial.Id,
                ProductNameRu = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                ProductNameKz = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                NdProduct = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " +
                            reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdNumber,
                Regulation = reslutTaskMaterial.Regulation,
                SubTaskIndicator = reslutTaskMaterial.OBK_ResearchCenterResult.Select(e => new SubTaskIndicator
                {
                    LaboratoryMarkId = e.LaboratoryMarkId,
                    Claim = e.Claim,
                    FactResult = e.FactResult,
                    Humidity = e.Humidity,
                    ExpertiseResult = (bool)e.ExpertiseResult
                }).ToList()
            };
            var xmlData = SerializeHelper.SerializeDataContract(str);
            return xmlData.Replace("utf-16", "utf-8");
        }

        public string GetTaskSignDataChief(Guid taskId)
        {
            var departamentId = UserHelper.GetDepartment().Id;
            var unitCode = AppContext.Units.FirstOrDefault(e => e.Id == departamentId)?.Code;

            var reslutTaskMaterials =
            AppContext.OBK_TaskMaterial.Where(e => unitCode == OrganizationConsts.ReasearchCenterHeadCode ? e.TaskId == taskId : e.TaskId == taskId && e.UnitLaboratoryId == departamentId);
            if (!reslutTaskMaterials.Any())
                return null;

            var strs = new List<OBKSubTaskResult>();
            foreach (var reslutTaskMaterial in reslutTaskMaterials)
            {
                var str = new OBKSubTaskResult
                {
                    Id = reslutTaskMaterial.Id,
                    ProductNameRu = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                    ProductNameKz = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                    NdProduct = reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " +
                                reslutTaskMaterial.OBK_Procunts_Series.OBK_RS_Products?.NdNumber,
                    SubTaskIndicator = reslutTaskMaterial.OBK_ResearchCenterResult.Select(e => new SubTaskIndicator
                    {
                        LaboratoryMarkId = e.LaboratoryMarkId,
                        Claim = e.Claim,
                        FactResult = e.FactResult,
                        Humidity = e.Humidity,
                        ExpertiseResult = (bool)e.ExpertiseResult
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
            var taskExecutor = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.OBK_TaskMaterailExecutor.Any(x => x.TaskMaterialId == id) && e.ExecutorId == userId && e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_EXECUTOR);
            if (taskExecutor == null) return false;
            taskExecutor.SignedData = signedData;
            taskExecutor.IsCompleted = true;
            AppContext.SaveChanges();
            AutoSendToChief(taskExecutor.TaskId);
            return true;
        }

        public void AutoSendToChief(Guid taskId)
        {
            var departamentId = UserHelper.GetDepartment().Id;
            var tms = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId && e.UnitLaboratoryId == departamentId);
            if (!tms.All(x => x.OBK_TaskExecutor.All(e => e.IsCompleted == true))) return;
            foreach (var tm in tms)
            {
                tm.StatusId = GetStageStatusByCode(OBK_Ref_StageStatus.RequiresSigning).Id;
            }
            AppContext.SaveChanges();
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
                AppContext.OBK_TaskMaterial.Where(e => e.OBK_TaskExecutor.Any(x => x.ExecutorId == userId) && e.TaskId == taskId);
            foreach (var taskMaterial in taskMaterials)
            {
                taskMaterial.StatusId = GetStageStatusByCode(OBK_Ref_StageStatus.RequiresSigning).Id;
            }
            AppContext.SaveChanges();
            return true;
        }

        public bool SaveSignedTaskChief(Guid taskId, string signedData)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var te = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == taskId && e.ExecutorId == userId);
            if (te == null) return false;
            te.SignedData = signedData;
            te.IsCompleted = true;
            AppContext.SaveChanges();
            NextStatus(te.ExecutorType, taskId);
            return true;
        }

        private void NextStatus(int? eType, Guid taskId)
        {
            switch (eType)
            {
                case CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING:
                    var tes = AppContext.OBK_TaskExecutor.Where(e => e.TaskId == taskId && e.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_ASSIGNING);
                    if (!tes.All(x => x.IsCompleted == true)) break;
                    var status = GetStageStatusByCode(OBK_Ref_StageStatus.OnApprove).Id;
                    var tmss = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId);
                    foreach (var t in tmss)
                    {
                        t.StatusId = status;
                    }
                    AppContext.SaveChanges();
                    break;
                case CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER:
                    var tms = AppContext.OBK_TaskMaterial.Where(e => e.TaskId == taskId);
                    var statusId = GetStageStatusByCode(OBK_Ref_StageStatus.Completed).Id;
                    foreach (var tm in tms)
                    {
                        tm.StatusId = statusId;
                    }

                    AppContext.SaveChanges();

                    if (eType == 3)
                    {
                        var task = AppContext.OBK_Tasks.Where(r => r.Id == taskId).FirstOrDefault();
                        var declaration = AppContext.OBK_AssessmentDeclaration.Where(r => r.Id == task.AssessmentDeclarationId).FirstOrDefault();
                        var stage = AppContext.OBK_AssessmentStage.Where(r => r.DeclarationId == declaration.Id).FirstOrDefault();
                        var stageExecutor = AppContext.OBK_AssessmentStageExecutors.Where(r => r.AssessmentStageId == stage.Id && r.ExecutorType == 2).FirstOrDefault();
                        var executor = AppContext.Employees.Where(r => r.Id == stageExecutor.ExecutorId).FirstOrDefault();
                        new SafetyAssessmentRepository().AddHistory(declaration.Id, OBK_Ref_StageStatus.InWork, executor.Id);
                    }

                    break;
            }
        }

        public OBKTaskResearchCenter EditChiefResearchCenter(Guid taskId, Guid unitLabId, string stautsCode)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var te = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.ExecutorId == userId && e.TaskId == taskId);
            if (te == null) return null;
            var task = AppContext.OBK_Tasks.FirstOrDefault(e => e.Id == taskId);
            if (task == null) return null;

            Expression<Func<OBK_TaskMaterial, bool>> teaskMaterials;
            if (te.ExecutorType == CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER || stautsCode == OBK_Ref_StageStatus.Completed)
            {
                teaskMaterials = e => e.TaskId == taskId;
            }
            else
            {
                teaskMaterials = e => e.TaskId == taskId && e.UnitLaboratoryId == unitLabId;
            }
            var taskMaterials = AppContext.OBK_TaskMaterial.AsQueryable().Where(teaskMaterials);
            var productSeries = AppContext.OBK_Procunts_Series.Where(e => e.OBK_TaskMaterial.Any(x => taskMaterials.Any(d => d.Id == x.Id)));
            var researchCenter = new OBKTaskResearchCenter
            {
                TaskId = task.Id,
                AssessmentDeclarationId = task.AssessmentDeclarationId,
                TaskNumber = task.TaskNumber,
                RegisterDate = task.RegisterDate,
                ActNumber = task.OBK_ActReception.Number,
                UnitName = task.Unit.ShortName,
                UnitLaboratoryId = unitLabId,
                ExecutorCode = te.ExecutorType ?? 0,
                ChiefSign = te.SignedData != null,
                StatusCode = stautsCode
            };
            var taskListResearchCenters = new List<OBKTaskListResearchCenter>();
            foreach (var ps in productSeries)
            {
                var taskListResearchCenter = new OBKTaskListResearchCenter
                {
                    //Id = taskMaterial.Id,
                    ProductSeriesId = ps.Id,
                    ProductNameRu = ps.OBK_RS_Products.NameRu,
                    ProductNameKz = ps.OBK_RS_Products.NameKz,
                    Series = ps.Series,
                    LaboratoryName = string.Join(", ", ps.OBK_TaskMaterial.Where(e => e.ProductSeriesId == ps.Id).Select(x => x.OBK_Ref_LaboratoryType.NameRu).GroupBy(x => x).Select(g => g.Key))
                };
                taskListResearchCenters.Add(taskListResearchCenter);
            }
            researchCenter.TaskListResearchCenter = taskListResearchCenters;
            return researchCenter;
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
                AppContext.UnitsAddresses.FirstOrDefault(e => e.UnitsId == task.UnitId && (bool)!e.IsDeleted);
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
                LaboratoryName = string.Join(", ", ps.OBK_TaskMaterial.Where(e => e.ProductSeriesId == ps.Id).Select(x => x.OBK_Ref_LaboratoryType.NameRu))
            });
            return obj;
        }

        public SubTaskDetails GetTaskDetails(Guid taskId, int productSeriesId, int executorCode)
        {
            var departamentId = UserHelper.GetDepartment().Id;
            Expression<Func<OBK_TaskExecutor, bool>> taskExecutor;
            switch (executorCode)
            {
                case CodeConstManager.OBK_CONTRACT_STAGE_EXECUTOR_TYPE_SIGNER:
                case 0:
                    taskExecutor = e => e.TaskId == taskId && e.OBK_TaskMaterial.ProductSeriesId == productSeriesId;
                    break;
                default:
                    taskExecutor = e => e.TaskId == taskId && e.OBK_TaskMaterial.ProductSeriesId == productSeriesId && e.OBK_TaskMaterial.UnitLaboratoryId == departamentId;
                    break;
            }
            var tes = AppContext.OBK_TaskExecutor.AsQueryable().Where(taskExecutor);
            var stds = new SubTaskDetails();
            var strs = new List<OBKSubTaskResult>();
            foreach (var te in tes)
            {
                var str = new OBKSubTaskResult
                {
                    Id = te.OBK_TaskMaterial.Id,
                    TaskExecutorId = te.Id,
                    ProductNameRu = te.OBK_TaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameRu,
                    ProductNameKz = te.OBK_TaskMaterial.OBK_Procunts_Series.OBK_RS_Products.NameKz,
                    Regulation = te.OBK_TaskMaterial.Regulation,
                    //ExpertiseResultName = te.ExpertiseResult == null ? "Испытания не завершены" : (bool)te.ExpertiseResult ? "Соотвествует требованиям" : "Не соотвествует требованиям",
                    LaboratoryExpertName = te.Employee?.DisplayName,
                    LaboratoryTypeName = te.OBK_TaskMaterial.OBK_Ref_LaboratoryType?.NameRu,
                    UnitLaboratoryName = te.OBK_TaskMaterial.Unit?.DisplayName,
                    SubTaskNumber = te.OBK_TaskMaterial.SubTaskNumber,
                    SubTaskCreateDate = te.OBK_TaskMaterial.CreatedDate,
                    SubTaskIndicator = te.OBK_TaskMaterial.OBK_ResearchCenterResult.Where(x => x.ExecutorId == te.ExecutorId).Select(e => new SubTaskIndicator
                    {
                        ResearchCenterId = e.Id,
                        LaboratoryMarkNameRu = e.OBK_Ref_LaboratoryMark.NameRu,
                        LaboratoryMarkNameKz = e.OBK_Ref_LaboratoryMark.NameKz,
                        Claim = e.Claim,
                        FactResult = e.FactResult,
                        Humidity = e.Humidity,
                        LaboratoryRegulationNameRu = e.OBK_Ref_LaboratoryRegulation.NameRu,
                        LaboratoryRegulationNameKz = e.OBK_Ref_LaboratoryRegulation.NameKz,
                        ExpertiseResultName = e.ExpertiseResult == null ? "Испытания не завершены" : (bool)e.ExpertiseResult ? "Соотвествует требованиям" : "Не соотвествует требованиям",
                        ExecutorSign = te.SignedData != null,
                        TaskComment = e.OBK_ResearchCenterResultCom.Count > 0
                    }).ToList()
                };
                strs.Add(str);
            }
            stds.SubTaskResult = strs;
            return stds;
        }

        public List<OBKTaskHistory> EditTaskHistory(Guid taskId)
        {
            var tes = AppContext.OBK_TaskExecutor.Where(e => e.TaskId == taskId && e.StageId == 6);
            var historys = new List<OBKTaskHistory>();
            foreach (var te in tes)
            {
                var history = new OBKTaskHistory();
                history.ExecutorName = te.Employee.DisplayName;
                history.StatusName = te.OBK_TaskMaterial?.OBK_Ref_StageStatus.NameRu;
                history.TaskSign = te.SignedData != null ? "Да" : "Нет";
                historys.Add(history);
            }
            return historys;
        }

        public void ReturnToExecutor(Guid tid)
        {
            var rcrcs = AppContext.OBK_ResearchCenterResultCom.Where(e => e.OBK_ResearchCenterResult.OBK_TaskMaterial.TaskId == tid && !e.Fixed);
            var rcrs = AppContext.OBK_ResearchCenterResult.Where(e => rcrcs.Any(x => x.ResearchCenterResultId == e.Id));
            var statusId = new SafetyAssessmentRepository().GetStageStatusByCode(OBK_Ref_StageStatus.InWork).Id;
            foreach (var rcr in rcrs)
            {
                rcr.OBK_TaskExecutor.IsCompleted = false;
                rcr.OBK_TaskExecutor.SignedData = null;
                rcr.OBK_TaskMaterial.StatusId = statusId;
            }
            AppContext.SaveChanges();
        }

        public void SaveSubTaskRemark(List<SubTaskIndicator> stis)
        {
            foreach (var sti in stis)
            {
                var rcr = AppContext.OBK_ResearchCenterResult.FirstOrDefault(e => sti.ResearchCenterId == e.Id);
                if (rcr == null) continue;
                rcr.Claim = sti.Claim;
                rcr.FactResult = sti.FactResult;
                rcr.Humidity = sti.Humidity;
                rcr.LaboratoryMarkId = sti.LaboratoryMarkId;
                rcr.LaboratoryRegulationId = sti.RegulationId;
                rcr.ExpertiseResult = sti.ExpertiseResult;

                var rcrcs = AppContext.OBK_ResearchCenterResultCom.Where(e => sti.ResearchCenterId == e.ResearchCenterResultId);
                if (!rcrcs.Any()) continue;
                foreach (var rcrc in rcrcs)
                {
                    rcrc.Fixed = true;
                }
                AppContext.SaveChanges();
            }
        }

        public object GetTaskProtocolData(int psId)
        {
            var t = AppContext.OBK_TaskMaterial.FirstOrDefault(e => e.ProductSeriesId == psId);
            if (t == null) return null;
            var ceoInfo = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == t.OBK_Tasks.Id && e.StageId == CodeConstManager.STAGE_OBK_RESEARCH_CENTER && e.ExecutorType == 3);
            var rcrs = AppContext.OBK_ResearchCenterResult.Where(e => e.OBK_TaskMaterial.ProductSeriesId == psId);
            var obj = new
            {
                SubTaskNumber = t.SubTaskNumber,
                SubTaskCreatedDate = t.CreatedDate?.ToShortDateString(),
                Declarant = t.OBK_Tasks.OBK_AssessmentDeclaration.OBK_Contract.OBK_Declarant?.NameRu,
                ProductName = t.OBK_Procunts_Series.OBK_RS_Products?.NameRu,
                ActReceptionNumber = t.OBK_Tasks.OBK_ActReception?.Number,
                ActReceptionDate = t.OBK_Tasks.OBK_ActReception?.ActDate?.ToShortDateString(),
                ProducerName = t.OBK_Procunts_Series.OBK_RS_Products?.ProducerNameRu + " (" + t.OBK_Procunts_Series.OBK_RS_Products?.CountryNameRu + ")",
                Series = t.OBK_Procunts_Series.Series,
                SeriesStartDate = t.OBK_Procunts_Series.SeriesStartdate,
                SeriesEndDate = t.OBK_Procunts_Series.SeriesEndDate,
                Quantity = t.Quantity.ToString(),
                NdName = t.OBK_Procunts_Series.OBK_RS_Products?.NdName + " " + t.OBK_Procunts_Series.OBK_RS_Products?.NdNumber,
                Regulation = string.Join(", ", AppContext.OBK_TaskMaterial.Where(e => e.ProductSeriesId == psId).Select(e => e.Regulation)),
                SubTaskResult = AppContext.OBK_TaskMaterial.Where(e => e.ProductSeriesId == psId).All(x => x.OBK_ResearchCenterResult.All(e => e.ExpertiseResult == true)) ? "<u>соответствуют</u>/не соответствуют" : "соответствуют/<u>не соответствуют</u>",
                ProtocolResults = rcrs.Select(ps => new
                {
                    ps.OBK_Ref_LaboratoryMark.NameRu,
                    ps.Claim,
                    ps.FactResult,
                    ps.Humidity
                }).ToList()
            };
            return obj;
        }

        public object GetTaskProtocolExecutorListData(int psId)
        {
            //var t = AppContext.OBK_TaskMaterial.FirstOrDefault(e => e.ProductSeriesId == psId);
            //if (t == null) return null;
            //var ceoInfo = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == t.OBK_Tasks.Id && e.StageId == CodeConstManager.STAGE_OBK_RESEARCH_CENTER && e.ExecutorType == 3);
            //var obj = new
            //{
            //    ExecutorList = t.OBK_TaskMaterailExecutor.Where(x => x.OBK_TaskExecutor.StageId == CodeConstManager.STAGE_OBK_RESEARCH_CENTER).Select(el => new
            //    {
            //        ExeutorType = el.OBK_TaskExecutor.ExecutorType,
            //        FullName = el.OBK_TaskExecutor.Employee.LastName + " " + el.OBK_TaskExecutor.Employee.FirstName[0] + ". " + el.OBK_TaskExecutor.Employee.MiddleName[0] + ".",
            //        Position = el.OBK_TaskExecutor.Employee.Position.ShortName
            //    }).ToList().OrderByDescending(e=>e.ExeutorType),
            //    CeoName = ceoInfo?.Employee.LastName + " " + ceoInfo?.Employee.FirstName[0] + ". " + ceoInfo?.Employee.MiddleName[0] + ".",
            //    Ceo = ceoInfo?.Employee.Position.ShortName
            //};

            var t = AppContext.OBK_TaskMaterial.Where(e => e.ProductSeriesId == psId);
            if (!t.Any()) return null;
            var ceoInfo = AppContext.OBK_TaskExecutor.FirstOrDefault(e => e.TaskId == t.FirstOrDefault().OBK_Tasks.Id && e.StageId == CodeConstManager.STAGE_OBK_RESEARCH_CENTER && e.ExecutorType == 3);

            var tme = AppContext.OBK_TaskMaterailExecutor.Where(e => t.Any(x => x.Id == e.TaskMaterialId)).ToList();

            var obj = new
            {

                ExecutorList = tme.Where(x => x.OBK_TaskExecutor.StageId == CodeConstManager.STAGE_OBK_RESEARCH_CENTER).Select(el => new
                {
                    ExeutorType = el.OBK_TaskExecutor.ExecutorType,
                    FullName = el.OBK_TaskExecutor.Employee.LastName + " " + el.OBK_TaskExecutor.Employee.FirstName[0] + ". " + el.OBK_TaskExecutor.Employee.MiddleName[0] + ".",
                    Position = el.OBK_TaskExecutor.Employee.Position.ShortName
                }).ToList().OrderByDescending(e => e.ExeutorType),


                CeoName = ceoInfo?.Employee.LastName + " " + ceoInfo?.Employee.FirstName[0] + ". " + ceoInfo?.Employee.MiddleName[0] + ".",
                Ceo = ceoInfo?.Employee.Position.ShortName
            };
            return obj;
        }

        public class ResearchCenterResultModel
        {
            public System.Guid Id { get; set; }
            public System.Guid TaskMaterialId { get; set; }
            public System.Guid LaboratoryMarkId { get; set; }
            public string Claim { get; set; }
            public string Humidity { get; set; }
            public string FactResult { get; set; }
            public Nullable<System.Guid> LaboratoryRegulationId { get; set; }
            public Nullable<System.Guid> ExecutorId { get; set; }
            public Nullable<bool> ExpertiseResult { get; set; }
            public Nullable<System.Guid> TaskExecutorId { get; set; }
        }

        /*public List<OBK_ResearchCenterResult> GetResultButton(Guid? searchTaskId, Guid? searchLaboratoryTypeId, string searchPRId)
        {
            var tasks = AppContext.OBK_Tasks.Where(r => r.Id == searchTaskId).ToList();
            var laboratories = AppContext.OBK_Ref_LaboratoryType.Where(r => r.Id == searchLaboratoryTypeId).ToList();

            List<OBK_TaskMaterial> task_materialTasks = new List<OBK_TaskMaterial>();
            List<OBK_TaskMaterial> task_materialLaboratories = new List<OBK_TaskMaterial>();
            List<OBK_TaskMaterial> task_materialsubTaskNumbers = AppContext.OBK_TaskMaterial.Where(r => r.SubTaskNumber == searchPRId).ToList();

            List<OBK_ResearchCenterResult> researchCenterResult = new List<OBK_ResearchCenterResult>();
            var rcrm = new List<ResearchCenterResultModel>();

            //OBK_TaskMaterial
            foreach (var task in tasks)
            {
                task_materialTasks = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == task.Id).ToList();
            }
            foreach (var laboratory in laboratories)
            {
                task_materialLaboratories = AppContext.OBK_TaskMaterial.Where(r => r.LaboratoryTypeId == laboratory.Id).ToList();
            }

            //OBK_ResearchCenterResult
            foreach (var task_materialTask in task_materialTasks)
            {
                researchCenterResult.AddRange(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialTask.Id).ToList());
            }
            foreach (var task_materialLaboratory in task_materialLaboratories)
            {
                foreach (OBK_ResearchCenterResult rs in researchCenterResult)
                {
                    if (!researchCenterResult.Contains(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialLaboratory.Id).FirstOrDefault()))
                    {
                        researchCenterResult.Add(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialLaboratory.Id).FirstOrDefault());
                    }
                }
            }
            foreach (var task_materialsubTaskNumber in task_materialsubTaskNumbers)
            {
                foreach (OBK_ResearchCenterResult rs in researchCenterResult)
                {
                    if (!researchCenterResult.Contains(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialsubTaskNumber.Id).FirstOrDefault()))
                    {
                        researchCenterResult.Add(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialsubTaskNumber.Id).FirstOrDefault());
                    }
                }
            }

            return researchCenterResult;
        }*/

        /*public List<OBK_ResearchCenterResult> GetResultButton( Guid? searchLaboratoryTypeId, string searchPRId)
        {
         var laboratories = AppContext.OBK_Ref_LaboratoryType.Where(r => r.Id == searchLaboratoryTypeId).ToList();

         List<OBK_TaskMaterial> task_materialLaboratories = new List<OBK_TaskMaterial>();
         List<OBK_TaskMaterial> task_materialsubTaskNumbers = AppContext.OBK_TaskMaterial.Where(r => r.SubTaskNumber == searchPRId).ToList();

         List<OBK_ResearchCenterResult> researchCenterResult = new List<OBK_ResearchCenterResult>();

         foreach (var laboratory in laboratories)
         {
             task_materialLaboratories = AppContext.OBK_TaskMaterial.Where(r => r.LaboratoryTypeId == laboratory.Id).ToList();
         }


         //reseacrhcenter
         foreach (var task_materialLaboratory in task_materialLaboratories)
         {
             //if (!researchCenterResult.Contains(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialLaboratory.Id).FirstOrDefault()))
             //{
             researchCenterResult.AddRange(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialLaboratory.Id).ToList());
             //}
         }
         foreach (var task_materialsubTaskNumber in task_materialsubTaskNumbers)
         {
             //if (!researchCenterResult.Contains(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialsubTaskNumber.Id).FirstOrDefault()))
             //{
             researchCenterResult.AddRange(AppContext.OBK_ResearchCenterResult.Where(t => t.TaskMaterialId == task_materialsubTaskNumber.Id).ToList());
             //}
         }

         return researchCenterResult;
        }*/
        public IQueryable<ResearchCenterResultModel> GetResultButton(Guid? searchTaskId, Guid? searchLaboratoryTypeId, string searchPRId, int? searchProductId)
        {
            var rcrmList = new List<ResearchCenterResultModel>();



            List<OBK_RS_Products> products = AppContext.OBK_RS_Products.Where(r => r.Id == searchProductId).ToList();
            List<OBK_Procunts_Series> procount_series = new List<OBK_Procunts_Series>();
            foreach (var product in products)
            {
                procount_series.AddRange(AppContext.OBK_Procunts_Series.Where(r => r.OBK_RS_ProductsId == product.Id).ToList());
            }

            List<OBK_TaskMaterial> task_materials_searchProductIds = new List<OBK_TaskMaterial>();
            List<OBK_TaskMaterial> task_materials_searchTaskIds = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId).ToList();
            List<OBK_TaskMaterial> task_materials_searchLaboratoryTypeIds = AppContext.OBK_TaskMaterial.Where(r => r.LaboratoryTypeId == searchLaboratoryTypeId).ToList();
            List<OBK_TaskMaterial> task_materials_searchPRIds = AppContext.OBK_TaskMaterial.Where(r => r.SubTaskNumber == searchPRId).ToList();

            List<OBK_TaskMaterial> task_materials = new List<OBK_TaskMaterial>();


            if (searchTaskId == null && searchLaboratoryTypeId == null && searchPRId == "" && searchProductId == null)
            {

            }
            if (searchTaskId == null && searchLaboratoryTypeId == null && searchPRId == "" && searchProductId != null)
            {
                foreach (var procount_serie in procount_series)
                {
                    task_materials = AppContext.OBK_TaskMaterial.Where(r => r.ProductSeriesId == procount_serie.Id).ToList();
                }
            }
            if (searchTaskId == null && searchLaboratoryTypeId == null && searchPRId != "" && searchProductId == null)
            {
                task_materials = AppContext.OBK_TaskMaterial.Where(r => r.SubTaskNumber == searchPRId).ToList();
            }
            if (searchTaskId == null && searchLaboratoryTypeId == null && searchPRId != "" && searchProductId != null)
            {
                foreach (var procount_serie in procount_series)
                {
                    task_materials = AppContext.OBK_TaskMaterial.Where(r => r.SubTaskNumber == searchPRId && r.ProductSeriesId == procount_serie.Id).ToList();
                }
            }
            if (searchTaskId == null && searchLaboratoryTypeId != null && searchPRId == "" && searchProductId == null)
            {
                task_materials = AppContext.OBK_TaskMaterial.Where(r => r.LaboratoryTypeId == searchLaboratoryTypeId).ToList();
            }
            if (searchTaskId == null && searchLaboratoryTypeId != null && searchPRId == "" && searchProductId != null)
            {
                foreach (var procount_serie in procount_series)
                {
                    task_materials = AppContext.OBK_TaskMaterial.Where(r => r.LaboratoryTypeId == searchLaboratoryTypeId && r.ProductSeriesId == procount_serie.Id).ToList();
                }
            }
            if (searchTaskId == null && searchLaboratoryTypeId != null && searchPRId != "" && searchProductId == null)
            {
                 task_materials = AppContext.OBK_TaskMaterial.Where(r => r.LaboratoryTypeId == searchLaboratoryTypeId && r.SubTaskNumber == searchPRId).ToList();
            }
            if (searchTaskId == null && searchLaboratoryTypeId != null && searchPRId != "" && searchProductId != null)
            {
                foreach (var procount_serie in procount_series)
                {
                    task_materials = AppContext.OBK_TaskMaterial.Where(r => r.LaboratoryTypeId == searchLaboratoryTypeId && r.SubTaskNumber == searchPRId &&
                    r.ProductSeriesId == procount_serie.Id).ToList();
                }
            }
            if (searchTaskId != null && searchLaboratoryTypeId == null && searchPRId == "" && searchProductId == null)
            {
                 task_materials = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId).ToList();
            }
            if (searchTaskId != null && searchLaboratoryTypeId == null && searchPRId == "" && searchProductId != null)
            {
                foreach (var procount_serie in procount_series)
                {
                    task_materials = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId && r.ProductSeriesId == procount_serie.Id).ToList();
                }
            }
            if (searchTaskId != null && searchLaboratoryTypeId == null && searchPRId != "" && searchProductId == null)
            {
                task_materials = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId && r.SubTaskNumber == searchPRId).ToList();
            }
            if (searchTaskId != null && searchLaboratoryTypeId == null && searchPRId != "" && searchProductId != null)
            {
                foreach (var procount_serie in procount_series)
                {
                    task_materials = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId && r.SubTaskNumber == searchPRId && r.ProductSeriesId == procount_serie.Id).ToList();
                }
            }
            if (searchTaskId != null && searchLaboratoryTypeId != null && searchPRId == "" && searchProductId == null)
            {
                task_materials = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId && r.LaboratoryTypeId == searchLaboratoryTypeId).ToList();
            }
            if (searchTaskId != null && searchLaboratoryTypeId != null && searchPRId == "" && searchProductId != null)
            {
                foreach (var procount_serie in procount_series)
                {
                    task_materials = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId && r.LaboratoryTypeId == searchLaboratoryTypeId &&
                    r.ProductSeriesId == procount_serie.Id).ToList();
                }
            }
            if (searchTaskId != null && searchLaboratoryTypeId != null && searchPRId != "" && searchProductId == null)
            {
                task_materials = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId && r.LaboratoryTypeId == searchLaboratoryTypeId && r.SubTaskNumber == searchPRId).ToList();
            }
            if (searchTaskId != null && searchLaboratoryTypeId != null && searchPRId != "" && searchProductId != null)
            {
                foreach (var procount_serie in procount_series)
                {
                    task_materials = AppContext.OBK_TaskMaterial.Where(r => r.TaskId == searchTaskId && r.LaboratoryTypeId == searchLaboratoryTypeId && r.SubTaskNumber == searchPRId &&
                    r.ProductSeriesId == procount_serie.Id).ToList();
                }
            }


            foreach (var task_material in task_materials) 
            {
                foreach (var qwe in task_material.OBK_ResearchCenterResult)
                {
                    ResearchCenterResultModel rcrmObj = new ResearchCenterResultModel();
                    rcrmObj.Id = qwe.Id;
                    rcrmObj.TaskMaterialId = qwe.TaskMaterialId;
                    rcrmObj.LaboratoryMarkId = qwe.LaboratoryMarkId;
                    rcrmObj.Claim = qwe.Claim;
                    rcrmObj.Humidity = qwe.Humidity;
                    rcrmObj.FactResult = qwe.FactResult;
                    rcrmObj.LaboratoryRegulationId = qwe.LaboratoryRegulationId;
                    rcrmObj.ExecutorId = qwe.ExecutorId;
                    rcrmObj.ExpertiseResult = qwe.ExpertiseResult;
                    rcrmObj.TaskExecutorId = qwe.TaskExecutorId;

                    rcrmList.Add(rcrmObj);
                }
            }
            return rcrmList.AsQueryable();
        }

        public string GetRegulationVal(Guid? id)
        {
            var result = AppContext.OBK_Ref_LaboratoryRegulation.Where(r => r.Id == id).FirstOrDefault().NameRu;
            return result;
        }

        public string GetMarkVal(Guid? id)
        {
            var result = AppContext.OBK_Ref_LaboratoryMark.Where(r => r.Id == id).FirstOrDefault().NameRu;
            return result;
        }
    }
}
