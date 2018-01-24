namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_ContractStageExecutors
    {
        public Guid ContractStageId { get; set; }

        public Guid ExecutorId { get; set; }

        public int ExecutorType { get; set; }

        public Guid Id { get; set; }

        public virtual EMP_ContractStage EMP_ContractStage { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
