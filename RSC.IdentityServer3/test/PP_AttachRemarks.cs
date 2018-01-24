namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PP_AttachRemarks
    {
        public Guid Id { get; set; }

        public Guid PriceProjectId { get; set; }

        public Guid AttachPriceDicId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Note { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        public virtual PriceProject PriceProject { get; set; }
    }
}
