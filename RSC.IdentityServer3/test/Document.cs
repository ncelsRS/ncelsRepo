namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            AccessDocuments = new HashSet<AccessDocument>();
            Activities = new HashSet<Activity>();
            Reports = new HashSet<Report>();
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAdministrativeUse { get; set; }

        public bool IsAwaitingResponse { get; set; }

        public bool IsTradeSecret { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? AwaitingResponseDate { get; set; }

        public DateTime? DocumentDate { get; set; }

        public DateTime? OutgoingDate { get; set; }

        public DateTime? ProtocolDate { get; set; }

        public DateTime? MonitoringDate { get; set; }

        public int ApplicantType { get; set; }

        public int DocumentType { get; set; }

        public int MonitoringType { get; set; }

        public int PriorityType { get; set; }

        public int StateType { get; set; }

        public int AppendixCount { get; set; }

        public int CopiesCount { get; set; }

        public int PageCount { get; set; }

        public int RepeatCount { get; set; }

        [StringLength(512)]
        public string ApplicantAddress { get; set; }

        [StringLength(4000)]
        public string ApplicantEmail { get; set; }

        [StringLength(512)]
        public string ApplicantName { get; set; }

        [StringLength(512)]
        public string ApplicantPhone { get; set; }

        [StringLength(512)]
        public string BlankNumber { get; set; }

        [StringLength(512)]
        public string CorrespondentsInfo { get; set; }

        [StringLength(512)]
        public string Number { get; set; }

        [StringLength(512)]
        public string OutgoingNumber { get; set; }

        [StringLength(512)]
        public string SortingNumber { get; set; }

        [StringLength(512)]
        public string SortingOutgoingNumber { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }

        [StringLength(4000)]
        public string Summary { get; set; }

        [StringLength(4000)]
        public string AdministrativeTypeDictionaryId { get; set; }

        [StringLength(4000)]
        public string AdministrativeTypeDictionaryValue { get; set; }

        [StringLength(4000)]
        public string ApplicantCategoryDictionaryId { get; set; }

        [StringLength(4000)]
        public string ApplicantCategoryDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CauseCitizenDictionaryId { get; set; }

        [StringLength(4000)]
        public string CauseCitizenDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CitizenCategoryDictionaryId { get; set; }

        [StringLength(4000)]
        public string CitizenCategoryDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CitizenResultDictionaryId { get; set; }

        [StringLength(4000)]
        public string CitizenResultDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CitizenTypeDictionaryId { get; set; }

        [StringLength(4000)]
        public string CitizenTypeDictionaryValue { get; set; }

        [StringLength(4000)]
        public string DocumentKindDictionaryId { get; set; }

        [StringLength(4000)]
        public string DocumentKindDictionaryValue { get; set; }

        [StringLength(4000)]
        public string FormDeliveryDictionaryId { get; set; }

        [StringLength(4000)]
        public string FormDeliveryDictionaryValue { get; set; }

        [StringLength(4000)]
        public string FormSendingDictionaryId { get; set; }

        [StringLength(4000)]
        public string FormSendingDictionaryValue { get; set; }

        [StringLength(4000)]
        public string KatoDictionaryId { get; set; }

        [StringLength(4000)]
        public string KatoDictionaryValue { get; set; }

        [StringLength(4000)]
        public string LanguageDictionaryId { get; set; }

        [StringLength(4000)]
        public string LanguageDictionaryValue { get; set; }

        [StringLength(4000)]
        public string NomenclatureDictionaryId { get; set; }

        [StringLength(4000)]
        public string NomenclatureDictionaryValue { get; set; }

        [StringLength(4000)]
        public string QuestionDesignDictionaryId { get; set; }

        [StringLength(4000)]
        public string QuestionDesignDictionaryValue { get; set; }

        [StringLength(4000)]
        public string SigningFormDictionaryId { get; set; }

        [StringLength(4000)]
        public string SigningFormDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CompleteDocumentsId { get; set; }

        [StringLength(4000)]
        public string EditDocumentsId { get; set; }

        [StringLength(4000)]
        public string RepealDocumentsId { get; set; }

        public Guid? RepeaterId { get; set; }

        public Guid? AttachmentId { get; set; }

        public Guid? ResolutionId { get; set; }

        [StringLength(4000)]
        public string AgreementsId { get; set; }

        [StringLength(4000)]
        public string AgreementsValue { get; set; }

        [StringLength(4000)]
        public string ExecutorsId { get; set; }

        [StringLength(4000)]
        public string ExecutorsValue { get; set; }

        [StringLength(4000)]
        public string ReadersId { get; set; }

        [StringLength(4000)]
        public string ReadersValue { get; set; }

        [StringLength(4000)]
        public string RecipientsId { get; set; }

        [StringLength(4000)]
        public string RecipientsValue { get; set; }

        [StringLength(4000)]
        public string RegistratorId { get; set; }

        [StringLength(4000)]
        public string RegistratorValue { get; set; }

        [StringLength(510)]
        public string ResponsibleId { get; set; }

        [StringLength(4000)]
        public string ResponsibleValue { get; set; }

        [StringLength(4000)]
        public string SignerId { get; set; }

        [StringLength(4000)]
        public string SignerValue { get; set; }

        [StringLength(4000)]
        public string CorrespondentsId { get; set; }

        [StringLength(4000)]
        public string CorrespondentsValue { get; set; }

        [StringLength(4000)]
        public string MonitoringAuthorId { get; set; }

        [StringLength(4000)]
        public string MonitoringAuthorValue { get; set; }

        [StringLength(4000)]
        public string MonitoringNote { get; set; }

        [StringLength(4000)]
        public string AnswersId { get; set; }

        [StringLength(4000)]
        public string AnswersValue { get; set; }

        [StringLength(4000)]
        public string CompleteDocumentsValue { get; set; }

        [StringLength(4000)]
        public string EditDocumentsValue { get; set; }

        [StringLength(4000)]
        public string RepealDocumentsValue { get; set; }

        [StringLength(4000)]
        public string DisplayName { get; set; }

        [StringLength(4000)]
        public string AutoAnswersId { get; set; }

        [StringLength(4000)]
        public string AutoAnswersValue { get; set; }

        [StringLength(4000)]
        public string AutoAnswersTempId { get; set; }

        [StringLength(4000)]
        public string AutoAnswersTempValue { get; set; }

        [StringLength(4000)]
        public string AutoCompleteDocumentsValue { get; set; }

        [StringLength(4000)]
        public string AutoEditDocumentsValue { get; set; }

        [StringLength(4000)]
        public string AutoRepealDocumentsValue { get; set; }

        public int SortNumber { get; set; }

        [StringLength(4000)]
        public string AutoCompleteDocumentsId { get; set; }

        [StringLength(4000)]
        public string AutoEditDocumentsId { get; set; }

        [StringLength(4000)]
        public string AutoRepealDocumentsId { get; set; }

        public DateTime? FactExecutionDate { get; set; }

        public DateTime? FirstExecutionDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public Guid? TemplateId { get; set; }

        [StringLength(4000)]
        public string Counters { get; set; }

        [StringLength(4000)]
        public string DocumentDictionaryTypeId { get; set; }

        [StringLength(4000)]
        public string DocumentDictionaryTypeValue { get; set; }

        [StringLength(4000)]
        public string ResolutionValue { get; set; }

        public int OutgoingType { get; set; }

        [StringLength(4000)]
        public string SourceId { get; set; }

        [StringLength(4000)]
        public string SourceValue { get; set; }

        [StringLength(4000)]
        public string DestinationId { get; set; }

        [StringLength(4000)]
        public string DestinationValue { get; set; }

        public Guid? MainTaskId { get; set; }

        public Guid? MainDocumentId { get; set; }

        [StringLength(4000)]
        public string OwnerId { get; set; }

        [StringLength(4000)]
        public string OwnerValue { get; set; }

        [StringLength(4000)]
        public string Country { get; set; }

        [StringLength(4000)]
        public string Area { get; set; }

        [StringLength(4000)]
        public string Postcode { get; set; }

        [StringLength(4000)]
        public string Phone { get; set; }

        [StringLength(4000)]
        public string Department { get; set; }

        [StringLength(4000)]
        public string City { get; set; }

        [StringLength(4000)]
        public string Address { get; set; }

        [StringLength(4000)]
        public string NumberBill { get; set; }

        [StringLength(4000)]
        public string Email { get; set; }

        public Guid? SuperMainDocumentId { get; set; }

        [StringLength(4000)]
        public string ModifiedUser { get; set; }

        public bool IsNotification { get; set; }

        public int NotificationCount { get; set; }

        public DateTime? DateDispatch { get; set; }

        [StringLength(4000)]
        public string DispatchNote { get; set; }

        [StringLength(4000)]
        public string Digest { get; set; }

        public bool IsAttachments { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        [StringLength(4000)]
        public string Recipient { get; set; }

        [Column(TypeName = "image")]
        public byte[] QrCode { get; set; }

        public bool IsArchive { get; set; }

        public int? InventoryId { get; set; }

        public DateTime? FulfilledDate { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoAwaitingResponseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoDocumentDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoOutgoingDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoProtocolDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoMonitoringDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoFactExecutionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoFirstExecutionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoExecutionDate { get; set; }

        [StringLength(900)]
        public string CreatedUserId { get; set; }

        [StringLength(900)]
        public string CreatedUserValue { get; set; }

        [StringLength(900)]
        public string Book { get; set; }

        [StringLength(900)]
        public string Deed { get; set; }

        [StringLength(100)]
        public string Ip { get; set; }

        [StringLength(900)]
        public string Akt { get; set; }

        public int ProjectType { get; set; }

        public Guid OrganizationId { get; set; }

        [StringLength(300)]
        public string AttachPath { get; set; }

        [StringLength(4000)]
        public string RegionValue { get; set; }

        [StringLength(4000)]
        public string RegionId { get; set; }

        public decimal? PriceSum { get; set; }

        public int? RemarkId { get; set; }

        public DateTime? ParleyStartDate { get; set; }

        public DateTime? ParleyEndDate { get; set; }

        [StringLength(4000)]
        public string RemarkText1 { get; set; }

        [StringLength(4000)]
        public string RemarkText2 { get; set; }

        [StringLength(4000)]
        public string RemarkText3 { get; set; }

        public int? CountDay { get; set; }

        public DateTime? ReturnDate { get; set; }

        public DateTime? CompareConterDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccessDocument> AccessDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        public virtual RemarkType RemarkType { get; set; }

        public virtual Template Template { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
