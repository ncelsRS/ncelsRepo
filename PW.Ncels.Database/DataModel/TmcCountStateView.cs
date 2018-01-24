namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcCountStateView")]
    public partial class TmcCountStateView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [StringLength(450)]
        public string Code { get; set; }

        [Key]
        [Column(Order = 1)]
        public decimal CountFact { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal CountConvert { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal IssuedCount { get; set; }

        public decimal? UsedCount { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime CreatedDate { get; set; }
    }
}
