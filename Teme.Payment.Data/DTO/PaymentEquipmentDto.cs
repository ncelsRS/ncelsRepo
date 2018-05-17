using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Payment.Data.DTO
{
    public class PaymentEquipmentDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Тип комплектации ИМН и МТ
        /// </summary>
        public int EquipmentTypeId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Мордель
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public int? CountryId { get; set; }
    }
}
