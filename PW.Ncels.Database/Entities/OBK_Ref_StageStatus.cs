using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PW.Ncels.Database.DataModel
{
    public partial class OBK_Ref_StageStatus
    {
        /// <summary>
        /// Статус "На распределение"
        /// </summary>
        public const string New = "inQueue";
        /// <summary>
        /// Статус "В работе"
        /// </summary>
        public const string InWork = "inWork";
        /// <summary>
        /// Статус "На даработке"
        /// </summary>
        public const string InReWork = "inReWork";
        /// <summary>
        /// Статус "Выполнен"
        /// </summary>
        public const string Completed = "completed";
        /// <summary>
        /// На корректировке у заявителя
        /// </summary>
        public const string OnCorrection = "onCorrection";
        /// <summary>
        /// На согласовании у руководителя
        /// </summary>
        public const string OnAgreement = "onAgreement";
        /// <summary>
        /// Несогласованный
        /// </summary>
        public const string NotAgreed = "notAgreed";
        /// <summary>
        /// Требует регистрации
        /// </summary>
        public const string RequiresRegistration = "requiresRegistration";
        /// <summary>
        /// Требует подписания
        /// </summary>
        public const string RequiresSigning = "requiresSigning";
        /// <summary>
        /// Активный
        /// </summary>
        public const string Active = "active";
        /// <summary>
        /// Требует заключения
        /// </summary>
        public const string RequiresConclusion = "requiresConclusion";
        /// <summary>
        /// На Экспертихе документов
        /// </summary>
        public const string OnExpDocument = "onExpDocument";
        /// <summary>
        /// Экспертиза документов завершена
        /// </summary>
        public const string DocumentReviewCompleted = "documentReviewCompleted";
        /// <summary>
        /// На оценке производства
        /// </summary>
        public const string OnOP = "OnOP";
        /// <summary>
        /// Требует выдачи копии ЗБК
        /// </summary>
        public const string RequiresIssuingZBKCopy = "requiresIssuingZBKCopy";
        /// <summary>
        /// Оценка производства завершена
        /// </summary>
        public const string OPCompleted = "OPCompleted";

        #region статусы для задания

        /// <summary>
        /// статус "Сформирован"
        /// </summary>
        public const string TaskNew = "taskNew";

        /// <summary>
        /// статус "Образцы приняты ЦОЗ"
        /// </summary>
        public const string TaskAcceptCoz = "taskAcceptCoz";

        /// <summary>
        /// статус "Передано в ИЦл"
        /// </summary>
        public const string TaskSendRC = "taskSendRC";

        /// <summary>
        /// статус "Принято ИЦл"
        /// </summary>
        public const string TaskAcceptRC = "taskAcceptRC";

        /// <summary>
        /// статус "Переданов лабораторию"
        /// </summary>
        public const string TaskSendLab = "taskSendLab";

        #endregion
    }
}
