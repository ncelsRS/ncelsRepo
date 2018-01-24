namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_Ref_Bank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMP_Ref_Bank()
        {
            OBK_DeclarantContact = new HashSet<OBK_DeclarantContact>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        public string NameRu { get; set; }

        [Required]
        public string NameKz { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsConfirmed { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_DeclarantContact> OBK_DeclarantContact { get; set; }
    }
}
