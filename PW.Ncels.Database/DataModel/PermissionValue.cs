namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PermissionValue
    {
        public int Id { get; set; }

        [StringLength(4000)]
        public string PermissionKey { get; set; }

        [StringLength(4000)]
        public string Value { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        public bool IsDefault { get; set; }
    }
}
