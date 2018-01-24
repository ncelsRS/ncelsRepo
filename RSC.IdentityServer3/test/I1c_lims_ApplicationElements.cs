namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_lims_ApplicationElements
    {
        public Guid Id { get; set; }

        public Guid TmcCode { get; set; }

        [StringLength(450)]
        public string TmcNumber { get; set; }

        public string Name { get; set; }

        public decimal? QuntityVolume { get; set; }

        [StringLength(450)]
        public string QuntityVolumeStr { get; set; }

        [StringLength(450)]
        public string Unit { get; set; }

        public Guid? UnitCode { get; set; }

        [StringLength(450)]
        public string Kind { get; set; }

        public Guid? KindCode { get; set; }

        [StringLength(450)]
        public string Paking { get; set; }

        public Guid? PakingCode { get; set; }

        public DateTime? ExportDatetime { get; set; }

        public DateTime? ImportDatetime { get; set; }

        public Guid refApplication { get; set; }
    }
}
