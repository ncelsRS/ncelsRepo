namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PermissionKey
    {
        public int Id { get; set; }

        [StringLength(1000)]
        public string Key { get; set; }

        public int Type { get; set; }

        [StringLength(4000)]
        public string KeyName { get; set; }

        [StringLength(4000)]
        public string KeyDescription { get; set; }

        [StringLength(4000)]
        public string GroupName { get; set; }
    }
}
