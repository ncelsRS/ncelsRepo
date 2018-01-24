namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_UniqueNumber
    {
        public Guid Id { get; set; }

        public Guid DeclarantId { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        public int Number { get; set; }

        public int ProductSeriesId { get; set; }
    }
}
