namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugPrimaryRemark
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public int? RemarkTypeId { get; set; }

        public int? OtdId { get; set; }

        [StringLength(2000)]
        public string NameRemark { get; set; }

        public Guid? ExecuterId { get; set; }

        public DateTime? RemarkDate { get; set; }

        [StringLength(2000)]
        public string AnswerRemark { get; set; }

        public bool IsFixed { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime? FixedDate { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        public bool IsReadOnly { get; set; }

        [StringLength(50)]
        public string CorespondenceId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EXP_DIC_RemarkType EXP_DIC_RemarkType { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
