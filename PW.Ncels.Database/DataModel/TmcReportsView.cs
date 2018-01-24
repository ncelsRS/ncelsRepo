namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcReportsView")]
    public partial class TmcReportsView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4000)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime PeriodStartDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime PeriodEndDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid DepartmentId { get; set; }

        [StringLength(4000)]
        public string DepartmentValue { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid CreateEmployeeId { get; set; }

        [StringLength(4000)]
        public string CreateEmployeeValue { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(4000)]
        public string ReportType { get; set; }

        [Key]
        [Column(Order = 8)]
        public Guid TaskId { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(512)]
        public string Operation { get; set; }

        [Key]
        [Column(Order = 10)]
        public Guid ExecutorEmployeeId { get; set; }

        [Key]
        [Column(Order = 11)]
        [StringLength(4000)]
        public string ExecutorEmployeeValue { get; set; }

        [Key]
        [Column(Order = 12)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Stage { get; set; }

        [Key]
        [Column(Order = 13)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int State { get; set; }

        [Key]
        [Column(Order = 14)]
        public Guid TaskAuthorId { get; set; }

        [Key]
        [Column(Order = 15)]
        [StringLength(4000)]
        public string TaskAuthorValue { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }
    }
}
