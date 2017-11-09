namespace PW.Ncels.Database.DataModel
{
    using System;
    using System.Collections.Generic;

    public partial class EXP_DIC_Status
    {
        /// <summary>
        /// Экспертиза завершена
        /// </summary>
        public const string Completed = "7";
        /// <summary>
        /// Инициированна процедура отказа
        /// </summary>
        public const string OnRefusing = "9";
        /// <summary>
        /// Отказанно в регистрации
        /// </summary>
        public const string Refused = "10";
    }
}
