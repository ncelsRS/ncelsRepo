using System;
using System.Collections.Generic;
using System.Text;

namespace Teme.Contract.Data.Model
{
    public class DeclarantModel
    {
        public int Id { get; set; }
        public string IdNumber { get; set; }
        public string NameRu { get; set; }
        public string NameKz { get; set; }
        public string NameEn { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        public int? CountryId { get; set; }
        /// <summary>
        /// Огранизационная форма
        /// </summary>
        public int? OrganizationFormId { get; set; }
        /// <summary>
        /// true резидент, false не резидент
        /// </summary>
        public bool IsResident { get; set; }
        /// <summary>
        /// подтверждени
        /// </summary>
        public bool IsConfirmed { get; set; }

        public DeclarantDetailModel DeclarantDetailModel { get; set; }
    }
}
