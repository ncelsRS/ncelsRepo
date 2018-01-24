namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContractJournal")]
    public partial class ContractJournal
    {
        public Guid Id { get; set; }

        public Guid? ManufacturerOrganizationId { get; set; }

        public Guid? ApplicantOrganizationId { get; set; }

        public Guid? DoverennostTypeDicId { get; set; }

        [StringLength(500)]
        public string DoverennostNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DoverennostCreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DoverennostExpiryDate { get; set; }

        public Guid? HolderOrganizationId { get; set; }

        public Guid? PayerOrganizationId { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public Guid? ContractId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public bool? IsExpired { get; set; }

        public bool? IsSite { get; set; }

        [StringLength(500)]
        public string ManufacturerName { get; set; }

        [StringLength(4000)]
        public string ManufacturerCountry { get; set; }

        [StringLength(4000)]
        public string ApplicantCountry { get; set; }

        [StringLength(4000)]
        public string ApplicantCurrency { get; set; }

        [StringLength(255)]
        public string Login { get; set; }

        [StringLength(4000)]
        public string DocumentTypeName { get; set; }

        [StringLength(4000)]
        public string StatusName { get; set; }

        [StringLength(4000)]
        public string StatusCode { get; set; }

        [StringLength(500)]
        public string HolderName { get; set; }

        [StringLength(500)]
        public string ApplicantName { get; set; }

        [StringLength(500)]
        public string PayerName { get; set; }

        [StringLength(4000)]
        public string CorrespondentsId { get; set; }

        [StringLength(4000)]
        public string CorrespondentsValue { get; set; }

        [StringLength(4000)]
        public string ExecutorId { get; set; }

        public int? CountApplications { get; set; }

        public Guid? SignerId { get; set; }
    }
}
