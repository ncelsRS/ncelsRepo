namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_DirectionSignData
    {
        [Key]
        public Guid DirectionToPaymentId { get; set; }

        public Guid? ExecutorId { get; set; }

        [Column(TypeName = "ntext")]
        public string ExecutorSign { get; set; }

        public DateTime? ExecutorSignDate { get; set; }

        public Guid? ChiefAccountantId { get; set; }

        [Column(TypeName = "ntext")]
        public string ChiefAccountantSign { get; set; }

        public DateTime? ChiefAccountantSignDate { get; set; }

        public virtual OBK_DirectionToPayments OBK_DirectionToPayments { get; set; }
    }
}
