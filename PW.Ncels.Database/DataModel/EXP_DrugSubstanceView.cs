namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugSubstanceView
    {
        [Key]
        public Guid DrugDeclarationId { get; set; }

        public string ActiveSubstanceRu { get; set; }

        public string ActiveSubstanceKz { get; set; }

        public string SecondarySubstanceRu { get; set; }

        public string SecondarySubstanceKz { get; set; }
    }
}
