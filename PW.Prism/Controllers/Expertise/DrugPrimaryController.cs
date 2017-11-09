using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Aspose.Words;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
using PW.Ncels.Database.Controller;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using PW.Ncels.Database.Models.Expertise;
using PW.Ncels.Database.Repository.Common;
using PW.Ncels.Database.Repository.Expertise;
using PW.Ncels.Database.Repository.Security;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace PW.Prism.Controllers.Expertise
{
    public class DrugPrimaryController : APrismDrugDeclaraionController
    {
        public override ActionResult AppDosage(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new AppDosageRepository().GetStageByAppDosageId(id.Value);
            if (model == null)
            {
                return HttpNotFound();
            }
            FillDosageControl(model);

            model.EXP_DrugDosage.EXP_DrugDeclaration.ExpDicPrimaryOtds = new ReadOnlyDictionaryRepository().GetExpDicPrimaryOTDs().Where(e => e.ParentId == null).ToList();
            model.EXP_ExpertisePrimaryFinalDoc = model.PrimaryFinalDocs.FirstOrDefault();
            if (model.EXP_ExpertisePrimaryFinalDoc == null)
            {
                model.EXP_ExpertisePrimaryFinalDoc = new EXP_ExpertisePrimaryFinalDoc();
                model.EXP_ExpertisePrimaryFinalDoc.EXP_ExpertiseStageDosage = model;
            }
            var repository = new ReadOnlyDictionaryRepository();
            ViewData["FinalyDocResultList" + model.EXP_DrugDosage.DrugDeclarationId] = new SelectList(repository.GetStageResultsByStage(model.EXP_ExpertiseStage.StageId), "Id", "NameRu",
            model.ResultId);

            var stageName = ExpStageNameHelper.GetName(GetStage());
            ActionLogger.WriteInt(stageName + ": Получение заявки №" + model.EXP_DrugDosage.RegNumber);

            return PartialView("~/Views/DrugDeclaration/AppDosage.cshtml", model);
        }
        public override int GetStage()
        {
            return CodeConstManager.STAGE_PRIMARY;
        }
        public ActionResult PrimaryPageView(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var expertiseStage = new ExpertiseStageRepository().GetById(id);
            if (expertiseStage == null)
            {
                return HttpNotFound();
            }
            var primaryRepository = new DrugPrimaryRepository();
            var ntd = primaryRepository.GetPrimaryNtdByDeclarationId(expertiseStage.DeclarationId);
            if (ntd == null)
            {
                ntd = new EXP_DrugPrimaryNTD
                {
                    DrugDeclarationId = expertiseStage.DeclarationId,
                    EXP_DrugDeclaration = expertiseStage.EXP_DrugDeclaration
                };
            }
            var repository = new ReadOnlyDictionaryRepository();
            ViewData["TypeNDList" + expertiseStage.DeclarationId] = new SelectList(repository.GetDicTypeNDs(), "Id", "NameRu",
                  ntd.TypeNDId);
            ViewData["TypeFileNDList" + expertiseStage.DeclarationId] = new SelectList(repository.GetDicTypeFileNDs(), "Id", "NameRu",
               ntd.TypeFileNDId);

            var model = new PrimaryEntity
            {
                EXP_DrugPrimaryNTD = ntd,
                EXP_DrugDeclaration = expertiseStage.EXP_DrugDeclaration,
                DrugDeclarationId = expertiseStage.DeclarationId.ToString(),
                Applicant = new EmployeesRepository().GetById(expertiseStage.EXP_DrugDeclaration.OwnerId),
                Editor = UserHelper.GetCurrentEmployee(),
                ExpExpertiseStageRemarks = new List<EXP_ExpertiseStageRemark>(),
                ExpDrugPrimaryKinds = primaryRepository.GetPrimaryKindList(expertiseStage.DeclarationId),
                ExpeditedType = expertiseStage.EXP_DrugDeclaration.ExpeditedType,
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
            var markList = primaryRepository.GetPrimaryMarkList(expertiseStage.DeclarationId, CodeConstManager.STAGE_PRIMARY);
            var remarkTypes = repository.GetRemarkTypes().ToArray();
            ViewData["RemarkTypes" + model.DrugDeclarationId] = new SelectList(remarkTypes, "Id", "NameRu",
                null);
            foreach (var expDrugPrimaryRemark in markList)
            {
                model.ExpExpertiseStageRemarks.Add(expDrugPrimaryRemark);
            }
            foreach (var expDrugPrimaryRemark in model.ExpExpertiseStageRemarks)
            {
                ViewData["RemarkTypes" + model.DrugDeclarationId + "_" + expDrugPrimaryRemark.Id] = new SelectList(remarkTypes, "Id", "NameRu",
                 expDrugPrimaryRemark.RemarkTypeId);
            }
            model.ExpDrugCorespondences = primaryRepository.GetDrugCorespondences(expertiseStage.DeclarationId);


            return PartialView(model);
        }
        
        [HttpPost]
        public virtual ActionResult UpdateFinalDocument(string fieldName, string fieldValue, Guid objectId)
        {
            var finalDoc = new DrugPrimaryRepository().UpdateFinalDocument(fieldName, fieldValue, objectId, UserHelper.GetCurrentEmployee().Id);
            return Json(new { Success = true, DocId = finalDoc.Id, Status = finalDoc.EXP_ExpertiseStageDosage.FinalDocStatus });
        }

        [HttpPost]
        public virtual ActionResult CloneDosageFinalDoc(Guid dosageId)
        {
            new DrugPrimaryRepository().CloneDosageFinalDoc(dosageId);
            return Json(new { Success = true });
        }

        [HttpPost]
        public virtual ActionResult GetCorespondence(string id)
        {
            var mail = new DrugPrimaryRepository().GetCorespondence(id);
            return Json(new { mail.NumberLetter, mail.Subject, mail.Note, mail.DateSend });
        }
        public ActionResult SaveCorespondence(string id,string numberLetter, string subject, string note, DateTime dateSend)
        {
             new DrugPrimaryRepository().SaveCorespondence(id, numberLetter, subject, note, dateSend, UserHelper.GetCurrentEmployee());
            return Json(new
            {
                isSuccess = true
            });
        }
  

        public ActionResult DeleteCorespondence(string id)
        {
            new DrugPrimaryRepository().DeleteCorespondence(id, UserHelper.GetCurrentEmployee());
            return Json(new
            {
                isSuccess = true
            });
        }
        public ActionResult SendMailRemark(string id)
        {
            var model = new DrugPrimaryRepository().SendMailRemark(id, UserHelper.GetCurrentEmployee());
            string sendDate;
            string statusName;
            if (model != null)
            {
                sendDate = model.DateSend.ToString();
                statusName = model.Dictionary.Name;
            }
            else
            {
                sendDate = null;
                statusName = null;
            }
            return Json(new
            {
                isSuccess = true,
                sendDate,
                statusName
            });
        }
        public virtual ActionResult UpdateOtd(Guid stageId, int noteId, bool isChecked)
        {
            var success = new DrugPrimaryRepository().UpdateOtd(stageId, noteId, isChecked, UserHelper.GetCurrentEmployee().Id);
            return Json(new { success });
        }
        public virtual ActionResult CreateRemark(string id)
        {
            var isSuccess = new DrugPrimaryRepository().CreateRemark(id, UserHelper.GetCurrentEmployee(), CodeConstManager.STAGE_PRIMARY);
            return Json(new
            {
                isSuccess
            });
        }


        public FileStreamResult ExportFile(Guid id)
        {
            var db=new ncelsEntities();
            var repo = new DrugPrimaryRepository();
            var correspondence = repo.GetCorespondence(id.ToString());
            var reportTemplate = "";
            switch (correspondence.EXP_DIC_CorespondenceSubject.Code)
            {
                case EXP_DIC_CorespondenceSubject.Remarks:
                    switch (correspondence.StageId)
                    {
                        case CodeConstManager.STAGE_PHARMACOLOGICAL:
                        case CodeConstManager.STAGE_SAFETYREPORT:
                            reportTemplate = "~/Reports/DrugPrimary/CorespondencePharmacological.mrt";
                            break;
                        case CodeConstManager.STAGE_TRANSLATE:
                            reportTemplate = "~/Reports/DrugPrimary/CorespondenceTranslate.mrt";
                            break;
                        case CodeConstManager.STAGE_PHARMACEUTICAL:
                            reportTemplate = "~/Reports/DrugPrimary/CorespondencePharmaceutical.mrt";
                            break;
                        default:
                            reportTemplate = "~/Reports/DrugPrimary/Corespondence.mrt";
                            break;
                    }
                    break;
                case EXP_DIC_CorespondenceSubject.RefuseByPayment:
                    reportTemplate = "~/Reports/DrugPrimary/RefuseByPaymentLetter.mrt";
                    break;
                default:
                    reportTemplate = "";
                    break;
            }
            var reportPath = Server.MapPath(reportTemplate);
            string fileType;
            string fileName;
            var file = repo.GetCorespondenceFilePreview(id, reportPath, out fileType, out fileName);
            return File(file, fileType, fileName);
        }

    }

}