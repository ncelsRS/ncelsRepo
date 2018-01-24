namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcOffView")]
    public partial class TmcOffView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CreatedEmployeeId { get; set; }

        [StringLength(4000)]
        public string CreatedEmployeeValue { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        public Guid? TmcOutId { get; set; }

        [StringLength(450)]
        public string Note { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal Count { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(11)]
        public string StateTypeValue { get; set; }

        public Guid? ExpertiseStatementId { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementNumber { get; set; }
    }
}
