namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PermissionRoleKey
    {
        public int Id { get; set; }

        public int? PermissionRoleId { get; set; }

        [StringLength(1000)]
        public string PermissionKey { get; set; }

        [StringLength(1000)]
        public string PermissionValue { get; set; }
    }
}
