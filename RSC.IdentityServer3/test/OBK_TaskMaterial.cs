namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_TaskMaterial
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_TaskMaterial()
        {
            OBK_ResearchCenterResult = new HashSet<OBK_ResearchCenterResult>();
            OBK_TaskExecutor = new HashSet<OBK_TaskExecutor>();
        }

        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public Guid LaboratoryTypeId { get; set; }

        public int ProductSeriesId { get; set; }

        public int? Quantity { get; set; }

        public Guid? UnitLaboratoryId { get; set; }

        [StringLength(50)]
        public string IdNumber { get; set; }

        public Guid? StorageConditionId { get; set; }

        public Guid? ExternalConditionId { get; set; }

        [StringLength(50)]
        public string DimensionIMN { get; set; }

        public bool? ExpertiseResult { get; set; }

        [StringLength(255)]
        public string Regulation { get; set; }

        [StringLength(50)]
        public string SubTaskNumber { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? StatusId { get; set; }

        public Guid? LaboratoryAssistantId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_Procunts_Series OBK_Procunts_Series { get; set; }

        public virtual OBK_Ref_LaboratoryType OBK_Ref_LaboratoryType { get; set; }

        public virtual OBK_Ref_MaterialCondition OBK_Ref_MaterialCondition { get; set; }

        public virtual OBK_Ref_MaterialCondition OBK_Ref_MaterialCondition1 { get; set; }

        public virtual OBK_Ref_StageStatus OBK_Ref_StageStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ResearchCenterResult> OBK_ResearchCenterResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskExecutor> OBK_TaskExecutor { get; set; }

        public virtual OBK_Tasks OBK_Tasks { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
