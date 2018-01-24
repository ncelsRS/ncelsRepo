namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_ObkApplications
    {
        public Guid Id { get; set; }

        [StringLength(450)]
        public string Organization { get; set; }

        [StringLength(450)]
        public string OrganizationCode { get; set; }

        public DateTime ExportDatetime { get; set; }

        [StringLength(450)]
        public string ContractNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractStartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractEndDate { get; set; }

        public Guid? ContractId { get; set; }

        [StringLength(500)]
        public string DoverennostNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DoverennostCreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DoverennostExpiryDate { get; set; }

        [StringLength(4000)]
        public string Payer { get; set; }

        public Guid? PayerId { get; set; }

        [StringLength(4000)]
        public string PayerAddress { get; set; }

        [StringLength(500)]
        public string PayerPhone { get; set; }

        [StringLength(4000)]
        public string PayerBank { get; set; }

        [StringLength(500)]
        public string PayerAccount { get; set; }

        [StringLength(500)]
        public string PayerBIK { get; set; }

        [StringLength(500)]
        public string PayerSWIFT { get; set; }

        [StringLength(500)]
        public string PayerBIN { get; set; }

        [StringLength(500)]
        public string PayerIIN { get; set; }

        [StringLength(500)]
        public string PayerCurrency { get; set; }

        [StringLength(500)]
        public string PayerCurrencyId { get; set; }

        [StringLength(500)]
        public string Country { get; set; }

        public Guid? CountryId { get; set; }

        public bool? IsResident { get; set; }

        public bool? IsLegal { get; set; }

        [StringLength(500)]
        public string Type { get; set; }

        public int? TypeId { get; set; }

        public decimal? TotalPrice { get; set; }

        [StringLength(512)]
        public string InvoiceNumber1C { get; set; }

        public DateTime? InvoiceDatetime1C { get; set; }

        public Guid? ZBKCopyId { get; set; }
    }
}
