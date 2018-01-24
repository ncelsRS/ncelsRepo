namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnitSigner")]
    public partial class UnitSigner
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UnitsId { get; set; }

        public Guid SignerId { get; set; }

        [StringLength(50)]
        public string DocNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool? IsDeleted { get; set; }

        public Guid? DocumentType { get; set; }
    }
}
