namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcView")]
    public partial class TmcView
    {
        [Key]
        [Column(Order = 0)]
        public decimal CountActual { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid CreatedEmployeeId { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(9)]
        public string StateTypeValue { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid TmcInId { get; set; }

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
        [Column(Order = 7)]
        public decimal Count { get; set; }

        public Guid? MeasureTypeDicId { get; set; }

        [StringLength(4000)]
        public string MeasureTypeDicValue { get; set; }

        [Key]
        [Column(Order = 8)]
        public decimal CountFact { get; set; }

        [Key]
        [Column(Order = 9)]
        public decimal CountConvert { get; set; }

        public Guid? MeasureTypeConvertDicId { get; set; }

        [StringLength(4000)]
        public string MeasureTypeConvertDicValue { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ManufactureDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpiryDate { get; set; }

        public Guid? PackageDicId { get; set; }

        [StringLength(4000)]
        public string PackageDicValue { get; set; }

        public Guid? TmcTypeDicId { get; set; }

        [StringLength(4000)]
        public string TmcTypeDicValue { get; set; }

        public Guid? StorageDicId { get; set; }

        [StringLength(4000)]
        public string StorageDicValue { get; set; }

        [StringLength(450)]
        public string Safe { get; set; }

        [StringLength(450)]
        public string Rack { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceivingDate { get; set; }

        [StringLength(4000)]
        public string OwnerEmployeeValue { get; set; }

        public decimal? UsedCount { get; set; }
    }
}
