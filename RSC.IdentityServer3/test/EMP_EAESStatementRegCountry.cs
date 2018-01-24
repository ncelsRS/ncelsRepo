namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_EAESStatementRegCountry
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string RegNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfIssue { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpDate { get; set; }

        public bool? IsIndefinitely { get; set; }

        public Guid StatementId { get; set; }
    }
}
