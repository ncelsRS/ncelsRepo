namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_CertificateType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Ref_CertificateType()
        {
            OBK_AssessmentDeclaration = new HashSet<OBK_AssessmentDeclaration>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(2000)]
        public string NameRu { get; set; }

        [Required]
        [StringLength(2000)]
        public string NameKz { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentDeclaration> OBK_AssessmentDeclaration { get; set; }
    }
}
