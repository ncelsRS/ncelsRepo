using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ncels.Helpers;
using PW.Ncels.Database.Constants;
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
    /// <summary>
    /// Фармацевтическая экспертиза
    /// </summary>
    public class PharmaceuticalController : DrugPrimaryController
    {
        public ActionResult PharmaceuticalPageView(Guid id)
        {
            var expertiseStage = new ExpertiseStageRepository().GetById(id);
            if (expertiseStage == null)
            {
                return HttpNotFound();
            }
            var primaryRepository = new DrugPrimaryRepository();
            var repository = new ReadOnlyDictionaryRepository();

            var model = new PharmaceuticalEntity
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
            var markList = primaryRepository.GetPrimaryMarkList(expertiseStage.DeclarationId, CodeConstManager.STAGE_PHARMACEUTICAL);
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
            model.PharmaceuticalFinalDoc = model.EXP_ExpertisePharmaceuticalFinalDoc.FirstOrDefault();
            if (model.PharmaceuticalFinalDoc == null)
            {
                model.PharmaceuticalFinalDoc = new EXP_ExpertisePharmaceuticalFinalDoc();
                model.PharmaceuticalFinalDoc.EXP_ExpertiseStageDosage = model;
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
            return CodeConstManager.STAGE_PHARMACEUTICAL;
        }
        [HttpPost]
        public override ActionResult CloneDosageFinalDoc(Guid dosageId)
        {
            new DrugPharmaceuticalRepository().CloneDosageFinalDoc(dosageId);
            return Json(new { Success = true });
        }
        [HttpPost]
        public override ActionResult UpdateFinalDocument(string fieldName, string fieldValue, Guid objectId)
        {
            var finalDoc = new DrugPharmaceuticalRepository().UpdateFinalDocument(fieldName, fieldValue, objectId, UserHelper.GetCurrentEmployee().Id);
            return Json(new { Success = true, DocId = finalDoc.Id, Status = finalDoc.EXP_ExpertiseStageDosage.FinalDocStatus });
        }
        public override ActionResult CreateRemark(string id)
        {
            var isSuccess = new DrugPrimaryRepository().CreateRemark(id, UserHelper.GetCurrentEmployee(), CodeConstManager.STAGE_PHARMACEUTICAL);
            return Json(new
            {
                isSuccess
            });
        }

        public FileStreamResult ExportFilePdf(Guid id)
        {
            string name = "Документ.pdf";
            StiReport report = new StiReport();

            try
            {
                report.Load(Server.MapPath("~/Reports/Pharmaceutical/OutcomeDoc.mrt"));

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
                report.Dictionary.Variables["PharmaceuticalFinalDocId"].ValueObject = id;

                report.Render(false);
            }
            catch (Exception ex)
            {
                LogHelper.Log.Error("ex:" + ex.Message + " \r\nstack:" + ex.StackTrace);
            }
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }
    }
}