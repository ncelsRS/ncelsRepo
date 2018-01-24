namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugProtectionDoc
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        [StringLength(500)]
        public string NameDocument { get; set; }

        [StringLength(500)]
        public string NumberDocument { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        [StringLength(500)]
        public string NameOwner { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
