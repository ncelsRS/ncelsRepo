namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugCorespondenceRemark
    {
        public long Id { get; set; }

        public Guid DrugCorespondenceId { get; set; }

        public int? RemarkTypeId { get; set; }

        public string NameRemark { get; set; }

        public Guid? ExecuterId { get; set; }

        public DateTime? RemarkDate { get; set; }

        public string AnswerRemark { get; set; }

        public bool IsFixed { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime? FixedDate { get; set; }

        public string Note { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EXP_DIC_RemarkType EXP_DIC_RemarkType { get; set; }

        public virtual EXP_DrugCorespondence EXP_DrugCorespondence { get; set; }
    }
}
