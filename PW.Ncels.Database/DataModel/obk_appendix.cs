namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class obk_appendix
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public obk_appendix()
        {
            obk_appendix_series = new HashSet<obk_appendix_series>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        public long certification_id { get; set; }

        [Required]
        [StringLength(50)]
        public string appendix_number { get; set; }

        [Required]
        public string comment { get; set; }

        public virtual obk_certifications obk_certifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<obk_appendix_series> obk_appendix_series { get; set; }
    }
}
