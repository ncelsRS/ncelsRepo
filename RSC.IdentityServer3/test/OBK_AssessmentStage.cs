namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentStage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_AssessmentStage()
        {
            OBK_AssessmentStageExecutors = new HashSet<OBK_AssessmentStageExecutors>();
            OBK_AssessmentStageSignData = new HashSet<OBK_AssessmentStageSignData>();
        }

        public Guid Id { get; set; }

        public Guid DeclarationId { get; set; }

        public int StageId { get; set; }

        public int StageStatusId { get; set; }

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

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_Ref_Stage OBK_Ref_Stage { get; set; }

        public virtual OBK_Ref_StageStatus OBK_Ref_StageStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentStageExecutors> OBK_AssessmentStageExecutors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentStageSignData> OBK_AssessmentStageSignData { get; set; }
    }
}
