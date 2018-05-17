using System.Collections.Generic;

namespace Teme.Shared.Data.Context
{
    public class Role : BaseEntity
    {
        /// <summary>
        /// Наименование роли, которое будет переводиться ангуларом
        /// </summary>
        public string RoleName { get; set; }
        public virtual ICollection<AuthUserRoles> RolesUsers { get; set; }
        public virtual ICollection<RolesPermissions> Permissions { get; set; }

        public Role()
        {
            RolesUsers = new HashSet<AuthUserRoles>();
        }
    }
}
