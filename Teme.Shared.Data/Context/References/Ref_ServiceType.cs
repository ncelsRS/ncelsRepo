using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Teme.Shared.Data.Context.References
{
    /// <summary>
    /// Тип услуги для калькулятора(справчоник)
    /// </summary>
    public class Ref_ServiceType : Reference
    {
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Ref_ServiceType Parent { get; set; }
        [Required]
        public int ApplicationTypeId { get; set; }
        [ForeignKey("ApplicationTypeId")]
        public virtual Ref_ApplicationType Ref_ApplicationType { get; set; }
        public virtual ICollection<Ref_ServiceType> Children { get; set; }
        public virtual ICollection<Ref_PriceList> Ref_PriceList { get; set; }
    }
}
