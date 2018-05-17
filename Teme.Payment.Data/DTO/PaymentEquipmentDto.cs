using System;
using System.Collections.Generic;
using System.Text;
using Teme.Shared.Data.Context.References;

namespace Teme.Payment.Data.DTO
{
    public class PaymentEquipmentDto
    {
        /// <summary>
        /// Заявка на платеж
        /// </summary>
        public int PaymentId { get; set; }
        
        /// <summary>
        /// Тип комплектации ИМН и МТ
        /// </summary>
        public int EquipmentTypeId { get; set; }

        public Ref_EquipmentType Ref_EquipmentType { get; set; }

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
  
        public Ref_Country Ref_Country { get; set; }
    }
}
