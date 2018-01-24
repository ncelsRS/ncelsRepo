namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DirectionToPays_PriceList
    {
        [Key]
        [Column(Order = 0)]
        public Guid DirectionToPayId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid PriceListId { get; set; }

        public decimal? Price { get; set; }

        public int? Count { get; set; }

        public decimal? Total { get; set; }

        public virtual EXP_DirectionToPays EXP_DirectionToPays { get; set; }

        public virtual EXP_PriceList EXP_PriceList { get; set; }
    }
}
