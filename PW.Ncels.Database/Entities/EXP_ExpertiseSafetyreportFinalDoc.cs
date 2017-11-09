using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.DataModel
{
    [MetadataType(typeof(EXP_ExpertiseSafetyreportFinalDocMetadata))]
    public partial class EXP_ExpertiseSafetyreportFinalDoc
    {
        public EXP_ExpertiseSafetyreportFinalDoc()
        {
            
        }
        public EXP_ExpertiseSafetyreportFinalDoc(EXP_ExpertiseSafetyreportFinalDoc doc)
        {
            this.Id=Guid.Empty;
            this.DosageStageId = doc.DosageStageId;
            this.Conclusion = doc.Conclusion;
            this.PrimaryConclusion = doc.PrimaryConclusion;
            this.PharmaceuticalConclusion = doc.PharmaceuticalConclusion;
            this.PharmacologicalConclusion = doc.PharmacologicalConclusion;
            this.AnalyticalConclusion = doc.AnalyticalConclusion;
            this.PrimaryConclusionKz = doc.PrimaryConclusionKz;
            this.PharmaceuticalConclusionKz = doc.PharmaceuticalConclusionKz;
            this.PharmacologicalConclusionKz = doc.PharmacologicalConclusionKz;
            this.AnalyticalConclusionKz = doc.AnalyticalConclusionKz;
            this.ConclusionKz = doc.ConclusionKz;
        }
        public class EXP_ExpertiseSafetyreportFinalDocMetadata
        {
            public System.Guid Id { get; set; }
            
            public System.Guid DosageStageId { get; set; }

            [Display(Name="Заключение первичной экспертизы:")]
            public string PrimaryConclusion { get; set; }

            [Display(Name = "Заключение управления фармацевтической экспертизы:")]
            public string PharmaceuticalConclusion { get; set; }

            [Display(Name = "Заключение управления фармаколоогической экспертизы:")]
            public string PharmacologicalConclusion { get; set; }

            [Display(Name = "Заключение испытательной лабораторий:")]
            public string AnalyticalConclusion { get; set; }

            [Display(Name = "Заключение:")]
            public string Conclusion { get; set; }

            [Display(Name = "Бастапқы сараптау қорытындысы:")]
            public string PrimaryConclusionKz { get; set; }

            [Display(Name = "Фармацевтикалық сараптау басқармасының қорытындысы:")]
            public string PharmaceuticalConclusionKz { get; set; }

            [Display(Name = "Фармакологиялық сараптау басқармасының қорытындысы:")]
            public string PharmacologicalConclusionKz { get; set; }

            [Display(Name = "Сынақ зертханасының қорытындысы:")]
            public string AnalyticalConclusionKz { get; set; }

            [Display(Name = "Қорытынды:")]
            public string ConclusionKz { get; set; }

            public virtual EXP_ExpertiseStageDosage EXP_ExpertiseStageDosage { get; set; }
        }
    }
}