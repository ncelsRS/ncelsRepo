using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class OBKReferenceValAdTaxModel
    {
        public Guid? Id { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        [RegularExpression("^[0-9]{4,10}$", ErrorMessage ="В ведите правильный формат года")]
        public int Year { get; set; }
    }
}