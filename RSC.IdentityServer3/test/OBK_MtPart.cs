namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_MtPart
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }

        public int? PartNumber { get; set; }

        [StringLength(500)]
        public string Model { get; set; }

        [StringLength(500)]
        public string Specification { get; set; }

        [StringLength(500)]
        public string SpecificationKz { get; set; }

        [StringLength(2000)]
        public string ProducerName { get; set; }

        [StringLength(500)]
        public string CountryName { get; set; }

        [StringLength(2000)]
        public string ProducerNameKz { get; set; }

        [StringLength(500)]
        public string CountryNameKz { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameKz { get; set; }

        public virtual OBK_RS_Products OBK_RS_Products { get; set; }
    }
}
