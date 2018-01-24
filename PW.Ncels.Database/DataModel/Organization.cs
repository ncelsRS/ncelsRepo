namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Organization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organization()
        {
            Contracts = new HashSet<Contract>();
            Contracts1 = new HashSet<Contract>();
            Contracts2 = new HashSet<Contract>();
            Contracts3 = new HashSet<Contract>();
            Contracts4 = new HashSet<Contract>();
            Contracts5 = new HashSet<Contract>();
            EXP_DirectionToPays = new HashSet<EXP_DirectionToPays>();
            EXP_Materials = new HashSet<EXP_Materials>();
            Organizations1 = new HashSet<Organization>();
        }

        public Guid Id { get; set; }

        public int Type { get; set; }

        [StringLength(500)]
        public string NameKz { get; set; }

        [StringLength(500)]
        public string NameRu { get; set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        public Guid? CountryDicId { get; set; }

        [StringLength(500)]
        public string AddressLegal { get; set; }

        [StringLength(500)]
        public string AddressFact { get; set; }

        [StringLength(500)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Fax { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(500)]
        public string BossFio { get; set; }

        [StringLength(500)]
        public string BossPosition { get; set; }

        [StringLength(500)]
        public string ContactFio { get; set; }

        [StringLength(500)]
        public string ContactPosition { get; set; }

        [StringLength(500)]
        public string ContactPhone { get; set; }

        [StringLength(500)]
        public string ContactFax { get; set; }

        [StringLength(500)]
        public string ContactEmail { get; set; }

        public Guid? OrgManufactureTypeDicId { get; set; }

        [StringLength(500)]
        public string DocNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DocDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DocExpiryDate { get; set; }

        public Guid? ObjectId { get; set; }

        public Guid? OpfTypeDicId { get; set; }

        [StringLength(500)]
        public string BankName { get; set; }

        [StringLength(500)]
        public string BankIik { get; set; }

        public Guid? BankCurencyDicId { get; set; }

        [StringLength(500)]
        public string BankSwift { get; set; }

        [StringLength(500)]
        public string Bin { get; set; }

        public bool IsResident { get; set; }

        public Guid? PayerTypeDicId { get; set; }

        [StringLength(100)]
        public string BossLastName { get; set; }

        [StringLength(100)]
        public string BossFirstName { get; set; }

        [StringLength(100)]
        public string BossMiddleName { get; set; }

        [StringLength(500)]
        public string PaymentBill { get; set; }

        [StringLength(500)]
        public string Iin { get; set; }

        [StringLength(500)]
        public string BankBik { get; set; }

        public Guid? OriginalOrgId { get; set; }

        [StringLength(500)]
        public string Filial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts4 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contract> Contracts5 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_DirectionToPays> EXP_DirectionToPays { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EXP_Materials> EXP_Materials { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Organization> Organizations1 { get; set; }

        public virtual Organization Organization1 { get; set; }
    }
}
