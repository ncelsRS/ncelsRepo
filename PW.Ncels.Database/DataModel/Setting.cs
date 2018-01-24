namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Setting
    {
        public Guid Id { get; set; }

        [StringLength(100)]
        public string UniqueName { get; set; }

        [StringLength(100)]
        public string Type { get; set; }

        public string Value { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(510)]
        public string Discription { get; set; }

        public int Rank { get; set; }

        public Guid? UserId { get; set; }
    }
}
