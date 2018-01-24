namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertisePrimaryFinalDoc
    {
        public Guid Id { get; set; }

        public bool? IsRKProduct { get; set; }

        public bool? IsDossierSection { get; set; }

        public bool? IsSetDocument { get; set; }

        public bool? IsColorModel { get; set; }

        public bool? IsForbiddenDyes { get; set; }

        public bool? IsFromBlood { get; set; }

        public bool? IsNarcoticDrug { get; set; }

        public bool? IsPhoneticSimilar { get; set; }

        public bool? IsAbilityMislead { get; set; }

        public bool? IsAdvertising { get; set; }

        public bool? IsMNNSimilar { get; set; }

        [StringLength(2000)]
        public string ExpertiseNormDoc { get; set; }

        [StringLength(2000)]
        public string SampleDrug { get; set; }

        [StringLength(2000)]
        public string ComplianceSeries { get; set; }

        [StringLength(2000)]
        public string ResidualShelfLife { get; set; }

        [StringLength(2000)]
        public string SampleSubstance { get; set; }

        [StringLength(2000)]
        public string SampleStandart { get; set; }

        [StringLength(2000)]
        public string TestLabRecommend { get; set; }

        [StringLength(2000)]
        public string MedicalInstruction { get; set; }

        [StringLength(2000)]
        public string ExpertOpinion { get; set; }

        public Guid DosageStageId { get; set; }

        public virtual EXP_ExpertiseStageDosage EXP_ExpertiseStageDosage { get; set; }
    }
}
