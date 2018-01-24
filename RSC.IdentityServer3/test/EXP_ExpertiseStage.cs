namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertiseStage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_ExpertiseStage()
        {
            CommissionDrugDosages = new HashSet<CommissionDrugDosage>();
            CommissionDrugDosageNeedCommissions = new HashSet<CommissionDrugDosageNeedCommission>();
            EXP_ExpertiseStage1 = new HashSet<EXP_ExpertiseStage>();
            EXP_ExpertiseStageDosage = new HashSet<EXP_ExpertiseStageDosage>();
            EXP_ExpertiseStageExecutors = new HashSet<EXP_ExpertiseStageExecutors>();
            EXP_ExpertiseStageRemark = new HashSet<EXP_ExpertiseStageRemark>();
        }

        public Guid Id { get; set; }

        public Guid DeclarationId { get; set; }

        public int StageId { get; set; }

        public int StatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? FactEndDate { get; set; }

        public int? ResultId { get; set; }

        public Guid? ParentStageId { get; set; }

        public bool IsHistory { get; set; }

        [StringLength(4000)]
        public string OtdIds { get; set; }

        public string OtdRemarks { get; set; }

        public bool IsSuspended { get; set; }

        public DateTime? SuspendedStartDate { get; set; }

        public bool IsNew { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionDrugDosage> CommissionDrugDosages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionDrugDosageNeedCommission> CommissionDrugDosageNeedCommissions { get; set; }

        public virtual EXP_DIC_Stage EXP_DIC_Stage { get; set; }

        public virtual EXP_DIC_StageResult EXP_DIC_StageResult { get; set; }

        public virtual EXP_DIC_StageStatus EXP_DIC_StageStatus { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStage> EXP_ExpertiseStage1 { get; set; }

        public virtual EXP_ExpertiseStage EXP_ExpertiseStage2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageDosage> EXP_ExpertiseStageDosage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageExecutors> EXP_ExpertiseStageExecutors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageRemark> EXP_ExpertiseStageRemark { get; set; }
    }
}
