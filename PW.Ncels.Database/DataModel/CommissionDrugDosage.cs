namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommissionDrugDosage")]
    public partial class CommissionDrugDosage
    {
        public int Id { get; set; }

        public int CommissionId { get; set; }

        public int? ConclusionTypeId { get; set; }

        public string ConclusionComment { get; set; }

        public long DrugDosageId { get; set; }

        public Guid StageId { get; set; }

        public int? ExpertResultId { get; set; }

        public virtual CommissionConclusionType CommissionConclusionType { get; set; }

        public virtual Commission Commission { get; set; }

        public virtual EXP_DrugDosage EXP_DrugDosage { get; set; }

        public virtual EXP_ExpertiseStageDosageResult EXP_ExpertiseStageDosageResult { get; set; }

        public virtual EXP_ExpertiseStage EXP_ExpertiseStage { get; set; }
    }
}
