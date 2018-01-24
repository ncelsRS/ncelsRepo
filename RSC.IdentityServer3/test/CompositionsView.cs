namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CompositionsView")]
    public partial class CompositionsView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        public Guid? MaterialTypeDicId { get; set; }

        public Guid? MaterialNameDicId { get; set; }

        [StringLength(500)]
        public string MaterialName { get; set; }

        [Key]
        [Column(Order = 1)]
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

        [Key]
        [Column(Order = 2)]
        public bool IsControl { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool IsPoison { get; set; }

        public Guid? MaterialGainDicId { get; set; }

        [StringLength(500)]
        public string MaterialGain { get; set; }

        public Guid? MaterialOriginDicId { get; set; }

        [StringLength(4000)]
        public string MaterialTypeName { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string CountryName { get; set; }

        [StringLength(4000)]
        public string MaterialGainName { get; set; }

        [StringLength(4000)]
        public string MaterialOriginName { get; set; }

        public Guid? ObjectId { get; set; }
    }
}
