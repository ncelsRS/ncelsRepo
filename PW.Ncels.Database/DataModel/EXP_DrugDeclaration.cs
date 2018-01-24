namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugDeclaration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DrugDeclaration()
        {
            EXP_CertificateOfCompletion = new HashSet<EXP_CertificateOfCompletion>();
            EXP_DrugChangeType = new HashSet<EXP_DrugChangeType>();
            EXP_DrugCorespondence = new HashSet<EXP_DrugCorespondence>();
            EXP_DrugDeclarationCom = new HashSet<EXP_DrugDeclarationCom>();
            EXP_DrugDeclarationFieldHistory = new HashSet<EXP_DrugDeclarationFieldHistory>();
            EXP_DrugDeclarationHistory = new HashSet<EXP_DrugDeclarationHistory>();
            EXP_DrugDosage = new HashSet<EXP_DrugDosage>();
            EXP_DrugExportTrade = new HashSet<EXP_DrugExportTrade>();
            EXP_DrugOtherCountry = new HashSet<EXP_DrugOtherCountry>();
            EXP_DrugPatent = new HashSet<EXP_DrugPatent>();
            EXP_DrugPrimaryNTD = new HashSet<EXP_DrugPrimaryNTD>();
            EXP_DrugPrimaryRemark = new HashSet<EXP_DrugPrimaryRemark>();
            EXP_DrugProtectionDoc = new HashSet<EXP_DrugProtectionDoc>();
            EXP_DrugType = new HashSet<EXP_DrugType>();
            EXP_DrugUseMethod = new HashSet<EXP_DrugUseMethod>();
            EXP_ExpertiseStage = new HashSet<EXP_ExpertiseStage>();
            EXP_MaterialDirections = new HashSet<EXP_MaterialDirections>();
            EXP_Materials = new HashSet<EXP_Materials>();
            EXP_DrugPrimaryKind = new HashSet<EXP_DrugPrimaryKind>();
            EXP_DrugOrganizations = new HashSet<EXP_DrugOrganizations>();
            EXP_DirectionToPays = new HashSet<EXP_DirectionToPays>();
        }

        public Guid Id { get; set; }

        public int TypeId { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public DateTime CreatedDate { get; set; }

        public int StatusId { get; set; }

        public Guid OwnerId { get; set; }

        public Guid? ContractId { get; set; }

        public int? AccelerationTypeId { get; set; }

        [StringLength(500)]
        public string AccelerationNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AccelerationDate { get; set; }

        [StringLength(500)]
        public string AccelerationNote { get; set; }

        [StringLength(500)]
        public string NameRu { get; set; }

        [StringLength(500)]
        public string NameKz { get; set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        public int? MnnId { get; set; }

        [StringLength(500)]
        public string MnnComment { get; set; }

        public int? DrugFormId { get; set; }

        [StringLength(500)]
        public string DrugFormRu { get; set; }

        [StringLength(500)]
        public string DrugFormKz { get; set; }

        public int? AtxId { get; set; }

        [StringLength(500)]
        public string OriginalName { get; set; }

        public int? SaleTypeId { get; set; }

        public decimal Dosage { get; set; }

        public long? DosageMeasureTypeId { get; set; }

        [StringLength(500)]
        public string DosageNoteKz { get; set; }

        [StringLength(500)]
        public string DosageNoteRu { get; set; }

        [StringLength(500)]
        public string ConcentrationRu { get; set; }

        [StringLength(500)]
        public string ConcentrationKz { get; set; }

        public bool IsGrls { get; set; }

        [StringLength(500)]
        public string Transportation { get; set; }

        public int? ManufactureTypeId { get; set; }

        public bool IsGmp { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GmpExpiryDate { get; set; }

        [StringLength(500)]
        public string BestBefore { get; set; }

        public double? ProposedShelfLife { get; set; }

        public int? ProposedShelfLifeMeasureId { get; set; }

        public long? BestBeforeMeasureTypeDicId { get; set; }

        [StringLength(500)]
        public string AppPeriodOpen { get; set; }

        public long? AppPeriodOpenMeasureDicId { get; set; }

        [StringLength(500)]
        public string AppPeriodMix { get; set; }

        public long? AppPeriodMixMeasureDicId { get; set; }

        [StringLength(500)]
        public string StorageConditions1 { get; set; }

        [StringLength(500)]
        public string StorageConditions2 { get; set; }

        public bool IsConvention { get; set; }

        public DateTime? DesignDate { get; set; }

        [StringLength(2000)]
        public string DesignNote { get; set; }

        public bool IsSigned { get; set; }

        public DateTime? FirstSendDate { get; set; }

        public DateTime? SendDate { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? ExecuterId { get; set; }

        [StringLength(2000)]
        public string ExpeditedType { get; set; }

        public int? RegisterId { get; set; }

        [StringLength(2000)]
        public string GrlsNote { get; set; }

        [StringLength(4000)]
        public string OtdIds { get; set; }

        [StringLength(500)]
        public string AtxComment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_CertificateOfCompletion> EXP_CertificateOfCompletion { get; set; }

        public virtual EXP_DIC_AccelerationType EXP_DIC_AccelerationType { get; set; }

        public virtual EXP_DIC_ManufactureType EXP_DIC_ManufactureType { get; set; }

        public virtual EXP_DIC_PeriodMeasure EXP_DIC_PeriodMeasure { get; set; }

        public virtual EXP_DIC_SaleType EXP_DIC_SaleType { get; set; }

        public virtual EXP_DIC_Status EXP_DIC_Status { get; set; }

        public virtual EXP_DIC_Type EXP_DIC_Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugChangeType> EXP_DrugChangeType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugCorespondence> EXP_DrugCorespondence { get; set; }

        public virtual sr_measures sr_measures { get; set; }

        public virtual sr_measures sr_measures1 { get; set; }

        public virtual sr_register sr_register { get; set; }

        public virtual sr_atc_codes sr_atc_codes { get; set; }

        public virtual sr_dosage_forms sr_dosage_forms { get; set; }

        public virtual sr_international_names sr_international_names { get; set; }

        public virtual sr_measures sr_measures2 { get; set; }

        public virtual sr_measures sr_measures3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclarationCom> EXP_DrugDeclarationCom { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclarationFieldHistory> EXP_DrugDeclarationFieldHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclarationHistory> EXP_DrugDeclarationHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDosage> EXP_DrugDosage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugExportTrade> EXP_DrugExportTrade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugOtherCountry> EXP_DrugOtherCountry { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPatent> EXP_DrugPatent { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPrimaryNTD> EXP_DrugPrimaryNTD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPrimaryRemark> EXP_DrugPrimaryRemark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugProtectionDoc> EXP_DrugProtectionDoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugType> EXP_DrugType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugUseMethod> EXP_DrugUseMethod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStage> EXP_ExpertiseStage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_MaterialDirections> EXP_MaterialDirections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPrimaryKind> EXP_DrugPrimaryKind { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugOrganizations> EXP_DrugOrganizations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DirectionToPays> EXP_DirectionToPays { get; set; }
    }
}
