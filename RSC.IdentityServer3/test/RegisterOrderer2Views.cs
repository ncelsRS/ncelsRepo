namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RegisterOrderer2Views
    {
        public int? IntId { get; set; }

        public Guid? Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int regId { get; set; }

        [Column("_int_name")]
        [StringLength(255)]
        public string C_int_name { get; set; }

        public string substance { get; set; }

        [StringLength(286)]
        public string volume { get; set; }

        [StringLength(500)]
        public string dosage_comment { get; set; }

        [StringLength(3000)]
        public string name { get; set; }

        [Column("_dosage_form_name")]
        [StringLength(500)]
        public string C_dosage_form_name { get; set; }

        [StringLength(500)]
        public string concentration { get; set; }

        [StringLength(50)]
        public string reg_number { get; set; }

        [Column("_producer_name")]
        [StringLength(1000)]
        public string C_producer_name { get; set; }

        [Column("_country_name")]
        [StringLength(255)]
        public string C_country_name { get; set; }

        [Column("_atc_code")]
        [StringLength(10)]
        public string C_atc_code { get; set; }

        [StringLength(281)]
        public string dosage_value { get; set; }

        [StringLength(4000)]
        public string um { get; set; }

        [StringLength(301)]
        public string box_name1 { get; set; }

        [StringLength(301)]
        public string box_name2 { get; set; }

        [StringLength(100)]
        public string box_count { get; set; }

        public int? dfId { get; set; }
    }
}
