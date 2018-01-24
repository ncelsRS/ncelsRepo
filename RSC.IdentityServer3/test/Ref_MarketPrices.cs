namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ref_MarketPrices
    {
        public long Id { get; set; }

        [StringLength(1024)]
        public string Name { get; set; }

        public float? DosageForm { get; set; }

        [StringLength(512)]
        public string Producer { get; set; }

        [StringLength(512)]
        public string Country { get; set; }

        [StringLength(64)]
        public string Number { get; set; }

        public DateTime? RegistrationDatetime { get; set; }

        public DateTime? ExpireDatetime { get; set; }

        public float? Price { get; set; }

        public long refSource { get; set; }

        public DateTime? CreateDatetime { get; set; }

        [StringLength(50)]
        public string refSourceName { get; set; }

        public int Type { get; set; }
    }
}
