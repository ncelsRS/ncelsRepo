namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AccessDocument
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ObjectId { get; set; }

        [StringLength(4000)]
        public string PropertyName { get; set; }

        public virtual Document Document { get; set; }
    }
}
