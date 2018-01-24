namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_boxes_rk_ls
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public bool? state_sign { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? state_date { get; set; }

        public virtual sr_register_boxes sr_register_boxes { get; set; }
    }
}
