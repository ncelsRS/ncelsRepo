namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Task
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Task()
        {
            AccessTasks = new HashSet<AccessTask>();
            Reports = new HashSet<Report>();
        }

        public Guid Id { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        public DateTime? ExecutionDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(4000)]
        public string AuthorId { get; set; }

        [StringLength(4000)]
        public string AuthorValue { get; set; }

        [StringLength(4000)]
        public string ExecutorId { get; set; }

        [StringLength(4000)]
        public string ExecutorValue { get; set; }

        public int State { get; set; }

        [StringLength(4000)]
        public string DocumentValue { get; set; }

        public int? Type { get; set; }

        public Guid? DocumentId { get; set; }

        public Guid? ActivityId { get; set; }

        [Column(TypeName = "date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? AutoExecutionDate { get; set; }

        [StringLength(4000)]
        public string Number { get; set; }

        public int FunctionType { get; set; }

        public bool IsActive { get; set; }

        public int NotificationCount { get; set; }

        public bool IsNotification { get; set; }

        public int SortingNumber { get; set; }

        public DateTime? DateOfOperation { get; set; }

        [StringLength(4000)]
        public string ModifiedUser { get; set; }

        public bool IsMainLine { get; set; }

        [StringLength(100)]
        public string Ip { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int TypeEx { get; set; }

        public int Stage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccessTask> AccessTasks { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Document Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
    }
}
