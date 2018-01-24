namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public Guid AuthorId { get; set; }

        [Required]
        [StringLength(4000)]
        public string AuthorValue { get; set; }

        public Guid refObjectId { get; set; }

        public Guid? refParentComment { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
