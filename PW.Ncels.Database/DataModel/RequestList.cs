namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestList")]
    public partial class RequestList
    {
        public Guid Id { get; set; }

        public int? ReestrId { get; set; }

        [StringLength(4000)]
        public string MnnName { get; set; }

        [StringLength(4000)]
        public string Characteristic { get; set; }

        [StringLength(4000)]
        public string DrugForm { get; set; }

        [StringLength(4000)]
        public string Concentration { get; set; }

        [StringLength(4000)]
        public string Dosage { get; set; }

        [StringLength(4000)]
        public string TradeName { get; set; }

        [StringLength(4000)]
        public string RegNumber { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? RegDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? RegDateExpire { get; set; }

        [StringLength(4000)]
        public string AtxCode { get; set; }

        [StringLength(4000)]
        public string Manufacturer { get; set; }

        [StringLength(4000)]
        public string Measure { get; set; }

        public int? Type { get; set; }

        public int? State { get; set; }

        public decimal? LimitPriceTn { get; set; }

        public decimal? LimitPriceMnn { get; set; }

        [StringLength(4000)]
        public string Country { get; set; }

        public int Number { get; set; }

        [StringLength(4000)]
        public string Applicant { get; set; }

        [StringLength(4000)]
        public string substance_count { get; set; }

        [StringLength(4000)]
        public string unit_count { get; set; }

        [StringLength(4000)]
        public string volume { get; set; }

        [StringLength(4000)]
        public string dosage_comment { get; set; }

        [StringLength(4000)]
        public string Mark { get; set; }

        public int? RegisterDfId { get; set; }

        public Guid? RequestOrderId { get; set; }

        [StringLength(100)]
        public string box_count { get; set; }

        public virtual RequestOrder RequestOrder { get; set; }
    }
}
