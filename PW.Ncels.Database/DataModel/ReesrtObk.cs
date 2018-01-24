namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ReesrtObk")]
    public partial class ReesrtObk
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(2000)]
        public string name { get; set; }

        [StringLength(1000)]
        public string producer_name { get; set; }

        [StringLength(150)]
        public string country_name { get; set; }

        [StringLength(20)]
        public string tnved_code { get; set; }

        [StringLength(20)]
        public string kpved_code { get; set; }

        [StringLength(2000)]
        public string register_nd { get; set; }

        [Column(TypeName = "money")]
        public decimal? cost { get; set; }

        [StringLength(50)]
        public string currency_name { get; set; }

        public decimal? costExch { get; set; }

        public long? register_id { get; set; }

        public long? drug_form_id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? reg_date { get; set; }
    }
}
