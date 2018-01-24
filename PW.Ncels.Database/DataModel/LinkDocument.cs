namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LinkDocument
    {
        public Guid Id { get; set; }

        public Guid DocumentId { get; set; }

        public Guid OwnerId { get; set; }

        [StringLength(400)]
        public string PropertyName { get; set; }
    }
}
