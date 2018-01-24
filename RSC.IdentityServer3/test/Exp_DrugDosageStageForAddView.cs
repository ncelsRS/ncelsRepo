namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exp_DrugDosageStageForAddView
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

        [StringLength(50)]
        public string DosageRegNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid DeclarationId { get; set; }

        [StringLength(500)]
        public string DeclarationNumber { get; set; }

        [StringLength(500)]
        public string DeclarationNameRu { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime DeclarationCreatedDate { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StageId { get; set; }

        [StringLength(2000)]
        public string StageNameRu { get; set; }

        public int? ResultId { get; set; }

        [StringLength(2000)]
        public string ResultNameRu { get; set; }

        public Guid? FinalDocStatusId { get; set; }

        [StringLength(4000)]
        public string FinalDocStatusCode { get; set; }

        [StringLength(4000)]
        public string FinalDocStatusDisplayName { get; set; }

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

        public int? DeclarationSaleTypeId { get; set; }

        [StringLength(2000)]
        public string DeclarationSaleTypeName { get; set; }

        public int? NtdId { get; set; }

        [StringLength(2000)]
        public string NtdNameRu { get; set; }
    }
}
