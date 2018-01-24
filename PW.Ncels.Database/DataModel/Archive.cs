namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Archive
    {
        public int Id { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [StringLength(4000)]
        public string ConnectionString { get; set; }

        [StringLength(4000)]
        public string DbName { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsCurrent { get; set; }

        [StringLength(4000)]
        public string Owner { get; set; }

        [StringLength(4000)]
        public string Path { get; set; }
    }
}
