namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NcelsEntities : DbContext
    {
        public NcelsEntities()
            : base("name=ncelsEntities")
        {
        }

        public virtual DbSet<AccessDocument> AccessDocuments { get; set; }
        public virtual DbSet<AccessTask> AccessTasks { get; set; }
        public virtual DbSet<ActionLogPlace> ActionLogPlaces { get; set; }
        public virtual DbSet<ActionLog> ActionLogs { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Archive> Archives { get; set; }
        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<Change> Changes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommissionConclusionType> CommissionConclusionTypes { get; set; }
        public virtual DbSet<CommissionDrugDosage> CommissionDrugDosages { get; set; }
        public virtual DbSet<CommissionDrugDosageNeedCommission> CommissionDrugDosageNeedCommissions { get; set; }
        public virtual DbSet<CommissionKind> CommissionKinds { get; set; }
        public virtual DbSet<CommissionQuestion> CommissionQuestions { get; set; }
        public virtual DbSet<CommissionQuestionType> CommissionQuestionTypes { get; set; }
        public virtual DbSet<Commission> Commissions { get; set; }
        public virtual DbSet<CommissionType> CommissionTypes { get; set; }
        public virtual DbSet<CommissionUnit> CommissionUnits { get; set; }
        public virtual DbSet<CommissionUnitType> CommissionUnitTypes { get; set; }
        public virtual DbSet<Composition> Compositions { get; set; }
        public virtual DbSet<ContractComment> ContractComments { get; set; }
        public virtual DbSet<ContractProcSetting> ContractProcSettings { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractSignedData> ContractSignedDatas { get; set; }
        public virtual DbSet<DIC_FileLinkStatus> DIC_FileLinkStatus { get; set; }
        public virtual DbSet<DIC_Storages> DIC_Storages { get; set; }
        public virtual DbSet<Dictionary> Dictionaries { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<EMP_Contract> EMP_Contract { get; set; }
        public virtual DbSet<EMP_ContractExtHistory> EMP_ContractExtHistory { get; set; }
        public virtual DbSet<EMP_ContractHistory> EMP_ContractHistory { get; set; }
        public virtual DbSet<EMP_ContractSignData> EMP_ContractSignData { get; set; }
        public virtual DbSet<EMP_ContractStage> EMP_ContractStage { get; set; }
        public virtual DbSet<EMP_ContractStageExecutors> EMP_ContractStageExecutors { get; set; }
        public virtual DbSet<EMP_CostWorks> EMP_CostWorks { get; set; }
        public virtual DbSet<EMP_DirectionToPayments> EMP_DirectionToPayments { get; set; }
        public virtual DbSet<EMP_EAESStatement> EMP_EAESStatement { get; set; }
        public virtual DbSet<EMP_EAESStatementRegCountry> EMP_EAESStatementRegCountry { get; set; }
        public virtual DbSet<EMP_Ref_Bank> EMP_Ref_Bank { get; set; }
        public virtual DbSet<EMP_Ref_ChangeType> EMP_Ref_ChangeType { get; set; }
        public virtual DbSet<EMP_Ref_ContractDocumentType> EMP_Ref_ContractDocumentType { get; set; }
        public virtual DbSet<EMP_Ref_ContractScope> EMP_Ref_ContractScope { get; set; }
        public virtual DbSet<EMP_Ref_ContractType> EMP_Ref_ContractType { get; set; }
        public virtual DbSet<EMP_Ref_DegreeRisk> EMP_Ref_DegreeRisk { get; set; }
        public virtual DbSet<EMP_Ref_HolderType> EMP_Ref_HolderType { get; set; }
        public virtual DbSet<EMP_Ref_PriceList> EMP_Ref_PriceList { get; set; }
        public virtual DbSet<EMP_Ref_PriceType> EMP_Ref_PriceType { get; set; }
        public virtual DbSet<EMP_Ref_ServiceType> EMP_Ref_ServiceType { get; set; }
        public virtual DbSet<EMP_Ref_Stage> EMP_Ref_Stage { get; set; }
        public virtual DbSet<EMP_Ref_StageStatus> EMP_Ref_StageStatus { get; set; }
        public virtual DbSet<EMP_Ref_Status> EMP_Ref_Status { get; set; }
        public virtual DbSet<EMP_Statement> EMP_Statement { get; set; }
        public virtual DbSet<EMP_StatementChange> EMP_StatementChange { get; set; }
        public virtual DbSet<EMP_StatementCountryRegistration> EMP_StatementCountryRegistration { get; set; }
        public virtual DbSet<EMP_StatementMedicalDeviceComplectation> EMP_StatementMedicalDeviceComplectation { get; set; }
        public virtual DbSet<EMP_StatementMedicalDevicePackage> EMP_StatementMedicalDevicePackage { get; set; }
        public virtual DbSet<EMP_StatementSamples> EMP_StatementSamples { get; set; }
        public virtual DbSet<EMP_StatementStorageLife> EMP_StatementStorageLife { get; set; }
        public virtual DbSet<EmployeePermissionRole> EmployeePermissionRoles { get; set; }
        public virtual DbSet<EmployeePermission> EmployeePermissions { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EXP_Activities> EXP_Activities { get; set; }
        public virtual DbSet<EXP_AgreementProcSettings> EXP_AgreementProcSettings { get; set; }
        public virtual DbSet<EXP_AgreementProcSettingsActivities> EXP_AgreementProcSettingsActivities { get; set; }
        public virtual DbSet<EXP_AgreementProcSettingsTasks> EXP_AgreementProcSettingsTasks { get; set; }
        public virtual DbSet<EXP_CertificateOfCompletion> EXP_CertificateOfCompletion { get; set; }
        public virtual DbSet<EXP_DIC_AccelerationType> EXP_DIC_AccelerationType { get; set; }
        public virtual DbSet<EXP_DIC_AnalyseIndicator> EXP_DIC_AnalyseIndicator { get; set; }
        public virtual DbSet<EXP_DIC_ChangeType> EXP_DIC_ChangeType { get; set; }
        public virtual DbSet<EXP_DIC_CorespondenceKind> EXP_DIC_CorespondenceKind { get; set; }
        public virtual DbSet<EXP_DIC_CorespondenceSubject> EXP_DIC_CorespondenceSubject { get; set; }
        public virtual DbSet<EXP_DIC_CorespondenceType> EXP_DIC_CorespondenceType { get; set; }
        public virtual DbSet<EXP_DIC_DrugType> EXP_DIC_DrugType { get; set; }
        public virtual DbSet<EXP_DIC_DrugTypeKind> EXP_DIC_DrugTypeKind { get; set; }
        public virtual DbSet<EXP_DIC_ManufactureType> EXP_DIC_ManufactureType { get; set; }
        public virtual DbSet<EXP_DIC_NMIRK> EXP_DIC_NMIRK { get; set; }
        public virtual DbSet<EXP_DIC_NormDocFarm> EXP_DIC_NormDocFarm { get; set; }
        public virtual DbSet<EXP_DIC_Origin> EXP_DIC_Origin { get; set; }
        public virtual DbSet<EXP_DIC_PeriodMeasure> EXP_DIC_PeriodMeasure { get; set; }
        public virtual DbSet<EXP_DIC_PlantKind> EXP_DIC_PlantKind { get; set; }
        public virtual DbSet<EXP_DIC_PrimaryFinalyDocResult> EXP_DIC_PrimaryFinalyDocResult { get; set; }
        public virtual DbSet<EXP_DIC_PrimaryMark> EXP_DIC_PrimaryMark { get; set; }
        public virtual DbSet<EXP_DIC_PrimaryOTD> EXP_DIC_PrimaryOTD { get; set; }
        public virtual DbSet<EXP_DIC_RemarkType> EXP_DIC_RemarkType { get; set; }
        public virtual DbSet<EXP_DIC_SaleType> EXP_DIC_SaleType { get; set; }
        public virtual DbSet<EXP_DIC_Stage> EXP_DIC_Stage { get; set; }
        public virtual DbSet<EXP_DIC_StageResult> EXP_DIC_StageResult { get; set; }
        public virtual DbSet<EXP_DIC_StageStatus> EXP_DIC_StageStatus { get; set; }
        public virtual DbSet<EXP_DIC_Status> EXP_DIC_Status { get; set; }
        public virtual DbSet<EXP_DIC_Type> EXP_DIC_Type { get; set; }
        public virtual DbSet<EXP_DIC_TypeFileND> EXP_DIC_TypeFileND { get; set; }
        public virtual DbSet<EXP_DIC_TypeND> EXP_DIC_TypeND { get; set; }
        public virtual DbSet<EXP_DIC_WrappingType> EXP_DIC_WrappingType { get; set; }
        public virtual DbSet<EXP_DirectionToPays> EXP_DirectionToPays { get; set; }
        public virtual DbSet<EXP_DirectionToPays_PriceList> EXP_DirectionToPays_PriceList { get; set; }
        public virtual DbSet<EXP_DrugAnaliseIndicator> EXP_DrugAnaliseIndicator { get; set; }
        public virtual DbSet<EXP_DrugAppDosageRemark> EXP_DrugAppDosageRemark { get; set; }
        public virtual DbSet<EXP_DrugAppDosageResult> EXP_DrugAppDosageResult { get; set; }
        public virtual DbSet<EXP_DrugChangeType> EXP_DrugChangeType { get; set; }
        public virtual DbSet<EXP_DrugCorespondence> EXP_DrugCorespondence { get; set; }
        public virtual DbSet<EXP_DrugCorespondenceRemark> EXP_DrugCorespondenceRemark { get; set; }
        public virtual DbSet<EXP_DrugDeclaration> EXP_DrugDeclaration { get; set; }
        public virtual DbSet<EXP_DrugDeclarationCom> EXP_DrugDeclarationCom { get; set; }
        public virtual DbSet<EXP_DrugDeclarationComRecord> EXP_DrugDeclarationComRecord { get; set; }
        public virtual DbSet<EXP_DrugDeclarationFieldHistory> EXP_DrugDeclarationFieldHistory { get; set; }
        public virtual DbSet<EXP_DrugDeclarationHistory> EXP_DrugDeclarationHistory { get; set; }
        public virtual DbSet<EXP_DrugDosage> EXP_DrugDosage { get; set; }
        public virtual DbSet<EXP_DrugExportTrade> EXP_DrugExportTrade { get; set; }
        public virtual DbSet<EXP_DrugOrganizations> EXP_DrugOrganizations { get; set; }
        public virtual DbSet<EXP_DrugOtherCountry> EXP_DrugOtherCountry { get; set; }
        public virtual DbSet<EXP_DrugPatent> EXP_DrugPatent { get; set; }
        public virtual DbSet<EXP_DrugPrice> EXP_DrugPrice { get; set; }
        public virtual DbSet<EXP_DrugPrimaryFinalDocument> EXP_DrugPrimaryFinalDocument { get; set; }
        public virtual DbSet<EXP_DrugPrimaryKind> EXP_DrugPrimaryKind { get; set; }
        public virtual DbSet<EXP_DrugPrimaryNTD> EXP_DrugPrimaryNTD { get; set; }
        public virtual DbSet<EXP_DrugPrimaryRemark> EXP_DrugPrimaryRemark { get; set; }
        public virtual DbSet<EXP_DrugProtectionDoc> EXP_DrugProtectionDoc { get; set; }
        public virtual DbSet<EXP_DrugSubstance> EXP_DrugSubstance { get; set; }
        public virtual DbSet<EXP_DrugSubstanceManufacture> EXP_DrugSubstanceManufacture { get; set; }
        public virtual DbSet<EXP_DrugType> EXP_DrugType { get; set; }
        public virtual DbSet<EXP_DrugUseMethod> EXP_DrugUseMethod { get; set; }
        public virtual DbSet<EXP_DrugWrapping> EXP_DrugWrapping { get; set; }
        public virtual DbSet<EXP_ExpertisePharmaceuticalFinalDoc> EXP_ExpertisePharmaceuticalFinalDoc { get; set; }
        public virtual DbSet<EXP_ExpertisePharmacologicalFinalDoc> EXP_ExpertisePharmacologicalFinalDoc { get; set; }
        public virtual DbSet<EXP_ExpertisePrimaryFinalDoc> EXP_ExpertisePrimaryFinalDoc { get; set; }
        public virtual DbSet<EXP_ExpertiseSafetyreportFinalDoc> EXP_ExpertiseSafetyreportFinalDoc { get; set; }
        public virtual DbSet<EXP_ExpertiseStage> EXP_ExpertiseStage { get; set; }
        public virtual DbSet<EXP_ExpertiseStageDosage> EXP_ExpertiseStageDosage { get; set; }
        public virtual DbSet<EXP_ExpertiseStageDosageResult> EXP_ExpertiseStageDosageResult { get; set; }
        public virtual DbSet<EXP_ExpertiseStageExecutors> EXP_ExpertiseStageExecutors { get; set; }
        public virtual DbSet<EXP_ExpertiseStageRemark> EXP_ExpertiseStageRemark { get; set; }
        public virtual DbSet<EXP_MaterialDirections> EXP_MaterialDirections { get; set; }
        public virtual DbSet<EXP_Materials> EXP_Materials { get; set; }
        public virtual DbSet<EXP_PriceList> EXP_PriceList { get; set; }
        public virtual DbSet<EXP_RegistrationExpSteps> EXP_RegistrationExpSteps { get; set; }
        public virtual DbSet<EXP_RegistrationTypes> EXP_RegistrationTypes { get; set; }
        public virtual DbSet<EXP_Tasks> EXP_Tasks { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<ExtensionExecution> ExtensionExecutions { get; set; }
        public virtual DbSet<FileLink> FileLinks { get; set; }
        public virtual DbSet<FileLinksCategoryCom> FileLinksCategoryComs { get; set; }
        public virtual DbSet<FileLinksCategoryComRecord> FileLinksCategoryComRecords { get; set; }
        public virtual DbSet<GridSetting> GridSettings { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<I1c_act_Application> I1c_act_Application { get; set; }
        public virtual DbSet<I1c_act_ObkApplication> I1c_act_ObkApplication { get; set; }
        public virtual DbSet<I1c_exp_Contracts> I1c_exp_Contracts { get; set; }
        public virtual DbSet<I1c_lims_ApplicationEAdmissions> I1c_lims_ApplicationEAdmissions { get; set; }
        public virtual DbSet<I1c_lims_ApplicationEExploitation> I1c_lims_ApplicationEExploitation { get; set; }
        public virtual DbSet<I1c_lims_ApplicationElements> I1c_lims_ApplicationElements { get; set; }
        public virtual DbSet<I1c_lims_Applications> I1c_lims_Applications { get; set; }
        public virtual DbSet<I1c_lims_ContractProducts> I1c_lims_ContractProducts { get; set; }
        public virtual DbSet<I1c_lims_Contracts> I1c_lims_Contracts { get; set; }
        public virtual DbSet<I1c_primary_ApplicationElements> I1c_primary_ApplicationElements { get; set; }
        public virtual DbSet<I1c_primary_ApplicationPartPaymentState> I1c_primary_ApplicationPartPaymentState { get; set; }
        public virtual DbSet<I1c_primary_ApplicationPaymentState> I1c_primary_ApplicationPaymentState { get; set; }
        public virtual DbSet<I1c_primary_ApplicationRefuseState> I1c_primary_ApplicationRefuseState { get; set; }
        public virtual DbSet<I1c_primary_Applications> I1c_primary_Applications { get; set; }
        public virtual DbSet<I1c_primary_DrugPackage> I1c_primary_DrugPackage { get; set; }
        public virtual DbSet<I1c_primary_ObkApplicationNotFullPaymentState> I1c_primary_ObkApplicationNotFullPaymentState { get; set; }
        public virtual DbSet<I1c_primary_ObkApplicationPaymentState> I1c_primary_ObkApplicationPaymentState { get; set; }
        public virtual DbSet<I1c_primary_ObkApplications> I1c_primary_ObkApplications { get; set; }
        public virtual DbSet<I1c_primary_ObkPriceListElements> I1c_primary_ObkPriceListElements { get; set; }
        public virtual DbSet<I1c_primary_PriceListElements> I1c_primary_PriceListElements { get; set; }
        public virtual DbSet<I1c_trl_Application> I1c_trl_Application { get; set; }
        public virtual DbSet<I1c_trl_ApplicationPaymentState> I1c_trl_ApplicationPaymentState { get; set; }
        public virtual DbSet<I1c_trl_ApplicationRefuseState> I1c_trl_ApplicationRefuseState { get; set; }
        public virtual DbSet<Installation> Installations { get; set; }
        public virtual DbSet<LimsApplicationJournal> LimsApplicationJournals { get; set; }
        public virtual DbSet<LimsEquipment> LimsEquipments { get; set; }
        public virtual DbSet<LimsEquipmentAct> LimsEquipmentActs { get; set; }
        public virtual DbSet<LimsEquipmentActSparePart> LimsEquipmentActSpareParts { get; set; }
        public virtual DbSet<LimsEquipmentJournal> LimsEquipmentJournals { get; set; }
        public virtual DbSet<LimsEquipmentJournalRecord> LimsEquipmentJournalRecords { get; set; }
        public virtual DbSet<LimsEquipmentPlan> LimsEquipmentPlans { get; set; }
        public virtual DbSet<LimsPlanEquipmentLink> LimsPlanEquipmentLinks { get; set; }
        public virtual DbSet<LimsTmcTemp> LimsTmcTemps { get; set; }
        public virtual DbSet<LinkDicOrphanDrugsIcdDeseas> LinkDicOrphanDrugsIcdDeseases { get; set; }
        public virtual DbSet<LinkDocument> LinkDocuments { get; set; }
        public virtual DbSet<LinkUnit> LinkUnits { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<OBK_ActReception> OBK_ActReception { get; set; }
        public virtual DbSet<obk_appendix> obk_appendix { get; set; }
        public virtual DbSet<obk_appendix_series> obk_appendix_series { get; set; }
        public virtual DbSet<OBK_Applicant> OBK_Applicant { get; set; }
        public virtual DbSet<OBK_AssessmentDeclaration> OBK_AssessmentDeclaration { get; set; }
        public virtual DbSet<OBK_AssessmentDeclaration__OBK_ExpertCouncil> OBK_AssessmentDeclaration__OBK_ExpertCouncil { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationCom> OBK_AssessmentDeclarationCom { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationComRecord> OBK_AssessmentDeclarationComRecord { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationFieldHistory> OBK_AssessmentDeclarationFieldHistory { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationHistory> OBK_AssessmentDeclarationHistory { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationProgram> OBK_AssessmentDeclarationProgram { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationProgramExecutors> OBK_AssessmentDeclarationProgramExecutors { get; set; }
        public virtual DbSet<OBK_AssessmentStage> OBK_AssessmentStage { get; set; }
        public virtual DbSet<OBK_AssessmentStageExecutors> OBK_AssessmentStageExecutors { get; set; }
        public virtual DbSet<OBK_AssessmentStageOP> OBK_AssessmentStageOP { get; set; }
        public virtual DbSet<OBK_AssessmentStageSignData> OBK_AssessmentStageSignData { get; set; }
        public virtual DbSet<OBK_BlankNumber> OBK_BlankNumber { get; set; }
        public virtual DbSet<OBK_BlankType> OBK_BlankType { get; set; }
        public virtual DbSet<OBK_CertificateOfCompletion> OBK_CertificateOfCompletion { get; set; }
        public virtual DbSet<OBK_CertificateReference> OBK_CertificateReference { get; set; }
        public virtual DbSet<OBK_CertificateReferenceFieldHistory> OBK_CertificateReferenceFieldHistory { get; set; }
        public virtual DbSet<OBK_CertificateValidityType> OBK_CertificateValidityType { get; set; }
        public virtual DbSet<obk_certification_copies> obk_certification_copies { get; set; }
        public virtual DbSet<obk_certifications> obk_certifications { get; set; }
        public virtual DbSet<OBK_Contract> OBK_Contract { get; set; }
        public virtual DbSet<OBK_ContractCom> OBK_ContractCom { get; set; }
        public virtual DbSet<OBK_ContractComRecord> OBK_ContractComRecord { get; set; }
        public virtual DbSet<OBK_ContractExtHistory> OBK_ContractExtHistory { get; set; }
        public virtual DbSet<OBK_ContractFactory> OBK_ContractFactory { get; set; }
        public virtual DbSet<OBK_ContractFactoryCom> OBK_ContractFactoryCom { get; set; }
        public virtual DbSet<OBK_ContractFactoryComRecord> OBK_ContractFactoryComRecord { get; set; }
        public virtual DbSet<OBK_ContractHistory> OBK_ContractHistory { get; set; }
        public virtual DbSet<OBK_ContractPrice> OBK_ContractPrice { get; set; }
        public virtual DbSet<OBK_ContractPriceCom> OBK_ContractPriceCom { get; set; }
        public virtual DbSet<OBK_ContractPriceComRecord> OBK_ContractPriceComRecord { get; set; }
        public virtual DbSet<OBK_ContractSignedDatas> OBK_ContractSignedDatas { get; set; }
        public virtual DbSet<OBK_ContractStage> OBK_ContractStage { get; set; }
        public virtual DbSet<OBK_ContractStageExecutors> OBK_ContractStageExecutors { get; set; }
        public virtual DbSet<obk_currencies> obk_currencies { get; set; }
        public virtual DbSet<OBK_Declarant> OBK_Declarant { get; set; }
        public virtual DbSet<OBK_DeclarantContact> OBK_DeclarantContact { get; set; }
        public virtual DbSet<OBK_Dictionaries> OBK_Dictionaries { get; set; }
        public virtual DbSet<OBK_DirectionSignData> OBK_DirectionSignData { get; set; }
        public virtual DbSet<OBK_DirectionToPayments> OBK_DirectionToPayments { get; set; }
        public virtual DbSet<obk_exchangerate> obk_exchangerate { get; set; }
        public virtual DbSet<OBK_ExpertCouncil> OBK_ExpertCouncil { get; set; }
        public virtual DbSet<OBK_LetterAttachments> OBK_LetterAttachments { get; set; }
        public virtual DbSet<OBK_LetterFromEdo> OBK_LetterFromEdo { get; set; }
        public virtual DbSet<OBK_LetterPortalEdo> OBK_LetterPortalEdo { get; set; }
        public virtual DbSet<OBK_LetterRegistration> OBK_LetterRegistration { get; set; }
        public virtual DbSet<OBK_MtPart> OBK_MtPart { get; set; }
        public virtual DbSet<OBK_OP_Commission> OBK_OP_Commission { get; set; }
        public virtual DbSet<OBK_OP_CommissionRoles> OBK_OP_CommissionRoles { get; set; }
        public virtual DbSet<OBK_Procunts_Series> OBK_Procunts_Series { get; set; }
        public virtual DbSet<obk_product_cost> obk_product_cost { get; set; }
        public virtual DbSet<obk_products> obk_products { get; set; }
        public virtual DbSet<OBK_Products_SeriesCom> OBK_Products_SeriesCom { get; set; }
        public virtual DbSet<OBK_Products_SeriesComRecord> OBK_Products_SeriesComRecord { get; set; }
        public virtual DbSet<OBK_Ref_CertificateType> OBK_Ref_CertificateType { get; set; }
        public virtual DbSet<OBK_Ref_ContractDocumentType> OBK_Ref_ContractDocumentType { get; set; }
        public virtual DbSet<OBK_Ref_ContractExtHistoryStatus> OBK_Ref_ContractExtHistoryStatus { get; set; }
        public virtual DbSet<OBK_Ref_ContractHistoryStatus> OBK_Ref_ContractHistoryStatus { get; set; }
        public virtual DbSet<OBK_Ref_DegreeRisk> OBK_Ref_DegreeRisk { get; set; }
        public virtual DbSet<OBK_Ref_LaboratoryMark> OBK_Ref_LaboratoryMark { get; set; }
        public virtual DbSet<OBK_Ref_LaboratoryRegulation> OBK_Ref_LaboratoryRegulation { get; set; }
        public virtual DbSet<OBK_Ref_LaboratoryType> OBK_Ref_LaboratoryType { get; set; }
        public virtual DbSet<OBK_Ref_MaterialCondition> OBK_Ref_MaterialCondition { get; set; }
        public virtual DbSet<OBK_Ref_Nomenclature> OBK_Ref_Nomenclature { get; set; }
        public virtual DbSet<OBK_Ref_PaymentStatus> OBK_Ref_PaymentStatus { get; set; }
        public virtual DbSet<OBK_Ref_PriceList> OBK_Ref_PriceList { get; set; }
        public virtual DbSet<OBK_Ref_Reason> OBK_Ref_Reason { get; set; }
        public virtual DbSet<OBK_Ref_ServiceType> OBK_Ref_ServiceType { get; set; }
        public virtual DbSet<OBK_Ref_Stage> OBK_Ref_Stage { get; set; }
        public virtual DbSet<OBK_Ref_StageStatus> OBK_Ref_StageStatus { get; set; }
        public virtual DbSet<OBK_Ref_Status> OBK_Ref_Status { get; set; }
        public virtual DbSet<OBK_Ref_Type> OBK_Ref_Type { get; set; }
        public virtual DbSet<OBK_Ref_ValueAddedTax> OBK_Ref_ValueAddedTax { get; set; }
        public virtual DbSet<OBK_ResearchCenterResult> OBK_ResearchCenterResult { get; set; }
        public virtual DbSet<OBK_RS_Products> OBK_RS_Products { get; set; }
        public virtual DbSet<OBK_RS_ProductsCom> OBK_RS_ProductsCom { get; set; }
        public virtual DbSet<OBK_RS_ProductsComRecord> OBK_RS_ProductsComRecord { get; set; }
        public virtual DbSet<OBK_StageExpDocument> OBK_StageExpDocument { get; set; }
        public virtual DbSet<OBK_StageExpDocumentResult> OBK_StageExpDocumentResult { get; set; }
        public virtual DbSet<OBK_TaskExecutor> OBK_TaskExecutor { get; set; }
        public virtual DbSet<OBK_TaskMaterial> OBK_TaskMaterial { get; set; }
        public virtual DbSet<OBK_Tasks> OBK_Tasks { get; set; }
        public virtual DbSet<OBK_TaskStatus> OBK_TaskStatus { get; set; }
        public virtual DbSet<OBK_UniqueNumber> OBK_UniqueNumber { get; set; }
        public virtual DbSet<OBK_ZBKCopy> OBK_ZBKCopy { get; set; }
        public virtual DbSet<OBK_ZBKCopyBlank> OBK_ZBKCopyBlank { get; set; }
        public virtual DbSet<OBK_ZBKCopySignData> OBK_ZBKCopySignData { get; set; }
        public virtual DbSet<OBK_ZBKCopyStage> OBK_ZBKCopyStage { get; set; }
        public virtual DbSet<OBK_ZBKCopyStageExecutors> OBK_ZBKCopyStageExecutors { get; set; }
        public virtual DbSet<OBK_ZBKCopyStageSignData> OBK_ZBKCopyStageSignData { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PermissionKey> PermissionKeys { get; set; }
        public virtual DbSet<PermissionRole> PermissionRoles { get; set; }
        public virtual DbSet<PermissionRoleKey> PermissionRoleKeys { get; set; }
        public virtual DbSet<PermissionValue> PermissionValues { get; set; }
        public virtual DbSet<PP_AttachRemarks> PP_AttachRemarks { get; set; }
        public virtual DbSet<PP_DIC_AvgExchangeRate> PP_DIC_AvgExchangeRate { get; set; }
        public virtual DbSet<PP_PharmaList> PP_PharmaList { get; set; }
        public virtual DbSet<PP_ProtocolComissionMembers> PP_ProtocolComissionMembers { get; set; }
        public virtual DbSet<PP_ProtocolProductPrices> PP_ProtocolProductPrices { get; set; }
        public virtual DbSet<PP_Protocols> PP_Protocols { get; set; }
        public virtual DbSet<PriceListExpertise> PriceListExpertises { get; set; }
        public virtual DbSet<PriceProject_Ext> PriceProject_Ext { get; set; }
        public virtual DbSet<PriceProjectCom> PriceProjectComs { get; set; }
        public virtual DbSet<PriceProjectComRecord> PriceProjectComRecords { get; set; }
        public virtual DbSet<PriceProjectFieldHistory> PriceProjectFieldHistories { get; set; }
        public virtual DbSet<PriceProject> PriceProjects { get; set; }
        public virtual DbSet<PriceProjectsHistory> PriceProjectsHistories { get; set; }
        public virtual DbSet<PriceRejectProject> PriceRejectProjects { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<PrismEnum> PrismEnums { get; set; }
        public virtual DbSet<PrismReport> PrismReports { get; set; }
        public virtual DbSet<PrtPrj> PrtPrjs { get; set; }
        public virtual DbSet<Ref_Knfs> Ref_Knfs { get; set; }
        public virtual DbSet<Ref_MarketPrices> Ref_MarketPrices { get; set; }
        public virtual DbSet<Ref_PurchasePrices> Ref_PurchasePrices { get; set; }
        public virtual DbSet<Ref_Sources> Ref_Sources { get; set; }
        public virtual DbSet<Registery> Registeries { get; set; }
        public virtual DbSet<RegisterProject> RegisterProjects { get; set; }
        public virtual DbSet<RemarkType> RemarkTypes { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<RequestList> RequestLists { get; set; }
        public virtual DbSet<RequestOrder> RequestOrders { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<SignDocument> SignDocuments { get; set; }
        public virtual DbSet<sr_atc_codes> sr_atc_codes { get; set; }
        public virtual DbSet<sr_boxes> sr_boxes { get; set; }
        public virtual DbSet<sr_categories> sr_categories { get; set; }
        public virtual DbSet<sr_countries> sr_countries { get; set; }
        public virtual DbSet<sr_degree_risk_details> sr_degree_risk_details { get; set; }
        public virtual DbSet<sr_degree_risks> sr_degree_risks { get; set; }
        public virtual DbSet<sr_dosage_forms> sr_dosage_forms { get; set; }
        public virtual DbSet<sr_drug_forms> sr_drug_forms { get; set; }
        public virtual DbSet<sr_drug_types> sr_drug_types { get; set; }
        public virtual DbSet<sr_form_types> sr_form_types { get; set; }
        public virtual DbSet<sr_instruction_types> sr_instruction_types { get; set; }
        public virtual DbSet<sr_international_names> sr_international_names { get; set; }
        public virtual DbSet<sr_life_types> sr_life_types { get; set; }
        public virtual DbSet<sr_measures> sr_measures { get; set; }
        public virtual DbSet<sr_mt_categories> sr_mt_categories { get; set; }
        public virtual DbSet<sr_nd_names> sr_nd_names { get; set; }
        public virtual DbSet<sr_nd_types> sr_nd_types { get; set; }
        public virtual DbSet<sr_pharmacological_actions> sr_pharmacological_actions { get; set; }
        public virtual DbSet<sr_producer_types> sr_producer_types { get; set; }
        public virtual DbSet<sr_producers> sr_producers { get; set; }
        public virtual DbSet<sr_reg_actions> sr_reg_actions { get; set; }
        public virtual DbSet<sr_reg_types> sr_reg_types { get; set; }
        public virtual DbSet<sr_register> sr_register { get; set; }
        public virtual DbSet<sr_register_boxes> sr_register_boxes { get; set; }
        public virtual DbSet<sr_register_boxes_rk_ls> sr_register_boxes_rk_ls { get; set; }
        public virtual DbSet<sr_register_drugs> sr_register_drugs { get; set; }
        public virtual DbSet<sr_register_instructions> sr_register_instructions { get; set; }
        public virtual DbSet<sr_register_instructions_files> sr_register_instructions_files { get; set; }
        public virtual DbSet<sr_register_mt> sr_register_mt { get; set; }
        public virtual DbSet<sr_register_mt_kz> sr_register_mt_kz { get; set; }
        public virtual DbSet<sr_register_mt_parts> sr_register_mt_parts { get; set; }
        public virtual DbSet<sr_register_names> sr_register_names { get; set; }
        public virtual DbSet<sr_register_ordered> sr_register_ordered { get; set; }
        public virtual DbSet<sr_register_pharmacological_actions> sr_register_pharmacological_actions { get; set; }
        public virtual DbSet<sr_register_producers> sr_register_producers { get; set; }
        public virtual DbSet<sr_register_substances> sr_register_substances { get; set; }
        public virtual DbSet<sr_register_use_methods> sr_register_use_methods { get; set; }
        public virtual DbSet<sr_substance_types> sr_substance_types { get; set; }
        public virtual DbSet<sr_substances> sr_substances { get; set; }
        public virtual DbSet<sr_use_methods> sr_use_methods { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Template> Templates { get; set; }
        public virtual DbSet<TempTmc> TempTmcs { get; set; }
        public virtual DbSet<TmcApplicationComment> TmcApplicationComments { get; set; }
        public virtual DbSet<TmcIn> TmcIns { get; set; }
        public virtual DbSet<TmcOff> TmcOffs { get; set; }
        public virtual DbSet<TmcOutCount> TmcOutCounts { get; set; }
        public virtual DbSet<TmcOut> TmcOuts { get; set; }
        public virtual DbSet<TmcReportData> TmcReportDatas { get; set; }
        public virtual DbSet<TmcReport> TmcReports { get; set; }
        public virtual DbSet<TmcReportTask> TmcReportTasks { get; set; }
        public virtual DbSet<Tmc> Tmcs { get; set; }
        public virtual DbSet<UniqueIdentificator> UniqueIdentificators { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<UnitsAddress> UnitsAddresses { get; set; }
        public virtual DbSet<UnitsBank> UnitsBanks { get; set; }
        public virtual DbSet<UnitSigner> UnitSigners { get; set; }
        public virtual DbSet<UpdateScript> UpdateScripts { get; set; }
        public virtual DbSet<VisitEmployeeType> VisitEmployeeTypes { get; set; }
        public virtual DbSet<VisitEmployeeWorkingTime> VisitEmployeeWorkingTimes { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<VisitStatus> VisitStatuses { get; set; }
        public virtual DbSet<VisitType> VisitTypes { get; set; }
        public virtual DbSet<sync_1c_I1c_act_Application_tracking> sync_1c_I1c_act_Application_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_exp_Contracts_tracking> sync_1c_I1c_exp_Contracts_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_lims_ApplicationEAdmissions_tracking> sync_1c_I1c_lims_ApplicationEAdmissions_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_lims_ApplicationEExploitation_tracking> sync_1c_I1c_lims_ApplicationEExploitation_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_lims_ApplicationElements_tracking> sync_1c_I1c_lims_ApplicationElements_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_lims_Applications_tracking> sync_1c_I1c_lims_Applications_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_lims_ContractProducts_tracking> sync_1c_I1c_lims_ContractProducts_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_lims_Contracts_tracking> sync_1c_I1c_lims_Contracts_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_primary_ApplicationElements_tracking> sync_1c_I1c_primary_ApplicationElements_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_primary_ApplicationPartPaymentState_tracking> sync_1c_I1c_primary_ApplicationPartPaymentState_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_primary_ApplicationPaymentState_tracking> sync_1c_I1c_primary_ApplicationPaymentState_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_primary_ApplicationRefuseState_tracking> sync_1c_I1c_primary_ApplicationRefuseState_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_primary_Applications_tracking> sync_1c_I1c_primary_Applications_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_primary_DrugPackage_tracking> sync_1c_I1c_primary_DrugPackage_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_primary_PriceListElements_tracking> sync_1c_I1c_primary_PriceListElements_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_trl_Application_tracking> sync_1c_I1c_trl_Application_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_trl_ApplicationPaymentState_tracking> sync_1c_I1c_trl_ApplicationPaymentState_tracking { get; set; }
        public virtual DbSet<sync_1c_I1c_trl_ApplicationRefuseState_tracking> sync_1c_I1c_trl_ApplicationRefuseState_tracking { get; set; }
        public virtual DbSet<sync_1c_schema_info> sync_1c_schema_info { get; set; }
        public virtual DbSet<sync_1c_scope_config> sync_1c_scope_config { get; set; }
        public virtual DbSet<sync_1c_scope_info> sync_1c_scope_info { get; set; }
        public virtual DbSet<DIC_IcdDeseases> DIC_IcdDeseases { get; set; }
        public virtual DbSet<DIC_OrphanDrugs> DIC_OrphanDrugs { get; set; }
        public virtual DbSet<EXP_PriceListDrugTypeMapping> EXP_PriceListDrugTypeMapping { get; set; }
        public virtual DbSet<LimsOrganization1CLink> LimsOrganization1CLink { get; set; }
        public virtual DbSet<LinkDictionary> LinkDictionaries { get; set; }
        public virtual DbSet<Ref_Limits> Ref_Limits { get; set; }
        public virtual DbSet<ActionLogsView> ActionLogsViews { get; set; }
        public virtual DbSet<AdministrativeDocument> AdministrativeDocuments { get; set; }
        public virtual DbSet<ArchivView> ArchivViews { get; set; }
        public virtual DbSet<AvgExchangeRatesView> AvgExchangeRatesViews { get; set; }
        public virtual DbSet<CategoriesView> CategoriesViews { get; set; }
        public virtual DbSet<CitizenDocument> CitizenDocuments { get; set; }
        public virtual DbSet<CommissionDrugDosageCount2View> CommissionDrugDosageCount2View { get; set; }
        public virtual DbSet<CommissionDrugDosageCount3View> CommissionDrugDosageCount3View { get; set; }
        public virtual DbSet<CommissionDrugDosageCount3WithoutRepeatView> CommissionDrugDosageCount3WithoutRepeatView { get; set; }
        public virtual DbSet<CommissionDrugDosageCount4View> CommissionDrugDosageCount4View { get; set; }
        public virtual DbSet<CommissionDrugDosageCount4WithoutRepeatView> CommissionDrugDosageCount4WithoutRepeatView { get; set; }
        public virtual DbSet<CommissionDrugDosageCountView> CommissionDrugDosageCountViews { get; set; }
        public virtual DbSet<CommissionsView> CommissionsViews { get; set; }
        public virtual DbSet<CompositionsView> CompositionsViews { get; set; }
        public virtual DbSet<ContractAdditionJournal> ContractAdditionJournals { get; set; }
        public virtual DbSet<ContractJournal> ContractJournals { get; set; }
        public virtual DbSet<ContractsView> ContractsViews { get; set; }
        public virtual DbSet<CorrespondenceDocument> CorrespondenceDocuments { get; set; }
        public virtual DbSet<EMP_ContractHistoryView> EMP_ContractHistoryView { get; set; }
        public virtual DbSet<EXP_ActivityTaskActualView> EXP_ActivityTaskActualView { get; set; }
        public virtual DbSet<EXP_CertificateOfCompletionView> EXP_CertificateOfCompletionView { get; set; }
        public virtual DbSet<EXP_DirectionToPaysView> EXP_DirectionToPaysView { get; set; }
        public virtual DbSet<Exp_DrugDeclarationChangeView> Exp_DrugDeclarationChangeView { get; set; }
        public virtual DbSet<EXP_DrugDeclarationDirectionToPayView> EXP_DrugDeclarationDirectionToPayView { get; set; }
        public virtual DbSet<EXP_DrugDeclarationPaysInfoView> EXP_DrugDeclarationPaysInfoView { get; set; }
        public virtual DbSet<EXP_DrugDeclarationRegisterView> EXP_DrugDeclarationRegisterView { get; set; }
        public virtual DbSet<EXP_DrugDeclarationView> EXP_DrugDeclarationView { get; set; }
        public virtual DbSet<Exp_DrugDosageStageForAddView> Exp_DrugDosageStageForAddView { get; set; }
        public virtual DbSet<Exp_DrugDosageStagePrevCommissionConcatinateView> Exp_DrugDosageStagePrevCommissionConcatinateView { get; set; }
        public virtual DbSet<Exp_DrugDosageStagePrevCommissionView> Exp_DrugDosageStagePrevCommissionView { get; set; }
        public virtual DbSet<Exp_DrugDosageStageView> Exp_DrugDosageStageView { get; set; }
        public virtual DbSet<EXP_DrugDosageView> EXP_DrugDosageView { get; set; }
        public virtual DbSet<EXP_DrugSubstanceView> EXP_DrugSubstanceView { get; set; }
        public virtual DbSet<EXP_DrugTypeView> EXP_DrugTypeView { get; set; }
        public virtual DbSet<EXP_ExpertiseStageDosageCommissionView> EXP_ExpertiseStageDosageCommissionView { get; set; }
        public virtual DbSet<EXP_MaterialDirectionsView> EXP_MaterialDirectionsView { get; set; }
        public virtual DbSet<EXP_PriceListDirectionToPayView> EXP_PriceListDirectionToPayView { get; set; }
        public virtual DbSet<EXP_TaskRegistryView> EXP_TaskRegistryView { get; set; }
        public virtual DbSet<FileLinkView> FileLinkViews { get; set; }
        public virtual DbSet<IncomingDocument> IncomingDocuments { get; set; }
        public virtual DbSet<Lims_ActivityView> Lims_ActivityView { get; set; }
        public virtual DbSet<LimsApplicationJournalView> LimsApplicationJournalViews { get; set; }
        public virtual DbSet<LimsEquipmentActSparePartsView> LimsEquipmentActSparePartsViews { get; set; }
        public virtual DbSet<LimsEquipmentActView> LimsEquipmentActViews { get; set; }
        public virtual DbSet<LimsEquipmentJournalRecordView> LimsEquipmentJournalRecordViews { get; set; }
        public virtual DbSet<LimsEquipmentPlanView> LimsEquipmentPlanViews { get; set; }
        public virtual DbSet<LimsEquipmentView> LimsEquipmentViews { get; set; }
        public virtual DbSet<LimsTmcActualView> LimsTmcActualViews { get; set; }
        public virtual DbSet<LimsTmcInView> LimsTmcInViews { get; set; }
        public virtual DbSet<LimsTmcOutView> LimsTmcOutViews { get; set; }
        public virtual DbSet<LimsTmcTempView> LimsTmcTempViews { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationRegisterView> OBK_AssessmentDeclarationRegisterView { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationReportView> OBK_AssessmentDeclarationReportView { get; set; }
        public virtual DbSet<OBK_AssessmentDeclarationView> OBK_AssessmentDeclarationView { get; set; }
        public virtual DbSet<OBK_BlankAccountingView> OBK_BlankAccountingView { get; set; }
        public virtual DbSet<OBK_ContractHistoryView> OBK_ContractHistoryView { get; set; }
        public virtual DbSet<OBK_ContractRegisterView> OBK_ContractRegisterView { get; set; }
        public virtual DbSet<OBK_ContractReportView> OBK_ContractReportView { get; set; }
        public virtual DbSet<OBK_ContractView> OBK_ContractView { get; set; }
        public virtual DbSet<OBK_ContractWithoutAttachmentsView> OBK_ContractWithoutAttachmentsView { get; set; }
        public virtual DbSet<OBK_CorruptedBlankNumberView> OBK_CorruptedBlankNumberView { get; set; }
        public virtual DbSet<OBK_DeclarationCorrespondingView> OBK_DeclarationCorrespondingView { get; set; }
        public virtual DbSet<OBK_DirectionToPaymentsView> OBK_DirectionToPaymentsView { get; set; }
        public virtual DbSet<OBK_ZBKCopyStageView> OBK_ZBKCopyStageView { get; set; }
        public virtual DbSet<OBK_ZBKRegisterView> OBK_ZBKRegisterView { get; set; }
        public virtual DbSet<OBK_ZBKTransferRegisterView> OBK_ZBKTransferRegisterView { get; set; }
        public virtual DbSet<OBKContractProductsView> OBKContractProductsViews { get; set; }
        public virtual DbSet<OBKTaskListRegisterView> OBKTaskListRegisterViews { get; set; }
        public virtual DbSet<OrganizationsView> OrganizationsViews { get; set; }
        public virtual DbSet<OrphanDrugsIcdDeseasesView> OrphanDrugsIcdDeseasesViews { get; set; }
        public virtual DbSet<OutgoingDocument> OutgoingDocuments { get; set; }
        public virtual DbSet<PackagesView> PackagesViews { get; set; }
        public virtual DbSet<PP_AttachRemarksView> PP_AttachRemarksView { get; set; }
        public virtual DbSet<PP_DIC_AvgExchangeRateView> PP_DIC_AvgExchangeRateView { get; set; }
        public virtual DbSet<PP_ProcessingJournal> PP_ProcessingJournal { get; set; }
        public virtual DbSet<PP_ProtocolListView> PP_ProtocolListView { get; set; }
        public virtual DbSet<PriceProjectArchiveDetail> PriceProjectArchiveDetails { get; set; }
        public virtual DbSet<PriceProjectArchiveView> PriceProjectArchiveViews { get; set; }
        public virtual DbSet<PriceProjectJournal> PriceProjectJournals { get; set; }
        public virtual DbSet<PriceProjectsView> PriceProjectsViews { get; set; }
        public virtual DbSet<PricesView> PricesViews { get; set; }
        public virtual DbSet<ProjectDocument> ProjectDocuments { get; set; }
        public virtual DbSet<ProjectsView> ProjectsViews { get; set; }
        public virtual DbSet<PrtPrjsView> PrtPrjsViews { get; set; }
        public virtual DbSet<PrtPrjsView2> PrtPrjsView2 { get; set; }
        public virtual DbSet<ReesrtObk> ReesrtObks { get; set; }
        public virtual DbSet<ReestrDirectionToPayView> ReestrDirectionToPayViews { get; set; }
        public virtual DbSet<RegisterOrderer2Views> RegisterOrderer2Views { get; set; }
        public virtual DbSet<RegisterOrdererView> RegisterOrdererViews { get; set; }
        public virtual DbSet<RegisterProjectJournal> RegisterProjectJournals { get; set; }
        public virtual DbSet<RegisterProjectsView> RegisterProjectsViews { get; set; }
        public virtual DbSet<RequestListView> RequestListViews { get; set; }
        public virtual DbSet<RequestOrderListView> RequestOrderListViews { get; set; }
        public virtual DbSet<SrProducerView> SrProducerViews { get; set; }
        public virtual DbSet<SrReestrView> SrReestrViews { get; set; }
        public virtual DbSet<TmcCountStateView> TmcCountStateViews { get; set; }
        public virtual DbSet<TmcInView> TmcInViews { get; set; }
        public virtual DbSet<TmcOffStateView> TmcOffStateViews { get; set; }
        public virtual DbSet<TmcOffView> TmcOffViews { get; set; }
        public virtual DbSet<TmcOutCountView> TmcOutCountViews { get; set; }
        public virtual DbSet<TmcOutView> TmcOutViews { get; set; }
        public virtual DbSet<TmcReportDataSourceView> TmcReportDataSourceViews { get; set; }
        public virtual DbSet<TmcReportsView> TmcReportsViews { get; set; }
        public virtual DbSet<TmcUseOffView> TmcUseOffViews { get; set; }
        public virtual DbSet<TmcView> TmcViews { get; set; }
        public virtual DbSet<VisitsView> VisitsViews { get; set; }
        public virtual DbSet<vw_aspnet_Applications> vw_aspnet_Applications { get; set; }
        public virtual DbSet<vw_aspnet_MembershipUsers> vw_aspnet_MembershipUsers { get; set; }
        public virtual DbSet<vw_aspnet_Profiles> vw_aspnet_Profiles { get; set; }
        public virtual DbSet<vw_aspnet_Roles> vw_aspnet_Roles { get; set; }
        public virtual DbSet<vw_aspnet_Users> vw_aspnet_Users { get; set; }
        public virtual DbSet<vw_aspnet_UsersInRoles> vw_aspnet_UsersInRoles { get; set; }
        public virtual DbSet<vw_aspnet_WebPartState_Paths> vw_aspnet_WebPartState_Paths { get; set; }
        public virtual DbSet<vw_aspnet_WebPartState_Shared> vw_aspnet_WebPartState_Shared { get; set; }
        public virtual DbSet<vw_aspnet_WebPartState_User> vw_aspnet_WebPartState_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.Activity)
                .WillCascadeOnDelete();

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Paths)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Roles)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Applications>()
                .HasMany(e => e.aspnet_Users)
                .WithRequired(e => e.aspnet_Applications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<aspnet_Paths>()
                .HasOptional(e => e.aspnet_PersonalizationAllUsers)
                .WithRequired(e => e.aspnet_Paths);

            modelBuilder.Entity<aspnet_Roles>()
                .HasMany(e => e.aspnet_Users)
                .WithMany(e => e.aspnet_Roles)
                .Map(m => m.ToTable("aspnet_UsersInRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Membership)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_Users>()
                .HasOptional(e => e.aspnet_Profile)
                .WithRequired(e => e.aspnet_Users);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventId)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventSequence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<aspnet_WebEvent_Events>()
                .Property(e => e.EventOccurrence)
                .HasPrecision(19, 0);

            modelBuilder.Entity<CommissionConclusionType>()
                .HasMany(e => e.CommissionDrugDosages)
                .WithOptional(e => e.CommissionConclusionType)
                .HasForeignKey(e => e.ConclusionTypeId);

            modelBuilder.Entity<CommissionKind>()
                .HasMany(e => e.Commissions)
                .WithRequired(e => e.CommissionKind)
                .HasForeignKey(e => e.KindId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommissionQuestionType>()
                .HasMany(e => e.CommissionQuestions)
                .WithRequired(e => e.CommissionQuestionType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Commission>()
                .HasMany(e => e.CommissionDrugDosages)
                .WithRequired(e => e.Commission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Commission>()
                .HasMany(e => e.CommissionQuestions)
                .WithRequired(e => e.Commission)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommissionType>()
                .HasMany(e => e.Commissions)
                .WithRequired(e => e.CommissionType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CommissionUnitType>()
                .HasMany(e => e.CommissionUnits)
                .WithRequired(e => e.CommissionUnitType)
                .HasForeignKey(e => e.UnitTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.ContractComments)
                .WithRequired(e => e.Contract)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.Contracts1)
                .WithOptional(e => e.ParentContract)
                .HasForeignKey(e => e.ContractId);

            modelBuilder.Entity<Contract>()
                .HasOptional(e => e.ContractSignedData)
                .WithRequired(e => e.Contract);

            modelBuilder.Entity<DIC_FileLinkStatus>()
                .HasMany(e => e.FileLinks)
                .WithOptional(e => e.DIC_FileLinkStatus)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<DIC_Storages>()
                .HasMany(e => e.DIC_Storages1)
                .WithOptional(e => e.DIC_Storages2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<DIC_Storages>()
                .HasMany(e => e.EXP_Materials)
                .WithOptional(e => e.DIC_Storages)
                .HasForeignKey(e => e.StorageId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.Contracts)
                .WithOptional(e => e.ContractAdditionType)
                .HasForeignKey(e => e.ContractAdditionTypeId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.Contracts1)
                .WithOptional(e => e.DoverennostType)
                .HasForeignKey(e => e.DoverennostTypeDicId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.Contracts2)
                .WithOptional(e => e.Dictionary2)
                .HasForeignKey(e => e.AgentDocTypeDicId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.Contracts3)
                .WithOptional(e => e.ContractStatus)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.Contracts4)
                .WithOptional(e => e.HolderType)
                .HasForeignKey(e => e.HolderTypeId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.Dictionaries1)
                .WithOptional(e => e.Dictionary1)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Activities)
                .WithRequired(e => e.ExpActivityStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Activities1)
                .WithRequired(e => e.ExpActivityType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Activities2)
                .WithRequired(e => e.ExpAgreedDocType)
                .HasForeignKey(e => e.DocumentTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_AgreementProcSettings)
                .WithRequired(e => e.Dictionary)
                .HasForeignKey(e => e.AgreedDocTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_AgreementProcSettingsActivities)
                .WithRequired(e => e.ActivityType)
                .HasForeignKey(e => e.ActivityTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_AgreementProcSettingsTasks)
                .WithRequired(e => e.TaskType)
                .HasForeignKey(e => e.TaskTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_CertificateOfCompletion)
                .WithOptional(e => e.Status)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_DirectionToPays)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_DrugCorespondence)
                .WithRequired(e => e.Dictionary)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_DrugPrimaryFinalDocument)
                .WithOptional(e => e.Dictionary)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_ExpertiseStageDosage)
                .WithOptional(e => e.FinalDocStatus)
                .HasForeignKey(e => e.FinalDocStatusId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_MaterialDirections)
                .WithRequired(e => e.Status)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials)
                .WithOptional(e => e.Dictionary)
                .HasForeignKey(e => e.ConcentrationUnitId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials1)
                .WithOptional(e => e.ConcordanceStatement)
                .HasForeignKey(e => e.ConcordanceStatementId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials2)
                .WithOptional(e => e.Country)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials3)
                .WithOptional(e => e.DosageUnit)
                .HasForeignKey(e => e.DosageUnitId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials4)
                .WithOptional(e => e.ExternalState)
                .HasForeignKey(e => e.ExternalStateId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials5)
                .WithOptional(e => e.Status)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials6)
                .WithOptional(e => e.StorageCondition)
                .HasForeignKey(e => e.StorageConditionId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials7)
                .WithRequired(e => e.MaterialType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials8)
                .WithRequired(e => e.Unit)
                .HasForeignKey(e => e.UnitId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Materials9)
                .WithOptional(e => e.VolumeUnit)
                .HasForeignKey(e => e.VolumeUnitId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Tasks)
                .WithRequired(e => e.ExpTaskType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.EXP_Tasks1)
                .WithRequired(e => e.StatusesDictionary)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.FileLinks)
                .WithOptional(e => e.FileCategory)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipments)
                .WithOptional(e => e.CountryProductionDic)
                .HasForeignKey(e => e.CountryProductionId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipments1)
                .WithOptional(e => e.EquipmentTypeDic)
                .HasForeignKey(e => e.EquipmentTypeId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipments2)
                .WithOptional(e => e.LaboratoryDic)
                .HasForeignKey(e => e.LaboratoryId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipments3)
                .WithOptional(e => e.LocationDic)
                .HasForeignKey(e => e.LocationId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipments4)
                .WithOptional(e => e.ModelDic)
                .HasForeignKey(e => e.ModelId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipments5)
                .WithOptional(e => e.ProducerDic)
                .HasForeignKey(e => e.ProducerId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipments6)
                .WithOptional(e => e.StatusDic)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipmentActs)
                .WithRequired(e => e.ActTypeDic)
                .HasForeignKey(e => e.ActTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipmentActSpareParts)
                .WithOptional(e => e.LocationDic)
                .HasForeignKey(e => e.LocationId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipmentJournals)
                .WithRequired(e => e.JournalTypeDic)
                .HasForeignKey(e => e.JournalTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.LimsEquipmentPlans)
                .WithRequired(e => e.PlanTypeDic)
                .HasForeignKey(e => e.PlanTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.OBK_ActReception)
                .WithOptional(e => e.Dictionary)
                .HasForeignKey(e => e.ProductSamplesId);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.OBK_Contract)
                .WithOptional(e => e.Dictionary)
                .HasForeignKey(e => e.ContractAdditionType);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.OBK_Ref_PriceList)
                .WithRequired(e => e.Dictionary)
                .HasForeignKey(e => e.UnitId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.PP_AttachRemarks)
                .WithRequired(e => e.Dictionary)
                .HasForeignKey(e => e.AttachPriceDicId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.PP_DIC_AvgExchangeRate)
                .WithRequired(e => e.Dictionary)
                .HasForeignKey(e => e.CurrencyId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Dictionary>()
                .HasMany(e => e.PP_PharmaList)
                .WithRequired(e => e.Dictionary)
                .HasForeignKey(e => e.RegionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.AccessDocuments)
                .WithRequired(e => e.Document)
                .HasForeignKey(e => e.ObjectId);

            modelBuilder.Entity<Document>()
                .HasMany(e => e.Activities)
                .WithOptional(e => e.Document)
                .WillCascadeOnDelete();

            modelBuilder.Entity<EMP_Contract>()
                .HasMany(e => e.EMP_ContractExtHistory)
                .WithRequired(e => e.EMP_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Contract>()
                .HasMany(e => e.EMP_ContractHistory)
                .WithRequired(e => e.EMP_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Contract>()
                .HasOptional(e => e.EMP_ContractSignData)
                .WithRequired(e => e.EMP_Contract);

            modelBuilder.Entity<EMP_Contract>()
                .HasMany(e => e.EMP_ContractStage)
                .WithRequired(e => e.EMP_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Contract>()
                .HasMany(e => e.EMP_CostWorks)
                .WithRequired(e => e.EMP_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Contract>()
                .HasMany(e => e.EMP_DirectionToPayments)
                .WithRequired(e => e.EMP_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_ContractStage>()
                .HasMany(e => e.EMP_ContractStage1)
                .WithOptional(e => e.EMP_ContractStage2)
                .HasForeignKey(e => e.ParentStageId);

            modelBuilder.Entity<EMP_ContractStage>()
                .HasMany(e => e.EMP_ContractStageExecutors)
                .WithRequired(e => e.EMP_ContractStage)
                .HasForeignKey(e => e.ContractStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Ref_Bank>()
                .HasMany(e => e.OBK_DeclarantContact)
                .WithOptional(e => e.EMP_Ref_Bank)
                .HasForeignKey(e => e.BankId);

            modelBuilder.Entity<EMP_Ref_ContractScope>()
                .HasMany(e => e.EMP_Contract)
                .WithOptional(e => e.EMP_Ref_ContractScope)
                .HasForeignKey(e => e.ContractScopeId);

            modelBuilder.Entity<EMP_Ref_ContractType>()
                .HasMany(e => e.EMP_Contract)
                .WithOptional(e => e.EMP_Ref_ContractType)
                .HasForeignKey(e => e.ContractType);

            modelBuilder.Entity<EMP_Ref_DegreeRisk>()
                .HasMany(e => e.EMP_Ref_ServiceType)
                .WithOptional(e => e.EMP_Ref_DegreeRisk)
                .HasForeignKey(e => e.DegreeRiskId);

            modelBuilder.Entity<EMP_Ref_PriceList>()
                .HasMany(e => e.EMP_CostWorks)
                .WithRequired(e => e.EMP_Ref_PriceList)
                .HasForeignKey(e => e.PriceListId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Ref_PriceType>()
                .HasMany(e => e.EMP_Ref_PriceList)
                .WithRequired(e => e.EMP_Ref_PriceType)
                .HasForeignKey(e => e.PriceTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Ref_ServiceType>()
                .HasMany(e => e.EMP_Ref_PriceList)
                .WithRequired(e => e.EMP_Ref_ServiceType)
                .HasForeignKey(e => e.ServiceTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Ref_ServiceType>()
                .HasMany(e => e.EMP_Ref_ServiceType1)
                .WithOptional(e => e.EMP_Ref_ServiceType2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<EMP_Ref_Stage>()
                .HasMany(e => e.EMP_ContractStage)
                .WithRequired(e => e.EMP_Ref_Stage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Ref_StageStatus>()
                .HasMany(e => e.EMP_ContractStage)
                .WithRequired(e => e.EMP_Ref_StageStatus)
                .HasForeignKey(e => e.StageStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMP_Ref_Status>()
                .HasMany(e => e.EMP_Contract)
                .WithOptional(e => e.EMP_Ref_Status)
                .HasForeignKey(e => e.ContractStatusId);

            modelBuilder.Entity<EMP_Statement>()
                .HasMany(e => e.EMP_StatementSamples)
                .WithRequired(e => e.EMP_Statement)
                .HasForeignKey(e => e.StatementId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CommissionUnits)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ContractComments)
                .WithRequired(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.ContractProcSettings)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ProcCenterHeadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Contracts)
                .WithOptional(e => e.Signer)
                .HasForeignKey(e => e.SignerId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EMP_ContractStageExecutors)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ExecutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EMP_DirectionToPayments)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.CreateEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_Activities)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_AgreementProcSettingsTasks)
                .WithOptional(e => e.Executor)
                .HasForeignKey(e => e.ExecutorId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_CertificateOfCompletion)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.CreateEmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DirectionToPays)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.CreateEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DrugCorespondence)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.AuthorId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DrugCorespondence1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.MatchingId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DrugCorespondence2)
                .WithOptional(e => e.Employee2)
                .HasForeignKey(e => e.SignatoryId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DrugCorespondenceRemark)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ExecuterId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DrugDeclarationComRecord)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DrugDeclarationFieldHistory)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DrugDeclarationHistory)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_DrugPrimaryRemark)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ExecuterId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_ExpertiseStageExecutors)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ExecutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_ExpertiseStageRemark)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ExecuterId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.FileLinks)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_MaterialDirections)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ExecutorEmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_MaterialDirections1)
                .WithOptional(e => e.Employee1)
                .HasForeignKey(e => e.SendEmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PriceProjectsHistories)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_RegistrationExpSteps)
                .WithOptional(e => e.SupervisingEmployee)
                .HasForeignKey(e => e.SupervisingEmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_Tasks)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_Tasks1)
                .WithRequired(e => e.ExecutorEmployee)
                .HasForeignKey(e => e.ExecutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.FileLinksCategoryComRecords)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.GridSettings)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsApplicationJournals)
                .WithOptional(e => e.AccepterEmp)
                .HasForeignKey(e => e.AccepterId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsApplicationJournals1)
                .WithOptional(e => e.ApplicationEmp)
                .HasForeignKey(e => e.ApplicantId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsApplicationJournals2)
                .WithOptional(e => e.EngineerEmp)
                .HasForeignKey(e => e.EngineerId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsEquipments)
                .WithOptional(e => e.ResponsiblePersonEmp)
                .HasForeignKey(e => e.ResponsiblePersonId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsEquipmentActs)
                .WithOptional(e => e.DirectorRcEmp)
                .HasForeignKey(e => e.DirectorRCId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsEquipmentActs1)
                .WithOptional(e => e.EngineerEmp)
                .HasForeignKey(e => e.EngineerId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsEquipmentActs2)
                .WithOptional(e => e.HeadOfLaboratoryEmp)
                .HasForeignKey(e => e.HeadOfLaboratoryId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsEquipmentJournalRecords)
                .WithOptional(e => e.ExecutorEmp)
                .HasForeignKey(e => e.ExecutorId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsEquipmentPlans)
                .WithOptional(e => e.DirectorRcEmp)
                .HasForeignKey(e => e.DirectorRcId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.LimsEquipmentPlans1)
                .WithOptional(e => e.HeadOfOpoloEmp)
                .HasForeignKey(e => e.HeadOfOpoloId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_AssessmentDeclarationComRecord)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_AssessmentDeclarationFieldHistory)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_AssessmentStageExecutors)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ExecutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_AssessmentStageSignData)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.SignerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_Contract)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.Signer);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_ContractComRecord)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_ContractPriceComRecord)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_ContractStageExecutors)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ExecutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_DirectionToPayments)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.CreateEmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_OP_Commission)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_Products_SeriesComRecord)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_ResearchCenterResult)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ExecutorId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_TaskExecutor)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ExecutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_TaskMaterial)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.LaboratoryAssistantId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_Tasks)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ExecutorId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_ZBKCopySignData)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.SignerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_ZBKCopyStageExecutors)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.ExecutorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_ZBKCopyStageSignData)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.SignerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PP_DIC_AvgExchangeRate)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.ChangeEmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PP_PharmaList)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PP_ProtocolComissionMembers)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PriceProjectComRecords)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PriceProjectFieldHistories)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_ContractFactoryComRecord)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.OBK_RS_ProductsComRecord)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.VisitEmployeeWorkingTimes)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Visits)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Visits1)
                .WithRequired(e => e.Employee1)
                .HasForeignKey(e => e.VisitorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EXP_RegistrationExpSteps1)
                .WithMany(e => e.Executors)
                .Map(m => m.ToTable("EXP_RegistrationExpStepsExecutors").MapLeftKey("ExecutorId").MapRightKey("StepId"));

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Units)
                .WithRequired(e => e.Employee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_Activities>()
                .HasMany(e => e.EXP_Tasks)
                .WithRequired(e => e.EXP_Activities)
                .HasForeignKey(e => e.ActivityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_AgreementProcSettings>()
                .HasMany(e => e.EXP_AgreementProcSettingsActivities)
                .WithRequired(e => e.EXP_AgreementProcSettings)
                .HasForeignKey(e => e.SettingId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_AgreementProcSettingsActivities>()
                .HasMany(e => e.EXP_AgreementProcSettingsTasks)
                .WithRequired(e => e.EXP_AgreementProcSettingsActivities)
                .HasForeignKey(e => e.ActivityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_AgreementProcSettingsTasks>()
                .HasMany(e => e.EXP_AgreementProcSettingsTasks1)
                .WithOptional(e => e.EXP_AgreementProcSettingsTasks2)
                .HasForeignKey(e => e.ParentTaskId);

            modelBuilder.Entity<EXP_DIC_AccelerationType>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.EXP_DIC_AccelerationType)
                .HasForeignKey(e => e.AccelerationTypeId);

            modelBuilder.Entity<EXP_DIC_AnalyseIndicator>()
                .HasMany(e => e.EXP_DrugAnaliseIndicator)
                .WithOptional(e => e.EXP_DIC_AnalyseIndicator)
                .HasForeignKey(e => e.AnalyseIndicator);

            modelBuilder.Entity<EXP_DIC_ChangeType>()
                .HasMany(e => e.EXP_DrugChangeType)
                .WithRequired(e => e.EXP_DIC_ChangeType)
                .HasForeignKey(e => e.ChangeTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_CorespondenceKind>()
                .HasMany(e => e.EXP_DrugCorespondence)
                .WithRequired(e => e.EXP_DIC_CorespondenceKind)
                .HasForeignKey(e => e.KindId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_CorespondenceSubject>()
                .HasMany(e => e.EXP_DrugCorespondence)
                .WithRequired(e => e.EXP_DIC_CorespondenceSubject)
                .HasForeignKey(e => e.SubjectId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_CorespondenceType>()
                .HasMany(e => e.EXP_DrugCorespondence)
                .WithRequired(e => e.EXP_DIC_CorespondenceType)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_DrugType>()
                .HasMany(e => e.EXP_DrugType)
                .WithOptional(e => e.EXP_DIC_DrugType)
                .HasForeignKey(e => e.DrugTypeId);

            modelBuilder.Entity<EXP_DIC_DrugTypeKind>()
                .HasMany(e => e.EXP_DrugType)
                .WithOptional(e => e.EXP_DIC_DrugTypeKind)
                .HasForeignKey(e => e.DrugTypeKind);

            modelBuilder.Entity<EXP_DIC_ManufactureType>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.EXP_DIC_ManufactureType)
                .HasForeignKey(e => e.ManufactureTypeId);

            modelBuilder.Entity<EXP_DIC_NMIRK>()
                .HasMany(e => e.EMP_EAESStatement)
                .WithOptional(e => e.EXP_DIC_NMIRK)
                .HasForeignKey(e => e.NmirkId);

            modelBuilder.Entity<EXP_DIC_NMIRK>()
                .HasMany(e => e.EMP_Statement)
                .WithOptional(e => e.EXP_DIC_NMIRK)
                .HasForeignKey(e => e.NmirkId);

            modelBuilder.Entity<EXP_DIC_NormDocFarm>()
                .HasMany(e => e.EXP_DrugSubstance)
                .WithOptional(e => e.EXP_DIC_NormDocFarm)
                .HasForeignKey(e => e.NormDocFarmId);

            modelBuilder.Entity<EXP_DIC_Origin>()
                .HasMany(e => e.EXP_DrugSubstance)
                .WithOptional(e => e.EXP_DIC_Origin)
                .HasForeignKey(e => e.OriginId);

            modelBuilder.Entity<EXP_DIC_PeriodMeasure>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.EXP_DIC_PeriodMeasure)
                .HasForeignKey(e => e.ProposedShelfLifeMeasureId);

            modelBuilder.Entity<EXP_DIC_PlantKind>()
                .HasMany(e => e.EXP_DrugSubstance)
                .WithOptional(e => e.EXP_DIC_PlantKind)
                .HasForeignKey(e => e.PlantKindId);

            modelBuilder.Entity<EXP_DIC_PrimaryFinalyDocResult>()
                .HasMany(e => e.EXP_DrugPrimaryFinalDocument)
                .WithOptional(e => e.EXP_DIC_PrimaryFinalyDocResult)
                .HasForeignKey(e => e.ResultId);

            modelBuilder.Entity<EXP_DIC_PrimaryMark>()
                .HasMany(e => e.EXP_DrugPrimaryKind)
                .WithRequired(e => e.EXP_DIC_PrimaryMark)
                .HasForeignKey(e => e.PrimaryKindId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_PrimaryOTD>()
                .HasMany(e => e.EXP_DIC_PrimaryOTD1)
                .WithOptional(e => e.EXP_DIC_PrimaryOTD2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<EXP_DIC_RemarkType>()
                .HasMany(e => e.EXP_DrugCorespondenceRemark)
                .WithOptional(e => e.EXP_DIC_RemarkType)
                .HasForeignKey(e => e.RemarkTypeId);

            modelBuilder.Entity<EXP_DIC_RemarkType>()
                .HasMany(e => e.EXP_DrugPrimaryRemark)
                .WithOptional(e => e.EXP_DIC_RemarkType)
                .HasForeignKey(e => e.RemarkTypeId);

            modelBuilder.Entity<EXP_DIC_RemarkType>()
                .HasMany(e => e.EXP_DrugAppDosageRemark)
                .WithOptional(e => e.EXP_DIC_RemarkType)
                .HasForeignKey(e => e.RemarkTypeId);

            modelBuilder.Entity<EXP_DIC_RemarkType>()
                .HasMany(e => e.EXP_DrugAppDosageResult)
                .WithOptional(e => e.EXP_DIC_RemarkType)
                .HasForeignKey(e => e.RemarkTypeId);

            modelBuilder.Entity<EXP_DIC_RemarkType>()
                .HasMany(e => e.EXP_ExpertiseStageRemark)
                .WithOptional(e => e.EXP_DIC_RemarkType)
                .HasForeignKey(e => e.RemarkTypeId);

            modelBuilder.Entity<EXP_DIC_SaleType>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.EXP_DIC_SaleType)
                .HasForeignKey(e => e.SaleTypeId);

            modelBuilder.Entity<EXP_DIC_SaleType>()
                .HasMany(e => e.EXP_DrugDosage)
                .WithOptional(e => e.EXP_DIC_SaleType)
                .HasForeignKey(e => e.SaleTypeId);

            modelBuilder.Entity<EXP_DIC_Stage>()
                .HasMany(e => e.EXP_CertificateOfCompletion)
                .WithOptional(e => e.EXP_DIC_Stage)
                .HasForeignKey(e => e.DicStageId);

            modelBuilder.Entity<EXP_DIC_Stage>()
                .HasMany(e => e.EXP_RegistrationExpSteps)
                .WithOptional(e => e.EXP_DIC_Stage)
                .HasForeignKey(e => e.RefId);

            modelBuilder.Entity<EXP_DIC_Stage>()
                .HasMany(e => e.FileLinks)
                .WithOptional(e => e.EXP_DIC_Stage)
                .HasForeignKey(e => e.StageId);

            modelBuilder.Entity<EXP_DIC_Stage>()
                .HasMany(e => e.EXP_DrugCorespondence)
                .WithRequired(e => e.EXP_DIC_Stage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_Stage>()
                .HasMany(e => e.EXP_ExpertiseStage)
                .WithRequired(e => e.EXP_DIC_Stage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_Stage>()
                .HasMany(e => e.EXP_DIC_StageResults)
                .WithMany(e => e.EXP_DIC_Stage)
                .Map(m => m.ToTable("EXP_DIC_ResultByStage").MapLeftKey("StageId").MapRightKey("StageResultId"));

            modelBuilder.Entity<EXP_DIC_StageResult>()
                .HasMany(e => e.EXP_ExpertiseStageDosageResult)
                .WithOptional(e => e.EXP_DIC_StageResult)
                .HasForeignKey(e => e.ResultId);

            modelBuilder.Entity<EXP_DIC_StageResult>()
                .HasMany(e => e.EXP_ExpertiseStage)
                .WithOptional(e => e.EXP_DIC_StageResult)
                .HasForeignKey(e => e.ResultId);

            modelBuilder.Entity<EXP_DIC_StageResult>()
                .HasMany(e => e.EXP_ExpertiseStageDosage)
                .WithOptional(e => e.EXP_DIC_StageResult)
                .HasForeignKey(e => e.ResultId);

            modelBuilder.Entity<EXP_DIC_StageStatus>()
                .HasMany(e => e.EXP_ExpertiseStage)
                .WithRequired(e => e.EXP_DIC_StageStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_Status>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithRequired(e => e.EXP_DIC_Status)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_Status>()
                .HasMany(e => e.EXP_DrugDeclarationHistory)
                .WithRequired(e => e.EXP_DIC_Status)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_Type>()
                .HasMany(e => e.EXP_RegistrationTypes)
                .WithOptional(e => e.EXP_DIC_Type)
                .HasForeignKey(e => e.RefId);

            modelBuilder.Entity<EXP_DIC_Type>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithRequired(e => e.EXP_DIC_Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DIC_TypeFileND>()
                .HasMany(e => e.EXP_DrugPrimaryNTD)
                .WithOptional(e => e.EXP_DIC_TypeFileND)
                .HasForeignKey(e => e.TypeFileNDId);

            modelBuilder.Entity<EXP_DIC_TypeND>()
                .HasMany(e => e.EXP_DrugPrimaryNTD)
                .WithOptional(e => e.EXP_DIC_TypeND)
                .HasForeignKey(e => e.TypeNDId);

            modelBuilder.Entity<EXP_DIC_WrappingType>()
                .HasMany(e => e.EXP_DrugWrapping)
                .WithOptional(e => e.EXP_DIC_WrappingType)
                .HasForeignKey(e => e.WrappingTypeId);

            modelBuilder.Entity<EXP_DirectionToPays>()
                .HasMany(e => e.EXP_DirectionToPays_PriceList)
                .WithRequired(e => e.EXP_DirectionToPays)
                .HasForeignKey(e => e.DirectionToPayId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DirectionToPays>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithMany(e => e.EXP_DirectionToPays)
                .Map(m => m.ToTable("EXP_DirectionToPays_DrugDeclaration").MapLeftKey("DirectionToPayId").MapRightKey("DrugDeclarationId"));

            modelBuilder.Entity<EXP_DrugCorespondence>()
                .HasMany(e => e.EXP_DrugCorespondenceRemark)
                .WithRequired(e => e.EXP_DrugCorespondence)
                .HasForeignKey(e => e.DrugCorespondenceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_CertificateOfCompletion)
                .WithOptional(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugChangeType)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugCorespondence)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugDeclarationCom)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugDeclarationFieldHistory)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugDeclarationHistory)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugDosage)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugExportTrade)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugOtherCountry)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugPatent)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugPrimaryNTD)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugPrimaryRemark)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugProtectionDoc)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugType)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugUseMethod)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_ExpertiseStage)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_MaterialDirections)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_Materials)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugPrimaryKind)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclaration>()
                .HasMany(e => e.EXP_DrugOrganizations)
                .WithRequired(e => e.EXP_DrugDeclaration)
                .HasForeignKey(e => e.DrugDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDeclarationCom>()
                .HasMany(e => e.EXP_DrugDeclarationComRecord)
                .WithRequired(e => e.EXP_DrugDeclarationCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.CommissionDrugDosages)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DrugDosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.CommissionDrugDosageNeedCommissions)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DrugDosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.EXP_DrugAppDosageRemark)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DrugDosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.EXP_DrugAppDosageResult)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DrugDosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.EXP_DrugPrice)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DrugDosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.EXP_DrugPrimaryFinalDocument)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DrugDosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.EXP_DrugSubstance)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DrugDosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.EXP_DrugWrapping)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DrugDosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugDosage>()
                .HasMany(e => e.EXP_ExpertiseStageDosage)
                .WithRequired(e => e.EXP_DrugDosage)
                .HasForeignKey(e => e.DosageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_DrugSubstance>()
                .HasMany(e => e.EXP_DrugSubstanceManufacture)
                .WithRequired(e => e.EXP_DrugSubstance)
                .HasForeignKey(e => e.DrugSubstanceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStage>()
                .HasMany(e => e.CommissionDrugDosages)
                .WithRequired(e => e.EXP_ExpertiseStage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStage>()
                .HasMany(e => e.CommissionDrugDosageNeedCommissions)
                .WithRequired(e => e.EXP_ExpertiseStage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStage>()
                .HasMany(e => e.EXP_ExpertiseStage1)
                .WithOptional(e => e.EXP_ExpertiseStage2)
                .HasForeignKey(e => e.ParentStageId);

            modelBuilder.Entity<EXP_ExpertiseStage>()
                .HasMany(e => e.EXP_ExpertiseStageDosage)
                .WithRequired(e => e.EXP_ExpertiseStage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStage>()
                .HasMany(e => e.EXP_ExpertiseStageExecutors)
                .WithRequired(e => e.EXP_ExpertiseStage)
                .HasForeignKey(e => e.ExpertiseStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStage>()
                .HasMany(e => e.EXP_ExpertiseStageRemark)
                .WithRequired(e => e.EXP_ExpertiseStage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStageDosage>()
                .HasMany(e => e.EXP_DrugAnaliseIndicator)
                .WithRequired(e => e.EXP_ExpertiseStageDosage)
                .HasForeignKey(e => e.DosageStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStageDosage>()
                .HasMany(e => e.EXP_ExpertisePharmaceuticalFinalDoc)
                .WithRequired(e => e.EXP_ExpertiseStageDosage)
                .HasForeignKey(e => e.DosageStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStageDosage>()
                .HasMany(e => e.EXP_ExpertisePharmacologicalFinalDoc)
                .WithRequired(e => e.EXP_ExpertiseStageDosage)
                .HasForeignKey(e => e.DosageStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStageDosage>()
                .HasMany(e => e.PrimaryFinalDocs)
                .WithRequired(e => e.EXP_ExpertiseStageDosage)
                .HasForeignKey(e => e.DosageStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStageDosage>()
                .HasMany(e => e.EXP_ExpertiseSafetyreportFinalDoc)
                .WithRequired(e => e.EXP_ExpertiseStageDosage)
                .HasForeignKey(e => e.DosageStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_ExpertiseStageDosage>()
                .HasMany(e => e.EXP_ExpertiseStageDosageResult)
                .WithRequired(e => e.EXP_ExpertiseStageDosage)
                .HasForeignKey(e => e.StageDosageId);

            modelBuilder.Entity<EXP_ExpertiseStageDosageResult>()
                .HasMany(e => e.CommissionDrugDosages)
                .WithOptional(e => e.EXP_ExpertiseStageDosageResult)
                .HasForeignKey(e => e.ExpertResultId);

            modelBuilder.Entity<EXP_PriceList>()
                .HasMany(e => e.EXP_DirectionToPays_PriceList)
                .WithRequired(e => e.EXP_PriceList)
                .HasForeignKey(e => e.PriceListId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EXP_RegistrationTypes>()
                .HasMany(e => e.EXP_RegistrationExpSteps)
                .WithRequired(e => e.EXP_RegistrationTypes)
                .HasForeignKey(e => e.RegistrationId);

            modelBuilder.Entity<EXP_RegistrationTypes>()
                .HasMany(e => e.EXP_RegistrationTypes1)
                .WithOptional(e => e.EXP_RegistrationTypes2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<EXP_Tasks>()
                .HasMany(e => e.EXP_TasksChild)
                .WithOptional(e => e.EXP_TasksParent)
                .HasForeignKey(e => e.ParentTaskId);

            modelBuilder.Entity<FileLink>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<FileLink>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<FileLink>()
                .HasMany(e => e.FileLinks1)
                .WithOptional(e => e.FileLink1)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<FileLinksCategoryCom>()
                .HasMany(e => e.FileLinksCategoryComRecords)
                .WithRequired(e => e.FileLinksCategoryCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<I1c_lims_ApplicationEAdmissions>()
                .Property(e => e.QuntityVolume)
                .HasPrecision(18, 6);

            modelBuilder.Entity<I1c_lims_ApplicationEAdmissions>()
                .Property(e => e.QuntityVolumeConvert)
                .HasPrecision(18, 6);

            modelBuilder.Entity<I1c_lims_ApplicationEExploitation>()
                .Property(e => e.QuntityVolume)
                .HasPrecision(18, 6);

            modelBuilder.Entity<I1c_lims_ApplicationEExploitation>()
                .Property(e => e.QuntityVolumeConvert)
                .HasPrecision(18, 6);

            modelBuilder.Entity<I1c_lims_ApplicationElements>()
                .Property(e => e.QuntityVolume)
                .HasPrecision(18, 6);

            modelBuilder.Entity<I1c_lims_ContractProducts>()
                .Property(e => e.QuantityVolume)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsEquipment>()
                .HasMany(e => e.LimsApplicationJournals)
                .WithOptional(e => e.LimsEquipment)
                .HasForeignKey(e => e.EquipmentId);

            modelBuilder.Entity<LimsEquipment>()
                .HasMany(e => e.LimsEquipmentActs)
                .WithRequired(e => e.LimsEquipment)
                .HasForeignKey(e => e.EquipmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LimsEquipment>()
                .HasMany(e => e.LimsEquipmentJournalRecords)
                .WithRequired(e => e.LimsEquipment)
                .HasForeignKey(e => e.EquipmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LimsEquipment>()
                .HasMany(e => e.LimsPlanEquipmentLinks)
                .WithRequired(e => e.LimsEquipment)
                .HasForeignKey(e => e.EquipmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LimsEquipmentAct>()
                .HasMany(e => e.LimsEquipmentActSpareParts)
                .WithOptional(e => e.LimsEquipmentAct)
                .HasForeignKey(e => e.EquipmentActId);

            modelBuilder.Entity<LimsEquipmentJournal>()
                .HasMany(e => e.LimsEquipmentJournalRecords)
                .WithOptional(e => e.LimsEquipmentJournal)
                .HasForeignKey(e => e.JournalId);

            modelBuilder.Entity<LimsEquipmentPlan>()
                .HasMany(e => e.LimsPlanEquipmentLinks)
                .WithRequired(e => e.LimsEquipmentPlan)
                .HasForeignKey(e => e.EquipmentPlanId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LimsTmcTemp>()
                .Property(e => e.CountRequest)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcTemp>()
                .Property(e => e.CountReceived)
                .HasPrecision(18, 6);

            modelBuilder.Entity<OBK_ActReception>()
                .HasMany(e => e.OBK_Tasks)
                .WithRequired(e => e.OBK_ActReception)
                .HasForeignKey(e => e.ActReceptionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<obk_appendix>()
                .Property(e => e.appendix_number)
                .IsUnicode(false);

            modelBuilder.Entity<obk_appendix>()
                .HasMany(e => e.obk_appendix_series)
                .WithRequired(e => e.obk_appendix)
                .HasForeignKey(e => e.appendix_id);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_AssessmentDeclaration__OBK_ExpertCouncil)
                .WithOptional(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.DeclarationId);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasOptional(e => e.OBK_AssessmentDeclaration1)
                .WithRequired(e => e.OBK_AssessmentDeclaration2);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_AssessmentDeclarationCom)
                .WithRequired(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.AssessmentDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_AssessmentDeclarationFieldHistory)
                .WithOptional(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.AssessmentDeclarationId);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_AssessmentDeclarationHistory)
                .WithRequired(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.AssessmentDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_AssessmentStage)
                .WithRequired(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.DeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_AssessmentStageOP)
                .WithOptional(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.DeclarationId);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_CertificateOfCompletion)
                .WithOptional(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.AssessmentDeclarationId);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_OP_Commission)
                .WithRequired(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.DeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_StageExpDocument)
                .WithOptional(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.AssessmentDeclarationId);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_StageExpDocumentResult)
                .WithRequired(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.AssessmetDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_AssessmentDeclaration>()
                .HasMany(e => e.OBK_Tasks)
                .WithRequired(e => e.OBK_AssessmentDeclaration)
                .HasForeignKey(e => e.AssessmentDeclarationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_AssessmentDeclarationCom>()
                .HasMany(e => e.OBK_AssessmentDeclarationComRecord)
                .WithRequired(e => e.OBK_AssessmentDeclarationCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_AssessmentStage>()
                .HasMany(e => e.OBK_AssessmentStageExecutors)
                .WithRequired(e => e.OBK_AssessmentStage)
                .HasForeignKey(e => e.AssessmentStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_AssessmentStage>()
                .HasMany(e => e.OBK_AssessmentStageSignData)
                .WithRequired(e => e.OBK_AssessmentStage)
                .HasForeignKey(e => e.AssessmentStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_BlankNumber>()
                .HasMany(e => e.OBK_BlankNumber1)
                .WithOptional(e => e.OBK_BlankNumber2)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<OBK_BlankType>()
                .HasMany(e => e.OBK_BlankNumber)
                .WithOptional(e => e.OBK_BlankType)
                .HasForeignKey(e => e.BlankTypeId);

            modelBuilder.Entity<OBK_CertificateValidityType>()
                .HasMany(e => e.OBK_CertificateReference)
                .WithOptional(e => e.OBK_CertificateValidityType)
                .HasForeignKey(e => e.CertificateValidityTypeId);

            modelBuilder.Entity<obk_certification_copies>()
                .Property(e => e.blank_begin_number)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certification_copies>()
                .Property(e => e.blank_end_number)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certification_copies>()
                .Property(e => e.certificate_reg_number)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certification_copies>()
                .Property(e => e.certificate_blank_number)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certification_copies>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certifications>()
                .Property(e => e.reg_number)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certifications>()
                .Property(e => e.blank_number)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certifications>()
                .Property(e => e.product_name)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certifications>()
                .Property(e => e.add_info)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certifications>()
                .Property(e => e.annulment_reason)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certifications>()
                .Property(e => e.refuse_reason)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certifications>()
                .Property(e => e.argument)
                .IsUnicode(false);

            modelBuilder.Entity<obk_certifications>()
                .HasMany(e => e.obk_appendix)
                .WithRequired(e => e.obk_certifications)
                .HasForeignKey(e => e.certification_id);

            modelBuilder.Entity<obk_certifications>()
                .HasMany(e => e.obk_certification_copies)
                .WithRequired(e => e.obk_certifications)
                .HasForeignKey(e => e.certification_id);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_AssessmentDeclaration)
                .WithOptional(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_CertificateOfCompletion)
                .WithOptional(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId);

            modelBuilder.Entity<OBK_Contract>()
                .HasOptional(e => e.OBK_Contract1)
                .WithRequired(e => e.OBK_Contract2);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_ContractCom)
                .WithRequired(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_ContractExtHistory)
                .WithRequired(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_ContractFactory)
                .WithRequired(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_ContractPrice)
                .WithRequired(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Contract>()
                .HasOptional(e => e.OBK_ContractSignedDatas)
                .WithRequired(e => e.OBK_Contract);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_ContractStage)
                .WithRequired(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_DirectionToPayments)
                .WithRequired(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_LetterPortalEdo)
                .WithOptional(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId);

            modelBuilder.Entity<OBK_Contract>()
                .HasMany(e => e.OBK_RS_Products)
                .WithOptional(e => e.OBK_Contract)
                .HasForeignKey(e => e.ContractId);

            modelBuilder.Entity<OBK_ContractCom>()
                .HasMany(e => e.OBK_ContractComRecord)
                .WithRequired(e => e.OBK_ContractCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_ContractFactory>()
                .HasMany(e => e.OBK_ContractFactoryCom)
                .WithRequired(e => e.OBK_ContractFactory)
                .HasForeignKey(e => e.ContractFactoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_ContractFactoryCom>()
                .HasMany(e => e.OBK_ContractFactoryComRecord)
                .WithRequired(e => e.OBK_ContractFactoryCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_ContractPrice>()
                .HasMany(e => e.OBK_ContractPriceCom)
                .WithRequired(e => e.OBK_ContractPrice)
                .HasForeignKey(e => e.ContractPriceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_ContractPriceCom>()
                .HasMany(e => e.OBK_ContractPriceComRecord)
                .WithRequired(e => e.OBK_ContractPriceCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_ContractStage>()
                .HasMany(e => e.OBK_ContractStage1)
                .WithOptional(e => e.OBK_ContractStage2)
                .HasForeignKey(e => e.ParentStageId);

            modelBuilder.Entity<OBK_ContractStage>()
                .HasMany(e => e.OBK_ContractStageExecutors)
                .WithRequired(e => e.OBK_ContractStage)
                .HasForeignKey(e => e.ContractStageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<obk_currencies>()
                .Property(e => e.currency_name)
                .IsUnicode(false);

            modelBuilder.Entity<obk_currencies>()
                .Property(e => e.short_name)
                .IsUnicode(false);

            modelBuilder.Entity<obk_currencies>()
                .Property(e => e.currency_code)
                .IsUnicode(false);

            modelBuilder.Entity<obk_currencies>()
                .Property(e => e.abbreviation)
                .IsUnicode(false);

            modelBuilder.Entity<obk_currencies>()
                .HasMany(e => e.obk_exchangerate)
                .WithRequired(e => e.obk_currencies)
                .HasForeignKey(e => e.currency_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Declarant>()
                .HasMany(e => e.EMP_Contract)
                .WithOptional(e => e.OBK_Declarant)
                .HasForeignKey(e => e.DeclarantId);

            modelBuilder.Entity<OBK_Declarant>()
                .HasMany(e => e.EMP_Contract1)
                .WithOptional(e => e.OBK_DeclarantManufactur)
                .HasForeignKey(e => e.ManufacturId);

            modelBuilder.Entity<OBK_Declarant>()
                .HasMany(e => e.EMP_Contract2)
                .WithOptional(e => e.OBK_DeclarantPayer)
                .HasForeignKey(e => e.PayerId);

            modelBuilder.Entity<OBK_Declarant>()
                .HasMany(e => e.EMP_DirectionToPayments)
                .WithOptional(e => e.OBK_Declarant)
                .HasForeignKey(e => e.PayerId);

            modelBuilder.Entity<OBK_Declarant>()
                .HasMany(e => e.OBK_Contract)
                .WithOptional(e => e.OBK_Declarant)
                .HasForeignKey(e => e.DeclarantId);

            modelBuilder.Entity<OBK_Declarant>()
                .HasMany(e => e.OBK_DeclarantContact)
                .WithOptional(e => e.OBK_Declarant)
                .HasForeignKey(e => e.DeclarantId);

            modelBuilder.Entity<OBK_Declarant>()
                .HasMany(e => e.OBK_DirectionToPayments)
                .WithOptional(e => e.OBK_Declarant)
                .HasForeignKey(e => e.PayerId);

            modelBuilder.Entity<OBK_DeclarantContact>()
                .HasMany(e => e.EMP_Contract)
                .WithOptional(e => e.OBK_DeclarantContact)
                .HasForeignKey(e => e.DeclarantContactId);

            modelBuilder.Entity<OBK_DeclarantContact>()
                .HasMany(e => e.EMP_Contract1)
                .WithOptional(e => e.OBK_DeclarantContactManufactur)
                .HasForeignKey(e => e.ManufacturContactId);

            modelBuilder.Entity<OBK_DeclarantContact>()
                .HasMany(e => e.EMP_Contract2)
                .WithOptional(e => e.OBK_DeclarantContactPayer)
                .HasForeignKey(e => e.PayerContactId);

            modelBuilder.Entity<OBK_DeclarantContact>()
                .HasMany(e => e.OBK_Contract)
                .WithOptional(e => e.OBK_DeclarantContact)
                .HasForeignKey(e => e.DeclarantContactId);

            modelBuilder.Entity<OBK_Dictionaries>()
                .HasMany(e => e.OBK_ActReception)
                .WithOptional(e => e.OBK_Dictionaries)
                .HasForeignKey(e => e.InspectionInstalledId);

            modelBuilder.Entity<OBK_Dictionaries>()
                .HasMany(e => e.OBK_ActReception1)
                .WithOptional(e => e.OBK_Dictionaries1)
                .HasForeignKey(e => e.MarkingId);

            modelBuilder.Entity<OBK_Dictionaries>()
                .HasMany(e => e.OBK_ActReception2)
                .WithOptional(e => e.OBK_Dictionaries2)
                .HasForeignKey(e => e.PackageConditionId);

            modelBuilder.Entity<OBK_Dictionaries>()
                .HasMany(e => e.OBK_ActReception3)
                .WithOptional(e => e.OBK_Dictionaries3)
                .HasForeignKey(e => e.StorageConditionsId);

            modelBuilder.Entity<OBK_DirectionToPayments>()
                .HasOptional(e => e.OBK_DirectionSignData)
                .WithRequired(e => e.OBK_DirectionToPayments);

            modelBuilder.Entity<obk_exchangerate>()
                .Property(e => e.rate)
                .HasPrecision(18, 3);

            //modelBuilder.Entity<OBK_ExpertCouncil>()
            //    .HasMany(e => e.OBK_AssessmentDeclaration__OBK_ExpertCouncil)
            //    .WithOptional(e => e.OBK_ExpertCouncil)
            //    .HasForeignKey(e => e.ExpertCouncilId);

            modelBuilder.Entity<OBK_LetterPortalEdo>()
                .HasMany(e => e.OBK_LetterFromEdo)
                .WithOptional(e => e.OBK_LetterPortalEdo)
                .HasForeignKey(e => e.LetterPortalEdoID);

            modelBuilder.Entity<OBK_LetterRegistration>()
                .HasMany(e => e.OBK_LetterPortalEdo)
                .WithOptional(e => e.OBK_LetterRegistration)
                .HasForeignKey(e => e.OBKLetterRegID);

            modelBuilder.Entity<OBK_MtPart>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_MtPart>()
                .Property(e => e.Specification)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_MtPart>()
                .Property(e => e.ProducerName)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_MtPart>()
                .Property(e => e.CountryName)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_OP_CommissionRoles>()
                .HasMany(e => e.OBK_OP_Commission)
                .WithRequired(e => e.OBK_OP_CommissionRoles)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Procunts_Series>()
                .HasMany(e => e.OBK_Products_SeriesCom)
                .WithRequired(e => e.OBK_Procunts_Series)
                .HasForeignKey(e => e.ProductSerieId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Procunts_Series>()
                .HasMany(e => e.OBK_StageExpDocument)
                .WithOptional(e => e.OBK_Procunts_Series)
                .HasForeignKey(e => e.ProductSeriesId);

            modelBuilder.Entity<OBK_Procunts_Series>()
                .HasMany(e => e.OBK_TaskMaterial)
                .WithRequired(e => e.OBK_Procunts_Series)
                .HasForeignKey(e => e.ProductSeriesId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<obk_product_cost>()
                .Property(e => e.cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.producer_name)
                .IsUnicode(false);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.country_name)
                .IsUnicode(false);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.party_count)
                .HasPrecision(19, 4);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.tnved_code)
                .IsUnicode(false);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.kpved_code)
                .IsUnicode(false);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.register_nd)
                .IsUnicode(false);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.nd)
                .IsUnicode(false);

            modelBuilder.Entity<obk_products>()
                .Property(e => e.reg_number)
                .IsUnicode(false);

            modelBuilder.Entity<obk_products>()
                .HasMany(e => e.obk_appendix_series)
                .WithRequired(e => e.obk_products)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<obk_products>()
                .HasMany(e => e.obk_certifications)
                .WithRequired(e => e.obk_products)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<obk_products>()
                .HasOptional(e => e.obk_product_cost)
                .WithRequired(e => e.obk_products)
                .WillCascadeOnDelete();

            modelBuilder.Entity<OBK_Products_SeriesCom>()
                .HasMany(e => e.OBK_Products_SeriesComRecord)
                .WithRequired(e => e.OBK_Products_SeriesCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_CertificateType>()
                .HasMany(e => e.OBK_AssessmentDeclaration)
                .WithOptional(e => e.OBK_Ref_CertificateType)
                .HasForeignKey(e => e.CertificateTypeId);

            modelBuilder.Entity<OBK_Ref_ContractExtHistoryStatus>()
                .HasMany(e => e.OBK_ContractExtHistory)
                .WithRequired(e => e.OBK_Ref_ContractExtHistoryStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_ContractHistoryStatus>()
                .HasMany(e => e.EMP_ContractHistory)
                .WithRequired(e => e.OBK_Ref_ContractHistoryStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_ContractHistoryStatus>()
                .HasMany(e => e.OBK_ContractHistory)
                .WithRequired(e => e.OBK_Ref_ContractHistoryStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_DegreeRisk>()
                .HasMany(e => e.OBK_Ref_PriceList)
                .WithOptional(e => e.OBK_Ref_DegreeRisk)
                .HasForeignKey(e => e.DegreeRiskId);

            modelBuilder.Entity<OBK_Ref_LaboratoryMark>()
                .HasMany(e => e.OBK_Ref_LaboratoryRegulation)
                .WithRequired(e => e.OBK_Ref_LaboratoryMark)
                .HasForeignKey(e => e.LaboratoryMarkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_LaboratoryMark>()
                .HasMany(e => e.OBK_ResearchCenterResult)
                .WithRequired(e => e.OBK_Ref_LaboratoryMark)
                .HasForeignKey(e => e.LaboratoryMarkId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_LaboratoryRegulation>()
                .HasMany(e => e.OBK_ResearchCenterResult)
                .WithOptional(e => e.OBK_Ref_LaboratoryRegulation)
                .HasForeignKey(e => e.LaboratoryRegulationId);

            modelBuilder.Entity<OBK_Ref_LaboratoryType>()
                .HasMany(e => e.OBK_TaskMaterial)
                .WithRequired(e => e.OBK_Ref_LaboratoryType)
                .HasForeignKey(e => e.LaboratoryTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_MaterialCondition>()
                .HasMany(e => e.OBK_TaskMaterial)
                .WithOptional(e => e.OBK_Ref_MaterialCondition)
                .HasForeignKey(e => e.StorageConditionId);

            modelBuilder.Entity<OBK_Ref_MaterialCondition>()
                .HasMany(e => e.OBK_TaskMaterial1)
                .WithOptional(e => e.OBK_Ref_MaterialCondition1)
                .HasForeignKey(e => e.ExternalConditionId);

            modelBuilder.Entity<OBK_Ref_PaymentStatus>()
                .HasMany(e => e.EMP_DirectionToPayments)
                .WithRequired(e => e.OBK_Ref_PaymentStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_PriceList>()
                .HasMany(e => e.OBK_ContractPrice)
                .WithRequired(e => e.OBK_Ref_PriceList)
                .HasForeignKey(e => e.PriceRefId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Reason>()
                .HasMany(e => e.OBK_StageExpDocument)
                .WithOptional(e => e.OBK_Ref_Reason)
                .HasForeignKey(e => e.RefReasonId);

            modelBuilder.Entity<OBK_Ref_ServiceType>()
                .HasMany(e => e.OBK_Ref_PriceList)
                .WithRequired(e => e.OBK_Ref_ServiceType)
                .HasForeignKey(e => e.ServiceTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Stage>()
                .HasMany(e => e.OBK_AssessmentStage)
                .WithRequired(e => e.OBK_Ref_Stage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Stage>()
                .HasMany(e => e.OBK_ContractStage)
                .WithRequired(e => e.OBK_Ref_Stage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Stage>()
                .HasMany(e => e.OBK_ZBKCopyStage)
                .WithRequired(e => e.OBK_Ref_Stage)
                .HasForeignKey(e => e.StageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_StageStatus>()
                .HasMany(e => e.OBK_AssessmentStage)
                .WithRequired(e => e.OBK_Ref_StageStatus)
                .HasForeignKey(e => e.StageStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_StageStatus>()
                .HasMany(e => e.OBK_AssessmentStageOP)
                .WithOptional(e => e.OBK_Ref_StageStatus)
                .HasForeignKey(e => e.StageStatus);

            modelBuilder.Entity<OBK_Ref_StageStatus>()
                .HasMany(e => e.OBK_ContractStage)
                .WithRequired(e => e.OBK_Ref_StageStatus)
                .HasForeignKey(e => e.StageStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_StageStatus>()
                .HasMany(e => e.OBK_TaskMaterial)
                .WithOptional(e => e.OBK_Ref_StageStatus)
                .HasForeignKey(e => e.StatusId);

            modelBuilder.Entity<OBK_Ref_StageStatus>()
                .HasMany(e => e.OBK_Tasks)
                .WithOptional(e => e.OBK_Ref_StageStatus)
                .HasForeignKey(e => e.TaskStatusId);

            modelBuilder.Entity<OBK_Ref_StageStatus>()
                .HasMany(e => e.OBK_TaskStatus)
                .WithRequired(e => e.OBK_Ref_StageStatus)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_StageStatus>()
                .HasMany(e => e.OBK_ZBKCopyStage)
                .WithRequired(e => e.OBK_Ref_StageStatus)
                .HasForeignKey(e => e.StageStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Status>()
                .HasMany(e => e.OBK_AssessmentDeclarationHistory)
                .WithRequired(e => e.OBK_Ref_Status)
                .HasForeignKey(e => e.StatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Status>()
                .HasMany(e => e.OBK_Contract)
                .WithRequired(e => e.OBK_Ref_Status)
                .HasForeignKey(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Type>()
                .HasMany(e => e.OBK_AssessmentDeclaration)
                .WithRequired(e => e.OBK_Ref_Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Type>()
                .HasMany(e => e.OBK_Contract)
                .WithRequired(e => e.OBK_Ref_Type)
                .HasForeignKey(e => e.Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Ref_Type>()
                .HasMany(e => e.OBK_Ref_PriceList)
                .WithRequired(e => e.OBK_Ref_Type)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_RS_Products>()
                .HasMany(e => e.OBK_ContractPrice)
                .WithOptional(e => e.OBK_RS_Products)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<OBK_RS_Products>()
                .HasMany(e => e.OBK_MtPart)
                .WithRequired(e => e.OBK_RS_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_RS_Products>()
                .HasMany(e => e.OBK_Procunts_Series)
                .WithRequired(e => e.OBK_RS_Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_RS_Products>()
                .HasMany(e => e.OBK_RS_ProductsCom)
                .WithRequired(e => e.OBK_RS_Products)
                .HasForeignKey(e => e.ProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_RS_Products>()
                .HasMany(e => e.OBK_StageExpDocument)
                .WithOptional(e => e.OBK_RS_Products)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<OBK_RS_ProductsCom>()
                .HasMany(e => e.OBK_RS_ProductsComRecord)
                .WithRequired(e => e.OBK_RS_ProductsCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_TaskMaterial>()
                .HasMany(e => e.OBK_ResearchCenterResult)
                .WithRequired(e => e.OBK_TaskMaterial)
                .HasForeignKey(e => e.TaskMaterialId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_TaskMaterial>()
                .HasMany(e => e.OBK_TaskExecutor)
                .WithOptional(e => e.OBK_TaskMaterial)
                .HasForeignKey(e => e.TaskMaterialId);

            modelBuilder.Entity<OBK_Tasks>()
                .HasMany(e => e.OBK_TaskExecutor)
                .WithRequired(e => e.OBK_Tasks)
                .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Tasks>()
                .HasMany(e => e.OBK_TaskMaterial)
                .WithRequired(e => e.OBK_Tasks)
                .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_Tasks>()
                .HasMany(e => e.OBK_TaskStatus)
                .WithRequired(e => e.OBK_Tasks)
                .HasForeignKey(e => e.TaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_ZBKCopy>()
                .HasMany(e => e.OBK_ZBKCopyBlank)
                .WithOptional(e => e.OBK_ZBKCopy)
                .HasForeignKey(e => e.ZBKCopyId);

            modelBuilder.Entity<OBK_ZBKCopy>()
                .HasMany(e => e.OBK_ZBKCopySignData)
                .WithRequired(e => e.OBK_ZBKCopy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OBK_ZBKCopyStage>()
                .HasOptional(e => e.OBK_ZBKCopyStageExecutors)
                .WithRequired(e => e.OBK_ZBKCopyStage);

            modelBuilder.Entity<OBK_ZBKCopyStage>()
                .HasMany(e => e.OBK_ZBKCopyStageSignData)
                .WithOptional(e => e.OBK_ZBKCopyStage)
                .HasForeignKey(e => e.StageId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Contracts)
                .WithOptional(e => e.ApplicantOrganization)
                .HasForeignKey(e => e.ApplicantOrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Contracts1)
                .WithOptional(e => e.HolderOrganization)
                .HasForeignKey(e => e.HolderOrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Contracts2)
                .WithOptional(e => e.PayerOrganization)
                .HasForeignKey(e => e.PayerOrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Contracts3)
                .WithOptional(e => e.PayerTranslationOrganization)
                .HasForeignKey(e => e.PayerTranslationOrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Contracts4)
                .WithOptional(e => e.AgentOrganization)
                .HasForeignKey(e => e.AgentOrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Contracts5)
                .WithOptional(e => e.ManufacturerOrganization)
                .HasForeignKey(e => e.ManufacturerOrganizationId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.EXP_DirectionToPays)
                .WithOptional(e => e.Organization)
                .HasForeignKey(e => e.PayerId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.EXP_Materials)
                .WithOptional(e => e.Producer)
                .HasForeignKey(e => e.ProducerId);

            modelBuilder.Entity<Organization>()
                .HasMany(e => e.Organizations1)
                .WithOptional(e => e.Organization1)
                .HasForeignKey(e => e.OriginalOrgId);

            modelBuilder.Entity<PP_Protocols>()
                .HasMany(e => e.PP_ProtocolComissionMembers)
                .WithRequired(e => e.PP_Protocols)
                .HasForeignKey(e => e.ProtocolId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PP_Protocols>()
                .HasMany(e => e.PP_ProtocolProductPrices)
                .WithRequired(e => e.PP_Protocols)
                .HasForeignKey(e => e.ProtocolId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceProjectCom>()
                .HasMany(e => e.PriceProjectComRecords)
                .WithRequired(e => e.PriceProjectCom)
                .HasForeignKey(e => e.CommentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceProject>()
                .Property(e => e.Volume)
                .IsUnicode(false);

            modelBuilder.Entity<PriceProject>()
                .HasMany(e => e.PP_AttachRemarks)
                .WithRequired(e => e.PriceProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceProject>()
                .HasMany(e => e.PriceProjectComs)
                .WithRequired(e => e.PriceProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceProject>()
                .HasMany(e => e.PriceProjectFieldHistories)
                .WithRequired(e => e.PriceProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PriceProject>()
                .HasMany(e => e.PriceProjectsHistories)
                .WithRequired(e => e.PriceProject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RemarkType>()
                .HasMany(e => e.Documents)
                .WithOptional(e => e.RemarkType)
                .HasForeignKey(e => e.RemarkId);

            modelBuilder.Entity<SignDocument>()
                .Property(e => e.ObjectId)
                .IsUnicode(false);

            modelBuilder.Entity<sr_atc_codes>()
                .Property(e => e.code)
                .IsUnicode(false);

            modelBuilder.Entity<sr_atc_codes>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.sr_atc_codes)
                .HasForeignKey(e => e.AtxId);

            modelBuilder.Entity<sr_atc_codes>()
                .HasMany(e => e.sr_atc_codes1)
                .WithOptional(e => e.sr_atc_codes2)
                .HasForeignKey(e => e.parent_id);

            modelBuilder.Entity<sr_atc_codes>()
                .HasMany(e => e.sr_register_drugs)
                .WithRequired(e => e.sr_atc_codes)
                .HasForeignKey(e => e.atc_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_boxes>()
                .HasMany(e => e.EXP_DrugWrapping)
                .WithOptional(e => e.sr_boxes)
                .HasForeignKey(e => e.WrappingKindId);

            modelBuilder.Entity<sr_boxes>()
                .HasMany(e => e.sr_boxes1)
                .WithOptional(e => e.sr_boxes2)
                .HasForeignKey(e => e.parent_id);

            modelBuilder.Entity<sr_boxes>()
                .HasMany(e => e.sr_register_boxes)
                .WithRequired(e => e.sr_boxes)
                .HasForeignKey(e => e.box_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_categories>()
                .HasMany(e => e.sr_register_drugs)
                .WithOptional(e => e.sr_categories)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<sr_categories>()
                .HasMany(e => e.sr_substances)
                .WithOptional(e => e.sr_categories)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<sr_countries>()
                .Property(e => e.name_en)
                .IsUnicode(false);

            modelBuilder.Entity<sr_countries>()
                .Property(e => e.short_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<sr_countries>()
                .HasMany(e => e.EXP_DrugExportTrade)
                .WithOptional(e => e.sr_countries)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<sr_countries>()
                .HasMany(e => e.EXP_DrugOtherCountry)
                .WithOptional(e => e.sr_countries)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<sr_countries>()
                .HasMany(e => e.EXP_DrugSubstance)
                .WithOptional(e => e.sr_countries)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<sr_countries>()
                .HasMany(e => e.EXP_DrugSubstanceManufacture)
                .WithOptional(e => e.sr_countries)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<sr_countries>()
                .HasMany(e => e.sr_register_names)
                .WithRequired(e => e.sr_countries)
                .HasForeignKey(e => e.country_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_countries>()
                .HasMany(e => e.sr_register_producers)
                .WithRequired(e => e.sr_countries)
                .HasForeignKey(e => e.country_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_degree_risk_details>()
                .HasMany(e => e.sr_register_mt)
                .WithRequired(e => e.sr_degree_risk_details)
                .HasForeignKey(e => e.risk_detail_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_degree_risks>()
                .HasMany(e => e.sr_degree_risk_details)
                .WithRequired(e => e.sr_degree_risks)
                .HasForeignKey(e => e.degree_risk_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_degree_risks>()
                .HasMany(e => e.sr_register_mt)
                .WithRequired(e => e.sr_degree_risks)
                .HasForeignKey(e => e.degree_risk_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_dosage_forms>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.sr_dosage_forms)
                .HasForeignKey(e => e.DrugFormId);

            modelBuilder.Entity<sr_dosage_forms>()
                .HasMany(e => e.EXP_Materials)
                .WithOptional(e => e.sr_dosage_forms)
                .HasForeignKey(e => e.DrugFormId);

            modelBuilder.Entity<sr_dosage_forms>()
                .HasMany(e => e.sr_dosage_forms1)
                .WithOptional(e => e.sr_dosage_forms2)
                .HasForeignKey(e => e.parent_id);

            modelBuilder.Entity<sr_dosage_forms>()
                .HasMany(e => e.sr_register_drugs)
                .WithRequired(e => e.sr_dosage_forms)
                .HasForeignKey(e => e.dosage_form_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_drug_forms>()
                .Property(e => e.pr_box_count)
                .IsUnicode(false);

            modelBuilder.Entity<sr_drug_forms>()
                .Property(e => e.sec_box_count)
                .IsUnicode(false);

            modelBuilder.Entity<sr_drug_forms>()
                .Property(e => e.inter_box_count)
                .IsUnicode(false);

            modelBuilder.Entity<sr_drug_types>()
                .HasMany(e => e.sr_register_drugs)
                .WithRequired(e => e.sr_drug_types)
                .HasForeignKey(e => e.drug_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_form_types>()
                .HasMany(e => e.sr_producers)
                .WithOptional(e => e.sr_form_types)
                .HasForeignKey(e => e.form_type_id);

            modelBuilder.Entity<sr_instruction_types>()
                .HasMany(e => e.sr_register_instructions)
                .WithRequired(e => e.sr_instruction_types)
                .HasForeignKey(e => e.instruction_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_international_names>()
                .Property(e => e.name_eng)
                .IsUnicode(false);

            modelBuilder.Entity<sr_international_names>()
                .Property(e => e.name_lat)
                .IsUnicode(false);

            modelBuilder.Entity<sr_international_names>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.sr_international_names)
                .HasForeignKey(e => e.MnnId);

            modelBuilder.Entity<sr_international_names>()
                .HasMany(e => e.sr_register_drugs)
                .WithRequired(e => e.sr_international_names)
                .HasForeignKey(e => e.int_name_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_life_types>()
                .HasMany(e => e.sr_register_drugs)
                .WithRequired(e => e.sr_life_types)
                .HasForeignKey(e => e.life_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.sr_measures)
                .HasForeignKey(e => e.DosageMeasureTypeId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugDeclaration1)
                .WithOptional(e => e.sr_measures1)
                .HasForeignKey(e => e.BestBeforeMeasureTypeDicId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugDeclaration2)
                .WithOptional(e => e.sr_measures2)
                .HasForeignKey(e => e.AppPeriodMixMeasureDicId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugDeclaration3)
                .WithOptional(e => e.sr_measures3)
                .HasForeignKey(e => e.AppPeriodOpenMeasureDicId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugDosage)
                .WithOptional(e => e.sr_measures)
                .HasForeignKey(e => e.AppPeriodMixMeasureDicId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugDosage1)
                .WithOptional(e => e.sr_measures1)
                .HasForeignKey(e => e.AppPeriodOpenMeasureDicId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugDosage2)
                .WithOptional(e => e.sr_measures2)
                .HasForeignKey(e => e.BestBeforeMeasureTypeDicId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugDosage3)
                .WithOptional(e => e.sr_measures3)
                .HasForeignKey(e => e.DosageMeasureTypeId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugSubstance)
                .WithOptional(e => e.sr_measures)
                .HasForeignKey(e => e.MeasureId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugWrapping)
                .WithOptional(e => e.sr_measures)
                .HasForeignKey(e => e.SizeMeasureId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.EXP_DrugWrapping1)
                .WithOptional(e => e.sr_measures1)
                .HasForeignKey(e => e.VolumeMeasureId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.OBK_Procunts_Series)
                .WithOptional(e => e.sr_measures)
                .HasForeignKey(e => e.SeriesMeasureId);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.sr_register_drugs)
                .WithOptional(e => e.sr_measures)
                .HasForeignKey(e => e.dosage_measure_id);

            modelBuilder.Entity<sr_measures>()
                .HasMany(e => e.sr_register)
                .WithOptional(e => e.sr_measures)
                .HasForeignKey(e => e.storage_measure_id);

            modelBuilder.Entity<sr_mt_categories>()
                .HasMany(e => e.sr_mt_categories1)
                .WithOptional(e => e.sr_mt_categories2)
                .HasForeignKey(e => e.parent_id);

            modelBuilder.Entity<sr_mt_categories>()
                .HasMany(e => e.sr_register_mt)
                .WithRequired(e => e.sr_mt_categories)
                .HasForeignKey(e => e.mt_category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_nd_names>()
                .HasMany(e => e.sr_register_drugs)
                .WithOptional(e => e.sr_nd_names)
                .HasForeignKey(e => e.nd_name_id);

            modelBuilder.Entity<sr_nd_types>()
                .HasMany(e => e.sr_register_substances)
                .WithOptional(e => e.sr_nd_types)
                .HasForeignKey(e => e.nd_type_id);

            modelBuilder.Entity<sr_pharmacological_actions>()
                .HasMany(e => e.sr_pharmacological_actions1)
                .WithOptional(e => e.sr_pharmacological_actions2)
                .HasForeignKey(e => e.parent_id);

            modelBuilder.Entity<sr_pharmacological_actions>()
                .HasMany(e => e.sr_register_pharmacological_actions)
                .WithRequired(e => e.sr_pharmacological_actions)
                .HasForeignKey(e => e.pharmacological_action_id);

            modelBuilder.Entity<sr_producer_types>()
                .HasMany(e => e.sr_register_producers)
                .WithRequired(e => e.sr_producer_types)
                .HasForeignKey(e => e.producer_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_producers>()
                .Property(e => e.name_eng)
                .IsUnicode(false);

            modelBuilder.Entity<sr_producers>()
                .Property(e => e.rnn)
                .IsUnicode(false);

            modelBuilder.Entity<sr_producers>()
                .Property(e => e.bin)
                .IsUnicode(false);

            modelBuilder.Entity<sr_producers>()
                .Property(e => e.iin)
                .IsUnicode(false);

            modelBuilder.Entity<sr_producers>()
                .HasMany(e => e.sr_register_mt_parts)
                .WithOptional(e => e.sr_producers)
                .HasForeignKey(e => e.producer_id);

            modelBuilder.Entity<sr_producers>()
                .HasMany(e => e.sr_register_producers)
                .WithRequired(e => e.sr_producers)
                .HasForeignKey(e => e.producer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_reg_actions>()
                .HasMany(e => e.sr_register)
                .WithRequired(e => e.sr_reg_actions)
                .HasForeignKey(e => e.reg_action_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_reg_types>()
                .HasMany(e => e.sr_register)
                .WithRequired(e => e.sr_reg_types)
                .HasForeignKey(e => e.reg_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register>()
                .Property(e => e.storage_term)
                .HasPrecision(6, 3);

            modelBuilder.Entity<sr_register>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register>()
                .HasMany(e => e.EXP_DrugDeclaration)
                .WithOptional(e => e.sr_register)
                .HasForeignKey(e => e.RegisterId);

            modelBuilder.Entity<sr_register>()
                .HasMany(e => e.EXP_DrugDosage)
                .WithOptional(e => e.sr_register)
                .HasForeignKey(e => e.RegisterId);

            modelBuilder.Entity<sr_register>()
                .HasMany(e => e.sr_drug_forms)
                .WithRequired(e => e.sr_register)
                .HasForeignKey(e => e.register_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register>()
                .HasMany(e => e.sr_register_boxes)
                .WithRequired(e => e.sr_register)
                .HasForeignKey(e => e.register_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register>()
                .HasOptional(e => e.sr_register_mt)
                .WithRequired(e => e.sr_register);

            modelBuilder.Entity<sr_register>()
                .HasMany(e => e.sr_register_names)
                .WithRequired(e => e.sr_register)
                .HasForeignKey(e => e.register_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register>()
                .HasMany(e => e.sr_register_producers)
                .WithRequired(e => e.sr_register)
                .HasForeignKey(e => e.register_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register>()
                .HasMany(e => e.sr_register_substances)
                .WithRequired(e => e.sr_register)
                .HasForeignKey(e => e.register_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register>()
                .HasMany(e => e.sr_register_instructions)
                .WithRequired(e => e.sr_register)
                .HasForeignKey(e => e.register_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register_boxes>()
                .Property(e => e.volume)
                .HasPrecision(9, 3);

            modelBuilder.Entity<sr_register_boxes>()
                .Property(e => e.box_size)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_boxes>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_boxes>()
                .HasMany(e => e.sr_drug_forms)
                .WithOptional(e => e.sr_register_boxes)
                .HasForeignKey(e => e.pr_box_id);

            modelBuilder.Entity<sr_register_boxes>()
                .HasMany(e => e.sr_drug_forms1)
                .WithOptional(e => e.sr_register_boxes1)
                .HasForeignKey(e => e.sec_box_id);

            modelBuilder.Entity<sr_register_boxes>()
                .HasOptional(e => e.sr_register_boxes_rk_ls)
                .WithRequired(e => e.sr_register_boxes);

            modelBuilder.Entity<sr_register_drugs>()
                .Property(e => e.C_atc_code)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_drugs>()
                .Property(e => e.dosage_value)
                .HasPrecision(20, 6);

            modelBuilder.Entity<sr_register_drugs>()
                .Property(e => e.concentration)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_drugs>()
                .Property(e => e.C_unit_count)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_drugs>()
                .HasMany(e => e.sr_register_pharmacological_actions)
                .WithRequired(e => e.sr_register_drugs)
                .HasForeignKey(e => e.register_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register_drugs>()
                .HasMany(e => e.sr_register_use_methods)
                .WithRequired(e => e.sr_register_drugs)
                .HasForeignKey(e => e.register_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_register_instructions>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_instructions>()
                .HasOptional(e => e.sr_register_instructions_files)
                .WithRequired(e => e.sr_register_instructions);

            modelBuilder.Entity<sr_register_mt>()
                .HasMany(e => e.sr_register_mt_parts)
                .WithRequired(e => e.sr_register_mt)
                .HasForeignKey(e => e.register_id);

            modelBuilder.Entity<sr_register_mt>()
                .HasOptional(e => e.sr_register_mt_kz)
                .WithRequired(e => e.sr_register_mt);

            modelBuilder.Entity<sr_register_names>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_names>()
                .Property(e => e.name_eng)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.law_number)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.law_description)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.registration_number)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.expired)
                .HasPrecision(19, 4);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.structure)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.reestr_drug_type_code)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.reestr_drug_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.ats_code)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.ats_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.MNN_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.PHARM_NAMES_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.PHARM_NAME_CATEGORY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.PHARM_FORM_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.PHARM_FORM_NAME_KAZ)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.FIRM_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.COUNTRY_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.PACKAGES_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.DOSES_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.DOSES_UNIT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.CONC_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.IN_CONTROL_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.color)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.REGISTRATION_TYPE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.VOF_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.VOF_UNIT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.country_type)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.reestr_text)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.pharm_form_union_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.package_union)
                .HasPrecision(10, 4);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.dose_union)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.conc_union)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.vof_union)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.primary_packing_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.secondary_packing_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.firm_owner_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.firm_packing_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.country_owner_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.country_packing_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.pkg_txt)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.license_owner_firm_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.license_owner_country_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_ordered>()
                .Property(e => e.change_type_name)
                .IsUnicode(false);

            modelBuilder.Entity<sr_register_substances>()
                .Property(e => e.substance_count)
                .HasPrecision(18, 8);

            modelBuilder.Entity<sr_register_substances>()
                .Property(e => e.comment)
                .IsUnicode(false);

            modelBuilder.Entity<sr_substance_types>()
                .HasMany(e => e.EXP_DrugSubstance)
                .WithOptional(e => e.sr_substance_types)
                .HasForeignKey(e => e.SubstanceTypeId);

            modelBuilder.Entity<sr_substance_types>()
                .HasMany(e => e.sr_register_substances)
                .WithRequired(e => e.sr_substance_types)
                .HasForeignKey(e => e.substance_type_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_substances>()
                .HasMany(e => e.EXP_DrugSubstance)
                .WithOptional(e => e.sr_substances)
                .HasForeignKey(e => e.SubstanceId);

            modelBuilder.Entity<sr_substances>()
                .HasMany(e => e.sr_register_substances)
                .WithRequired(e => e.sr_substances)
                .HasForeignKey(e => e.substance_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_use_methods>()
                .HasMany(e => e.EXP_DrugUseMethod)
                .WithRequired(e => e.sr_use_methods)
                .HasForeignKey(e => e.UseMethodsId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sr_use_methods>()
                .HasMany(e => e.sr_register_use_methods)
                .WithRequired(e => e.sr_use_methods)
                .HasForeignKey(e => e.use_method_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.AccessTasks)
                .WithRequired(e => e.Task)
                .HasForeignKey(e => e.ObjectId);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Reports)
                .WithOptional(e => e.Task)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TmcIn>()
                .HasMany(e => e.LimsTmcTemps)
                .WithRequired(e => e.TmcIn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TmcOff>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOutCount>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOutCount>()
                .Property(e => e.CountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.GeneralCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.GeneralCountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.GeneralConvertFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.IssuedCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.UseCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.UseFactCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.UseReasonText)
                .IsUnicode(false);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.ExpertiseStatementNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TmcReportData>()
                .Property(e => e.ExpertiseStatementTypeStr)
                .IsUnicode(false);

            modelBuilder.Entity<TmcReport>()
                .HasMany(e => e.TmcReportTasks)
                .WithRequired(e => e.TmcReport)
                .HasForeignKey(e => e.refTmcReport)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tmc>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Tmc>()
                .Property(e => e.CountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Tmc>()
                .Property(e => e.CountConvert)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Tmc>()
                .Property(e => e.CountActual)
                .HasPrecision(18, 6);

            modelBuilder.Entity<Tmc>()
                .HasMany(e => e.LimsTmcTemps)
                .WithRequired(e => e.Tmc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.DIC_Storages)
                .WithRequired(e => e.Unit)
                .HasForeignKey(e => e.OrganizationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Employees)
                .WithRequired(e => e.Organization)
                .HasForeignKey(e => e.OrganizationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Employees1)
                .WithOptional(e => e.Position)
                .HasForeignKey(e => e.PositionId);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.OBK_Contract)
                .WithOptional(e => e.Unit)
                .HasForeignKey(e => e.ExpertOrganization);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.OBK_TaskMaterial)
                .WithOptional(e => e.Unit)
                .HasForeignKey(e => e.UnitLaboratoryId);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.OBK_Tasks)
                .WithRequired(e => e.Unit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.OBK_TaskStatus)
                .WithOptional(e => e.Unit)
                .HasForeignKey(e => e.UnitLaboratoryId);

            modelBuilder.Entity<Unit>()
                .HasMany(e => e.Units1)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<UpdateScript>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<VisitStatus>()
                .HasMany(e => e.Visits)
                .WithRequired(e => e.VisitStatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VisitType>()
                .HasMany(e => e.VisitEmployeeTypes)
                .WithRequired(e => e.VisitType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VisitType>()
                .HasMany(e => e.Visits)
                .WithRequired(e => e.VisitType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sync_1c_I1c_act_Application_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_exp_Contracts_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_lims_ApplicationEAdmissions_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_lims_ApplicationEExploitation_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_lims_ApplicationElements_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_lims_Applications_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_lims_ContractProducts_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_lims_Contracts_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_primary_ApplicationElements_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_primary_ApplicationPartPaymentState_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_primary_ApplicationPaymentState_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_primary_ApplicationRefuseState_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_primary_Applications_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_primary_DrugPackage_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_primary_PriceListElements_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_trl_Application_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_trl_ApplicationPaymentState_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_I1c_trl_ApplicationRefuseState_tracking>()
                .Property(e => e.local_update_peer_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<sync_1c_scope_config>()
                .Property(e => e.scope_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<sync_1c_scope_info>()
                .Property(e => e.scope_timestamp)
                .IsFixedLength();

            modelBuilder.Entity<ArchivView>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<ArchivView>()
                .Property(e => e.DepartmentsValue)
                .IsUnicode(false);

            modelBuilder.Entity<AvgExchangeRatesView>()
                .Property(e => e.currency_code)
                .IsUnicode(false);

            modelBuilder.Entity<AvgExchangeRatesView>()
                .Property(e => e.rate)
                .HasPrecision(38, 6);

            modelBuilder.Entity<CategoriesView>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DirectionToPaysView>()
                .Property(e => e.DrugFormEn)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DirectionToPaysView>()
                .Property(e => e.ManufactureKz)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DirectionToPaysView>()
                .Property(e => e.ManufactureRu)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DirectionToPaysView>()
                .Property(e => e.ManufactureEn)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DirectionToPaysView>()
                .Property(e => e.ExpertDisplayName)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DirectionToPaysView>()
                .Property(e => e.Currency)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DirectionToPaysView>()
                .Property(e => e.PriceListValue)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DrugDeclarationPaysInfoView>()
                .Property(e => e.PaysDates)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DrugDeclarationRegisterView>()
                .Property(e => e.MnnRu)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DrugDeclarationRegisterView>()
                .Property(e => e.MnnEn)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_DrugDeclarationRegisterView>()
                .Property(e => e.DrugFormRu)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageForAddView>()
                .Property(e => e.DosageFormName)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageForAddView>()
                .Property(e => e.DeclarationPaysDates)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageForAddView>()
                .Property(e => e.DeclarationAtxName)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageForAddView>()
                .Property(e => e.DeclarationAtxCode)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageForAddView>()
                .Property(e => e.DeclarationMnnNameRu)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DosageDosageName)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DosageDosageShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DosageBestBeforeName)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DosageBestBeforeShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DosageFormName)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DeclarationPaysDates)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DeclarationAtxName)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DeclarationAtxCode)
                .IsUnicode(false);

            modelBuilder.Entity<Exp_DrugDosageStageView>()
                .Property(e => e.DeclarationMnnNameRu)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_MaterialDirectionsView>()
                .Property(e => e.DosageMeasureTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<EXP_MaterialDirectionsView>()
                .Property(e => e.DrugFormName)
                .IsUnicode(false);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.TmcCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.TmcCountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.TmcCountConvert)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.TmcCountActual)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.CountSum)
                .HasPrecision(38, 6);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.CountFactSum)
                .HasPrecision(38, 6);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.CountIssuedActual)
                .HasPrecision(38, 6);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.CountUseActual)
                .HasPrecision(38, 6);

            modelBuilder.Entity<LimsTmcActualView>()
                .Property(e => e.CountActual)
                .HasPrecision(38, 6);

            modelBuilder.Entity<LimsTmcOutView>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcOutView>()
                .Property(e => e.CountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcTempView>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcTempView>()
                .Property(e => e.CountRequest)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcTempView>()
                .Property(e => e.CountReceived)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcTempView>()
                .Property(e => e.CountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<LimsTmcTempView>()
                .Property(e => e.CountConvert)
                .HasPrecision(18, 6);

            modelBuilder.Entity<OBK_ContractReportView>()
                .Property(e => e.ПОдписано_ЭЦП)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_DeclarationCorrespondingView>()
                .Property(e => e.BlankNumber)
                .IsUnicode(false);

            modelBuilder.Entity<OBK_ZBKRegisterView>()
                .Property(e => e.BlankNumber)
                .IsUnicode(false);

            modelBuilder.Entity<OBKContractProductsView>()
                .Property(e => e.ProdShortName)
                .IsUnicode(false);

            modelBuilder.Entity<PriceProjectJournal>()
                .Property(e => e.ListTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<PriceProjectsView>()
                .Property(e => e.Volume)
                .IsUnicode(false);

            modelBuilder.Entity<PricesView>()
                .Property(e => e.PartsName)
                .IsUnicode(false);

            modelBuilder.Entity<ProjectDocument>()
                .Property(e => e.DocumentDictionaryTypeValue)
                .IsUnicode(false);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.producer_name)
                .IsUnicode(false);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.country_name)
                .IsUnicode(false);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.tnved_code)
                .IsUnicode(false);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.kpved_code)
                .IsUnicode(false);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.register_nd)
                .IsUnicode(false);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.currency_name)
                .IsUnicode(false);

            modelBuilder.Entity<ReesrtObk>()
                .Property(e => e.costExch)
                .HasPrecision(38, 7);

            modelBuilder.Entity<ReestrDirectionToPayView>()
                .Property(e => e.DrugFormRu)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.C_int_name)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.dosage_comment)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.C_dosage_form_name)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.concentration)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.reg_number)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.C_country_name)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.C_atc_code)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.box_name1)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrderer2Views>()
                .Property(e => e.box_name2)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.MnnName)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.Characteristic)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.DrugForm)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.Concentration)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.Dosage)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.TradeName)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.RegNumber)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.AtxCode)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.Manufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.Measure)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.substance_count)
                .HasPrecision(18, 8);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.volume)
                .HasPrecision(9, 3);

            modelBuilder.Entity<RegisterOrdererView>()
                .Property(e => e.dosage_comment)
                .IsUnicode(false);

            modelBuilder.Entity<SrProducerView>()
                .Property(e => e.NameEng)
                .IsUnicode(false);

            modelBuilder.Entity<SrProducerView>()
                .Property(e => e.FormTypeFullName)
                .IsUnicode(false);

            modelBuilder.Entity<SrProducerView>()
                .Property(e => e.FormTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.C_producer_name_en)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.C_country_name)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.reg_number)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.reg_action_name)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.C_int_name)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.C_dosage_form_name)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.concentration)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.C_atc_code)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.type_name)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.volume_measure)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.owner_name_en)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.box_name1)
                .IsUnicode(false);

            modelBuilder.Entity<SrReestrView>()
                .Property(e => e.box_name2)
                .IsUnicode(false);

            modelBuilder.Entity<TmcCountStateView>()
                .Property(e => e.CountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcCountStateView>()
                .Property(e => e.CountConvert)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcCountStateView>()
                .Property(e => e.IssuedCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcCountStateView>()
                .Property(e => e.UsedCount)
                .HasPrecision(38, 6);

            modelBuilder.Entity<TmcOffStateView>()
                .Property(e => e.UsedCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOffStateView>()
                .Property(e => e.RequstedCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOffStateView>()
                .Property(e => e.ReceivedCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOffStateView>()
                .Property(e => e.RequestedCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOffStateView>()
                .Property(e => e.CountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOffStateView>()
                .Property(e => e.ConvertedCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOffStateView>()
                .Property(e => e.ResidueCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOffView>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOffView>()
                .Property(e => e.StateTypeValue)
                .IsUnicode(false);

            modelBuilder.Entity<TmcOutCountView>()
                .Property(e => e.CountActual)
                .HasPrecision(38, 6);

            modelBuilder.Entity<TmcOutCountView>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOutCountView>()
                .Property(e => e.CountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOutCountView>()
                .Property(e => e.StateTypeValue)
                .IsUnicode(false);

            modelBuilder.Entity<TmcOutCountView>()
                .Property(e => e.TmcCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOutCountView>()
                .Property(e => e.TmcCountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcOutCountView>()
                .Property(e => e.CountConvert)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportDataSourceView>()
                .Property(e => e.GeneralCountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportDataSourceView>()
                .Property(e => e.GeneralConvertFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportDataSourceView>()
                .Property(e => e.IssuedCount)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcReportDataSourceView>()
                .Property(e => e.UsedCount)
                .HasPrecision(38, 6);

            modelBuilder.Entity<TmcReportDataSourceView>()
                .Property(e => e.UseFactCount)
                .HasPrecision(38, 6);

            modelBuilder.Entity<TmcUseOffView>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcUseOffView>()
                .Property(e => e.TmcCount)
                .HasPrecision(38, 6);

            modelBuilder.Entity<TmcView>()
                .Property(e => e.CountActual)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcView>()
                .Property(e => e.Count)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcView>()
                .Property(e => e.CountFact)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcView>()
                .Property(e => e.CountConvert)
                .HasPrecision(18, 6);

            modelBuilder.Entity<TmcView>()
                .Property(e => e.UsedCount)
                .HasPrecision(38, 6);
        }
    }
}
