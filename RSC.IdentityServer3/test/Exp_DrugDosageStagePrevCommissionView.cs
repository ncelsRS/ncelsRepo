namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Exp_DrugDosageStagePrevCommissionView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DrugDosageId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid StageId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CommissionId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DrevComId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string DrevComFullNumber { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime PrevComDate { get; set; }
    }
}
