namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FileLink
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileLink()
        {
            FileLinks1 = new HashSet<FileLink>();
        }

        public Guid Id { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(4000)]
        public string FileName { get; set; }

        public int? Version { get; set; }

        public Guid? DocumentId { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? ParentId { get; set; }

        public Guid? OwnerId { get; set; }

        public bool IsDeleted { get; set; }

        public string Comment { get; set; }

        [StringLength(20)]
        public string Language { get; set; }

        public int? PageNumbers { get; set; }

        public int? StageId { get; set; }

        public int? StatusId { get; set; }

        public bool IsSigned { get; set; }

        public virtual DIC_FileLinkStatus DIC_FileLinkStatus { get; set; }

        public virtual Dictionary FileCategory { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EXP_DIC_Stage EXP_DIC_Stage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileLink> FileLinks1 { get; set; }

        public virtual FileLink FileLink1 { get; set; }
    }
}
