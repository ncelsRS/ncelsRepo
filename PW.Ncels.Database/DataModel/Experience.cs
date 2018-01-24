namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Experience
    {
        public Guid Id { get; set; }

        [StringLength(900)]
        public string EmployeeId { get; set; }

        [StringLength(900)]
        public string EmployeeValue { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateEnd { get; set; }

        [StringLength(4000)]
        public string Organization { get; set; }

        [StringLength(4000)]
        public string Country { get; set; }

        [StringLength(4000)]
        public string Position { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }
    }
}
