namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ActivityTaskActualView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid TypeId { get; set; }

        [StringLength(4000)]
        public string TypeValue { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(4000)]
        public string Text { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid StatusId { get; set; }

        [StringLength(4000)]
        public string StatusValue { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid AuthorId { get; set; }

        [StringLength(4000)]
        public string AuthorValue { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? ExecutedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid DocumentId { get; set; }

        [StringLength(4000)]
        public string DocumentValue { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid DocumentTypeId { get; set; }

        [StringLength(4000)]
        public string DocumentTypeValue { get; set; }

        public Guid? TaskId { get; set; }

        public Guid? TaskTypeId { get; set; }

        [StringLength(4000)]
        public string TaskTypeValue { get; set; }

        public Guid? TaskStatusId { get; set; }

        [StringLength(4000)]
        public string TaskStatusValue { get; set; }

        public int? TaskOrderNumber { get; set; }

        [StringLength(256)]
        public string TaskNumber { get; set; }

        [StringLength(4000)]
        public string TaskText { get; set; }

        public DateTime? TaskCreatedDate { get; set; }

        public DateTime? TaskModifiedDate { get; set; }

        public DateTime? TaskExecutedDate { get; set; }

        public DateTime? TaskClosedDate { get; set; }

        public Guid? TaskAuthorId { get; set; }

        [StringLength(4000)]
        public string TaskAuthorValue { get; set; }

        public Guid? TaskExecutorId { get; set; }

        [StringLength(4000)]
        public string TaskExecutorValue { get; set; }

        public Guid? TaskActivityId { get; set; }

        public Guid? TaskParentTaskId { get; set; }

        [StringLength(4000)]
        public string TaskComment { get; set; }
    }
}
