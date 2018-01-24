namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_lims_ApplicationEAdmissions
    {
        public Guid Id { get; set; }

        public Guid TmcCode { get; set; }

        [StringLength(450)]
        public string TmcNumber { get; set; }

        public decimal? QuntityVolume { get; set; }

        [StringLength(450)]
        public string QuntityVolumeStr { get; set; }

        [StringLength(450)]
        public string Unit { get; set; }

        public Guid? UnitCode { get; set; }

        [StringLength(450)]
        public string Producer { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProducerExpirationDate { get; set; }

        [StringLength(450)]
        public string PartyNumber { get; set; }

        [StringLength(450)]
        public string ShelfNumber { get; set; }

        [StringLength(450)]
        public string CupboardNumber { get; set; }

        public Guid? WarehouseNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfReceiving { get; set; }

        public string Note { get; set; }

        public DateTime? ExportDatetime { get; set; }

        public DateTime? ImportDatetime { get; set; }

        [StringLength(450)]
        public string PowerOfAttorneyNumber_1C { get; set; }

        public DateTime? PowerOfAttorneyDatetime_1C { get; set; }

        public decimal? QuntityVolumeConvert { get; set; }

        [StringLength(450)]
        public string UnitConvert { get; set; }

        public Guid? UnitCodeConvert { get; set; }
    }
}
