using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
    public class DeclarantDetail : BaseEntity
    {
        /// <summary>
        /// Заявитель
        /// </summary>
        public int DeclarantId { get; set; }
        [ForeignKey("DeclarantId")]
        public Declarant Declarant { get; set; }

        /// <summary>
        /// Юридический адрес
        /// </summary>
        [MaxLength(255)]
        public string LegalAddress { get; set; }

        /// <summary>
        /// Фактический адрес
        /// </summary>
        [MaxLength(255)]
        public string FactAddress { get; set; }

        /// <summary>
        /// Фамилия первого руководителя
        /// </summary>
        [MaxLength(255)]
        public string BossLastName { get; set; }

        /// <summary>
        /// Имя первого руководителя
        /// </summary>
        [MaxLength(255)]
        public string BossFirstName { get; set; }

        /// <summary>
        /// Отчество первого руководителя
        /// </summary>
        [MaxLength(255)]
        public string BossMiddleName { get; set; }

        /// <summary>
        /// Должность первого руководителя на русском языке
        /// </summary>
        [MaxLength(255)]
        public string BossPositionRu { get; set; }

        /// <summary>
        /// Должность первого руководителя на государственном языке
        /// </summary>
        [MaxLength(255)]
        public string BossPositionKz { get; set; }

        /// <summary>
        /// Наименования банка
        /// </summary>
        [MaxLength(255)]
        public string BankName { get; set; }

        /// <summary>
        /// Расчетный счет и ИИК
        /// </summary>
        [MaxLength(255)]
        public string BankIik { get; set; }

        /// <summary>
        /// Бик и SWIFT
        /// </summary>
        [MaxLength(255)]
        public string BankSwift { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BankBin { get; set; }

        /// <summary>
        /// Валюта
        /// </summary>
        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Ref_Currency Ref_Currency { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(20)]
        public string Phone2 { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [MaxLength(255)]
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
        [MaxLength(255)]
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

        public virtual ICollection<Contract> DeclarantDetailContract { get; set; }
        public virtual ICollection<Contract> ManufacturDetailContract { get; set; }
        public virtual ICollection<Contract> PayerDetailContract { get; set; }
    }
}
