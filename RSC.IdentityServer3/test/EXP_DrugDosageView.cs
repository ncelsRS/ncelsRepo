namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugDosageView
    {
        [Key]
        public Guid DrugDeclarationId { get; set; }

        public string DosageRu { get; set; }

        public string DosageKz { get; set; }
    }
}
