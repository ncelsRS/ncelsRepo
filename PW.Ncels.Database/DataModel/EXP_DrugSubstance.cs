namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugSubstance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EXP_DrugSubstance()
        {
            EXP_DrugSubstanceManufacture = new HashSet<EXP_DrugSubstanceManufacture>();
        }

        public long Id { get; set; }

        public int? SubstanceTypeId { get; set; }

        public int? SubstanceId { get; set; }

        [StringLength(500)]
        public string SubstanceName { get; set; }

        [StringLength(500)]
        public string SubstanceCount { get; set; }

        public long? MeasureId { get; set; }

        [StringLength(500)]
        public string ProducerName { get; set; }

        [StringLength(500)]
        public string ProducerAddress { get; set; }

        public long? CountryId { get; set; }

        [StringLength(500)]
        public string NormativeDocument { get; set; }

        public bool IsControl { get; set; }

        public bool IsPoison { get; set; }

        public int? PlantKindId { get; set; }

        [StringLength(500)]
        public string Locus { get; set; }

        public int? OriginId { get; set; }

        public long DrugDosageId { get; set; }

        public bool IsNotFound { get; set; }

        [StringLength(500)]
        public string NewName { get; set; }

        public int? NormDocFarmId { get; set; }

        [StringLength(4000)]
        public string MathFormula { get; set; }

        public virtual EXP_DIC_NormDocFarm EXP_DIC_NormDocFarm { get; set; }

        public virtual EXP_DIC_Origin EXP_DIC_Origin { get; set; }

        public virtual EXP_DIC_PlantKind EXP_DIC_PlantKind { get; set; }

        public virtual EXP_DrugDosage EXP_DrugDosage { get; set; }

        public virtual sr_countries sr_countries { get; set; }

        public virtual sr_measures sr_measures { get; set; }

        public virtual sr_substance_types sr_substance_types { get; set; }

        public virtual sr_substances sr_substances { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DrugSubstanceManufacture> EXP_DrugSubstanceManufacture { get; set; }
    }
}
