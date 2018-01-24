namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileLinksCategoryCom")]
    public partial class FileLinksCategoryCom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FileLinksCategoryCom()
        {
            FileLinksCategoryComRecords = new HashSet<FileLinksCategoryComRecord>();
        }

        public Guid Id { get; set; }

        public Guid? DocumentId { get; set; }

        public Guid? CategoryId { get; set; }

        public bool IsError { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        public bool? IsNotApplicable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileLinksCategoryComRecord> FileLinksCategoryComRecords { get; set; }
    }
}
