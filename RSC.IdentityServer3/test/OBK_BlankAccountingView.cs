namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_BlankAccountingView
    {
        public Guid Id { get; set; }

        public int? Number { get; set; }

        public DateTime? RegisterDate { get; set; }

        [StringLength(50)]
        public string ExpConclusionNumber { get; set; }

        [StringLength(255)]
        public string Declarant { get; set; }

        [StringLength(4000)]
        public string Executor { get; set; }

        [StringLength(50)]
        public string DocumentType { get; set; }

        [StringLength(11)]
        public string Sign { get; set; }

        [StringLength(4000)]
        public string OrganName { get; set; }

        public bool? Decommissioned { get; set; }

        public DateTime? DecommissionedDate { get; set; }

        public bool? Corrupted { get; set; }
    }
}
