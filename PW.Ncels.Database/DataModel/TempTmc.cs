namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TempTmc")]
    public partial class TempTmc
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Count { get; set; }

        public string CountFact { get; set; }

        public string Unit { get; set; }
    }
}
