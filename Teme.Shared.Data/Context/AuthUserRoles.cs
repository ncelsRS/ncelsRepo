using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class AuthUserRoles:BaseEntity
    {
        public Role Role { get; set; }
        public AuthUser User { get; set; }
    }
}
