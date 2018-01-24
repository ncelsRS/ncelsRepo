namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugSubstanceManufacture
    {
        public long Id { get; set; }

        public long DrugSubstanceId { get; set; }

        [StringLength(500)]
        public string ProducerName { get; set; }

        [StringLength(500)]
        public string ProducerAddress { get; set; }

        public long? CountryId { get; set; }

        public virtual EXP_DrugSubstance EXP_DrugSubstance { get; set; }

        public virtual sr_countries sr_countries { get; set; }
    }
}
