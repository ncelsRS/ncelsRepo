namespace PW.Ncels.Database.DataModel
{
    public partial class EXP_DIC_StageStatus
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
    }
}