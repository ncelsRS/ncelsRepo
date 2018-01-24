namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_LetterRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_LetterRegistration()
        {
            OBK_LetterPortalEdo = new HashSet<OBK_LetterPortalEdo>();
        }

        public long ID { get; set; }

        [StringLength(100)]
        public string LetterRegName { get; set; }

        public DateTime? LetterRegDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_LetterPortalEdo> OBK_LetterPortalEdo { get; set; }
    }
}
