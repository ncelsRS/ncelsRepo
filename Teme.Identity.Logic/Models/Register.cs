using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Teme.Identity.Logic.Models
{
    public class Register
    {
        public string Bin { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string Iin { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
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
