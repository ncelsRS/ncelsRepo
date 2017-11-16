using System;
using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.Models
{
	public class OBKCertificateReferenceModel
    {
        public Guid? Id { get; set; }
        public string Number { get; set; }
        public string CertificateNumber { get; set; }
        [Required]
        public DateTime? StartDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
        public Guid CertificateCountryId { get; set; }
        public string CertificateCountry { get; set; }
        public string CertificateOrganization { get; set; }
        public int CertificateTypeId { get; set; }
        public string CertificateType { get; set; }
        public DateTime? LastInspection { get; set; }
        public string CertificateValidityType { get; set; }
        public string CertificateValidityCode { get; set; }
        public Guid? DocumentId { get; set; }
    }
}