namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TmcReport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TmcReport()
        {
            TmcReportTasks = new HashSet<TmcReportTask>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(4000)]
        public string Name { get; set; }

        public DateTime PeriodStartDate { get; set; }

        public DateTime PeriodEndDate { get; set; }

        public Guid DepartmentId { get; set; }

        [StringLength(4000)]
        public string DepartmentValue { get; set; }

        public Guid CreateEmployeeId { get; set; }

        [StringLength(4000)]
        public string CreateEmployeeValue { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [Required]
        [StringLength(4000)]
        public string ReportType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TmcReportTask> TmcReportTasks { get; set; }
    }
}
