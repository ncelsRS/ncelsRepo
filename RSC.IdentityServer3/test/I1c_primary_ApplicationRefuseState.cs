namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_ApplicationRefuseState
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string ApplicationNumber { get; set; }

        public DateTime? ApplicationDatetime { get; set; }

        public bool? IsRefuse { get; set; }

        public DateTime? RefuseDatetime { get; set; }

        public Guid refApplication { get; set; }
    }
}
