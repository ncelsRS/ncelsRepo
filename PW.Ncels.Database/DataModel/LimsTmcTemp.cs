namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LimsTmcTemp")]
    public partial class LimsTmcTemp
    {
        [Key]
        [Column(Order = 0)]
        public Guid TmcId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid TmcInId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public decimal? CountRequest { get; set; }

        public decimal? CountReceived { get; set; }

        public bool? IsSelected { get; set; }

        public virtual TmcIn TmcIn { get; set; }

        public virtual Tmc Tmc { get; set; }
    }
}
