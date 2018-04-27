using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Primitives.IUser;

namespace Teme.Shared.Data.Context
{
    public class AuthUser : BaseEntity
    {
        [Required] public string UserName { get; set; }
        public string Bin { get; set; }
        public bool? HasIin { get; set; }
        [Required] public string Iin { get; set; }
        [Required] public string Pwdhash { get; set; }
    [NotMapped]
        [Required] public virtual IEnumerable<string> Scopes { get; set; }
        public string CompanyName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required] public string Email { get; set; }
        public string UserType { get; set; }
    }
}
