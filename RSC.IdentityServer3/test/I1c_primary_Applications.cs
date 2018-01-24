namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_Applications
    {
        public Guid Id { get; set; }

        public DateTime ExportDatetime { get; set; }

        public DateTime? ImportDatetime { get; set; }

        public Guid? ApplicationId { get; set; }

        [StringLength(450)]
        public string ApplicationNumber { get; set; }

        public DateTime? ApplicationDatetime { get; set; }

        [StringLength(500)]
        public string ApplicationType { get; set; }

        [StringLength(4000)]
        public string Producer { get; set; }

        public Guid? ProducerId { get; set; }

        [StringLength(4000)]
        public string Applicant { get; set; }

        public Guid? ApplicantId { get; set; }

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
        public string Address { get; set; }

        [StringLength(500)]
        public string Phone { get; set; }

        [StringLength(4000)]
        public string Payer { get; set; }

        public Guid? PayerId { get; set; }

        [StringLength(4000)]
        public string PayerAddress { get; set; }

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
        public string PayerCurrencyCode { get; set; }

        public Guid? PayerCurrencyId { get; set; }

        [StringLength(500)]
        public string Country { get; set; }

        public Guid? CountryId { get; set; }

        public bool? IsResident { get; set; }

        public bool? IsLegal { get; set; }

        [StringLength(500)]
        public string InvoiceNumber1C { get; set; }

        public DateTime? InvoiceDatetime1C { get; set; }

        [StringLength(500)]
        public string StatementNumber { get; set; }

        [StringLength(500)]
        public string DrugTradeName { get; set; }

        [StringLength(500)]
        public string DrugTradeNameKz { get; set; }

        [StringLength(500)]
        public string DrugPackage { get; set; }

        [StringLength(500)]
        public string DrugPackageKz { get; set; }

        public int? TypeId { get; set; }

        [StringLength(500)]
        public string Type { get; set; }

        public decimal? TotalPrice { get; set; }

        public bool? ManufactureIsResident { get; set; }

        public string DrugFormName { get; set; }

        public string DrugFormNameKz { get; set; }
    }
}
