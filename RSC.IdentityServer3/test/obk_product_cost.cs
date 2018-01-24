namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class obk_product_cost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [Column(TypeName = "money")]
        public decimal cost { get; set; }

        public int currency_id { get; set; }

        public DateTime? date_cost { get; set; }

        public virtual obk_products obk_products { get; set; }
    }
}
