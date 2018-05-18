using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Shared.Data.Primitives.Statuses
{
    /// <summary>
    /// Статусы для договора внешнего потарал
    /// </summary>
    public class ExtContractStatus
    {
        /// <summary>
        /// Черновик
        /// </summary>
        public const string Draft = "draft";

        /// <summary>
        /// В обработке
        /// </summary>
        public const string InProcessing = "inProcessing";

        /// <summary>
        /// В работе
        /// </summary>
        public const string InWork = "inWork";

        /// <summary>
        /// На корректировке у заявителя
        /// </summary>
        public const string OnAdjustment = "onAdjustment";

        /// <summary>
        /// На формировании счета на оплату
        /// </summary>
        public const string FormationInvoice = "formationInvoice";

        /// <summary>
        /// Активный
        /// </summary>
        public const string Active = "active";

        /// <summary>
        /// Истекший
        /// </summary>
        public const string Expired = "expired";
    }
}
