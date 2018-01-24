namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIC_OrphanDrugs
    {
        [Key]
        [Column(Order = 0)]
        public long Id { get; set; }

        public string Name { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreateDatetime { get; set; }
    }
}
