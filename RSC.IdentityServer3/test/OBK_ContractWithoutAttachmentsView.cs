namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractWithoutAttachmentsView
    {
        [StringLength(61)]
        public string Number { get; set; }

        [Key]
        public Guid ExecutorId { get; set; }
    }
}
