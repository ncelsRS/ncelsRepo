namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Request
    {
        public Guid Id { get; set; }

        [StringLength(300)]
        public string Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateStart { get; set; }

        public Guid? CategoryId { get; set; }
    }
}
