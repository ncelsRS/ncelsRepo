using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Contract.Infrastructure.Primitives.Enums
{
    public class ScopeEnum
    {
        /// <summary>
        /// Корневая область
        /// </summary>
        public const string Root = "Root";
        /// <summary>
        /// Выполнение в Группе Валидации
        /// </summary>
        public const string Gv = "Gv";
        /// <summary>
        /// Выполнение в ЦОЗ
        /// </summary>
        public const string Coz = "Coz";
    }
}
