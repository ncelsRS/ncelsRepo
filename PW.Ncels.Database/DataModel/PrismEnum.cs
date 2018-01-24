namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PrismEnum
    {
        public int? Key { get; set; }

        [StringLength(510)]
        public string Value { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        public Guid Id { get; set; }
    }
}
