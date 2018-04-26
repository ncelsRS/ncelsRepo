using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Primitives.Contract
{
    /// <summary>
    /// Плательщик(спрачокни)
    /// </summary>
    public enum ChosenPayerEnum
    {
        /// <summary>
        /// Заявитель
        /// </summary>
        Declarant = 1,

        /// <summary>
        /// Производитель
        /// </summary>
        Manufactur = 2,

        /// <summary>
        /// Третье лицо
        /// </summary>
        Other = 3
    }
}
