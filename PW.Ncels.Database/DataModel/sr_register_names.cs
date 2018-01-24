namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_names
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        public int register_id { get; set; }

        [StringLength(3000)]
        public string name { get; set; }

        [StringLength(4000)]
        public string name_kz { get; set; }

        [StringLength(3000)]
        public string name_eng { get; set; }

        public long country_id { get; set; }

        public virtual sr_countries sr_countries { get; set; }

        public virtual sr_register sr_register { get; set; }
    }
}
