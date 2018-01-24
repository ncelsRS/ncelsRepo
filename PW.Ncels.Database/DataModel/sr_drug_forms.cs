namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_drug_forms
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int register_id { get; set; }

        public int? pr_box_id { get; set; }

        public int? sec_box_id { get; set; }

        [StringLength(100)]
        public string box_count { get; set; }

        [StringLength(4000)]
        public string full_name { get; set; }

        [StringLength(1000)]
        public string specification { get; set; }

        [StringLength(4000)]
        public string full_name_kz { get; set; }

        public bool? complete_sign { get; set; }

        public int? user_id { get; set; }

        [StringLength(50)]
        public string pr_box_count { get; set; }

        [StringLength(50)]
        public string sec_box_count { get; set; }

        public int? inter_box_id { get; set; }

        [StringLength(50)]
        public string inter_box_count { get; set; }

        public virtual sr_register sr_register { get; set; }

        public virtual sr_register_boxes sr_register_boxes { get; set; }

        public virtual sr_register_boxes sr_register_boxes1 { get; set; }
    }
}
