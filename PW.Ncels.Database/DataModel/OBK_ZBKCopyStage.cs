namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKCopyStage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_ZBKCopyStage()
        {
            OBK_ZBKCopyStageSignData = new HashSet<OBK_ZBKCopyStageSignData>();
        }

        public Guid Id { get; set; }

        public Guid? OBK_ZBKCopyId { get; set; }

        public int StageId { get; set; }

        public int StageStatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? ResultId { get; set; }

        public bool? SendToAccountant { get; set; }

        public virtual OBK_Ref_Stage OBK_Ref_Stage { get; set; }

        public virtual OBK_Ref_StageStatus OBK_Ref_StageStatus { get; set; }

        public virtual OBK_ZBKCopyStageExecutors OBK_ZBKCopyStageExecutors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopyStageSignData> OBK_ZBKCopyStageSignData { get; set; }
    }
}
