using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.OBK;
using PW.Prism.ViewModels.OBK;
using Kendo.Mvc.Extensions;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Models.OBK;

namespace PW.Prism.Controllers.OBKTask
{
    /// <summary>
    /// Создание задания
    /// </summary>
    [Authorize]
    public class OBKTaskController : Controller
    {
        private OBKTaskRepository repo;

        public OBKTaskController()
        {
            repo = new OBKTaskRepository();
        }

        public ActionResult Task(Guid id)
        {
            var expResult = repo.GetStageExpDocumentResult(id);
            if (expResult == null || !expResult.ExpResult)
            {
                ViewBag.Message = "Заявка не соотвествует требованиями";
                return PartialView("ShowMessage");
            }
            string[] code = { "00" };
            //заявление
            var declarant = repo.GetAssessmentDeclaration(id);
            if (declarant == null)
            {
                ViewBag.Message = "Заявка не найдена";
                return PartialView("ShowMessage");
            }
            var tasks = repo.GetTasks(id);
            var taskMaterial = repo.GetTaskMaterial(tasks);
            ViewData["Units"] = new SelectList(repo.GetUnits(code), "Id", "ShortName");
            ViewData["ActReceptions"] = new SelectList(declarant.OBK_ActReception1, "Id", "Number");

            OBKCreateTaskViewModel task = new OBKCreateTaskViewModel
            {
                AssessmentDeclarationId = id
            };

            var labs = repo.GeLaboratoryTypes();
            task.LaboratoryTypeList = new MultiSelectList(labs, "Id", "NameRu", task.LaboratoryTypeIds);

            List<OBKProductViewModel> productViewModel = new List<OBKProductViewModel>();
            List<OBKProductViewModel> modalViewModel = new List<OBKProductViewModel>();

            List<OBKTaskViewModel> taskViewModel = new List<OBKTaskViewModel>();
            foreach (var t in tasks)
            {
                OBKTaskViewModel tvm = new OBKTaskViewModel
                {
                    TaskNumber = t.TaskNumber,
                    RegisterDate = t.RegisterDate,
                    ExecutorName = t.OBK_TaskExecutor.FirstOrDefault(e=>e.TaskId == t.Id)?.Employee.ShortName,
                    UnitName = repo.GetUnit(t.UnitId)?.ShortName
                };
                taskViewModel.Add(tvm);
            }

            foreach (var product in declarant.OBK_Contract.OBK_RS_Products)
            {
                foreach (var productSeries in product.OBK_Procunts_Series)
                {
                    // все серии продукции
                    var productView = new OBKProductViewModel
                    {
                        ProductNameRu = product.NameRu,
                        ProductNameKz = product.NameKz,
                        Series = productSeries.Series,
                        TaskNumber = productSeries.OBK_TaskMaterial.Where(e => e.ProductSeriesId == productSeries.Id).Select(x => x.OBK_Tasks.TaskNumber).FirstOrDefault(),
                        LaboratoryName = string.Join(", ", taskMaterial.Where(e => e.ProductSeriesId == productSeries.Id).Select(x => x.OBK_Ref_LaboratoryType.NameRu).GroupBy(x => x).Select(g => g.Key))
                    };
                    productViewModel.Add(productView);

                    var count = taskMaterial.Count(e => e.ProductSeriesId == productSeries.Id);

                    // для создания задания(не отмеченные ранее)
                    if (count == 0)
                    {
                        var modalView = new OBKProductViewModel
                        {
                            Id = productSeries.Id,
                            ProductNameRu = product.NameRu,
                            ProductNameKz = product.NameKz,
                            Series = productSeries.Series,
                            SeriesMeasure = productSeries.sr_measures.name,
                            SeriesStartdate = productSeries.SeriesStartdate,
                            SeriesEndDate = productSeries.SeriesEndDate,
                            Quantity = productSeries.Quantity
                        };
                        modalViewModel.Add(modalView);
                    }
                }
            }
            task.ProductViewModels = productViewModel;
            task.ModalViewModels = modalViewModel;
            task.TaskViewModels = taskViewModel;

            return PartialView("Index", task);
        }

