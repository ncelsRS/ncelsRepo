namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_AssessmentDeclaration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_AssessmentDeclaration()
        {
            OBK_ActReception = new HashSet<OBK_ActReception>();
            OBK_AssessmentDeclaration__OBK_ExpertCouncil = new HashSet<OBK_AssessmentDeclaration__OBK_ExpertCouncil>();
            OBK_AssessmentDeclarationCom = new HashSet<OBK_AssessmentDeclarationCom>();
            OBK_AssessmentDeclarationFieldHistory = new HashSet<OBK_AssessmentDeclarationFieldHistory>();
            OBK_AssessmentDeclarationHistory = new HashSet<OBK_AssessmentDeclarationHistory>();
            OBK_AssessmentStage = new HashSet<OBK_AssessmentStage>();
            OBK_AssessmentStageOP = new HashSet<OBK_AssessmentStageOP>();
            OBK_CertificateOfCompletion = new HashSet<OBK_CertificateOfCompletion>();
            OBK_OP_Commission = new HashSet<OBK_OP_Commission>();
            OBK_StageExpDocument = new HashSet<OBK_StageExpDocument>();
            OBK_StageExpDocumentResult = new HashSet<OBK_StageExpDocumentResult>();
            OBK_Tasks = new HashSet<OBK_Tasks>();
        }

        public Guid Id { get; set; }

        public string CertificateGMP { get; set; }

        public string CertificateNumber { get; set; }

        public bool AssuranceCheck { get; set; }

        public bool OrderCheck { get; set; }

        public bool StabilityCheck { get; set; }

        public bool PaymentCheck { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid? ContractId { get; set; }

        public DateTime? CertificateDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool? CertificateGMPCheck { get; set; }

        public int StatusId { get; set; }

        public string InvoiceRu { get; set; }

        public string InvoiceKz { get; set; }

        public DateTime? InvoiceDate { get; set; }

        public string InvoiceContractRu { get; set; }

        public string InvoiceContractKz { get; set; }

        public string InvoiceAgentLastName { get; set; }

        public string InvoiceAgentFirstName { get; set; }

        public string InvoiceAgentMiddelName { get; set; }

        public string InvoiceAgentPositionName { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public DateTime? SendDate { get; set; }

        public Guid? ExecuterId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DesignDate { get; set; }

        public DateTime? FirstSendDate { get; set; }

        public bool IsSigned { get; set; }

        public string DesignNote { get; set; }

        public Guid? CertificateCountryId { get; set; }

        [StringLength(500)]
        public string CertificateOrganization { get; set; }

        public DateTime? CertificateStartDate { get; set; }

        public int? CertificateTypeId { get; set; }

        public DateTime? InvoiceContractDate { get; set; }

        [StringLength(512)]
        public string CertificateManufacturName { get; set; }

        public int TypeId { get; set; }

        public bool? DomesticProducer { get; set; }

        public bool? KfSelection { get; set; }

        public bool? GDPItself { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? ExpertRequest { get; set; }

        public bool? ApplicantAgreement { get; set; }

        public bool? OBKApplicantParty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ActReception> OBK_ActReception { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclaration__OBK_ExpertCouncil> OBK_AssessmentDeclaration__OBK_ExpertCouncil { get; set; }

        public virtual OBK_Ref_Type OBK_Ref_Type { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration1 { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration2 { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }

        public virtual OBK_Ref_CertificateType OBK_Ref_CertificateType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclarationCom> OBK_AssessmentDeclarationCom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclarationFieldHistory> OBK_AssessmentDeclarationFieldHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclarationHistory> OBK_AssessmentDeclarationHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentStage> OBK_AssessmentStage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentStageOP> OBK_AssessmentStageOP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_CertificateOfCompletion> OBK_CertificateOfCompletion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_OP_Commission> OBK_OP_Commission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_StageExpDocument> OBK_StageExpDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_StageExpDocumentResult> OBK_StageExpDocumentResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Tasks> OBK_Tasks { get; set; }
    }
}
