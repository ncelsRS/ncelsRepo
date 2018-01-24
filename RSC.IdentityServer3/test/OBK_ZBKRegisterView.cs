namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_ZBKRegisterView
    {
        [StringLength(50)]
        public string BlankNumber { get; set; }

        public DateTime? ExtraditeDate { get; set; }

        [StringLength(50)]
        public string ExpConclusionNumber { get; set; }

        [StringLength(50)]
        public string ZBKType { get; set; }

        [StringLength(255)]
        public string Declarant { get; set; }

        [StringLength(4000)]
        public string ProductName { get; set; }

        public Guid Id { get; set; }
    }
}
