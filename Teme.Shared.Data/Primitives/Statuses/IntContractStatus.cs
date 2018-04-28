using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Primitives.Statuses
{
    /// <summary>
    /// Статусы для договора внутреннего потарал
    /// </summary>
    public class IntContractStatus
    {
        /// <summary>
        /// На распеделнии
        /// </summary>
        public const string OnDistribution = "onDistribution";

        /// <summary>
        /// В работе
        /// </summary>
        public const string InWork = "inWork";

        /// <summary>
        /// Требует согласования
        /// </summary>
        public const string RequiredAgreement = "requiredAgreement";

        /// <summary>
        /// Согласованные
        /// </summary>
        public const string OnAgreement = "onAgreement";

        /// <summary>
        /// Требудет регистрации
        /// </summary>
        public const string RequiredRegistration = "requiredRegistration";

        /// <summary>
        /// На корректировке у заявителя
        /// </summary>
        public const string OnAdjustment = "onAdjustment";

        /// <summary>
        /// Активный
        /// </summary>
        public const string Active = "active";

        /// <summary>
        /// Истекший
        /// </summary>
        public const string Expired = "expired";

        /// <summary>
        /// Все
        /// </summary>
        public const string All = "all";
    }
}
