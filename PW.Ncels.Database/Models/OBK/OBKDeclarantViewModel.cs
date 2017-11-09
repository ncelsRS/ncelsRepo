using System;

namespace PW.Ncels.Database.Models.OBK
{
    public class OBKDeclarantViewModel
    {
        public Guid? Id { get; set; }
        public Guid? OrganizationFormId { get; set; }
        public string NameKz { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
        public Guid? CountryId { get; set; }
        public string Bin { get; set; }
        public bool IsResident { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
