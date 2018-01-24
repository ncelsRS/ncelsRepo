namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugPrimaryNTD
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public DateTime? DateReg { get; set; }

        public DateTime? DateConfirm { get; set; }

        public int? TypeNDId { get; set; }

        public int? TypeFileNDId { get; set; }

        public bool isNdConfirm { get; set; }

        [StringLength(50)]
        public string NumberNd { get; set; }

        [StringLength(2000)]
        public string Note { get; set; }

        public virtual EXP_DIC_TypeFileND EXP_DIC_TypeFileND { get; set; }

        public virtual EXP_DIC_TypeND EXP_DIC_TypeND { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
