namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_PharmaList
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UserId { get; set; }

        public Guid RegionId { get; set; }

        public int Type { get; set; }

        [StringLength(200)]
        public string Phone { get; set; }

        public int? RegisterId { get; set; }

        public int? RegisterDfId { get; set; }

        [StringLength(4000)]
        public string MnnName { get; set; }

        [StringLength(4000)]
        public string TradeName { get; set; }

        [StringLength(4000)]
        public string RegNumber { get; set; }

        [StringLength(4000)]
        public string DrugForm { get; set; }

        [StringLength(4000)]
        public string Dosage { get; set; }

        [StringLength(4000)]
        public string Concentration { get; set; }

        [StringLength(100)]
        public string Box_count { get; set; }

        [StringLength(4000)]
        public string Volume { get; set; }

        [StringLength(4000)]
        public string Country { get; set; }

        [StringLength(4000)]
        public string Manufacturer { get; set; }

        public decimal? PricePackage { get; set; }

        public decimal? PriceUnit { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
