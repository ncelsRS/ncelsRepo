namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_TaskStatus
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public int StatusId { get; set; }

        public Guid? UnitLaboratoryId { get; set; }

        public virtual OBK_Ref_StageStatus OBK_Ref_StageStatus { get; set; }

        public virtual OBK_Tasks OBK_Tasks { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
