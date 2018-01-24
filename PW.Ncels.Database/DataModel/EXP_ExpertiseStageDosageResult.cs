namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertiseStageDosageResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_ExpertiseStageDosageResult()
        {
            CommissionDrugDosages = new HashSet<CommissionDrugDosage>();
        }

        public int Id { get; set; }

        public Guid StageDosageId { get; set; }

        public int? ResultId { get; set; }

        public DateTime? ResultDate { get; set; }

        public Guid? ResultCreatorId { get; set; }

        public int? CommissionId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionDrugDosage> CommissionDrugDosages { get; set; }

        public virtual Commission Commission { get; set; }

        public virtual EXP_DIC_StageResult EXP_DIC_StageResult { get; set; }

        public virtual EXP_ExpertiseStageDosage EXP_ExpertiseStageDosage { get; set; }
    }
}
