using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class DrugAnaliticRepository : ADrugDeclarationRepository
    {
        public EXP_DrugAnaliseIndicator GetIndicator(Guid id)
        {
            return AppContext.EXP_DrugAnaliseIndicator.FirstOrDefault(e => e.Id == id);
        }

        public EXP_DrugAnaliseIndicator SaveOrUpdateIndicator(Guid stageId, Guid id, double? temperature, double? humidity, string designation, string demand, string actualResult, int? analyseIndicator, bool? isMatches, Employee getCurrentEmployee)
        {
            var model = AppContext.EXP_DrugAnaliseIndicator.FirstOrDefault(e => e.Id == id) ??
                        new EXP_DrugAnaliseIndicator { DosageStageId = stageId };
            model.ActualResult = actualResult;
            model.AnalyseIndicator = analyseIndicator;
            model.Demand = demand;
            model.Designation = designation;
            model.Humidity = humidity;
            model.Temperature = temperature;
            model.IsMatches = isMatches;
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                int max =
                    AppContext.EXP_DrugAnaliseIndicator.Where(e => e.DosageStageId == stageId)
                        .Max(e => (int?)e.PositionNumber) ?? 0;

                model.PositionNumber = max + 1;
                AppContext.EXP_DrugAnaliseIndicator.Add(model);
            }
            if (analyseIndicator != null)
            {
                var inidcator = AppContext.EXP_DIC_AnalyseIndicator.FirstOrDefault(e => e.Id == analyseIndicator.Value);
                if (inidcator != null)
                {
                    model.AnalyseIndicatorName = inidcator.NameRu;
                }
            }

            AppContext.SaveChanges();
            return model;
        }

        public void DeleteAnaliticIndicator(string id, Employee getCurrentEmployee)
        {
            var model = AppContext.EXP_DrugAnaliseIndicator.FirstOrDefault(e => e.Id == new Guid(id));
            if (model == null)
            {
                return;
            }
            AppContext.EXP_DrugAnaliseIndicator.Remove(model);
            AppContext.SaveChanges();
        }

        public void CloneAnaliseDosage(Guid dosageId)
        {
            var dosage = AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == dosageId);
            if (dosage == null)
            {
                return;
            }
            var currentList = dosage.EXP_DrugAnaliseIndicator;
            var list = AppContext.EXP_ExpertiseStageDosage.Where(e => e.StageId == dosage.StageId && e.Id != dosageId);
            foreach (var expDrugDosage in list)
            {
                foreach (var entity in currentList)
                {
                    if (expDrugDosage.EXP_DrugAnaliseIndicator.All(e => e.AnalyseIndicator != entity.AnalyseIndicator))
                    {
                        var obj = new EXP_DrugAnaliseIndicator
                        {
                            Id = Guid.NewGuid(),
                            AnalyseIndicator = entity.AnalyseIndicator,
                            Humidity = entity.Humidity,
                            IsMatches = entity.IsMatches,
                            Temperature = entity.Temperature,
                            ActualResult = entity.ActualResult,
                            Demand = entity.Demand,
                            Designation = entity.Designation,
                            PositionNumber = entity.PositionNumber,
                            DosageStageId = expDrugDosage.Id
                        };
                        AppContext.EXP_DrugAnaliseIndicator.Add(obj);
                    }
                }
            }
            AppContext.SaveChanges();
        }

        public void UpdateOrder(string id, int fromPosition, int toPosition, string direction)
        {
            var indicator = AppContext.EXP_DrugAnaliseIndicator.FirstOrDefault(e => e.Id == new Guid(id));
            if (indicator == null)
            {
                return;
            }
            if (direction == "back")
            {
                var movedCompanies = AppContext.EXP_DrugAnaliseIndicator
                            .Where(c => c.DosageStageId == indicator.DosageStageId && (toPosition <= c.PositionNumber && c.PositionNumber <= fromPosition))
                            .ToList();

                foreach (var company in movedCompanies)
                {
                    company.PositionNumber++;
                }
            }
            else
            {
                var movedCompanies = AppContext.EXP_DrugAnaliseIndicator
                            .Where(c => c.DosageStageId == indicator.DosageStageId && (fromPosition <= c.PositionNumber && c.PositionNumber <= toPosition))
                            .ToList();
                foreach (var company in movedCompanies)
                {
                    company.PositionNumber--;
                }
            }

            indicator.PositionNumber = toPosition;
            AppContext.SaveChanges();
        }

        public void CheckInProtocol(Guid entityId, bool fieldValue)
        {
            var model = AppContext.EXP_DrugAnaliseIndicator.FirstOrDefault(e => e.Id == entityId);
            if (model == null)
            {
                return;
            }
            model.InProtocol = fieldValue;
            AppContext.SaveChanges();
        }

        public EXP_ExpertiseStageDosage GetExpExpertiseStageDosage(Guid? id)
        {
            return AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == id);
        }
    }
}
