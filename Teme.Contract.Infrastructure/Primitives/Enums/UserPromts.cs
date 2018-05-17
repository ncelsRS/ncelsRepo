namespace Teme.Contract.Infrastructure.Primitives.Enums
{
    public class UserPromts
    {
        /// <summary>
        /// Действия, доступные Декларанту
        /// </summary>
        public class Declarant
        {
            /// <summary>
            /// Отправить в НЦЭЛС
            /// </summary>
            public const string SendOrRemove = "SendOrRemove";
        }
         /// <summary>
         /// Выбрать исполнителей
         /// </summary>
        public const string SelectExecutors = "SelectExecutors";
        /// <summary>
        /// Проверить соответствия требованиям
        /// </summary>
        public const string IsMeetRequirements = "IsMeetRequirements";
        /// <summary>
        /// Согласование Руководителя ЦОЗ
        /// </summary>
        public const string CozBossAgreements = "CozBossAgreements";
        /// <summary>
        /// Согласование ЗамГенДир
        /// </summary>
        public const string CeoAgreements = "CeoAgreements";
        /// <summary>
        /// Возврат заявителю 
        /// </summary>
        public const string ReturnToDeclarant = "ReturnToDeclarant";
        /// <summary>
        /// Регистрация договора
        /// </summary>
        public const string RegisterContract = "RegisterContract";

    }
}
