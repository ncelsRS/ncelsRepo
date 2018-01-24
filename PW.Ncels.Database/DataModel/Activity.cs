namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            Tasks = new HashSet<Task>();
        }

        public Guid Id { get; set; }

        [StringLength(4000)]
        public string Text { get; set; }

        public int? Type { get; set; }

        [StringLength(4000)]
        public string AuthorId { get; set; }

        [StringLength(4000)]
        public string AuthorValue { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ExecutionDate { get; set; }

        [StringLength(4000)]
        public string ExecutorsId { get; set; }

        [StringLength(4000)]
        public string ExecutorsValue { get; set; }

        public Guid? DocumentId { get; set; }

        [StringLength(4000)]
        public string DocumentValue { get; set; }

        public bool IsParrent { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? ParentTask { get; set; }

        [StringLength(4000)]
        public string ResponsibleId { get; set; }

        [StringLength(4000)]
        public string ResponsibleValue { get; set; }

        public bool IsNotActive { get; set; }

        public bool IsCurrent { get; set; }

        [StringLength(4000)]
        public string ModifiedUser { get; set; }

        public bool IsMainLine { get; set; }

        [StringLength(100)]
        public string Ip { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int TypeEx { get; set; }

        public int Branch { get; set; }

        public virtual Document Document { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
