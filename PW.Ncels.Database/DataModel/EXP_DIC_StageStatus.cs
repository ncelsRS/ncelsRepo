namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DIC_StageStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DIC_StageStatus()
        {
            EXP_ExpertiseStage = new HashSet<EXP_ExpertiseStage>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
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

        public DateTime? DateEnd { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStage> EXP_ExpertiseStage { get; set; }
    }
}
