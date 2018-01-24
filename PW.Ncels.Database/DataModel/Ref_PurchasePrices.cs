namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ref_PurchasePrices
    {
        public long Id { get; set; }

        [StringLength(32)]
        public string SKP { get; set; }

        [StringLength(512)]
        public string Name { get; set; }

        [StringLength(512)]
        public string TradeName { get; set; }

        [StringLength(512)]
        public string DosageForm { get; set; }

        [StringLength(128)]
        public string Unit { get; set; }

        public float? Packing { get; set; }

        [StringLength(512)]
        public string Producer { get; set; }

        public float? Price { get; set; }
    }
}
