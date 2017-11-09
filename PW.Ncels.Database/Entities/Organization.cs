using System.ComponentModel.DataAnnotations;

namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(OrganizationMetadata))]
    public partial class Organization : ICloneable
    {
        [Display(Name = "Название")]
        public string Name
        {
            get { return NameRu; }
        }

        [Display(Name = "Валюта")]
        public string BankCurencyName { get; set; }

        [Display(Name = "Страна")]
        public string CountryName { get; set; }

        public class OrganizationMetadata
        {
            public System.Guid Id { get; set; }
            public int Type { get; set; }
            public string NameKz { get; set; }
            public string NameRu { get; set; }
            public string NameEn { get; set; }

            [Display(Name = "Страна")]
            public Nullable<System.Guid> CountryDicId { get; set; }

            [Display(Name = "Юридический адрес")]
            public string AddressLegal { get; set; }

            [Display(Name = "Фактический адрес")]
            public string AddressFact { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }
            public string BossFio { get; set; }
            public string BossPosition { get; set; }
            public string ContactFio { get; set; }
            public string ContactPosition { get; set; }
            public string ContactPhone { get; set; }
            public string ContactFax { get; set; }
            public string ContactEmail { get; set; }
            public Nullable<System.Guid> OrgManufactureTypeDicId { get; set; }
            public string DocNumber { get; set; }
            public Nullable<System.DateTime> DocDate { get; set; }
            public Nullable<System.DateTime> DocExpiryDate { get; set; }
            public Nullable<System.Guid> ObjectId { get; set; }
            public Nullable<System.Guid> OpfTypeDicId { get; set; }

            [Display(Name = "Банк")]
            public string BankName { get; set; }
            public string BankIik { get; set; }

            [Display(Name = "Валюта")]
            public Nullable<System.Guid> BankCurencyDicId { get; set; }

            [Display(Name = "SWIFT")]
            public string BankSwift { get; set; }

            [Display(Name = "БИН")]
            public string Bin { get; set; }
            public bool IsResident { get; set; }
            public Nullable<System.Guid> PayerTypeDicId { get; set; }
            public string BossLastName { get; set; }
            public string BossFirstName { get; set; }
            public string BossMiddleName { get; set; }

            [Display(Name = "Расчетный счет")]
            public string PaymentBill { get; set; }

            [Display(Name = "ИИН")]
            public string Iin { get; set; }

            [Display(Name = "БИК")]
            public string BankBik { get; set; }
        }

        public object Clone()
        {
            var clone = new Organization()
            {
                Id = Guid.NewGuid(),
                Type = this.Type,
                NameKz = this.NameKz,
                NameRu = this.NameRu,
                NameEn = this.NameEn,
                CountryDicId = this.CountryDicId,
                AddressLegal = this.AddressLegal,
                AddressFact = this.AddressFact,
                Phone = this.Phone,
                Fax = this.Fax,
                Email = this.Email,
                BossFio = this.BossFio,
                BossPosition = this.BossPosition,
                ContactFio = this.ContactFio,
                ContactPosition = this.ContactPosition,
                ContactPhone = this.ContactPhone,
                ContactFax = this.ContactFax,
                ContactEmail = this.ContactEmail,
                OrgManufactureTypeDicId = this.OrgManufactureTypeDicId,
                DocNumber = this.DocNumber,
                DocDate = this.DocDate,
                DocExpiryDate = this.DocExpiryDate,
                ObjectId = this.ObjectId,
                OpfTypeDicId = this.OpfTypeDicId,
                BankName = this.BankName,
                BankIik = this.BankIik,
                BankCurencyDicId = this.BankCurencyDicId,
                BankSwift = this.BankSwift,
                Bin = this.Bin,
                IsResident = this.IsResident,
                PayerTypeDicId = this.PayerTypeDicId,
                BossLastName = this.BossLastName,
                BossFirstName = this.BossFirstName,
                BossMiddleName = this.BossMiddleName,
                PaymentBill = this.PaymentBill,
                Iin = this.Iin,
                BankBik = this.BankBik,
                OriginalOrgId = this.Id
            };
            return clone;
        }
    }
}