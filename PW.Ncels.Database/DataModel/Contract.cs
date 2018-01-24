namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contract()
        {
            ContractComments = new HashSet<ContractComment>();
            Contracts1 = new HashSet<Contract>();
        }

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

        public bool IsHasDoverennostNumber { get; set; }

        public Guid OwnerId { get; set; }

        public Guid? PayerTranslationOrganizationId { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? SignerId { get; set; }

        public Guid? ContractAdditionTypeId { get; set; }

        public Guid? HolderTypeId { get; set; }

        public bool? IsHasDigCert { get; set; }

        public bool? IsHasAgentDocNumber { get; set; }

        public Guid? AgentDocTypeDicId { get; set; }

        [StringLength(500)]
        public string AgentDocNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AgentDocCreateDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AgentDocExpiryDate { get; set; }

        public Guid? AgentOrganizationId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractComment> ContractComments { get; set; }

        public virtual Organization ApplicantOrganization { get; set; }

        public virtual Dictionary ContractAdditionType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts1 { get; set; }

        public virtual Contract ParentContract { get; set; }

        public virtual Dictionary DoverennostType { get; set; }

        public virtual Employee Signer { get; set; }

        public virtual Organization HolderOrganization { get; set; }

        public virtual Organization PayerOrganization { get; set; }

        public virtual Organization PayerTranslationOrganization { get; set; }

        public virtual Dictionary Dictionary2 { get; set; }

        public virtual Organization AgentOrganization { get; set; }

        public virtual Dictionary ContractStatus { get; set; }

        public virtual Organization ManufacturerOrganization { get; set; }

        public virtual Dictionary HolderType { get; set; }

        public virtual ContractSignedData ContractSignedData { get; set; }
    }
}
