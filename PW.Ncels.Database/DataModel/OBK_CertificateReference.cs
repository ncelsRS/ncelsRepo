namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_CertificateReference
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_CertificateReference()
        {
            OBK_CertificateReferenceFieldHistory = new HashSet<OBK_CertificateReferenceFieldHistory>();
        }

        public Guid Id { get; set; }

        public string Number { get; set; }

        public string CertificateNumber { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? CertificateCountryId { get; set; }

        [StringLength(500)]
        public string CertificateOrganization { get; set; }

        public int CertificateTypeId { get; set; }

        public DateTime? LastInspection { get; set; }

        public short? CertificateValidityTypeId { get; set; }

        [StringLength(300)]
        public string AttachPath { get; set; }

        [StringLength(500)]
        public string CertificateProducer { get; set; }

        public virtual OBK_CertificateValidityType OBK_CertificateValidityType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_CertificateReferenceFieldHistory> OBK_CertificateReferenceFieldHistory { get; set; }
    }
}
