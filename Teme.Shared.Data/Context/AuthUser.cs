using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class AuthUser: BaseEntity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

    }
}
