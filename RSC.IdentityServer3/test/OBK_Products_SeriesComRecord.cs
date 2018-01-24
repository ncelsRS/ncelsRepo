namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Products_SeriesComRecord
    {
        public Guid Id { get; set; }

        public Guid CommentId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(500)]
        public string ValueField { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        [StringLength(500)]
        public string DisplayField { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_Products_SeriesCom OBK_Products_SeriesCom { get; set; }
    }
}
