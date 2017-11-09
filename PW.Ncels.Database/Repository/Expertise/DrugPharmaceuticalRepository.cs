using System;
using System.Linq;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;

namespace PW.Ncels.Database.Repository.Expertise
{
    public class DrugPharmaceuticalRepository : ADrugDeclarationRepository
    {
        public void CloneDosageFinalDoc(Guid dosageId)
        {
            var dosage = AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == dosageId);
            if (dosage == null)
            {
                return;
            }
            var currentFinal = dosage.EXP_ExpertisePharmaceuticalFinalDoc.FirstOrDefault();
            if (currentFinal == null) return;

            var list = AppContext.EXP_ExpertiseStageDosage.Where(e => e.StageId == dosage.StageId && e.Id != dosageId);
            foreach (var expDrugDosage in list)
            {
                var expFinal = expDrugDosage.EXP_ExpertisePharmaceuticalFinalDoc.FirstOrDefault() ??
                               new EXP_ExpertisePharmaceuticalFinalDoc(currentFinal);
                expFinal.DosageStageId = expDrugDosage.Id;
                if (expFinal.Id == Guid.Empty)
                {
                    expFinal.Id = Guid.NewGuid();
                    AppContext.EXP_ExpertisePharmaceuticalFinalDoc.Add(expFinal);
                }
            }
            AppContext.SaveChanges();
        }

        public EXP_ExpertisePharmaceuticalFinalDoc UpdateFinalDocument(string fieldName, string fieldValue, Guid objectId, Guid userId)
        {
            var appDosage = AppContext.EXP_ExpertiseStageDosage.FirstOrDefault(e => e.Id == objectId);
            if (appDosage == null)
            {
                return null;
            }
            var model = appDosage.EXP_ExpertisePharmaceuticalFinalDoc.FirstOrDefault() ??
                        new EXP_ExpertisePharmaceuticalFinalDoc { DosageStageId = appDosage.Id };
            var property = model.GetType().GetProperty(fieldName);
            if (property != null)
            {
                var t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                if (string.IsNullOrEmpty(fieldValue))
                {
                    fieldValue = null;
                }
                var safeValue = fieldValue == null ? null : Convert.ChangeType(fieldValue, t);
                property.SetValue(model, safeValue, null);
            }
            if (model.Id == Guid.Empty)
            {
                model.Id = Guid.NewGuid();
                AppContext.EXP_ExpertisePharmaceuticalFinalDoc.Add(model);
            }
            var addLogInfo = "";
            addLogInfo += "field: '" + fieldName + "' value: " + fieldValue;
            ActionLogger.WriteInt(AppContext, "Заявка №" + appDosage.EXP_DrugDosage.RegNumber + " изменения результатов: ФМК", addLogInfo);
            AppContext.SaveChanges();
            return model;
        }
    }
}