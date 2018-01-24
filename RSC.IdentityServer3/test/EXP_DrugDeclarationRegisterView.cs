namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugDeclarationRegisterView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [StringLength(2000)]
        public string TypeNameRu { get; set; }

        [StringLength(2000)]
        public string TypeNameKz { get; set; }

        [StringLength(500)]
        public string NameRu { get; set; }

        [StringLength(500)]
        public string NameKz { get; set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        public DateTime? FirstSendDate { get; set; }

        [StringLength(500)]
        public string ProducerRu { get; set; }

        [StringLength(500)]
        public string ProducerKz { get; set; }

        [StringLength(500)]
        public string ProducerEn { get; set; }

        [StringLength(500)]
        public string PackerRu { get; set; }

        [StringLength(500)]
        public string PackerKz { get; set; }

        [StringLength(500)]
        public string PackerEn { get; set; }

        [StringLength(500)]
        public string ReleaseControlRu { get; set; }

        [StringLength(500)]
        public string ReleaseControlKz { get; set; }

        [StringLength(500)]
        public string ReleaseControlEn { get; set; }

        public Guid? CountryId { get; set; }

        [StringLength(4000)]
        public string CountryRu { get; set; }

        [StringLength(4000)]
        public string CountryKz { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [StringLength(2000)]
        public string StatusRu { get; set; }

        [StringLength(2000)]
        public string StatusKz { get; set; }

        public string DrugTypeRu { get; set; }

        public string DrugTypeKz { get; set; }

        public int? MnnId { get; set; }

        [StringLength(255)]
        public string MnnRu { get; set; }

        [StringLength(510)]
        public string MnnKz { get; set; }

        [StringLength(255)]
        public string MnnEn { get; set; }

        [StringLength(500)]
        public string DrugFormRu { get; set; }

        [StringLength(1000)]
        public string DrugFormKz { get; set; }

        public string DosageRu { get; set; }

        public string DosageKz { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid StageId { get; set; }

        [StringLength(2000)]
        public string StageRu { get; set; }

        [StringLength(2000)]
        public string StageKz { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string StatusCode { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid ExpertId { get; set; }

        [StringLength(4000)]
        public string ExpertInitials { get; set; }

        public bool? Suspended { get; set; }

        public bool? OnBoard { get; set; }

        public bool? ProductionEvaluation { get; set; }

        public DateTime? StageStartDate { get; set; }

        public DateTime? StageControlDate { get; set; }

        public int? SuspensionPeriod { get; set; }

        public DateTime? ConclusionDate { get; set; }

        public bool? StageCompleted { get; set; }

        public DateTime? StageEndDate { get; set; }

        public bool? StageOverdue { get; set; }

        public int? OverdueDays { get; set; }

        public Guid? LetterId { get; set; }

        [StringLength(250)]
        public string LetterNumber { get; set; }

        public int? StageDays { get; set; }

        public bool? IsNewProducer { get; set; }

        public string ActiveSubstanceRu { get; set; }

        public string ActiveSubstanceKz { get; set; }

        public string SecondarySubstanceRu { get; set; }

        public string SecondarySubstanceKz { get; set; }

        public int? CountDosageIsControl { get; set; }

        [StringLength(500)]
        public string ApplicantRu { get; set; }

        [StringLength(500)]
        public string ApplicantKz { get; set; }

        [StringLength(500)]
        public string ApplicantEn { get; set; }

        [StringLength(4000)]
        public string ApplicantCountryRu { get; set; }

        [StringLength(4000)]
        public string ApplicantCountryKz { get; set; }

        [StringLength(500)]
        public string HolderRu { get; set; }

        [StringLength(500)]
        public string HolderKz { get; set; }

        [StringLength(500)]
        public string HolderEn { get; set; }

        [StringLength(4000)]
        public string HolderCountryRu { get; set; }

        [StringLength(4000)]
        public string HolderCountryKz { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DicStageId { get; set; }

        [StringLength(50)]
        public string StageCode { get; set; }

        [StringLength(2000)]
        public string StageResultRu { get; set; }

        [StringLength(2000)]
        public string StageResultKz { get; set; }

        public DateTime? SuspendedStartDate { get; set; }

        public bool? Paid { get; set; }

        public DateTime? PaymentDate { get; set; }

        public bool? PaymentOverdue { get; set; }

        [StringLength(512)]
        public string PaymentNumber { get; set; }

        [StringLength(512)]
        public string InvoiceNumber1C { get; set; }

        public bool? Unlimited { get; set; }

        public string Experts { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool IsNew { get; set; }
    }
}
