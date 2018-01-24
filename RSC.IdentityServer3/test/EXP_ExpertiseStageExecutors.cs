namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertiseStageExecutors
    {
        [Key]
        [Column(Order = 0)]
        public Guid ExpertiseStageId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ExecutorId { get; set; }

        public bool IsHead { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EXP_ExpertiseStage EXP_ExpertiseStage { get; set; }
    }
}
