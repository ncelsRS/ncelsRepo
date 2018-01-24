namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Dictionary
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dictionary()
        {
            Contracts = new HashSet<Contract>();
            Contracts1 = new HashSet<Contract>();
            Contracts2 = new HashSet<Contract>();
            Contracts3 = new HashSet<Contract>();
            Contracts4 = new HashSet<Contract>();
            Dictionaries1 = new HashSet<Dictionary>();
            EXP_Activities = new HashSet<EXP_Activities>();
            EXP_Activities1 = new HashSet<EXP_Activities>();
            EXP_Activities2 = new HashSet<EXP_Activities>();
            EXP_AgreementProcSettings = new HashSet<EXP_AgreementProcSettings>();
            EXP_AgreementProcSettingsActivities = new HashSet<EXP_AgreementProcSettingsActivities>();
            EXP_AgreementProcSettingsTasks = new HashSet<EXP_AgreementProcSettingsTasks>();
            EXP_CertificateOfCompletion = new HashSet<EXP_CertificateOfCompletion>();
            EXP_DirectionToPays = new HashSet<EXP_DirectionToPays>();
            EXP_DrugCorespondence = new HashSet<EXP_DrugCorespondence>();
            EXP_DrugPrimaryFinalDocument = new HashSet<EXP_DrugPrimaryFinalDocument>();
            EXP_ExpertiseStageDosage = new HashSet<EXP_ExpertiseStageDosage>();
            EXP_MaterialDirections = new HashSet<EXP_MaterialDirections>();
            EXP_Materials = new HashSet<EXP_Materials>();
            EXP_Materials1 = new HashSet<EXP_Materials>();
            EXP_Materials2 = new HashSet<EXP_Materials>();
            EXP_Materials3 = new HashSet<EXP_Materials>();
            EXP_Materials4 = new HashSet<EXP_Materials>();
            EXP_Materials5 = new HashSet<EXP_Materials>();
            EXP_Materials6 = new HashSet<EXP_Materials>();
            EXP_Materials7 = new HashSet<EXP_Materials>();
            EXP_Materials8 = new HashSet<EXP_Materials>();
            EXP_Materials9 = new HashSet<EXP_Materials>();
            EXP_Tasks = new HashSet<EXP_Tasks>();
            EXP_Tasks1 = new HashSet<EXP_Tasks>();
            FileLinks = new HashSet<FileLink>();
            LimsEquipments = new HashSet<LimsEquipment>();
            LimsEquipments1 = new HashSet<LimsEquipment>();
            LimsEquipments2 = new HashSet<LimsEquipment>();
            LimsEquipments3 = new HashSet<LimsEquipment>();
            LimsEquipments4 = new HashSet<LimsEquipment>();
            LimsEquipments5 = new HashSet<LimsEquipment>();
            LimsEquipments6 = new HashSet<LimsEquipment>();
            LimsEquipmentActs = new HashSet<LimsEquipmentAct>();
            LimsEquipmentActSpareParts = new HashSet<LimsEquipmentActSparePart>();
            LimsEquipmentJournals = new HashSet<LimsEquipmentJournal>();
            LimsEquipmentPlans = new HashSet<LimsEquipmentPlan>();
            OBK_ActReception = new HashSet<OBK_ActReception>();
            OBK_Contract = new HashSet<OBK_Contract>();
            OBK_Ref_PriceList = new HashSet<OBK_Ref_PriceList>();
            PP_AttachRemarks = new HashSet<PP_AttachRemarks>();
            PP_DIC_AvgExchangeRate = new HashSet<PP_DIC_AvgExchangeRate>();
            PP_PharmaList = new HashSet<PP_PharmaList>();
        }

        public Guid Id { get; set; }

        [StringLength(4000)]
        public string Code { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Type { get; set; }

        [StringLength(4000)]
        public string ExpireDate { get; set; }

        [StringLength(4000)]
        public string Year { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }

        [StringLength(4000)]
        public string DepartmentsId { get; set; }

        [StringLength(4000)]
        public string DepartmentsValue { get; set; }

        public Guid? ParentId { get; set; }

        [StringLength(4000)]
        public string DisplayName { get; set; }

        [StringLength(4000)]
        public string EmployeesValue { get; set; }

        [StringLength(4000)]
        public string EmployeesId { get; set; }

        [StringLength(4000)]
        public string NameKz { get; set; }

        public bool IsGuide { get; set; }

        public Guid? OrganizationId { get; set; }

        [StringLength(500)]
        public string AdditionalInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dictionary> Dictionaries1 { get; set; }

        public virtual Dictionary Dictionary1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Activities> EXP_Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Activities> EXP_Activities1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Activities> EXP_Activities2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_AgreementProcSettings> EXP_AgreementProcSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_AgreementProcSettingsActivities> EXP_AgreementProcSettingsActivities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_AgreementProcSettingsTasks> EXP_AgreementProcSettingsTasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_CertificateOfCompletion> EXP_CertificateOfCompletion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DirectionToPays> EXP_DirectionToPays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugCorespondence> EXP_DrugCorespondence { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPrimaryFinalDocument> EXP_DrugPrimaryFinalDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageDosage> EXP_ExpertiseStageDosage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_MaterialDirections> EXP_MaterialDirections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials5 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials6 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials7 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials8 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials9 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Tasks> EXP_Tasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Tasks> EXP_Tasks1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileLink> FileLinks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipment> LimsEquipments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipment> LimsEquipments1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipment> LimsEquipments2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipment> LimsEquipments3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipment> LimsEquipments4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipment> LimsEquipments5 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipment> LimsEquipments6 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentAct> LimsEquipmentActs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentActSparePart> LimsEquipmentActSpareParts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentJournal> LimsEquipmentJournals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentPlan> LimsEquipmentPlans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ActReception> OBK_ActReception { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Contract> OBK_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Ref_PriceList> OBK_Ref_PriceList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_AttachRemarks> PP_AttachRemarks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_DIC_AvgExchangeRate> PP_DIC_AvgExchangeRate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_PharmaList> PP_PharmaList { get; set; }
    }
}
