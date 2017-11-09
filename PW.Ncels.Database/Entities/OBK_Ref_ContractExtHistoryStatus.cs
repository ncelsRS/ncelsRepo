namespace PW.Ncels.Database.DataModel
{
    public partial class OBK_Ref_ContractExtHistoryStatus
    {
        /// <summary>
        /// Черновик
        /// </summary>
        public const string Draft = "draft";
        /// <summary>
        /// В обработке
        /// </summary>
        public const string Inprocessing = "inprocessing";
        /// <summary>
        /// В работе
        /// </summary>
        public const string Work = "work";
        /// <summary>
        /// На корректировке у заявителя
        /// </summary>
        public const string Oncorrection = "oncorrection";
        /// <summary>
        /// Активный
        /// </summary>
        public const string Active = "active";
    }
}
