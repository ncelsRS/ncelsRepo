namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_LaboratoryMark
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Ref_LaboratoryMark()
        {
            OBK_Ref_LaboratoryRegulation = new HashSet<OBK_Ref_LaboratoryRegulation>();
            OBK_ResearchCenterResult = new HashSet<OBK_ResearchCenterResult>();
        }

        public Guid Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(255)]
        public string NameRu { get; set; }

        [Required]
        [StringLength(255)]
        public string NameKz { get; set; }

        public bool IsDeleted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_Ref_LaboratoryRegulation> OBK_Ref_LaboratoryRegulation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ResearchCenterResult> OBK_ResearchCenterResult { get; set; }
    }
}
