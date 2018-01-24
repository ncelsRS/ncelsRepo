namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContractsView")]
    public partial class ContractsView
    {
        [Key]
        [Column(Order = 0)]
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

        public Guid? PayerTranslationOrganizationId { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public Guid? ContractId { get; set; }

        public int? Type { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public bool? IsExpired { get; set; }

        public bool? IsSite { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsHasDoverennostNumber { get; set; }

        [StringLength(500)]
        public string ManufactureOrgName { get; set; }

        [StringLength(500)]
        public string ApplicantOrgName { get; set; }

        [StringLength(500)]
        public string HolderOrgName { get; set; }

        [StringLength(500)]
        public string PayerOrgName { get; set; }

        [StringLength(500)]
        public string PayerTranslationOrgName { get; set; }

        [StringLength(4000)]
        public string DoverenostName { get; set; }

        [StringLength(4000)]
        public string StatusName { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid OwnerId { get; set; }

        public Guid? ContractAdditionTypeId { get; set; }

        [StringLength(4000)]
        public string ContractAdditionTypeName { get; set; }

        public Guid? AgentOrganizationId { get; set; }

        public Guid? AgentDocTypeDicId { get; set; }

        [StringLength(500)]
        public string AgentDocNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AgentDocCreateDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AgentDocExpiryDate { get; set; }

        public bool? IsHasAgentDocNumber { get; set; }

        [StringLength(4000)]
        public string AgentDocName { get; set; }
    }
}
