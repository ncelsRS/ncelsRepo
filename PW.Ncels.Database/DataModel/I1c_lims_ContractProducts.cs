namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_lims_ContractProducts
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string ProductId { get; set; }

        public string Name { get; set; }

        [StringLength(500)]
        public string Unit { get; set; }

        public decimal? QuantityVolume { get; set; }

        [StringLength(500)]
        public string ContractNumber { get; set; }

        public DateTime? ExportDatetime { get; set; }

        public DateTime? ImportDatetime { get; set; }
    }
}
