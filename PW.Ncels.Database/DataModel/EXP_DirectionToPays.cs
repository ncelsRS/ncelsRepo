namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DirectionToPays
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DirectionToPays()
        {
            EXP_DirectionToPays_PriceList = new HashSet<EXP_DirectionToPays_PriceList>();
            EXP_DrugDeclaration = new HashSet<EXP_DrugDeclaration>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Number { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public DateTime DirectionDate { get; set; }

        public Guid? PayerId { get; set; }

        [StringLength(4000)]
        public string PayerValue { get; set; }

        public Guid CreateEmployeeId { get; set; }

        [Required]
        [StringLength(1024)]
        public string CreateEmployeeValue { get; set; }

        public decimal? TotalPrice { get; set; }

        [StringLength(512)]
        public string InvoiceCode1C { get; set; }

        [StringLength(512)]
        public string InvoiceNumber1C { get; set; }

        public DateTime? InvoiceDatetime1C { get; set; }

        public DateTime? PaymentDatetime1C { get; set; }

        public decimal? PaymentValue1C { get; set; }

        [StringLength(4000)]
        public string PaymentComment1C { get; set; }

        public Guid StatusId { get; set; }

        [StringLength(4000)]
        public string StatusValue { get; set; }

        public int Type { get; set; }

        [StringLength(4000)]
        public string TypeValue { get; set; }

        public int? PageCount { get; set; }

        public decimal? PriceForPage { get; set; }

        public virtual Dictionary Status { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Organization Organization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DirectionToPays_PriceList> EXP_DirectionToPays_PriceList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclaration> EXP_DrugDeclaration { get; set; }
    }
}
