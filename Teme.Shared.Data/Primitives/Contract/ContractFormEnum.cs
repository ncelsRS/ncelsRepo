using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Primitives.Contract
{
    /// <summary>
    /// Тип договора
    /// </summary>
    public enum ContractFormEnum
    {
        /// <summary>
        /// Регистрация
        /// </summary>
        Registration = 1,

        /// <summary>
        /// Перерегистрация
        /// </summary>
        Reregistration = 2,

        /// <summary>
        /// Внесение изменений
        /// </summary>
        Modification = 3
    }
}
