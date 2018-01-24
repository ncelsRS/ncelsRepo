namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PrismReport
    {
        public Guid Id { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Category { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        public byte[] Report { get; set; }

        public int Type { get; set; }
    }
}
