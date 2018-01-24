namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_ObkApplicationPaymentState
    {
        public Guid Id { get; set; }

        public bool? IsPaid { get; set; }

        public DateTime? PaymentDatetime { get; set; }

        public Guid? refContractId { get; set; }

        public Guid? ZBKCopyId { get; set; }
    }
}
