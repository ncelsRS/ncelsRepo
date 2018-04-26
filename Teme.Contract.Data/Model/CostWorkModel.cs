using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Teme.Contract.Data.Model
{
    public class CostWorkModel
    {
        [Required]
        public int PriceListId { get; set; }
        [Required]
        public int ContractId { get; set; }

        public int? Count { get; set; }
        [Required]
        public bool IsImport { get; set; }
        [Required]
        public decimal PriceWithValueAddedTax { get; set; }
        [Required]
        public decimal TotalPriceWithValueAddedTax { get; set; }
    }
}
