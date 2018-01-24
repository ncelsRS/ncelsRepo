namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PricesView")]
    public partial class PricesView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid PriceProjectId { get; set; }

        public Guid? CountryId { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal ManufacturerPrice { get; set; }

        public Guid? ManufacturerPriceCurrencyDicId { get; set; }

        [StringLength(500)]
        public string ManufacturerPriceNote { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal LimitPrice { get; set; }

        public Guid? LimitPriceCurrencyDicId { get; set; }

        [StringLength(500)]
        public string LimitPriceNote { get; set; }

        [Key]
        [Column(Order = 5)]
        public decimal AvgOptPrice { get; set; }

        public Guid? AvgOptPriceCurrencyDicId { get; set; }

        [StringLength(500)]
        public string AvgOptPriceNote { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal AvgRozPrice { get; set; }

        public Guid? AvgRozPriceCurrencyDicId { get; set; }

        [StringLength(500)]
        public string AvgRozPriceNote { get; set; }

        [Key]
        [Column(Order = 7)]
        public decimal CipPrice { get; set; }

        public Guid? CipPriceCurrencyDicId { get; set; }

        public Guid? RefPriceTypeDicId { get; set; }

        [Key]
        [Column(Order = 8)]
        public decimal RefPrice { get; set; }

        public Guid? RefPriceCurrencyDicId { get; set; }

        [Key]
        [Column(Order = 9)]
        public decimal OwnerPrice { get; set; }

        public Guid? OwnerPriceCurrencyDicId { get; set; }

        [Key]
        [Column(Order = 10)]
        public decimal BritishPrice { get; set; }

        [StringLength(4000)]
        public string CountryName { get; set; }

        [StringLength(4000)]
        public string ManufacturerPriceCurrencyName { get; set; }

        [StringLength(4000)]
        public string LimitPriceCurrencyName { get; set; }

        [StringLength(4000)]
        public string AvgOptPriceCurrencyName { get; set; }

        [StringLength(4000)]
        public string AvgRozPriceCurrencyName { get; set; }

        [StringLength(4000)]
        public string CipPriceCurrencyName { get; set; }

        [StringLength(4000)]
        public string RefPriceCurrencyName { get; set; }

        [StringLength(4000)]
        public string RefPriceTypeName { get; set; }

        [StringLength(4000)]
        public string OwnerPriceCurrencyName { get; set; }

        [Key]
        [Column(Order = 11)]
        public decimal UnitPrice { get; set; }

        public Guid? UnitPriceCurrencyDicId { get; set; }

        [StringLength(4000)]
        public string UnitPriceCurrencyName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        [Key]
        [Column(Order = 12)]
        public decimal BritishCost { get; set; }

        public int? MtPartsId { get; set; }

        public string PartsName { get; set; }

        [Key]
        [Column(Order = 13)]
        public bool IsIncluded { get; set; }

        [Key]
        [Column(Order = 14)]
        public bool IsUnitPrice { get; set; }

        [Key]
        [Column(Order = 15)]
        public bool IsAvgOptPrice { get; set; }

        [Key]
        [Column(Order = 16)]
        public bool IsAvgRozPrice { get; set; }

        [Key]
        [Column(Order = 17)]
        public bool IsLimitPrice { get; set; }

        [Key]
        [Column(Order = 18)]
        public bool IsManufacturerPrice { get; set; }
    }
}
