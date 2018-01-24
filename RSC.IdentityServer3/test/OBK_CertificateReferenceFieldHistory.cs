namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_CertificateReferenceFieldHistory
    {
        public int Id { get; set; }

        public string Controlld { get; set; }

        public Guid? UserId { get; set; }

        public string ValueField { get; set; }

        public string DisplayField { get; set; }

        public DateTime? CreateDate { get; set; }

        public Guid? OBK_CertificateReferenceId { get; set; }

        public virtual OBK_CertificateReference OBK_CertificateReference { get; set; }
    }
}
