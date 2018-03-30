using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Teme.Shared.Data.Context
{
    //[Table("AuthUsers")]
    public class AuthUser: BaseEntity
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

    }
}
