namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ref_Limits
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

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

        [Key]
        [Column(Order = 1)]
        public decimal Cost { get; set; }
    }
}
