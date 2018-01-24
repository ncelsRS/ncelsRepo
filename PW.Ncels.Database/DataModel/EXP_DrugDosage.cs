namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugDosage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DrugDosage()
        {
            CommissionDrugDosages = new HashSet<CommissionDrugDosage>();
            CommissionDrugDosageNeedCommissions = new HashSet<CommissionDrugDosageNeedCommission>();
            EXP_DrugAppDosageRemark = new HashSet<EXP_DrugAppDosageRemark>();
            EXP_DrugAppDosageResult = new HashSet<EXP_DrugAppDosageResult>();
            EXP_DrugPrice = new HashSet<EXP_DrugPrice>();
            EXP_DrugPrimaryFinalDocument = new HashSet<EXP_DrugPrimaryFinalDocument>();
            EXP_DrugSubstance = new HashSet<EXP_DrugSubstance>();
            EXP_DrugWrapping = new HashSet<EXP_DrugWrapping>();
            EXP_ExpertiseStageDosage = new HashSet<EXP_ExpertiseStageDosage>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string RegNumber { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public decimal Dosage { get; set; }

        public long? DosageMeasureTypeId { get; set; }

        [StringLength(500)]
        public string DosageNoteKz { get; set; }

        [StringLength(500)]
        public string DosageNoteRu { get; set; }

        [StringLength(500)]
        public string ConcentrationRu { get; set; }

        [StringLength(500)]
        public string ConcentrationKz { get; set; }

        public int? SaleTypeId { get; set; }

        [StringLength(500)]
        public string BestBefore { get; set; }

        public long? BestBeforeMeasureTypeDicId { get; set; }

        [StringLength(500)]
        public string AppPeriodOpen { get; set; }

        public long? AppPeriodOpenMeasureDicId { get; set; }

        [StringLength(500)]
        public string AppPeriodMix { get; set; }

        public long? AppPeriodMixMeasureDicId { get; set; }

        public int? RegisterId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionDrugDosage> CommissionDrugDosages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommissionDrugDosageNeedCommission> CommissionDrugDosageNeedCommissions { get; set; }

        public virtual EXP_DIC_SaleType EXP_DIC_SaleType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugAppDosageRemark> EXP_DrugAppDosageRemark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugAppDosageResult> EXP_DrugAppDosageResult { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        public virtual sr_measures sr_measures { get; set; }

        public virtual sr_measures sr_measures1 { get; set; }

        public virtual sr_measures sr_measures2 { get; set; }

        public virtual sr_register sr_register { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPrice> EXP_DrugPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugPrimaryFinalDocument> EXP_DrugPrimaryFinalDocument { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugSubstance> EXP_DrugSubstance { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugWrapping> EXP_DrugWrapping { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_ExpertiseStageDosage> EXP_ExpertiseStageDosage { get; set; }

        public virtual sr_measures sr_measures3 { get; set; }
    }
}
