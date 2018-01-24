namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcReportDataSourceView")]
    public partial class TmcReportDataSourceView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid TmcId { get; set; }

        public string TmcName { get; set; }

        [StringLength(450)]
        public string TmcCode { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal GeneralCountFact { get; set; }

        [Key]
        [Column(Order = 3)]
        public decimal GeneralConvertFact { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReceivingDate { get; set; }

        [Key]
        [Column(Order = 4)]
        public decimal IssuedCount { get; set; }

        public decimal? UsedCount { get; set; }

        public decimal? UseFactCount { get; set; }

        [StringLength(4000)]
        public string MeasureName { get; set; }

        [StringLength(4000)]
        public string MeasureConvertName { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime CreatedDate { get; set; }

        [StringLength(450)]
        public string UseReason { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid CreatedEmployeeId { get; set; }

        [StringLength(4000)]
        public string CreateEmployeeName { get; set; }

        public Guid? ExpertiseStatementId { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementNumber { get; set; }

        [StringLength(512)]
        public string ExpertiseStatementTypeStr { get; set; }

        [Key]
        [Column(Order = 7)]
        public Guid PositionId { get; set; }

        [StringLength(4000)]
        public string PositionName { get; set; }

        [Key]
        [Column(Order = 8)]
        public Guid SubDepartmentId { get; set; }

        [StringLength(4000)]
        public string SubDepartmentName { get; set; }

        [Key]
        [Column(Order = 9)]
        public Guid DepartmentId { get; set; }

        [StringLength(4000)]
        public string DepartmentName { get; set; }

        [Key]
        [Column(Order = 10)]
        public Guid CenterId { get; set; }

        [StringLength(4000)]
        public string CenterName { get; set; }
    }
}
