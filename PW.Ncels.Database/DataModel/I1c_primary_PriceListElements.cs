namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_PriceListElements
    {
        public Guid Id { get; set; }

        public Guid? PriceListId { get; set; }

        [StringLength(500)]
        public string PriceListName { get; set; }

        [StringLength(500)]
        public string PriceListNameKz { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public Guid refApplication { get; set; }
    }
}
