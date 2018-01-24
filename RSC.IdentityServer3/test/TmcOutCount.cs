namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TmcOutCount
    {
        public Guid Id { get; set; }

        public Guid TmcOutId { get; set; }

        public Guid TmcId { get; set; }

        public decimal Count { get; set; }

        public decimal CountFact { get; set; }

        public int StateType { get; set; }

        [StringLength(450)]
        public string Note { get; set; }
    }
}
