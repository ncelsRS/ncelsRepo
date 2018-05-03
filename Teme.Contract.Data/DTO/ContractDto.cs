using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Primitives.Contract;

namespace Teme.Contract.Data.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class ContractDto
    {
        public int Id { get; set; }
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
        public string Number { get; set; }

        /// <summary>
        /// Является производителем
        /// </summary>
        public bool DeclarantIsManufacture { get; set; }

        /// <summary>
        /// Наименование ИМН/МТ на русском языке
        /// </summary>
        public string MedicalDeviceNameRu { get; set; }

        /// <summary>
        /// Наименование ИМН/МТ на государственном языке
        /// </summary>
        public string MedicalDeviceNameKz { get; set; }

        /// <summary>
        /// Заявитель
        /// </summary>
        public int? DeclarantId { get; set; }

        /// <summary>
        /// Детали заявиетля
        /// </summary>
        public int? DeclarantDetailId { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public int? ManufacturId { get; set; }

        /// <summary>
        /// Детали производитель
        /// </summary>
        public int? ManufacturDetailId { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public int? PayerId { get; set; }

        /// <summary>
        /// Детали Плательщика
        /// </summary>
        public int? PayerDetailId { get; set; }

        public IEnumerable<CostWorkDto> CostWorkDto { get; set; }
    }
}
