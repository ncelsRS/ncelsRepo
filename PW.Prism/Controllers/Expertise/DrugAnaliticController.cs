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
    public class DrugAnaliticController : APrismDrugDeclaraionController
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
            model.ExpDrugAnaliseIndicators = new List<EXP_DrugAnaliseIndicator>();
            var list = model.EXP_DrugAnaliseIndicator.OrderBy(e => e.PositionNumber);
            var repository = new ReadOnlyDictionaryRepository();
            var remarkTypes = repository.GetDicAnalyseIndicators().ToArray();
            ViewData["AnalyseIndicatorList"+ model.Id] = new SelectList(remarkTypes, "Id", "NameRu",null);
            var booleans = repository.GetBooleanList();
            ViewData["Booleans" + model.Id] = new SelectList(booleans, "IsSign", "NameRu", null);

            foreach (var expDrugAnaliseIndicator in list)
            {
               // ViewData["AnalyseIndicatorList"+ expDrugAnaliseIndicator.Id] = new SelectList(remarkTypes, "Id", "NameRu", expDrugAnaliseIndicator.AnalyseIndicator);
                model.ExpDrugAnaliseIndicators.Add(expDrugAnaliseIndicator);
            }
            ViewData["FinalyDocResultList" + model.EXP_DrugDosage.DrugDeclarationId] = new SelectList(repository.GetStageResultsByStage(model.EXP_ExpertiseStage.StageId), "Id", "NameRu",
                model.ResultId);

            var stageName = ExpStageNameHelper.GetName(GetStage());
            ActionLogger.WriteInt(stageName + ": Получение заявки №" + model.EXP_DrugDosage.RegNumber);
            return PartialView("~/Views/DrugAnalitic/AppDosage.cshtml", model);
        }

        public override int GetStage()
        {
            return CodeConstManager.STAGE_ANALITIC;
        }
        public ActionResult AnaliticPageView(Guid? id)
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
             var repository = new ReadOnlyDictionaryRepository();
            var employee = UserHelper.GetCurrentEmployee();
            var model = new DrugAnaliticEntity
            {
                EXP_DrugDeclaration = expertiseStage.EXP_DrugDeclaration,
                DrugDeclarationId = expertiseStage.DeclarationId.ToString(),
                Applicant = new EmployeesRepository().GetById(expertiseStage.EXP_DrugDeclaration.OwnerId),
                Editor = employee,
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
            var markList = new DrugDeclarationRepository().GetPrimaryMarkList(expertiseStage.DeclarationId, CodeConstManager.STAGE_ANALITIC);
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
            model.ExpDrugCorespondences = new DrugDeclarationRepository().GetDrugCorespondences(expertiseStage.DeclarationId);


            return PartialView(model);
        }

        [HttpPost]
        public virtual ActionResult GetIndicator(string id)
        {
            var model = new DrugAnaliticRepository().GetIndicator(new Guid(id));
            if (model == null)
            {
                return Json(new {Id=Guid.Empty,ActualResult = "", AnalyseIndicator = "", Demand="", Designation = "", Humidity = "", Temperature = "", IsMatches="" });

            }
            return Json(new { model.Id, model.ActualResult,model.AnalyseIndicator, model.Demand,model.Designation, model.Humidity, model.Temperature, model.IsMatches });
        }
        public ActionResult SaveOrUpdateIndicator(Guid stageId , Guid id, string temperature, string humidity, string designation, string demand, string actualResult, int? analyseIndicator, bool? isMatches)
        {
            double tempr;
            double? temperatureVal = null;
            if (!string.IsNullOrEmpty(temperature) && double.TryParse(temperature.Replace(".", ","), out tempr))
            {
                temperatureVal = tempr;
            }

            double humid;
            double? humidityVal = null;
            if (!string.IsNullOrEmpty(humidity) && double.TryParse(humidity.Replace(".", ","), out humid))
            {
                humidityVal = humid;
            }
            var model = new DrugAnaliticRepository().SaveOrUpdateIndicator(stageId, id, temperatureVal, humidityVal, designation, demand, actualResult, analyseIndicator, isMatches,  UserHelper.GetCurrentEmployee());
            var isNew = id == Guid.Empty;

            return Json(new
            {
                isNew,
                model.IsMatchesStr,
                modelId =model.Id,
                model.AnalyseIndicatorName,
                model.PositionNumber
            });
        }
        public ActionResult DeleteIndicator(string id)
        {
            new DrugAnaliticRepository().DeleteAnaliticIndicator(id, UserHelper.GetCurrentEmployee());
            return Json(new
            {
                isSuccess = true
            });
        }

        [HttpPost]
        public virtual ActionResult CloneAnaliseDosage(Guid dosageId)
        {
            new DrugAnaliticRepository().CloneAnaliseDosage(dosageId);
            return Json(new { Success = true });
        }
        [HttpPost]
        public virtual ActionResult CheckInProtocol(Guid entityId, bool fieldValue)
        {
            new DrugAnaliticRepository().CheckInProtocol(entityId, fieldValue);
            return Json(new { Success = true });
        }
        public void UpdateOrder(string id, int fromPosition, int toPosition, string direction)
        {
            new DrugAnaliticRepository().UpdateOrder(id, fromPosition, toPosition, direction);
        }
        public FileStreamResult ExportProtocol(Guid id)
        {

            StiReport report = new StiReport();
            var model = new DrugAnaliticRepository().GetExpExpertiseStageDosage(id);
            var applicant = new EmployeesRepository().GetById(model.EXP_ExpertiseStage.EXP_DrugDeclaration.OwnerId);


            report.Load(Server.MapPath("../Reports/DrugDeclaration/AnaliseProtocol.mrt"));
            foreach (var data in report.Dictionary.Databases.Items.OfType<StiSqlDatabase>())
            {
                data.ConnectionString = UserHelper.GetCnString();
            }

            if (report.Dictionary.Variables.Contains("DosageStageId"))
            {
                report.Dictionary.Variables["DosageStageId"].ValueObject = id;
            }
            if (report.Dictionary.Variables.Contains("ApplicationName"))
            {
                report.Dictionary.Variables["ApplicationName"].ValueObject = applicant.DisplayName;
            }
            report.Render(false);
            var stream = new MemoryStream();

            report.ExportDocument(StiExportFormat.Pdf, stream);
            stream.Position = 0;
            var reportname = "Протокол_" + DateTime.Now.ToString("yyyy-mm-dd hh.mm.ss") + ".pdf";
            return File(stream, "application/pdf", reportname);


        }
    }
}