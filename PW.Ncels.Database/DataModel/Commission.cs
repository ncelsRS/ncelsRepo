namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Commission
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Commission()
        {
            CommissionDrugDosages = new HashSet<CommissionDrugDosage>();
            CommissionQuestions = new HashSet<CommissionQuestion>();
            CommissionUnits = new HashSet<CommissionUnit>();
            EXP_ExpertiseStageDosageResult = new HashSet<EXP_ExpertiseStageDosageResult>();
        }

        public int Id { get; set; }

        public int Number { get; set; }

        [Required]
        [StringLength(100)]
        public string FullNumber { get; set; }

        public int KindId { get; set; }

        public int TypeId { get; set; }

        public DateTime Date { get; set; }

        public bool IsComplete { get; set; }

        [StringLength(4000)]
        public string Comment { get; set; }

        public bool? IsNeedSendTimeOverNotifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionDrugDosage> CommissionDrugDosages { get; set; }

        public virtual CommissionKind CommissionKind { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionQuestion> CommissionQuestions { get; set; }

        public virtual CommissionType CommissionType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionUnit> CommissionUnits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageDosageResult> EXP_ExpertiseStageDosageResult { get; set; }
    }
}
