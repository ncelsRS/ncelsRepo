using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Payment.Data.DTO
{
    public class PaymentPackagingDto
    {
        public int Id { get; set; }
       /// <summary>
        /// Тип упаковки
        /// </summary>
        public int PackagingtTypeId { get; set; }

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

        /// <summary>
        /// Объем значение
        /// </summary>
        public string VolumeValue { get; set; }

        /// <summary>
        /// Объем Еденица измерения
        /// </summary>
        public Nullable<int> VolumeMeasureId { get; set; }

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
