namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DIC_StageResult
    {
        /// <summary>
        /// Соответствует
        /// </summary>
        public static readonly int Matches = 1;
        /// <summary>
        /// Не соответствует
        /// </summary>
        public static readonly int DoesNotMatch = 2;
        /// <summary>
        /// Снять с регистрации
        /// </summary>
        public static readonly int RemoveFromRegistration = 3;

        /// <summary>
        /// Рекомендовать
        /// </summary>
        public static readonly int Recommended = 4;

        /// <summary>
        /// Не рекомендовать
        /// </summary>
        public static readonly int DoesNotRecommended = 5;

        /// <summary>
        /// Рассмотреть документы повторно
        /// </summary>
        public static readonly int NeedMoreWork = 6;

        /// <summary>
        /// Решение Экспертного Совета
        /// </summary>
        public static readonly int NeedExpertCouncilConclusion = 7;


        /// <summary>
        /// Соответствует
        /// </summary>
        public static readonly string MatchesCode = "1";
        /// <summary>
        /// Не соответствует
        /// </summary>
        public static readonly string DoesNotMatchCode = "2";
        /// <summary>
        /// Снять с регистрации
        /// </summary>
        public static readonly string RemoveFromRegistrationCode = "3";

        /// <summary>
        /// Рекомендовать
        /// </summary>
        public static readonly string RecommendedCode = "4";

        /// <summary>
        /// Не рекомендовать
        /// </summary>
        public static readonly string DoesNotRecommendedCode = "5";

        /// <summary>
        /// Рассмотреть документы повторно
        /// </summary>
        public static readonly string NeedMoreWorkCode = "6";

        /// <summary>
        /// Решение Экспертного Совета
        /// </summary>
        public static readonly string NeedExpertCouncilConclusionCode = "7";
    }
}