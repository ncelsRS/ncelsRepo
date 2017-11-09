namespace PW.Ncels.Database.DataModel
{
    public partial class OBK_Ref_ContractHistoryStatus
    {
        /// <summary>
        /// Отправлен заявителем
        /// </summary>
        public const string SentByApplicant = "sentByApplicant";
        /// <summary>
        /// Распределен
        /// </summary>
        public const string SentToWork = "sentToWork";
        /// <summary>
        /// Соответствует требованиям
        /// </summary>
        public const string MeetsRequirements = "meetsRequirements";
        /// <summary>
        /// Не соответствует требованиям
        /// </summary>
        public const string DoesNotMeetRequirements = "doesNotMeetRequirements";
        /// <summary>
        /// Возвращен на доработку
        /// </summary>
        public const string Returned = "returned";
        /// <summary>
        /// Отправлен на согласование
        /// </summary>
        public const string SentToApproval = "sentToApproval";
        /// <summary>
        /// Отказано в согласовании
        /// </summary>
        public const string Refused = "refused";
        /// <summary>
        /// Согласован
        /// </summary>
        public const string Approved = "approved";
        /// <summary>
        /// Подписан
        /// </summary>
        public const string Signed = "signed";
        /// <summary>
        /// Зарегистрирован
        /// </summary>
        public const string Registered = "registered";
        /// <summary>
        /// Прикреплено вложение
        /// </summary>
        public const string Attached = "attached";
    }
}
