namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_mt_parts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int register_id { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string name_kz { get; set; }

        public int? part_number { get; set; }

        [StringLength(500)]
        public string model { get; set; }

        [StringLength(500)]
        public string specification { get; set; }

        [StringLength(500)]
        public string specification_kz { get; set; }

        public int? producer_id { get; set; }

        public int? country_id { get; set; }

        [StringLength(2000)]
        public string producer_name { get; set; }

        [StringLength(500)]
        public string country_name { get; set; }

        [StringLength(2000)]
        public string producer_name_kz { get; set; }

        [StringLength(500)]
        public string country_name_kz { get; set; }

        public virtual sr_producers sr_producers { get; set; }

        public virtual sr_register_mt sr_register_mt { get; set; }
    }
}
