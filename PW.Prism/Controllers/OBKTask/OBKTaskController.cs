using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Aspose.BarCode;
using Aspose.Words;
using Aspose.Words.Drawing;
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
using PW.Prism.Helpers;
using Stimulsoft.Report;

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
            //string[] code = { "00" };
            //заявление
            var declarant = repo.GetAssessmentDeclaration(id);
            if (declarant == null)
            {
                ViewBag.Message = "Заявка не найдена";
                return PartialView("ShowMessage");
            }
            ViewBag.DecalrationType = declarant.TypeId;
            var tasks = repo.GetTasks(id);
            var taskMaterial = repo.GetTaskMaterial(tasks);
            ViewData["Units"] = new SelectList(repo.GetUnits(OrganizationConsts.Filials), "Id", "ShortName");
            ViewData["ActReceptions"] = new SelectList(declarant.OBK_ActReception, "Id", "Number");

            var task = new OBKCreateTaskViewModel
            {
                AssessmentDeclarationId = id
            };

            var labs = repo.GeLaboratoryTypes();
            task.LaboratoryTypeList = new MultiSelectList(labs, "Id", "NameRu", task.LaboratoryTypeIds);

            var productViewModel = new List<OBKProductViewModel>();
            var modalViewModel = new List<OBKProductViewModel>();

            var taskViewModel = new List<OBKTaskViewModel>();
            foreach (var t in tasks)
            {
                var tvm = new OBKTaskViewModel
                {
                    TaskNumber = t.TaskNumber,
                    RegisterDate = t.RegisterDate,
                    ExecutorName = t.OBK_TaskExecutor.FirstOrDefault(e=>e.TaskId == t.Id && e.StageId == 3)?.Employee.ShortName,
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
                        TaskNumber = tasks.Any() ? productSeries.OBK_TaskMaterial?.Where(e => e.ProductSeriesId == productSeries.Id).Select(x => x.OBK_Tasks.TaskNumber).FirstOrDefault() : null,
                        LaboratoryName = tasks.Any() ? string.Join(", ", taskMaterial.Where(e => e.ProductSeriesId == productSeries.Id).Select(x => x.OBK_Ref_LaboratoryType.NameRu).GroupBy(x => x).Select(g => g.Key)) : null
                    };
                    productViewModel.Add(productView);

                    var count = taskMaterial.Count(e => e.ProductSeriesId == productSeries.Id);

                    // для создания задания(не отмеченные ранее)
                    if (count != 0) continue;
                    var modalView = new OBKProductViewModel
                    {
                        Id = productSeries.Id,
                        ProductId = product.Id,
                        ActReceptionId = product.ActReceptionId,
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
            ViewData["Executors"] = new SelectList(repo.GetExecutors(code, id), "Id", "DisplayName");
            var model = new OBKShowModalExecutor
            {
                Id = id,
                Code = code
            };
            return PartialView(model);
        }

        public ActionResult ShowModalFilialExecutor(Guid adId)
        {
            var results = repo.GetFilialExecutors(adId);
            ViewBag.AssessmentDeclarationId = adId;
            return PartialView(results);
        }

        [HttpPost]
        public ActionResult SendToCoz(OBKManagerResearchCenter filialExecutors)//(Guid adId,List<ManagerLaboratories> filialExecutors)
        {
            repo.SendToCoz(filialExecutors);
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
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
            //var organizationId = UserHelper.GetCurrentEmployee().OrganizationId;
            var userId = UserHelper.GetCurrentEmployee().Id;
            var list = repo.GetTaskList(userId); //organizationId
            var result = list.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult EditTask(Guid taskId)
        {
            var userId = UserHelper.GetCurrentEmployee().Id;
            var taskExecutor = repo.GetTaskExecutor(taskId, userId);

            switch (taskExecutor.StageId)
            {
                case CodeConstManager.STAGE_OBK_COZ:
                    var taskViewModel = repo.EditTask(taskId);
                    ViewBag.SendToIC = taskViewModel.SentToIC == null;
                    ViewBag.CozAccept = taskViewModel.CozExecutorName != null;
                    return PartialView(taskViewModel);
                case CodeConstManager.STAGE_OBK_ICL:
                    var taskReseachCenter = repo.EditTaskResearchCenter(taskId);
                    return PartialView("TaskResearchCenter", taskReseachCenter);
                default:
                    var taskList = new OBKTaskListViewModel();
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

        public ActionResult ExportTaskFilePdf(Guid taskId, string taskNumber)
        {
            var report = new StiReport();
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Mrts/OBK/OBKTask.mrt");
            report.Load(path);
            report.Compile();
            report.RegBusinessObject("taskModel", repo.GetTaskReportData(taskId));
            report.Render(false);
            Stream stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", $"Задание №{taskNumber}.pdf");
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
            repo.GenerateSubTaskNumber(managerResearchCenter.TaskId);
            return Json(new {isSuccess = true});
        }

        public ActionResult AcceptTaskReseachCenter(Guid taskId)
        {
            repo.AcceptTaskReseachCenter(taskId);
            return Json("OK", JsonRequestBehavior.AllowGet);
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

            var departamentId = UserHelper.GetDepartment().Id;
            var userId = UserHelper.GetCurrentEmployee().Id;

            var list = repo.GetResearchCenterListView(departamentId, userId, filterStatus);
            var result = list.ToDataSourceResult(request);
            return Json(result);
        }

        private string ModifyFilters(IEnumerable<IFilterDescriptor> filters)
        {
            if (!filters.Any()) return null;
            foreach (var filter in filters)
            {
                var descriptor = filter as FilterDescriptor;
                if (descriptor != null && descriptor.Member == "StageStatusCode")
                {
                    return descriptor.Value.ToString();
                }
            }
            return null;
        }

        public ActionResult EditResearchCenter(Guid taskId, Guid unitLabId, string stautsCode)
        {
            switch (stautsCode)
            {
                case OBK_Ref_StageStatus.New:
                    var model = repo.EditResearchCenter(taskId, unitLabId);
                    return PartialView(model);
                case OBK_Ref_StageStatus.InWork:
                    var result = repo.EditExpertResearchCenter(taskId, unitLabId);
                    return PartialView("EditExpertResearchCenter", result);
                case OBK_Ref_StageStatus.RequiresSigning:
                case OBK_Ref_StageStatus.InReWork:
                case OBK_Ref_StageStatus.OnApprove:
                case OBK_Ref_StageStatus.Completed:
                    var resposnse = repo.EditChiefResearchCenter(taskId, unitLabId, stautsCode);
                    return PartialView("EditChiefResearchCenter", resposnse);
                default:
                    var history = repo.EditTaskHistory(taskId);
                    return PartialView("EditTaskHistory", history);
            }
        }

        public ActionResult SendToExecutorReseachCenter(OBKTaskResearchCenter taskResearchCenter)
        {
            repo.SendToExecutorReseachCenter(taskResearchCenter);
            return Json(new {isSuccess = true});
        }

        public ActionResult SubTaskResult(Guid taskMaterialId, string type)
        {
            var model = repo.SubTaskResult(taskMaterialId, type);
            if (type == "create")
            {
                ViewData["LabMarks"] = new SelectList(repo.GetLaboratoryMark(), "Id", "NameRu");
                ViewData["LabNdMarks"] = new SelectList(repo.GetLaboratoryMark(), "Id", "NameRu");
                var booleans = new ReadOnlyDictionaryRepository().GetUOBKCheck();
                ViewData["ExpertiseResults"] = new SelectList(booleans, "ExpertiseResult", "Name", "");
            }
            else
            {
                ViewData["LabMarks"] = new SelectList(repo.GetLaboratoryMark(), "Id", "NameRu");
                ViewData["LabMaskNd"]  = new SelectList(repo.GetRegulation(), "Id", "NameRu");
                var booleans = new ReadOnlyDictionaryRepository().GetUOBKCheck();
                ViewData["ExpertiseResults"] = new SelectList(booleans, "ExpertiseResult", "Name");
            }
            return PartialView(model);
        }

        public ActionResult SaveSubTaskResult(OBKSubTaskResult subTaskResult)
        {
            var result = repo.SaveSubTaskResult(subTaskResult);
            return Json(new { result });
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
            var maskNd = new SelectList(result, "Id", "NameRu");
            return Json(maskNd);
        }

        public ActionResult SendToChief(Guid taskId)
        {
            var result = repo.SendToChief(taskId);
            return Json(new {isSuccess = result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowModalTaskDetails(Guid taskId, int productSeriesId, int executorCode)
        {
            var model = repo.GetTaskDetails(taskId, productSeriesId, executorCode);
            return PartialView(model);
        }

        public ActionResult ReturnToExecutor(Guid tid)
        {
            repo.ReturnToExecutor(tid);
            return Json(new { isSuccess = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveSubTaskRemark(List<SubTaskIndicator> stis)
        {
            repo.SaveSubTaskRemark(stis);
            return Json(new { isSuccess = true });
        }
        [HttpPost]
        public ActionResult GetInstuctionCount(int registerId)
        {
            var instructionCount = InstructionFileHelper.GetInstructionFileCount(registerId);
            return Json(instructionCount, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Полчение файла АНД
        /// </summary>
        /// <param name="registerId"></param>
        /// <returns></returns>
        public ActionResult GetInstruction(int registerId)
        {
            var name = "doc_" + registerId + ".zip";
            var file = InstructionFileHelper.GetInstructionFile(registerId);
            return File(file, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }

        [Authorize]
        public ActionResult SubTaskProtokol(int psId)
        {
            var report = new StiReport();
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Mrts/OBK/TaskProtocol.mrt");
            report.Load(path);
            report.Compile();
            report.RegBusinessObject("tmp", repo.GetTaskProtocolData(psId));
            report.RegBusinessObject("tmpExecutors", repo.GetTaskProtocolExecutorListData(psId));
            report.Render(false);
            Stream stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Word2007, stream);
            stream.Position = 0;
            return File(stream, "application/msword", $"Протокол.docx");
        }

        #endregion
    }
}