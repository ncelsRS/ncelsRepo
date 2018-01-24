namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PriceProjectArchiveDetail
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string MnnCode { get; set; }

        [StringLength(500)]
        public string MnnRu { get; set; }

        [StringLength(1000)]
        public string DrugDescription { get; set; }

        [StringLength(1000)]
        public string DrugName { get; set; }

        [StringLength(500)]
        public string CountPackage { get; set; }

        [StringLength(500)]
        public string ProducerName { get; set; }

        [StringLength(4000)]
        public string ProducerCountry { get; set; }

        [StringLength(500)]
        public string RegNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RegDate { get; set; }

        public DateTime? RegEndDate { get; set; }

        [StringLength(500)]
        public string ProxyName { get; set; }

        [StringLength(500)]
        public string RequesterContacts { get; set; }

        public Guid? LsTypeDicId { get; set; }

        [StringLength(4000)]
        public string LsTypeName { get; set; }

        public decimal? MarginalPriceTn622 { get; set; }

        public decimal? CipPrice { get; set; }

        [StringLength(4000)]
        public string CipPriceCur { get; set; }

        public decimal? ManufacturerPrice { get; set; }

        [StringLength(4000)]
        public string ManufacturerPriceCur { get; set; }

        public decimal? RefPrice { get; set; }

        [StringLength(4000)]
        public string RefPriceCur { get; set; }

        public decimal? OwnerPrice { get; set; }

        [StringLength(4000)]
        public string OwnerPriceCur { get; set; }

        public decimal? BasePrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BasePriceDate { get; set; }

        public decimal? LimitCost { get; set; }

        public decimal? AvgObkCost { get; set; }

        public decimal? AvgOptCost { get; set; }

        public decimal? AvgRznCost { get; set; }

        public decimal? ZakupCost { get; set; }

        public decimal? MinimalCost { get; set; }

        public decimal? TalkCipPrice { get; set; }

        [StringLength(4000)]
        public string TalkCipPriceCur { get; set; }

        public decimal? TalkManufacturerPrice { get; set; }

        [StringLength(4000)]
        public string TalkManufacturerPriceCur { get; set; }

        public decimal? TalkefPrice { get; set; }

        [StringLength(4000)]
        public string TalkefPriceCur { get; set; }

        public decimal? TalkOwnerPrice { get; set; }

        [StringLength(4000)]
        public string TalkOwnerPriceCur { get; set; }

        public decimal? TalkUnitPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TalkPriceDate { get; set; }

        public decimal? MarginalPriceMnn2016 { get; set; }

        public decimal? PriceDropPercent { get; set; }

        public bool? IsConformity639 { get; set; }

        [StringLength(500)]
        public string Conformity639Note { get; set; }

        public decimal? FixPrice_a_11_16 { get; set; }

        public decimal? PriceDifference2016 { get; set; }

        public decimal? IntRef_AvgInPricePackage2015 { get; set; }

        [StringLength(4000)]
        public string IntRef_AvgInPricePackage2015Cur { get; set; }

        public decimal? IntRef_AvgInPricePackage { get; set; }

        public decimal? IntRef_RetailAktobe { get; set; }

        public decimal? MinRefPrice2016 { get; set; }

        public decimal? MinRefPriceCoef { get; set; }

        public decimal? RegNcelsPrice_11_16 { get; set; }

        public decimal? RegMzsrPrice_11_16 { get; set; }

        public decimal? FinalPrice { get; set; }

        public decimal? FinalPricePercent { get; set; }

        public decimal? FinalFixPrice { get; set; }

        public decimal? FinalMarginalPriceTn { get; set; }

        public decimal? ProjectPrice2017 { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedDate { get; set; }

        public int? RequestOrderType { get; set; }

        public int? RequestOrderYear { get; set; }

        public decimal? BritishPrice { get; set; }

        public decimal? BelarusPrice { get; set; }

        public decimal? CzechPrice { get; set; }

        public decimal? HungaryPrice { get; set; }

        public decimal? LatviaPrice { get; set; }

        public decimal? RfPrice { get; set; }

        public decimal? AustriaPrice { get; set; }

        public decimal? UkrainePrice { get; set; }

        public decimal? TurkeyPrice { get; set; }
    }
}
