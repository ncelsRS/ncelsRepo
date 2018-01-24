namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Price
    {
        public Guid Id { get; set; }

        public int Type { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public Guid PriceProjectId { get; set; }

        public Guid? CountryId { get; set; }

        public decimal ManufacturerPrice { get; set; }

        public Guid? ManufacturerPriceCurrencyDicId { get; set; }

        [StringLength(500)]
        public string ManufacturerPriceNote { get; set; }

        public decimal LimitPrice { get; set; }

        public Guid? LimitPriceCurrencyDicId { get; set; }

        [StringLength(500)]
        public string LimitPriceNote { get; set; }

        public decimal AvgOptPrice { get; set; }

        public Guid? AvgOptPriceCurrencyDicId { get; set; }

        [StringLength(500)]
        public string AvgOptPriceNote { get; set; }

        public decimal AvgRozPrice { get; set; }

        public Guid? AvgRozPriceCurrencyDicId { get; set; }

        [StringLength(500)]
        public string AvgRozPriceNote { get; set; }

        public decimal CipPrice { get; set; }

        public Guid? CipPriceCurrencyDicId { get; set; }

        public Guid? RefPriceTypeDicId { get; set; }

        public decimal RefPrice { get; set; }

        public Guid? RefPriceCurrencyDicId { get; set; }

        public decimal OwnerPrice { get; set; }

        public Guid? OwnerPriceCurrencyDicId { get; set; }

        public decimal BritishPrice { get; set; }

        public decimal UnitPrice { get; set; }

        public Guid? UnitPriceCurrencyDicId { get; set; }

        public decimal BritishCost { get; set; }

        public decimal AvgObkCost { get; set; }

        public decimal AvgRznCost { get; set; }

        public decimal AvgOptCost { get; set; }

        public decimal ZakupCost { get; set; }

        public decimal LimitCost { get; set; }

        public decimal MinimalCost { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? MtPartsId { get; set; }

        public decimal OriginalCost { get; set; }

        public decimal MarkupCost { get; set; }

        public decimal MarkupCostOpt { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CalcDateStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CalcDateEnd { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RequestDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsIncluded { get; set; }

        public bool IsUnitPrice { get; set; }

        public bool IsManufacturerPrice { get; set; }

        public bool IsLimitPrice { get; set; }

        public bool IsAvgOptPrice { get; set; }

        public bool IsAvgRozPrice { get; set; }

        public decimal FixRfkPrice { get; set; }

        [StringLength(1000)]
        public string AvgRozPriceLink { get; set; }

        [StringLength(1000)]
        public string AvgOptPriceLink { get; set; }
    }
}
