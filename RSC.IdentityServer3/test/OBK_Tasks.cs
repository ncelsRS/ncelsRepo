namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Tasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Tasks()
        {
            OBK_TaskExecutor = new HashSet<OBK_TaskExecutor>();
            OBK_TaskMaterial = new HashSet<OBK_TaskMaterial>();
            OBK_TaskStatus = new HashSet<OBK_TaskStatus>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string TaskNumber { get; set; }

        [StringLength(50)]
        public string RegisterDate { get; set; }

        public Guid? ExecutorId { get; set; }

        public Guid UnitId { get; set; }

        public Guid ActReceptionId { get; set; }

        public Guid AssessmentDeclarationId { get; set; }

        public DateTime? TaskEndDate { get; set; }

        public bool IsSigned { get; set; }

        public bool SendToCoz { get; set; }

        public bool AcceptToCoz { get; set; }

        public DateTime? SendToIC { get; set; }

        public Guid? CozExecutorId { get; set; }

        public int? TaskStatusId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_ActReception OBK_ActReception { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_Ref_StageStatus OBK_Ref_StageStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskExecutor> OBK_TaskExecutor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskMaterial> OBK_TaskMaterial { get; set; }

        public virtual Unit Unit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskStatus> OBK_TaskStatus { get; set; }
    }
}
