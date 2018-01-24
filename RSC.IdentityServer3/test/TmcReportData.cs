namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TmcReportData")]
    public partial class TmcReportData
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatedEmployeeId { get; set; }

        [Required]
        [StringLength(4000)]
        public string CreatedEmployeeName { get; set; }

        public Guid? TmcId { get; set; }

        [StringLength(4000)]
        public string TmcName { get; set; }

        [StringLength(512)]
        public string TmcCode { get; set; }

        public decimal? GeneralCount { get; set; }

        public decimal? GeneralCountFact { get; set; }

        public decimal? GeneralConvertFact { get; set; }

        public decimal? IssuedCount { get; set; }

        public decimal? UseCount { get; set; }

        public decimal? UseFactCount { get; set; }

        public DateTime? ReceivedDate { get; set; }

        [StringLength(4000)]
        public string MeasureName { get; set; }

        [StringLength(4000)]
        public string MeasureConvertName { get; set; }

        [Column(TypeName = "text")]
        public string UseReasonText { get; set; }

        public Guid? PositionId { get; set; }

        [StringLength(4000)]
        public string PositionName { get; set; }

        public Guid? SubDepartmentId { get; set; }

        [StringLength(4000)]
        public string SubDepartmentName { get; set; }

        public Guid? DepartmentId { get; set; }

        [StringLength(4000)]
        public string DepartmentName { get; set; }

        public Guid? CenterId { get; set; }

        [StringLength(4000)]
        public string CenterName { get; set; }

        public Guid? ExpertiseStatementId { get; set; }

        [Column(TypeName = "text")]
        public string ExpertiseStatementNumber { get; set; }

        [Column(TypeName = "text")]
        public string ExpertiseStatementTypeStr { get; set; }

        public Guid refTmcReport { get; set; }
    }
}
