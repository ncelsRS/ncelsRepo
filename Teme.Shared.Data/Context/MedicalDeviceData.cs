using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Данные ИМН/МТ
    /// </summary>
    public class MedicalDeviceData : BaseEntity
    {
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
        /// № НД
        /// </summary>
        public string NdNumber {get;set;}

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
    }
}
