namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertiseSafetyreportFinalDoc
    {
        public Guid Id { get; set; }

        [Column(TypeName = "ntext")]
        public string PrimaryConclusion { get; set; }

        [Column(TypeName = "ntext")]
        public string PrimaryConclusionKz { get; set; }

        [Column(TypeName = "ntext")]
        public string PharmaceuticalConclusion { get; set; }

        [Column(TypeName = "ntext")]
        public string PharmaceuticalConclusionKz { get; set; }

        [Column(TypeName = "ntext")]
        public string PharmacologicalConclusion { get; set; }

        [Column(TypeName = "ntext")]
        public string PharmacologicalConclusionKz { get; set; }

        [Column(TypeName = "ntext")]
        public string AnalyticalConclusion { get; set; }

        [Column(TypeName = "ntext")]
        public string AnalyticalConclusionKz { get; set; }

        [Column(TypeName = "ntext")]
        public string Conclusion { get; set; }

        [Column(TypeName = "ntext")]
        public string ConclusionKz { get; set; }

        public Guid DosageStageId { get; set; }

        public bool? IsAccepted { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        public bool? IsAcceptedKz { get; set; }

        [StringLength(500)]
        public string RemarkKz { get; set; }

        public virtual EXP_ExpertiseStageDosage EXP_ExpertiseStageDosage { get; set; }
    }
}
