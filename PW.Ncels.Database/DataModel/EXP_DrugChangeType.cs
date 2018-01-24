namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugChangeType
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public int ChangeTypeId { get; set; }

        [StringLength(2000)]
        public string Condition { get; set; }

        [StringLength(2000)]
        public string ConditionOld { get; set; }

        public virtual EXP_DIC_ChangeType EXP_DIC_ChangeType { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
