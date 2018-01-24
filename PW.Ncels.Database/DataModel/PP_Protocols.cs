namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_Protocols
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PP_Protocols()
        {
            PP_ProtocolComissionMembers = new HashSet<PP_ProtocolComissionMembers>();
            PP_ProtocolProductPrices = new HashSet<PP_ProtocolProductPrices>();
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public DateTime ProtocolDate { get; set; }

        public Guid OwnerId { get; set; }

        public Guid? ChiefId { get; set; }

        public Guid? RequesterId { get; set; }

        [StringLength(500)]
        public string RequesterName { get; set; }

        [StringLength(500)]
        public string AdditionalPersonName { get; set; }

        public bool IsImn { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_ProtocolComissionMembers> PP_ProtocolComissionMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_ProtocolProductPrices> PP_ProtocolProductPrices { get; set; }
    }
}
