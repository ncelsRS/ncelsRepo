using System;

namespace Teme.Contract.Data.DTO
{
    public class DeclarantDetailDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Юридический адрес
        /// </summary>
        public string LegalAddress { get; set; }

        /// <summary>
        /// Фактический адрес
        /// </summary>
        public string FactAddress { get; set; }

        /// <summary>
        /// Фамилия первого руководителя
        /// </summary>
        public string BossLastName { get; set; }

        /// <summary>
        /// Имя первого руководителя
        /// </summary>
        public string BossFirstName { get; set; }

        /// <summary>
        /// Отчество первого руководителя
        /// </summary>
        public string BossMiddleName { get; set; }

        /// <summary>
        /// Должность первого руководителя на русском языке
        /// </summary>
        public string BossPositionRu { get; set; }

        /// <summary>
        /// Должность первого руководителя на государственном языке
        /// </summary>
        public string BossPositionKz { get; set; }

        /// <summary>
        /// Наименования банка
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Расчетный счет и ИИК
        /// </summary>
        public string BankIik { get; set; }

        /// <summary>
        /// Бик и SWIFT
        /// </summary>
        public string BankSwift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BankBin { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public int? CurrencyId { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        public string Phone2 { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Действующее на основании
        /// </summary>
        public int DeclarantDocType { get; set; }

        /// <summary>
        /// Без номера
        /// </summary>
        public bool DeclarantDocWithoutNumber { get; set; }

        /// <summary>
        /// № документа *
        /// </summary>
        public string DeclarantDocNumber { get; set; }

        /// <summary>
        /// Дата начала действия
        /// </summary>
        public DateTime? DeclarantDocStartDate { get; set; }

        /// <summary>
        /// Дата окончание
        /// </summary>
        public DateTime? DeclarantDocEndDate { get; set; }

        /// <summary>
        /// Бессрочный
        /// </summary>
        public bool DeclarantPerpetualDoc { get; set; }
    }
}
