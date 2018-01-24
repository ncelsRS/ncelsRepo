namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_CertificateOfCompletion
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public Guid? ContractId { get; set; }

        public Guid? AssessmentDeclarationId { get; set; }

        [StringLength(512)]
        public string InvoiceNumber1C { get; set; }

        public DateTime? InvoiceDatetime1C { get; set; }

        public decimal? TotalPrice { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? SendDate { get; set; }

        [StringLength(500)]
        public string ActNumber1C { get; set; }

        public DateTime? ActDate1C { get; set; }

        public bool? ActReturnedBack { get; set; }

        public bool? SendNotification { get; set; }

        public Guid? ZBKCopyId { get; set; }

        public virtual OBK_AssessmentDeclaration OBK_AssessmentDeclaration { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }
    }
}
