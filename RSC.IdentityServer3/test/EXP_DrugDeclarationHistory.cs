namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugDeclarationHistory
    {
        public long Id { get; set; }

        public Guid? UserId { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        public int StatusId { get; set; }

        public string Note { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public string XmlSign { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EXP_DIC_Status EXP_DIC_Status { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
