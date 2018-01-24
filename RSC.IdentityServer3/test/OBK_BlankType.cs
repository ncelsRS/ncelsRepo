namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_BlankType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_BlankType()
        {
            OBK_BlankNumber = new HashSet<OBK_BlankNumber>();
        }

        public short Id { get; set; }

        [StringLength(50)]
        public string NameRu { get; set; }

        [StringLength(50)]
        public string NameKz { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public DateTime? CreateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_BlankNumber> OBK_BlankNumber { get; set; }
    }
}
