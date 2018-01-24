namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_TaskExecutor
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public Guid ExecutorId { get; set; }

        public int StageId { get; set; }

        public int? ExecutorType { get; set; }

        [StringLength(1024)]
        public string SignedData { get; set; }

        public Guid? TaskMaterialId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual OBK_Tasks OBK_Tasks { get; set; }

        public virtual OBK_TaskMaterial OBK_TaskMaterial { get; set; }
    }
}
