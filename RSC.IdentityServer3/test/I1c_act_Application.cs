namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class I1c_act_Application
    {
        public Guid Id { get; set; }

        [StringLength(500)]
        public string Number { get; set; }

        public DateTime? CreateDatetime { get; set; }

        public decimal? TotalPrice { get; set; }

        [StringLength(500)]
        public string ActNumber1C { get; set; }

        public DateTime? ActDate1C { get; set; }

        public DateTime ExportDatetime { get; set; }

        public DateTime? ImportDatetime { get; set; }

        public int? StageId { get; set; }

        [StringLength(500)]
        public string StageCode { get; set; }

        [StringLength(500)]
        public string StageValue { get; set; }

        public Guid? refPrimaryApplication { get; set; }
    }
}
