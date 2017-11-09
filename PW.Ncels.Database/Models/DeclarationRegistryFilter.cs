using System;

namespace PW.Ncels.Database.Models
{
    public class DeclarationRegistryFilter
    {
        public string Number { get; set; }
        public DateTime? DeclarationDateFrom { get; set; }
        public DateTime? DeclarationDateTo { get; set; }
        public int? TypeId { get; set; }
        public string ProducerName { get; set; }
        public string ProducerCountry { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantCountry { get; set; }
        public string HolderName { get; set; }
        public string HolderCountry { get; set; }
        public string DrugName { get; set; }
        public DateTime? StageStartDateFrom { get; set; }
        public DateTime? StageStartDateTo { get; set; }
        public DateTime? StageEndDateFrom { get; set; }
        public DateTime? StageEndDateTo { get; set; }
        public Guid? StageId { get; set; }
        public Guid? StageResultId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ExpertId { get; set; }
        public string Mnn { get; set; }
    }
}