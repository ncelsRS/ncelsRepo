namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProjectDocument")]
    public partial class ProjectDocument
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool IsDeleted { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool IsAdministrativeUse { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool IsAwaitingResponse { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsTradeSecret { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? AwaitingResponseDate { get; set; }

        public DateTime? DocumentDate { get; set; }

        public DateTime? OutgoingDate { get; set; }

        public DateTime? ProtocolDate { get; set; }

        public DateTime? MonitoringDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ApplicantType { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentType { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MonitoringType { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PriorityType { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AppendixCount { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CopiesCount { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PageCount { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        public string AdministrativeTypeDictionaryValue { get; set; }

        [StringLength(4000)]
        public string ApplicantCategoryDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CauseCitizenDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CitizenCategoryDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CitizenResultDictionaryValue { get; set; }

        [StringLength(4000)]
        public string CitizenTypeDictionaryValue { get; set; }

        [StringLength(4000)]
        public string DocumentKindDictionaryValue { get; set; }

        [StringLength(4000)]
        public string FormDeliveryDictionaryValue { get; set; }

        [StringLength(4000)]
        public string FormSendingDictionaryValue { get; set; }

        [StringLength(4000)]
        public string KatoDictionaryValue { get; set; }

        [StringLength(4000)]
        public string LanguageDictionaryValue { get; set; }

        [StringLength(4000)]
        public string NomenclatureDictionaryValue { get; set; }

        [StringLength(4000)]
        public string QuestionDesignDictionaryValue { get; set; }

        [StringLength(4000)]
        public string SigningFormDictionaryValue { get; set; }

        [StringLength(4000)]
        public string AgreementsValue { get; set; }

        [StringLength(4000)]
        public string ExecutorsValue { get; set; }

        [StringLength(4000)]
        public string ReadersValue { get; set; }

        [StringLength(4000)]
        public string RecipientsValue { get; set; }

        [StringLength(4000)]
        public string RegistratorValue { get; set; }

        [StringLength(4000)]
        public string ResponsibleValue { get; set; }

        [StringLength(4000)]
        public string SignerValue { get; set; }

        [StringLength(4000)]
        public string CorrespondentsValue { get; set; }

        [StringLength(4000)]
        public string MonitoringAuthorValue { get; set; }

        [StringLength(4000)]
        public string MonitoringNote { get; set; }

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
        public string AutoAnswersValue { get; set; }

        [StringLength(4000)]
        public string AutoAnswersTempValue { get; set; }

        [StringLength(4000)]
        public string AutoCompleteDocumentsValue { get; set; }

        [StringLength(4000)]
        public string AutoEditDocumentsValue { get; set; }

        [StringLength(4000)]
        public string AutoRepealDocumentsValue { get; set; }

        [Key]
        [Column(Order = 14)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SortNumber { get; set; }

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

        public DateTime? FactExecutionDate { get; set; }

        public DateTime? FirstExecutionDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoFactExecutionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoFirstExecutionDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AutoExecutionDate { get; set; }

        [StringLength(4000)]
        public string Counters { get; set; }

        [StringLength(40)]
        public string DocumentDictionaryTypeValue { get; set; }

        [StringLength(4000)]
        public string ResolutionValue { get; set; }

        [Key]
        [Column(Order = 15)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OutgoingType { get; set; }

        [StringLength(4000)]
        public string SourceValue { get; set; }

        [StringLength(4000)]
        public string DestinationValue { get; set; }

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

        [Key]
        [Column(Order = 16)]
        public bool IsNotification { get; set; }

        [Key]
        [Column(Order = 17)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int NotificationCount { get; set; }

        public DateTime? DateDispatch { get; set; }

        [StringLength(4000)]
        public string DispatchNote { get; set; }

        [StringLength(4000)]
        public string Digest { get; set; }

        [Key]
        [Column(Order = 18)]
        public bool IsAttachments { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        [StringLength(4000)]
        public string Recipient { get; set; }

        [Column(TypeName = "image")]
        public byte[] QrCode { get; set; }

        [Key]
        [Column(Order = 19)]
        public bool IsArchive { get; set; }

        public DateTime? FulfilledDate { get; set; }

        [StringLength(510)]
        public string State { get; set; }

        [StringLength(510)]
        public string Priority { get; set; }
    }
}
