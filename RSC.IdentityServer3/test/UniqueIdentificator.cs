namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UniqueIdentificator
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Type { get; set; }

        [StringLength(500)]
        public string Value { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
