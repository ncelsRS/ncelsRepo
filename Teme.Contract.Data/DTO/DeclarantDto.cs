
namespace Teme.Contract.Data.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class DeclarantDto
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

        public DeclarantDetailDto DeclarantDetailDto { get; set; }
    }
}
