namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UnitsAddress")]
    public partial class UnitsAddress
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UnitsId { get; set; }

        public Guid RegionId { get; set; }

        [StringLength(4000)]
        public string AddressNameRu { get; set; }

        [StringLength(4000)]
        public string AddressNameKz { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
