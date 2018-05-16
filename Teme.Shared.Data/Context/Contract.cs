using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Договор
    /// </summary>
    public class Contract : BaseEntity
    {
        [MaxLength(50)]
        public string WorkflowId { get; set; }
        public ContractTypeEnum ContractType { get; set; }

        /// <summary>
        /// Тип договора
        /// </summary>
        public ContractFormEnum ContractForm { get; set; }

        /// <summary>
        /// Договор составляется с
        /// </summary>
        public HolderTypeEnum HolderType { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public ChosenPayerEnum ChoosePayer { get; set; }

        /// <summary>
        /// scope ЕАЭС или Нац
        /// </summary>
        public string ContractScope { get; set; }

        /// <summary>
        /// Номер договора
        /// </summary>
        [MaxLength(255)]
        public string Number { get; set; }

        /// <summary>
        /// Является производителем
        /// </summary>
        public bool DeclarantIsManufacture { get; set; } = false;

        /// <summary>
        /// Наименование ИМН/МТ на русском языке
        /// </summary>
        [MaxLength(255)]
        public string MedicalDeviceNameRu { get; set; }

        /// <summary>
        /// Наименование ИМН/МТ на государственном языке
        /// </summary>
        [MaxLength(255)]
        public string MedicalDeviceNameKz { get; set; }

        /// <summary>
        /// Заявитель
        /// </summary>
        public int? DeclarantId { get; set; }
        [ForeignKey("DeclarantId")]
        public virtual Declarant Declarant { get; set; }

        /// <summary>
        /// Детали заявиетля
        /// </summary>
        public int? DeclarantDetailId { get; set; }
        [ForeignKey("DeclarantDetailId")]
        public virtual DeclarantDetail DeclarantDetail { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public int? ManufacturId { get; set; }
        [ForeignKey("ManufacturId")]
        public virtual Declarant Manufactur { get; set; }

        /// <summary>
        /// Детали производитель
        /// </summary>
        public int? ManufacturDetailId { get; set; }
        [ForeignKey("ManufacturDetailId")]
        public virtual DeclarantDetail ManufacturDetail { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public int? PayerId { get; set; }
        [ForeignKey("PayerId")]
        public virtual Declarant Payer { get; set; }

        /// <summary>
        /// Детали Плательщика
        /// </summary>
        public int? PayerDetailId { get; set; }
        [ForeignKey("PayerDetailId")]
        public virtual DeclarantDetail PayerDetail { get; set; }

        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool isDeleted { get; set; } = false;

        public virtual ICollection<CostWork> CostWorks { get; set; }
        public virtual ICollection<UserForAction> UserForActions { get; set; }
        public virtual ICollection<StatePolicy> StatePolicies { get; set; }
    }
}
