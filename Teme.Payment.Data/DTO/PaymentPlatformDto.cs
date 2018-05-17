using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Payment.Data.DTO
{
    public class PaymentPlatformDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public int? CountryId { get; set; }

        public string NameRu { get; set; }
        public string NameKz { get; set; }
        public string NameEn { get; set; }

        /// <summary>
        /// Юридический адрес
        /// </summary>
        public string LegalAddress { get; set; }

        /// <summary>
        /// Фактический адрес
        /// </summary>
        public string FactAddress { get; set; }
    }
}
