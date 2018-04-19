using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context.References
{
    /// <summary>
    /// Банки(справочник)
    /// </summary>
    public class Ref_Bank :Reference
    {
        /// <summary>
        /// Признак подвержедения ЦОЗ
        /// </summary>
        public bool IsConfirmed { get; set; } = false;
    }
}
