using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class OBKReferencePriceListModel
    {
        public Guid? Id { get; set; }

        [Required]
        public int TypeId { get; set; }
        public string Type { get; set; }
        [Required]
        public string NameRu { get; set; }
        [Required]
        public string NameKz { get; set; }
        [Required]
        public Guid UnitId { get; set; }
        public string Unit { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public Guid ServiceTypeId { get; set; }
        public string ServiceType { get; set; }
        [Required]
        public Guid DegreeRiskId { get; set; }
        public string Degree { get; set; }
    }
}