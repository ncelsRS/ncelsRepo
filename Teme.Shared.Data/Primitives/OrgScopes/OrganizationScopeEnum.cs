using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Primitives.OrgScopes
{
    /// <summary>
    /// Описание всех структурных подразделений Теми
    /// </summary>
    public class OrganizationScopeEnum
    {
        /// <summary>
        /// Доступ к Identity серверу для продления аутентификации
        /// </summary>
        public const string Identity = "identity";

        /// <summary>
        /// Обязательное использование для всех пользователей для всех пользователей
        /// </summary>
        public const string Common = "common";

        /// <summary>
        /// Внешний портал Теми
        /// </summary>
        public const string Ext = "ext";

        /// <summary>
        /// Центр Обратоки Заявлений
        /// </summary>
        public const string Coz = "coz";

        /// <summary>
        /// Группа валидации
        /// </summary>
        public const string Gv = "gv";

    }
}
