namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Dictionaries
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Dictionaries()
        {
            OBK_ActReception = new HashSet<OBK_ActReception>();
            OBK_ActReception1 = new HashSet<OBK_ActReception>();
            OBK_ActReception2 = new HashSet<OBK_ActReception>();
            OBK_ActReception3 = new HashSet<OBK_ActReception>();
        }

        public Guid Id { get; set; }

        [StringLength(4000)]
        public string Type { get; set; }

        [StringLength(4000)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string NameKz { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ExpireDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ActReception> OBK_ActReception { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ActReception> OBK_ActReception1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ActReception> OBK_ActReception2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ActReception> OBK_ActReception3 { get; set; }
    }
}
