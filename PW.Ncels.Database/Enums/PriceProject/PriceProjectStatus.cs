namespace PW.Ncels.Database.Enums.PriceProject{

    public enum PriceProjectStatus{

        /// <summary>
        /// Черновик
        /// </summary>
        Draft = 0,

        /// <summary>
        /// Зарегистрированный
        /// </summary>
        Registered = 1,

        /// <summary>
        /// На анализе
        /// </summary>
        OnAnalize = 2,

        /// <summary>
        /// Переговоры
        /// </summary>
        Conversation = 3,

        /// <summary>
        /// На доработке у заявителя
        /// </summary>
        OnRevision = 4,

        /// <summary>
        /// Завершено
        /// </summary>
        Finished = 5,

        /// <summary>
        /// Отказные
        /// </summary>
        Rejected = 6,

        /// <summary>
        /// На регистрации
        /// </summary>
        OnRegistration = 7

    }
}
