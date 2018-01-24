namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TmcOut
    {
        public Guid Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedDate { get; set; }

        public Guid CreatedEmployeeId { get; set; }

        public int StateType { get; set; }

        [StringLength(450)]
        public string Note { get; set; }

        public Guid? OutTypeDicId { get; set; }

        public Guid? StorageDicId { get; set; }

        [StringLength(450)]
        public string Safe { get; set; }

        [StringLength(450)]
        public string Rack { get; set; }

        public Guid? OwnerEmployeeId { get; set; }

        public string Comment { get; set; }
    }
}
