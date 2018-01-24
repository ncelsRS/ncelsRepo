namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OBK_Ref_Stage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OBK_Ref_Stage()
        {
            OBK_AssessmentStage = new HashSet<OBK_AssessmentStage>();
            OBK_ContractStage = new HashSet<OBK_ContractStage>();
            OBK_ZBKCopyStage = new HashSet<OBK_ZBKCopyStage>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(2000)]
        public string NameRu { get; set; }

        [StringLength(2000)]
        public string NameKz { get; set; }

        public DateTime DateCreate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DateEdit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_AssessmentStage> OBK_AssessmentStage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ContractStage> OBK_ContractStage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBK_ZBKCopyStage> OBK_ZBKCopyStage { get; set; }
    }
}
