namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Contract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Contract()
        {
            OBK_AssessmentDeclaration = new HashSet<OBK_AssessmentDeclaration>();
            OBK_CertificateOfCompletion = new HashSet<OBK_CertificateOfCompletion>();
            OBK_ContractCom = new HashSet<OBK_ContractCom>();
            OBK_ContractExtHistory = new HashSet<OBK_ContractExtHistory>();
            OBK_ContractFactory = new HashSet<OBK_ContractFactory>();
            OBK_ContractPrice = new HashSet<OBK_ContractPrice>();
            OBK_ContractStage = new HashSet<OBK_ContractStage>();
            OBK_DirectionToPayments = new HashSet<OBK_DirectionToPayments>();
            OBK_LetterPortalEdo = new HashSet<OBK_LetterPortalEdo>();
            OBK_RS_Products = new HashSet<OBK_RS_Products>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Number { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid? DeclarantId { get; set; }

        public Guid? DeclarantContactId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? ContractType { get; set; }

        public Guid? ExpertOrganization { get; set; }

        public Guid? Signer { get; set; }

        public DateTime? SendDate { get; set; }

        public Guid? ContractAdditionType { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclaration> OBK_AssessmentDeclaration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_CertificateOfCompletion> OBK_CertificateOfCompletion { get; set; }

        public virtual Unit Unit { get; set; }

        public virtual OBK_Contract OBK_Contract1 { get; set; }

        public virtual OBK_Contract OBK_Contract2 { get; set; }

        public virtual OBK_Declarant OBK_Declarant { get; set; }

        public virtual OBK_DeclarantContact OBK_DeclarantContact { get; set; }

        public virtual OBK_Ref_Status OBK_Ref_Status { get; set; }

        public virtual OBK_Ref_Type OBK_Ref_Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractCom> OBK_ContractCom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractExtHistory> OBK_ContractExtHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractFactory> OBK_ContractFactory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractPrice> OBK_ContractPrice { get; set; }

        public virtual OBK_ContractSignedDatas OBK_ContractSignedDatas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractStage> OBK_ContractStage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_DirectionToPayments> OBK_DirectionToPayments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_LetterPortalEdo> OBK_LetterPortalEdo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_RS_Products> OBK_RS_Products { get; set; }
    }
}
