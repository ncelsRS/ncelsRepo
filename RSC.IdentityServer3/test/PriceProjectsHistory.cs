namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceProjectsHistory")]
    public partial class PriceProjectsHistory
    {
        public long Id { get; set; }

        public Guid? UserId { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        public int StatusId { get; set; }

        public string Note { get; set; }

        public Guid PriceProjectId { get; set; }

        public string XmlSign { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual PriceProject PriceProject { get; set; }
    }
}
