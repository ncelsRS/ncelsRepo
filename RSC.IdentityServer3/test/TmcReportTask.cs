namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TmcReportTask
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Operation { get; set; }

        public int Stage { get; set; }

        public int State { get; set; }

        public Guid ExecutorEmployeeId { get; set; }

        [Required]
        [StringLength(4000)]
        public string ExecutorEmployeeValue { get; set; }

        public Guid CreateEmployeeId { get; set; }

        [Required]
        [StringLength(4000)]
        public string CreateEmployeeValue { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }

        public Guid refTmcReport { get; set; }

        public virtual TmcReport TmcReport { get; set; }
    }
}
