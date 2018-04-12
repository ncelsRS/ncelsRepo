using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Teme.Shared.Data.Context
{
    public class AuthUser: BaseEntity
    {
        public string Bin { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string Iin { get; set; }
        [Required]
        public string Pwdhash { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Email { get; set; }
        public string UserType { get; set; }
        public bool? HasIin { get; set; }

    }
}
