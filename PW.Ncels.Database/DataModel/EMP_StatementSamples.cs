namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_StatementSamples
    {
        public int Id { get; set; }

        public Guid StatementId { get; set; }

        [Required]
        [StringLength(50)]
        public string SampleType { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Count { get; set; }

        [Required]
        [StringLength(50)]
        public string Unit { get; set; }

        [Required]
        [StringLength(50)]
        public string SeriesPart { get; set; }

        [StringLength(50)]
        public string Storage { get; set; }

        [StringLength(50)]
        public string Conditions { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreateDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpirationDate { get; set; }

        public bool? Addition { get; set; }

        public virtual EMP_Statement EMP_Statement { get; set; }
    }
}
