namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcOffStateView")]
    public partial class TmcOffStateView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CreatedEmployeeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        [StringLength(450)]
        public string UseReason { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal UsedCount { get; set; }

        public Guid? ExpertiseStatementId { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementNumber { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementTypeStr { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal RequstedCount { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal ReceivedCount { get; set; }

        [Key]
        [Column(Order = 7)]
        public Guid TmcId { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "date")]
        public DateTime TmcCreateDate { get; set; }

        [Key]
        [Column(Order = 9)]
        public Guid TmcCreateEmployeeId { get; set; }

        [StringLength(450)]
        public string Number { get; set; }

        public string Name { get; set; }

        [StringLength(450)]
        public string Code { get; set; }

        [StringLength(450)]
        public string Manufacturer { get; set; }

        [StringLength(450)]
        public string Serial { get; set; }

        [Key]
        [Column(Order = 10)]
        public decimal RequestedCount { get; set; }

        public Guid? MeasureTypeDicId { get; set; }

        [Key]
        [Column(Order = 11)]
        public decimal CountFact { get; set; }

        [Key]
        [Column(Order = 12)]
        public decimal ConvertedCount { get; set; }

        public Guid? MeasureTypeConvertDicId { get; set; }

        [Key]
        [Column(Order = 13)]
        public decimal ResidueCount { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ManufactureDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpiryDate { get; set; }

        public Guid? PackageDicId { get; set; }

        public Guid? TmcTypeDicId { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceivingDate { get; set; }

        public DateTime? WriteoffDate { get; set; }
    }
}
