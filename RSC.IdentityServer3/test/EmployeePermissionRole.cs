namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmployeePermissionRole
    {
        public int Id { get; set; }

        public Guid EmployeeId { get; set; }

        public int PermissionRoleId { get; set; }
    }
}
