namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class obk_exchangerate
    {
        public long id { get; set; }

        public int currency_id { get; set; }

        public decimal rate { get; set; }

        [Column(TypeName = "date")]
        public DateTime rate_date { get; set; }

        public virtual obk_currencies obk_currencies { get; set; }
    }
}
