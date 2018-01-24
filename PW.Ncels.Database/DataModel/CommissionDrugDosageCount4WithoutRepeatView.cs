namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CommissionDrugDosageCount4WithoutRepeatView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommissionId { get; set; }

        public int? TypeId { get; set; }

        public int? Count { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string TypeName { get; set; }

        [StringLength(300)]
        public string TypeName2 { get; set; }

        public string TypeList { get; set; }
    }
}
