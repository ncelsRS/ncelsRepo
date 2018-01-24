namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceProjectArchiveView")]
    public partial class PriceProjectArchiveView
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

        public decimal? BasePrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BasePriceDate { get; set; }

        public decimal? TalkPrice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TalkPriceDate { get; set; }

        public decimal? BritishPrice { get; set; }

        public decimal? LimitCost { get; set; }

        public decimal? MinRefPrice2016 { get; set; }

        public decimal? FinalPrice { get; set; }

        public decimal? FinalFixPrice { get; set; }

        public decimal? FinalMarginalPriceTn { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedDate { get; set; }

        public int? RequestOrderType { get; set; }

        public int? RequestOrderYear { get; set; }
    }
}
