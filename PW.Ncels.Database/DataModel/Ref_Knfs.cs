namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ref_Knfs
    {
        [StringLength(4000)]
        public string Code { get; set; }

        [StringLength(4000)]
        public string GroupFarm { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Form { get; set; }

        [StringLength(4000)]
        public string Number { get; set; }

        public decimal Cost { get; set; }

        public Guid Id { get; set; }

        [StringLength(4000)]
        public string Characteristic { get; set; }

        [StringLength(4000)]
        public string Measure { get; set; }

        [StringLength(4000)]
        public string Tn { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateExpiry { get; set; }

        [StringLength(4000)]
        public string Dosage { get; set; }

        public decimal CostTn { get; set; }

        public int State { get; set; }

        public int Type { get; set; }
    }
}
