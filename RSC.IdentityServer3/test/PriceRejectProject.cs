namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PriceRejectProject
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string RegNumber { get; set; }

        public Guid? RejectReasonDicId { get; set; }

        public Guid? DocumentId { get; set; }

        public int? RegisterId { get; set; }

        public int? RegisterDfId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
