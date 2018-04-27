using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Стоимость работ
    /// </summary>
    public class CostWork : BaseEntity
    {
        /// <summary>
        /// Прайс лист
        /// </summary>
        public int? PriceListId { get; set; }
        [ForeignKey("PriceListId")]
        public virtual Ref_PriceList Ref_PriceList { get; set; }

        /// <summary>
        /// Id Договора
        /// </summary>
        public int ContractId { get; set; }
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; }

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
