using System.ComponentModel.DataAnnotations.Schema;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Договор
    /// </summary>
    public class Contract : BaseEntity
    {
        public string WorkflowId { get; set; }
        /// <summary>
        /// Заявитель
        /// </summary>
        public int? DeclarantId { get; set; }
        [ForeignKey("DeclarantId")]
        public Declarant Declarant { get; set; }
        /// <summary>
        /// Детали заявиетля
        /// </summary>
        public int? DeclarantDetailId { get; set; }
        [ForeignKey("DeclarantDetailId")]
        public DeclarantDetail DeclarantDetail { get; set; }
        /// <summary>
        /// Производитель
        /// </summary>
        public int? ManufacturId { get; set; }
        [ForeignKey("ManufacturId")]
        public Declarant Manufactur { get; set; }
        /// <summary>
        /// Детали производитель
        /// </summary>
        public int? ManufacturDetailId { get; set; }
        [ForeignKey("ManufacturDetailId")]
        public DeclarantDetail ManufacturDetail { get; set; }
        /// <summary>
        /// Плательщик
        /// </summary>
        public int? PayerId { get; set; }
        [ForeignKey("PayerId")]
        public Declarant Payer { get; set; }
        /// <summary>
        /// Детали Плательщика
        /// </summary>
        public int? PayerDetailId { get; set; }
        [ForeignKey("PayerDetailId")]
        public DeclarantDetail PayerDetail { get; set; }

    }
}