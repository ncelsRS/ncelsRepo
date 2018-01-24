namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SrReestrView")]
    public partial class SrReestrView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int n { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? df_id { get; set; }

        [Column("_producer_name_kz")]
        [StringLength(1000)]
        public string C_producer_name_kz { get; set; }

        [Column("_producer_name")]
        [StringLength(1000)]
        public string C_producer_name { get; set; }

        [Column("_producer_name_en")]
        [StringLength(500)]
        public string C_producer_name_en { get; set; }

        [Column("_country_name")]
        [StringLength(255)]
        public string C_country_name { get; set; }

        [Column("_country_Id")]
        public Guid? C_country_Id { get; set; }

        [StringLength(4000)]
        public string name_kz { get; set; }

        [StringLength(3000)]
        public string name { get; set; }

        [StringLength(50)]
        public string reg_number { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "smalldatetime")]
        public DateTime reg_date { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int reg_action_id { get; set; }

        [StringLength(50)]
        public string reg_action_name { get; set; }

        [StringLength(100)]
        public string reg_action_name_kz { get; set; }

        [Column("_int_name")]
        [StringLength(255)]
        public string C_int_name { get; set; }

        public string SubstanceName { get; set; }

        [Column("_dosage_form_name")]
        [StringLength(500)]
        public string C_dosage_form_name { get; set; }

        [Column("_dosage_form_name_kz")]
        [StringLength(1000)]
        public string C_dosage_form_name_kz { get; set; }

        [StringLength(281)]
        public string dosage_value { get; set; }

        [StringLength(500)]
        public string concentration { get; set; }

        [Column("_atc_code")]
        [StringLength(10)]
        public string C_atc_code { get; set; }

        [StringLength(15)]
        public string description { get; set; }

        [StringLength(4000)]
        public string um { get; set; }

        public Guid? umId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? expiration_date { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int type { get; set; }

        [StringLength(50)]
        public string type_name { get; set; }

        [StringLength(100)]
        public string type_name_kz { get; set; }

        [StringLength(286)]
        public string volume { get; set; }

        [StringLength(255)]
        public string volume_measure { get; set; }

        [StringLength(1000)]
        public string owner_name_kz { get; set; }

        [StringLength(1000)]
        public string owner_name_ru { get; set; }

        [StringLength(500)]
        public string owner_name_en { get; set; }

        [StringLength(301)]
        public string box_name1 { get; set; }

        [StringLength(301)]
        public string box_name2 { get; set; }

        [StringLength(100)]
        public string box_count { get; set; }

        public Guid? owner_country_id { get; set; }

        public Guid? degree_risk_id { get; set; }
    }
}
