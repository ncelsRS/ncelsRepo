namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcOutCountView")]
    public partial class TmcOutCountView
    {
        public decimal? CountActual { get; set; }

        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid TmcOutId { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid TmcId { get; set; }

        public string Name { get; set; }

        public Guid? MeasureTypeConvertDicId { get; set; }

        [StringLength(4000)]
        public string MeasureTypeConvertDicValue { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal Count { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal CountFact { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(6)]
        public string StateTypeValue { get; set; }

        [StringLength(450)]
        public string Note { get; set; }

        [StringLength(4000)]
        public string OwnerEmployeeValue { get; set; }

        [StringLength(4000)]
        public string StorageDicValue { get; set; }

        [StringLength(450)]
        public string Safe { get; set; }

        [StringLength(450)]
        public string Rack { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public Guid? CreatedEmployeeId { get; set; }

        public int? TmcStateType { get; set; }

        public Guid? TmcInId { get; set; }

        [StringLength(450)]
        public string Number { get; set; }

        [StringLength(450)]
        public string Code { get; set; }

        [StringLength(450)]
        public string Manufacturer { get; set; }

        [StringLength(450)]
        public string Serial { get; set; }

        public decimal? TmcCount { get; set; }

        public Guid? MeasureTypeDicId { get; set; }

        public decimal? TmcCountFact { get; set; }

        public decimal? CountConvert { get; set; }

        public Guid? TmcMeasureTypeConvertDicId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ManufactureDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpiryDate { get; set; }

        public Guid? PackageDicId { get; set; }

        public Guid? TmcTypeDicId { get; set; }

        public Guid? StorageDicId { get; set; }

        [StringLength(450)]
        public string TmcSafe { get; set; }

        [StringLength(450)]
        public string TmcRack { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        [StringLength(4000)]
        public string MeasureTypeDicValue { get; set; }

        [StringLength(4000)]
        public string PackageDicValue { get; set; }

        [StringLength(4000)]
        public string TmcTypeDicValue { get; set; }

        [StringLength(4000)]
        public string OwnerEmployeeName { get; set; }

        public int? ApplicationStateType { get; set; }
    }
}
