namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugOtherCountry
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        [StringLength(500)]
        public string RegNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IssueDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpireDate { get; set; }

        public long? CountryId { get; set; }

        public bool IsUnLimited { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        public virtual sr_countries sr_countries { get; set; }
    }
}
