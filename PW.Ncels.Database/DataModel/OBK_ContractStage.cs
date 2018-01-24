namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractStage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_ContractStage()
        {
            OBK_ContractStage1 = new HashSet<OBK_ContractStage>();
            OBK_ContractStageExecutors = new HashSet<OBK_ContractStageExecutors>();
        }

        public Guid Id { get; set; }

        public Guid ContractId { get; set; }

        public int StageId { get; set; }

        public int StageStatusId { get; set; }

        public Guid? ParentStageId { get; set; }

        public int? ResultId { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractStage> OBK_ContractStage1 { get; set; }

        public virtual OBK_ContractStage OBK_ContractStage2 { get; set; }

        public virtual OBK_Ref_Stage OBK_Ref_Stage { get; set; }

        public virtual OBK_Ref_StageStatus OBK_Ref_StageStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractStageExecutors> OBK_ContractStageExecutors { get; set; }
    }
}
