namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Change
    {
        public Guid Id { get; set; }

        public Guid? RegisterProjectId { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Type { get; set; }

        [StringLength(500)]
        public string Conditions { get; set; }
    }
}
