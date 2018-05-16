using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class OBKReferenceContractDocTypeModel
    {
        public Guid? Id { get; set; }
        
        [Required]
        public string NameRu { get; set; }
        [Required]
        public string NameKz { get; set; }
        [Required]
        public string NameGenitiveRu { get; set; }
        [Required]
        public string NameGenitiveKz { get; set; }

    }
}