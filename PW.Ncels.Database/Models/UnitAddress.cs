using System;

namespace PW.Ncels.Database.Models
{
    public class UnitAddress
    {
        public Nullable<System.Guid> UnitAddressId { get; set; }
        public Guid RegionId { get; set; }
        public string RegionName { get; set; }
        public string AddressNameRu { get; set; }
        public string AddressNameKz { get; set; }
    }
}
