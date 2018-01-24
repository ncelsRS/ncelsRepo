namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_EAESStatement
    {
        public Guid Id { get; set; }

        [StringLength(50)]
        public string RegistrationKindValue { get; set; }

        [StringLength(50)]
        public string RegistrationCertificateNumber { get; set; }

        [StringLength(50)]
        public string NormativeDocumentNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [StringLength(50)]
        public string LetterNumber { get; set; }

        public DateTime? LetterDate { get; set; }

        public bool? IsMt { get; set; }

        [StringLength(255)]
        public string MedicalDeviceNameKz { get; set; }

        [StringLength(255)]
        public string MedicalDeviceNameRu { get; set; }

        [StringLength(50)]
        public string NomenclatureCode { get; set; }

        [StringLength(255)]
        public string NomenclatureNameKz { get; set; }

        [StringLength(255)]
        public string NomenclatureNameRu { get; set; }

        [StringLength(2000)]
        public string NomenclatureDescriptionKz { get; set; }

        [StringLength(2000)]
        public string NomenclatureDescriptionRu { get; set; }

        [StringLength(2000)]
        public string ApplicationAreaKz { get; set; }

        [StringLength(2000)]
        public string ApplicationAreaRu { get; set; }

        [StringLength(255)]
        public string PurposeKz { get; set; }

        [StringLength(255)]
        public string PurposeRu { get; set; }

        public bool? IsClosedSystem { get; set; }

        [StringLength(50)]
        public string RegistrationDossierPageNumber { get; set; }

        [StringLength(255)]
        public string ShortTechnicalCharacteristicKz { get; set; }

        [StringLength(255)]
        public string ShortTechnicalCharacteristicRu { get; set; }

        [StringLength(50)]
        public string ClassOfPotentialRisk { get; set; }

        public bool? IsBalk { get; set; }

        public bool? IsMeasurementDevice { get; set; }

        public bool? IsForInvitroDiagnostics { get; set; }

        public bool? IsSterile { get; set; }

        public bool? IsMedicalProductPresence { get; set; }

        public bool? WithouAe { get; set; }

        [StringLength(255)]
        public string TransportConditions { get; set; }

        [StringLength(255)]
        public string StorageConditions { get; set; }

        [StringLength(255)]
        public string Production { get; set; }

        public bool? IsComplectation { get; set; }

        [StringLength(255)]
        public string ManufacturerType { get; set; }

        [StringLength(255)]
        public string ManufacturerNameRu { get; set; }

        [StringLength(255)]
        public string AllowedDocumentNumber { get; set; }

        [StringLength(50)]
        public string BossLastName { get; set; }

        [StringLength(255)]
        public string BossPosition { get; set; }

        [StringLength(255)]
        public string OrganizationForm { get; set; }

        [StringLength(255)]
        public string ManufacturerNameKz { get; set; }

        public DateTime? DateOfIssue { get; set; }

        [StringLength(50)]
        public string BossFirstName { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Country { get; set; }

        [StringLength(255)]
        public string ManufacturerNameEn { get; set; }

        public DateTime? ManufacturerExpirationDate { get; set; }

        [StringLength(50)]
        public string BossMiddleName { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string ContactPersonInitials { get; set; }

        [StringLength(255)]
        public string ContactPersonLegalAddress { get; set; }

        [StringLength(255)]
        public string ContactPersonPosition { get; set; }

        [StringLength(255)]
        public string ContactPersonFactAddress { get; set; }

        [StringLength(3999)]
        public string Agreement { get; set; }

        public bool? IsAgreed { get; set; }

        public Guid? ContractId { get; set; }

        [StringLength(50)]
        public string RegistrationTypeValue { get; set; }

        public int? NmirkId { get; set; }

        [StringLength(50)]
        public string RefCountry { get; set; }

        [StringLength(50)]
        public string ConCountry { get; set; }

        public int? GarantExpDate { get; set; }

        [StringLength(50)]
        public string GarantUnit { get; set; }

        public bool? GarantNoExp { get; set; }

        [StringLength(255)]
        public string PlaceType { get; set; }

        [StringLength(255)]
        public string PlaceNameRu { get; set; }

        [StringLength(255)]
        public string PlaceAllowedDocumentNumber { get; set; }

        [StringLength(255)]
        public string PlaceBossLastName { get; set; }

        [StringLength(255)]
        public string PlaceBossPosition { get; set; }

        [StringLength(255)]
        public string PlaceOrganizationForm { get; set; }

        [StringLength(255)]
        public string PlaceNameKz { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PlaceDateOfIssue { get; set; }

        [StringLength(255)]
        public string PlaceBossFirstName { get; set; }

        [StringLength(255)]
        public string PlacePhone { get; set; }

        [StringLength(255)]
        public string PlaceCountry { get; set; }

        [StringLength(255)]
        public string PlaceNameEn { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PlaceExpirationDate { get; set; }

        [StringLength(255)]
        public string PlaceBossMiddleName { get; set; }

        [StringLength(255)]
        public string PlaceEmail { get; set; }

        [StringLength(255)]
        public string PlaceContactPersonInitials { get; set; }

        [StringLength(255)]
        public string PlaceContactPersonPosition { get; set; }

        [StringLength(255)]
        public string PlaceContactPersonFactAddress { get; set; }

        [StringLength(255)]
        public string ShowerType { get; set; }

        [StringLength(255)]
        public string ShowerNameRu { get; set; }

        [StringLength(255)]
        public string ShowerPAllowedDocumentNumber { get; set; }

        [StringLength(255)]
        public string ShowerBossLastName { get; set; }

        [StringLength(255)]
        public string ShowerBossPosition { get; set; }

        [StringLength(255)]
        public string ShowerOrganizationForm { get; set; }

        [StringLength(255)]
        public string ShowerNameKz { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ShowerDateOfIssue { get; set; }

        [StringLength(255)]
        public string ShowerBossFirstName { get; set; }

        [StringLength(255)]
        public string ShowerPhone { get; set; }

        [StringLength(255)]
        public string ShowerCountry { get; set; }

        [StringLength(255)]
        public string ShowerNameEn { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ShowerExpirationDate { get; set; }

        [StringLength(255)]
        public string ShowerBossMiddleName { get; set; }

        [StringLength(255)]
        public string ShowerEmail { get; set; }

        [StringLength(255)]
        public string ShowerContactPersonInitials { get; set; }

        [StringLength(255)]
        public string ShowerContactPersonPosition { get; set; }

        [StringLength(255)]
        public string ShowerContactPersonFactAddress { get; set; }

        [StringLength(255)]
        public string ShowerContactPersonActualAddress { get; set; }

        public virtual EXP_DIC_NMIRK EXP_DIC_NMIRK { get; set; }
    }
}
