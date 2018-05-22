using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Заявка на платеж
    /// </summary>
    public class Payment : BaseEntity
    {
        /// <summary>
        /// Id Договора
        /// </summary>
        public int ContractId { get; set; }
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }

        /// <summary>
        /// Вид регистрации
        /// </summary>
        public ContractFormEnum? PaymentForm { get; set; }

        /// <summary>
        /// Данные ИМН/МТ
        /// </summary>
        public MedicalDeviceData MedicalDeviceData { get; set; }

        /// <summary>
        /// Субъект, осуществляющий оплату за проведение экспертизы
        /// </summary>
        public Declarant Payer { get; set; }

        /// <summary>
        /// Субъект, осуществляющий оплату за проведение экспертизы(детали)
        /// </summary>
        public DeclarantDetail PayerDetail { get; set; }

        /// <summary>
        /// Тип ИМН/МТ
        /// </summary>
        public bool? IsTypeImnMt { get; set; }

        /// <summary>
        /// Торговое название
        /// </summary>
        public string TradeName { get; set; }

        public string NameRu { get; set; }
        public string NameKz { get; set; }

        /// <summary>
        /// Количество модификации ИМН
        /// </summary>
        public string NumberModificationImn { get; set; }

        /// <summary>
        /// Редакция до внесения изменений
        /// </summary>
        public string RevisionBeforeChanges { get; set; }

        /// <summary>
        /// Вносимые изменения
        /// </summary>
        public string ChangesMade { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool isDeleted { get; set; } = false;

        public virtual ICollection<PaymentEquipment> PaymentEquipments { get; set; }
        public virtual ICollection<PaymentPackaging> PaymentPackaging { get; set; }
        //public virtual ICollection<PaymentPlatform> PaymentPlatforms { get; set; }
        public virtual ICollection<MedicalDeviceManufacturer> MedicalDeviceManufacturers { get; set; }

    }
}
