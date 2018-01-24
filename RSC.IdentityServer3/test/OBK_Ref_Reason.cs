namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_Reason
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Ref_Reason()
        {
            OBK_StageExpDocument = new HashSet<OBK_StageExpDocument>();
        }

        public int Id { get; set; }

        public string Code { get; set; }

        [Required]
        public string NameRu { get; set; }

        [Required]
        public string NameKz { get; set; }

        public bool ExpertiseResult { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_StageExpDocument> OBK_StageExpDocument { get; set; }
    }
}
