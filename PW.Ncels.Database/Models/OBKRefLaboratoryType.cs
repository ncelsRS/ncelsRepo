using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class OBKRefLaboratoryType
    {
        public Guid Id { get; set; }
        [Required]
        public string NameRu { get; set; }
        [Required]
        public string NameKz { get; set; }
        [Required]
        public bool isDeleted { get; set; }
    }
}