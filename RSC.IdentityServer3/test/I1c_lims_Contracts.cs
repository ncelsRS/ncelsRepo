namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_lims_Contracts
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string BIN { get; set; }

        public string Name { get; set; }

        [StringLength(500)]
        public string ContractNumber { get; set; }

        public DateTime? ContractDate { get; set; }

        public DateTime? ContractDeliveryLastDate { get; set; }

        public DateTime? ExportDatetime { get; set; }

        public DateTime? ImportDatetime { get; set; }
    }
}
