namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sr_register_substances
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int register_id { get; set; }

        public int substance_type_id { get; set; }

        public int substance_id { get; set; }

        public decimal? substance_count { get; set; }

        public long? measure_id { get; set; }

        public int? producer_id { get; set; }

        public long? country_id { get; set; }

        public int? nd_type_id { get; set; }

        [StringLength(250)]
        public string comment { get; set; }

        public virtual sr_nd_types sr_nd_types { get; set; }

        public virtual sr_register sr_register { get; set; }

        public virtual sr_substance_types sr_substance_types { get; set; }

        public virtual sr_substances sr_substances { get; set; }
    }
}
