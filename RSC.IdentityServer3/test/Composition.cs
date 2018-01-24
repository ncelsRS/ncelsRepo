namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Composition
    {
        public Guid Id { get; set; }

        public Guid? MaterialTypeDicId { get; set; }

        public Guid? MaterialNameDicId { get; set; }

        [StringLength(500)]
        public string MaterialName { get; set; }

        public decimal Count { get; set; }

        [StringLength(500)]
        public string NormativeDocument { get; set; }

        public Guid? CountryDicId { get; set; }

        [StringLength(500)]
        public string City { get; set; }

        [StringLength(500)]
        public string Street { get; set; }

        [StringLength(500)]
        public string House { get; set; }

        public bool IsControl { get; set; }

        public bool IsPoison { get; set; }

        public Guid? MaterialGainDicId { get; set; }

        [StringLength(500)]
        public string MaterialGain { get; set; }

        public Guid? MaterialOriginDicId { get; set; }

        public Guid? ObjectId { get; set; }
    }
}
