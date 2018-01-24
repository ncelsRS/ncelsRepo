namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Registery
    {
        public Guid Id { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        public int Count { get; set; }

        public decimal Cost { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        public int Number { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsTake { get; set; }

        public Guid? Country { get; set; }

        public int Type { get; set; }

        public Guid OrganizationId { get; set; }
    }
}
