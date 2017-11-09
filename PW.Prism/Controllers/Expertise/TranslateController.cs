using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Ncels.Core.ActivityManager;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Notifications;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using PW.Ncels.Database.Repository.Security;

namespace PW.Prism.Controllers.Expertise
{
    public class TranslateController : DrugPrimaryController
    {
        public ActionResult TranslatePageView(Guid id)
        {
            var expertiseStage = new ExpertiseStageRepository().GetById(id);
            if (expertiseStage == null)
            {
                return HttpNotFound();
            }
            var repository = new ReadOnlyDictionaryRepository();
            var primaryRepository = new DrugPrimaryRepository();

            var model = new TranslateEntity
            {
                EXP_DrugDeclaration = expertiseStage.EXP_DrugDeclaration,
                DrugDeclarationId = expertiseStage.DeclarationId.ToString(),
                Applicant = new EmployeesRepository().GetById(expertiseStage.EXP_DrugDeclaration.OwnerId),
                Editor = UserHelper.GetCurrentEmployee(),
                ExpExpertiseStageRemarks = new List<EXP_ExpertiseStageRemark>(),
                ExpStageId = expertiseStage.Id,
                CurrentStage = new StageModel()
                {
                    CurrentStage = GetExpertiseStage(expertiseStage.Id),
                    StageResults = repository.GetStageResultsByStage(expertiseStage.StageId)
                }
            };
            var employeName = "";
            if (UserHelper.GetCurrentEmployee() != null)
            {
                employeName = UserHelper.GetCurrentEmployee().DisplayName;
            }
            ViewBag.CurrentEmployeeName = employeName;
            var markList = primaryRepository.GetPrimaryMarkList(expertiseStage.DeclarationId, CodeConstManager.STAGE_TRANSLATE);
            var remarkTypes = repository.GetRemarkTypes().ToArray();
            ViewData["RemarkTypes" + model.DrugDeclarationId] = new SelectList(remarkTypes, "Id", "NameRu",
                null);
            foreach (var expDrugPrimaryRemark in markList)
            {
                model.ExpExpertiseStageRemarks.Add(expDrugPrimaryRemark);
            }
            foreach (var remark in model.ExpExpertiseStageRemarks)
            {
                ViewData["RemarkTypes" + model.DrugDeclarationId + "_" + remark.Id] = new SelectList(remarkTypes, "Id", "NameRu",
                    remark.RemarkTypeId);
            }
            model.ExpDrugCorespondences = primaryRepository.GetDrugCorespondences(expertiseStage.DeclarationId);
            return PartialView(model);
        }
        public override ActionResult AppDosage(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dosageRepository = new AppDosageRepository();
            var model = dosageRepository.GetStageByAppDosageId(id.Value);
            if (model == null)
            {
                return HttpNotFound();
            }
            FillDosageControl(model);
            var safetyReportDosageStage = dosageRepository.GetStageDosage(model.DosageId, CodeConstManager.STAGE_SAFETYREPORT);
            model.ExpertiseSafetyreportFinalDoc = safetyReportDosageStage != null ? safetyReportDosageStage.EXP_ExpertiseSafetyreportFinalDoc.FirstOrDefault() : null;
            model.EXP_DrugDosage.EXP_DrugDeclaration.ExpDicPrimaryOtds = new ReadOnlyDictionaryRepository().GetExpDicPrimaryOTDs().Where(e => e.ParentId == null).ToList();
            var repository = new ReadOnlyDictionaryRepository();
            ViewData["FinalyDocResultList" + model.EXP_DrugDosage.DrugDeclarationId] = new SelectList(repository.GetStageResultsByStage(model.EXP_ExpertiseStage.StageId), "Id", "NameRu",
                model.ResultId);

            var stageName = ExpStageNameHelper.GetName(GetStage());
            ActionLogger.WriteInt(stageName + ": Получение заявки №" + model.EXP_DrugDosage.RegNumber);
            return PartialView("~/Views/DrugDeclaration/AppDosage.cshtml", model);
        }
        [HttpPost]
        public override ActionResult CloneDosageFinalDoc(Guid dosageId)
        {
            new SafetyreportRepository().CloneDosageFinalDoc(dosageId);
            return Json(new { Success = true });
        }

        [HttpPost]
        public override ActionResult UpdateFinalDocument(string fieldName, string fieldValue, Guid objectId)
        {
            var finalDoc = new SafetyreportRepository().UpdateFinalDocument(fieldName, fieldValue, objectId, UserHelper.GetCurrentEmployee().Id, true );
            return Json(new { Success = true, DocId = finalDoc.Id, Status = finalDoc.EXP_ExpertiseStageDosage.FinalDocStatus });
        }
        public override int GetStage()
        {
            return CodeConstManager.STAGE_TRANSLATE;
        }

        [HttpGet]
        public ActionResult SendTranslateOnAgreement(Guid docId, string documentType, string taskType = null)
        {
            ViewBag.UiId = Guid.NewGuid();
            ViewBag.DocumentTypeCode = documentType;
            ViewBag.TaskType = taskType;
            return PartialView(docId);
        }
        [HttpPost]
        public ActionResult SendTranslateOnAgreement(Guid docId, Guid executorId, string documentType, string taskType = null)
        {
            var activityManager = new ActivityManager();
            var file = new UploadRepository().GetFileLinkById(docId);
            var declaration =  GetDrugDeclarationById(file.DocumentId.ToString());

            switch (documentType)
            {
                case "1": // Согласование документа на перевод
                    {
                        activityManager.SendToExecution(Dictionary.ExpActivityType.TranslateDocAgreement, docId,
                 Dictionary.ExpAgreedDocType.TranslateFile, taskType ?? Dictionary.ExpTaskType.Agreement,
                  declaration.Number, declaration.CreatedDate, executorId);
                        break;
                }
                case "2": // Согласование макета
                    {
                        activityManager.SendToExecution(Dictionary.ExpActivityType.MaketDocAgreement, docId,
                 Dictionary.ExpAgreedDocType.MaketFile, taskType ?? Dictionary.ExpTaskType.Agreement,
                  declaration.Number, declaration.CreatedDate, executorId);
                        break;
                    }
            }
            var primaryDocStatus = new UploadRepository().GetFileLinkById(docId).DIC_FileLinkStatus;
            return Json(primaryDocStatus.NameRu, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SendToApplicant(string id)
        {
            var model = new TranslateRepository().SendToApplicant(id, UserHelper.GetCurrentEmployee());
            string statusName;
            if (model != null)
            {
                statusName = model.DIC_FileLinkStatus.NameRu;
            }
            else
            {
                statusName = null;
            }
            return Json(new
            {
                isSuccess = true,
                statusName
            });
        }
        public override ActionResult CreateRemark(string id)
        {
            var isSuccess = new DrugPrimaryRepository().CreateRemark(id, UserHelper.GetCurrentEmployee(), CodeConstManager.STAGE_TRANSLATE);
            return Json(new
            {
                isSuccess
            });
        }


    }
}