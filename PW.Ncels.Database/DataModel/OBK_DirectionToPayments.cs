namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_DirectionToPayments
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Number { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime DirectionDate { get; set; }

        public Guid ContractId { get; set; }

        public decimal? TotalPrice { get; set; }

        public Guid? PayerId { get; set; }

        [StringLength(4000)]
        public string PayerValue { get; set; }

        public Guid CreateEmployeeId { get; set; }

        [Required]
        [StringLength(1024)]
        public string CreateEmployeeValue { get; set; }

        public DateTime? DeleteDate { get; set; }

        public bool? IsDeleted { get; set; }

        public Guid StatusId { get; set; }

        [StringLength(512)]
        public string InvoiceNumber { get; set; }

        [StringLength(512)]
        public string InvoiceNumber1C { get; set; }

        public DateTime? InvoiceDatetime1C { get; set; }

        public bool IsPaid { get; set; }

        public bool IsNotFullPaid { get; set; }

        public DateTime? PaymentDatetime { get; set; }

        public decimal? PaymentValue { get; set; }

        public decimal? PaymentBill { get; set; }

        [StringLength(20)]
        public string SendNotification { get; set; }

        public Guid? ZBKCopy_id { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_Contract OBK_Contract { get; set; }

        public virtual OBK_Declarant OBK_Declarant { get; set; }

        public virtual OBK_DirectionSignData OBK_DirectionSignData { get; set; }
    }
}
