namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_Ref_PriceList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMP_Ref_PriceList()
        {
            EMP_CostWorks = new HashSet<EMP_CostWorks>();
        }

        public Guid Id { get; set; }

        public Guid ServiceTypeId { get; set; }

        public Guid PriceTypeId { get; set; }

        public bool? Import { get; set; }

        public decimal? Price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_CostWorks> EMP_CostWorks { get; set; }

        public virtual EMP_Ref_PriceType EMP_Ref_PriceType { get; set; }

        public virtual EMP_Ref_ServiceType EMP_Ref_ServiceType { get; set; }
    }
}
