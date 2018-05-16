using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Contract.Infrastructure.Primitives
{
    public class UserOptions
    {
        /// <summary>
        /// Удалить договор 
        /// </summary>
        public const string Delete = "Delete";

        /// <summary>
        /// Отправить с подписью
        /// </summary>
        public const string SendWithSign = "sendWithSign";

        /// <summary>
        /// Отпарвить без подписи
        /// </summary>
        public const string SendWithoutSign = "sendWithoutSign";

        /// <summary>
        /// Выбрать исполнителя или исполнителей
        /// </summary>
        public const string SelectExecutors = "selectExecutors";

        /// <summary>
        /// Соответсвует требованиям
        /// </summary>
        public const string MeetRequirements = "MeetRequirements";

        /// <summary>
        /// НЕ Соответсвует требованиям
        /// </summary>
        public const string NotMeetRequirements = "NotMeetRequirements";

        ///// <summary>
        ///// Согласование руководителем ЦОЗ
        ///// </summary>
        //public const string CozBossAgreementsOption = "CozBossAgreementsOption";

        ///// <summary>
        ///// Согласование ЗамГенДир
        ///// </summary>
        //public const string CeoAgreedOption = "CeoAgreedOption";

        /// <summary>
        /// Возврат заявителю 
        /// </summary>
        public const string ReturnToDeclarant = "ReturnToDeclarant";
    }
}
