using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }
        /// <summary>
        /// Ид разрешения
        /// </summary>
        public string Permission { get; set; }
        /// <summary>
        /// Ид скоупа из воркфлоу
        /// </summary>
        public string Scope { get; set; }
        /// <summary>
        /// Статус для текущего правила
        /// </summary>
        public string Status { get; set; }
    }
}