        public ActionResult SaveTask(OBKCreateTaskViewModel model)
        {
            repo.SaveTask(model);
            return Json(new {isSuccess = true});
        }

        public ActionResult SaveSignedTask(Guid id, string signedData)
        {
            var tasks = repo.GetTasks(id);
            new SignDocumentRepository().SaveSignDocument(UserHelper.GetCurrentEmployee().Id, signedData, tasks);
            return Json(new { message = "Задание успешно подписано"});
        }

        [HttpGet]
        public ActionResult GetSignTask(Guid id)
        {
            var data = repo.GetSignTask(id);
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SendToNextStep(Guid id, Guid executorId, string code)
        {
            repo.SendToNextStep(id, executorId, code);
            return Json(new {isSuccess = true}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ShowModalExecutor(string code, Guid id)
        {
            ViewData["Executors"] = new SelectList(repo.GetExecutors(code), "Id", "DisplayName");
            OBKShowModalExecutor model = new OBKShowModalExecutor
            {
                Id = id,
                Code = code
            };
            return PartialView(model);
        }

        #region ЦОЗ список созданных заданий

        /// <summary>
        /// Цоз список заданий на испытания
        /// </summary>
        /// <returns></returns>
        public ActionResult TaskList()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult GetTaskList([DataSourceRequest] DataSourceRequest request)
        {
            var organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var userId = UserHelper.GetCurrentEmployee().Id;
            var list = repo.GetTaskList(organizationId, userId);
            var result = list.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult EditTask(Guid taskId)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var taskExecutor = repo.GetTaskExecutor(taskId, userId);

            if (taskExecutor.StageId == CodeConstManager.STAGE_OBK_COZ)
            {
                var taskViewModel = repo.EditTask(taskId);
                ViewBag.SendToIC = taskViewModel.SentToIC == null;
                ViewBag.CozAccept = taskViewModel.CozExecutorName != null;

                return PartialView(taskViewModel);
            }

            if (taskExecutor.StageId == CodeConstManager.STAGE_OBK_ICL)
            {
                var taskReseachCenter = repo.EditTaskResearchCenter(taskId);
                
                ViewData["StorageConditions"] = new SelectList(repo.GetMaterialCondition("storage"), "Id", "NameRu");
                ViewData["ExternalConditions"] = new SelectList(repo.GetMaterialCondition("external"), "Id", "NameRu");

                string[] codeResearchCenter = { "researchcenter5", "researchcenter3", "researchcenter6", "researchcenter4", "researchcenter7" };
                ViewData["Researchcenters"] = new SelectList(repo.GetUnits(codeResearchCenter), "Id", "Name");
                return PartialView("TaskResearchCenter", taskReseachCenter);
            }
            else
            {
                OBKTaskListViewModel taskList = new OBKTaskListViewModel();
                return PartialView(taskList);
            }
        }

        /// <summary>
        /// принять образцы
        /// </summary>
        /// <param name="taskListViewModel"></param>
        /// <returns></returns>
        public ActionResult AcceptTaskList(OBKTaskListViewModel taskListViewModel)
        {
            repo.AcceptTaskList(taskListViewModel);
            return Json(new { isSuccess = true });
        }

        #endregion


        #region ИЦл/ИЛ(Маржан) распределение заданий по ИЦ

        public ActionResult SendToReseachCenter(OBKTaskResearchCenter taskResearchCenter)
        {
            var result = repo.SendToReseachCenter(taskResearchCenter);
            return Json(new { isSuccess = result });
        }

        public ActionResult ShowManagerResearchCenter(Guid taskId)
        {
            var results = repo.GetManagerResearchCenter(taskId);
            return PartialView(results);
        }

        public ActionResult SaveManagerLaboratory(OBKManagerResearchCenter managerResearchCenter)
        {
            repo.SaveManagerLaboratory(managerResearchCenter);
            return Json(new {isSuccess = true});
        }

        #endregion



        #region Испытательный центр

        public ActionResult ResearchCenter()
        {
            return PartialView(Guid.NewGuid());
        }

        public ActionResult ResearchCenterList([DataSourceRequest] DataSourceRequest request)
        {
            var filterStatus = ModifyFilters(request.Filters);

            //var organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var departamentId = UserHelper.GetDepartment().Id;
            var userId = UserHelper.GetCurrentEmployee().Id;
            //var list = repo.GetCenterListViews(departamentId, userId);
            var list = repo.GetResearchCenterListView(departamentId, userId, filterStatus);
            var result = list.ToDataSourceResult(request);
            return Json(result);
        }

        private string ModifyFilters(IEnumerable<IFilterDescriptor> filters)
        {
            if (filters.Any())
            {
                foreach (var filter in filters)
                {
                    var descriptor = filter as FilterDescriptor;
                    if (descriptor != null && descriptor.Member == "StageStatusCode")
                    {
                        return descriptor.Value.ToString();
                    }
                }
            }
            return null;
        }

        public ActionResult EditResearchCenter(Guid taskId, Guid unitLabId, string stautsCode)
        {
            switch (stautsCode)
            {
                case OBK_Ref_StageStatus.New:
                    //todo добавить статус и вызывать разные view
                    var model = repo.EditResearchCenter(taskId, unitLabId);
                    return PartialView(model);
                case OBK_Ref_StageStatus.InWork:
                case OBK_Ref_StageStatus.InReWork:
                    var result = repo.EditExpertResearchCenter(taskId, unitLabId);
                    return PartialView("EditExpertResearchCenter", result);
                case OBK_Ref_StageStatus.RequiresSigning:
                case OBK_Ref_StageStatus.Completed:
                    var resposnse = repo.EditChiefResearchCenter(taskId, unitLabId);
                    return PartialView("EditChiefResearchCenter", resposnse);
            }
            return PartialView(null);
        }

        public ActionResult SendToExecutorReseachCenter(OBKTaskResearchCenter taskResearchCenter)
        {
            repo.SendToExecutorReseachCenter(taskResearchCenter);
            return Json(new {isSuccess = true});
        }

        public ActionResult SubTaskResult(Guid taskMaterialId, bool isNew)
        {
            var model = repo.SubTaskResult(taskMaterialId, isNew);
            if (isNew)
            {
                ViewData["LabMarks"] = new SelectList(repo.GetLaboratoryMark(), "Id", "NameRu");
                ViewData["LabNdMarks"] = new SelectList(repo.GetLaboratoryMark(), "Id", "NameRu");
                var booleans = new ReadOnlyDictionaryRepository().GetUOBKCheck();
                ViewData["ExpertiseResults"] = new SelectList(booleans, "ExpertiseResult", "Name", "");
            }
            else
            {
              
                ViewData["LabMarks"] = new SelectList(repo.GetLaboratoryMark(), "Id", "NameRu");
                var booleans = new ReadOnlyDictionaryRepository().GetUOBKCheck();
                ViewData["ExpertiseResults"] = new SelectList(booleans, "ExpertiseResult", "Name");
            }
            return PartialView(model);
        }

        public ActionResult SaveSubTaskResult(OBKSubTaskResult subTaskResult)
        {
            var result = repo.SaveSubTaskResult(subTaskResult);
            return Json(new { isSuccess = result });
        }

        public ActionResult GetSignSubTaskExpert(Guid id)
        {
            var signData = repo.GetSubTaskSignDataExpert(id);
            return Json(new { data = signData }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveSignedSubTaskExpert(Guid id, string signedData)
        {
            var result = repo.SaveSignedSubTaskExpert(id, signedData);
            return Json(new { isSuccess = result });
        }

        public ActionResult GetSignTaskChief(Guid taskId)
        {
            var signData = repo.GetTaskSignDataChief(taskId);
            return Json(new { data = signData }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSignedTaskChief(Guid taskId, string signedData)
        {
            var result = repo.SaveSignedTaskChief(taskId, signedData);
            return Json(new { isSuccess = result });
        }

        public ActionResult GetRegulation(Guid id)
        {
            var result = repo.GetRegulation(id);
            SelectList maskNd = new SelectList(result, "Id", "NameRu");
            return Json(maskNd);
        }

        public ActionResult SendToChief(Guid taskId)
        {
            var result = repo.SendToChief(taskId);
            return Json(new {isSuccess = result}, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}