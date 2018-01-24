namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileLinksCategoryComRecord")]
    public partial class FileLinksCategoryComRecord
    {
        public long Id { get; set; }

        public Guid CommentId { get; set; }

        public Guid? UserId { get; set; }

        public DateTime CreateDate { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual FileLinksCategoryCom FileLinksCategoryCom { get; set; }
    }
}
