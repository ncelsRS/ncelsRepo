using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class OBKReferenceNomenclatureModel
    {
        public int Id { get; set; }

        public string Code { get; set; }
        [Required]
        public string NameRu { get; set; }
        [Required]
        public string NameKz { get; set; }
    }
}