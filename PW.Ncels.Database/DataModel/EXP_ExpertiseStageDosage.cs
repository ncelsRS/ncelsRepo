namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_ExpertiseStageDosage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_ExpertiseStageDosage()
        {
            EXP_DrugAnaliseIndicator = new HashSet<EXP_DrugAnaliseIndicator>();
            EXP_ExpertisePharmaceuticalFinalDoc = new HashSet<EXP_ExpertisePharmaceuticalFinalDoc>();
            EXP_ExpertisePharmacologicalFinalDoc = new HashSet<EXP_ExpertisePharmacologicalFinalDoc>();
            PrimaryFinalDocs = new HashSet<EXP_ExpertisePrimaryFinalDoc>();
            EXP_ExpertiseSafetyreportFinalDoc = new HashSet<EXP_ExpertiseSafetyreportFinalDoc>();
            EXP_ExpertiseStageDosageResult = new HashSet<EXP_ExpertiseStageDosageResult>();
        }

        public Guid Id { get; set; }

        public Guid StageId { get; set; }

        public long DosageId { get; set; }

        public int? ResultId { get; set; }

        public Guid? FinalDocStatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual Dictionary FinalDocStatus { get; set; }

        public virtual EXP_DIC_StageResult EXP_DIC_StageResult { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugAnaliseIndicator> EXP_DrugAnaliseIndicator { get; set; }

        public virtual EXP_DrugDosage EXP_DrugDosage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertisePharmaceuticalFinalDoc> EXP_ExpertisePharmaceuticalFinalDoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertisePharmacologicalFinalDoc> EXP_ExpertisePharmacologicalFinalDoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertisePrimaryFinalDoc> PrimaryFinalDocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseSafetyreportFinalDoc> EXP_ExpertiseSafetyreportFinalDoc { get; set; }

        public virtual EXP_ExpertiseStage EXP_ExpertiseStage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageDosageResult> EXP_ExpertiseStageDosageResult { get; set; }
    }
}
