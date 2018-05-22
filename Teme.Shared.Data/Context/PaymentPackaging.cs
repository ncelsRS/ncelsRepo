using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Shared.Data.Context
{
    /// <summary>
    /// Упаковка
    /// </summary>
    public class PaymentPackaging : BaseEntity
    {
        /// <summary>
        /// Заявка на платеж
        /// </summary>
        public int PaymentId { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }

        /// <summary>
        /// заявление
        /// </summary>
        public Declaration Declaration { get; set; }

        /// <summary>
        /// Тип упаковки
        /// </summary>
        public int PackagingtTypeId { get; set; }
        [ForeignKey("PackagingTypeId")]
        public Ref_PackagingType Ref_PackagingType { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Размер Ширина
        /// </summary>
        public string SizeWidth { get; set; }

        /// <summary>
        /// Размер Высота
        /// </summary>
        public string SizeHeight { get; set; }

        /// <summary>
        /// Размер Длина
        /// </summary>
        public string SizeLength { get; set; }

        /// <summary>
        /// Размер Еденица измерения
        /// </summary>
        public Nullable<int> SizeMeasureId { get; set; }
        [ForeignKey("SizeMeasureId")]
        public Ref_Measure Ref_SizeMeasure { get; set; }

        /// <summary>
        /// Объем значение
        /// </summary>
        public string VolumeValue { get; set; }

        /// <summary>
        /// Объем Еденица измерения
        /// </summary>
        public Nullable<int> VolumeMeasureId { get; set; }
        [ForeignKey("VolumeMeasureId")]
        public Ref_Measure Ref_VolumeMeasure { get; set; }

        /// <summary>
        /// Кол-во ед. в упаковке
        /// </summary>
        public string NumberUnitsInBox { get; set; }

        /// <summary>
        /// Краткое описание
        /// </summary>
        public string ShortDescription { get; set; }
    }
}
