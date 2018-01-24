namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EXP_DrugOrganizations
    {
        public long Id { get; set; }

        public Guid DrugDeclarationId { get; set; }

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

        public virtual EXP_DrugDeclaration EXP_DrugDeclaration { get; set; }
    }
}
