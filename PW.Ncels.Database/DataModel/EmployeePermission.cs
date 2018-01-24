namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmployeePermission
    {
        public Guid EmployeeId { get; set; }

        [StringLength(4000)]
        public string PermissionKey { get; set; }

        [StringLength(4000)]
        public string PermissionValue { get; set; }

        public int Id { get; set; }

        [StringLength(4000)]
        public string GroupName { get; set; }
    }
}
