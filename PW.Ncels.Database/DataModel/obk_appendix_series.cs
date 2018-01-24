namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class obk_appendix_series
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        public long appendix_id { get; set; }

        public long product_id { get; set; }

        public virtual obk_appendix obk_appendix { get; set; }

        public virtual obk_products obk_products { get; set; }
    }
}
