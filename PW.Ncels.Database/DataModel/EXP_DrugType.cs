namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugType
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public int? DrugTypeId { get; set; }

        [StringLength(500)]
        public string DrugName { get; set; }

        public int? DrugTypeKind { get; set; }

        public virtual EXP_DIC_DrugType EXP_DIC_DrugType { get; set; }

        public virtual EXP_DIC_DrugTypeKind EXP_DIC_DrugTypeKind { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
