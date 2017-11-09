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
    }
}
