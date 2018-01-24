namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileLinkView")]
    public partial class FileLinkView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreateDate { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4000)]
        public string FileName { get; set; }

        public int? Version { get; set; }

        public Guid? DocumentId { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? ParentId { get; set; }

        [StringLength(4000)]
        public string Category { get; set; }

        [StringLength(4000)]
        public string ParentFileName { get; set; }
    }
}
