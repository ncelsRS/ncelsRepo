namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcUseOffView")]
    public partial class TmcUseOffView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid CreatedEmployeeId { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StateType { get; set; }

        public Guid? TmcOutId { get; set; }

        public Guid? TmcId { get; set; }

        [StringLength(450)]
        public string Note { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal Count { get; set; }

        public Guid? ExpertiseStatementId { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementNumber { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementTypeStr { get; set; }

        [StringLength(450)]
        public string TmcCode { get; set; }

        public string TmcName { get; set; }

        public decimal? TmcCount { get; set; }
    }
}
