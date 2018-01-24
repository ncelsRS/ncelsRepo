namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugPrimaryKind
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public int PrimaryKindId { get; set; }

        public virtual EXP_DIC_PrimaryMark EXP_DIC_PrimaryMark { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
