namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKTransferRegisterView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [StringLength(255)]
        public string Declarant { get; set; }

        [StringLength(50)]
        public string ConclusionNumber { get; set; }

        public DateTime? SendDate { get; set; }

        public string DrugFormFullName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string RequestType { get; set; }

        public DateTime? ExtraditeDate { get; set; }

        [StringLength(500)]
        public string ReceiverFIO { get; set; }
    }
}
