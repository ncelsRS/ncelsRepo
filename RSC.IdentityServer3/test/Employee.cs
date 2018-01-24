namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            CommissionUnits = new HashSet<CommissionUnit>();
            ContractComments = new HashSet<ContractComment>();
            ContractProcSettings = new HashSet<ContractProcSetting>();
            Contracts = new HashSet<Contract>();
            EMP_Contract = new HashSet<EMP_Contract>();
            EMP_ContractExtHistory = new HashSet<EMP_ContractExtHistory>();
            EMP_ContractStageExecutors = new HashSet<EMP_ContractStageExecutors>();
            EMP_DirectionToPayments = new HashSet<EMP_DirectionToPayments>();
            EXP_Activities = new HashSet<EXP_Activities>();
            EXP_AgreementProcSettingsTasks = new HashSet<EXP_AgreementProcSettingsTasks>();
            EXP_CertificateOfCompletion = new HashSet<EXP_CertificateOfCompletion>();
            EXP_DirectionToPays = new HashSet<EXP_DirectionToPays>();
            EXP_DrugCorespondence = new HashSet<EXP_DrugCorespondence>();
            EXP_DrugCorespondence1 = new HashSet<EXP_DrugCorespondence>();
            EXP_DrugCorespondence2 = new HashSet<EXP_DrugCorespondence>();
            EXP_DrugCorespondenceRemark = new HashSet<EXP_DrugCorespondenceRemark>();
            EXP_DrugDeclarationComRecord = new HashSet<EXP_DrugDeclarationComRecord>();
            EXP_DrugDeclarationFieldHistory = new HashSet<EXP_DrugDeclarationFieldHistory>();
            EXP_DrugDeclarationHistory = new HashSet<EXP_DrugDeclarationHistory>();
            EXP_DrugPrimaryRemark = new HashSet<EXP_DrugPrimaryRemark>();
            EXP_ExpertiseStageExecutors = new HashSet<EXP_ExpertiseStageExecutors>();
            EXP_ExpertiseStageRemark = new HashSet<EXP_ExpertiseStageRemark>();
            FileLinks = new HashSet<FileLink>();
            EXP_MaterialDirections = new HashSet<EXP_MaterialDirections>();
            EXP_MaterialDirections1 = new HashSet<EXP_MaterialDirections>();
            PriceProjectsHistories = new HashSet<PriceProjectsHistory>();
            EXP_RegistrationExpSteps = new HashSet<EXP_RegistrationExpSteps>();
            EXP_Tasks = new HashSet<EXP_Tasks>();
            EXP_Tasks1 = new HashSet<EXP_Tasks>();
            FileLinksCategoryComRecords = new HashSet<FileLinksCategoryComRecord>();
            GridSettings = new HashSet<GridSetting>();
            LimsApplicationJournals = new HashSet<LimsApplicationJournal>();
            LimsApplicationJournals1 = new HashSet<LimsApplicationJournal>();
            LimsApplicationJournals2 = new HashSet<LimsApplicationJournal>();
            LimsEquipments = new HashSet<LimsEquipment>();
            LimsEquipmentActs = new HashSet<LimsEquipmentAct>();
            LimsEquipmentActs1 = new HashSet<LimsEquipmentAct>();
            LimsEquipmentActs2 = new HashSet<LimsEquipmentAct>();
            LimsEquipmentJournalRecords = new HashSet<LimsEquipmentJournalRecord>();
            LimsEquipmentPlans = new HashSet<LimsEquipmentPlan>();
            LimsEquipmentPlans1 = new HashSet<LimsEquipmentPlan>();
            OBK_AssessmentDeclarationComRecord = new HashSet<OBK_AssessmentDeclarationComRecord>();
            OBK_AssessmentDeclarationFieldHistory = new HashSet<OBK_AssessmentDeclarationFieldHistory>();
            OBK_AssessmentStageExecutors = new HashSet<OBK_AssessmentStageExecutors>();
            OBK_AssessmentStageSignData = new HashSet<OBK_AssessmentStageSignData>();
            OBK_Contract = new HashSet<OBK_Contract>();
            OBK_ContractComRecord = new HashSet<OBK_ContractComRecord>();
            OBK_ContractExtHistory = new HashSet<OBK_ContractExtHistory>();
            OBK_ContractPriceComRecord = new HashSet<OBK_ContractPriceComRecord>();
            OBK_ContractStageExecutors = new HashSet<OBK_ContractStageExecutors>();
            OBK_DirectionToPayments = new HashSet<OBK_DirectionToPayments>();
            OBK_OP_Commission = new HashSet<OBK_OP_Commission>();
            OBK_Products_SeriesComRecord = new HashSet<OBK_Products_SeriesComRecord>();
            OBK_ResearchCenterResult = new HashSet<OBK_ResearchCenterResult>();
            OBK_TaskExecutor = new HashSet<OBK_TaskExecutor>();
            OBK_TaskMaterial = new HashSet<OBK_TaskMaterial>();
            OBK_Tasks = new HashSet<OBK_Tasks>();
            OBK_ZBKCopy = new HashSet<OBK_ZBKCopy>();
            OBK_ZBKCopySignData = new HashSet<OBK_ZBKCopySignData>();
            OBK_ZBKCopyStageExecutors = new HashSet<OBK_ZBKCopyStageExecutors>();
            OBK_ZBKCopyStageSignData = new HashSet<OBK_ZBKCopyStageSignData>();
            PP_DIC_AvgExchangeRate = new HashSet<PP_DIC_AvgExchangeRate>();
            PP_PharmaList = new HashSet<PP_PharmaList>();
            PP_ProtocolComissionMembers = new HashSet<PP_ProtocolComissionMembers>();
            PriceProjectComRecords = new HashSet<PriceProjectComRecord>();
            PriceProjectFieldHistories = new HashSet<PriceProjectFieldHistory>();
            OBK_ContractFactoryComRecord = new HashSet<OBK_ContractFactoryComRecord>();
            OBK_RS_ProductsComRecord = new HashSet<OBK_RS_ProductsComRecord>();
            VisitEmployeeWorkingTimes = new HashSet<VisitEmployeeWorkingTime>();
            Visits = new HashSet<Visit>();
            Visits1 = new HashSet<Visit>();
            EXP_RegistrationExpSteps1 = new HashSet<EXP_RegistrationExpSteps>();
        }

        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [StringLength(4000)]
        public string LastName { get; set; }

        [StringLength(4000)]
        public string FirstName { get; set; }

        [StringLength(4000)]
        public string MiddleName { get; set; }

        [StringLength(4000)]
        public string FullName { get; set; }

        [StringLength(4000)]
        public string ShortName { get; set; }

        public Guid? PositionId { get; set; }

        public int Sex { get; set; }

        [StringLength(510)]
        public string Email { get; set; }

        [StringLength(510)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Login { get; set; }

        [StringLength(4000)]
        public string DisplayName { get; set; }

        [StringLength(4000)]
        public string DeputyId { get; set; }

        [StringLength(4000)]
        public string DeputyValue { get; set; }

        [StringLength(4000)]
        public string AssistantsId { get; set; }

        [StringLength(4000)]
        public string AssistantsValue { get; set; }

        [StringLength(4000)]
        public string Cabinet { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        public int TimeAgreement { get; set; }

        [StringLength(4000)]
        public string Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(4000)]
        public string Birthplace { get; set; }

        [StringLength(4000)]
        public string UlNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UlDate { get; set; }

        [StringLength(4000)]
        public string UlOwner { get; set; }

        [StringLength(4000)]
        public string Iin { get; set; }

        [StringLength(4000)]
        public string PlaceLive { get; set; }

        [StringLength(4000)]
        public string PlaceRegistration { get; set; }

        [StringLength(4000)]
        public string PhoneHome { get; set; }

        [StringLength(4000)]
        public string PhoneMobile { get; set; }

        public int FamilyStatus { get; set; }

        [StringLength(4000)]
        public string Families { get; set; }

        [StringLength(4000)]
        public string Education { get; set; }

        public int ExperienceTotal { get; set; }

        public int ExperienceSpec { get; set; }

        public bool IsDegree { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DegreeDate { get; set; }

        [StringLength(4000)]
        public string DegreeSpec { get; set; }

        public int MilitaryType { get; set; }

        [StringLength(4000)]
        public string MilitaryCategory { get; set; }

        [StringLength(4000)]
        public string MilitarySostav { get; set; }

        [StringLength(4000)]
        public string MilitaryRank { get; set; }

        [StringLength(4000)]
        public string MilitaryVus { get; set; }

        [StringLength(4000)]
        public string MilitaryLocation { get; set; }

        [Column(TypeName = "date")]
        public DateTime? MilitaryLastDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateFile { get; set; }

        [StringLength(4000)]
        public string EducationNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EducationDate { get; set; }

        [StringLength(4000)]
        public string EducationSpec { get; set; }

        [StringLength(4000)]
        public string EducationQual { get; set; }

        public Guid OrganizationId { get; set; }

        [StringLength(4000)]
        public string NationalityId { get; set; }

        [StringLength(4000)]
        public string NationalityValue { get; set; }

        [StringLength(4000)]
        public string DegreeName { get; set; }

        public bool IsAcademic { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AcademicDate { get; set; }

        [StringLength(4000)]
        public string AcademicSpec { get; set; }

        [StringLength(4000)]
        public string AcademicName { get; set; }

        [StringLength(4000)]
        public string Education2 { get; set; }

        [StringLength(4000)]
        public string EducationNumber2 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EducationDate2 { get; set; }

        [StringLength(4000)]
        public string EducationSpec2 { get; set; }

        [StringLength(4000)]
        public string EducationQual2 { get; set; }

        [StringLength(4000)]
        public string Education3 { get; set; }

        [StringLength(4000)]
        public string EducationNumber3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EducationDate3 { get; set; }

        [StringLength(4000)]
        public string EducationSpec3 { get; set; }

        [StringLength(4000)]
        public string EducationQual3 { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TerminationDate { get; set; }

        [StringLength(4000)]
        public string MilitaryRankId { get; set; }

        [StringLength(4000)]
        public string MilitarySostavId { get; set; }

        [StringLength(4000)]
        public string AcademicNameId { get; set; }

        [StringLength(4000)]
        public string DegreeNameId { get; set; }

        [StringLength(4000)]
        public string CauseLayoffsId { get; set; }

        [StringLength(4000)]
        public string CauseLayoffsValue { get; set; }

        [StringLength(4000)]
        public string QualificationCategoryId { get; set; }

        [StringLength(4000)]
        public string QualificationCategoryValue { get; set; }

        public bool IsGuide { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public bool? IsLockedOut { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastLoginDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionUnit> CommissionUnits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractComment> ContractComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContractProcSetting> ContractProcSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Contract> EMP_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_ContractExtHistory> EMP_ContractExtHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_ContractStageExecutors> EMP_ContractStageExecutors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_DirectionToPayments> EMP_DirectionToPayments { get; set; }

        public virtual Unit Organization { get; set; }

        public virtual Unit Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Unit> Units { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Activities> EXP_Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_AgreementProcSettingsTasks> EXP_AgreementProcSettingsTasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_CertificateOfCompletion> EXP_CertificateOfCompletion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DirectionToPays> EXP_DirectionToPays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugCorespondence> EXP_DrugCorespondence { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugCorespondence> EXP_DrugCorespondence1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugCorespondence> EXP_DrugCorespondence2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugCorespondenceRemark> EXP_DrugCorespondenceRemark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclarationComRecord> EXP_DrugDeclarationComRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclarationFieldHistory> EXP_DrugDeclarationFieldHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugDeclarationHistory> EXP_DrugDeclarationHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPrimaryRemark> EXP_DrugPrimaryRemark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageExecutors> EXP_ExpertiseStageExecutors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageRemark> EXP_ExpertiseStageRemark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileLink> FileLinks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_MaterialDirections> EXP_MaterialDirections { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_MaterialDirections> EXP_MaterialDirections1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceProjectsHistory> PriceProjectsHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_RegistrationExpSteps> EXP_RegistrationExpSteps { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Tasks> EXP_Tasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Tasks> EXP_Tasks1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileLinksCategoryComRecord> FileLinksCategoryComRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GridSetting> GridSettings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsApplicationJournal> LimsApplicationJournals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsApplicationJournal> LimsApplicationJournals1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsApplicationJournal> LimsApplicationJournals2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipment> LimsEquipments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentAct> LimsEquipmentActs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentAct> LimsEquipmentActs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentAct> LimsEquipmentActs2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentJournalRecord> LimsEquipmentJournalRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentPlan> LimsEquipmentPlans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LimsEquipmentPlan> LimsEquipmentPlans1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclarationComRecord> OBK_AssessmentDeclarationComRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclarationFieldHistory> OBK_AssessmentDeclarationFieldHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentStageExecutors> OBK_AssessmentStageExecutors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentStageSignData> OBK_AssessmentStageSignData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Contract> OBK_Contract { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractComRecord> OBK_ContractComRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractExtHistory> OBK_ContractExtHistory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractPriceComRecord> OBK_ContractPriceComRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractStageExecutors> OBK_ContractStageExecutors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_DirectionToPayments> OBK_DirectionToPayments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_OP_Commission> OBK_OP_Commission { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Products_SeriesComRecord> OBK_Products_SeriesComRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ResearchCenterResult> OBK_ResearchCenterResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskExecutor> OBK_TaskExecutor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_TaskMaterial> OBK_TaskMaterial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Tasks> OBK_Tasks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopy> OBK_ZBKCopy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopySignData> OBK_ZBKCopySignData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopyStageExecutors> OBK_ZBKCopyStageExecutors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopyStageSignData> OBK_ZBKCopyStageSignData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_DIC_AvgExchangeRate> PP_DIC_AvgExchangeRate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_PharmaList> PP_PharmaList { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PP_ProtocolComissionMembers> PP_ProtocolComissionMembers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceProjectComRecord> PriceProjectComRecords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceProjectFieldHistory> PriceProjectFieldHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractFactoryComRecord> OBK_ContractFactoryComRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_RS_ProductsComRecord> OBK_RS_ProductsComRecord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitEmployeeWorkingTime> VisitEmployeeWorkingTimes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visit> Visits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visit> Visits1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_RegistrationExpSteps> EXP_RegistrationExpSteps1 { get; set; }
    }
}
