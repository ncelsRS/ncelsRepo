namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_primary_ApplicationElements
    {
        public Guid Id { get; set; }

        public decimal? Dosage { get; set; }

        [StringLength(500)]
        public string MeasureName { get; set; }

        [StringLength(50)]
        public string RegNumber { get; set; }

        [StringLength(500)]
        public string ConcentrationRu { get; set; }

        [StringLength(500)]
        public string ConcentrationKz { get; set; }

        public Guid refApplication { get; set; }

        public long? DosageId { get; set; }
    }
}
