namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegisterProjectsView")]
    public partial class RegisterProjectsView
    {
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
        public string NameRu { get; set; }

        [StringLength(500)]
        public string NameKz { get; set; }

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

        [StringLength(4000)]
        public string OwnerLastName { get; set; }

        [StringLength(4000)]
        public string OwnerFirstName { get; set; }

        [StringLength(4000)]
        public string OwnerMiddleName { get; set; }

        [StringLength(4000)]
        public string AccelerationTypeName { get; set; }

        [StringLength(4000)]
        public string LsTypeDicName { get; set; }

        [StringLength(4000)]
        public string LsType2Name { get; set; }

        [StringLength(4000)]
        public string SaleTypeName { get; set; }

        [StringLength(4000)]
        public string IntroductionMethodName { get; set; }

        [StringLength(4000)]
        public string DosageMeasureTypeName { get; set; }

        [StringLength(4000)]
        public string ManufactureTypeName { get; set; }

        [StringLength(4000)]
        public string BestBeforeMeasureTypeName { get; set; }

        [StringLength(500)]
        public string AppPeriodOpen { get; set; }

        public Guid? AppPeriodOpenMeasureDicId { get; set; }

        [StringLength(500)]
        public string AppPeriodMix { get; set; }

        public Guid? AppPeriodMixMeasureDicId { get; set; }

        [StringLength(4000)]
        public string AppPeriodOpenMeasureName { get; set; }

        [StringLength(4000)]
        public string AppPeriodMixMeasureName { get; set; }

        [StringLength(4000)]
        public string StatusValue { get; set; }
    }
}
