namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKCopyStageExecutors
    {
        [Key]
        public Guid ZBKCopyStageId { get; set; }

        public Guid ExecutorId { get; set; }

        public int ExecutorType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_ZBKCopyStage OBK_ZBKCopyStage { get; set; }
    }
}
