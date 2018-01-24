namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TmcIn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TmcIn()
        {
            LimsTmcTemps = new HashSet<LimsTmcTemp>();
        }

        public Guid Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public Guid CreatedEmployeeId { get; set; }

        public int StateType { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        [StringLength(450)]
        public string Provider { get; set; }

        [StringLength(450)]
        public string ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastDeliveryDate { get; set; }

        public bool IsFullDelivery { get; set; }

        [StringLength(450)]
        public string PowerOfAttorney { get; set; }

        [StringLength(450)]
        public string ProviderBin { get; set; }

        public Guid? ExecutorEmployeeId { get; set; }

        public Guid? AgreementEmployeeId { get; set; }

        public bool IsScan { get; set; }

        public Guid? AccountantEmployeeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsTmcTemp> LimsTmcTemps { get; set; }
    }
}
