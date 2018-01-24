namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DIC_NMIRK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DIC_NMIRK()
        {
            EMP_EAESStatement = new HashSet<EMP_EAESStatement>();
            EMP_Statement = new HashSet<EMP_Statement>();
        }

        public int Id { get; set; }

        public int Code { get; set; }

        [StringLength(50)]
        public string NameRu { get; set; }

        [StringLength(50)]
        public string NameKk { get; set; }

        [StringLength(1000)]
        public string DescriptionRu { get; set; }

        [StringLength(1000)]
        public string Descriptionkk { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_EAESStatement> EMP_EAESStatement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Statement> EMP_Statement { get; set; }
    }
}
