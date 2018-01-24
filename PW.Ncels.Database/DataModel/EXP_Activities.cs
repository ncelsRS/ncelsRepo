namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_Activities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_Activities()
        {
            EXP_Tasks = new HashSet<EXP_Tasks>();
        }

        public Guid Id { get; set; }

        public Guid TypeId { get; set; }

        [StringLength(4000)]
        public string TypeValue { get; set; }

        [Required]
        [StringLength(4000)]
        public string Text { get; set; }

        public Guid StatusId { get; set; }

        [StringLength(4000)]
        public string StatusValue { get; set; }

        public Guid AuthorId { get; set; }

        [StringLength(4000)]
        public string AuthorValue { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? ExecutedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public Guid DocumentId { get; set; }

        [StringLength(4000)]
        public string DocumentValue { get; set; }

        public Guid DocumentTypeId { get; set; }

        [StringLength(4000)]
        public string DocumentTypeValue { get; set; }

        [StringLength(255)]
        public string DocNumber { get; set; }

        public DateTime? DocDate { get; set; }

        public virtual Dictionary ExpActivityStatus { get; set; }

        public virtual Dictionary ExpActivityType { get; set; }

        public virtual Dictionary ExpAgreedDocType { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Tasks> EXP_Tasks { get; set; }
    }
}
