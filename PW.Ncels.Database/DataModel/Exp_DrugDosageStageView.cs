namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exp_DrugDosageStageView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DosageId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid DosageStageId { get; set; }

        public DateTime? DosageStageStartDate { get; set; }

        public DateTime? DosageStageEndDate { get; set; }

        [StringLength(50)]
        public string DosageRegNumber { get; set; }

        [StringLength(500)]
        public string DosageBestBefore { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal DosageDosageValue { get; set; }

        public int? DosageSaleTypeId { get; set; }

        [StringLength(2000)]
        public string DosageSaleTypeName { get; set; }

        [StringLength(255)]
        public string DosageDosageName { get; set; }

        [StringLength(250)]
        public string DosageDosageShortName { get; set; }

        [StringLength(255)]
        public string DosageBestBeforeName { get; set; }

        [StringLength(250)]
        public string DosageBestBeforeShortName { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid DeclarationId { get; set; }

        [StringLength(500)]
        public string DeclarationNumber { get; set; }

        [StringLength(500)]
        public string DeclarationNameRu { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime DeclarationCreatedDate { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DeclarationTypeId { get; set; }

        [StringLength(2000)]
        public string DeclarationTypeNameRu { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StageId { get; set; }

        [StringLength(2000)]
        public string StageNameRu { get; set; }

        public int? ResultId { get; set; }

        [StringLength(2000)]
        public string ResultNameRu { get; set; }

        public DateTime? ResultDate { get; set; }

        public Guid? ResultCreatorId { get; set; }

        [StringLength(4000)]
        public string ResultCreatorLastName { get; set; }

        [StringLength(4000)]
        public string ResultCreatorFirstName { get; set; }

        [StringLength(4000)]
        public string ResultCreatorMiddleName { get; set; }

        [StringLength(4000)]
        public string ResultCreatorShortName { get; set; }

        public Guid? FinalDocStatusId { get; set; }

        [StringLength(4000)]
        public string FinalDocStatusCode { get; set; }

        [StringLength(4000)]
        public string FinalDocStatusDisplayName { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommissionId { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommissionTypeId { get; set; }

        public int? CommissionConclusionTypeId { get; set; }

        public string CommissionConclusionComment { get; set; }

        public int? CommissionConclusionTypeCode { get; set; }

        [StringLength(500)]
        public string ProducerNameRu { get; set; }

        public Guid? ProducerCountryId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProducerDocDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProducerDocExpiryDate { get; set; }

        [StringLength(4000)]
        public string ProducerCountryName { get; set; }

        public int? DosageFormId { get; set; }

        [StringLength(500)]
        public string DosageFormName { get; set; }

        public string DeclarationPaysDates { get; set; }

        public int? DeclarationAtxId { get; set; }

        [StringLength(500)]
        public string DeclarationAtxName { get; set; }

        [StringLength(10)]
        public string DeclarationAtxCode { get; set; }

        public int? DeclarationMnnId { get; set; }

        [StringLength(255)]
        public string DeclarationMnnNameRu { get; set; }

        public int? NtdId { get; set; }

        [StringLength(2000)]
        public string NtdNameRu { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PrevCommissionCount { get; set; }
    }
}
