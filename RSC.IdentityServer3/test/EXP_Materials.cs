namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_Materials
    {
        public Guid Id { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [Required]
        public string Name { get; set; }

        public Guid TypeId { get; set; }

        public int? DrugFormId { get; set; }

        public decimal? Dosage { get; set; }

        public Guid? DosageUnitId { get; set; }

        public int? DosageQuantity { get; set; }

        [StringLength(512)]
        public string Concentration { get; set; }

        public decimal? Volume { get; set; }

        public Guid? VolumeUnitId { get; set; }

        public bool IsContainNPP { get; set; }

        public Guid? ProducerId { get; set; }

        public Guid? CountryId { get; set; }

        public int Quantity { get; set; }

        public Guid UnitId { get; set; }

        [Required]
        public string Batch { get; set; }

        public DateTime? DateOfManufacture { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? RetestDate { get; set; }

        public bool IsCertificatePassport { get; set; }

        public Guid? StorageId { get; set; }

        public decimal? StorageTemperatureFrom { get; set; }

        public decimal? StorageTemperatureTo { get; set; }

        public decimal? ActiveSubstancePercent { get; set; }

        public decimal? WaterContentPercent { get; set; }

        public Guid DrugDeclarationId { get; set; }

        public bool IsAdditional { get; set; }

        public Guid? StorageConditionId { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? ExternalStateId { get; set; }

        public Guid? ConcordanceStatementId { get; set; }

        public DateTime? OpeningDate { get; set; }

        public DateTime? ExpirationAfterOpeningDate { get; set; }

        public Guid? ConcentrationUnitId { get; set; }

        public bool IsUnLimited { get; set; }

        public virtual DIC_Storages DIC_Storages { get; set; }

        public virtual Dictionary Dictionary { get; set; }

        public virtual Dictionary ConcordanceStatement { get; set; }

        public virtual Dictionary Country { get; set; }

        public virtual Dictionary DosageUnit { get; set; }

        public virtual Dictionary ExternalState { get; set; }

        public virtual Dictionary Status { get; set; }

        public virtual Dictionary StorageCondition { get; set; }

        public virtual Dictionary MaterialType { get; set; }

        public virtual Dictionary Unit { get; set; }

        public virtual Dictionary VolumeUnit { get; set; }

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }

        public virtual Organization Producer { get; set; }

        public virtual sr_dosage_forms sr_dosage_forms { get; set; }
    }
}
