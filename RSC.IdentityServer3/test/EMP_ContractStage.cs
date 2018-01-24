namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_ContractStage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMP_ContractStage()
        {
            EMP_ContractStage1 = new HashSet<EMP_ContractStage>();
            EMP_ContractStageExecutors = new HashSet<EMP_ContractStageExecutors>();
        }

        public Guid Id { get; set; }

        public Guid ContractId { get; set; }

        public Guid StageId { get; set; }

        public Guid StageStatusId { get; set; }

        public Guid? ParentStageId { get; set; }

        public int? Result { get; set; }

        public DateTime DateCreate { get; set; }

        public virtual EMP_Contract EMP_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_ContractStage> EMP_ContractStage1 { get; set; }

        public virtual EMP_ContractStage EMP_ContractStage2 { get; set; }

        public virtual EMP_Ref_Stage EMP_Ref_Stage { get; set; }

        public virtual EMP_Ref_StageStatus EMP_Ref_StageStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_ContractStageExecutors> EMP_ContractStageExecutors { get; set; }
    }
}
