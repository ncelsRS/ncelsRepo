namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequestOrderListView")]
    public partial class RequestOrderListView
    {
        [Key]
        [Column(Order = 0)]
        public Guid OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderType { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderYear { get; set; }

        [StringLength(500)]
        public string OrderNumber { get; set; }

        [Key]
        [Column(Order = 3)]
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

        public int? State { get; set; }

        public decimal? LimitPriceTn { get; set; }

        public decimal? LimitPriceMnn { get; set; }

        [StringLength(4000)]
        public string Country { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [Key]
        [Column(Order = 5)]
        [StringLength(5)]
        public string Status { get; set; }

        [StringLength(4000)]
        public string Reason { get; set; }

        public int? RegisterDfId { get; set; }

        [StringLength(100)]
        public string box_count { get; set; }
    }
}
