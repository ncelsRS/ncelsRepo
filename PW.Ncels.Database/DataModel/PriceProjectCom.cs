namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceProjectCom")]
    public partial class PriceProjectCom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PriceProjectCom()
        {
            PriceProjectComRecords = new HashSet<PriceProjectComRecord>();
        }

        public long Id { get; set; }

        public Guid PriceProjectId { get; set; }

        public bool IsError { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDelete { get; set; }

        [Required]
        [StringLength(500)]
        public string ControlId { get; set; }

        public virtual PriceProject PriceProject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceProjectComRecord> PriceProjectComRecords { get; set; }
    }
}
