using System;

namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_ExpertisePrimaryFinalDoc
    {
        public EXP_ExpertisePrimaryFinalDoc()
        {
            
        }
        public EXP_ExpertisePrimaryFinalDoc(EXP_ExpertisePrimaryFinalDoc doc)
        {
            this.Id=Guid.Empty;
            this.DosageStageId = doc.DosageStageId;
            this.IsRKProduct = doc.IsRKProduct;
            this.IsDossierSection = doc.IsDossierSection;
            this.IsSetDocument = doc.IsSetDocument;
            this.IsColorModel = doc.IsColorModel;
            this.IsForbiddenDyes = doc.IsForbiddenDyes;
            this.IsFromBlood = doc.IsFromBlood;
            this.IsNarcoticDrug = doc.IsNarcoticDrug;
            this.IsPhoneticSimilar = doc.IsPhoneticSimilar;
            this.IsAbilityMislead = doc.IsAbilityMislead;
            this.IsAdvertising = doc.IsAdvertising;
            this.IsMNNSimilar = doc.IsMNNSimilar;
            this.ExpertiseNormDoc = doc.ExpertiseNormDoc;
            this.SampleDrug = doc.SampleDrug;
            this.ComplianceSeries = doc.ComplianceSeries;
            this.ResidualShelfLife = doc.ResidualShelfLife;
            this.SampleSubstance = doc.SampleSubstance;
            this.SampleStandart = doc.SampleStandart;
            this.TestLabRecommend = doc.TestLabRecommend;
            this.MedicalInstruction = doc.MedicalInstruction;
            this.ExpertOpinion = doc.ExpertOpinion;
        }
    }
}