namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DIC_IcdDeseases
    {
        public long Id { get; set; }

        [StringLength(512)]
        public string CodeICD { get; set; }

        public string DiseaseOfICD { get; set; }

        public string SysnonimAndRareDesease { get; set; }
    }
}
