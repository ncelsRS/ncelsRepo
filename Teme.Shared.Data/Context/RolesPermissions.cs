using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class RolesPermissions : BaseEntity
    {
        public string PermissionCode { get; set; }
        //public virtual ICollection<Role> Roles { get; set; }
    }
}
