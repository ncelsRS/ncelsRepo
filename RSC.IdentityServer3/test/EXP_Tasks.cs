namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_Tasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_Tasks()
        {
            EXP_TasksChild = new HashSet<EXP_Tasks>();
        }

        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        [StringLength(4000)]
        public string TypeValue { get; set; }

        public Guid StatusId { get; set; }

        [StringLength(4000)]
        public string StatusValue { get; set; }

        public int OrderNumber { get; set; }

        [StringLength(256)]
        public string Number { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? ExecutedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public Guid AuthorId { get; set; }

        [StringLength(4000)]
        public string AuthorValue { get; set; }

        public Guid ExecutorId { get; set; }

        [StringLength(4000)]
        public string ExecutorValue { get; set; }

        public Guid ActivityId { get; set; }

        public Guid? ParentTaskId { get; set; }

        [StringLength(4000)]
        public string Comment { get; set; }

        public string DigSign { get; set; }

        public virtual Dictionary ExpTaskType { get; set; }

        public virtual Dictionary StatusesDictionary { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee ExecutorEmployee { get; set; }

        public virtual EXP_Activities EXP_Activities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Tasks> EXP_TasksChild { get; set; }

        public virtual EXP_Tasks EXP_TasksParent { get; set; }
    }
}
