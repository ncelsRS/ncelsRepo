namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceProjectFieldHistory")]
    public partial class PriceProjectFieldHistory
    {
        public long Id { get; set; }

        public Guid PriceProjectId { get; set; }

        [Required]
        [StringLength(500)]
        public string ControlId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(500)]
        public string ValueField { get; set; }

        [StringLength(500)]
        public string DisplayField { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual PriceProject PriceProject { get; set; }
    }
}
