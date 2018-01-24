namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ContractStageExecutors
    {
        [Key]
        [Column(Order = 0)]
        public Guid ContractStageId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ExecutorId { get; set; }

        public int ExecutorType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_ContractStage OBK_ContractStage { get; set; }
    }
}
