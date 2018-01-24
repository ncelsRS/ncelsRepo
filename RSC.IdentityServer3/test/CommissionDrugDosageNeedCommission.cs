namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CommissionDrugDosageNeedCommission")]
    public partial class CommissionDrugDosageNeedCommission
    {
        public int Id { get; set; }

        public long DrugDosageId { get; set; }

        public Guid StageId { get; set; }

        public bool IsNeedEs { get; set; }

        public bool IsNeedFmk { get; set; }

        public bool IsNeedFmc { get; set; }

        public virtual EXP_DrugDosage EXP_DrugDosage { get; set; }

        public virtual EXP_ExpertiseStage EXP_ExpertiseStage { get; set; }
    }
}
