using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
    /// Завобчение о безопастности
    /// </summary>
    public class SafetyreportController : DrugPrimaryController
    {
        public override int GetStage()
        {
            return CodeConstManager.STAGE_SAFETYREPORT;
        }

        public ActionResult SafetyReportPageView(Guid id)
        {
            var expertiseStage = new ExpertiseStageRepository().GetById(id);
            if (expertiseStage == null)
            {
                return HttpNotFound();
            }
            var safetyreportRepository = new SafetyreportRepository();
            var repository = new ReadOnlyDictionaryRepository();

            var model = new SafereportEntity
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
            var markList = safetyreportRepository.GetPrimaryMarkList(expertiseStage.DeclarationId, CodeConstManager.STAGE_SAFETYREPORT);
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
            model.ExpDrugCorespondences = safetyreportRepository.GetDrugCorespondences(expertiseStage.DeclarationId);


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

            var repositoryDic = new ReadOnlyDictionaryRepository();
            var repository = new SafetyreportRepository();

            model.EXP_DrugDosage.EXP_DrugDeclaration.ExpDicPrimaryOtds = repositoryDic.GetExpDicPrimaryOTDs().Where(e => e.ParentId == null).ToList();
            model.ExpertiseSafetyreportFinalDoc = model.EXP_ExpertiseSafetyreportFinalDoc.FirstOrDefault();

            if (model.ExpertiseSafetyreportFinalDoc == null)
            {
                model.ExpertiseSafetyreportFinalDoc = CreateExpertiseSafetyreportFinalDoc(model);
                repository.CreateExpertiseSafetyreportFinalDoc(model.ExpertiseSafetyreportFinalDoc);

                model.ExpertiseSafetyreportFinalDoc.EXP_ExpertiseStageDosage = model;
            }
            else
            {
                var newExpertiseSafetyreport = CreateExpertiseSafetyreportFinalDoc(model);

                if (string.IsNullOrEmpty(model.ExpertiseSafetyreportFinalDoc.Conclusion))
                    model.ExpertiseSafetyreportFinalDoc.Conclusion = newExpertiseSafetyreport.Conclusion;
                if (string.IsNullOrEmpty(model.ExpertiseSafetyreportFinalDoc.ConclusionKz))
                    model.ExpertiseSafetyreportFinalDoc.ConclusionKz = newExpertiseSafetyreport.ConclusionKz;

                repository.UpdateExpertiseSafetyreportFinalDoc(model.ExpertiseSafetyreportFinalDoc);
            }
            
            ViewData["FinalyDocResultList" + model.EXP_DrugDosage.DrugDeclarationId] = new SelectList(repositoryDic.GetStageResultsByStage(model.EXP_ExpertiseStage.StageId), "Id", "NameRu",
                model.ResultId);

            var stageName = ExpStageNameHelper.GetName(GetStage());
            ActionLogger.WriteInt(stageName + ": Получение заявки №" + model.EXP_DrugDosage.RegNumber);
            return PartialView("~/Views/DrugDeclaration/AppDosage.cshtml", model);
        }

        public ActionResult ReturnToStаge(Guid currentStageId, Guid declarationId, List<int> newStages)
        {
            ExpertiseStageRepository stageRepository = new ExpertiseStageRepository();
            string result;
            stageRepository.ToBackStage(declarationId, currentStageId, newStages.ToArray(),  out result);
            return Json(new { IsSussess = true}, JsonRequestBehavior.AllowGet);
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
            var finalDoc = new SafetyreportRepository().UpdateFinalDocument(fieldName, fieldValue, objectId, UserHelper.GetCurrentEmployee().Id);
            return Json(new { Success = true, DocId = finalDoc.Id, Status = finalDoc.EXP_ExpertiseStageDosage.FinalDocStatus });
        }
        
        public override ActionResult CreateRemark(string id)
        {
            var isSuccess = new DrugPrimaryRepository().CreateRemark(id, UserHelper.GetCurrentEmployee(), CodeConstManager.STAGE_SAFETYREPORT);
            return Json(new
            {
                isSuccess
            });
        }

        public FileStreamResult ExportSafetyReportFile(Guid id)
        {
            SafetyreportRepository repository = new SafetyreportRepository();
            var expertiseStageDosage = repository.GetExpertiseStageDosageById(id);

            var typeCode = expertiseStageDosage.EXP_DrugDosage.EXP_DrugDeclaration.EXP_DIC_Type.Code;

            var mrt = "~/Reports/SafetyReport/Registration_rus.mrt";

            if (typeCode == EXP_DIC_Type.ReRegistration)
                mrt = "~/Reports/SafetyReport/ReRegistration_rus.mrt";
            else if (typeCode == EXP_DIC_Type.Alteration)
                mrt = "~/Reports/SafetyReport/Alteration_rus.mrt";

            string name = "Заключение.pdf";
            StiReport report = new StiReport();

            report.Load(Server.MapPath(mrt));

            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            // имя и должность эксперта
//            var currentEmployee = UserHelper.GetCurrentEmployee();
//            var employeeName = currentEmployee.FullName;
//            var employeePosition = currentEmployee.Position.Name;

            report.Dictionary.Variables["DeclarationId"].ValueObject = expertiseStageDosage.EXP_ExpertiseStage.DeclarationId;
            report.Dictionary.Variables["StageDosageId"].ValueObject = id;

            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }

        public FileStreamResult ExportSafetyReportKzFile(Guid id)
        {
            SafetyreportRepository repository = new SafetyreportRepository();
            var expertiseStageDosage = repository.GetExpertiseStageDosageById(id);

            var typeCode = expertiseStageDosage.EXP_DrugDosage.EXP_DrugDeclaration.EXP_DIC_Type.Code;

            var mrt = "~/Reports/SafetyReport/Registration_kaz.mrt";
            if (typeCode == EXP_DIC_Type.ReRegistration)
                mrt = "~/Reports/SafetyReport/ReRegistration_kaz.mrt";
            else if (typeCode == EXP_DIC_Type.Alteration)
                mrt = "~/Reports/SafetyReport/Alteration_kaz.mrt";

            string name = "Заключение на государственном языке.pdf";
            StiReport report = new StiReport();

            report.Load(Server.MapPath(mrt));

            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }
            // имя и должность эксперта
            //            var currentEmployee = UserHelper.GetCurrentEmployee();
            //            var employeeName = currentEmployee.FullName;
            //            var employeePosition = currentEmployee.Position.Name;

            report.Dictionary.Variables["DeclarationId"].ValueObject = expertiseStageDosage.EXP_ExpertiseStage.DeclarationId;
            report.Dictionary.Variables["StageDosageId"].ValueObject = id;

            report.Render(false);
            var stream = new MemoryStream();
            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            return File(stream, "application/pdf", name);
        }

        private EXP_ExpertiseSafetyreportFinalDoc CreateExpertiseSafetyreportFinalDoc(EXP_ExpertiseStageDosage model)
        {
            EXP_DIC_Type type = null;
            bool termless = false;
            IEnumerable<EXP_ExpertiseStage> stages = new List<EXP_ExpertiseStage>();
            if (model.EXP_DrugDosage?.EXP_DrugDeclaration != null)
            {
                type = model.EXP_DrugDosage?.EXP_DrugDeclaration.EXP_DIC_Type;
                // EXP_DIC_PrimaryKind
                termless = model.EXP_DrugDosage?.EXP_DrugDeclaration.EXP_DrugPrimaryKind.FirstOrDefault(dpk => dpk.PrimaryKindId == 7) == null;
                stages = model.EXP_DrugDosage?.EXP_DrugDeclaration.EXP_ExpertiseStage
                    .Where(es => !es.IsHistory).ToList();
                // && (es.EXP_DIC_StageStatus.Code == EXP_DIC_StageStatus.Completed|| es.EXP_DIC_StageStatus.Code == EXP_DIC_StageStatus.InWork)
            }
            // Первичная
            var expExpertiseStage = stages.FirstOrDefault(s => s.EXP_DIC_Stage.Code == CodeConstManager.STAGE_PRIMARY.ToString());
            string primaryResult = string.Empty;
            if (expExpertiseStage?.EXP_DIC_StageResult != null)
            {
                primaryResult = GetStageText(expExpertiseStage, type);
            }

            // Фармацевтика
            expExpertiseStage = stages.FirstOrDefault(s => s.EXP_DIC_Stage.Code == CodeConstManager.STAGE_PHARMACEUTICAL.ToString());
            string pharmaceuticalResult = string.Empty;
            if (expExpertiseStage?.EXP_DIC_StageResult != null)
            {
                pharmaceuticalResult = GetStageText(expExpertiseStage, type);
            }

            // Фармакология
            expExpertiseStage = stages.FirstOrDefault(s => s.EXP_DIC_Stage.Code == CodeConstManager.STAGE_PHARMACOLOGICAL.ToString());
            string pharmacologicalResult = string.Empty;
            if (expExpertiseStage?.EXP_DIC_StageResult != null)
            {
                pharmacologicalResult = GetStageText(expExpertiseStage, type);
            }

            // Иследовательский центр
            expExpertiseStage = stages.FirstOrDefault(s => s.EXP_DIC_Stage.Code == CodeConstManager.STAGE_ANALITIC.ToString());
            string analiticResult = string.Empty;
            if (expExpertiseStage?.EXP_DIC_StageResult != null)
            {
                analiticResult = GetStageText(expExpertiseStage, type);
            }

            // ЗОБ
            expExpertiseStage = stages.FirstOrDefault(s => s.EXP_DIC_Stage.Code == CodeConstManager.STAGE_SAFETYREPORT.ToString());
            string conclusion = string.Empty;
            string conclusionKz = string.Empty;
            //if (expExpertiseStage?.EXP_DIC_StageResult != null)
            if (model?.EXP_DIC_StageResult != null && expExpertiseStage != null)
            {
                // этап 
                expExpertiseStage.EXP_DIC_StageResult = model?.EXP_DIC_StageResult;
                conclusion = GetStageText(expExpertiseStage, type);
                conclusionKz = GetStageText(expExpertiseStage, type, termless, true);
            }
            
            return new EXP_ExpertiseSafetyreportFinalDoc()
            {
                Id = Guid.NewGuid(),
                PrimaryConclusion = primaryResult,
                PrimaryConclusionKz = primaryResult,
                PharmaceuticalConclusion = pharmaceuticalResult,
                PharmaceuticalConclusionKz = pharmaceuticalResult,
                PharmacologicalConclusion = pharmacologicalResult,
                PharmacologicalConclusionKz = pharmacologicalResult,
                AnalyticalConclusion = analiticResult,
                AnalyticalConclusionKz = analiticResult,
                Conclusion = conclusion,
                ConclusionKz = conclusionKz,
                DosageStageId = model.Id
            };
        }

        private string GetStageText(EXP_ExpertiseStage expertiseStage, EXP_DIC_Type type, bool termless = false, bool isKz = false)
        {
            //  expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.Matches ? ;
            string outStr = string.Empty;

            const string positive = "Положительное";
            const string negative = "Отрицательное";

            const string protocolPositive = "Положительное";  //"{дата протокола} и {№ протокола}, положительное";
            const string protocolNegative = "Отрицательное";  //"{дата протокола} и {№ протокола}, отрицательное";

            const string recommendationsRegistrationPositive5 = "Рекомендована государственная регистрация сроком на 5 лет";
            const string recommendationsReRegistrationPositive5 = "Рекомендована государственная перерегистрация сроком на 5 лет";
            const string recommendationsReRegistrationPositive = "Рекомендована государственная перерегистрация с выдачей бессрочного регистрационного удостоверения";
            const string recommendationsNegative = "Отказ в государственной регистрации";
            const string recommendationsReNegative = "Отказ в государственной перерегистрации";


            const string conclusionPositive5 = "Материалы и документы регистрационного досье на лекарственное средство, предоставленные для государственной регистрации в Республике Казахстан, соответствуют установленным требованиям, безопасность, эффективность и качество лекарственного средства подтверждены соответствующими материалами и проведенными испытаниями. Лекарственное средство может быть зарегистрировано в Республике Казахстан сроком на 5 лет.";

            const string conclusionPositiveKz5 = "Қазақстан Республикасында мемлекеттік тіркеуге ұсынылған дәрілік затқа берілген материалдар мен тіркеу дерекнамасының құжаттары белгіленген талаптарға сәйкес келеді, дәрілік заттың қауіпсіздігі, тиімділігі және сапасы тиісті материалдармен және жүргізілген сынақтармен дәлелденді. Дәрілік затты Қазақстан Республикасында 5 жыл мерзімге тіркеуге болады";
            const string conclusionPositive = "Материалы и документы регистрационного досье на лекарственное средство, предоставленные для государственной перерегистрации в Республике Казахстан, соответствуют установленным требованиям, безопасность, эффективность и качество лекарственного средства подтверждены соответствующими материалами и проведенными испытаниями. Лекарственное средство может быть перезарегистрировано в Республике Казахстан с выдачей бессрочного регистрационного удостворения.";
            const string conclusionNegative = "Проведенная экспертиза регистрационного досье, представленного для государственной регистрации, в Республике Казахстан показала, что лекарственное средство не соответствует требованиям по безопасности, эффективности и качеству по следующим показателям: ________________ и не может быть зарегистрирован в Республике Казахстан.";


            const string conclutionA = "Материалы и документы регистрационного досье на лекарственное средство, предоставленные для внесения изменений в регистрационное досье, соответствуют установленным требованиям, безопасность, эффективность и качество лекарственного средства подтверждены соответствующими материалами и проведенными испытаниями. Вносимые изменения могут быть зарегистрированы с выдачей нового регистрационного удостворения.";

            switch (expertiseStage.EXP_DIC_Stage.Code)
            {
                case EXP_DIC_Stage.PrimaryExp:
                        if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode)
                            return positive;
                        else
                            return negative;
                    break;
                case EXP_DIC_Stage.PharmaceuticalExp:
                case EXP_DIC_Stage.PharmacologicalExp:
                    switch (type.Code)
                    {
                        case TypeCodeConts.Registration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode || expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.RecommendedCode)
                                return recommendationsRegistrationPositive5;
                            else
                                return recommendationsNegative;
                            break;
                        case TypeCodeConts.ReRegistration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode || expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.RecommendedCode)
                            {
                                if (termless)
                                    return recommendationsReRegistrationPositive;
                                return recommendationsReRegistrationPositive5;
                            }
                            else
                                return recommendationsReNegative;
                            break;
                        case TypeCodeConts.Alteration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode || expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.RecommendedCode)
                            {
                                if (termless)
                                    return recommendationsReRegistrationPositive;
                                return recommendationsRegistrationPositive5;
                            }
                            else
                                return recommendationsNegative;
                            break;
                    }
                    break;
                case EXP_DIC_Stage.AnalyticalExp:
                    switch (type.Code)
                    {
                        case TypeCodeConts.Registration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode)
                                return protocolPositive;
                            else
                                return protocolNegative;
                            break;
                        case TypeCodeConts.ReRegistration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode)
                                return protocolPositive;
                            else
                                return protocolNegative;
                            break;
                        case TypeCodeConts.Alteration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode)
                                return protocolPositive;
                            else
                                return protocolNegative;
                            break;
                    }
                    break;
                case EXP_DIC_Stage.SafetyConclusion:
                    switch (type.Code)
                    {
                        case TypeCodeConts.Registration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode)
                            {
                                if (isKz)
                                    return conclusionPositiveKz5;
                                return conclusionPositive5;
                            }
                            else
                                return conclusionNegative;
                            break;
                        case TypeCodeConts.ReRegistration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode)
                            {
                                if (termless)
                                    return conclusionPositive;
                                return conclusionPositive5;
                            }
                            else
                                return conclusionNegative;
                            break;
                        case TypeCodeConts.Alteration:
                            if (expertiseStage.EXP_DIC_StageResult.Code == EXP_DIC_StageResult.MatchesCode)
                            {
                                return conclutionA;
                            }
                            else
                                return string.Empty;
                            break;
                    }
                    break;
            }

            return outStr;
        }

        public ActionResult SendToApplicant(Guid stageId)
        {
            var repo = new ExpertiseStageRepository();
            var stage = repo.GetStage(stageId);
            var repository = new DrugDeclarationRepository();
            var declaraion = repository.GetById(stage.DeclarationId.ToString());
            if (declaraion == null)
            {
                return Json(new
                {
                    isSuccess = false,
                });
            }
            declaraion.DesignDate = DateTime.Now;
                declaraion.StatusId = CodeConstManager.STATUS_EXP_SEND_INSTRUCTION_ID;
                new DrugDeclarationRepository().Update(declaraion);
                var history = new EXP_DrugDeclarationHistory()
                {
                    DateCreate = DateTime.Now,
                    DrugDeclarationId = declaraion.Id,
                    StatusId = declaraion.StatusId,
                    UserId = UserHelper.GetCurrentEmployee().Id,
                    Note = "Инструкция для согласования"
                };
                new DrugDeclarationRepository().SaveHisotry(history, UserHelper.GetCurrentEmployee().Id);
            var status = new ReadOnlyDictionaryRepository().GetDicStatusById(CodeConstManager.STATUS_EXP_SEND_INSTRUCTION_ID);
            return Json(new
            {
                isSuccess = true,
                statusName = status.NameRu
            });
        }

    }
}