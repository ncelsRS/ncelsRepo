namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegisterProjectJournal")]
    public partial class RegisterProjectJournal
    {
        public bool? IsStageEnd { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }

        [StringLength(512)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Status { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid OwnerId { get; set; }

        public Guid? ContractId { get; set; }

        public Guid? AccelerationTypeDicId { get; set; }

        [StringLength(500)]
        public string AccelerationNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AccelerationDate { get; set; }

        [StringLength(500)]
        public string AccelerationNote { get; set; }

        [StringLength(500)]
        public string TradeName { get; set; }

        [StringLength(500)]
        public string NameKz { get; set; }

        [StringLength(500)]
        public string NameRu { get; set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        [StringLength(500)]
        public string MnnKz { get; set; }

        [StringLength(500)]
        public string MnnEn { get; set; }

        [StringLength(500)]
        public string MnnRu { get; set; }

        [Key]
        [Column(Order = 5)]
        public bool IsPatent { get; set; }

        [StringLength(500)]
        public string PatentNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PatentDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PatentExpiryDate { get; set; }

        [StringLength(500)]
        public string LsFormNameKz { get; set; }

        [StringLength(500)]
        public string LsFormNameRu { get; set; }

        [StringLength(500)]
        public string AtxCode { get; set; }

        [StringLength(500)]
        public string AtxNameKz { get; set; }

        [StringLength(500)]
        public string AtxNameRu { get; set; }

        public Guid? LsTypeDicId { get; set; }

        public Guid? LsType2DicId { get; set; }

        [StringLength(500)]
        public string OriginalName { get; set; }

        public Guid? SaleTypeDicId { get; set; }

        public Guid? IntroducingMethodDicId { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal Dosage { get; set; }

        public Guid? DosageMeasureTypeDicId { get; set; }

        [StringLength(500)]
        public string DosageNoteKz { get; set; }

        [StringLength(500)]
        public string DosageNoteRu { get; set; }

        [StringLength(500)]
        public string ConcentrationRu { get; set; }

        [StringLength(500)]
        public string ConcentrationKz { get; set; }

        public Guid? CompositionId { get; set; }

        [Key]
        [Column(Order = 7)]
        public bool IsGrls { get; set; }

        [StringLength(500)]
        public string Transportation { get; set; }

        public Guid? ManufactureTypeDicId { get; set; }

        [Key]
        [Column(Order = 8)]
        public bool IsGmp { get; set; }

        [Column(TypeName = "date")]
        public DateTime? GmpExpiryDate { get; set; }

        [StringLength(500)]
        public string BestBefore { get; set; }

        public Guid? BestBeforeMeasureTypeDicId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AppPeriod1BeginDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AppPeriod1FinishDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AppPeriod2BeginDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AppPeriod2FinishDate { get; set; }

        [StringLength(500)]
        public string StorageConditions1 { get; set; }

        [StringLength(500)]
        public string StorageConditions2 { get; set; }

        [StringLength(500)]
        public string Barcode { get; set; }

        [Key]
        [Column(Order = 9)]
        public decimal ManufacturePrice { get; set; }

        public decimal? RefPrice { get; set; }

        public decimal? RegPrice { get; set; }

        [StringLength(500)]
        public string SecureDocument { get; set; }

        [StringLength(500)]
        public string SecureDocumentNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SecureDocumentDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? SecureDocumentExpiryDate { get; set; }

        [Key]
        [Column(Order = 10)]
        public bool IsConvention { get; set; }

        [StringLength(500)]
        public string RegDocNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegDocDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegDocExpiryDate { get; set; }

        [StringLength(500)]
        public string RegDocNormativeNumber { get; set; }

        public Guid? ResultTypeDicId { get; set; }

        public bool? IsPayed { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PayDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContrDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ConclusionDate { get; set; }

        public bool? IsStageExpired { get; set; }

        public int? ExpiredDayCount { get; set; }

        [StringLength(200)]
        public string ExpertAz { get; set; }

        [StringLength(200)]
        public string OutgoingDoc { get; set; }

        public int? DayCount { get; set; }

        public bool? IsNewManufacrurer { get; set; }

        [StringLength(4000)]
        public string RegisterType { get; set; }

        [StringLength(500)]
        public string ManufaturerName { get; set; }

        [StringLength(4000)]
        public string CountryName { get; set; }

        [StringLength(500)]
        public string ApplicantName { get; set; }

        [StringLength(4000)]
        public string Classification { get; set; }

        [StringLength(4000)]
        public string Mnn { get; set; }

        [StringLength(4000)]
        public string DosageMeasureTypeName { get; set; }

        [StringLength(4000)]
        public string ExpertiseStage { get; set; }

        [StringLength(510)]
        public string ResponsibleId { get; set; }

        [StringLength(4000)]
        public string ResponsibleValue { get; set; }

        [StringLength(4000)]
        public string ResultTypeName { get; set; }

        [StringLength(4000)]
        public string StatusValue { get; set; }
    }
}
