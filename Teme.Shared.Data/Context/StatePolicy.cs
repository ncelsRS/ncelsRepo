namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Видимости и разрешения
    /// </summary>
    public class StatePolicy : BaseEntity
    {
        /// <summary>
        /// Ид договора
        /// </summary>
        public int ContractId { get; set; }
        /// <summary>
        /// Ид скоупа из воркфлоу или Ид разрешения
        /// </summary>
        public int PermissionScopeId { get; set; }
        /// <summary>
        /// Статус для текущего правила
        /// </summary>
        public string Status { get; set; }
    }
}