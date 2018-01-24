namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_OP_CommissionRoles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_OP_CommissionRoles()
        {
            OBK_OP_Commission = new HashSet<OBK_OP_Commission>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NameRu { get; set; }

        [Required]
        [StringLength(50)]
        public string NameKk { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_OP_Commission> OBK_OP_Commission { get; set; }
    }
}
