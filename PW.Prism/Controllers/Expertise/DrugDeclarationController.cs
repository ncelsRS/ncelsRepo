using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ncels.Core.ActivityManager;
using Ncels.Helpers;
using Newtonsoft.Json;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Controller;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using PW.Ncels.Database.Repository.Security;
using PW.Prism.ViewModels.DrugDeclaration;
using PW.Prism.ViewModels.Expertise;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers.Expertise
{
    public class DrugDeclarationController : APrismDrugDeclaraionController
    {

        public override int GetStage()
        {
            return CodeConstManager.STAGE_COZ;
        }
        public ActionResult ListRegister([DataSourceRequest] DataSourceRequest request, string type, int stage, DeclarationRegistryFilter customFilter = null)
        {
            var stageName = ExpStageNameHelper.GetName(stage);
            ActionLogger.WriteInt(stageName + ": Получение списка заявлений");
            var list = new DrugDeclarationRepository().DrugDeclarationRegisterByStatus(type, stage, UserHelper.GetCurrentEmployee().Id, customFilter);
            var result = list.ToDataSourceResult(request);
            return Json(result);
        }

        public ActionResult DocumentReview(Guid id)
        {
            //            var repository = new DrugDeclarationRepository();
            //            var model = repository.GetByViewId(id.ToString());
            //            var setting = new SettingsRepository().GetByCode("Stage1");
            //            var employe = new EmployeesRepository().GetById(setting.UserId);
            //            model.CustomExecutorsId = DictionaryHelper.GetItems(employe.Id.ToString(), employe.DisplayName);

            return PartialView(id);
        }

        public ActionResult DocumentReviewConfirm(Guid? id)
        {
            if (id == null) return Json("Ok!", JsonRequestBehavior.AllowGet);
            var expertise = GetExpertiseStage(id);
            var model = expertise.EXP_DrugDeclaration;
            if (model == null) return Json("Ok!", JsonRequestBehavior.AllowGet);
            model.StatusId = CodeConstManager.STATUS_EXP_SEND_ID;
            new DrugDeclarationRepository().Update(model);
            var history = new EXP_DrugDeclarationHistory()
            {
                DateCreate = DateTime.Now,
                DrugDeclarationId = model.Id,
                StatusId = model.StatusId,
                UserId = UserHelper.GetCurrentEmployee().Id,
            };
            new DrugDeclarationRepository().SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            var stageRepository = new ExpertiseStageRepository();
            string resultDescription;
            if (!stageRepository.HasStage(model.Id, CodeConstManager.STAGE_PRIMARY))
                stageRepository.ToNextStage(model.Id, null, new[] { CodeConstManager.STAGE_PRIMARY }, out resultDescription);

            return Json("Ok!", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reject(Guid id, string note)
        {
            var model = GetExpertiseStage(id);
            model.EXP_DrugDeclaration.DesignNote = note;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult RejectConfirm(Guid? id, string note)
        {

            if (id != null)
            {
                var expertise = GetExpertiseStage(id);
                var model = expertise.EXP_DrugDeclaration;
                if (model != null)
                {
                    model.DesignNote = note;
                    model.DesignDate = DateTime.Now;
                    model.StatusId = CodeConstManager.STATUS_REJECT_ID;
                    new DrugDeclarationRepository().Update(model);
                    var history = new EXP_DrugDeclarationHistory()
                    {
                        DateCreate = DateTime.Now,
                        DrugDeclarationId = model.Id,
                        StatusId = model.StatusId,
                        UserId = UserHelper.GetCurrentEmployee().Id,
                        Note = model.DesignNote
                    };
                    new DrugDeclarationRepository().SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
                }
            }
            return Json("Ok!", JsonRequestBehavior.AllowGet);
        }
        public ActionResult DocumentRead(Guid id)
        {
            var model = new DrugDeclarationRepository().GetByViewId(id.ToString());
            return Content(JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings() { DateFormatString = "dd.MM.yyyy HH:mm" }));
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Design(Guid[] id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = GetExpertiseStage(id[0]);
            model.EXP_DrugDeclaration.Applicant = new EmployeesRepository().GetById(model.EXP_DrugDeclaration.OwnerId);
            ViewBag.CanRefuseExpertise =
                model.StageId == CodeConstManager.STAGE_PRIMARY /*&& model.EXP_DIC_StageStatus.Code != EXP_DIC_StageStatus.Completed*/ &&
                model.EXP_DrugDeclaration.StatusId != CodeConstManager.STATUS_EXP_ON_REFUSING_ID
                && model.EXP_DrugDeclaration.StatusId != CodeConstManager.STATUS_EXP_REFUSED_ID;
            FillDeclarationControl(model.EXP_DrugDeclaration);
            if (model == null)
            {
                return HttpNotFound();
            }
            var stageName = ExpStageNameHelper.GetName(model.StageId);
            ActionLogger.WriteInt(stageName + ": Получение заявления №" + model.EXP_DrugDeclaration.Number);
            return PartialView(model);
        }

        /// <summary>
        /// Установить исполнителя
        /// </summary>
        /// <param name="id">список заявлении</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetExecuter()
        {
            return PartialView(Guid.NewGuid());
        }
        [HttpPost]
        public ActionResult SetExecuter(Guid[] stages, Guid[] executors)
        {
            var expRepository = new ExpertiseStageRepository();
            expRepository.SendToWork(stages, executors);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAttachList(string id = null, string type = null, bool byMetadata = false)
        {
            var db = UserHelper.GetCn();
            return Json(FileHelper.GetAttachList(db, id, type, byMetadata), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AttachFileView(Guid? id)
        {
            var repository = new UploadRepository();
            var list = repository.GetAttachListEdit(id, CodeConstManager.ATTACH_DRUG_FILE_CODE);
            return PartialView(list);
        }

        /// <summary>
        /// При выборе значение Наименование вещества
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult GenerateNd(string modelId, int? typeId)
        {

            var numder = new DrugDeclarationRepository().GenerateNd(modelId, UserHelper.GetCurrentEmployee().Id.ToString(), typeId);

            if (numder == null)
            {
                return Json(new
                {
                    isSuccess = false
                });
            }
            return Json(new
            {
                numder,
                isSuccess = true
            });
        }

        public ActionResult DeclarationSearch()
        {
            var db = new ncelsEntities();
            ViewBag.EXP_DIC_Type = db.EXP_DIC_Type.ToList().Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = e.NameRu
            }).ToList();
            return PartialView(Guid.NewGuid());
        }
        [HttpGet]
        public ActionResult SendExpertiseDocumentToAgreement(Guid docId, string documentType, string taskType = null)
        {
            ViewBag.UiId = Guid.NewGuid();
            ViewBag.DocumentTypeCode = documentType;
            ViewBag.TaskType = taskType;
            return PartialView(docId);
        }

        [HttpPost]
        public ActionResult SendExpertiseDocumentToAgreement(Guid docId, Guid executorId, string documentType, string taskType = null)
        {
            taskType = string.IsNullOrEmpty(taskType) ? null : taskType;
            var db = new ncelsEntities();
            var repository = new DrugDeclarationRepository();
            var activityManager = new ActivityManager();
            switch (documentType)
            {
                case Dictionary.ExpAgreedDocType.EXP_DrugFinalDocument:
                    var declarationInfo = db.EXP_ExpertiseStageDosage.Where(e => e.Id == docId)
                        .Select(e => new { e.EXP_DrugDosage.RegNumber, e.EXP_DrugDosage.EXP_DrugDeclaration.CreatedDate }).FirstOrDefault();
                    activityManager.SendToExecution(Dictionary.ExpActivityType.FinalDocAgreement, docId,
                        Dictionary.ExpAgreedDocType.EXP_DrugFinalDocument, taskType ?? Dictionary.ExpTaskType.Agreement,
                        declarationInfo.RegNumber, declarationInfo.CreatedDate, executorId);
                    var primaryDocStatus = repository.GetPrimaryFinalDocumentStatus(docId);
                    return Json(primaryDocStatus.Name, JsonRequestBehavior.AllowGet);
                case Dictionary.ExpAgreedDocType.Letter:
                    var primaryRepo = new DrugPrimaryRepository();
                    bool isSigning = taskType == Dictionary.ExpTaskType.Signing;
                    var letter = primaryRepo.GetCorespondence(docId.ToString());
                    if (isSigning)
                    {
                        activityManager.SendToExecution(Dictionary.ExpActivityType.ExpertiseLetterSigning, docId,
                            Dictionary.ExpAgreedDocType.Letter, Dictionary.ExpTaskType.Signing,
                            letter.NumberLetter, letter.DateCreate, executorId);
                        return Json(
                            DictionaryHelper.GetDicItemByCode(CodeConstManager.STATUS_ONSIGNING,
                                CodeConstManager.STATUS_ONSIGNING).Name, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        activityManager.SendToExecution(Dictionary.ExpActivityType.ExpertiseLetterAgreement, docId,
                            Dictionary.ExpAgreedDocType.Letter, Dictionary.ExpTaskType.Agreement,
                            letter.NumberLetter, letter.DateCreate, executorId);
                        return Json(
                            DictionaryHelper.GetDicItemByCode(CodeConstManager.STATUS_ONAGREEMENT,
                                CodeConstManager.STATUS_ONAGREEMENT).Name, JsonRequestBehavior.AllowGet);
                    }
                  case Dictionary.ExpAgreedDocType.CertificateOfCompletion:
                    var coc = db.EXP_CertificateOfCompletion.FirstOrDefault(e => e.Id == docId);
                    if (coc != null)
                    {
                        activityManager.SendToExecution(Dictionary.ExpActivityType.CertificateOfComplitionAgreement, docId,
                           Dictionary.ExpAgreedDocType.CertificateOfCompletion, taskType ?? Dictionary.ExpTaskType.Agreement,
                           string.Empty, coc.CreateDate, executorId);
                        //var primaryDocStatus = repository.GetPrimaryFinalDocumentStatus(docId);
                    }
                    return Json(DictionaryHelper.GetDicItemByCode(CodeConstManager.STATUS_ONAGREEMENT,
                                CodeConstManager.STATUS_ONAGREEMENT).Name, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult GetRdDocumentsHistory(Guid id)
        {
            var repository = new UploadRepository();
            ViewBag.Path = id;
            var list = repository.GetAttachListEdit(id, CodeConstManager.ATTACH_DRUG_FILE_CODE, needFillFilesStages: true);
            return PartialView(list);
        }

        public ActionResult GetRdDocumentsHistoryInstruction(Guid id)
        {
            string instrCode = "4";
            var repository = new UploadRepository();
            ViewBag.Path = id;
            var item = repository.GetAttachListbyCode(id, CodeConstManager.ATTACH_DRUG_FILE_CODE, instrCode);
            return PartialView(item);
        }

        public ActionResult GetRdDocumentsHistoryMaket(Guid id)
        {
            string maketCode = "3";
            var repository = new UploadRepository();
            ViewBag.Path = id;
            var item = repository.GetAttachListbyCode(id, CodeConstManager.ATTACH_DRUG_FILE_CODE, maketCode);
            return PartialView(item);
        }

        public ActionResult GetRdDocumentsHistoryAnd(Guid id)
        {
            string andCode = "5";
            var repository = new UploadRepository();
            ViewBag.Path = id;
            var item = repository.GetAttachListbyCode(id, CodeConstManager.ATTACH_DRUG_FILE_CODE, andCode);
            return PartialView(item);
        }

        public Object DeclarationStepsNew(string id)
        {
            try
            {
                var id2 = Guid.Parse(id);
                ViewBag.DeclarationId = id;
                var steps = new List<DeclarationStepsModel>();
                int priorityInList = 0;
                using (var db = new ncelsEntities())
                {
                    var currentEmployeeGuid = UserHelper.GetCurrentEmployee().Id;
                    var stages = db.EXP_ExpertiseStage.Where(x => x.DeclarationId == id2);
                    var expStageRep = new ExpertiseStageRepository();
                    foreach (var item in stages.OrderBy(x => x.EXP_DIC_Stage.Code))
                    {

                        var isEndedStage = item.FactEndDate.HasValue;
                        var dbStatus = item.EXP_DIC_StageStatus;
                        var statusCode = dbStatus.Code;
                        var statucCorrectText = GetCostilStatusByStageCode(statusCode);

                        //kostil dlya nachalnikov
                        var executeLeader = expStageRep.GetExecutorByDicStageId(item.StageId);
                        var currentStageCode = item.EXP_DIC_Stage.Code;



                        var leaderStep = new DeclarationStepsModel();
                        leaderStep.ExecutorShortName = executeLeader.ShortName;
                        leaderStep.StepName = item.EXP_DIC_Stage.NameRu;
                        leaderStep.IsLeaderStep = true;
                        leaderStep.Status = statucCorrectText;
                        leaderStep.StageCode = currentStageCode;
                        leaderStep.NeedWorkers = (statusCode == "inQueue");
                        leaderStep.PriorityInList = priorityInList;
                        priorityInList++;
                        leaderStep.DateStart = item.StartDate.HasValue ? item.StartDate.Value.ToShortDateString() : "";
                        steps.Add(leaderStep);

                        if (currentStageCode != EXP_DIC_Stage.ProcCenter)
                        {
                            var step = new DeclarationStepsModel();
                            step.Id = item.Id;
                            step.AllowAddWorkers = (currentEmployeeGuid == executeLeader.Id);
                            step.StageId = item.StageId;
                            step.StepName = item.EXP_DIC_Stage.NameRu;
                            step.NeedWorkers = (statusCode == "inQueue");
                            step.StageCode = currentStageCode;
                            step.PriorityInList = priorityInList;
                            priorityInList++;
                            step.Status = statucCorrectText;
                            step.ExecutorShortName = item.Executors.Select(x => x.ShortName)
                                .Aggregate((i, j) => i + Environment.NewLine + j);
                            if (item.StartDate != null && item.EndDate != null)
                            {
                                var dayDiff = (item.EndDate.Value - item.StartDate.Value).TotalDays;
                                step.DueToAllDays = (int)dayDiff;
                                step.DateStart = item.StartDate.Value.ToShortDateString();
                                if (isEndedStage)
                                {
                                    step.DateEnd = item.FactEndDate.Value.ToShortDateString();
                                }
                                step.ControlDate = item.EndDate.Value.ToShortDateString();
                                var daysLeave = (item.EndDate.Value - DateTime.Now.Date).TotalDays;
                                step.DueToEndDays = (int)daysLeave;
                                step.StepIsEnded = isEndedStage;
                                int? totalDaysOverdue = null;
                                if (isEndedStage)
                                {
                                    totalDaysOverdue = (int)(item.FactEndDate.Value - item.EndDate.Value).TotalDays;
                                }
                                else
                                {
                                    totalDaysOverdue = (int)(DateTime.Now.Date - item.EndDate.Value).TotalDays;
                                }
                                var isOverdue = isEndedStage && totalDaysOverdue > 0;
                                step.StepIsOverdue = isOverdue;
                                step.OverdueDays = totalDaysOverdue;
                            }
                            steps.Add(step);
                        }



                    }
                }
                var model = new DeclarationStepsViewModel();
                model.Steps = steps.OrderBy(x => x.PriorityInList).ToList();
                model.DeclarationId = id2;
                var views = RenderRazorViewToString(View("DeclarationStepsNew").ViewName, model, ViewData, TempData,
                    ControllerContext);
                return views;

            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("DeclarationSteps ex:" + ex.Message + " \r\nstack:" + ex.StackTrace);
                throw;
            }
        }

        public static string GetCostilStatusByStageCode(string code)
        {
            switch (code)
            {
                case EXP_DIC_StageStatus.New:
                    return "Новое";
                case EXP_DIC_StageStatus.InWork:
                    return "В работе";
                case EXP_DIC_StageStatus.InReWork:
                    return "На доработке";
                case EXP_DIC_StageStatus.Completed:
                    return "Выполнено";
                default:
                    return "";
            }
        }

        public static string RenderRazorViewToString(string viewName, object model, ViewDataDictionary viewData, TempDataDictionary tempData, ControllerContext controllerContext)
        {
            viewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        [HttpPost]
        public ActionResult SetStageResult(Guid stageId, int resultId)
        {
            var repository = new ExpertiseStageRepository();
            repository.SetStageResult(stageId, resultId);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult SendToNextStage(Guid expStageId, int dicStageId, int stageResultId)
        {
            var repo = new ExpertiseStageRepository();
            var stage = repo.GetStage(expStageId);
            ViewBag.ExpStageId = expStageId;
            ViewBag.DicStageId = dicStageId;
            ViewBag.StageResultId = stageResultId;
            ViewBag.RegType = stage.EXP_DrugDeclaration.TypeId;

            return PartialView(Guid.NewGuid());
        }
        [HttpPost]
        public ActionResult SendToNextStage(Guid expStageId, int[] nextStageIds, int? stageResultId = null)
        {
            var repository = new ExpertiseStageRepository();
            string resultDescription;

            var expertiseStage = repository.GetById(expStageId);
            var dec = repository.GetDeclarationByStage(expStageId);
            if (expertiseStage.EXP_DIC_Stage.Code == CodeConstManager.STAGE_SAFETYREPORT.ToString())
            {
                var rDictionary = new ReadOnlyDictionaryRepository();
                EXP_DIC_StageResult stageResult = null;
                if (stageResultId.HasValue)
                    stageResult = rDictionary.GetStageResultById(stageResultId.Value);

                if (stageResult != null && stageResult.Code == EXP_DIC_StageResult.DoesNotMatchCode)
                {
                    repository.ToBackStage(dec.Id, expStageId, nextStageIds, out resultDescription);
                }
                else
                {
                    repository.ToNextStage(dec.Id, expStageId, nextStageIds, out resultDescription);
                }
            }
            else
            {
                repository.ToNextStage(dec.Id, expStageId, nextStageIds, out resultDescription);
            }
            var from = ExpStageNameHelper.GetName(expertiseStage.StageId);
            var to = "";
            foreach (var nextStageId in nextStageIds)
            {
                if (!String.IsNullOrEmpty(to))
                {
                    to += ", ";
                }
                var stageName = ExpStageNameHelper.GetName(nextStageId);
                to += stageName;
            }
            ActionLogger.WriteInt(" Отправка заявления №" + dec.Number + " на другой этап", "from: " + from + "; to:" + to);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }


        //public JsonResult CheckAndUploadHistoryFile(string code = null, string path = null, bool saveMetadata = false, string originFileId = null,string fileTypeCode)
        //{
        //    var currentEmployeeGuid = UserHelper.GetCurrentEmployee().Id;
        //    using (var db = new ncelsEntities())
        //    {
        //        db.FileLinks.FirstOrDefault(x=>x.)
        //    }
        //}

        public FileStreamResult ExportFile(Guid id)
        {
            string name = "Документ.pdf";
            StiReport report = new StiReport();

            report.Load(Server.MapPath("~/Reports/DrugDeclaration/OutcomeDocCopy.mrt"));

            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            // имя и должность эксперта
            var currentEmployee = UserHelper.GetCurrentEmployee();
            var employeeName = currentEmployee.FullName;
            var employeePosition = currentEmployee.Position.Name;

            report.Dictionary.Variables["EmployeeName"].ValueObject = employeeName;
            report.Dictionary.Variables["EmployeePosition"].ValueObject = employeePosition;
            report.Dictionary.Variables["DrugPrimaryFinalDocumentId"].ValueObject = id;

            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }

        public ActionResult ExportFilePdf(Guid id)
        {
            var db = new ncelsEntities();
            string name = "Заявление на проведение экспертизы лс.pdf";
            StiReport report = new StiReport();
            try
            {
                report.Load(Server.MapPath("~/Reports/DrugDeclaration/DrugDeclaration.mrt"));
                foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
                {
                    data.ConnectionString = UserHelper.GetCnString();
                }

                report.Dictionary.Variables["DrugDeclarationId"].ValueObject = id;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex: " + ex.Message + " \r\nstack: " + ex.StackTrace);
            }
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            var drugDeclaration = db.EXP_DrugDeclaration.FirstOrDefault(dd => dd.Id == id);
            var drugDeclarationHistory = drugDeclaration.EXP_DrugDeclarationHistory.Where(dh => dh.XmlSign != null)
                .OrderByDescending(dh => dh.DateCreate).FirstOrDefault();
            //if (drugDeclarationHistory != null)
            //{
            //    Aspose.Words.Document doc = new Aspose.Words.Document(stream);
            //    doc.InserQrCodesToEnd("ExecutorSign", drugDeclarationHistory.XmlSign);
            //    var pdfFile = new MemoryStream();
            //    pdfFile.Position = 0;
            //    stream.Close();
            //    return new FileStreamResult(pdfFile, "application/pdf");
            //}
            return File(stream, "application/pdf", name);
        }

        public ActionResult ListCommissions([DataSourceRequest] DataSourceRequest request, Guid stageId, long dosageId)
        {
            var db = new ncelsEntities();
            return Json(db.EXP_ExpertiseStageDosageCommissionView.Where(e => e.StageId == stageId && e.DrugDosageId == dosageId).ToDataSourceResult(request));
        }

        public ActionResult EndExpertise(Guid stageId)
        {
            var repo = new ExpertiseStageRepository();
            repo.EndExpertise(stageId);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmeRefuseExpertise(Guid stageId)
        {
            return PartialView(stageId);
        }
        [HttpPost]
        public ActionResult RefuseInExpertise(Guid stageId)
        {
            var repo = new ExpertiseStageRepository();
            repo.StartRefuseInExpertise(stageId, true);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListStages(int currentDicStageId, int regTypeId, int stageResultId = 0)
        {
            var stages = (new ExpertiseStageRepository()).ListStages(currentDicStageId, regTypeId, stageResultId);
            return Json(stages.Select(e => new { e.Id, Name = e.NameRu }), JsonRequestBehavior.AllowGet);
        }
    }
}