namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertiseStageRemark
    {
        public long Id { get; set; }

        public Guid StageId { get; set; }

        public int? RemarkTypeId { get; set; }

        public int? OtdId { get; set; }

        public string NameRemark { get; set; }

        public Guid? ExecuterId { get; set; }

        public DateTime? RemarkDate { get; set; }

        public string AnswerRemark { get; set; }

        public bool IsFixed { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime? FixedDate { get; set; }

        public string Note { get; set; }

        public bool IsReadOnly { get; set; }

        [StringLength(50)]
        public string CorespondenceId { get; set; }

        public Guid? AtthachId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EXP_DIC_RemarkType EXP_DIC_RemarkType { get; set; }

        public virtual EXP_ExpertiseStage EXP_ExpertiseStage { get; set; }
    }
}
