namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_DirectionToPaymentsView
    {
        [Key]
        [Column(Order = 0)]
        public DateTime CreateDate { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ContractId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CreateEmployeeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1024)]
        public string CreateEmployeeValue { get; set; }

        public Guid? PayerId { get; set; }

        [StringLength(4000)]
        public string PayerValue { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid StatusId { get; set; }

        public bool? IsDeleted { get; set; }

        [StringLength(4000)]
        public string DisplayName { get; set; }

        public decimal? TotalPrice { get; set; }

        public string StatusCode { get; set; }

        [StringLength(512)]
        public string InvoiceNumber { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(30)]
        public string ContractNumber { get; set; }

        public DateTime? ContractStartDate { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid Id { get; set; }

        [StringLength(512)]
        public string InvoiceNumber1C { get; set; }

        public Guid? ExecutorId { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(3)]
        public string ExecutorSign { get; set; }

        public Guid? ChiefAccountantId { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(3)]
        public string ChiefAccountantSign { get; set; }

        public Guid? ExpertOrganization { get; set; }

        [StringLength(4000)]
        public string ChiefName { get; set; }

        [StringLength(4000)]
        public string ExecutorName { get; set; }

        public Guid? ZBKCopy_id { get; set; }
    }
}
