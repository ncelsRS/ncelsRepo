namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PriceProject_Ext
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string MnnCode { get; set; }

        [StringLength(1000)]
        public string DrugDescription { get; set; }

        public DateTime? RegEndDate { get; set; }

        [StringLength(500)]
        public string RequesterContacts { get; set; }

        public decimal? MarginalPriceMnn2016 { get; set; }

        public decimal? MarginalPriceTn622 { get; set; }

        public decimal? PriceDropPercent { get; set; }

        public bool? IsConformity639 { get; set; }

        [StringLength(500)]
        public string Conformity639Note { get; set; }

        public decimal? FixPrice_a_11_16 { get; set; }

        public decimal? PriceDifference2016 { get; set; }

        public decimal? IntRef_AvgInPricePackage2015 { get; set; }

        public Guid? IntRef_AvgInPricePackage2015CurrencyId { get; set; }

        public decimal? IntRef_AvgInPricePackage { get; set; }

        public decimal? IntRef_RetailAktobe { get; set; }

        public decimal? MinRefPriceCoef { get; set; }

        [StringLength(100)]
        public string p16_Country { get; set; }

        [StringLength(100)]
        public string p16_RegPrice { get; set; }

        public int? p16_RegYear { get; set; }

        [StringLength(100)]
        public string p16_MarginalPrice { get; set; }

        public int? p16_MarginalYear { get; set; }

        [StringLength(100)]
        public string p16_AvgOptPricee { get; set; }

        [StringLength(100)]
        public string p16_AvgRetailPrice { get; set; }

        public decimal? RegNcelsPrice_11_16 { get; set; }

        public decimal? RegMzsrPrice_11_16 { get; set; }

        public decimal? FinalPrice { get; set; }

        public decimal? FinalPricePercent { get; set; }

        public decimal? FinalFixPrice { get; set; }

        public decimal? FinalMarginalPriceTn { get; set; }

        public decimal? ProjectPrice2017 { get; set; }

        public decimal? MinRefPrice2016 { get; set; }

        [StringLength(100)]
        public string FixPrice_a_11_16_Reason { get; set; }

        [StringLength(500)]
        public string BritishPriceNote { get; set; }

        public decimal? IntRef_AvgRetPricePackage { get; set; }
    }
}
