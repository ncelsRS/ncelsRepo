namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugTypeView
    {
        [Key]
        public Guid DrugDeclarationId { get; set; }

        public string DrugTypeRu { get; set; }

        public string DrugTypeKz { get; set; }
    }
}
