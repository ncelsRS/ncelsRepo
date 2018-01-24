namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_producers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int register_id { get; set; }

        public int producer_id { get; set; }

        public int producer_type_id { get; set; }

        public long country_id { get; set; }

        public bool language_sign { get; set; }

        public virtual sr_countries sr_countries { get; set; }

        public virtual sr_producer_types sr_producer_types { get; set; }

        public virtual sr_producers sr_producers { get; set; }

        public virtual sr_register sr_register { get; set; }
    }
}
