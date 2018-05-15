using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Contract.Data.DTO
{
    public class CostWorkDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Прайс лист
        /// </summary>
        public int? PriceListId { get; set; }

        public string NameRu { get; set; }

        public string NameKz { get; set; }

        /// <summary>
        /// Id Договора
        /// </summary>
        public int ContractId { get; set; }

        /// <summary>
        /// Количетсво
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Стоимость отечественного/зарубежного производителя
        /// </summary>
        public bool IsImport { get; set; }

        /// <summary>
        /// Цена с НДС
        /// </summary>
        public decimal? PriceWithValueAddedTax { get; set; }

        /// <summary>
        /// Общая стоимость с НДС
        /// </summary>
        public decimal? TotalPriceWithValueAddedTax { get; set; }
    }
}
