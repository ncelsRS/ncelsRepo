namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lims_ActivityView
    {
        [StringLength(4000)]
        public string DisplayName { get; set; }

        public int? AppPoaNewCount { get; set; }

        public int? AppPoaSendedTo1CCount { get; set; }

        public int? AppPoaReceivedFrom1CCount { get; set; }

        public int? TmcReceivedFrom1CCount { get; set; }

        public int? AppRecCount { get; set; }

        public int? AppRecIssuedCount { get; set; }

        public int? AppRecUsedCount { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ReportByEmployee { get; set; }

        public int? ReportByDepartment { get; set; }

        public int? ReportByIc { get; set; }
    }
}
