namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_mt_kz
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(4000)]
        public string purpose_kz { get; set; }

        [StringLength(4000)]
        public string use_area_kz { get; set; }

        [StringLength(4000)]
        public string description_kz { get; set; }

        public virtual sr_register_mt sr_register_mt { get; set; }
    }
}
