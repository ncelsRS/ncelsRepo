namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RegisterOrdererView")]
    public partial class RegisterOrdererView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReestrId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1000)]
        public string MnnName { get; set; }

        [StringLength(652)]
        public string Characteristic { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(500)]
        public string DrugForm { get; set; }

        [StringLength(50)]
        public string Concentration { get; set; }

        [StringLength(100)]
        public string Dosage { get; set; }

        [StringLength(4000)]
        public string TradeName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string RegNumber { get; set; }

        [Key]
        [Column(Order = 4, TypeName = "smalldatetime")]
        public DateTime RegDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? RegDateExpire { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string AtxCode { get; set; }

        [StringLength(1000)]
        public string Manufacturer { get; set; }

        [StringLength(50)]
        public string Measure { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(200)]
        public string Country { get; set; }

        public decimal? substance_count { get; set; }

        public int? unit_count { get; set; }

        public decimal? volume { get; set; }

        [StringLength(500)]
        public string dosage_comment { get; set; }
    }
}
