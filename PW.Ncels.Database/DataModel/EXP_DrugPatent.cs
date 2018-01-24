namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugPatent
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        [StringLength(500)]
        public string PatentNumber { get; set; }

        public DateTime? PatentDate { get; set; }

        public DateTime? PatentExpiryDate { get; set; }

        [StringLength(500)]
        public string NameOwner { get; set; }

        [StringLength(500)]
        public string NameDocument { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
