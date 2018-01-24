namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_ApplicationPartPaymentState
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string ApplicationNumber { get; set; }

        public DateTime? ApplicationDatetime { get; set; }

        public DateTime? PaymentDatetime { get; set; }

        public string PaymentComment { get; set; }

        public decimal? PaymentValue { get; set; }

        public decimal? PaymentSurcharge { get; set; }
    }
}
