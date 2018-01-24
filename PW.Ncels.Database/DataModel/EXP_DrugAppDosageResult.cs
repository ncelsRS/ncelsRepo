namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugAppDosageResult
    {
        public long Id { get; set; }

        [StringLength(2000)]
        public string NameResult { get; set; }

        public bool IsMark { get; set; }

        public int? RemarkTypeId { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        public DateTime? RemarkDate { get; set; }

        public DateTime? FixedDate { get; set; }

        public long DrugDosageId { get; set; }

        public virtual EXP_DIC_RemarkType EXP_DIC_RemarkType { get; set; }

        public virtual EXP_DrugDosage EXP_DrugDosage { get; set; }
    }
}
