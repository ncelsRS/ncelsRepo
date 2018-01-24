namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_ContractHistoryStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Ref_ContractHistoryStatus()
        {
            EMP_ContractHistory = new HashSet<EMP_ContractHistory>();
            OBK_ContractHistory = new HashSet<OBK_ContractHistory>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(2000)]
        public string NameRu { get; set; }

        [StringLength(2000)]
        public string NameKz { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DateEdit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_ContractHistory> EMP_ContractHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractHistory> OBK_ContractHistory { get; set; }
    }
}
