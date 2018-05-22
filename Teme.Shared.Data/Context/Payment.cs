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
        /// Тип договора
        /// </summary>
        public ContractFormEnum? ContractForm { get; set; }

        /// <summary>
        /// Номер регистрационного удостоверения
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Дата регисрации регистрационного удостоверения
        /// </summary>
        public DateTime? CardBeginDate { get; set; }

        /// <summary>
        /// Срок действия регистрационного удостоверения
        /// </summary>
        public DateTime? CardEndDate { get; set; }

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
        /// Область применения на государственном языке
        /// </summary>
        public string ApplicationAreaKz { get; set; }

        /// <summary>
        /// Область применения на русском языке
        /// </summary>
        public string ApplicationAreaRu { get; set; }

        /// <summary>
        /// Назначение на государственном языке
        /// </summary>
        public string AppointmentKz { get; set; }

        /// <summary>
        /// Назначение на русском языке
        /// </summary>
        public string AppointmentRu { get; set; }

        /// <summary>
        /// Закрытая система
        /// </summary>
        public bool? IsClosedSystem { get; set; }

        /// <summary>
        /// Обоснование от производителя (указать страницу регистрационного досье)
        /// </summary>
        public string RationaleManufacturer { get; set; }

        /// <summary>
        /// Класс в зависимости от степени потенциального риска применения
        /// </summary>
        public int? DegreeRiskClassId { get; set; }
        [ForeignKey("DegreeRiskClassId")]
        public virtual Ref_DegreeRiskClass Ref_DegreeRiskClass { get; set; }

        /// <summary>
        /// Бланк
        /// </summary>
        public bool? IsBlank { get; set; }

        /// <summary>
        /// Средство измерения
        /// </summary>
        public bool? IsMeasures { get; set; }

        /// <summary>
        /// ИМН и МТ для ин витро диагностики
        /// </summary>
        public bool? IsDiagnostics { get; set; }

        /// <summary>
        /// Стирильное
        /// </summary>
        public bool? IsStyryl { get; set; }

        /// <summary>
        /// В наличие лекарственное средства
        /// </summary>
        public bool? IsPresenceMedicinalProduct { get; set; }

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
        public virtual ICollection<PaymentPlatform> PaymentPlatforms { get; set; }

    }
}
