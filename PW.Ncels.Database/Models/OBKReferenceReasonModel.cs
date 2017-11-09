using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class OBKReferenceReasonModel
    {
        public int Id { get; set; }
        
        public string Code { get; set; }
        [Required]
        public string NameRu { get; set; }
        [Required]
        public string NameKz { get; set; }
        [Required]
        public bool ExpertiseResult { get; set; }
    }
}