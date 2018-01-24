namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_TaskRegistryView
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid ActivityId { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Key]
        [Column(Order = 2)]
        public Guid DocumentId { get; set; }

        [StringLength(255)]
        public string DocNumber { get; set; }

        public DateTime? DocDate { get; set; }

        [Key]
        [Column(Order = 3)]
        public Guid StatusId { get; set; }

        [StringLength(4000)]
        public string StatusRu { get; set; }

        [StringLength(4000)]
        public string StatusKz { get; set; }

        [StringLength(4000)]
        public string StatusCode { get; set; }

        [Key]
        [Column(Order = 4)]
        public Guid TypeId { get; set; }

        [StringLength(4000)]
        public string TaskTypeCode { get; set; }

        [Key]
        [Column(Order = 5)]
        public Guid DocumentTypeId { get; set; }

        [StringLength(4000)]
        public string DocumentTypeRu { get; set; }

        [StringLength(4000)]
        public string DocumentTypeKz { get; set; }

        [StringLength(4000)]
        public string DocumentTypeCode { get; set; }

        [Key]
        [Column(Order = 6)]
        public Guid AuthorId { get; set; }

        [StringLength(4000)]
        public string AuthorName { get; set; }

        [Key]
        [Column(Order = 7)]
        public Guid ExecutorId { get; set; }

        [StringLength(4000)]
        public string ActivityTypeCode { get; set; }

        public string ApproverNames { get; set; }

        public string SignerNames { get; set; }
    }
}
