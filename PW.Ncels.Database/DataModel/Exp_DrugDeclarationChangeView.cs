namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exp_DrugDeclarationChangeView
    {
        [Key]
        public Guid DrugDeclarationId { get; set; }

        public string ChangeString { get; set; }
    }
}
