namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_ContractExtHistoryStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Ref_ContractExtHistoryStatus()
        {
            OBK_ContractExtHistory = new HashSet<OBK_ContractExtHistory>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(2000)]
        public string NameRu { get; set; }

        [StringLength(2000)]
        public string NameKz { get; set; }

        [StringLength(2000)]
        public string DescriptionRu { get; set; }

        [StringLength(2000)]
        public string DescriptionKz { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DateEdit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractExtHistory> OBK_ContractExtHistory { get; set; }
    }
}
