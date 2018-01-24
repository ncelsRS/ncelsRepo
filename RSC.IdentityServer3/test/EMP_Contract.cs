namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_Contract
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMP_Contract()
        {
            EMP_ContractExtHistory = new HashSet<EMP_ContractExtHistory>();
            EMP_ContractHistory = new HashSet<EMP_ContractHistory>();
            EMP_ContractStage = new HashSet<EMP_ContractStage>();
            EMP_CostWorks = new HashSet<EMP_CostWorks>();
            EMP_DirectionToPayments = new HashSet<EMP_DirectionToPayments>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Number { get; set; }

        public Guid? HolderType { get; set; }

        public int Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(255)]
        public string MedicalDeviceName { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid? DeclarantId { get; set; }

        public Guid? ManufacturId { get; set; }

        public Guid? PayerId { get; set; }

        public Guid? DeclarantContactId { get; set; }

        public Guid? ManufacturContactId { get; set; }

        public Guid? PayerContactId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? ExpertOrganization { get; set; }

        public Guid? Signer { get; set; }

        public DateTime? SendDate { get; set; }

        public bool? DeclarantIsManufactur { get; set; }

        [StringLength(20)]
        public string ChoosePayer { get; set; }

        public Guid? ContractType { get; set; }

        public Guid? ContractStatusId { get; set; }

        public Guid? ContractScopeId { get; set; }

        [StringLength(255)]
        public string MedicalDeviceNameKz { get; set; }

        public bool? HasProxy { get; set; }

        public int? DocumentType { get; set; }

        [StringLength(20)]
        public string StatemantNumber { get; set; }

        public virtual EMP_Ref_ContractScope EMP_Ref_ContractScope { get; set; }

        public virtual EMP_Ref_Status EMP_Ref_Status { get; set; }

        public virtual EMP_Ref_ContractType EMP_Ref_ContractType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_Declarant OBK_Declarant { get; set; }

        public virtual OBK_DeclarantContact OBK_DeclarantContact { get; set; }

        public virtual OBK_Declarant OBK_DeclarantManufactur { get; set; }

        public virtual OBK_Declarant OBK_DeclarantPayer { get; set; }

        public virtual OBK_DeclarantContact OBK_DeclarantContactManufactur { get; set; }

        public virtual OBK_DeclarantContact OBK_DeclarantContactPayer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_ContractExtHistory> EMP_ContractExtHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_ContractHistory> EMP_ContractHistory { get; set; }

        public virtual EMP_ContractSignData EMP_ContractSignData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_ContractStage> EMP_ContractStage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_CostWorks> EMP_CostWorks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_DirectionToPayments> EMP_DirectionToPayments { get; set; }
    }
}
