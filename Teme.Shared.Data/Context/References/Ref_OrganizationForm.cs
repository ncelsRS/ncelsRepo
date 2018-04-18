using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Context.References
{
    /// <summary>
    /// Организационная форма(справочник)
    /// </summary>
    public class Ref_OrganizationForm : Reference
    {
        /// <summary>
        /// Признак подвержедения ЦОЗ
        /// </summary>
        public bool IsConfirmed { get; set; } = false;
    }
}
