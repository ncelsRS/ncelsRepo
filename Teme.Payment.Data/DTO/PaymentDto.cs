using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Payment.Data.DTO
{
    public class PaymentDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Id Договора
        /// </summary>
        public int ContractId { get; set; }

        public string ContractNumber { get; set; }
        public DateTime ContractDateCreate { get; set; }

        /// <summary>
        /// Тип договора
        /// </summary>
        public ContractFormEnum? ContractForm { get; set; }

        /// <summary>
        /// Номер регистрационного удостоверения
        /// </summary>
        public string CardNumber { get; set; }

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

        public virtual IEnumerable<PaymentEquipmentDto> PaymentEquipmentDtos { get; set; }
        public virtual IEnumerable<PaymentPackagingDto> PaymentPackagingDtos { get; set; }
        public virtual IEnumerable<PaymentPlatformDto> PaymentPlatformDtos { get; set; }
    }
}
