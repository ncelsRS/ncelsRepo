namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RequestOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RequestOrder()
        {
            RequestLists = new HashSet<RequestList>();
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public int OrderType { get; set; }

        public int OrderYear { get; set; }

        [StringLength(500)]
        public string OrderNumber { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RequestList> RequestLists { get; set; }
    }
}
