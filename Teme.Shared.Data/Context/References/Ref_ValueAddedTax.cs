using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context.References
{
    /// <summary>
    /// Налог на добавленную стоимость
    /// </summary>
    public class Ref_ValueAddedTax : BaseEntity
    {
        public decimal Value { get; set; }
        public int Year { get; set; }
        public bool IsDeleted { get; set; }
    }
}
