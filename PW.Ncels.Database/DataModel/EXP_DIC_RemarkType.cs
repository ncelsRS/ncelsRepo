namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DIC_RemarkType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DIC_RemarkType()
        {
            EXP_DrugCorespondenceRemark = new HashSet<EXP_DrugCorespondenceRemark>();
            EXP_DrugPrimaryRemark = new HashSet<EXP_DrugPrimaryRemark>();
            EXP_DrugAppDosageRemark = new HashSet<EXP_DrugAppDosageRemark>();
            EXP_DrugAppDosageResult = new HashSet<EXP_DrugAppDosageResult>();
            EXP_ExpertiseStageRemark = new HashSet<EXP_ExpertiseStageRemark>();
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
        public virtual ICollection<EXP_DrugCorespondenceRemark> EXP_DrugCorespondenceRemark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPrimaryRemark> EXP_DrugPrimaryRemark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugAppDosageRemark> EXP_DrugAppDosageRemark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugAppDosageResult> EXP_DrugAppDosageResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageRemark> EXP_ExpertiseStageRemark { get; set; }
    }
}
