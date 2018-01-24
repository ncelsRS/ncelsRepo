namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertiseStageDosageCommissionView
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string FullNumber { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool IsComplete { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(300)]
        public string CommisionType { get; set; }

        [StringLength(200)]
        public string ConclusionName { get; set; }

        public string ConclusionComment { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid StageId { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long DrugDosageId { get; set; }
    }
}
