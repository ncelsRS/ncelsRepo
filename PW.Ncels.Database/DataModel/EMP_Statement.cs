namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EMP_Statement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EMP_Statement()
        {
            EMP_StatementSamples = new HashSet<EMP_StatementSamples>();
        }

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

        public virtual EXP_DIC_NMIRK EXP_DIC_NMIRK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_StatementSamples> EMP_StatementSamples { get; set; }
    }
}
