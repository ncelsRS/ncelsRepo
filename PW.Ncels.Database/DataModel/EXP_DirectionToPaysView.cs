namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DirectionToPaysView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(512)]
        public string Number { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime CreateDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime DirectionDate { get; set; }

        public string ApplicationNumbers { get; set; }

        public string ApplicationDates { get; set; }

        public string RegistrationForm { get; set; }

        public string TradeNameKz { get; set; }

        public string TradeNameRu { get; set; }

        public string TradeNameEn { get; set; }

        public string DrugFormKz { get; set; }

        public string DrugFormRu { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(1)]
        public string DrugFormEn { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(1)]
        public string ManufactureKz { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(1)]
        public string ManufactureRu { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(1)]
        public string ManufactureEn { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(1)]
        public string ExpertDisplayName { get; set; }

        public decimal? Price { get; set; }

        public decimal? TotalPrice { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(1)]
        public string Currency { get; set; }

        public string PriceListNameKz { get; set; }

        public string PriceListNameRu { get; set; }

        public string PriceListNameEn { get; set; }

        [Key]
        [Column(Order = 10)]
        [StringLength(1)]
        public string PriceListValue { get; set; }

        [StringLength(500)]
        public string InvoiceNumber1C { get; set; }

        public DateTime? InvoiceDatetime1C { get; set; }

        public DateTime? PaymentDatetime1C { get; set; }

        public decimal? PaymentValue1C { get; set; }

        public string PaymentComment1C { get; set; }

        [Key]
        [Column(Order = 11)]
        public Guid StatusId { get; set; }

        [StringLength(4000)]
        public string StatusCode { get; set; }

        [StringLength(4000)]
        public string StatusName { get; set; }

        [StringLength(4000)]
        public string StatusNameKz { get; set; }

        [Key]
        [Column(Order = 12)]
        public Guid CreateEmployeeId { get; set; }

        public Guid? ExecutorId { get; set; }

        [StringLength(4000)]
        public string ExecutorName { get; set; }

        [StringLength(4000)]
        public string Comment { get; set; }

        public int? PageCount { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Type { get; set; }
    }
}
