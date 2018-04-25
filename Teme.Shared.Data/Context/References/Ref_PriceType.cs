using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context.References
{
    /// <summary>
    /// Единица измерения(справочник) для калькулятора
    /// </summary>
    public class Ref_PriceType : Reference
    {
        public virtual ICollection<Ref_PriceList> Ref_PriceList { get; set; }
    }
}
