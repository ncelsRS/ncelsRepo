using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Teme.Shared.Data.Context.References
{
    /// <summary>
    /// Прайс лист для калькулятора
    /// </summary>
    public class Ref_PriceList : BaseEntity
    {
        [Required]
        public bool IsImport { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public int ServiceTypeId { get; set; }
        [ForeignKey("ServiceTypeId")]
        public virtual Ref_ServiceType Ref_ServiceType { get; set; }
        [Required]
        public int PriceTypeId { get; set; }
        [ForeignKey("PriceTypeId")]
        public virtual Ref_PriceType Ref_PriceType { get; set; }

        public virtual ICollection<CostWork> CostWorks { get; set; }
    }
}
