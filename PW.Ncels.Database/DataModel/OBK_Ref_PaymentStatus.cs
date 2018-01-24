namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_PaymentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Ref_PaymentStatus()
        {
            EMP_DirectionToPayments = new HashSet<EMP_DirectionToPayments>();
        }

        public Guid Id { get; set; }

        public string Code { get; set; }

        [Required]
        public string NameRu { get; set; }

        [Required]
        public string NameKz { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_DirectionToPayments> EMP_DirectionToPayments { get; set; }
    }
}
