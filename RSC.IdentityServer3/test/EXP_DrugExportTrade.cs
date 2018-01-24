namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugExportTrade
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        [StringLength(500)]
        public string NameRu { get; set; }

        [StringLength(500)]
        public string NameKz { get; set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        public long? CountryId { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        public virtual sr_countries sr_countries { get; set; }
    }
}
