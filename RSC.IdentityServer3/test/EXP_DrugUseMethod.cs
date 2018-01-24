namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugUseMethod
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public int UseMethodsId { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        public virtual sr_use_methods sr_use_methods { get; set; }
    }
}
